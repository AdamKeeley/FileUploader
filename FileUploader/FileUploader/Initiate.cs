using Microsoft.WindowsAPICodePack.Dialogs;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace FileUploader
{
    public partial class Initiate : Form
    {
        public Initiate()
        {
            InitializeComponent();
        }

        private void btnBrowse_Click(object sender, EventArgs e)
        {
            // Opens the good file browser. Had to install 'WindowsAPICodePack' from NuGet  //
            CommonOpenFileDialog dialog = new CommonOpenFileDialog();
            dialog.InitialDirectory = "N:\\Academic-Services\\ISS\\IRC-Data-Services\\Suppliers\\CPRD\\Lookups\\Lookups_2016_08";
            dialog.IsFolderPicker = true;
            if (dialog.ShowDialog() == CommonFileDialogResult.Ok)
            {
                tbxSourcePath.Text = dialog.FileName;
            }
        }

        private void tbxServerName_Leave(object sender, EventArgs e)
        {
            // Clears and populates the database combo box with available databases from instance specified     //
            // Calls 'populateDatabase' method to do so                                                         //
            if (string.IsNullOrEmpty(tbxServerName.Text))
            {
                cbxDatabaseName.ResetText();
                cbxDatabaseName.Items.Clear();
            }
            else
            {
                var server = tbxServerName.Text;
                populateDatabase(server);
            }
        }

        private void btnInitiateUpload_Click(object sender, EventArgs e)
        {
            // Packs the three variable options into a string array,    //
            // opens and and passes it through to the 'Confirm' form    //
            string[] varOptions = {tbxSourcePath.Text, tbxServerName.Text, cbxDatabaseName.Text};
            Confirm confirm = new Confirm();
            confirm.lblConfirmSourceDirectory.Text = varOptions[0].ToString();
            confirm.lblConfirmDestinationServer.Text = varOptions[1].ToString();
            confirm.lblConfirmDestinationDatabase.Text = varOptions[2].ToString();
            confirm.Show();
        }

        private void populateDatabase(string server)
        {
            // Connects to the instance specified in tbxServerName and queries for database names.  //
            // Loads the query into a list that is used to populate the cbxDatabaseName combo box.  //
            SqlConnectionStringBuilder builtConnection = new SqlConnectionStringBuilder();
            builtConnection["Data Source"] = server;
            builtConnection["Trusted_Connection"] = "yes";
            builtConnection["Initial Catalog"] = "master";
            builtConnection["Connection Timeout"] = "5";

            SqlConnection myConnection = new SqlConnection(builtConnection.ConnectionString);
            string query = "select name from sys.databases";
            List<String> databases = new List<String>();

            using (myConnection)
            {
                using (SqlCommand command = new SqlCommand(query, myConnection))
                {
                    try
                    {
                        myConnection.Open();
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                databases.Add(reader.GetString(0));
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                }
            }

            cbxDatabaseName.BeginUpdate();
            for (int i = 0; i < databases.Count; i++)
            {
                cbxDatabaseName.Items.Add(databases[i].ToString());
            }
            cbxDatabaseName.EndUpdate();
        }
    }
}