using System;

namespace FileUploader
{
    partial class Initiate
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.tbxSourcePath = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.btnInitiateBrowse = new System.Windows.Forms.Button();
            this.cbxDatabaseName = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.btnInitiateUpload = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.tbxServerName = new System.Windows.Forms.TextBox();
            this.InitiateBindingSource = new System.Windows.Forms.BindingSource(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.InitiateBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // tbxSourcePath
            // 
            this.tbxSourcePath.Location = new System.Drawing.Point(119, 12);
            this.tbxSourcePath.Name = "tbxSourcePath";
            this.tbxSourcePath.Size = new System.Drawing.Size(324, 20);
            this.tbxSourcePath.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 12);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(97, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Source file location";
            // 
            // btnInitiateBrowse
            // 
            this.btnInitiateBrowse.Location = new System.Drawing.Point(458, 12);
            this.btnInitiateBrowse.Name = "btnInitiateBrowse";
            this.btnInitiateBrowse.Size = new System.Drawing.Size(75, 23);
            this.btnInitiateBrowse.TabIndex = 2;
            this.btnInitiateBrowse.Text = "Browse";
            this.btnInitiateBrowse.UseVisualStyleBackColor = true;
            this.btnInitiateBrowse.Click += new System.EventHandler(this.btnBrowse_Click);
            // 
            // cbxDatabaseName
            // 
            this.cbxDatabaseName.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Append;
            this.cbxDatabaseName.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cbxDatabaseName.FormattingEnabled = true;
            this.cbxDatabaseName.Location = new System.Drawing.Point(119, 79);
            this.cbxDatabaseName.Name = "cbxDatabaseName";
            this.cbxDatabaseName.Size = new System.Drawing.Size(172, 21);
            this.cbxDatabaseName.TabIndex = 6;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 46);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(92, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Destination server";
            // 
            // btnInitiateUpload
            // 
            this.btnInitiateUpload.Location = new System.Drawing.Point(458, 83);
            this.btnInitiateUpload.Name = "btnInitiateUpload";
            this.btnInitiateUpload.Size = new System.Drawing.Size(75, 23);
            this.btnInitiateUpload.TabIndex = 7;
            this.btnInitiateUpload.Text = "Upload";
            this.btnInitiateUpload.UseVisualStyleBackColor = true;
            this.btnInitiateUpload.Click += new System.EventHandler(this.btnInitiateUpload_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(6, 79);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(107, 13);
            this.label3.TabIndex = 5;
            this.label3.Text = "Destination database";
            // 
            // tbxServerName
            // 
            this.tbxServerName.Location = new System.Drawing.Point(119, 46);
            this.tbxServerName.Name = "tbxServerName";
            this.tbxServerName.Size = new System.Drawing.Size(324, 20);
            this.tbxServerName.TabIndex = 4;
            this.tbxServerName.Leave += new System.EventHandler(this.tbxServerName_Leave);
            // 
            // InitiateBindingSource
            // 
            this.InitiateBindingSource.DataSource = typeof(FileUploader.Initiate);
            // 
            // Initiate
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(546, 456);
            this.Controls.Add(this.tbxServerName);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.btnInitiateUpload);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.cbxDatabaseName);
            this.Controls.Add(this.btnInitiateBrowse);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.tbxSourcePath);
            this.Name = "Initiate";
            this.Text = "CPRD Data Upload";
            ((System.ComponentModel.ISupportInitialize)(this.InitiateBindingSource)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox tbxSourcePath;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnInitiateBrowse;
        private System.Windows.Forms.ComboBox cbxDatabaseName;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnInitiateUpload;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox tbxServerName;
        private System.Windows.Forms.BindingSource InitiateBindingSource;
    }
}

