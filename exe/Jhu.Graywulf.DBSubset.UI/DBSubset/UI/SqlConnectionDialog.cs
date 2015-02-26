using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
using Microsoft.Data.ConnectionUI;

namespace Jhu.Graywulf.DBSubset.UI
{
    public partial class SqlConnectionDialog : Form
    {
        private SqlFileConnectionProperties cp;

        public string ConnectionString
        {
            get
            {
                return cp.ConnectionStringBuilder.ConnectionString;
            }
            set
            {
                cp = new SqlFileConnectionProperties();
                cp.ConnectionStringBuilder.ConnectionString = value;
                connection.Initialize(cp);
                connection.LoadProperties();
            }
        }

        public SqlConnectionDialog()
        {
            InitializeComponent();
        }
    }
}
