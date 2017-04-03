using System;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Threading;
using System.Windows.Forms;

namespace FileUploader
{
    public partial class Confirm : Form
    {
        public Confirm()
        {
            InitializeComponent();
        }

        private void btnConfirmOK_Click(object sender, EventArgs e)
        {
            // Pack variable options from label values into a new string array.         //
            // Kick off deployed SSIS package using parameters from string array.       //
            // Kick off progress updater on seperate thread.                            //
            // Progress updater method changes OK button the say 'Done' when complete   //
            if (btnConfirmOK.Text != "Done")
            {
                btnConfirmOK.Enabled = false;
                btnConfirmCancel.Enabled = false;

                string[] varImportOptions = { lblConfirmDestinationServer.Text, lblConfirmDestinationDatabase.Text, lblConfirmSourceDirectory.Text };

                initiateLookupTableImport(varImportOptions);

                backgroundWorker1.RunWorkerAsync(executionID);
            }
            else
            {
                Close();
            }
        }

        private void btnConfirmCancel_Click(object sender, EventArgs e)
        {
            // Not a real cancel, is disabled when upload initiated.    //
            this.Close();
        }

        private void lblConfirmSourceDirectory_Click(object sender, EventArgs e)
        {
            this.lblConfirmSourceDirectory.LinkVisited = true;
            System.Diagnostics.Process.Start(lblConfirmSourceDirectory.Text);
        }

        int executionID;
        int status;
        int _status;
        string statusText;
        //string backgroundCompleted;

        public int initiateLookupTableImport(string[] varImportOptions)
        {
            // Unpacks parameters from string array.                                                                        //
            // Establishes connection to package server (myConnection) and runs stored procedure to initiate SSIS package   //
            // with user defined parameters.                                                                                //
            // Returns ExecutionID for use in progress updater method, 'initiatePackageProgress'.                           //
            var dataSource = varImportOptions[0].ToString();
            var initialCatalogue = varImportOptions[1].ToString();

            var destinationConnection = $"Data Source={dataSource};Initial Catalog={initialCatalogue};Provider=SQLNCLI11.1;Integrated Security=SSPI;Auto Translate=False;";
            var sourceDirectory = varImportOptions[2];
            var sourceTxtDirectory = $"{varImportOptions[2]}\\TXTFILES";

            SqlConnection myConnection = new SqlConnection(@"Data Source=IRC-PC010\QSRNVIVO10;Trusted_Connection=Yes;Initial Catalog=CPRD");
            string qryRunPackage = "[dbo].[procLookupTableImport]";

            using (myConnection)
            {
                using (SqlCommand cmdRunPackage = new SqlCommand(qryRunPackage, myConnection))
                {
                    try
                    {
                        cmdRunPackage.CommandType = CommandType.StoredProcedure;

                        cmdRunPackage.Parameters.AddWithValue("@ConnectionString", destinationConnection);
                        cmdRunPackage.Parameters.AddWithValue("@SourceDirectory", sourceDirectory);
                        cmdRunPackage.Parameters.AddWithValue("@SourceTxtDirectory", sourceTxtDirectory);

                        var returnedValue = cmdRunPackage.Parameters.Add("@ReturnVal", SqlDbType.Int);
                        returnedValue.Direction = ParameterDirection.ReturnValue;

                        myConnection.Open();
                        cmdRunPackage.ExecuteNonQuery();
                        executionID = (int)returnedValue.Value;
                        //tbxProgress.AppendText("execution_id = " + executionID);
                        //tbxProgress.AppendText(Environment.NewLine);
                    }
                    catch (Exception ex)
                    {
                        tbxProgress.Text = ex.Message;
                        Console.WriteLine(ex.Message);
                    }
                }
            }
            return (int)executionID;
        }

        delegate void updateProgressTextBoxCallback(string statusText);
        delegate void updateProgressDataTableCallback(DataTable t);
        delegate void updateConfirmOkToDoneCallback();

        public void initiatePackageProgress(BackgroundWorker bw, int executionID)
        {
            // Runs on new thread, is initiated by backgroundWorker1 which passes executionID through.                          //
            // Establishes connection to package server (myConnection) and queries SSISDB views for status (qryExecutionStatus) //
            // and execution events (qryEventMessage).                                                                          //
            // Had to limit execution event messages to file started/file ended events because the data table took too long     //
            // to refresh with all 2,000 messages.                                                                              //
            // Status update uses method 'statusSwitch' to interpret integer status.                                            //
            // Runs for as long as status is Created, Running, Pending or Stopping.                                             //
            // Because it runs on different thread via BackgroundWorker it uses delegates to call                               //
            // 'updateProgressTextBox' for status feedback,                                                                     //
            // 'updateProgressDataTable' for events feedback and                                                                //
            // 'updateConfirmOkToDone' to change/enable OK button to Done.                                                      //

            SqlConnection myConnection = new SqlConnection(@"Data Source=IRC-PC010\QSRNVIVO10;Trusted_Connection=Yes;Initial Catalog=CPRD");
            //string qryEventMessage = "SELECT [message_time], message FROM [SSISDB].[catalog].[event_messages] where operation_id = @executionID order by event_message_id desc";
            string qryEventMessage = "SELECT [message_time], message FROM [SSISDB].[catalog].[event_messages] where operation_id = @executionID and message like '%processing%'	and (message like '%started%' or message like '%ended%') order by event_message_id desc";
            string qryExecutionStatus = "select status from[SSISDB].[catalog].[executions] where execution_id = @executionID ";

            using (myConnection)
            {
                using (SqlCommand cmdExecutionStatus = new SqlCommand(qryExecutionStatus, myConnection))
                {

                    try
                    {
                        cmdExecutionStatus.Parameters.AddWithValue("@executionID", executionID);
                        myConnection.Open();

                        status = (int)cmdExecutionStatus.ExecuteScalar();

                        statusText = statusSwitch(status).ToString();

                        updateProgressTextBoxCallback d = new updateProgressTextBoxCallback(updateProgressTextBox);

                        while (status == 1 || status == 2 || status == 5 || status == 8)
                        {

                            _status = (int)cmdExecutionStatus.ExecuteScalar();
                            if (status != _status)
                            {
                                status = _status;
                                statusText = statusSwitch(status).ToString();
                                this.Invoke(d, new object[] { statusText });
                                Thread.Sleep(1000);
                            }

                            using (SqlDataAdapter adpEventMessage = new SqlDataAdapter(qryEventMessage, myConnection))
                            {
                                try
                                {
                                    DataTable t = new DataTable();
                                    adpEventMessage.SelectCommand.Parameters.AddWithValue("@executionID", executionID);
                                    adpEventMessage.Fill(t);

                                    updateProgressDataTableCallback dt = new updateProgressDataTableCallback(updateProgressDataTable);
                                    this.Invoke(dt, new object[] { t });
                                    Thread.Sleep(1000);
                                }
                                catch (Exception ex)
                                {
                                    this.Invoke(d, new object[] { ex.Message });
                                    Console.WriteLine(ex.Message);
                                }
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        updateProgressTextBoxCallback d = new updateProgressTextBoxCallback(updateProgressTextBox);
                        this.Invoke(d, new object[] { ex.Message });
                        Console.WriteLine(ex.Message);
                    }
                }

                updateConfirmOkToDoneCallback dd = new updateConfirmOkToDoneCallback(updateConfirmOkToDone);
                this.Invoke(dd);
            }
        }

        private string statusSwitch(int status)
        {
            // Method used to interpret integer status of SSIS package execution
            switch (status)
            {
                case 1:
                    statusText = "Created";
                    break;
                case 2:
                    statusText = "Running";
                    break;
                case 3:
                    statusText = "Canceled";
                    break;
                case 4:
                    statusText = "Failed";
                    break;
                case 5:
                    statusText = "Pending";
                    break;
                case 6:
                    statusText = "Ended unexpectedly";
                    break;
                case 7:
                    statusText = "Succeeded";
                    break;
                case 8:
                    statusText = "Stopping";
                    break;
                case 9:
                    statusText = "Completed";
                    break;
                default: throw new Exception("Unknown status");
            }
            return statusText;
        }

        private void updateProgressTextBox(string statusText)
        {
            // Updates tbxProgress text box with current status of SSIS package execution, obtained from 'initiatePackageProgress' method   //
            tbxProgress.Clear();
            tbxProgress.AppendText("Status = " + statusText);
            tbxProgress.AppendText(Environment.NewLine);
        }

        private void updateProgressDataTable(DataTable t)
        {
            // update dataGridView1 data grid with event messages, obtained from 'initiatePackageProgress' method.  //
            dataGridView1.DataSource = t;
            dataGridView1.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.AllCells);
            dataGridView1.Refresh();
        }

        private void updateConfirmOkToDone()
        {
            btnConfirmOK.Text = "Done";
            btnConfirmOK.Enabled = true;
        }

        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            // Used to enable 'initiatePackageProgress' method to run and update form without locking/freezing application  //

            // Do not access the form's BackgroundWorker reference directly.
            // Instead, use the reference provided by the sender parameter.
            BackgroundWorker bw = sender as BackgroundWorker;

            // Extract the argument.
            int executionID = (int)e.Argument;
            
            // Start the time-consuming operation.
            initiatePackageProgress(bw, executionID);
        }

        //private void backgroundWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        //{
        //    if (e.Cancelled)
        //    {
        //        backgroundCompleted = "Process was cancelled";
        //    }
        //    else if (e.Error != null)
        //    {
        //        backgroundCompleted = "There was an error running the process. The thread aborted";
        //    }
        //    else
        //    {
        //        backgroundCompleted = "Process was completed";
        //    }
        //}

    }
}
