namespace MCMyVault
{
    partial class SettingschangeForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SettingschangeForm));
            this.txtSettingupdate = new System.Windows.Forms.TextBox();
            this.btnSettingchange = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // txtSettingupdate
            // 
            this.txtSettingupdate.Location = new System.Drawing.Point(36, 23);
            this.txtSettingupdate.Name = "txtSettingupdate";
            this.txtSettingupdate.Size = new System.Drawing.Size(265, 20);
            this.txtSettingupdate.TabIndex = 0;
            // 
            // btnSettingchange
            // 
            this.btnSettingchange.Location = new System.Drawing.Point(131, 49);
            this.btnSettingchange.Name = "btnSettingchange";
            this.btnSettingchange.Size = new System.Drawing.Size(75, 23);
            this.btnSettingchange.TabIndex = 1;
            this.btnSettingchange.Text = "OK";
            this.btnSettingchange.UseVisualStyleBackColor = true;
            this.btnSettingchange.Click += new System.EventHandler(this.btnSettingchange_Click);
            // 
            // SettingschangeForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(336, 104);
            this.Controls.Add(this.btnSettingchange);
            this.Controls.Add(this.txtSettingupdate);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "SettingschangeForm";
            this.Text = "Setting update...";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtSettingupdate;
        private System.Windows.Forms.Button btnSettingchange;
    }
}