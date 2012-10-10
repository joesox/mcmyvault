using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace MCMyVault
{
    public partial class RenameForm : Form
    {
        public String NewName { get; set; }

        public RenameForm(string currentfilename)
        {
            InitializeComponent();
            txtbRenameto.AppendText(currentfilename);
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            NewName = txtbRenameto.Text;
            this.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.Close();
        }
    }
}
