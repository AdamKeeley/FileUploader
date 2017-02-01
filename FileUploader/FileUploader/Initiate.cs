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
            string[] varOptions = {tbxSourcePath.Text, tbxServerName.Text, cbxDatabaseName.Text};
            Confirm confirm = new Confirm();
            confirm.lblConfirmSourceDirectory.Text = varOptions[0].ToString();
            confirm.lblConfirmDestinationServer.Text = varOptions[1].ToString();
            confirm.lblConfirmDestinationDatabase.Text = varOptions[2].ToString();
            confirm.Show();
        }

        private void populateDatabase(string server)
        {
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