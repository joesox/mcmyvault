namespace MCMyVault
{
    partial class Form1
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.backupAllToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.launchMinecraftexeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.restartMCMyVaultToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.viewToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.viewWorldUsingEihortToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.viewWorldUsingMcmapDZToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator7 = new System.Windows.Forms.ToolStripSeparator();
            this.openBackupLocationToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exploreBackuptexturpacksLocationToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exploreMinecraftSavedLocationToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exploreMinecraftTexturepacksLocationsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exploreMinecraftModFolderToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exploreToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.configiniFileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.logFileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.helpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.wikiAndVideosToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.supportSiteToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.checkForUpdateToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.readMeFileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator6 = new System.Windows.Forms.ToolStripSeparator();
            this.aboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabMain = new System.Windows.Forms.TabPage();
            this.btnBackupAll = new System.Windows.Forms.Button();
            this.btnBackup = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.ColumnMap = new System.Windows.Forms.DataGridViewImageColumn();
            this.ColumnName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColumnLastBackup = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColumnFilename = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tabBackups = new System.Windows.Forms.TabPage();
            this.listBoxBackups = new System.Windows.Forms.ListBox();
            this.tabSnapshots = new System.Windows.Forms.TabPage();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.listBoxSnapshots = new System.Windows.Forms.ListBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.label12 = new System.Windows.Forms.Label();
            this.btnCreateSnapshot = new System.Windows.Forms.Button();
            this.tabTexturepacks = new System.Windows.Forms.TabPage();
            this.groupBoxCurrentView = new System.Windows.Forms.GroupBox();
            this.radioButtonCurrentTP = new System.Windows.Forms.RadioButton();
            this.radioButtonInactiveTP = new System.Windows.Forms.RadioButton();
            this.lboxCurrentTP = new System.Windows.Forms.ListBox();
            this.picBoxItems = new System.Windows.Forms.PictureBox();
            this.picBoxWorldPreview = new System.Windows.Forms.PictureBox();
            this.picBoxlogo = new System.Windows.Forms.PictureBox();
            this.tabMyApps = new System.Windows.Forms.TabPage();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.tabSettings = new System.Windows.Forms.TabPage();
            this.btnBrowse2SnapshotsLoc = new System.Windows.Forms.Button();
            this.txtBoxSnapshotsLoc = new System.Windows.Forms.TextBox();
            this.label13 = new System.Windows.Forms.Label();
            this.btnBrowseCloudBackupLoc = new System.Windows.Forms.Button();
            this.txtBoxCloudBackup = new System.Windows.Forms.TextBox();
            this.label11 = new System.Windows.Forms.Label();
            this.numericUpDown1 = new System.Windows.Forms.NumericUpDown();
            this.label10 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.txtbMCtexturepacks = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.btnBrowseTPBackupsLoc = new System.Windows.Forms.Button();
            this.txtTexturepackbackups = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.btnBrowseBackupsLoc = new System.Windows.Forms.Button();
            this.txtbBackupsLoc = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.btnBrowseEihortLoc = new System.Windows.Forms.Button();
            this.txtbEihortLoc = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.btnBrowseMCsaved = new System.Windows.Forms.Button();
            this.txtbMCsaved = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.btnBrowseMCMAPDZ = new System.Windows.Forms.Button();
            this.txtbMCMAPDZLoc = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.btnBrowseMCServer = new System.Windows.Forms.Button();
            this.txtbMCServerLoc = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.btnBrowseMCExe = new System.Windows.Forms.Button();
            this.txtbMCExeLoc = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.bSave = new System.Windows.Forms.Button();
            this.label7 = new System.Windows.Forms.Label();
            this.tBoxAppPath = new System.Windows.Forms.TextBox();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.backupToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.renameSelectedBackupFileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.restoreSelectedBackupFileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.viewWorldUsingEihortToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.viewWorldUsingToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator5 = new System.Windows.Forms.ToolStripSeparator();
            this.runMcmapDZToUpdateMapToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openMcmapDZGUIToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.deleteSelectedWorldFromMinecraftToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.contextMenuStrip2 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.openSelectedBackupInEihortToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.renameSelectedBackupFileToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.restoreSelectedBackupFileToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.purgekeepXDaysForYToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.sendCopyToCloudStorageToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.deleteToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.btnMinecraft = new System.Windows.Forms.Button();
            this.contextMenuStripTP = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.backupToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.restoreToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.deleteThisTexturepackToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.contextMenuStrip3 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.toolStripMenuRestoreSnap = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuRenamesnap = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuDeleteSnap = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip1.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tabMain.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.tabBackups.SuspendLayout();
            this.tabSnapshots.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.tabTexturepacks.SuspendLayout();
            this.groupBoxCurrentView.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picBoxItems)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picBoxWorldPreview)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picBoxlogo)).BeginInit();
            this.tabMyApps.SuspendLayout();
            this.tabSettings.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).BeginInit();
            this.contextMenuStrip1.SuspendLayout();
            this.contextMenuStrip2.SuspendLayout();
            this.contextMenuStripTP.SuspendLayout();
            this.contextMenuStrip3.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.viewToolStripMenuItem,
            this.helpToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(548, 24);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.backupAllToolStripMenuItem,
            this.launchMinecraftexeToolStripMenuItem,
            this.toolStripSeparator3,
            this.restartMCMyVaultToolStripMenuItem,
            this.exitToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // backupAllToolStripMenuItem
            // 
            this.backupAllToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("backupAllToolStripMenuItem.Image")));
            this.backupAllToolStripMenuItem.Name = "backupAllToolStripMenuItem";
            this.backupAllToolStripMenuItem.Size = new System.Drawing.Size(187, 22);
            this.backupAllToolStripMenuItem.Text = "Backup all";
            this.backupAllToolStripMenuItem.Click += new System.EventHandler(this.backupAllToolStripMenuItem_Click);
            // 
            // launchMinecraftexeToolStripMenuItem
            // 
            this.launchMinecraftexeToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("launchMinecraftexeToolStripMenuItem.Image")));
            this.launchMinecraftexeToolStripMenuItem.Name = "launchMinecraftexeToolStripMenuItem";
            this.launchMinecraftexeToolStripMenuItem.Size = new System.Drawing.Size(187, 22);
            this.launchMinecraftexeToolStripMenuItem.Text = "Launch Minecraft.exe";
            this.launchMinecraftexeToolStripMenuItem.Click += new System.EventHandler(this.launchMinecraftexeToolStripMenuItem_Click);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(184, 6);
            // 
            // restartMCMyVaultToolStripMenuItem
            // 
            this.restartMCMyVaultToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("restartMCMyVaultToolStripMenuItem.Image")));
            this.restartMCMyVaultToolStripMenuItem.Name = "restartMCMyVaultToolStripMenuItem";
            this.restartMCMyVaultToolStripMenuItem.Size = new System.Drawing.Size(187, 22);
            this.restartMCMyVaultToolStripMenuItem.Text = "Restart MCMyVault";
            this.restartMCMyVaultToolStripMenuItem.Click += new System.EventHandler(this.restartMCMyVaultToolStripMenuItem_Click);
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("exitToolStripMenuItem.Image")));
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(187, 22);
            this.exitToolStripMenuItem.Text = "Exit";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
            // 
            // viewToolStripMenuItem
            // 
            this.viewToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.viewWorldUsingEihortToolStripMenuItem1,
            this.viewWorldUsingMcmapDZToolStripMenuItem,
            this.toolStripSeparator7,
            this.openBackupLocationToolStripMenuItem,
            this.exploreBackuptexturpacksLocationToolStripMenuItem,
            this.exploreMinecraftSavedLocationToolStripMenuItem,
            this.exploreMinecraftTexturepacksLocationsToolStripMenuItem,
            this.exploreMinecraftModFolderToolStripMenuItem,
            this.exploreToolStripMenuItem,
            this.toolStripSeparator2,
            this.configiniFileToolStripMenuItem,
            this.logFileToolStripMenuItem});
            this.viewToolStripMenuItem.Name = "viewToolStripMenuItem";
            this.viewToolStripMenuItem.Size = new System.Drawing.Size(44, 20);
            this.viewToolStripMenuItem.Text = "View";
            // 
            // viewWorldUsingEihortToolStripMenuItem1
            // 
            this.viewWorldUsingEihortToolStripMenuItem1.Image = global::MCMyVault.Properties.Resources.eihort;
            this.viewWorldUsingEihortToolStripMenuItem1.Name = "viewWorldUsingEihortToolStripMenuItem1";
            this.viewWorldUsingEihortToolStripMenuItem1.Size = new System.Drawing.Size(286, 22);
            this.viewWorldUsingEihortToolStripMenuItem1.Text = "View world using eihort";
            this.viewWorldUsingEihortToolStripMenuItem1.Click += new System.EventHandler(this.viewWorldUsingEihortToolStripMenuItem1_Click);
            // 
            // viewWorldUsingMcmapDZToolStripMenuItem
            // 
            this.viewWorldUsingMcmapDZToolStripMenuItem.Image = global::MCMyVault.Properties.Resources.mcmapDZ;
            this.viewWorldUsingMcmapDZToolStripMenuItem.Name = "viewWorldUsingMcmapDZToolStripMenuItem";
            this.viewWorldUsingMcmapDZToolStripMenuItem.Size = new System.Drawing.Size(286, 22);
            this.viewWorldUsingMcmapDZToolStripMenuItem.Text = "View world using mcmapDZ";
            this.viewWorldUsingMcmapDZToolStripMenuItem.Click += new System.EventHandler(this.viewWorldUsingMcmapDZToolStripMenuItem_Click);
            // 
            // toolStripSeparator7
            // 
            this.toolStripSeparator7.Name = "toolStripSeparator7";
            this.toolStripSeparator7.Size = new System.Drawing.Size(283, 6);
            // 
            // openBackupLocationToolStripMenuItem
            // 
            this.openBackupLocationToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("openBackupLocationToolStripMenuItem.Image")));
            this.openBackupLocationToolStripMenuItem.Name = "openBackupLocationToolStripMenuItem";
            this.openBackupLocationToolStripMenuItem.Size = new System.Drawing.Size(286, 22);
            this.openBackupLocationToolStripMenuItem.Text = "Explore backup location";
            this.openBackupLocationToolStripMenuItem.Click += new System.EventHandler(this.openBackupLocationToolStripMenuItem_Click);
            // 
            // exploreBackuptexturpacksLocationToolStripMenuItem
            // 
            this.exploreBackuptexturpacksLocationToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("exploreBackuptexturpacksLocationToolStripMenuItem.Image")));
            this.exploreBackuptexturpacksLocationToolStripMenuItem.Name = "exploreBackuptexturpacksLocationToolStripMenuItem";
            this.exploreBackuptexturpacksLocationToolStripMenuItem.Size = new System.Drawing.Size(286, 22);
            this.exploreBackuptexturpacksLocationToolStripMenuItem.Text = "Explore backup-texturepacks location";
            this.exploreBackuptexturpacksLocationToolStripMenuItem.Click += new System.EventHandler(this.exploreBackuptexturpacksLocationToolStripMenuItem_Click);
            // 
            // exploreMinecraftSavedLocationToolStripMenuItem
            // 
            this.exploreMinecraftSavedLocationToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("exploreMinecraftSavedLocationToolStripMenuItem.Image")));
            this.exploreMinecraftSavedLocationToolStripMenuItem.Name = "exploreMinecraftSavedLocationToolStripMenuItem";
            this.exploreMinecraftSavedLocationToolStripMenuItem.Size = new System.Drawing.Size(286, 22);
            this.exploreMinecraftSavedLocationToolStripMenuItem.Text = "Explore Minecraft saved location";
            this.exploreMinecraftSavedLocationToolStripMenuItem.Click += new System.EventHandler(this.exploreMinecraftSavedLocationToolStripMenuItem_Click);
            // 
            // exploreMinecraftTexturepacksLocationsToolStripMenuItem
            // 
            this.exploreMinecraftTexturepacksLocationsToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("exploreMinecraftTexturepacksLocationsToolStripMenuItem.Image")));
            this.exploreMinecraftTexturepacksLocationsToolStripMenuItem.Name = "exploreMinecraftTexturepacksLocationsToolStripMenuItem";
            this.exploreMinecraftTexturepacksLocationsToolStripMenuItem.Size = new System.Drawing.Size(286, 22);
            this.exploreMinecraftTexturepacksLocationsToolStripMenuItem.Text = "Explore Minecraft texturepacks locations";
            this.exploreMinecraftTexturepacksLocationsToolStripMenuItem.Click += new System.EventHandler(this.exploreMinecraftTexturepacksLocationsToolStripMenuItem_Click);
            // 
            // exploreMinecraftModFolderToolStripMenuItem
            // 
            this.exploreMinecraftModFolderToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("exploreMinecraftModFolderToolStripMenuItem.Image")));
            this.exploreMinecraftModFolderToolStripMenuItem.Name = "exploreMinecraftModFolderToolStripMenuItem";
            this.exploreMinecraftModFolderToolStripMenuItem.Size = new System.Drawing.Size(286, 22);
            this.exploreMinecraftModFolderToolStripMenuItem.Text = "Explore Minecraft mod folder";
            this.exploreMinecraftModFolderToolStripMenuItem.Click += new System.EventHandler(this.exploreMinecraftModFolderToolStripMenuItem_Click);
            // 
            // exploreToolStripMenuItem
            // 
            this.exploreToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("exploreToolStripMenuItem.Image")));
            this.exploreToolStripMenuItem.Name = "exploreToolStripMenuItem";
            this.exploreToolStripMenuItem.Size = new System.Drawing.Size(286, 22);
            this.exploreToolStripMenuItem.Text = "Explore cloud backup location";
            this.exploreToolStripMenuItem.Click += new System.EventHandler(this.exploreToolStripMenuItem_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(283, 6);
            // 
            // configiniFileToolStripMenuItem
            // 
            this.configiniFileToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("configiniFileToolStripMenuItem.Image")));
            this.configiniFileToolStripMenuItem.Name = "configiniFileToolStripMenuItem";
            this.configiniFileToolStripMenuItem.Size = new System.Drawing.Size(286, 22);
            this.configiniFileToolStripMenuItem.Text = "Config.ini File";
            this.configiniFileToolStripMenuItem.Click += new System.EventHandler(this.configiniFileToolStripMenuItem_Click);
            // 
            // logFileToolStripMenuItem
            // 
            this.logFileToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("logFileToolStripMenuItem.Image")));
            this.logFileToolStripMenuItem.Name = "logFileToolStripMenuItem";
            this.logFileToolStripMenuItem.Size = new System.Drawing.Size(286, 22);
            this.logFileToolStripMenuItem.Text = "Log File";
            this.logFileToolStripMenuItem.Click += new System.EventHandler(this.logFileToolStripMenuItem_Click);
            // 
            // helpToolStripMenuItem
            // 
            this.helpToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.wikiAndVideosToolStripMenuItem,
            this.supportSiteToolStripMenuItem,
            this.checkForUpdateToolStripMenuItem,
            this.readMeFileToolStripMenuItem,
            this.toolStripSeparator6,
            this.aboutToolStripMenuItem});
            this.helpToolStripMenuItem.Name = "helpToolStripMenuItem";
            this.helpToolStripMenuItem.Size = new System.Drawing.Size(44, 20);
            this.helpToolStripMenuItem.Text = "Help";
            // 
            // wikiAndVideosToolStripMenuItem
            // 
            this.wikiAndVideosToolStripMenuItem.Name = "wikiAndVideosToolStripMenuItem";
            this.wikiAndVideosToolStripMenuItem.Size = new System.Drawing.Size(167, 22);
            this.wikiAndVideosToolStripMenuItem.Text = "Wiki and Videos";
            this.wikiAndVideosToolStripMenuItem.Click += new System.EventHandler(this.wikiAndVideosToolStripMenuItem_Click);
            // 
            // supportSiteToolStripMenuItem
            // 
            this.supportSiteToolStripMenuItem.Name = "supportSiteToolStripMenuItem";
            this.supportSiteToolStripMenuItem.Size = new System.Drawing.Size(167, 22);
            this.supportSiteToolStripMenuItem.Text = "Support Purchase";
            this.supportSiteToolStripMenuItem.Click += new System.EventHandler(this.supportSiteToolStripMenuItem_Click);
            // 
            // checkForUpdateToolStripMenuItem
            // 
            this.checkForUpdateToolStripMenuItem.Name = "checkForUpdateToolStripMenuItem";
            this.checkForUpdateToolStripMenuItem.Size = new System.Drawing.Size(167, 22);
            this.checkForUpdateToolStripMenuItem.Text = "Check for update";
            this.checkForUpdateToolStripMenuItem.Click += new System.EventHandler(this.checkForUpdateToolStripMenuItem_Click);
            // 
            // readMeFileToolStripMenuItem
            // 
            this.readMeFileToolStripMenuItem.Name = "readMeFileToolStripMenuItem";
            this.readMeFileToolStripMenuItem.Size = new System.Drawing.Size(167, 22);
            this.readMeFileToolStripMenuItem.Text = "ReadMe file";
            this.readMeFileToolStripMenuItem.Click += new System.EventHandler(this.readMeFileToolStripMenuItem_Click);
            // 
            // toolStripSeparator6
            // 
            this.toolStripSeparator6.Name = "toolStripSeparator6";
            this.toolStripSeparator6.Size = new System.Drawing.Size(164, 6);
            // 
            // aboutToolStripMenuItem
            // 
            this.aboutToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("aboutToolStripMenuItem.Image")));
            this.aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
            this.aboutToolStripMenuItem.Size = new System.Drawing.Size(167, 22);
            this.aboutToolStripMenuItem.Text = "About";
            this.aboutToolStripMenuItem.Click += new System.EventHandler(this.aboutToolStripMenuItem_Click);
            // 
            // tabControl1
            // 
            this.tabControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tabControl1.Controls.Add(this.tabMain);
            this.tabControl1.Controls.Add(this.tabBackups);
            this.tabControl1.Controls.Add(this.tabSnapshots);
            this.tabControl1.Controls.Add(this.tabTexturepacks);
            this.tabControl1.Controls.Add(this.tabMyApps);
            this.tabControl1.Controls.Add(this.tabSettings);
            this.tabControl1.Location = new System.Drawing.Point(0, 56);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(536, 417);
            this.tabControl1.TabIndex = 1;
            this.tabControl1.SelectedIndexChanged += new System.EventHandler(this.tabControl1_SelectedIndexChanged);
            // 
            // tabMain
            // 
            this.tabMain.Controls.Add(this.btnBackupAll);
            this.tabMain.Controls.Add(this.btnBackup);
            this.tabMain.Controls.Add(this.groupBox1);
            this.tabMain.Location = new System.Drawing.Point(4, 22);
            this.tabMain.Name = "tabMain";
            this.tabMain.Padding = new System.Windows.Forms.Padding(3);
            this.tabMain.Size = new System.Drawing.Size(528, 391);
            this.tabMain.TabIndex = 0;
            this.tabMain.Text = "Main";
            this.tabMain.UseVisualStyleBackColor = true;
            this.tabMain.Enter += new System.EventHandler(this.tabMain_Enter);
            // 
            // btnBackupAll
            // 
            this.btnBackupAll.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnBackupAll.Location = new System.Drawing.Point(177, 362);
            this.btnBackupAll.Name = "btnBackupAll";
            this.btnBackupAll.Size = new System.Drawing.Size(157, 23);
            this.btnBackupAll.TabIndex = 5;
            this.btnBackupAll.Text = "Backup All!";
            this.btnBackupAll.UseVisualStyleBackColor = true;
            this.btnBackupAll.Click += new System.EventHandler(this.btnBackupAll_Click);
            // 
            // btnBackup
            // 
            this.btnBackup.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnBackup.Location = new System.Drawing.Point(14, 362);
            this.btnBackup.Name = "btnBackup";
            this.btnBackup.Size = new System.Drawing.Size(157, 23);
            this.btnBackup.TabIndex = 4;
            this.btnBackup.Text = "Backup Selected!";
            this.btnBackup.UseVisualStyleBackColor = true;
            this.btnBackup.Click += new System.EventHandler(this.btnBackup_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.dataGridView1);
            this.groupBox1.Location = new System.Drawing.Point(8, 7);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(514, 349);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Current saved Minecraft worlds...";
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.AllowUserToOrderColumns = true;
            this.dataGridView1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridView1.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridView1.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.DisplayedCells;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ColumnMap,
            this.ColumnName,
            this.ColumnLastBackup,
            this.ColumnFilename});
            this.dataGridView1.Location = new System.Drawing.Point(7, 20);
            this.dataGridView1.MultiSelect = false;
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ReadOnly = true;
            this.dataGridView1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridView1.Size = new System.Drawing.Size(501, 323);
            this.dataGridView1.TabIndex = 0;
            this.dataGridView1.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellDoubleClick);
            this.dataGridView1.MouseDown += new System.Windows.Forms.MouseEventHandler(this.dataGridView1_MouseDown);
            this.dataGridView1.MouseHover += new System.EventHandler(this.dataGridView1_MouseHover);
            // 
            // ColumnMap
            // 
            this.ColumnMap.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells;
            this.ColumnMap.HeaderText = "Map";
            this.ColumnMap.Name = "ColumnMap";
            this.ColumnMap.ReadOnly = true;
            this.ColumnMap.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.ColumnMap.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.ColumnMap.Width = 53;
            // 
            // ColumnName
            // 
            this.ColumnName.HeaderText = "Name";
            this.ColumnName.Name = "ColumnName";
            this.ColumnName.ReadOnly = true;
            // 
            // ColumnLastBackup
            // 
            this.ColumnLastBackup.HeaderText = "Last Backup";
            this.ColumnLastBackup.Name = "ColumnLastBackup";
            this.ColumnLastBackup.ReadOnly = true;
            // 
            // ColumnFilename
            // 
            this.ColumnFilename.HeaderText = "Filename";
            this.ColumnFilename.Name = "ColumnFilename";
            this.ColumnFilename.ReadOnly = true;
            // 
            // tabBackups
            // 
            this.tabBackups.Controls.Add(this.listBoxBackups);
            this.tabBackups.Location = new System.Drawing.Point(4, 22);
            this.tabBackups.Name = "tabBackups";
            this.tabBackups.Size = new System.Drawing.Size(528, 391);
            this.tabBackups.TabIndex = 2;
            this.tabBackups.Text = "Backups";
            this.tabBackups.UseVisualStyleBackColor = true;
            this.tabBackups.Enter += new System.EventHandler(this.tabBackups_Enter);
            // 
            // listBoxBackups
            // 
            this.listBoxBackups.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.listBoxBackups.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.listBoxBackups.FormattingEnabled = true;
            this.listBoxBackups.ItemHeight = 16;
            this.listBoxBackups.Location = new System.Drawing.Point(9, 24);
            this.listBoxBackups.Name = "listBoxBackups";
            this.listBoxBackups.Size = new System.Drawing.Size(516, 356);
            this.listBoxBackups.TabIndex = 0;
            this.listBoxBackups.MouseDown += new System.Windows.Forms.MouseEventHandler(this.listBoxBackups_MouseDown);
            // 
            // tabSnapshots
            // 
            this.tabSnapshots.Controls.Add(this.groupBox4);
            this.tabSnapshots.Controls.Add(this.groupBox3);
            this.tabSnapshots.Location = new System.Drawing.Point(4, 22);
            this.tabSnapshots.Name = "tabSnapshots";
            this.tabSnapshots.Size = new System.Drawing.Size(528, 391);
            this.tabSnapshots.TabIndex = 5;
            this.tabSnapshots.Text = "Snapshots";
            this.tabSnapshots.UseVisualStyleBackColor = true;
            this.tabSnapshots.Enter += new System.EventHandler(this.tabSnapshots_Enter);
            // 
            // groupBox4
            // 
            this.groupBox4.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox4.Controls.Add(this.listBoxSnapshots);
            this.groupBox4.Location = new System.Drawing.Point(8, 84);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(514, 304);
            this.groupBox4.TabIndex = 3;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Current Snapshots...";
            // 
            // listBoxSnapshots
            // 
            this.listBoxSnapshots.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.listBoxSnapshots.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.listBoxSnapshots.FormattingEnabled = true;
            this.listBoxSnapshots.ItemHeight = 16;
            this.listBoxSnapshots.Location = new System.Drawing.Point(6, 19);
            this.listBoxSnapshots.Name = "listBoxSnapshots";
            this.listBoxSnapshots.Size = new System.Drawing.Size(502, 276);
            this.listBoxSnapshots.TabIndex = 1;
            this.listBoxSnapshots.MouseDown += new System.Windows.Forms.MouseEventHandler(this.listBoxSnapshots_MouseDown);
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.label12);
            this.groupBox3.Controls.Add(this.btnCreateSnapshot);
            this.groupBox3.Location = new System.Drawing.Point(8, 3);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(514, 75);
            this.groupBox3.TabIndex = 2;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Snapshot creation...";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(7, 20);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(457, 13);
            this.label12.TabIndex = 40;
            this.label12.Text = "Archive your entire \\.minecraft folder. Take a current snap or your environment a" +
    "s it is right now.";
            // 
            // btnCreateSnapshot
            // 
            this.btnCreateSnapshot.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCreateSnapshot.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCreateSnapshot.Location = new System.Drawing.Point(107, 36);
            this.btnCreateSnapshot.Name = "btnCreateSnapshot";
            this.btnCreateSnapshot.Size = new System.Drawing.Size(298, 34);
            this.btnCreateSnapshot.TabIndex = 39;
            this.btnCreateSnapshot.Text = "Take \\.minecraft SNAPSHOT!";
            this.btnCreateSnapshot.UseVisualStyleBackColor = true;
            this.btnCreateSnapshot.Click += new System.EventHandler(this.btnCreateSnapshot_Click);
            // 
            // tabTexturepacks
            // 
            this.tabTexturepacks.Controls.Add(this.groupBoxCurrentView);
            this.tabTexturepacks.Controls.Add(this.lboxCurrentTP);
            this.tabTexturepacks.Controls.Add(this.picBoxItems);
            this.tabTexturepacks.Controls.Add(this.picBoxWorldPreview);
            this.tabTexturepacks.Controls.Add(this.picBoxlogo);
            this.tabTexturepacks.Location = new System.Drawing.Point(4, 22);
            this.tabTexturepacks.Name = "tabTexturepacks";
            this.tabTexturepacks.Size = new System.Drawing.Size(528, 391);
            this.tabTexturepacks.TabIndex = 4;
            this.tabTexturepacks.Text = "Texturepacks";
            this.tabTexturepacks.UseVisualStyleBackColor = true;
            // 
            // groupBoxCurrentView
            // 
            this.groupBoxCurrentView.Controls.Add(this.radioButtonCurrentTP);
            this.groupBoxCurrentView.Controls.Add(this.radioButtonInactiveTP);
            this.groupBoxCurrentView.Location = new System.Drawing.Point(8, 3);
            this.groupBoxCurrentView.Name = "groupBoxCurrentView";
            this.groupBoxCurrentView.Size = new System.Drawing.Size(193, 67);
            this.groupBoxCurrentView.TabIndex = 7;
            this.groupBoxCurrentView.TabStop = false;
            this.groupBoxCurrentView.Text = "Current View...";
            // 
            // radioButtonCurrentTP
            // 
            this.radioButtonCurrentTP.AutoSize = true;
            this.radioButtonCurrentTP.Checked = true;
            this.radioButtonCurrentTP.Location = new System.Drawing.Point(6, 16);
            this.radioButtonCurrentTP.Name = "radioButtonCurrentTP";
            this.radioButtonCurrentTP.Size = new System.Drawing.Size(160, 17);
            this.radioButtonCurrentTP.TabIndex = 5;
            this.radioButtonCurrentTP.TabStop = true;
            this.radioButtonCurrentTP.Text = "Current Active Texturepacks";
            this.radioButtonCurrentTP.UseVisualStyleBackColor = true;
            this.radioButtonCurrentTP.CheckedChanged += new System.EventHandler(this.radioButtonCurrentTP_CheckedChanged);
            // 
            // radioButtonInactiveTP
            // 
            this.radioButtonInactiveTP.AutoSize = true;
            this.radioButtonInactiveTP.Location = new System.Drawing.Point(6, 34);
            this.radioButtonInactiveTP.Name = "radioButtonInactiveTP";
            this.radioButtonInactiveTP.Size = new System.Drawing.Size(188, 17);
            this.radioButtonInactiveTP.TabIndex = 6;
            this.radioButtonInactiveTP.TabStop = true;
            this.radioButtonInactiveTP.Text = "Backed-up & inactive Texturepacks";
            this.radioButtonInactiveTP.UseVisualStyleBackColor = true;
            this.radioButtonInactiveTP.CheckedChanged += new System.EventHandler(this.radioButtonInactiveTP_CheckedChanged);
            // 
            // lboxCurrentTP
            // 
            this.lboxCurrentTP.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.lboxCurrentTP.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lboxCurrentTP.FormattingEnabled = true;
            this.lboxCurrentTP.ItemHeight = 14;
            this.lboxCurrentTP.Location = new System.Drawing.Point(3, 202);
            this.lboxCurrentTP.Name = "lboxCurrentTP";
            this.lboxCurrentTP.Size = new System.Drawing.Size(217, 186);
            this.lboxCurrentTP.TabIndex = 1;
            this.lboxCurrentTP.SelectedIndexChanged += new System.EventHandler(this.lboxCurrentTP_SelectedIndexChanged);
            this.lboxCurrentTP.MouseDown += new System.Windows.Forms.MouseEventHandler(this.lboxCurrentTP_MouseDown);
            // 
            // picBoxItems
            // 
            this.picBoxItems.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.picBoxItems.BackColor = System.Drawing.Color.White;
            this.picBoxItems.Location = new System.Drawing.Point(226, 76);
            this.picBoxItems.Name = "picBoxItems";
            this.picBoxItems.Size = new System.Drawing.Size(299, 312);
            this.picBoxItems.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.picBoxItems.TabIndex = 4;
            this.picBoxItems.TabStop = false;
            // 
            // picBoxWorldPreview
            // 
            this.picBoxWorldPreview.Location = new System.Drawing.Point(33, 72);
            this.picBoxWorldPreview.Name = "picBoxWorldPreview";
            this.picBoxWorldPreview.Size = new System.Drawing.Size(128, 128);
            this.picBoxWorldPreview.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.picBoxWorldPreview.TabIndex = 3;
            this.picBoxWorldPreview.TabStop = false;
            // 
            // picBoxlogo
            // 
            this.picBoxlogo.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.picBoxlogo.Location = new System.Drawing.Point(226, 26);
            this.picBoxlogo.Name = "picBoxlogo";
            this.picBoxlogo.Size = new System.Drawing.Size(277, 44);
            this.picBoxlogo.TabIndex = 2;
            this.picBoxlogo.TabStop = false;
            // 
            // tabMyApps
            // 
            this.tabMyApps.Controls.Add(this.groupBox2);
            this.tabMyApps.Location = new System.Drawing.Point(4, 22);
            this.tabMyApps.Name = "tabMyApps";
            this.tabMyApps.Size = new System.Drawing.Size(528, 391);
            this.tabMyApps.TabIndex = 3;
            this.tabMyApps.Text = "MyApps";
            this.tabMyApps.UseVisualStyleBackColor = true;
            // 
            // groupBox2
            // 
            this.groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox2.Location = new System.Drawing.Point(9, 14);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(516, 374);
            this.groupBox2.TabIndex = 0;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "My favorite Minecraft apps...";
            // 
            // tabSettings
            // 
            this.tabSettings.Controls.Add(this.btnBrowse2SnapshotsLoc);
            this.tabSettings.Controls.Add(this.txtBoxSnapshotsLoc);
            this.tabSettings.Controls.Add(this.label13);
            this.tabSettings.Controls.Add(this.btnBrowseCloudBackupLoc);
            this.tabSettings.Controls.Add(this.txtBoxCloudBackup);
            this.tabSettings.Controls.Add(this.label11);
            this.tabSettings.Controls.Add(this.numericUpDown1);
            this.tabSettings.Controls.Add(this.label10);
            this.tabSettings.Controls.Add(this.button1);
            this.tabSettings.Controls.Add(this.txtbMCtexturepacks);
            this.tabSettings.Controls.Add(this.label9);
            this.tabSettings.Controls.Add(this.btnBrowseTPBackupsLoc);
            this.tabSettings.Controls.Add(this.txtTexturepackbackups);
            this.tabSettings.Controls.Add(this.label8);
            this.tabSettings.Controls.Add(this.btnBrowseBackupsLoc);
            this.tabSettings.Controls.Add(this.txtbBackupsLoc);
            this.tabSettings.Controls.Add(this.label6);
            this.tabSettings.Controls.Add(this.btnBrowseEihortLoc);
            this.tabSettings.Controls.Add(this.txtbEihortLoc);
            this.tabSettings.Controls.Add(this.label5);
            this.tabSettings.Controls.Add(this.btnBrowseMCsaved);
            this.tabSettings.Controls.Add(this.txtbMCsaved);
            this.tabSettings.Controls.Add(this.label4);
            this.tabSettings.Controls.Add(this.btnBrowseMCMAPDZ);
            this.tabSettings.Controls.Add(this.txtbMCMAPDZLoc);
            this.tabSettings.Controls.Add(this.label3);
            this.tabSettings.Controls.Add(this.btnBrowseMCServer);
            this.tabSettings.Controls.Add(this.txtbMCServerLoc);
            this.tabSettings.Controls.Add(this.label2);
            this.tabSettings.Controls.Add(this.btnBrowseMCExe);
            this.tabSettings.Controls.Add(this.txtbMCExeLoc);
            this.tabSettings.Controls.Add(this.label1);
            this.tabSettings.Controls.Add(this.bSave);
            this.tabSettings.Controls.Add(this.label7);
            this.tabSettings.Controls.Add(this.tBoxAppPath);
            this.tabSettings.Location = new System.Drawing.Point(4, 22);
            this.tabSettings.Name = "tabSettings";
            this.tabSettings.Padding = new System.Windows.Forms.Padding(3);
            this.tabSettings.Size = new System.Drawing.Size(528, 391);
            this.tabSettings.TabIndex = 1;
            this.tabSettings.Text = "Settings";
            this.tabSettings.UseVisualStyleBackColor = true;
            // 
            // btnBrowse2SnapshotsLoc
            // 
            this.btnBrowse2SnapshotsLoc.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnBrowse2SnapshotsLoc.Location = new System.Drawing.Point(434, 277);
            this.btnBrowse2SnapshotsLoc.Name = "btnBrowse2SnapshotsLoc";
            this.btnBrowse2SnapshotsLoc.Size = new System.Drawing.Size(75, 23);
            this.btnBrowse2SnapshotsLoc.TabIndex = 70;
            this.btnBrowse2SnapshotsLoc.Text = "Browse...";
            this.btnBrowse2SnapshotsLoc.UseVisualStyleBackColor = true;
            this.btnBrowse2SnapshotsLoc.Click += new System.EventHandler(this.btnBrowse2SnapshotsLoc_Click);
            // 
            // txtBoxSnapshotsLoc
            // 
            this.txtBoxSnapshotsLoc.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtBoxSnapshotsLoc.Location = new System.Drawing.Point(129, 277);
            this.txtBoxSnapshotsLoc.Name = "txtBoxSnapshotsLoc";
            this.txtBoxSnapshotsLoc.Size = new System.Drawing.Size(299, 20);
            this.txtBoxSnapshotsLoc.TabIndex = 69;
            this.txtBoxSnapshotsLoc.TextChanged += new System.EventHandler(this.txtBoxSnapshotsLoc_TextChanged);
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(9, 277);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(104, 13);
            this.label13.TabIndex = 68;
            this.label13.Text = "Snapshots Location:";
            // 
            // btnBrowseCloudBackupLoc
            // 
            this.btnBrowseCloudBackupLoc.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnBrowseCloudBackupLoc.Location = new System.Drawing.Point(434, 251);
            this.btnBrowseCloudBackupLoc.Name = "btnBrowseCloudBackupLoc";
            this.btnBrowseCloudBackupLoc.Size = new System.Drawing.Size(75, 23);
            this.btnBrowseCloudBackupLoc.TabIndex = 67;
            this.btnBrowseCloudBackupLoc.Text = "Browse...";
            this.btnBrowseCloudBackupLoc.UseVisualStyleBackColor = true;
            this.btnBrowseCloudBackupLoc.Click += new System.EventHandler(this.btnBrowseCloudBackupLoc_Click);
            // 
            // txtBoxCloudBackup
            // 
            this.txtBoxCloudBackup.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtBoxCloudBackup.Location = new System.Drawing.Point(129, 251);
            this.txtBoxCloudBackup.Name = "txtBoxCloudBackup";
            this.txtBoxCloudBackup.Size = new System.Drawing.Size(299, 20);
            this.txtBoxCloudBackup.TabIndex = 66;
            this.txtBoxCloudBackup.TextChanged += new System.EventHandler(this.txtBoxCloudBackup_TextChanged);
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(9, 251);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(121, 13);
            this.label11.TabIndex = 65;
            this.label11.Text = "Cloud Backup Location:";
            // 
            // numericUpDown1
            // 
            this.numericUpDown1.Location = new System.Drawing.Point(227, 334);
            this.numericUpDown1.Maximum = new decimal(new int[] {
            364,
            0,
            0,
            0});
            this.numericUpDown1.Name = "numericUpDown1";
            this.numericUpDown1.Size = new System.Drawing.Size(47, 20);
            this.numericUpDown1.TabIndex = 64;
            this.numericUpDown1.ValueChanged += new System.EventHandler(this.numericUpDown1_ValueChanged);
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(9, 336);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(212, 13);
            this.label10.TabIndex = 63;
            this.label10.Text = "Cleanup days (days to examine for purging):";
            // 
            // button1
            // 
            this.button1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.button1.Location = new System.Drawing.Point(434, 135);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 62;
            this.button1.Text = "Browse...";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // txtbMCtexturepacks
            // 
            this.txtbMCtexturepacks.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtbMCtexturepacks.Location = new System.Drawing.Point(129, 135);
            this.txtbMCtexturepacks.Name = "txtbMCtexturepacks";
            this.txtbMCtexturepacks.Size = new System.Drawing.Size(299, 20);
            this.txtbMCtexturepacks.TabIndex = 61;
            this.txtbMCtexturepacks.TextChanged += new System.EventHandler(this.txtbMCtexturepacks_TextChanged);
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(9, 135);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(118, 13);
            this.label9.TabIndex = 60;
            this.label9.Text = "Minecraft texturepacks:";
            // 
            // btnBrowseTPBackupsLoc
            // 
            this.btnBrowseTPBackupsLoc.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnBrowseTPBackupsLoc.Location = new System.Drawing.Point(434, 222);
            this.btnBrowseTPBackupsLoc.Name = "btnBrowseTPBackupsLoc";
            this.btnBrowseTPBackupsLoc.Size = new System.Drawing.Size(75, 23);
            this.btnBrowseTPBackupsLoc.TabIndex = 59;
            this.btnBrowseTPBackupsLoc.Text = "Browse...";
            this.btnBrowseTPBackupsLoc.UseVisualStyleBackColor = true;
            this.btnBrowseTPBackupsLoc.Click += new System.EventHandler(this.btnBrowseTPBackupsLoc_Click);
            // 
            // txtTexturepackbackups
            // 
            this.txtTexturepackbackups.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtTexturepackbackups.Location = new System.Drawing.Point(129, 222);
            this.txtTexturepackbackups.Name = "txtTexturepackbackups";
            this.txtTexturepackbackups.Size = new System.Drawing.Size(299, 20);
            this.txtTexturepackbackups.TabIndex = 58;
            this.txtTexturepackbackups.TextChanged += new System.EventHandler(this.txtTexturepackbackups_TextChanged);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(9, 222);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(111, 13);
            this.label8.TabIndex = 57;
            this.label8.Text = "texturepack Backups:";
            // 
            // btnBrowseBackupsLoc
            // 
            this.btnBrowseBackupsLoc.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnBrowseBackupsLoc.Location = new System.Drawing.Point(434, 193);
            this.btnBrowseBackupsLoc.Name = "btnBrowseBackupsLoc";
            this.btnBrowseBackupsLoc.Size = new System.Drawing.Size(75, 23);
            this.btnBrowseBackupsLoc.TabIndex = 56;
            this.btnBrowseBackupsLoc.Text = "Browse...";
            this.btnBrowseBackupsLoc.UseVisualStyleBackColor = true;
            this.btnBrowseBackupsLoc.Click += new System.EventHandler(this.btnBrowseBackupsLoc_Click);
            // 
            // txtbBackupsLoc
            // 
            this.txtbBackupsLoc.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtbBackupsLoc.Location = new System.Drawing.Point(129, 193);
            this.txtbBackupsLoc.Name = "txtbBackupsLoc";
            this.txtbBackupsLoc.Size = new System.Drawing.Size(299, 20);
            this.txtbBackupsLoc.TabIndex = 55;
            this.txtbBackupsLoc.TextChanged += new System.EventHandler(this.txtbBackupsLoc_TextChanged);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(9, 193);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(92, 13);
            this.label6.TabIndex = 54;
            this.label6.Text = "Backups location:";
            // 
            // btnBrowseEihortLoc
            // 
            this.btnBrowseEihortLoc.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnBrowseEihortLoc.Location = new System.Drawing.Point(434, 164);
            this.btnBrowseEihortLoc.Name = "btnBrowseEihortLoc";
            this.btnBrowseEihortLoc.Size = new System.Drawing.Size(75, 23);
            this.btnBrowseEihortLoc.TabIndex = 53;
            this.btnBrowseEihortLoc.Text = "Browse...";
            this.btnBrowseEihortLoc.UseVisualStyleBackColor = true;
            this.btnBrowseEihortLoc.Click += new System.EventHandler(this.btnBrowseEihortLoc_Click);
            // 
            // txtbEihortLoc
            // 
            this.txtbEihortLoc.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtbEihortLoc.Location = new System.Drawing.Point(129, 164);
            this.txtbEihortLoc.Name = "txtbEihortLoc";
            this.txtbEihortLoc.Size = new System.Drawing.Size(299, 20);
            this.txtbEihortLoc.TabIndex = 52;
            this.txtbEihortLoc.TextChanged += new System.EventHandler(this.txtbEihortLoc_TextChanged);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(9, 164);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(96, 13);
            this.label5.TabIndex = 51;
            this.label5.Text = "eihort.exe location:";
            // 
            // btnBrowseMCsaved
            // 
            this.btnBrowseMCsaved.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnBrowseMCsaved.Location = new System.Drawing.Point(434, 106);
            this.btnBrowseMCsaved.Name = "btnBrowseMCsaved";
            this.btnBrowseMCsaved.Size = new System.Drawing.Size(75, 23);
            this.btnBrowseMCsaved.TabIndex = 50;
            this.btnBrowseMCsaved.Text = "Browse...";
            this.btnBrowseMCsaved.UseVisualStyleBackColor = true;
            this.btnBrowseMCsaved.Click += new System.EventHandler(this.btnBrowseMCsaved_Click);
            // 
            // txtbMCsaved
            // 
            this.txtbMCsaved.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtbMCsaved.Location = new System.Drawing.Point(129, 106);
            this.txtbMCsaved.Name = "txtbMCsaved";
            this.txtbMCsaved.Size = new System.Drawing.Size(299, 20);
            this.txtbMCsaved.TabIndex = 49;
            this.txtbMCsaved.TextChanged += new System.EventHandler(this.txtbMCsaved_TextChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(9, 106);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(115, 13);
            this.label4.TabIndex = 48;
            this.label4.Text = "Minecraft saved folder:";
            // 
            // btnBrowseMCMAPDZ
            // 
            this.btnBrowseMCMAPDZ.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnBrowseMCMAPDZ.Location = new System.Drawing.Point(434, 77);
            this.btnBrowseMCMAPDZ.Name = "btnBrowseMCMAPDZ";
            this.btnBrowseMCMAPDZ.Size = new System.Drawing.Size(75, 23);
            this.btnBrowseMCMAPDZ.TabIndex = 47;
            this.btnBrowseMCMAPDZ.Text = "Browse...";
            this.btnBrowseMCMAPDZ.UseVisualStyleBackColor = true;
            this.btnBrowseMCMAPDZ.Click += new System.EventHandler(this.btnBrowseMCMAPDZ_Click);
            // 
            // txtbMCMAPDZLoc
            // 
            this.txtbMCMAPDZLoc.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtbMCMAPDZLoc.Location = new System.Drawing.Point(164, 77);
            this.txtbMCMAPDZLoc.Name = "txtbMCMAPDZLoc";
            this.txtbMCMAPDZLoc.Size = new System.Drawing.Size(264, 20);
            this.txtbMCMAPDZLoc.TabIndex = 46;
            this.txtbMCMAPDZLoc.TextChanged += new System.EventHandler(this.txtbMCMAPDZLoc_TextChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(9, 77);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(128, 13);
            this.label3.TabIndex = 45;
            this.label3.Text = "mcmapDZ\\renders folder:";
            // 
            // btnBrowseMCServer
            // 
            this.btnBrowseMCServer.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnBrowseMCServer.Location = new System.Drawing.Point(434, 48);
            this.btnBrowseMCServer.Name = "btnBrowseMCServer";
            this.btnBrowseMCServer.Size = new System.Drawing.Size(75, 23);
            this.btnBrowseMCServer.TabIndex = 44;
            this.btnBrowseMCServer.Text = "Browse...";
            this.btnBrowseMCServer.UseVisualStyleBackColor = true;
            this.btnBrowseMCServer.Click += new System.EventHandler(this.btnBrowseMCServer_Click);
            // 
            // txtbMCServerLoc
            // 
            this.txtbMCServerLoc.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtbMCServerLoc.Location = new System.Drawing.Point(164, 48);
            this.txtbMCServerLoc.Name = "txtbMCServerLoc";
            this.txtbMCServerLoc.Size = new System.Drawing.Size(264, 20);
            this.txtbMCServerLoc.TabIndex = 43;
            this.txtbMCServerLoc.TextChanged += new System.EventHandler(this.txtbMCServerLoc_TextChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(9, 48);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(149, 13);
            this.label2.TabIndex = 42;
            this.label2.Text = "Minecraft_server.exe location:";
            // 
            // btnBrowseMCExe
            // 
            this.btnBrowseMCExe.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnBrowseMCExe.Location = new System.Drawing.Point(434, 19);
            this.btnBrowseMCExe.Name = "btnBrowseMCExe";
            this.btnBrowseMCExe.Size = new System.Drawing.Size(75, 23);
            this.btnBrowseMCExe.TabIndex = 41;
            this.btnBrowseMCExe.Text = "Browse...";
            this.btnBrowseMCExe.UseVisualStyleBackColor = true;
            this.btnBrowseMCExe.Click += new System.EventHandler(this.btnBrowseMCExe_Click);
            // 
            // txtbMCExeLoc
            // 
            this.txtbMCExeLoc.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtbMCExeLoc.Location = new System.Drawing.Point(129, 19);
            this.txtbMCExeLoc.Name = "txtbMCExeLoc";
            this.txtbMCExeLoc.Size = new System.Drawing.Size(299, 20);
            this.txtbMCExeLoc.TabIndex = 40;
            this.txtbMCExeLoc.TextChanged += new System.EventHandler(this.txtbMCExeLoc_TextChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(9, 19);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(114, 13);
            this.label1.TabIndex = 39;
            this.label1.Text = "Minecraft.exe location:";
            // 
            // bSave
            // 
            this.bSave.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.bSave.Enabled = false;
            this.bSave.Location = new System.Drawing.Point(361, 325);
            this.bSave.Name = "bSave";
            this.bSave.Size = new System.Drawing.Size(148, 34);
            this.bSave.TabIndex = 38;
            this.bSave.Text = "Save";
            this.bSave.UseVisualStyleBackColor = true;
            this.bSave.Click += new System.EventHandler(this.bSave_Click);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(15, 365);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(87, 13);
            this.label7.TabIndex = 37;
            this.label7.Text = "Application Path:";
            // 
            // tBoxAppPath
            // 
            this.tBoxAppPath.AcceptsReturn = true;
            this.tBoxAppPath.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tBoxAppPath.Location = new System.Drawing.Point(108, 365);
            this.tBoxAppPath.Name = "tBoxAppPath";
            this.tBoxAppPath.ReadOnly = true;
            this.tBoxAppPath.Size = new System.Drawing.Size(414, 20);
            this.tBoxAppPath.TabIndex = 36;
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.backupToolStripMenuItem,
            this.renameSelectedBackupFileToolStripMenuItem,
            this.restoreSelectedBackupFileToolStripMenuItem,
            this.toolStripSeparator4,
            this.viewWorldUsingEihortToolStripMenuItem,
            this.viewWorldUsingToolStripMenuItem,
            this.toolStripSeparator5,
            this.runMcmapDZToUpdateMapToolStripMenuItem,
            this.openMcmapDZGUIToolStripMenuItem,
            this.toolStripSeparator1,
            this.deleteSelectedWorldFromMinecraftToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(270, 198);
            // 
            // backupToolStripMenuItem
            // 
            this.backupToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("backupToolStripMenuItem.Image")));
            this.backupToolStripMenuItem.Name = "backupToolStripMenuItem";
            this.backupToolStripMenuItem.Size = new System.Drawing.Size(269, 22);
            this.backupToolStripMenuItem.Text = "Backup";
            this.backupToolStripMenuItem.Click += new System.EventHandler(this.backupToolStripMenuItem_Click);
            // 
            // renameSelectedBackupFileToolStripMenuItem
            // 
            this.renameSelectedBackupFileToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("renameSelectedBackupFileToolStripMenuItem.Image")));
            this.renameSelectedBackupFileToolStripMenuItem.Name = "renameSelectedBackupFileToolStripMenuItem";
            this.renameSelectedBackupFileToolStripMenuItem.Size = new System.Drawing.Size(269, 22);
            this.renameSelectedBackupFileToolStripMenuItem.Text = "Rename selected backup file";
            this.renameSelectedBackupFileToolStripMenuItem.Click += new System.EventHandler(this.renameSelectedBackupFileToolStripMenuItem_Click);
            // 
            // restoreSelectedBackupFileToolStripMenuItem
            // 
            this.restoreSelectedBackupFileToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("restoreSelectedBackupFileToolStripMenuItem.Image")));
            this.restoreSelectedBackupFileToolStripMenuItem.Name = "restoreSelectedBackupFileToolStripMenuItem";
            this.restoreSelectedBackupFileToolStripMenuItem.Size = new System.Drawing.Size(269, 22);
            this.restoreSelectedBackupFileToolStripMenuItem.Text = "Restore selected backup file";
            this.restoreSelectedBackupFileToolStripMenuItem.Click += new System.EventHandler(this.restoreSelectedBackupFileToolStripMenuItem_Click);
            // 
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            this.toolStripSeparator4.Size = new System.Drawing.Size(266, 6);
            // 
            // viewWorldUsingEihortToolStripMenuItem
            // 
            this.viewWorldUsingEihortToolStripMenuItem.Image = global::MCMyVault.Properties.Resources.eihort;
            this.viewWorldUsingEihortToolStripMenuItem.Name = "viewWorldUsingEihortToolStripMenuItem";
            this.viewWorldUsingEihortToolStripMenuItem.Size = new System.Drawing.Size(269, 22);
            this.viewWorldUsingEihortToolStripMenuItem.Text = "View world using eihort";
            this.viewWorldUsingEihortToolStripMenuItem.Click += new System.EventHandler(this.viewWorldUsingEihortToolStripMenuItem_Click);
            // 
            // viewWorldUsingToolStripMenuItem
            // 
            this.viewWorldUsingToolStripMenuItem.Image = global::MCMyVault.Properties.Resources.mcmapDZ;
            this.viewWorldUsingToolStripMenuItem.Name = "viewWorldUsingToolStripMenuItem";
            this.viewWorldUsingToolStripMenuItem.Size = new System.Drawing.Size(269, 22);
            this.viewWorldUsingToolStripMenuItem.Text = "View world using mcmapDZ";
            this.viewWorldUsingToolStripMenuItem.Click += new System.EventHandler(this.viewWorldUsingToolStripMenuItem_Click);
            // 
            // toolStripSeparator5
            // 
            this.toolStripSeparator5.Name = "toolStripSeparator5";
            this.toolStripSeparator5.Size = new System.Drawing.Size(266, 6);
            // 
            // runMcmapDZToUpdateMapToolStripMenuItem
            // 
            this.runMcmapDZToUpdateMapToolStripMenuItem.Image = global::MCMyVault.Properties.Resources.mcmapDZ;
            this.runMcmapDZToUpdateMapToolStripMenuItem.Name = "runMcmapDZToUpdateMapToolStripMenuItem";
            this.runMcmapDZToUpdateMapToolStripMenuItem.Size = new System.Drawing.Size(269, 22);
            this.runMcmapDZToUpdateMapToolStripMenuItem.Text = "Auto-run mcmapDZ to update map";
            this.runMcmapDZToUpdateMapToolStripMenuItem.Click += new System.EventHandler(this.runMcmapDZToUpdateMapToolStripMenuItem_Click);
            // 
            // openMcmapDZGUIToolStripMenuItem
            // 
            this.openMcmapDZGUIToolStripMenuItem.Image = global::MCMyVault.Properties.Resources.mcmapDZ;
            this.openMcmapDZGUIToolStripMenuItem.Name = "openMcmapDZGUIToolStripMenuItem";
            this.openMcmapDZGUIToolStripMenuItem.Size = new System.Drawing.Size(269, 22);
            this.openMcmapDZGUIToolStripMenuItem.Text = "Open mcmapDZ-GUI.exe";
            this.openMcmapDZGUIToolStripMenuItem.Click += new System.EventHandler(this.openMcmapDZGUIToolStripMenuItem_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(266, 6);
            // 
            // deleteSelectedWorldFromMinecraftToolStripMenuItem
            // 
            this.deleteSelectedWorldFromMinecraftToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("deleteSelectedWorldFromMinecraftToolStripMenuItem.Image")));
            this.deleteSelectedWorldFromMinecraftToolStripMenuItem.Name = "deleteSelectedWorldFromMinecraftToolStripMenuItem";
            this.deleteSelectedWorldFromMinecraftToolStripMenuItem.Size = new System.Drawing.Size(269, 22);
            this.deleteSelectedWorldFromMinecraftToolStripMenuItem.Text = "Delete selected world from Minecraft";
            this.deleteSelectedWorldFromMinecraftToolStripMenuItem.Click += new System.EventHandler(this.deleteSelectedWorldFromMinecraftToolStripMenuItem_Click);
            // 
            // contextMenuStrip2
            // 
            this.contextMenuStrip2.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.openSelectedBackupInEihortToolStripMenuItem,
            this.renameSelectedBackupFileToolStripMenuItem1,
            this.restoreSelectedBackupFileToolStripMenuItem1,
            this.purgekeepXDaysForYToolStripMenuItem,
            this.sendCopyToCloudStorageToolStripMenuItem,
            this.deleteToolStripMenuItem});
            this.contextMenuStrip2.Name = "contextMenuStrip2";
            this.contextMenuStrip2.Size = new System.Drawing.Size(241, 136);
            // 
            // openSelectedBackupInEihortToolStripMenuItem
            // 
            this.openSelectedBackupInEihortToolStripMenuItem.Image = global::MCMyVault.Properties.Resources.eihort;
            this.openSelectedBackupInEihortToolStripMenuItem.Name = "openSelectedBackupInEihortToolStripMenuItem";
            this.openSelectedBackupInEihortToolStripMenuItem.Size = new System.Drawing.Size(240, 22);
            this.openSelectedBackupInEihortToolStripMenuItem.Text = "View backup world using eihort";
            this.openSelectedBackupInEihortToolStripMenuItem.Click += new System.EventHandler(this.openSelectedBackupInEihortToolStripMenuItem_Click);
            // 
            // renameSelectedBackupFileToolStripMenuItem1
            // 
            this.renameSelectedBackupFileToolStripMenuItem1.Image = ((System.Drawing.Image)(resources.GetObject("renameSelectedBackupFileToolStripMenuItem1.Image")));
            this.renameSelectedBackupFileToolStripMenuItem1.Name = "renameSelectedBackupFileToolStripMenuItem1";
            this.renameSelectedBackupFileToolStripMenuItem1.Size = new System.Drawing.Size(240, 22);
            this.renameSelectedBackupFileToolStripMenuItem1.Text = "Rename selected backup file";
            this.renameSelectedBackupFileToolStripMenuItem1.Click += new System.EventHandler(this.renameSelectedBackupFileToolStripMenuItem1_Click);
            // 
            // restoreSelectedBackupFileToolStripMenuItem1
            // 
            this.restoreSelectedBackupFileToolStripMenuItem1.Image = ((System.Drawing.Image)(resources.GetObject("restoreSelectedBackupFileToolStripMenuItem1.Image")));
            this.restoreSelectedBackupFileToolStripMenuItem1.Name = "restoreSelectedBackupFileToolStripMenuItem1";
            this.restoreSelectedBackupFileToolStripMenuItem1.Size = new System.Drawing.Size(240, 22);
            this.restoreSelectedBackupFileToolStripMenuItem1.Text = "Restore selected backup file";
            this.restoreSelectedBackupFileToolStripMenuItem1.Click += new System.EventHandler(this.restoreSelectedBackupFileToolStripMenuItem1_Click);
            // 
            // purgekeepXDaysForYToolStripMenuItem
            // 
            this.purgekeepXDaysForYToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("purgekeepXDaysForYToolStripMenuItem.Image")));
            this.purgekeepXDaysForYToolStripMenuItem.Name = "purgekeepXDaysForYToolStripMenuItem";
            this.purgekeepXDaysForYToolStripMenuItem.Size = new System.Drawing.Size(240, 22);
            this.purgekeepXDaysForYToolStripMenuItem.Text = "Cleanup last X days for Y ";
            this.purgekeepXDaysForYToolStripMenuItem.Click += new System.EventHandler(this.purgekeepXDaysForYToolStripMenuItem_Click);
            // 
            // sendCopyToCloudStorageToolStripMenuItem
            // 
            this.sendCopyToCloudStorageToolStripMenuItem.Image = global::MCMyVault.Properties.Resources.cloud_hd;
            this.sendCopyToCloudStorageToolStripMenuItem.Name = "sendCopyToCloudStorageToolStripMenuItem";
            this.sendCopyToCloudStorageToolStripMenuItem.Size = new System.Drawing.Size(240, 22);
            this.sendCopyToCloudStorageToolStripMenuItem.Text = "Send copy to cloud storage";
            this.sendCopyToCloudStorageToolStripMenuItem.Click += new System.EventHandler(this.sendCopyToCloudStorageToolStripMenuItem_Click);
            // 
            // deleteToolStripMenuItem
            // 
            this.deleteToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("deleteToolStripMenuItem.Image")));
            this.deleteToolStripMenuItem.Name = "deleteToolStripMenuItem";
            this.deleteToolStripMenuItem.Size = new System.Drawing.Size(240, 22);
            this.deleteToolStripMenuItem.Text = "Delete";
            this.deleteToolStripMenuItem.Click += new System.EventHandler(this.deleteToolStripMenuItem_Click);
            // 
            // btnMinecraft
            // 
            this.btnMinecraft.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btnMinecraft.BackColor = System.Drawing.Color.LightGreen;
            this.btnMinecraft.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnMinecraft.Location = new System.Drawing.Point(4, 27);
            this.btnMinecraft.Name = "btnMinecraft";
            this.btnMinecraft.Size = new System.Drawing.Size(532, 23);
            this.btnMinecraft.TabIndex = 6;
            this.btnMinecraft.Text = "Play Minecraft.exe!";
            this.btnMinecraft.UseVisualStyleBackColor = false;
            this.btnMinecraft.Click += new System.EventHandler(this.btnMinecraft_Click);
            // 
            // contextMenuStripTP
            // 
            this.contextMenuStripTP.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.backupToolStripMenuItem1,
            this.restoreToolStripMenuItem,
            this.deleteThisTexturepackToolStripMenuItem});
            this.contextMenuStripTP.Name = "contextMenuStripTP";
            this.contextMenuStripTP.Size = new System.Drawing.Size(114, 70);
            // 
            // backupToolStripMenuItem1
            // 
            this.backupToolStripMenuItem1.Image = ((System.Drawing.Image)(resources.GetObject("backupToolStripMenuItem1.Image")));
            this.backupToolStripMenuItem1.Name = "backupToolStripMenuItem1";
            this.backupToolStripMenuItem1.Size = new System.Drawing.Size(113, 22);
            this.backupToolStripMenuItem1.Text = "Backup";
            this.backupToolStripMenuItem1.Click += new System.EventHandler(this.backupToolStripMenuItem1_Click);
            // 
            // restoreToolStripMenuItem
            // 
            this.restoreToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("restoreToolStripMenuItem.Image")));
            this.restoreToolStripMenuItem.Name = "restoreToolStripMenuItem";
            this.restoreToolStripMenuItem.Size = new System.Drawing.Size(113, 22);
            this.restoreToolStripMenuItem.Text = "Restore";
            this.restoreToolStripMenuItem.Click += new System.EventHandler(this.restoreToolStripMenuItem_Click);
            // 
            // deleteThisTexturepackToolStripMenuItem
            // 
            this.deleteThisTexturepackToolStripMenuItem.Image = global::MCMyVault.Properties.Resources.delete_16x;
            this.deleteThisTexturepackToolStripMenuItem.Name = "deleteThisTexturepackToolStripMenuItem";
            this.deleteThisTexturepackToolStripMenuItem.Size = new System.Drawing.Size(113, 22);
            this.deleteThisTexturepackToolStripMenuItem.Text = "Delete";
            this.deleteThisTexturepackToolStripMenuItem.Click += new System.EventHandler(this.deleteThisTexturepackToolStripMenuItem_Click);
            // 
            // contextMenuStrip3
            // 
            this.contextMenuStrip3.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuRestoreSnap,
            this.toolStripMenuRenamesnap,
            this.toolStripMenuDeleteSnap});
            this.contextMenuStrip3.Name = "contextMenuStrip3";
            this.contextMenuStrip3.Size = new System.Drawing.Size(234, 70);
            // 
            // toolStripMenuRestoreSnap
            // 
            this.toolStripMenuRestoreSnap.Image = ((System.Drawing.Image)(resources.GetObject("toolStripMenuRestoreSnap.Image")));
            this.toolStripMenuRestoreSnap.Name = "toolStripMenuRestoreSnap";
            this.toolStripMenuRestoreSnap.Size = new System.Drawing.Size(233, 22);
            this.toolStripMenuRestoreSnap.Text = "Restore selected snapshot";
            this.toolStripMenuRestoreSnap.Click += new System.EventHandler(this.toolStripMenuRestoreSnap_Click);
            // 
            // toolStripMenuRenamesnap
            // 
            this.toolStripMenuRenamesnap.Image = ((System.Drawing.Image)(resources.GetObject("toolStripMenuRenamesnap.Image")));
            this.toolStripMenuRenamesnap.Name = "toolStripMenuRenamesnap";
            this.toolStripMenuRenamesnap.Size = new System.Drawing.Size(233, 22);
            this.toolStripMenuRenamesnap.Text = "Rename selected snapshot file";
            this.toolStripMenuRenamesnap.Click += new System.EventHandler(this.toolStripMenuRenamesnap_Click);
            // 
            // toolStripMenuDeleteSnap
            // 
            this.toolStripMenuDeleteSnap.Image = global::MCMyVault.Properties.Resources.delete_16x;
            this.toolStripMenuDeleteSnap.Name = "toolStripMenuDeleteSnap";
            this.toolStripMenuDeleteSnap.Size = new System.Drawing.Size(233, 22);
            this.toolStripMenuDeleteSnap.Text = "Delete";
            this.toolStripMenuDeleteSnap.Click += new System.EventHandler(this.toolStripMenuDeleteSnap_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.ClientSize = new System.Drawing.Size(548, 485);
            this.Controls.Add(this.btnMinecraft);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.menuStrip1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.Load += new System.EventHandler(this.Form1_Load);
            this.SizeChanged += new System.EventHandler(this.Form1_SizeChanged);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.tabControl1.ResumeLayout(false);
            this.tabMain.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.tabBackups.ResumeLayout(false);
            this.tabSnapshots.ResumeLayout(false);
            this.groupBox4.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.tabTexturepacks.ResumeLayout(false);
            this.groupBoxCurrentView.ResumeLayout(false);
            this.groupBoxCurrentView.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picBoxItems)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picBoxWorldPreview)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picBoxlogo)).EndInit();
            this.tabMyApps.ResumeLayout(false);
            this.tabSettings.ResumeLayout(false);
            this.tabSettings.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).EndInit();
            this.contextMenuStrip1.ResumeLayout(false);
            this.contextMenuStrip2.ResumeLayout(false);
            this.contextMenuStripTP.ResumeLayout(false);
            this.contextMenuStrip3.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem helpToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem aboutToolStripMenuItem;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabMain;
        private System.Windows.Forms.TabPage tabSettings;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox tBoxAppPath;
        private System.Windows.Forms.Button bSave;
        private System.Windows.Forms.ToolStripMenuItem viewToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem logFileToolStripMenuItem;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button btnBackup;
        private System.Windows.Forms.ToolStripMenuItem configiniFileToolStripMenuItem;
        private System.Windows.Forms.TextBox txtbMCExeLoc;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnBrowseMCExe;
        private System.Windows.Forms.Button btnBrowseMCMAPDZ;
        private System.Windows.Forms.TextBox txtbMCMAPDZLoc;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button btnBrowseMCServer;
        private System.Windows.Forms.TextBox txtbMCServerLoc;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnBrowseMCsaved;
        private System.Windows.Forms.TextBox txtbMCsaved;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button btnBrowseEihortLoc;
        private System.Windows.Forms.TextBox txtbEihortLoc;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem viewWorldUsingEihortToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem viewWorldUsingToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem openMcmapDZGUIToolStripMenuItem;
        private System.Windows.Forms.Button btnBrowseBackupsLoc;
        private System.Windows.Forms.TextBox txtbBackupsLoc;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.ToolStripMenuItem openBackupLocationToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripMenuItem backupToolStripMenuItem;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.ToolStripMenuItem backupAllToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.Button btnBackupAll;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
        private System.Windows.Forms.ToolStripMenuItem renameSelectedBackupFileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem viewWorldUsingEihortToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem viewWorldUsingMcmapDZToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem launchMinecraftexeToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator5;
        private System.Windows.Forms.ToolStripMenuItem deleteSelectedWorldFromMinecraftToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exploreMinecraftSavedLocationToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem restoreSelectedBackupFileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem runMcmapDZToUpdateMapToolStripMenuItem;
        private System.Windows.Forms.TabPage tabBackups;
        private System.Windows.Forms.ListBox listBoxBackups;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip2;
        private System.Windows.Forms.ToolStripMenuItem deleteToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem renameSelectedBackupFileToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem restoreSelectedBackupFileToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem openSelectedBackupInEihortToolStripMenuItem;
        private System.Windows.Forms.TabPage tabMyApps;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button btnMinecraft;
        private System.Windows.Forms.ToolStripMenuItem supportSiteToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator6;
        private System.Windows.Forms.ToolStripMenuItem wikiAndVideosToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem restartMCMyVaultToolStripMenuItem;
        private System.Windows.Forms.Button btnBrowseTPBackupsLoc;
        private System.Windows.Forms.TextBox txtTexturepackbackups;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.TextBox txtbMCtexturepacks;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TabPage tabTexturepacks;
        private System.Windows.Forms.ToolStripMenuItem exploreMinecraftTexturepacksLocationsToolStripMenuItem;
        private System.Windows.Forms.PictureBox picBoxlogo;
        private System.Windows.Forms.PictureBox picBoxWorldPreview;
        private System.Windows.Forms.ListBox lboxCurrentTP;
        private System.Windows.Forms.PictureBox picBoxItems;
        private System.Windows.Forms.RadioButton radioButtonInactiveTP;
        private System.Windows.Forms.RadioButton radioButtonCurrentTP;
        private System.Windows.Forms.GroupBox groupBoxCurrentView;
        private System.Windows.Forms.ContextMenuStrip contextMenuStripTP;
        private System.Windows.Forms.ToolStripMenuItem deleteThisTexturepackToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem backupToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem restoreToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator7;
        private System.Windows.Forms.ToolStripMenuItem exploreBackuptexturpacksLocationToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem purgekeepXDaysForYToolStripMenuItem;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.NumericUpDown numericUpDown1;
        private System.Windows.Forms.ToolStripMenuItem sendCopyToCloudStorageToolStripMenuItem;
        private System.Windows.Forms.Button btnBrowseCloudBackupLoc;
        private System.Windows.Forms.TextBox txtBoxCloudBackup;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.DataGridViewImageColumn ColumnMap;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnName;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnLastBackup;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnFilename;
        private System.Windows.Forms.ToolStripMenuItem checkForUpdateToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem readMeFileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exploreToolStripMenuItem;
        private System.Windows.Forms.TabPage tabSnapshots;
        private System.Windows.Forms.ListBox listBoxSnapshots;
        private System.Windows.Forms.ToolStripMenuItem exploreMinecraftModFolderToolStripMenuItem;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Button btnCreateSnapshot;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip3;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuRestoreSnap;
        private System.Windows.Forms.Button btnBrowse2SnapshotsLoc;
        private System.Windows.Forms.TextBox txtBoxSnapshotsLoc;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuRenamesnap;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuDeleteSnap;

    }
}

