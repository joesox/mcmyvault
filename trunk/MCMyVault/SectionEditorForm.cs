using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.IO;
using System.Windows.Forms;

namespace MCMyVault
{
    public partial class SectionEditorForm : Form
    {
        public String FullFileName { get; set; }

        public SectionEditorForm(string currentfilename, string sectionname, List<String> section)
        {
            InitializeComponent();
            this.FullFileName = currentfilename;
            this.labelSectionname.Text = sectionname;
            if (File.Exists(currentfilename))
            {
                FileInfo file = new FileInfo(currentfilename);
                this.Text = "Section editor: " + file.Name;
                //Time to fill the panel
                foreach (string line in section)
                {
                    if (!line.Contains("{") && !line.Contains("}"))
                    {
                        TextBox t = new TextBox();
                        t.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                        //t.Anchor = (AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right);//WHY DON'T YOU WORK?!?!
                        t.AutoSize = true;
                        Size tempSize = t.Size;
                        tempSize.Width = (flowLayoutPanelSectionEd.Size.Width - 10);
                        t.Size = tempSize;
                        t.Text = line.Trim();
                        flowLayoutPanelSectionEd.Controls.Add(t);
                    }
                }
            }
            else
                this.labelSectionname.Text = "ERROR: This config file doesn't exist?";
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            //OK TO SAVE
            //foreach control if it is a textbox, update each line
            //save config
            try
            {
                foreach (Control control in flowLayoutPanelSectionEd.Controls)
                {
                    if (control.GetType().ToString() == "System.Windows.Forms.TextBox")
                    {
                        string textboxText = ((TextBox)control).Text;
                        if(textboxText.Contains("="))
                        {
                            string prefix = ((TextBox)control).Text.Split('=')[0];
                            List<String> OriginalFile = new List<string>();
                            OriginalFile.AddRange(File.ReadAllLines(this.FullFileName));
                            int LINETOEDIT = 0;
                            int startindex = -1;
                            foreach (string line in OriginalFile)
                            {
                                if (line.Trim().StartsWith(prefix) && (!line.Trim().StartsWith("#") || !line.Trim().StartsWith("//")))
                                {
                                    //we found the line
                                    startindex = line.IndexOf(prefix);//this is where to start the text
                                    break;
                                }
                                LINETOEDIT = LINETOEDIT + 1;
                            }
                            //time to edit
                            string spaces = "";
                            spaces = spaces.PadLeft(startindex);
                            OriginalFile[LINETOEDIT] = spaces + ((TextBox)control).Text;
                            //time to write
                            File.WriteAllLines(this.FullFileName, OriginalFile);
                        }
                        else
                            MessageBox.Show("This line doesn't have an '='??. Not sure how to fix this. Manually edit the config file. Sorry.\r\n" + textboxText);
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
            this.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.Close();
        }
    }
}
