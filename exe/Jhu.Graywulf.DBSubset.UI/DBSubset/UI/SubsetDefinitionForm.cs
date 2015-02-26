using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using Jhu.Graywulf.DBSubset.Lib;

namespace Jhu.Graywulf.DBSubset.UI
{
    public partial class SubsetDefinitionForm : Form
    {
        private SubsetDefinition subsetDefinition = new SubsetDefinition();
        private List<Table> tables = new List<Table>();
        private List<Label> orders = new List<Label>();
        private List<Label> names = new List<Label>();
        private List<ComboBox> methods = new List<ComboBox>();
        private List<TextBox> factors = new List<TextBox>();
        private List<Label> dataSpaceUseds = new List<Label>();
        private List<Label> rowCounts = new List<Label>();

        public SubsetDefinition SubsetDefinition
        {
            get { return subsetDefinition; }
            set { subsetDefinition = value; }
        }

        public SubsetDefinitionForm()
        {
            InitializeComponent();
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
        }

        private void RefreshForm()
        {
            SuspendLayout();

            DestroyForm();

            table.RowCount = subsetDefinition.Tables.Count + 1;

            int i = 1;
            foreach (Table t in subsetDefinition.Tables.Values.OrderBy(t => t.FullyQualifiedName))
            {
                int c = 0;

                tables.Add(t);

                Label order = new Label();
                order.Tag = i - 1;
                order.Text = t.Order.ToString();
                order.TextAlign = ContentAlignment.MiddleLeft;
                order.Anchor = AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Top | AnchorStyles.Bottom;
                table.Controls.Add(order, c++, i);
                orders.Add(order);

                Label name = new Label();
                name.Tag = i - 1;
                name.Text = t.FullyQualifiedName;
                name.TextAlign = ContentAlignment.MiddleLeft;
                name.Anchor = AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Top | AnchorStyles.Bottom;
                table.Controls.Add(name, c++, i);
                names.Add(name);

                ComboBox method = new ComboBox();
                method.Tag = i - 1;
                method.DropDownStyle = ComboBoxStyle.DropDownList;
                method.Items.Add(SamplingMethod.None);
                method.Items.Add(SamplingMethod.FullTable);
                method.Items.Add(SamplingMethod.Random);
                method.SelectedItem = t.SamplingMethod;
                method.SelectedIndexChanged += new EventHandler(method_SelectedIndexChanged);
                table.Controls.Add(method, c++, i);
                methods.Add(method);

                TextBox factor = new TextBox();
                factor.Tag = i - 1;
                factor.Anchor = AnchorStyles.Left | AnchorStyles.Right;
                factor.Text = t.SamplingFactor.ToString();
                factor.Enabled = (t.SamplingMethod == SamplingMethod.Random);
                table.Controls.Add(factor, c++, i);
                factors.Add(factor);

                Label dataSpaceUsed = new Label();
                dataSpaceUsed.Tag = i - 1;
                dataSpaceUsed.Text = t.DataSpaceUsed.ToString("###,###,##0");
                dataSpaceUsed.TextAlign = ContentAlignment.MiddleRight;
                dataSpaceUsed.Anchor = AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Top | AnchorStyles.Bottom;
                table.Controls.Add(dataSpaceUsed, c++, i);
                dataSpaceUseds.Add(dataSpaceUsed);

                Label rowCount = new Label();
                rowCount.Tag = i - 1;
                rowCount.Text = t.RowCount.ToString("###,###,##0");
                rowCount.TextAlign = ContentAlignment.MiddleRight;
                rowCount.Anchor = AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Top | AnchorStyles.Bottom;
                table.Controls.Add(rowCount, c++, i);
                rowCounts.Add(rowCount);

                i++;
            }

            ResumeLayout();
        }

        private void DestroyForm()
        {
            for (int i = 0; i < names.Count; i++)
            {
                table.Controls.Remove(orders[i]);
                table.Controls.Remove(names[i]);
                table.Controls.Remove(methods[i]);
                table.Controls.Remove(factors[i]);
                table.Controls.Remove(dataSpaceUseds[i]);
                table.Controls.Remove(rowCounts[i]);

                orders[i].Dispose();
                names[i].Dispose();
                methods[i].Dispose();
                factors[i].Dispose();
                dataSpaceUseds[i].Dispose();
                rowCounts[i].Dispose();
            }

            tables.Clear();
            orders.Clear();
            names.Clear();
            methods.Clear();
            factors.Clear();
            dataSpaceUseds.Clear();
            rowCounts.Clear();
        }

        private void SaveForm()
        {
            int i = 0;
            foreach (Table t in tables)
            {
                t.SamplingMethod = (SamplingMethod)methods[i].SelectedItem;
                t.SamplingFactor = double.Parse(factors[i].Text);

                i++;
            }
        }

        void method_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBox method = (ComboBox)sender;
            int i = (int)method.Tag;
            bool updatemethods = true;

            switch ((SamplingMethod)method.SelectedItem)
            {
                case SamplingMethod.FullTable:
                case SamplingMethod.Random:
                    updatemethods = CheckReferencedTables(tables[i]);
                    break;
                case SamplingMethod.None:
                    updatemethods = CheckReferencingTables(tables[i]);
                    break;
                default:
                    throw new NotImplementedException();
            }

            if (updatemethods)
            {
                tables[i].SamplingMethod = (SamplingMethod)method.SelectedItem;
            }
            else
            {
                method.SelectedItem = tables[i].SamplingMethod;
            }

            factors[i].Enabled = ((SamplingMethod)method.SelectedItem == SamplingMethod.Random);

            if (updatemethods)
            {
                for (int j = 0; j < tables.Count; j++)
                {
                    methods[j].SelectedItem = tables[j].SamplingMethod;
                }
            }
        }


        private bool CheckReferencedTables(Table table)
        {
            // Make sure all referenced columns are copied or sampled
            List<Table> references = DiscoveryEngine.FindAllReferencedTables(table);
            List<Table> nonref = new List<Table>(references.Where(t => t.SamplingMethod == SamplingMethod.None));

            if (nonref.Count != 0)
            {
                StringBuilder msg = new StringBuilder();

                msg.AppendLine("The following tables are referenced in foreign keys but not marked for copy or sampling:");
                msg.AppendLine();
                foreach (Table t in nonref)
                {
                    msg.AppendLine(t.FullyQualifiedName);
                }
                msg.AppendLine();
                msg.AppendLine("Do you want to mark them for copy?");

                switch (MessageBox.Show(msg.ToString(), Application.ProductName, MessageBoxButtons.YesNoCancel))
                {
                    case System.Windows.Forms.DialogResult.Yes:
                        foreach (Table t in nonref)
                        {
                            t.SamplingMethod = SamplingMethod.FullTable;
                        }
                        return true;
                    case System.Windows.Forms.DialogResult.No:
                        return true;
                    case System.Windows.Forms.DialogResult.Cancel:
                        return false;
                }
            }

            return true;
        }

        private bool CheckReferencingTables(Table table)
        {
            // Make sure all referenced columns are copied or sampled
            List<Table> references = DiscoveryEngine.FindAllReferencingTables(table);
            List<Table> nonref = new List<Table>(references.Where(t => t.SamplingMethod != SamplingMethod.None));

            if (nonref.Count != 0)
            {
                StringBuilder msg = new StringBuilder();

                msg.AppendLine("The following tables are referencing the current table but it is not marked for copy or sampling:");
                msg.AppendLine();
                foreach (Table t in nonref)
                {
                    msg.AppendLine(t.FullyQualifiedName);
                }
                msg.AppendLine();
                msg.AppendLine("Do you want to mark them to no copy?");

                switch (MessageBox.Show(msg.ToString(), Application.ProductName, MessageBoxButtons.YesNoCancel))
                {
                    case System.Windows.Forms.DialogResult.Yes:
                        foreach (Table t in nonref)
                        {
                            t.SamplingMethod = SamplingMethod.None;
                        }
                        return true;
                    case System.Windows.Forms.DialogResult.No:
                        return true;
                    case System.Windows.Forms.DialogResult.Cancel:
                        return false;
                }
            }

            return true;
        }

        private void Open()
        {
            using (OpenFileDialog f = new OpenFileDialog())
            {
                f.DefaultExt = ".xml";
                f.Filter = "XML documents (*.xml)|*.xml";

                if (f.ShowDialog() == DialogResult.OK)
                {
                    using (StreamReader infile = new StreamReader(f.FileName))
                    {
                        subsetDefinition = SubsetDefinition.Load(infile);
                    }

                    RefreshForm();
                }
            }
        }

        private void Save()
        {
            SaveForm();

            using (SaveFileDialog f = new SaveFileDialog())
            {
                f.DefaultExt = ".xml";
                f.Filter = "XML documents (*.xml)|*.xml";

                if (f.ShowDialog() == DialogResult.OK)
                {
                    using (StreamWriter outfile = new StreamWriter(f.FileName))
                    {
                        subsetDefinition.Save(outfile);
                    }
                }
            }
        }

        private void SetSourceDatabase()
        {
            string ncs = SelectDatabaseConnection(subsetDefinition.SourceDatabaseConnectionString.ConnectionString);

            if (ncs != null)
            {
                subsetDefinition.SourceDatabaseConnectionString.ConnectionString = ncs;
            }
        }

        private void SetDestinationDatabase()
        {
            string ncs = SelectDatabaseConnection(subsetDefinition.DestinationDatabaseConnectionString.ConnectionString);

            if (ncs != null)
            {
                subsetDefinition.DestinationDatabaseConnectionString.ConnectionString = ncs;
            }
        }

        private string SelectDatabaseConnection(string connectionString)
        {
            using (SqlConnectionDialog f = new SqlConnectionDialog())
            {
                f.ConnectionString = connectionString;

                if (f.ShowDialog() == DialogResult.OK)
                {
                    return f.ConnectionString;
                }
                else
                {
                    return null;
                }
            }
        }

        private void DiscoverDatabase()
        {
            DiscoveryEngine de = new DiscoveryEngine();
            de.SourceDatabaseConnectionString.ConnectionString = subsetDefinition.SourceDatabaseConnectionString.ConnectionString;

            string dcs = subsetDefinition.DestinationDatabaseConnectionString.ConnectionString;
            subsetDefinition = de.Execute();
            subsetDefinition.DestinationDatabaseConnectionString.ConnectionString = dcs;

            RefreshForm();
        }

        private void ValidateSubsetDefinition()
        {
            List<Table> badtables = new List<Table>();

            foreach (Table t in tables)
            {
                if (t.SamplingMethod == SamplingMethod.Random)
                {
                    List<Table> referenced = new List<Table>(DiscoveryEngine.FindAllReferencedTables(t));

                    foreach (Table rf in referenced)
                    {
                        if (rf.SamplingMethod == SamplingMethod.Random)
                        {
                            badtables.Add(t);
                            break;
                        }
                    }
                }
            }

            StringBuilder msg = new StringBuilder();
            if (badtables.Count > 0)
            {

                msg.AppendLine("The following tables are referencing randomly sampled table while they are randomly sampled themselves:");
                msg.AppendLine();

                foreach (Table t in badtables)
                {
                    msg.AppendLine(t.FullyQualifiedName);
                }
            }
            else
            {
                msg.Append("Validation succeeded.");

            }

            MessageBox.Show(msg.ToString(), Application.ProductName, MessageBoxButtons.OK);
        }

        private void NonReferencedTableSubSet()
        {   
                foreach (Table t in tables)
                {
                   
                        List<Table> referenced = new List<Table>(DiscoveryEngine.FindAllReferencedTables(t));
                        if (referenced.Count == 0)
                        {
                            if (t.RowCount > 100000)
	                        {
		                            t.SamplingMethod = SamplingMethod.Random;
                                    t.SamplingFactor = double.Parse(textRandom.Text);
	                        }
                            else
	                        {
                                t.SamplingMethod = SamplingMethod.FullTable;
                                   
	                        }
                            
                        }
                  }
                
            
        RefreshForm();
        }
        
        private void SetRandom()
        {
            foreach (Table t in tables)
            {
                t.SamplingFactor = double.Parse(textRandom.Text);
                
            }
            RefreshForm();
        }

        private void Execute()
        {
            SaveForm();

            SubsetEngine e = new SubsetEngine(subsetDefinition);

            e.Execute();
        }

        private void Script()
        {
            SaveForm();

            using (SaveFileDialog f = new SaveFileDialog())
            {
                f.DefaultExt = ".sql";
                f.Filter = "SQL documents (*.sql)|*.sql";

                if (f.ShowDialog() == DialogResult.OK)
                {
                    SubsetEngine e = new SubsetEngine(subsetDefinition);

                    using (StreamWriter outfile = new StreamWriter(f.FileName))
                    {
                        e.GenerateScript(outfile);
                    }
                }
            }
        }

        private void toolStrip_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            if (e.ClickedItem == openFileButton)
            {
                Open();
            }
            if (e.ClickedItem == saveFileButton)
            {
                Save();
            }
            else if (e.ClickedItem == setSourceButton)
            {
                SetSourceDatabase();
            }
            else if (e.ClickedItem == setDestinationButton)
            {
                SetDestinationDatabase();
            }
            else if (e.ClickedItem == dicoverDatabaseButton)
            {
                DiscoverDatabase();
            }
            else if (e.ClickedItem == validateButton)
            {
                ValidateSubsetDefinition();
            }
            else if (e.ClickedItem == NonReferencedTableButton)
            {
                NonReferencedTableSubSet();
            }
            else if (e.ClickedItem == executeButton)
            {
                Execute();
            }
            else if (e.ClickedItem == scriptButton)
            {
                Script();
            }
            else if (e.ClickedItem == RandomButton)
            {
                SetRandom();
            }
        }

        

       

        private void menu_Click(object sender, EventArgs e)
        {
            if (sender == menuFileExit)
            {
                this.Close();
            }
        }


    }
}
