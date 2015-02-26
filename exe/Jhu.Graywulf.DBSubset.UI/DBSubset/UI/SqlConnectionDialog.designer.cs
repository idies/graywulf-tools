namespace Jhu.Graywulf.DBSubset.UI
{
    partial class SqlConnectionDialog
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
            this.connection = new Microsoft.Data.ConnectionUI.SqlConnectionUIControl();
            this.cancel = new System.Windows.Forms.Button();
            this.ok = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // connection
            // 
            this.connection.Location = new System.Drawing.Point(9, 9);
            this.connection.Margin = new System.Windows.Forms.Padding(0);
            this.connection.MinimumSize = new System.Drawing.Size(350, 360);
            this.connection.Name = "connection";
            this.connection.Size = new System.Drawing.Size(350, 360);
            this.connection.TabIndex = 0;
            // 
            // cancel
            // 
            this.cancel.CausesValidation = false;
            this.cancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.cancel.Location = new System.Drawing.Point(283, 373);
            this.cancel.Name = "cancel";
            this.cancel.Size = new System.Drawing.Size(75, 23);
            this.cancel.TabIndex = 1;
            this.cancel.Text = "Cancel";
            this.cancel.UseVisualStyleBackColor = true;
            // 
            // ok
            // 
            this.ok.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.ok.Location = new System.Drawing.Point(202, 373);
            this.ok.Name = "ok";
            this.ok.Size = new System.Drawing.Size(75, 23);
            this.ok.TabIndex = 2;
            this.ok.Text = "OK";
            this.ok.UseVisualStyleBackColor = true;
            // 
            // SqlConnectionDialog
            // 
            this.AcceptButton = this.ok;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.cancel;
            this.ClientSize = new System.Drawing.Size(371, 404);
            this.Controls.Add(this.ok);
            this.Controls.Add(this.cancel);
            this.Controls.Add(this.connection);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "SqlConnectionDialog";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.Text = "SqlConnectionDialog";
            this.ResumeLayout(false);

        }

        #endregion

        private Microsoft.Data.ConnectionUI.SqlConnectionUIControl connection;
        private System.Windows.Forms.Button cancel;
        private System.Windows.Forms.Button ok;
    }
}