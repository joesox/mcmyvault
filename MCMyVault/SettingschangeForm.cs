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
    public partial class SettingschangeForm : Form
    {
        public String NewValue { get; set; }

        public SettingschangeForm(string currentvalue)
        {
            InitializeComponent();
            txtSettingupdate.AppendText(currentvalue);
        }

        private void btnSettingchange_Click(object sender, EventArgs e)
        {
            NewValue = txtSettingupdate.Text;
            this.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.Close();
        }
    }
}
