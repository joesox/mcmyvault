using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace MCMyVault
{
    public partial class NewProfileForm : Form
    {
        public String NewName { get; set; }

        public NewProfileForm(string currentfilename)
        {
            InitializeComponent();
            txtbNewProfileName.AppendText(currentfilename);
        }

        private void btnOKNewProfileName_Click(object sender, EventArgs e)
        {
            NewName = txtbNewProfileName.Text;
            this.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.Close();
        }
    }
}
