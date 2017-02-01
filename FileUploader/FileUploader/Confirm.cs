using System;
using System.Data;
using System.Data.SqlClient;
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
            string[] varImportOptions = { this.lblConfirmDestinationServer.Text, this.lblConfirmDestinationDatabase.Text, this.lblConfirmSourceDirectory.Text };
            initiateLookupTableImport(varImportOptions);
        }

        private void btnConfirmCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void lblConfirmSourceDirectory_Click(object sender, EventArgs e)
        {
            this.lblConfirmSourceDirectory.LinkVisited = true;
            System.Diagnostics.Process.Start(lblConfirmSourceDirectory.Text);
        }

        private void initiateLookupTableImport(string[] varImportOptions)
        {
            SqlConnectionStringBuilder builtConnection = new SqlConnectionStringBuilder();
            builtConnection["Data Source"] = varImportOptions[0].ToString();
            builtConnection["Trusted_Connection"] = "yes";
            builtConnection["Initial Catalog"] = varImportOptions[1].ToString();
            builtConnection["Connection Timeout"] = "5";

            string destinationConnection = builtConnection.ToString();

            SqlConnection myConnection = new SqlConnection("Data Source=IRC-PC010\\QSRNVIVO10;Trusted_Connection=Yes;Initial Catalog=CPRD");
            string query = "[dbo].[procLookupTableImport]";
            //string query = "[dbo].[procDebugTest]";

            using (myConnection)
            {
                using (SqlCommand command = new SqlCommand(query, myConnection))
                {
                    try
                    {
                        command.CommandType = CommandType.StoredProcedure;

                        command.Parameters.AddWithValue("@ConnectionString", destinationConnection);
                        command.Parameters.AddWithValue("@Directory", varImportOptions[2]);
                        command.Parameters.AddWithValue("@TxtDirectory", varImportOptions[2] + "\\TXTFILES");

                        myConnection.Open();

                        command.ExecuteNonQuery();
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                }
            }
            this.Close();
        }
    }
}
