namespace MCMyVault
{
    partial class NewProfileForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(NewProfileForm));
            this.labelNewProfileName = new System.Windows.Forms.Label();
            this.btnOKNewProfileName = new System.Windows.Forms.Button();
            this.txtbNewProfileName = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // labelNewProfileName
            // 
            this.labelNewProfileName.Location = new System.Drawing.Point(13, 13);
            this.labelNewProfileName.Name = "labelNewProfileName";
            this.labelNewProfileName.Size = new System.Drawing.Size(212, 31);
            this.labelNewProfileName.TabIndex = 0;
            this.labelNewProfileName.Text = "What is the name of your new profile? (examples: MC161, TekkitLite, BigDig, etc)";
            // 
            // btnOKNewProfileName
            // 
            this.btnOKNewProfileName.Location = new System.Drawing.Point(133, 111);
            this.btnOKNewProfileName.Name = "btnOKNewProfileName";
            this.btnOKNewProfileName.Size = new System.Drawing.Size(75, 23);
            this.btnOKNewProfileName.TabIndex = 3;
            this.btnOKNewProfileName.Text = "OK";
            this.btnOKNewProfileName.UseVisualStyleBackColor = true;
            this.btnOKNewProfileName.Click += new System.EventHandler(this.btnOKNewProfileName_Click);
            // 
            // txtbNewProfileName
            // 
            this.txtbNewProfileName.Location = new System.Drawing.Point(25, 59);
            this.txtbNewProfileName.Name = "txtbNewProfileName";
            this.txtbNewProfileName.Size = new System.Drawing.Size(292, 20);
            this.txtbNewProfileName.TabIndex = 2;
            // 
            // NewProfileForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(341, 151);
            this.Controls.Add(this.btnOKNewProfileName);
            this.Controls.Add(this.txtbNewProfileName);
            this.Controls.Add(this.labelNewProfileName);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "NewProfileForm";
            this.Text = "NewProfileForm";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label labelNewProfileName;
        private System.Windows.Forms.Button btnOKNewProfileName;
        private System.Windows.Forms.TextBox txtbNewProfileName;
    }
}