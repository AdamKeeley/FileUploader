using System;

namespace FileUploader
{
    partial class Confirm
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
        public void InitializeComponent()
        {
            this.label1 = new System.Windows.Forms.Label();
            this.btnConfirmOK = new System.Windows.Forms.Button();
            this.btnConfirmCancel = new System.Windows.Forms.Button();
            this.lblConfirmSourceDirectory = new System.Windows.Forms.LinkLabel();
            this.lblConfirmDestinationServer = new System.Windows.Forms.Label();
            this.lblConfirmDestinationDatabase = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(234, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Please review your selected actions and confirm";
            // 
            // btnConfirmOK
            // 
            this.btnConfirmOK.Location = new System.Drawing.Point(232, 150);
            this.btnConfirmOK.Name = "btnConfirmOK";
            this.btnConfirmOK.Size = new System.Drawing.Size(75, 23);
            this.btnConfirmOK.TabIndex = 1;
            this.btnConfirmOK.Text = "OK";
            this.btnConfirmOK.UseVisualStyleBackColor = true;
            this.btnConfirmOK.Click += new System.EventHandler(this.btnConfirmOK_Click);
            // 
            // btnConfirmCancel
            // 
            this.btnConfirmCancel.Location = new System.Drawing.Point(313, 150);
            this.btnConfirmCancel.Name = "btnConfirmCancel";
            this.btnConfirmCancel.Size = new System.Drawing.Size(75, 23);
            this.btnConfirmCancel.TabIndex = 2;
            this.btnConfirmCancel.Text = "Cancel";
            this.btnConfirmCancel.UseVisualStyleBackColor = true;
            this.btnConfirmCancel.Click += new System.EventHandler(this.btnConfirmCancel_Click);
            // 
            // lblConfirmSourceDirectory
            // 
            this.lblConfirmSourceDirectory.AutoSize = true;
            this.lblConfirmSourceDirectory.Location = new System.Drawing.Point(12, 47);
            this.lblConfirmSourceDirectory.MaximumSize = new System.Drawing.Size(400, 0);
            this.lblConfirmSourceDirectory.Name = "lblConfirmSourceDirectory";
            this.lblConfirmSourceDirectory.Size = new System.Drawing.Size(0, 13);
            this.lblConfirmSourceDirectory.TabIndex = 3;
            this.lblConfirmSourceDirectory.TabStop = true;
            this.lblConfirmSourceDirectory.Click += new System.EventHandler(this.lblConfirmSourceDirectory_Click);
            // 
            // lblConfirmDestinationServer
            // 
            this.lblConfirmDestinationServer.AutoSize = true;
            this.lblConfirmDestinationServer.Location = new System.Drawing.Point(12, 101);
            this.lblConfirmDestinationServer.Name = "lblConfirmDestinationServer";
            this.lblConfirmDestinationServer.Size = new System.Drawing.Size(0, 13);
            this.lblConfirmDestinationServer.TabIndex = 4;
            // 
            // lblConfirmDestinationDatabase
            // 
            this.lblConfirmDestinationDatabase.AutoSize = true;
            this.lblConfirmDestinationDatabase.Location = new System.Drawing.Point(12, 136);
            this.lblConfirmDestinationDatabase.Name = "lblConfirmDestinationDatabase";
            this.lblConfirmDestinationDatabase.Size = new System.Drawing.Size(0, 13);
            this.lblConfirmDestinationDatabase.TabIndex = 5;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 34);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(87, 13);
            this.label2.TabIndex = 6;
            this.label2.Text = "Source directory:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 88);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(95, 13);
            this.label3.TabIndex = 7;
            this.label3.Text = "Destination server:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(12, 123);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(110, 13);
            this.label4.TabIndex = 8;
            this.label4.Text = "Destination database:";
            // 
            // Confirm
            // 
            this.AcceptButton = this.btnConfirmOK;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(400, 185);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.lblConfirmDestinationDatabase);
            this.Controls.Add(this.lblConfirmDestinationServer);
            this.Controls.Add(this.lblConfirmSourceDirectory);
            this.Controls.Add(this.btnConfirmCancel);
            this.Controls.Add(this.btnConfirmOK);
            this.Controls.Add(this.label1);
            this.Name = "Confirm";
            this.Text = "Confirm";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnConfirmOK;
        private System.Windows.Forms.Button btnConfirmCancel;
        public System.Windows.Forms.LinkLabel lblConfirmSourceDirectory;
        public System.Windows.Forms.Label lblConfirmDestinationServer;
        public System.Windows.Forms.Label lblConfirmDestinationDatabase;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
    }
}