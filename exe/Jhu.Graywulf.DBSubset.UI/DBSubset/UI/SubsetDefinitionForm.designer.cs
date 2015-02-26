namespace Jhu.Graywulf.DBSubset.UI
{
    partial class SubsetDefinitionForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SubsetDefinitionForm));
            this.table = new System.Windows.Forms.TableLayoutPanel();
            this.samplingFactorLabel = new System.Windows.Forms.Label();
            this.samplingMethodLabel = new System.Windows.Forms.Label();
            this.tableNameLabel = new System.Windows.Forms.Label();
            this.dataSpaceUsedLabel = new System.Windows.Forms.Label();
            this.rowCountLabel = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.toolStrip = new System.Windows.Forms.ToolStrip();
            this.openFileButton = new System.Windows.Forms.ToolStripButton();
            this.saveFileButton = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.setSourceButton = new System.Windows.Forms.ToolStripButton();
            this.setDestinationButton = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.dicoverDatabaseButton = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.validateButton = new System.Windows.Forms.ToolStripButton();
            this.executeButton = new System.Windows.Forms.ToolStripButton();
            this.scriptButton = new System.Windows.Forms.ToolStripButton();
            this.NonReferencedTableButton = new System.Windows.Forms.ToolStripButton();
            this.textRandom = new System.Windows.Forms.ToolStripTextBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.menuStrip = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.menuFileExit = new System.Windows.Forms.ToolStripMenuItem();
            this.RandomButton = new System.Windows.Forms.ToolStripButton();
            this.table.SuspendLayout();
            this.toolStrip.SuspendLayout();
            this.panel1.SuspendLayout();
            this.menuStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // table
            // 
            this.table.AutoSize = true;
            this.table.ColumnCount = 6;
            this.table.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 32F));
            this.table.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.table.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 120F));
            this.table.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 120F));
            this.table.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 120F));
            this.table.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 120F));
            this.table.Controls.Add(this.samplingFactorLabel, 3, 0);
            this.table.Controls.Add(this.samplingMethodLabel, 2, 0);
            this.table.Controls.Add(this.tableNameLabel, 1, 0);
            this.table.Controls.Add(this.dataSpaceUsedLabel, 4, 0);
            this.table.Controls.Add(this.rowCountLabel, 5, 0);
            this.table.Controls.Add(this.label1, 0, 0);
            this.table.Dock = System.Windows.Forms.DockStyle.Top;
            this.table.Location = new System.Drawing.Point(0, 0);
            this.table.Name = "table";
            this.table.RowCount = 2;
            this.table.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.table.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.table.Size = new System.Drawing.Size(828, 17);
            this.table.TabIndex = 0;
            // 
            // samplingFactorLabel
            // 
            this.samplingFactorLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.samplingFactorLabel.AutoSize = true;
            this.samplingFactorLabel.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.samplingFactorLabel.Location = new System.Drawing.Point(471, 0);
            this.samplingFactorLabel.Name = "samplingFactorLabel";
            this.samplingFactorLabel.Padding = new System.Windows.Forms.Padding(2);
            this.samplingFactorLabel.Size = new System.Drawing.Size(114, 17);
            this.samplingFactorLabel.TabIndex = 2;
            this.samplingFactorLabel.Text = "Sampling Factor";
            // 
            // samplingMethodLabel
            // 
            this.samplingMethodLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.samplingMethodLabel.AutoSize = true;
            this.samplingMethodLabel.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.samplingMethodLabel.Location = new System.Drawing.Point(351, 0);
            this.samplingMethodLabel.Name = "samplingMethodLabel";
            this.samplingMethodLabel.Padding = new System.Windows.Forms.Padding(2);
            this.samplingMethodLabel.Size = new System.Drawing.Size(114, 17);
            this.samplingMethodLabel.TabIndex = 1;
            this.samplingMethodLabel.Text = "Sampling Method";
            // 
            // tableNameLabel
            // 
            this.tableNameLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.tableNameLabel.AutoSize = true;
            this.tableNameLabel.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.tableNameLabel.Location = new System.Drawing.Point(35, 0);
            this.tableNameLabel.Name = "tableNameLabel";
            this.tableNameLabel.Padding = new System.Windows.Forms.Padding(2);
            this.tableNameLabel.Size = new System.Drawing.Size(310, 17);
            this.tableNameLabel.TabIndex = 0;
            this.tableNameLabel.Text = "Table Name";
            // 
            // dataSpaceUsedLabel
            // 
            this.dataSpaceUsedLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.dataSpaceUsedLabel.AutoSize = true;
            this.dataSpaceUsedLabel.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.dataSpaceUsedLabel.Location = new System.Drawing.Point(591, 0);
            this.dataSpaceUsedLabel.Name = "dataSpaceUsedLabel";
            this.dataSpaceUsedLabel.Padding = new System.Windows.Forms.Padding(2);
            this.dataSpaceUsedLabel.Size = new System.Drawing.Size(114, 17);
            this.dataSpaceUsedLabel.TabIndex = 3;
            this.dataSpaceUsedLabel.Text = "Data Size";
            // 
            // rowCountLabel
            // 
            this.rowCountLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.rowCountLabel.AutoSize = true;
            this.rowCountLabel.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.rowCountLabel.Location = new System.Drawing.Point(711, 0);
            this.rowCountLabel.Name = "rowCountLabel";
            this.rowCountLabel.Padding = new System.Windows.Forms.Padding(2);
            this.rowCountLabel.Size = new System.Drawing.Size(114, 17);
            this.rowCountLabel.TabIndex = 4;
            this.rowCountLabel.Text = "Row Count";
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.label1.Location = new System.Drawing.Point(3, 0);
            this.label1.Name = "label1";
            this.label1.Padding = new System.Windows.Forms.Padding(2);
            this.label1.Size = new System.Drawing.Size(26, 17);
            this.label1.TabIndex = 5;
            // 
            // toolStrip
            // 
            this.toolStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.openFileButton,
            this.saveFileButton,
            this.toolStripSeparator1,
            this.setSourceButton,
            this.setDestinationButton,
            this.toolStripSeparator3,
            this.dicoverDatabaseButton,
            this.toolStripSeparator2,
            this.validateButton,
            this.executeButton,
            this.scriptButton,
            this.NonReferencedTableButton,
            this.RandomButton,
            this.textRandom});
            this.toolStrip.Location = new System.Drawing.Point(0, 24);
            this.toolStrip.Name = "toolStrip";
            this.toolStrip.Size = new System.Drawing.Size(832, 25);
            this.toolStrip.TabIndex = 1;
            this.toolStrip.Text = "toolStrip1";
            this.toolStrip.ItemClicked += new System.Windows.Forms.ToolStripItemClickedEventHandler(this.toolStrip_ItemClicked);
            // 
            // openFileButton
            // 
            this.openFileButton.Image = ((System.Drawing.Image)(resources.GetObject("openFileButton.Image")));
            this.openFileButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.openFileButton.Name = "openFileButton";
            this.openFileButton.Size = new System.Drawing.Size(56, 22);
            this.openFileButton.Text = "Open";
            // 
            // saveFileButton
            // 
            this.saveFileButton.Image = ((System.Drawing.Image)(resources.GetObject("saveFileButton.Image")));
            this.saveFileButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.saveFileButton.Name = "saveFileButton";
            this.saveFileButton.Size = new System.Drawing.Size(51, 22);
            this.saveFileButton.Text = "Save";
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // setSourceButton
            // 
            this.setSourceButton.Image = ((System.Drawing.Image)(resources.GetObject("setSourceButton.Image")));
            this.setSourceButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.setSourceButton.Name = "setSourceButton";
            this.setSourceButton.Size = new System.Drawing.Size(63, 22);
            this.setSourceButton.Text = "Source";
            // 
            // setDestinationButton
            // 
            this.setDestinationButton.Image = ((System.Drawing.Image)(resources.GetObject("setDestinationButton.Image")));
            this.setDestinationButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.setDestinationButton.Name = "setDestinationButton";
            this.setDestinationButton.Size = new System.Drawing.Size(87, 22);
            this.setDestinationButton.Text = "Destination";
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(6, 25);
            // 
            // dicoverDatabaseButton
            // 
            this.dicoverDatabaseButton.Image = ((System.Drawing.Image)(resources.GetObject("dicoverDatabaseButton.Image")));
            this.dicoverDatabaseButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.dicoverDatabaseButton.Name = "dicoverDatabaseButton";
            this.dicoverDatabaseButton.Size = new System.Drawing.Size(72, 22);
            this.dicoverDatabaseButton.Text = "Discover";
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 25);
            // 
            // validateButton
            // 
            this.validateButton.Image = ((System.Drawing.Image)(resources.GetObject("validateButton.Image")));
            this.validateButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.validateButton.Name = "validateButton";
            this.validateButton.Size = new System.Drawing.Size(69, 22);
            this.validateButton.Text = "Validate";
            // 
            // executeButton
            // 
            this.executeButton.Image = ((System.Drawing.Image)(resources.GetObject("executeButton.Image")));
            this.executeButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.executeButton.Name = "executeButton";
            this.executeButton.Size = new System.Drawing.Size(67, 22);
            this.executeButton.Text = "Execute";
            // 
            // scriptButton
            // 
            this.scriptButton.Image = ((System.Drawing.Image)(resources.GetObject("scriptButton.Image")));
            this.scriptButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.scriptButton.Name = "scriptButton";
            this.scriptButton.Size = new System.Drawing.Size(57, 22);
            this.scriptButton.Text = "Script";
            // 
            // NonReferencedTableButton
            // 
            this.NonReferencedTableButton.Image = ((System.Drawing.Image)(resources.GetObject("NonReferencedTableButton.Image")));
            this.NonReferencedTableButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.NonReferencedTableButton.Name = "NonReferencedTableButton";
            this.NonReferencedTableButton.Size = new System.Drawing.Size(101, 22);
            this.NonReferencedTableButton.Text = "NonRefTables";
            // 
            // textRandom
            // 
            this.textRandom.Name = "textRandom";
            this.textRandom.Size = new System.Drawing.Size(40, 25);
            this.textRandom.Text = "0";
            // 
            // panel1
            // 
            this.panel1.AutoScroll = true;
            this.panel1.BackColor = System.Drawing.SystemColors.Window;
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel1.Controls.Add(this.table);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 49);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(832, 328);
            this.panel1.TabIndex = 2;
            // 
            // menuStrip
            // 
            this.menuStrip.GripMargin = new System.Windows.Forms.Padding(0, 2, 0, 2);
            this.menuStrip.GripStyle = System.Windows.Forms.ToolStripGripStyle.Visible;
            this.menuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem});
            this.menuStrip.Location = new System.Drawing.Point(0, 0);
            this.menuStrip.Name = "menuStrip";
            this.menuStrip.Size = new System.Drawing.Size(832, 24);
            this.menuStrip.TabIndex = 3;
            this.menuStrip.Text = "menuStrip";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.openToolStripMenuItem,
            this.saveToolStripMenuItem,
            this.toolStripSeparator4,
            this.menuFileExit});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "&File";
            // 
            // openToolStripMenuItem
            // 
            this.openToolStripMenuItem.Name = "openToolStripMenuItem";
            this.openToolStripMenuItem.Size = new System.Drawing.Size(112, 22);
            this.openToolStripMenuItem.Text = "&Open...";
            // 
            // saveToolStripMenuItem
            // 
            this.saveToolStripMenuItem.Name = "saveToolStripMenuItem";
            this.saveToolStripMenuItem.Size = new System.Drawing.Size(112, 22);
            this.saveToolStripMenuItem.Text = "&Save";
            // 
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            this.toolStripSeparator4.Size = new System.Drawing.Size(109, 6);
            // 
            // menuFileExit
            // 
            this.menuFileExit.Name = "menuFileExit";
            this.menuFileExit.Size = new System.Drawing.Size(112, 22);
            this.menuFileExit.Text = "E&xit";
            this.menuFileExit.Click += new System.EventHandler(this.menu_Click);
            // 
            // RandomButton
            // 
            this.RandomButton.Image = ((System.Drawing.Image)(resources.GetObject("RandomButton.Image")));
            this.RandomButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.RandomButton.Name = "RandomButton";
            this.RandomButton.Size = new System.Drawing.Size(115, 20);
            this.RandomButton.Text = "SetSampleFactor";
            // 
            // SubsetDefinitionForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(832, 377);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.toolStrip);
            this.Controls.Add(this.menuStrip);
            this.MainMenuStrip = this.menuStrip;
            this.Name = "SubsetDefinitionForm";
            this.Text = "SubsetDefinitionForm";
            this.table.ResumeLayout(false);
            this.table.PerformLayout();
            this.toolStrip.ResumeLayout(false);
            this.toolStrip.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.menuStrip.ResumeLayout(false);
            this.menuStrip.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel table;
        private System.Windows.Forms.ToolStrip toolStrip;
        private System.Windows.Forms.ToolStripButton openFileButton;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label samplingFactorLabel;
        private System.Windows.Forms.Label samplingMethodLabel;
        private System.Windows.Forms.Label tableNameLabel;
        private System.Windows.Forms.ToolStripButton saveFileButton;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripButton setSourceButton;
        private System.Windows.Forms.ToolStripButton setDestinationButton;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripButton dicoverDatabaseButton;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripButton executeButton;
        private System.Windows.Forms.MenuStrip menuStrip;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem menuFileExit;
        private System.Windows.Forms.ToolStripMenuItem openToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
        private System.Windows.Forms.ToolStripButton scriptButton;
        private System.Windows.Forms.Label dataSpaceUsedLabel;
        private System.Windows.Forms.Label rowCountLabel;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ToolStripButton validateButton;
        private System.Windows.Forms.ToolStripButton NonReferencedTableButton;
        private System.Windows.Forms.ToolStripTextBox textRandom;
        private System.Windows.Forms.ToolStripButton RandomButton;
    }
}