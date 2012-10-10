// MCMyVault v1.4.0.0
// by Joe Socoloski
// Copyright 2012. All Rights Reserved
// To Do: 
//      - 
//      - 
// Limits: 
/////////////////////////////////////////////////////////
//LICENSE 
//BY DOWNLOADING AND USING, YOU AGREE TO THE FOLLOWING TERMS: 
//If it is your intent to use this software for non-commercial purposes,  
//such as in academic research, this software is free and is covered under  
//the GNU GPL License, given here: <http://www.gnu.org/licenses/gpl.txt>  
//You agree with 3RDPARTY's Terms Of Service 
//given here: <http://3RDPARTY.com> 
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Threading;
using Ionic.Zip; //http://dotnetzip.codeplex.com/wikipage?title=CS-Examples
using System.Diagnostics;
using Microsoft.VisualBasic.FileIO;

namespace MCMyVault
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        IniFile Ini = new IniFile();
        ImageList ImageListWorlds = new ImageList();

        private void Form1_Load(object sender, EventArgs e)
        {
            //Update the Title bar text
            this.Text = Application.ProductName + " " + Application.ProductVersion;

            //ToolTips
            toolTip1.SetToolTip(this.listView1, "Right-Click for menu. Double-click to open map.");
            toolTip1.SetToolTip(this.listBoxBackups, "Right-Click for menu.");
            //toolTip1.SetToolTip(openSelectedBackupInEihortToolStripMenuItem, "Explore this world with eihirt viewer before restoring.");

            ReadSettings();
            CreateLogFile();

            InitIniSettings();//load settings into GUI
            bSave.Enabled = false;

            //now that settings are set, try and display the listview
            LoadSavedWorlds();

            MyAppsSetup();
        }

        #region Settings Read and Save
        private void ReadSettings()
        {
            //Read the Config file and display in the textboxes
            if (Properties.Settings.Default.AppPath == "")
            {
                tBoxAppPath.Text = Application.StartupPath;
            }
            else
                tBoxAppPath.Text = Properties.Settings.Default.AppPath;

            //INI
            if (File.Exists(Common.ConfigIniFile))
                Ini.Load(Common.ConfigIniFile);
            else
            {
                //File.Create(Common.ConfigIniFile);
                Ini.AddSection("settings").AddKey("minecraft").SetValue("");
                if(Directory.Exists(Common.mcmapDZ_renders))
                    Ini.AddSection("settings").AddKey("mcmapDZ_renders").SetValue(Common.mcmapDZ_renders);
                else
                    Ini.AddSection("settings").AddKey("mcmapDZ_renders").SetValue("");
                Ini.AddSection("settings").AddKey("minecraft_server").SetValue("");
                Ini.AddSection("settings").AddKey("minecraft_saved").SetValue("");
                Ini.AddSection("settings").AddKey("eihort").SetValue("");
                Ini.AddSection("settings").AddKey("backup_loc").SetValue(Common.MinecraftToolsBackupsFolder);

                Ini.AddSection("myapps");
                Ini.Save(Common.ConfigIniFile);
            }

        }

        /// <summary>
        /// Load the settings from INI and into GUI
        /// </summary>
        private void InitIniSettings()
        {
            if (Ini.HasSection("settings"))
            {
                //GET Minecraft.exe//GET Minecraft.exe//GET Minecraft.exe//GET Minecraft.exe//GET Minecraft.exe
                if (String.IsNullOrEmpty(Ini.GetKeyValue("settings", "minecraft")))
                {
                    //Minecraft.exe doesn't have a default installation folder, so we must prompt the user
                    //NO value found in ini, ask user where Minecraft.exe is
                    MessageBox.Show("No path to Minecraft.exe found in config.ini. Please navigate to its path...", "No path to Minecraft.exe found", MessageBoxButtons.OK, MessageBoxIcon.Question);
                    String minecraft_path = "";
                    if (Directory.Exists(Environment.GetFolderPath(Environment.SpecialFolder.ProgramFilesX86) + "\\Minecraft"))
                        minecraft_path = BrowseTo("exe", Environment.GetFolderPath(Environment.SpecialFolder.ProgramFilesX86) + "\\Minecraft");
                    else
                        minecraft_path = BrowseTo("exe", Environment.GetFolderPath(Environment.SpecialFolder.ProgramFilesX86));
                    txtbMCExeLoc.Text = minecraft_path;
                    Ini.SetKeyValue("settings", "minecraft", minecraft_path);
                    Ini.Save(Common.ConfigIniFile);//save right away!!
                }
                else
                {
                    //has value
                    txtbMCExeLoc.Text = Ini.GetKeyValue("settings", "minecraft");
                }
                //GET Minecraft_Server.exe//GET Minecraft_Server.exe//GET Minecraft_Server.exe//GET Minecraft_Server.exe
                if (String.IsNullOrEmpty(Ini.GetKeyValue("settings", "minecraft_server")))
                {
                    //Minecraft.exe doesn't have a default installation folder, so we must prompt the user
                    //NO value found in ini, ask user where Minecraft.exe is
                    MessageBox.Show("No path to Minecraft_Server.exe found in config.ini. Please navigate to its path...", "No path to Minecraft_Server.exe found", MessageBoxButtons.OK, MessageBoxIcon.Question);
                    String minecraftsrvr_path = "";
                    if (Directory.Exists(Environment.GetFolderPath(Environment.SpecialFolder.ProgramFilesX86) + "\\Minecraft"))
                        minecraftsrvr_path = BrowseTo("exe", Environment.GetFolderPath(Environment.SpecialFolder.ProgramFilesX86) + "\\Minecraft");
                    else
                        minecraftsrvr_path = BrowseTo("exe", Environment.GetFolderPath(Environment.SpecialFolder.ProgramFilesX86));
                    txtbMCServerLoc.Text = minecraftsrvr_path;
                    Ini.SetKeyValue("settings", "minecraft_server", minecraftsrvr_path);
                    Ini.Save(Common.ConfigIniFile);//save right away!!
                }
                else
                {
                    //has value
                    txtbMCServerLoc.Text = Ini.GetKeyValue("settings", "minecraft_server");
                }
                //GET mcmapDZ_renders//GET mcmapDZ_renders//GET mcmapDZ_renders//GET mcmapDZ_renders//GET mcmapDZ_renders
                if (String.IsNullOrEmpty(Ini.GetKeyValue("settings", "mcmapDZ_renders")))
                {
                    //Minecraft.exe doesn't have a default installation folder, so we must prompt the user
                    //NO value found in ini, ask user where Minecraft.exe is
                    MessageBox.Show("No path to mcmapDZ\\renders found in config.ini. Please navigate to its path...", "No path to mcmapDZ\renders found", MessageBoxButtons.OK, MessageBoxIcon.Question);
                    String mcmapDZ_path = "";
                    if (Directory.Exists(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\MCMyVault"))
                    {
                        mcmapDZ_path = BrowseToFolder(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\MCMyVault");
                    }
                    else
                        mcmapDZ_path = BrowseToFolder(Environment.SpecialFolder.MyDocuments);
                    txtbMCMAPDZLoc.Text = mcmapDZ_path;
                    Ini.SetKeyValue("settings", "mcmapDZ_renders", mcmapDZ_path);
                    Ini.Save(Common.ConfigIniFile);//save right away!!
                }
                else
                {
                    //has value
                    txtbMCMAPDZLoc.Text = Ini.GetKeyValue("settings", "mcmapDZ_renders");
                }
                //GET minecraft_saved//GET minecraft_saved//GET minecraft_saved//GET minecraft_saved//GET minecraft_saved
                if (String.IsNullOrEmpty(Ini.GetKeyValue("settings", "minecraft_saved")))
                {
                    if (IsDefaultFolder())
                    {
                        //there is a default folder
                        txtbMCsaved.Text = Common.MinecraftDefaultFolderWin;
                        Ini.SetKeyValue("settings", "minecraft_saved", Common.MinecraftDefaultFolderWin);
                        Ini.Save(Common.ConfigIniFile);//save right away!!
                    }
                    else
                    {
                        //Minecraft.exe doesn't have a default installation folder, so we must prompt the user
                        //NO value found in ini, ask user where Minecraft.exe is
                        MessageBox.Show("No path to C:\\Users\\[USER]\\AppData\\Roaming\\.minecraft\\saves found on harddrive. Please navigate to its path...", "No path to .minecraft\\saved found", MessageBoxButtons.OK, MessageBoxIcon.Question);
                        String mcsaved_path = BrowseToFolder(Environment.SpecialFolder.ApplicationData);
                        txtbMCsaved.Text = mcsaved_path;
                        Ini.SetKeyValue("settings", "minecraft_saved", mcsaved_path);
                    }
                }
                else
                {
                    //has value
                    txtbMCsaved.Text = Ini.GetKeyValue("settings", "minecraft_saved");
                }
                //GET eihort.exe//GET eihort.exe//GET eihort.exe//GET eihort.exe//GET eihort.exe
                if (String.IsNullOrEmpty(Ini.GetKeyValue("settings", "eihort")))
                {
                    //eihort.exe doesn't have a default installation folder, so we must prompt the user
                    //NO value found in ini, ask user where eihort.exe is
                    MessageBox.Show("No path to eihort.exe found in config.ini. Please navigate to its path...", "No path to eihort.exe found", MessageBoxButtons.OK, MessageBoxIcon.Question);
                    String eihort_path = "";
                    if (Directory.Exists(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\MCMyVault"))
                    {
                        eihort_path = BrowseTo("exe", Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\MCMyVault");
                    }
                    else
                        eihort_path = BrowseTo("exe", Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments));
                    txtbEihortLoc.Text = eihort_path;
                    Ini.SetKeyValue("settings", "eihort", eihort_path);
                    Ini.Save(Common.ConfigIniFile);//save right away!!
                }
                else
                {
                    //has value
                    txtbEihortLoc.Text = Ini.GetKeyValue("settings", "eihort");
                }
                //GET backup_loc//GET backup_loc//GET backup_loc//GET backup_loc//GET backup_loc
                if (String.IsNullOrEmpty(Ini.GetKeyValue("settings", "backup_loc")))
                {
                    //MessageBox.Show(, MessageBoxButtons.OK, MessageBoxIcon.Question);

                    DialogResult dResult = MessageBox.Show("No path to backup folder yet.\r\nDo you want to use the default location?", "No path to a backup folder found", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Information);
                    if (dResult == DialogResult.Yes)
                    {
                        //Create Backups Folder if not there
                        if (!Directory.Exists(Common.MinecraftToolsBackupsFolder))
                        {
                            Directory.CreateDirectory(Common.MinecraftToolsBackupsFolder);
                        }

                        txtbBackupsLoc.Text = Common.MinecraftToolsBackupsFolder;
                        Ini.SetKeyValue("settings", "backup_loc", Common.MinecraftToolsBackupsFolder);
                        Ini.Save(Common.ConfigIniFile);//save right away!!
                    }
                    else if (dResult == DialogResult.No)
                    {
                        String backups_path = "";
                        if (Directory.Exists(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\MCMyVault"))
                        {
                            backups_path = BrowseToFolder(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\MCMyVault");
                        }
                        else
                            backups_path = BrowseToFolder(Environment.SpecialFolder.MyDocuments);
                        txtbBackupsLoc.Text = backups_path;
                        Ini.SetKeyValue("settings", "backup_loc", backups_path);
                        Ini.Save(Common.ConfigIniFile);//save right away!!
                    }
                    else if (dResult == DialogResult.Cancel)
                    {

                    }
                }
                else
                {
                    //has value
                    txtbBackupsLoc.Text = Ini.GetKeyValue("settings", "backup_loc");
                }
            }
        }

        private void bSave_Click(object sender, EventArgs e)
        {
            try
            {
                Properties.Settings.Default.AppPath = tBoxAppPath.Text;
                Properties.Settings.Default.Save();

                //read the controls and save the selections
                Ini.SetKeyValue("settings", "minecraft", txtbMCExeLoc.Text.Trim());
                Ini.SetKeyValue("settings", "minecraft_server", txtbMCServerLoc.Text.Trim());
                Ini.SetKeyValue("settings", "mcmapDZ_renders", txtbMCMAPDZLoc.Text.Trim());
                Ini.SetKeyValue("settings", "minecraft_saved", txtbMCsaved.Text.Trim());
                Ini.Save(Common.ConfigIniFile);
                bSave.Enabled = false;
            }
            catch (Exception ex)
            {
                Log(ex.Message);
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private String BrowseTo(string filetype, string default_path)
        {
            String path = "";

            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = filetype + " files (*." + filetype + ")|*." + filetype + "|All files (*.*)|*.*";
            ofd.InitialDirectory = default_path;//System.Windows.Forms.Application.StartupPath;
            ofd.ShowDialog();
            try
            {
                //Make sure user didn't cancel
                if (ofd.FileName != string.Empty)
                {
                    path = ofd.FileName;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("ERROR: " + ex.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            return path;
        }

        private String BrowseToFolder(string default_path)
        {
            String path = "";
            try
            {
                FolderBrowserDialog folderBrowserDialog1 = new FolderBrowserDialog();
                folderBrowserDialog1.SelectedPath = default_path;
                // Show the FolderBrowserDialog.
                DialogResult result = folderBrowserDialog1.ShowDialog();
                if (result == DialogResult.OK)
                {
                    path = folderBrowserDialog1.SelectedPath;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("ERROR: " + ex.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            return path;
        }

        private String BrowseToFolder(Environment.SpecialFolder default_path)
        {
            String path = "";
            try
            {
                FolderBrowserDialog folderBrowserDialog1 = new FolderBrowserDialog();
                folderBrowserDialog1.RootFolder = default_path;
                // Show the FolderBrowserDialog.
                DialogResult result = folderBrowserDialog1.ShowDialog();
                if (result == DialogResult.OK)
                {
                    path = folderBrowserDialog1.SelectedPath;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("ERROR: " + ex.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            return path;
        }

        private void btnBrowseMCExe_Click(object sender, EventArgs e)
        {
            String minecraft_path = "";
            if (String.IsNullOrEmpty(txtbMCExeLoc.Text))
                minecraft_path = BrowseTo("exe", Environment.GetFolderPath(Environment.SpecialFolder.ProgramFilesX86));
            else
            {
                FileInfo mcexe = new FileInfo(txtbMCExeLoc.Text);
                minecraft_path = BrowseTo("exe", mcexe.DirectoryName);
            }
            if (!String.IsNullOrEmpty(minecraft_path))
            {
                txtbMCExeLoc.Text = minecraft_path;
                Ini.SetKeyValue("settings", "minecraft", minecraft_path);
            }
        }

        private void btnBrowseMCServer_Click(object sender, EventArgs e)
        {
            String minecraftsrvr_path = "";
            if (String.IsNullOrEmpty(txtbMCServerLoc.Text))
                minecraftsrvr_path = BrowseTo("exe", Environment.GetFolderPath(Environment.SpecialFolder.ProgramFilesX86));
            else
            {
                FileInfo mcexe = new FileInfo(txtbMCServerLoc.Text);
                minecraftsrvr_path = BrowseTo("exe", mcexe.DirectoryName);
            }
            if (!String.IsNullOrEmpty(minecraftsrvr_path))
            {
                txtbMCServerLoc.Text = minecraftsrvr_path;
                Ini.SetKeyValue("settings", "minecraft_server", minecraftsrvr_path);
            }
        }

        private void btnBrowseMCMAPDZ_Click(object sender, EventArgs e)
        {
            String mcmapDZ_path = "";
            if (String.IsNullOrEmpty(txtbMCMAPDZLoc.Text))
                mcmapDZ_path = BrowseToFolder(Environment.SpecialFolder.MyDocuments);
            else
            {
                mcmapDZ_path = BrowseTo("exe", txtbMCMAPDZLoc.Text);
            }
            if (!String.IsNullOrEmpty(mcmapDZ_path))
            {
                txtbMCMAPDZLoc.Text = mcmapDZ_path;
                Ini.SetKeyValue("settings", "mcmapDZ_renders", mcmapDZ_path);
            }
        }

        private void btnBrowseMCsaved_Click(object sender, EventArgs e)
        {
            String MCsaved_path = BrowseToFolder(Environment.SpecialFolder.ApplicationData);
            if (!String.IsNullOrEmpty(MCsaved_path))
            {
                txtbMCsaved.Text = MCsaved_path;
                Ini.SetKeyValue("settings", "minecraft_saved", MCsaved_path);
            }
        }

        private void btnBrowseEihortLoc_Click(object sender, EventArgs e)
        {
            //txtbEihortLoc
            String eihort_path = "";
            if (String.IsNullOrEmpty(txtbEihortLoc.Text))
                eihort_path = BrowseTo("exe", Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments));
            else
            {
                FileInfo eihortexe = new FileInfo(txtbEihortLoc.Text);
                eihort_path = BrowseTo("exe", eihortexe.DirectoryName);
            }
            if (!String.IsNullOrEmpty(eihort_path))
            {
                txtbEihortLoc.Text = eihort_path;
                Ini.SetKeyValue("settings", "eihort", eihort_path);
            }
        }

        private void btnBrowseBackupsLoc_Click(object sender, EventArgs e)
        {
            String backups_path = BrowseToFolder(Environment.SpecialFolder.MyDocuments);
            txtbBackupsLoc.Text = backups_path;
            Ini.SetKeyValue("settings", "backup_loc", backups_path);
            Ini.Save(Common.ConfigIniFile);//save right away!!
        }
        #endregion Settings Read and Save

        #region MenuStrip Items
        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void logFileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                System.Diagnostics.Process.Start(Common.gLogFile);
            }
            catch (Exception ex)
            {
                Log(ex.Message);
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AboutBox1 aboutBox = new AboutBox1();
            aboutBox.Show();
        }

        private void configiniFileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                System.Diagnostics.Process.Start(Common.ConfigIniFile);
            }
            catch (Exception ex)
            {
                Log(ex.Message);
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void openBackupLocationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                System.Diagnostics.Process.Start(Ini.GetKeyValue("settings", "backup_loc"));
            }
            catch (Exception ex)
            {
                Log(ex.Message);
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void backupToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                DoBackup();
            }
            catch (Exception ex)
            {
                Log("ERROR: " + ex.Message);
                MessageBox.Show("ERROR: " + ex.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void backupAllToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                DoBackupForAll();
            }
            catch (Exception ex)
            {
                Log("ERROR: " + ex.Message);
                MessageBox.Show("ERROR: " + ex.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void exploreMinecraftSavedLocationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                System.Diagnostics.Process.Start(Ini.GetKeyValue("settings", "minecraft_saved"));
            }
            catch (Exception ex)
            {
                Log(ex.Message);
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void renameSelectedBackupFileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                DoRenameOfBackup();
            }
            catch (Exception ex)
            {
                Log("ERROR renameSelectedBackupFileToolStripMenuItem_Click(): " + ex.Message);
                MessageBox.Show("ERROR: " + ex.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void launchMinecraftexeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                System.Diagnostics.Process.Start(Ini.GetSection("settings").GetKey("minecraft").GetValue());
            }
            catch (Exception ex)
            {
                Log(ex.Message);
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void deleteSelectedWorldFromMinecraftToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                try
                {
                    if (listView1.SelectedItems.Count == 1)
                    {
                        DialogResult dResult = MessageBox.Show("Are you sure you want to DELETE WORLD " + listView1.SelectedItems[0].Text + "\r\n from Minecraft?\r\n(Make sure you have backed it up first)", "DELETE WORLD " + listView1.SelectedItems[0].Text, MessageBoxButtons.YesNoCancel, MessageBoxIcon.Warning);
                        if (dResult == DialogResult.Yes)
                        {
                            DirectoryInfo worldDir = new DirectoryInfo(Ini.GetSection("settings").GetKey("minecraft_saved").GetValue() + "\\" + listView1.SelectedItems[0].Text);
                            if (Directory.Exists(worldDir.FullName))
                            {
                                Directory.Delete(worldDir.FullName, true);
                                ShowMsgBoxGenericCompleted(listView1.SelectedItems[0].Text + " deleted");
                                LoadSavedWorlds();
                            }
                            else
                            {
                                Log("deleteSelectedWorldFromMinecraftToolStripMenuItem_Click: could not find " + worldDir.FullName);
                                MessageBox.Show("Sorry, could not find " + worldDir.FullName);
                            }
                        }
                        else if (dResult == DialogResult.No)
                        {

                        }
                        else if (dResult == DialogResult.Cancel)
                        {

                        }
                    }
                    else
                        MessageBox.Show("Sorry, only select one world to delete at a time.");
                }
                catch (Exception ex)
                {
                    Log(ex.Message);
                    MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                Log(ex.Message);
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void restoreSelectedBackupFileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                try
                {
                    DoRestore();
                }
                catch (Exception ex)
                {
                    Log(ex.Message);
                    MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                Log(ex.Message);
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void runMcmapDZToUpdateMapToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {

                string mcmapDZ_exe = Ini.GetKeyValue("settings", "mcmapDZ_renders").Replace("renders", "mcmapDZ.exe");
                string worldFullname = Ini.GetKeyValue("settings", "minecraft_saved") + "\\" + listView1.SelectedItems[0].Text;
                // Use ProcessStartInfo class
                ProcessStartInfo startInfo = new ProcessStartInfo();
                startInfo.CreateNoWindow = false;
                startInfo.UseShellExecute = false;
                startInfo.FileName = mcmapDZ_exe;
                //startInfo.WindowStyle = ProcessWindowStyle.Hidden;
                startInfo.Arguments = "-day -night -cave -skylight -extension jpg -quality 1 -orientation E \"" + worldFullname + "\"";
                Log("Auto-mcmapDZ executing: mcmapDZ " + startInfo.Arguments);
                // Start the process with the info we specified.
                // Call WaitForExit and then the using statement will close.
                using (Process exeProcess = Process.Start(startInfo))
                {
                    exeProcess.WaitForExit();
                }
                //Refresh
                LoadSavedWorlds();
            }
            catch (Exception ex)
            {
                MessageBox.Show("ERROR: " + ex.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                try
                {
                    DialogResult dResult = MessageBox.Show("Are you sure you want to DELETE?", "DELETE BACKUP ", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Warning);
                    if (dResult == DialogResult.Yes)
                    {
                        DirectoryInfo backupDir = new DirectoryInfo(Ini.GetSection("settings").GetKey("backup_loc").GetValue());
                        if (Directory.Exists(backupDir.FullName))
                        {
                            List<FileInfo> FilesList = new List<FileInfo>();
                            foreach (FileInfo item in listBoxBackups.SelectedItems)
                            {
                                FilesList.Add(item);
                            }

                            foreach (FileInfo f in FilesList)
                            {
                                //File.Delete(f.FullName);
                                FileSystem.DeleteFile(f.FullName, UIOption.OnlyErrorDialogs, RecycleOption.SendToRecycleBin);
                                listBoxBackups.Items.Remove(f);
                            }
                            listBoxBackups.Update();
                            ShowMsgBoxGenericCompleted("Deleted");
                        }
                        else
                        {
                            Log("deleteToolStripMenuItem_Click: could not find " + backupDir.FullName);
                            MessageBox.Show("Sorry, could not find " + backupDir.FullName);
                        }
                    }
                    else if (dResult == DialogResult.No)
                    {

                    }
                    else if (dResult == DialogResult.Cancel)
                    {

                    }
                }
                catch (Exception ex)
                {
                    Log(ex.Message);
                    MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                Log(ex.Message);
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void renameSelectedBackupFileToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            try
            {
                if (listBoxBackups.SelectedItems.Count == 1)
                {
                    //MessageBox.Show(Convert.ToString(listView1.SelectedItems[0].Tag));
                    //System.IO.File.Move("oldfilename", "newfilename");
                    //open move box
                    FileInfo file = (FileInfo)listBoxBackups.SelectedItems[0];
                    RenameForm newf = new RenameForm(file.Name);
                    DialogResult r = newf.ShowDialog();
                    if (r == System.Windows.Forms.DialogResult.OK)
                    {
                        string fullnew = file.FullName.Replace(file.Name, newf.NewName);
                        System.IO.File.Move(file.FullName, fullnew);
                        ShowMsgBoxGenericCompleted("Rename");
                        //Now reload the listview
                        listBoxBackups.BeginUpdate();
                        listBoxBackups.Items.Clear();
                        foreach (string filepath in Directory.GetFiles(Ini.GetKeyValue("settings", "backup_loc")))
                        {
                            FileInfo file2 = new FileInfo(filepath);
                            listBoxBackups.Items.Add(file2);

                        }
                        listBoxBackups.EndUpdate();
                        listBoxBackups.Update();
                    }
                    else
                        MessageBox.Show("Rename cancelled.");
                }
                else
                    Log("WARNING: Nothing selected to rename. OR more than one item selected.");
            }
            catch (Exception ex)
            {
                Log("renameSelectedBackupFileToolStripMenuItem1_Click: " + ex.Message);
                MessageBox.Show("ERROR: " + ex.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void restoreSelectedBackupFileToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            try
            {
                if (listBoxBackups.SelectedItems.Count == 1)
                {
                    FileInfo file = (FileInfo)listBoxBackups.SelectedItems[0];
                    string backupfilePath = file.FullName;
                    DialogResult dResult = MessageBox.Show("Are you sure you want to RESTORE " + file.Name + "\r\n World to Minecraft from file\r\n" + backupfilePath + "?\r\n(Make sure you have backed it up first as this deletes the current world)", "RESTORE WORLD " + file.Name, MessageBoxButtons.YesNoCancel, MessageBoxIcon.Warning);
                    if (dResult == DialogResult.Yes)
                    {
                        //first Delete the saved dir
                        DirectoryInfo worldDir = new DirectoryInfo(Ini.GetSection("settings").GetKey("minecraft_saved").GetValue() + "\\" +  file.Name.Split('_')[0]);
                        if (Directory.Exists(worldDir.FullName))
                        {
                            Directory.Delete(worldDir.FullName, true);
                            //now recreate it from the zip file
                            Directory.CreateDirectory(worldDir.FullName);
                            ZipFile backupZip = new ZipFile(backupfilePath);
                            backupZip.ExtractAll(worldDir.FullName);

                            //update
                            ShowMsgBoxGenericCompleted( file.Name + " restored");
                            LoadSavedWorlds();
                        }
                        else
                        {
                            Log("restoreSelectedBackupFileToolStripMenuItem_Click: could not find " + worldDir.FullName);
                            MessageBox.Show("Sorry, could not find " + worldDir.FullName +"\r\nRemeber the backup file must have the following format:\r\n <WORLDNAME>_<anythingelse>.zip" );
                        }
                    }
                    else if (dResult == DialogResult.No)
                    {

                    }
                    else if (dResult == DialogResult.Cancel)
                    {

                    }
                }
                else
                    MessageBox.Show("Sorry, only select one world to restore at a time.");
            }
            catch (Exception ex)
            {
                Log(ex.Message);
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void openSelectedBackupInEihortToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                if (listBoxBackups.SelectedItems.Count == 1)
                {
                    FileInfo backupzip = new FileInfo(listBoxBackups.SelectedItem.ToString());
                    string temp_loc = Ini.GetSection("settings").GetKey("backup_loc").GetValue().Trim() + "\\" + backupzip.Name.Replace(".zip", "") ;
                   
                    //get the backupfile name and extract to temp location
                    ZipFile buZip = new ZipFile(backupzip.FullName);
                    buZip.ExtractAll(temp_loc);

                    //lets try to open 
                    FileInfo eihort_exe = new FileInfo(Ini.GetKeyValue("settings", "eihort"));
                    string world_to_open = "\"" + temp_loc + "\"";

                    // Use ProcessStartInfo class
                    ProcessStartInfo startInfo = new ProcessStartInfo();
                    startInfo.CreateNoWindow = false;
                    startInfo.UseShellExecute = true;
                    startInfo.FileName = eihort_exe.FullName;
                    //startInfo.WindowStyle = ProcessWindowStyle.Hidden;
                    startInfo.Arguments = world_to_open;
                    Log("DoViewEihort command: " + eihort_exe + " " + world_to_open);
                    // Start the process with the info we specified.
                    // Call WaitForExit and then the using statement will close.
                    using (Process exeProcess = Process.Start(startInfo))
                    {
                        exeProcess.WaitForExit();
                    }
                    //delete the temp location
                    Directory.Delete(temp_loc, true);
                }
                else
                    MessageBox.Show("You must select a world first.");
            }
            catch (Exception ex)
            {
                Log("viewWorldUsingEihortToolStripMenuItem_Click() error: " + ex.Message + Environment.NewLine + ex.StackTrace);
                MessageBox.Show(ex.Message, "viewWorldUsingEihortToolStripMenuItem_Click() error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        #endregion MenuStrip Items

        #region Sound
        /// <summary>
        /// Play local copy 
        /// </summary>
        private void PlaySound()
        {
            System.Media.SoundPlayer sound = new System.Media.SoundPlayer();
            //sound.SoundLocation = Common.sound_wav_correct4;
            sound.Play();
        }

        /// <summary>
        /// Play local copy 
        /// </summary>
        private void PlayRandomSound(bool IsCorrect)
        {
            if (IsCorrect)
            {
                System.Media.SoundPlayer sound = new System.Media.SoundPlayer();
                Random rand = new Random();//rand.Next(1, 12)
                int iChoose = rand.Next(1, 4);
                /*
                switch (iChoose)
                {
                    case 1:
                        sound.SoundLocation = Common.sound_wav_correct1;
                        break;
                    case 2:
                        sound.SoundLocation = Common.sound_wav_correct2;
                        break;
                    case 3:
                        sound.SoundLocation = Common.sound_wav_correct3;
                        break;
                    case 4:
                        sound.SoundLocation = Common.sound_wav_correct4;
                        break;
                    default:
                        sound.SoundLocation = Common.sound_wav_correct4;
                        break;
                }
                 */
                sound.Play();
            }
            else
            {

            }

        }
        #endregion Sound

        #region Misc
        private void DoRenameOfBackup()
        {
            if (listView1.SelectedItems.Count == 1)
            {
                //MessageBox.Show(Convert.ToString(listView1.SelectedItems[0].Tag));
                //System.IO.File.Move("oldfilename", "newfilename");
                //open move box
                FileInfo file = new FileInfo(Convert.ToString(listView1.SelectedItems[0].Tag));
                RenameForm newf = new RenameForm(file.Name);
                DialogResult r = newf.ShowDialog();
                if (r == System.Windows.Forms.DialogResult.OK)
                {
                    string fullnew = file.FullName.Replace(file.Name, newf.NewName);
                    System.IO.File.Move(file.FullName, fullnew);
                    ShowMsgBoxGenericCompleted("Rename");
                    //Now reload the listview
                    LoadSavedWorlds();
                }
                else
                    MessageBox.Show("Rename cancelled.");
            }
            else
                Log("WARNING: Nothing selected to rename. OR more than one item selected.");
        }

        private void CreateLogFile()
        {
            //Create log file if not there
            if (!File.Exists(Common.gLogFile))
            {
                FileStream fs = File.Create(Common.gLogFile);
                fs.Close();
            }
        }

        private void Log(string logline)
        {
            File.AppendAllText(Common.gLogFile, DateTime.Now.ToString() + ", " + logline + "\r\n");
        }

        /// <summary>
        /// Nice little MessageBox.Show
        /// </summary>
        /// <param name="whatdone"></param>
        public void ShowMsgBoxGenericCompleted(String whatdone)
        {
            MessageBox.Show(whatdone + " Completed!", "Completed", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        private int PickRandomNumber(bool removeeasies)
        {
            Cursor.Current = Cursors.WaitCursor;
            int i = 1;
            try
            {
                if (!removeeasies)
                {
                    Random rand = new Random();//rand.Next(1, 12)
                    i = rand.Next(1, 12);
                }
                else
                {
                    //don't pick any easy ones 1,2,5,10,11
                    while (i == 1 || i == 2 || i == 5 || i == 10 || i == 11)
                    {
                        Random rand = new Random();
                        i = rand.Next(1, 12);
                    }
                }
                System.Threading.Thread.Sleep(100);
            }
            catch (Exception ex)
            {
                Log(ex.Message);
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            Cursor.Current = Cursors.Default;
            return i;
        }

        /// <summary>
        /// Looks to see if the default C:\Users\[USER]\AppData\Roaming\.minecraft\saves exists
        /// </summary>
        /// <returns></returns>
        private bool IsDefaultFolder()
        {
            bool b = false;
            if (Directory.Exists(Common.MinecraftDefaultFolderWin))
            {
                b = true;
                Log("IsDefaultFolder: Directory.Exists: " + Common.MinecraftDefaultFolderWin);
            }
            else
            {
                Log("IsDefaultFolder: Directory does NOT Exist: " + Common.MinecraftDefaultFolderWin);
            }

            return b;
        }

        /// <summary>
        /// Opens the selected map in mcmapDZ
        /// </summary>
        private void OpenRenderingForSelected()
        {
            if (listView1.SelectedItems.Count == 1)
            {
                //lets try to open the rendering
                DirectoryInfo mcmapDZ_rendersDir = new DirectoryInfo(Ini.GetKeyValue("settings", "mcmapDZ_renders"));
                string filetoopen = mcmapDZ_rendersDir.FullName + "\\" + listView1.SelectedItems[0].Text + "\\index.html";
                try
                {
                    if (File.Exists(filetoopen))
                        System.Diagnostics.Process.Start(filetoopen);
                    else
                        MessageBox.Show(filetoopen + "\r\nFile not found. Run mcmapDZ for this map.", "Map not Rendered by mcmapDZ", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                catch (Exception ex)
                {
                    Log(ex.Message);
                    MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
                MessageBox.Show("You must select a world first.");
        }

        void exe_Command(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start(Convert.ToString(((Button)sender).Tag));
        }

        /// <summary>
        /// Checks to see if the directory exists then loads the image to the button and displays the name
        /// </summary>
        /// <param name="btn">Button</param>
        /// <param name="lbl">Label</param>
        /// <param name="fullpath">fullpath with filename</param>
        private void iniButton(Button btn, string fullpath)
        {
            try
            {
                FileInfo file = new FileInfo(fullpath);
                if (Directory.Exists(file.DirectoryName))
                {
                    btn.Enabled = true;
                    Image image = Icon.ExtractAssociatedIcon(file.FullName).ToBitmap();
                    btn.Image = image;
                    btn.Update();
                }
                else
                {
                    btn.Enabled = false;
                    btn.Update();
                }

            }
            catch (Exception)
            {
                throw;
            }
        }

        private void MyAppsSetup()
        {
            try
            {
                if (Ini.HasSection("myapps"))
                {
                    int index = 0;//
                    int HeightPoint = 50;
                    int WidthPoint = 6;
                    foreach (IniFile.IniSection.IniKey app in Ini.GetSection("myapps").Keys)
                    {
                        groupBox2.Controls.Add(new Button());
                        ((Button)groupBox2.Controls[index]).Width = 45;
                        ((Button)groupBox2.Controls[index]).Height = 45;
                        groupBox2.Controls[index].Text = app.Name;
                        toolTip1.SetToolTip(groupBox2.Controls[index], app.Name);
                        groupBox2.Controls[index].SetBounds(WidthPoint, HeightPoint, 45, 45);
                        iniButton(((Button)groupBox2.Controls[index]), app.GetValue());//draw the image on the button
                        ((Button)groupBox2.Controls[index]).Tag = app.GetValue();//assign exe path to Tag
                        ((Button)groupBox2.Controls[index]).Click += new EventHandler(exe_Command);
                        HeightPoint = HeightPoint + 50;
                        if (HeightPoint > 450)
                        {
                            HeightPoint = 50;
                            WidthPoint = WidthPoint + 50;
                        }

                        index = index + 1;
                    }
                }
            }
            catch (Exception)
            {

                throw;
            }
        }
        #endregion Misc

        #region Events
        private void listView1_DoubleClick(object sender, EventArgs e)
        {
            OpenRenderingForSelected();
        }

        private void DoViewEihort()
        {
            try
            {
                if (listView1.SelectedItems.Count == 1)
                {
                    //lets try to open 
                    FileInfo eihort_exe = new FileInfo(Ini.GetKeyValue("settings", "eihort"));
                    string world_to_open = "\"" + Ini.GetSection("settings").GetKey("minecraft_saved").GetValue().Trim() + "\\" + listView1.SelectedItems[0].Text + "\"";

                    // Use ProcessStartInfo class
                    ProcessStartInfo startInfo = new ProcessStartInfo();
                    startInfo.CreateNoWindow = false;
                    startInfo.UseShellExecute = true;
                    startInfo.FileName = eihort_exe.FullName;
                    //startInfo.WindowStyle = ProcessWindowStyle.Hidden;
                    startInfo.Arguments = world_to_open;
                    Log("DoViewEihort command: " + eihort_exe + " " + world_to_open);
                    // Start the process with the info we specified.
                    // Call WaitForExit and then the using statement will close.
                    using (Process exeProcess = Process.Start(startInfo))
                    {
                        exeProcess.WaitForExit();
                    }
                    //Log("viewWorldUsingEihortToolStripMenuItem_Click: ");
                }
                else
                    MessageBox.Show("You must select a world first.");
            }
            catch (Exception ex)
            {
                Log("viewWorldUsingEihortToolStripMenuItem_Click() error: " + ex.Message + Environment.NewLine + ex.StackTrace);
                MessageBox.Show(ex.Message, "viewWorldUsingEihortToolStripMenuItem_Click() error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void viewWorldUsingEihortToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DoViewEihort();
        }

        private void viewWorldUsingEihortToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            DoViewEihort();
        }

        private void openMcmapDZGUIToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                //lets try to open 
                string mcmapDZGUI_exe = Ini.GetKeyValue("settings", "mcmapDZ_renders").Replace("renders", "mcmapDZ-GUI.exe");

                // Use ProcessStartInfo class
                ProcessStartInfo startInfo = new ProcessStartInfo();
                startInfo.CreateNoWindow = false;
                startInfo.UseShellExecute = false;
                startInfo.FileName = mcmapDZGUI_exe;
                //startInfo.WindowStyle = ProcessWindowStyle.Hidden;
                startInfo.Arguments = "";
                // Start the process with the info we specified.
                // Call WaitForExit and then the using statement will close.
                using (Process exeProcess = Process.Start(startInfo))
                {
                    exeProcess.WaitForExit();
                }
                //Now reload the listview
                LoadSavedWorlds();
            }
            catch (Exception ex)
            {
                Log("openMcmapDZGUIToolStripMenuItem_Click() error: " + ex.Message + Environment.NewLine + ex.StackTrace);
                MessageBox.Show(ex.Message, "openMcmapDZGUIToolStripMenuItem_Click() error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void viewWorldUsingToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenRenderingForSelected();
        }

        private void viewWorldUsingMcmapDZToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenRenderingForSelected();
        }

        private void listView1_MouseDown(object sender, MouseEventArgs e)
        {
            try
            {
                //Copy the node selected...
                //selectedTreeNode = (TreeNode)(treeView1.GetNodeAt(e.X, e.Y).Clone());
                #region If RIGHT-CLICK...
                //If RIGHT-CLICK...
                if (e.Button == MouseButtons.Right)
                {
                    if (((ListViewItem)(listView1.GetItemAt(e.X, e.Y))) != null)
                    {
                        //now show RIGHTCLICK MENU...
                        this.ContextMenuStrip = contextMenuStrip1;
                    }
                }
                #endregion If RIGHT-CLICK...
            }
            catch (Exception ex)
            {
                MessageBox.Show("treeView1_MouseDown ERROR: " + ex.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void txtbMCExeLoc_TextChanged(object sender, EventArgs e)
        {
            bSave.Enabled = true;
        }

        private void txtbMCServerLoc_TextChanged(object sender, EventArgs e)
        {
            bSave.Enabled = true;
        }

        private void txtbMCMAPDZLoc_TextChanged(object sender, EventArgs e)
        {
            bSave.Enabled = true;
        }

        private void txtbMCsaved_TextChanged(object sender, EventArgs e)
        {
            bSave.Enabled = true;
        }

        private void txtbEihortLoc_TextChanged(object sender, EventArgs e)
        {
            bSave.Enabled = true;
        }

        private void txtbBackupsLoc_TextChanged(object sender, EventArgs e)
        {
            bSave.Enabled = true;
        }

        private void tabMain_Enter(object sender, EventArgs e)
        {
            //Refresh
            LoadSavedWorlds();
        }

        private void tabBackups_Enter(object sender, EventArgs e)
        {
            listBoxBackups.BeginUpdate();
            listBoxBackups.Items.Clear();
            foreach (string filepath in Directory.GetFiles(Ini.GetKeyValue("settings", "backup_loc")))
            {
                FileInfo file = new FileInfo(filepath);
                listBoxBackups.Items.Add(file);

            }
            listBoxBackups.EndUpdate();
            listBoxBackups.Update();
        }

        private void listBoxBackups_MouseDown(object sender, MouseEventArgs e)
        {
            try
            {
                //Copy the node selected...
                //selectedTreeNode = (TreeNode)(treeView1.GetNodeAt(e.X, e.Y).Clone());
                #region If RIGHT-CLICK...
                //If RIGHT-CLICK...
                if (e.Button == MouseButtons.Right)
                {
                    if (listBoxBackups.SelectedIndex >= 0)
                    {
                        //now show RIGHTCLICK MENU...
                        this.ContextMenuStrip = contextMenuStrip2;
                    }
                }
                #endregion If RIGHT-CLICK...
            }
            catch (Exception ex)
            {
                MessageBox.Show("treeView1_MouseDown ERROR: " + ex.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        #endregion Events

        #region Backups
        private string FindLastBackup(string WorldName, out FileInfo theFile)
        {
            theFile = null;
            string str_DateTime = "";
            try
            {
                //look in the current archive folder for all the filenames that begin with this world
                FileInfo YoungestFile = null;
                foreach (string file in Directory.GetFiles(Ini.GetKeyValue("settings", "backup_loc")))
                {
                    FileInfo curFile = new FileInfo(file);
                    if (curFile.Name.StartsWith(WorldName))
                    {
                        //we found a world that matches search; if younest is null then this search is the youngest
                        if (YoungestFile == null)
                        {
                            YoungestFile = curFile;
                            str_DateTime = YoungestFile.CreationTime.ToString("MM/dd/yyyy HH:mm");
                            theFile = YoungestFile;
                        }
                        else
                        {
                            if (curFile.CreationTime > YoungestFile.CreationTime)
                            {
                                YoungestFile = curFile;
                                str_DateTime = YoungestFile.CreationTime.ToString("MM/dd/yyyy HH:mm");
                                theFile = YoungestFile;
                            }
                        }
                    }
                }
                //what is that latest?
            }
            catch (Exception ex)
            {
                Log("ERROR FindLastBackup(): " + ex.Message);
                MessageBox.Show("ERROR: " + ex.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            return str_DateTime;
        }

        private void DoBackup()
        {
            Cursor.Current = Cursors.WaitCursor;
            DirectoryInfo savedDir = new DirectoryInfo(Ini.GetKeyValue("settings", "minecraft_saved"));
            //Now list each saved World and then match a rendered image
            DirectoryInfo[] subdirectorysaved = savedDir.GetDirectories();
            foreach (ListViewItem item in listView1.SelectedItems)
            {
                string uniqueFilename = item.Text + "_" + DateTime.Now.ToString("MMddyyyyHHmmss") + ".zip";
                foreach (DirectoryInfo world in subdirectorysaved)
                {
                    //Only backup the world we selected
                    if (world.Name == item.Text)
                    {
                        CreateZipFile(Ini.GetKeyValue("settings", "backup_loc") + "\\" + uniqueFilename, world.FullName);
                        Log("DoBackup(): " + Ini.GetKeyValue("settings", "backup_loc") + "\\" + uniqueFilename);
                        break;
                    }
                }
            }
            //Now reload the listview
            LoadSavedWorlds();
            Cursor.Current = Cursors.Default;
            ShowMsgBoxGenericCompleted("Backup");
        }

        private void DoBackupForAll()
        {
            Cursor.Current = Cursors.WaitCursor;
            DirectoryInfo savedDir = new DirectoryInfo(Ini.GetKeyValue("settings", "minecraft_saved"));
            //Now list each saved World and then match a rendered image
            DirectoryInfo[] subdirectorysaved = savedDir.GetDirectories();
            //do it for all
            foreach (ListViewItem item in listView1.Items)
            {
                string uniqueFilename = item.Text + "_" + DateTime.Now.ToString("MMddyyyyHHmmss") + ".zip";
                foreach (DirectoryInfo world in subdirectorysaved)
                {
                    //Only backup the world we selected
                    if (world.Name == item.Text)
                    {
                        CreateZipFile(Ini.GetKeyValue("settings", "backup_loc") + "\\" + uniqueFilename, world.FullName);
                        Log("DoBackup(): " + Ini.GetKeyValue("settings", "backup_loc") + "\\" + uniqueFilename);
                        break;
                    }
                }
            }
            //Now reload the listview
            LoadSavedWorlds();
            Cursor.Current = Cursors.Default;
            ShowMsgBoxGenericCompleted("Backup");
        }

        private void btnBackup_Click(object sender, EventArgs e)
        {
            try
            {
                DoBackup();
            }
            catch (Exception ex)
            {
                Log("ERROR: " + ex.Message);
                MessageBox.Show("ERROR: " + ex.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnBackupAll_Click(object sender, EventArgs e)
        {
            try
            {
                DoBackupForAll();
            }
            catch (Exception ex)
            {
                Log("ERROR: " + ex.Message);
                MessageBox.Show("ERROR: " + ex.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void DoRestore()
        {
            if (listView1.SelectedItems.Count == 1)
            {
                string backupfilePath = Convert.ToString(listView1.SelectedItems[0].Tag);
                DialogResult dResult = MessageBox.Show("Are you sure you want to RESTORE " + listView1.SelectedItems[0].Text + "\r\n World to Minecraft from file\r\n" + backupfilePath + "?\r\n(Make sure you have backed it up first as this deletes the current world)", "RESTORE WORLD " + listView1.SelectedItems[0].Text, MessageBoxButtons.YesNoCancel, MessageBoxIcon.Warning);
                if (dResult == DialogResult.Yes)
                {
                    //first Delete the saved dir
                    DirectoryInfo worldDir = new DirectoryInfo(Ini.GetSection("settings").GetKey("minecraft_saved").GetValue() + "\\" + listView1.SelectedItems[0].Text);
                    if (Directory.Exists(worldDir.FullName))
                    {
                        Directory.Delete(worldDir.FullName, true);
                        //now recreate it from the zip file
                        Directory.CreateDirectory(worldDir.FullName);
                        ZipFile backupZip = new ZipFile(backupfilePath);
                        backupZip.ExtractAll(worldDir.FullName);

                        //update
                        ShowMsgBoxGenericCompleted(listView1.SelectedItems[0].Text + " restored");
                        LoadSavedWorlds();
                    }
                    else
                    {
                        Log("restoreSelectedBackupFileToolStripMenuItem_Click: could not find " + worldDir.FullName);
                        MessageBox.Show("Sorry, could not find " + worldDir.FullName);
                    }
                }
                else if (dResult == DialogResult.No)
                {

                }
                else if (dResult == DialogResult.Cancel)
                {

                }
            }
            else
                MessageBox.Show("Sorry, only select one world to restore at a time.");
        }

        #endregion Backups

        /// <summary>
        /// Creates a zip file
        /// </summary>
        /// <param name="fullfilename"></param>
        /// <param name="fileList"></param>
        /// <returns></returns>
        public static bool CreateZipFile(String fullfilename, string rootFolder)
        {
            bool bCreated = false;
            try
            {
                ZipFile newZipfile = new ZipFile(fullfilename);
                newZipfile.AddSelectedFiles("*.*", rootFolder, "", true);
                newZipfile.Save();
                newZipfile.Dispose();
                bCreated = true;
            }
            catch (Exception ex)
            {
                Console.WriteLine("MCMyVault.CreateZipFile: " + ex.Message);
            }

            return bCreated;
        }

        private void LoadSavedWorlds()
        {
            listView1.BeginUpdate();
            listView1.Clear();
            //the settings should be ok...
            DirectoryInfo savedDir = new DirectoryInfo(Ini.GetKeyValue("settings", "minecraft_saved"));
            DirectoryInfo mcmapDZ_rendersDir = new DirectoryInfo(Ini.GetKeyValue("settings", "mcmapDZ_renders"));

            //foreach rendered world, get one image at location 
            // C:\Users\Joe\Documents\minecraft tools\mcmapDZ\renders\cybertron     \maps\day_files\9\0_0.jpg
            DirectoryInfo[] subdirectoryEntries = mcmapDZ_rendersDir.GetDirectories();
            if (subdirectoryEntries.Length > 0)
            {
                foreach (DirectoryInfo dir in subdirectoryEntries)
                {
                    string tempfilename = Environment.GetFolderPath(Environment.SpecialFolder.InternetCache) + "\\" + DateTime.Now.Ticks.ToString() +".jpg";
                    Thread.Sleep(10);
                    File.Copy(dir.FullName + "\\maps\\day_files\\8\\0_0.jpg", tempfilename); 
                    string jpg = tempfilename;
                    //  File.Copy(jpg, tempfilename);
                    if (File.Exists(jpg))
                    {
                        Image img = Image.FromFile(jpg);
                        img.Tag = dir.Name;
                        ImageListWorlds.Images.Add(dir.Name, img);
                    }
                }
            }
            else
            {
                //No subdirectories found in renders, ask user if they wish to render all their worlds....

                DialogResult dResult = MessageBox.Show("Can't display the maps of your worlds.\r\nDo you want to auto render now using mcmapDZ?\r\n(this may take a few minutes depending upon how many worlds you have)", "Create images of the map for all Worlds using mcmapDZ?", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Information);
                if (dResult == DialogResult.Yes)
                {
                    //ok, send the commandline to mcmapsDZ
                    //"C:\Users\Joe\Documents\MineCraft MyVault\mcmapDZ\mcmapDZ.exe"  -day -night -cave -skylight -extension jpg -quality 1 -orientation E "C:\Users\Joe\AppData\Roaming\.minecraft\saves\cybertron" 
                    //lets try to open 
                    string mcmapDZ_exe = Ini.GetKeyValue("settings", "mcmapDZ_renders").Replace("renders", "mcmapDZ.exe");
                    //foreach world
                    //Now list each saved World and then match a rendered image
                    DirectoryInfo[] subdirectorysaved0 = savedDir.GetDirectories();
                    foreach (DirectoryInfo world in subdirectorysaved0)
                    {
                        // Use ProcessStartInfo class
                        ProcessStartInfo startInfo = new ProcessStartInfo();
                        startInfo.CreateNoWindow = false;
                        startInfo.UseShellExecute = false;
                        startInfo.FileName = mcmapDZ_exe;
                        //startInfo.WindowStyle = ProcessWindowStyle.Hidden;
                        startInfo.Arguments = "-day -night -cave -skylight -extension jpg -quality 1 -orientation E \"" + world.FullName + "\"";
                        Log("Auto-mcmapDZ executing: mcmapDZ " + startInfo.Arguments);
                        // Start the process with the info we specified.
                        // Call WaitForExit and then the using statement will close.
                        using (Process exeProcess = Process.Start(startInfo))
                        {
                            exeProcess.WaitForExit();
                        }
                    }
                    //Now! load the images
                    subdirectoryEntries = mcmapDZ_rendersDir.GetDirectories();
                    foreach (DirectoryInfo dir in subdirectoryEntries)
                    {
                        string tempfilename = Environment.GetFolderPath(Environment.SpecialFolder.InternetCache) + "\\" + DateTime.Now.Ticks.ToString() + ".jpg";
                        Thread.Sleep(10);
                        File.Copy(dir.FullName + "\\maps\\day_files\\8\\0_0.jpg", tempfilename);
                        string jpg = tempfilename;
                        if (File.Exists(jpg))
                        {
                            Image img = Image.FromFile(jpg);
                            img.Tag = dir.Name;
                            ImageListWorlds.Images.Add(dir.Name, img);
                        }
                    }

                }
                else if (dResult == DialogResult.No)
                {

                }
                else if (dResult == DialogResult.Cancel)
                {

                }
            }

            ImageListWorlds.ImageSize = new Size(128, 128);
            //Assign the image list to list control
            listView1.LargeImageList = ImageListWorlds;
            listView1.SmallImageList = ImageListWorlds;
            //listView1.Columns.Add("Map");
            listView1.Columns.Add("Map and Name");
            listView1.Columns.Add("Last Backup");
            listView1.Columns.Add("Filename");
            
            //Now list each saved World and then match a rendered image
            DirectoryInfo[] subdirectorysaved = savedDir.GetDirectories();
            foreach (DirectoryInfo world in subdirectorysaved)
            {
                FileInfo file = null;
                string lastbackedupdate = FindLastBackup(world.Name, out file);
                if (file != null)
                {
                    listView1.Items.Add(new ListViewItem(new string[] { world.Name, lastbackedupdate, file.Name }, world.Name));
                    //lets add the file path to Tag for later use
                    listView1.Items[listView1.Items.Count - 1].Tag = file.FullName;
                }
                else
                {
                    listView1.Items.Add(new ListViewItem(new string[] { world.Name, lastbackedupdate, "no backup found" }, world.Name));
                    //lets add the file path to Tag for later use
                    listView1.Items[listView1.Items.Count - 1].Tag = "no backup found";
                }
            }

            listView1.AutoResizeColumns(ColumnHeaderAutoResizeStyle.ColumnContent);//autosize
            listView1.EndUpdate();
            listView1.Update();
        }

        private void btnMinecraft_Click(object sender, EventArgs e)
        {
            try
            {
                System.Diagnostics.Process.Start(Ini.GetSection("settings").GetKey("minecraft").GetValue());
            }
            catch (Exception ex)
            {
                Log(ex.Message);
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void supportSiteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                System.Diagnostics.Process.Start("http://joeswammi.com/mcmyvault/page2.html#purchase");
            }
            catch (Exception ex)
            {
                Log(ex.Message);
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void wikiAndVideosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                System.Diagnostics.Process.Start("http://mcmyvault.googlecode.com");
            }
            catch (Exception ex)
            {
                Log(ex.Message);
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

    }
}