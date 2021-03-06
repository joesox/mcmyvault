// MCMyVault v1.8.7.0
// by JPSIII
// Copyright 2012-2015. All Rights Reserved
//      * NEW FEATURE: [Backups tab] added Keys.Delete event
//      - NEW FEATURE: [Issue 30:] Added View Screenshots folder from menu options
//      - BUG FIXED:   [Issue 28:] Incorrect settings displayed on Settings tab after changing config ini files
// To Do:
//      - NEw FEATURE: [Issue 14:] Add feature to change eihort texture and maybe other features 
// Limits: 
/////////////////////////////////////////////////////////
//LICENSE 
//BY DOWNLOADING AND USING, YOU AGREE TO THE FOLLOWING TERMS: 
//If it is your intent to use this software for non-commercial purposes,  
//such as in academic research, this software is free and is covered under  
//the GNU GPL License, given here: <http://www.gnu.org/licenses/gpl.txt>  
//You agree with 3RDPARTY's Terms Of Service 
//provided with other files included with this code.
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
using System.Text.RegularExpressions;
using System.Web;
using System.Net;

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
        Dictionary<String, List<Object>> CurrentTPObjects = new Dictionary<String, List<Object>>(); //CurrentTPObjects[packname, DirectoryInfo or FileInfo]
        List<DirectoryInfo> CleanUpDirs = new List<DirectoryInfo>();
        bool bOnFirstEnter = true;

        private void Form1_Load(object sender, EventArgs e)
        {
            //this.tabMain.SuspendLayout();
            tabControl1.Hide();
            //Update the Title bar text
            this.Text = Application.ProductName + " " + Application.ProductVersion;

            //ToolTips
            toolTip1.SetToolTip(this.dataGridView1, "Right-Click for menu. Double-click to open map.");
            toolTip1.SetToolTip(this.listBoxBackups, "Right-Click for menu.");
            toolTip1.SetToolTip(this.lboxCurrentTP, "Right-Click for menu. Select a Texturepack for a preview.");
            toolTip1.SetToolTip(this.picBoxlogo, "logo");
            toolTip1.SetToolTip(this.picBoxWorldPreview, "screenshot");
            toolTip1.SetToolTip(this.picBoxItems, "items");

            //We need to check if this is a 1.5.1.9+ upgrade and copy the config.ini over to new location
            if (DoIniCopy())
            {
                //Copy the config.ini
                //Common.ConfigIniFileOLD
                if (!Directory.Exists(Common.AppDataPathName))
                    Directory.CreateDirectory(Common.AppDataPathName);
                //only copy the file if old location exists
                FileInfo oldfile = new FileInfo(Common.ConfigIniFileOLD);
                if (Directory.Exists(oldfile.DirectoryName))
                {
                    if (oldfile.Exists)
                    {
                        //Ask the user if they want to copy the old settings file over or not
                        DialogResult dResult = MessageBox.Show("Found a previous installation of MCMyVault @.\r\n" + oldfile.DirectoryName + "\r\nWould you like to copy the settings to new installation? [NON-ADMINISTRATORS: Reccomendation is to click no or you will need to edit your config.ini/path settings]", "Copy previous settings?", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
                        if (dResult == DialogResult.Yes)
                        {
                            File.Copy(Common.ConfigIniFileOLD, Common.ConfigIniFile, true);
                            //now lets make a note for next time...
                            //create the upgraded.txt file showing it was upgraded
                            string newfilename = Application.StartupPath + "\\upgraded.txt";
                            FileStream fs = File.Create(newfilename);
                            fs.Close();
                            File.WriteAllText(newfilename, "uninstall");
                            Log("Upgrade completed; Copied ini file to: " + Common.ConfigIniFile);
                            MessageBox.Show("Upgrade completed. MCMyVault will restart with new settings.");
                            Application.Restart();
                        }
                        else if (dResult == DialogResult.No)
                        {

                        }
                        else if (dResult == DialogResult.Cancel)
                        {

                        }
                    }
                }
            }

            CreateLogFile();//Gets created in Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\" + GetTopFolderName(Application.StartupPath)

            //need to do this incase there is no existing config.ini
            ReadSettings();
            //Populate cboxProfiles with all the known ini files
            cboxProfilesRefresh();

            bOnFirstEnter = false;
            bSave.Enabled = false;

            MyAppsSetup();

            //this.tabMain.ResumeLayout();
            tabControl1.Show();

            //DataGridView Prep
            //dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;//Set in properties now
            foreach (DataGridViewColumn col in dataGridView1.Columns)
            {
                col.DefaultCellStyle.Font = new System.Drawing.Font("Consolas", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                col.DefaultCellStyle.ForeColor = System.Drawing.SystemColors.HotTrack;
            }

            //make sure the top row is selected
            if (dataGridView1.Rows.Count > 0)
                dataGridView1.Rows[0].Selected = true;
            this.tabMain.BringToFront();//make sure to bring to front, this also forces the selection above.

            this.tabMain.Focus();
        }

        private bool DoIniCopy()
        {
            bool bDoCopy = true;
            //look in program files for upgraded.txt
            string newfilename = Application.StartupPath + "\\upgraded.txt";
            if (File.Exists(newfilename))
            {
                bDoCopy = false;
                ///////////// This could create problems if they uninstall the old version after installing new because dependancies would vanish
                ////Is the upgraded txt file in an uninstall previous version state?
                //string message = File.ReadAllText(newfilename).Trim();
                //if (message.ToLower() == "uninstall")
                //{
                //    //Ask the user if they want to uninstall the old MCMyVault
                //    DialogResult dResult = MessageBox.Show("Would you like to uninstall the old MCMyVault installation from the Program Files?\r\n[NOTE: You must have administrator rights]", "Uninstall the old MCMyVault installation?", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
                //    if (dResult == DialogResult.Yes)
                //    {
                //        string UNINSTALLFILE = Common.ConfigIniFileOLD.Replace("config.ini", "unins000.exe");
                //        try
                //        {
                //            System.Diagnostics.Process.Start(UNINSTALLFILE);
                //        }
                //        catch (Exception ex)
                //        {
                //            Log(ex.Message);
                //            MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                //        }
                //    }
                //    else if (dResult == DialogResult.No)
                //    {
                //        //we need to mark this as upgrade completed
                //        File.WriteAllText(newfilename, "uninstall");
                //    }
                //    else if (dResult == DialogResult.Cancel)
                //    {

                //    }
                //}
            }
            return bDoCopy;
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

            //What is the Current ini config to load??
            if (File.Exists(Common.StartupConfigIniFile))
            {
                IniFile Startupini = new IniFile();
                Startupini.Load(Common.StartupConfigIniFile);
                //assign the one to load
                String currentini = Startupini.GetKeyValue("settings", "currentconfig");
                if (File.Exists(Common.AppDataPathName + "\\" + currentini))
                {
                    Common.ConfigIniFile = Common.AppDataPathName + "\\" + currentini;
                }
            }

            //INI LOADING START
            if (File.Exists(Common.ConfigIniFile))
                Ini.Load(Common.ConfigIniFile);
            else
            {
                //File.Create(Common.ConfigIniFile);
                Log("configuration file not found. Creating...");
                Ini.AddSection("settings").AddKey("minecraft").SetValue("");
                //Ini.AddSection("settings").AddKey("minecraft_server").SetValue(""); //NOT USED
                Ini.AddSection("settings").AddKey("minecraft_saved").SetValue(Common.MinecraftDefaultFolderWin);
                if (Directory.Exists(Common.MinecraftDefaultTextureFolder))
                    Ini.AddSection("settings").AddKey("minecraft_textpacks").SetValue(Common.MinecraftDefaultTextureFolder);
                else
                    Ini.AddSection("settings").AddKey("minecraft_textpacks").SetValue(Common.MinecraftDefaultTextureFolder.Replace("texturepacks", "resourcepacks"));

                Ini.AddSection("settings").AddKey("backup_loc").SetValue(Common.MinecraftToolsBackupsFolder);
                Ini.AddSection("settings").AddKey("backup_loc_textpacks").SetValue(Common.MinecraftToolsBackupsFolder + "-texturepacks");
                bool bIs64Bit = Is64BitSystem();
                if (Directory.Exists(Common.mcmap2_dir))
                {
                    Ini.AddSection("settings").AddKey("mcmapDZ_renders").SetValue(Common.mcmap2_dir);
                }
                if (File.Exists(Common.eihort))
                {
                    if (bIs64Bit)
                        Ini.AddSection("settings").AddKey("eihort").SetValue(Common.eihort);
                    else
                        Ini.AddSection("settings").AddKey("eihort").SetValue(Common.eihort32bit);
                }
                Ini.AddSection("settings").AddKey("cleanupdays").SetValue("7");
                Ini.AddSection("settings").AddKey("map_col_width").SetValue(Convert.ToString(dataGridView1.Columns["ColumnMap"].Width));
                Ini.AddSection("settings").AddKey("name_col_width").SetValue(Convert.ToString(dataGridView1.Columns["ColumnName"].Width));
                Ini.AddSection("settings").AddKey("lastbackup_col_width").SetValue(Convert.ToString(dataGridView1.Columns["ColumnLastBackup"].Width));
                Ini.AddSection("settings").AddKey("filename_col_width").SetValue(Convert.ToString(dataGridView1.Columns["ColumnFilename"].Width));
                Ini.AddSection("myapps");
                Ini.Save(Common.ConfigIniFile);
                Log("Created file: " + Common.ConfigIniFile);
            }


            //Ini File should be there, now populate tab dataGridViewSettings

        }

        /// <summary>
        /// Load the settings from INI and into GUI
        /// </summary>
        private void InitIniSettings()
        {
            bool Is64 = Is64BitSystem();
            Log("Is64BitSystem: " + Is64.ToString());

            if (Ini.HasSection("settings"))
            {
                //GET Minecraft.exe//GET Minecraft.exe//GET Minecraft.exe//GET Minecraft.exe//GET Minecraft.exe
                if (String.IsNullOrEmpty(Ini.GetKeyValue("settings", "minecraft")))
                {
                    //Minecraft.exe doesn't have a default installation folder, so we must prompt the user
                    //NO value found in ini, ask user where Minecraft.exe is
                    MessageBox.Show("No path to Minecraft.exe found in config.ini. Please navigate to its path...", "No path to Minecraft.exe found", MessageBoxButtons.OK, MessageBoxIcon.Question);

                    String minecraft_path = "";
                    if (Is64)
                    {
                        minecraft_path = Environment.GetFolderPath(Environment.SpecialFolder.ProgramFilesX86) + "\\Minecraft";
                        Log("InitIniSettings: looking for " + minecraft_path);
                        if (Directory.Exists(minecraft_path))
                            minecraft_path = BrowseTo("exe", minecraft_path);
                        else
                            minecraft_path = BrowseTo("exe", Environment.GetFolderPath(Environment.SpecialFolder.ProgramFilesX86));
                        //     dataGridViewSettings.Rows[GetRowIndex("minecraft")].Cells["Value"].Value = minecraft_path;
                        Ini.SetKeyValue("settings", "minecraft", minecraft_path);
                        Ini.Save(Common.ConfigIniFile);//save right away!!
                    }
                    else
                    {
                        minecraft_path = Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles) + "\\Minecraft";
                        Log("InitIniSettings: looking for " + minecraft_path);
                        if (Directory.Exists(minecraft_path))
                            minecraft_path = BrowseTo("exe", minecraft_path);
                        else
                            minecraft_path = BrowseTo("exe", Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles));
                        //    dataGridViewSettings.Rows[GetRowIndex("minecraft")].Cells["Value"].Value = minecraft_path;
                        Ini.SetKeyValue("settings", "minecraft", minecraft_path);
                        Ini.Save(Common.ConfigIniFile);//save right away!!
                    }
                }
                else
                {
                    //has value
                    //   txtbMCExeLoc.Text = Ini.GetKeyValue("settings", "minecraft");
                }

                if (String.IsNullOrEmpty(Ini.GetKeyValue("settings", "mcmapDZ_renders")))
                {
                    //Minecraft.exe doesn't have a default installation folder, so we must prompt the user
                    //NO value found in ini, ask user where mcmapDZ is
                    //  MessageBox.Show("No path to mcmapDZ\\renders found in config.ini. Please navigate to its path...", "No path to mcmapDZ\renders found", MessageBoxButtons.OK, MessageBoxIcon.Question);
                    String mcmapDZ_path = "";

                    if (Directory.Exists(Common.mcmap2_dir))
                    {
                        //mcmapDZ_path = BrowseToFolder(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\MCMyVault");
                        mcmapDZ_path = Common.mcmap2_dir;
                    }
                    else
                        mcmapDZ_path = BrowseToFolder(Environment.SpecialFolder.MyDocuments);
                    //    dataGridViewSettings.Rows[GetRowIndex("mcmapDZ_renders")].Cells["Value"].Value = mcmapDZ_path;
                    Ini.SetKeyValue("settings", "mcmapDZ_renders", mcmapDZ_path);
                    Ini.Save(Common.ConfigIniFile);//save right away!!
                }
                else
                {
                    //has value
                    //DETECT IF MCMAPDZ
                    string mcmapPath = Ini.GetKeyValue("settings", "mcmapDZ_renders");
                    if (mcmapPath.Contains("mcmapDZ"))
                    {
                        string mcmap2PATH = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\MCMyVault\\mcmap2";
                        if (Directory.Exists(mcmap2PATH))
                        {
                            MessageBox.Show("Detected a path to mcmapDZ; Auto-correcting your settings to the new default path for mcmap2.");
                            //    dataGridViewSettings.Rows[GetRowIndex("mcmapDZ_renders")].Cells["Value"].Value = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\MCMyVault\\mcmap2";
                            Ini.SetKeyValue("settings", "mcmapDZ_renders", mcmap2PATH);
                            Ini.Save(Common.ConfigIniFile);//save right away!!
                        }
                        else
                        {
                            MessageBox.Show("Detected a path to mcmapDZ; Please re-install MCMyVault so mcmap2 gets installed to default location. Using previous setting for mcmap2 path.");
                            //        txtbMCMAPDZLoc.Text = Ini.GetKeyValue("settings", "mcmapDZ_renders");
                        }
                    }
                    else
                    {
                    }
                    //         txtbMCMAPDZLoc.Text = Ini.GetKeyValue("settings", "mcmapDZ_renders");
                }
                //GET minecraft_saved//GET minecraft_saved//GET minecraft_saved//GET minecraft_saved//GET minecraft_saved
                if (String.IsNullOrEmpty(Ini.GetKeyValue("settings", "minecraft_saved")))
                {
                    if (IsDefaultFolder())
                    {
                        //there is a default folder
                        //         txtbMCsaved.Text = Common.MinecraftDefaultFolderWin;
                        Ini.SetKeyValue("settings", "minecraft_saved", Common.MinecraftDefaultFolderWin);
                        Ini.Save(Common.ConfigIniFile);//save right away!!
                    }
                    else
                    {
                        //Minecraft.exe doesn't have a default installation folder, so we must prompt the user
                        //NO value found in ini, ask user where Minecraft.exe is
                        MessageBox.Show("No path to C:\\Users\\[USER]\\AppData\\Roaming\\.minecraft\\saves found on harddrive. Please navigate to its path...", "No path to .minecraft\\saved found", MessageBoxButtons.OK, MessageBoxIcon.Question);
                        String mcsaved_path = BrowseToFolder(Environment.SpecialFolder.ApplicationData);
                        //         txtbMCsaved.Text = mcsaved_path;
                        Ini.SetKeyValue("settings", "minecraft_saved", mcsaved_path);
                    }
                }
                else
                {
                    //has value
                    //     txtbMCsaved.Text = Ini.GetKeyValue("settings", "minecraft_saved");
                }
                //GET eihort.exe//GET eihort.exe//GET eihort.exe//GET eihort.exe//GET eihort.exe
                if (String.IsNullOrEmpty(Ini.GetKeyValue("settings", "eihort")))
                {
                    //eihort.exe doesn't have a default installation folder, so we must prompt the user
                    //NO value found in ini, ask user where eihort.exe is
                    MessageBox.Show("No path to eihort.exe found in config.ini. Please navigate to its path...", "No path to eihort.exe found", MessageBoxButtons.OK, MessageBoxIcon.Question);
                    String eihort_path = "";
                    if (Is64)
                    {
                        if (File.Exists(Common.eihort))
                        {
                            eihort_path = Common.eihort;
                        }
                        else
                            eihort_path = BrowseTo("exe", Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\MCMyVault");
                        //          txtbEihortLoc.Text = eihort_path;
                        Ini.SetKeyValue("settings", "eihort", eihort_path);
                        Ini.Save(Common.ConfigIniFile);//save right away!!
                    }
                    else
                    {
                        if (File.Exists(Common.eihort32bit))
                        {
                            eihort_path = Common.eihort32bit;
                        }
                        else
                            eihort_path = BrowseTo("exe", Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\MCMyVault");
                        //           txtbEihortLoc.Text = eihort_path;
                        Ini.SetKeyValue("settings", "eihort", eihort_path);
                        Ini.Save(Common.ConfigIniFile);//save right away!!
                    }
                }
                else
                {
                    //has value
                    //       txtbEihortLoc.Text = Ini.GetKeyValue("settings", "eihort");
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

                        //            txtbBackupsLoc.Text = Common.MinecraftToolsBackupsFolder;
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
                        //           txtbBackupsLoc.Text = backups_path;
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
                    //        txtbBackupsLoc.Text = Ini.GetKeyValue("settings", "backup_loc");
                }
                if (String.IsNullOrEmpty(Ini.GetKeyValue("settings", "backup_loc_textpacks")))
                {
                    //GET backup_loc_textpacks//GET backup_loc_textpacks//GET backup_loc_textpacks//GET backup_loc_textpacks//GET backup_loc_textpacks
                    DialogResult dResulttp = MessageBox.Show("No path to texturepacks backup folder yet.\r\nDo you want to use the default location?", "No path to a texturepacks backup folder found", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Information);
                    if (dResulttp == DialogResult.Yes)
                    {
                        //Create Backups Folder if not there
                        string tp_backuploc = Common.MinecraftToolsBackupsFolder + "-texturepacks";
                        if (!Directory.Exists(tp_backuploc))
                        {
                            Directory.CreateDirectory(tp_backuploc);
                        }

                        //           txtTexturepackbackups.Text = tp_backuploc;
                        Ini.SetKeyValue("settings", "backup_loc_textpacks", tp_backuploc);
                        Ini.Save(Common.ConfigIniFile);//save right away!!
                    }
                    else if (dResulttp == DialogResult.No)
                    {
                        String textpacksbackups_path = "";
                        if (Directory.Exists(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\MCMyVault"))
                        {
                            textpacksbackups_path = BrowseToFolder(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\MCMyVault");
                        }
                        else
                            textpacksbackups_path = BrowseToFolder(Environment.SpecialFolder.MyDocuments);
                        //            txtTexturepackbackups.Text = textpacksbackups_path;
                        Ini.SetKeyValue("settings", "backup_loc_textpacks", textpacksbackups_path);
                        Ini.Save(Common.ConfigIniFile);//save right away!!
                    }
                    else if (dResulttp == DialogResult.Cancel)
                    {

                    }
                }
                else
                {
                    //has value
                    //            txtTexturepackbackups.Text = Ini.GetKeyValue("settings", "backup_loc_textpacks");
                }
                //GET minecraft_textpacks//GET minecraft_textpacks//GET minecraft_textpacks//GET minecraft_textpacks//GET minecraft_textpacks
                if (String.IsNullOrEmpty(Ini.GetKeyValue("settings", "minecraft_textpacks")))
                {
                    if (IsDefaultFolder())
                    {
                        //there is a default folder
                        //            txtbMCtexturepacks.Text = Common.MinecraftDefaultTextureFolder;
                        Ini.SetKeyValue("settings", "minecraft_textpacks", Common.MinecraftDefaultTextureFolder);
                        Ini.Save(Common.ConfigIniFile);//save right away!!
                    }
                    else
                    {
                        //Minecraft.exe doesn't have a default installation folder, so we must prompt the user
                        //NO value found in ini, ask user where Minecraft.exe is
                        MessageBox.Show("No path to C:\\Users\\[USER]\\AppData\\Roaming\\.minecraft\\texturepacks found on harddrive. Please navigate to its path...", "No path to .minecraft\\texturepacks found", MessageBoxButtons.OK, MessageBoxIcon.Question);
                        String mctextpacks = BrowseToFolder(Environment.SpecialFolder.ApplicationData);
                        //           txtbMCtexturepacks.Text = mctextpacks;
                        Ini.SetKeyValue("settings", "minecraft_textpacks", mctextpacks);
                    }
                }
                else
                {
                    //has value
                    //         txtbMCtexturepacks.Text = Ini.GetKeyValue("settings", "minecraft_textpacks");
                }
                //GET cleanupdays//GET cleanupdays//GET cleanupdays//GET cleanupdays//GET cleanupdays
                string cleanupdays = Ini.GetKeyValue("settings", "cleanupdays");
                if (String.IsNullOrEmpty(cleanupdays) || (!Char.IsNumber(cleanupdays, 0)))
                {
                    cleanupdays = "7";
                    Ini.SetKeyValue("settings", "cleanupdays", cleanupdays);
                    Ini.Save(Common.ConfigIniFile);//save right away!!
                }
                //has value
                numericUpDown1.Value = Convert.ToDecimal(cleanupdays);
                //END GET cleanupdays//END GET cleanupdays//END GET cleanupdays//END GET cleanupdays//END GET cleanupdays
                //GET cloud_backup_loc//GET cloud_backup_loc//GET cloud_backup_loc//GET cloud_backup_loc//GET cloud_backup_loc
                if (String.IsNullOrEmpty(Ini.GetKeyValue("settings", "cloud_backup_loc")))
                {
                    DialogResult dResulttp = MessageBox.Show("No path to cloud storage backup folder yet.\r\nDo you use cloud storage for Minecraft backups?", "No path to cloud storage backup folder found", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                    if (dResulttp == DialogResult.Yes)
                    {
                        string cloud_backuploc = BrowseToFolder(Environment.SpecialFolder.UserProfile);
                        //               txtBoxCloudBackup.Text = cloud_backuploc;
                        Ini.SetKeyValue("settings", "cloud_backup_loc", cloud_backuploc);
                        Ini.Save(Common.ConfigIniFile);//save right away!!
                    }
                    else if (dResulttp == DialogResult.No)
                    {
                        Ini.SetKeyValue("settings", "cloud_backup_loc", Common.MinecraftToolsBackupsFolder);//we need a value or it keeps prompting us.
                        Ini.Save(Common.ConfigIniFile);//save right away!!
                    }
                }
                else
                {
                    //has value
                    //           txtBoxCloudBackup.Text = Ini.GetKeyValue("settings", "cloud_backup_loc");
                }
                //GET snapshots_loc//GET snapshots_loc//GET snapshots_loc//GET snapshots_loc//GET snapshots_loc
                if (String.IsNullOrEmpty(Ini.GetKeyValue("settings", "snapshots_loc")))
                {
                    //MessageBox.Show(, MessageBoxButtons.OK, MessageBoxIcon.Question);

                    DialogResult dResult = MessageBox.Show("No path to snapshots folder yet.\r\nDo you want to use the default location?", "No path to a snapshots folder found", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Information);
                    if (dResult == DialogResult.Yes)
                    {
                        //Create snapshotss Folder if not there
                        if (!Directory.Exists(Common.MinecraftToolssnapshotssFolder))
                        {
                            Directory.CreateDirectory(Common.MinecraftToolssnapshotssFolder);
                        }

                        //            txtBoxSnapshotsLoc.Text = Common.MinecraftToolssnapshotssFolder;
                        Ini.SetKeyValue("settings", "snapshots_loc", Common.MinecraftToolssnapshotssFolder);
                        Ini.Save(Common.ConfigIniFile);//save right away!!
                    }
                    else if (dResult == DialogResult.No)
                    {
                        String snapshotss_path = "";
                        if (Directory.Exists(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\MCMyVault"))
                        {
                            snapshotss_path = BrowseToFolder(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\MCMyVault");
                        }
                        else
                            snapshotss_path = BrowseToFolder(Environment.SpecialFolder.MyDocuments);
                        //            txtBoxSnapshotsLoc.Text = snapshotss_path;
                        Ini.SetKeyValue("settings", "snapshots_loc", snapshotss_path);
                        Ini.Save(Common.ConfigIniFile);//save right away!!
                    }
                    else if (dResult == DialogResult.Cancel)
                    {

                    }
                }
                else
                {
                    //has value
                    //            txtBoxSnapshotsLoc.Text = Ini.GetKeyValue("settings", "snapshots_loc");
                }

                UpdateExploreCloudBackupMenuItem();

                //PREP CUSTOMS
                readcustomfilesSettings();
                readcustomfoldersSettings();
                tboxCustomBUpath.Text = Ini.GetKeyValue("settings", "cloud_backup_loc");

                //Get the Column settings
                ReadColumnSettings();

                //Need to make sure Settingsview is up to date.
                UpdateSettingsView(true);

                //Just checkto see if  IsTextpack_WorldSameBackup()
                IsTextpack_WorldSameBackup();
            }
        }

        private void readcustomfilesSettings()
        {
            //make sure it has a section first [customfiles]
            if (Ini.HasSection("customfiles"))
            {
                listbCustomFiles.Items.Clear();
                foreach (IniFile.IniSection.IniKey key in Ini.GetSection("customfiles").Keys)
                {
                    if (!String.IsNullOrWhiteSpace(key.Value))
                        listbCustomFiles.Items.Add(key.Value);
                }
            }
        }

        private void readcustomfoldersSettings()
        {
            //make sure it has a section first [customfiles]
            if (Ini.HasSection("customfolders"))
            {
                foreach (IniFile.IniSection.IniKey key in Ini.GetSection("customfolders").Keys)
                {
                    if (!String.IsNullOrWhiteSpace(key.Value))
                        listbCustomFolders.Items.Add(key.Value);
                }
            }
        }

        /// <summary>
        /// Get the index of a row for a specific setting name in first column (0).
        /// </summary>
        /// <param name="settingsname"></param>
        /// <returns></returns>
        private int GetRowIndex(string settingsname)
        {
            int rowIndex = -1;
            foreach (DataGridViewRow row in dataGridViewSettings.Rows)
            {
                if (row.Cells[0].Value.ToString().Equals(settingsname))
                {
                    rowIndex = row.Index;
                    break;
                }
            }
            return rowIndex;
        }

        private void bSave_Click(object sender, EventArgs e)
        {
            try
            {
                Properties.Settings.Default.AppPath = tBoxAppPath.Text;
                Properties.Settings.Default.Save();

                //read the controls and save the selections
                //Ini.SetKeyValue("settings", "minecraft_server", txtbMCServerLoc.Text.Trim()); //Future use?
                Ini.SetKeyValue("settings", "cleanupdays", dataGridViewSettings.Rows[GetRowIndex("cleanupdays")].Cells["Value"].Value.ToString());
                Ini.SetKeyValue("settings", "minecraft", dataGridViewSettings.Rows[GetRowIndex("minecraft")].Cells["Value"].Value.ToString());
                Ini.SetKeyValue("settings", "mcmapDZ_renders", dataGridViewSettings.Rows[GetRowIndex("mcmapDZ_renders")].Cells["Value"].Value.ToString());
                Ini.SetKeyValue("settings", "minecraft_saved", dataGridViewSettings.Rows[GetRowIndex("minecraft_saved")].Cells["Value"].Value.ToString());
                Ini.SetKeyValue("settings", "minecraft_textpacks", dataGridViewSettings.Rows[GetRowIndex("minecraft_textpacks")].Cells["Value"].Value.ToString());
                Ini.SetKeyValue("settings", "eihort", dataGridViewSettings.Rows[GetRowIndex("eihort")].Cells["Value"].Value.ToString());
                Ini.SetKeyValue("settings", "backup_loc", dataGridViewSettings.Rows[GetRowIndex("backup_loc")].Cells["Value"].Value.ToString());
                Ini.SetKeyValue("settings", "backup_loc_textpacks", dataGridViewSettings.Rows[GetRowIndex("backup_loc_textpacks")].Cells["Value"].Value.ToString());
                Ini.SetKeyValue("settings", "cloud_backup_loc", dataGridViewSettings.Rows[GetRowIndex("cloud_backup_loc")].Cells["Value"].Value.ToString());
                Ini.SetKeyValue("settings", "snapshots_loc", dataGridViewSettings.Rows[GetRowIndex("snapshots_loc")].Cells["Value"].Value.ToString());
                Ini.Save(Common.ConfigIniFile);
                bSave.Enabled = false;
                UpdateExploreCloudBackupMenuItem();
            }
            catch (Exception ex)
            {
                Log(ex.Message);
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private String BrowseTo(string[] filetype, string default_path)
        {
            String path = "";
            String filesfilterstring = "";

            OpenFileDialog ofd = new OpenFileDialog();

            foreach (string extension in filetype)
            {
                filesfilterstring += "(*." + extension + ")|*." + extension + "|";
            }

            String prettyprint = "Common Files|";
            foreach (string item in filetype)
            {
                prettyprint += "*." + item + ";";
            }
            prettyprint = prettyprint.Remove(prettyprint.Length - 1);//remove the last ';'

            ofd.Filter = prettyprint + "|All files (*.*)|*.*";
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
            ////String minecraft_path = "";
            ////if (String.IsNullOrEmpty(txtbMCExeLoc.Text))
            ////    minecraft_path = BrowseTo("exe", Environment.GetFolderPath(Environment.SpecialFolder.ProgramFilesX86));
            ////else
            ////{
            ////    FileInfo mcexe = new FileInfo(txtbMCExeLoc.Text);
            ////    minecraft_path = BrowseTo("exe", mcexe.DirectoryName);
            ////    bSave.Enabled = true;
            ////}
            ////if (!String.IsNullOrEmpty(minecraft_path))
            ////{
            ////    txtbMCExeLoc.Text = minecraft_path;
            ////    Ini.SetKeyValue("settings", "minecraft", minecraft_path);
            ////}
        }

        //private void btnBrowseMCServer_Click(object sender, EventArgs e)
        //{//Future use?
        //    String minecraftsrvr_path = "";
        //    if (String.IsNullOrEmpty(txtbMCServerLoc.Text))
        //        minecraftsrvr_path = BrowseTo("exe", Environment.GetFolderPath(Environment.SpecialFolder.ProgramFilesX86));
        //    else
        //    {
        //        FileInfo mcexe = new FileInfo(txtbMCServerLoc.Text);
        //        minecraftsrvr_path = BrowseTo("exe", mcexe.DirectoryName);
        //    }
        //    if (!String.IsNullOrEmpty(minecraftsrvr_path))
        //    {
        //        txtbMCServerLoc.Text = minecraftsrvr_path;
        //        Ini.SetKeyValue("settings", "minecraft_server", minecraftsrvr_path);
        //        bSave.Enabled = true;
        //    }
        //}

        private void btnBrowseMCMAPDZ_Click(object sender, EventArgs e)
        {
            String mcmapDZ_path = "";
            mcmapDZ_path = BrowseToFolder(Environment.SpecialFolder.MyDocuments);

            if (!String.IsNullOrEmpty(mcmapDZ_path))
            {
                //   txtbMCMAPDZLoc.Text = mcmapDZ_path;
                Ini.SetKeyValue("settings", "mcmapDZ_renders", mcmapDZ_path);
                bSave.Enabled = true;
            }
        }

        private void btnBrowseMCsaved_Click(object sender, EventArgs e)
        {
            String MCsaved_path = BrowseToFolder(Environment.SpecialFolder.ApplicationData);
            if (!String.IsNullOrEmpty(MCsaved_path))
            {
                //        txtbMCsaved.Text = MCsaved_path;
                Ini.SetKeyValue("settings", "minecraft_saved", MCsaved_path);
                bSave.Enabled = true;
            }
        }

        private void btnBrowseEihortLoc_Click(object sender, EventArgs e)
        {
            //////txtbEihortLoc
            ////String eihort_path = "";
            ////if (String.IsNullOrEmpty(txtbEihortLoc.Text))
            ////    eihort_path = BrowseTo("exe", Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments));
            ////else
            ////{
            ////    FileInfo eihortexe = new FileInfo(txtbEihortLoc.Text);
            ////    eihort_path = BrowseTo("exe", eihortexe.DirectoryName);
            ////}
            ////if (!String.IsNullOrEmpty(eihort_path))
            ////{
            ////    txtbEihortLoc.Text = eihort_path;
            ////    Ini.SetKeyValue("settings", "eihort", eihort_path);
            ////    bSave.Enabled = true;
            ////}
        }

        private void btnBrowseBackupsLoc_Click(object sender, EventArgs e)
        {
            String backups_path = BrowseToFolder(Environment.SpecialFolder.MyDocuments);
            if (!String.IsNullOrEmpty(backups_path) && !IsTextpack_WorldSameBackup(backups_path, Ini.GetKeyValue("settings", "backup_loc_textpacks")))
            {
                //     txtbBackupsLoc.Text = backups_path;
                Ini.SetKeyValue("settings", "backup_loc", backups_path);
                bSave.Enabled = true;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            String MCtextpacks_path = BrowseToFolder(Environment.SpecialFolder.ApplicationData);
            if (!String.IsNullOrEmpty(MCtextpacks_path) && !IsTextpack_WorldSameBackup(MCtextpacks_path, Ini.GetKeyValue("settings", "minecraft_saved")))
            {
                //        txtbMCtexturepacks.Text = MCtextpacks_path;
                Ini.SetKeyValue("settings", "minecraft_textpacks", MCtextpacks_path);
            }
        }

        private void btnBrowseCloudBackupLoc_Click(object sender, EventArgs e)
        {
            String cloudbackups_path = BrowseToFolder(Environment.SpecialFolder.UserProfile);
            if (!String.IsNullOrEmpty(cloudbackups_path))
            {
                //          txtBoxCloudBackup.Text = cloudbackups_path;
                Ini.SetKeyValue("settings", "backup_loc", "cloud_backup_loc");
                bSave.Enabled = true;
            }
            UpdateExploreCloudBackupMenuItem();
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

        private void minecraftScreenshotLocationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                string scrnshts_folder = Ini.GetKeyValue("settings", "minecraft_saved").Replace("\\saves", "\\screenshots");
                //make sure it exists
                if (Directory.Exists(scrnshts_folder))
                {
                    System.Diagnostics.Process.Start(scrnshts_folder);
                }
                else
                    MessageBox.Show("There are no screenshots for this world yet.\r\n(Pressing F2 while playing Minecraft takes a screenshot)", "No screenshots taken", MessageBoxButtons.OK, MessageBoxIcon.Information);
                
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
                    DialogResult dResult = MessageBox.Show("Are you sure you want to DELETE WORLD " + dataGridView1.CurrentRow.Cells["ColumnName"].Value.ToString() + "\r\n from Minecraft?\r\n(Make sure you have backed it up first)", "DELETE WORLD " + dataGridView1.CurrentRow.Cells["ColumnName"].Value.ToString(), MessageBoxButtons.YesNoCancel, MessageBoxIcon.Warning);
                    if (dResult == DialogResult.Yes)
                    {
                        DirectoryInfo worldDir = new DirectoryInfo(Ini.GetSection("settings").GetKey("minecraft_saved").GetValue() + "\\" + dataGridView1.CurrentRow.Cells["ColumnName"].Value.ToString());
                        if (Directory.Exists(worldDir.FullName))
                        {
                            Directory.Delete(worldDir.FullName, true);
                            ShowMsgBoxGenericCompleted(dataGridView1.CurrentRow.Cells["ColumnName"].Value.ToString() + " deleted");
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
                string mcmap_exe = Ini.GetKeyValue("settings", "mcmapDZ_renders") + "\\mcmap.exe";
                string worldNAME = dataGridView1.CurrentRow.Cells["ColumnName"].Value.ToString();
                string worldFullname = Ini.GetKeyValue("settings", "minecraft_saved") + "\\" + worldNAME;
                // Use ProcessStartInfo class
                ProcessStartInfo startInfo = new ProcessStartInfo();
                startInfo.CreateNoWindow = false;
                startInfo.UseShellExecute = false;
                startInfo.WorkingDirectory = Ini.GetKeyValue("settings", "mcmapDZ_renders");
                startInfo.FileName = mcmap_exe;
                //startInfo.WindowStyle = ProcessWindowStyle.Hidden;

                //WORKS: mcmap.exe -day -skylight -file 1_7_4.png C:\Users\Joe\AppData\Roaming\.minecraft\saves\1_7_4
                //C:\Users\Joe\AppData\Roaming\.minecraft\saves\1_7_4
                startInfo.Arguments = "-day -skylight -file" + " \"" + worldNAME + ".png\" \"" + worldFullname + "\"";
                Log("Auto-mcmapDZ executing: mcmap2 " + startInfo.Arguments);
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

        private void DeleteBackupFile()
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

        private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                DeleteBackupFile();
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
                    //MessageBox.Show(Convert.ToString(dataGridView1.SelectedRows[0].Tag));
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
                        DirectoryInfo worldDir = new DirectoryInfo(Ini.GetSection("settings").GetKey("minecraft_saved").GetValue() + "\\" + file.Name.Split('_')[0]);
                        if (Directory.Exists(worldDir.FullName))
                        {
                            Directory.Delete(worldDir.FullName, true);
                            //now recreate it from the zip file
                            Directory.CreateDirectory(worldDir.FullName);
                            ZipFile backupZip = new ZipFile(backupfilePath);
                            backupZip.ExtractAll(worldDir.FullName);
                            //CheckSavedFolderStructure
                            CheckSavedFolderStructure(worldDir.FullName);

                            //update
                            ShowMsgBoxGenericCompleted(file.Name + " restored");
                            LoadSavedWorlds();
                        }
                        else
                        {
                            //Might be a new world so create directory
                            //now recreate it from the zip file
                            Directory.CreateDirectory(worldDir.FullName);
                            ZipFile backupZip = new ZipFile(backupfilePath);
                            backupZip.ExtractAll(worldDir.FullName);
                            //CheckSavedFolderStructure
                            CheckSavedFolderStructure(worldDir.FullName);

                            //update
                            ShowMsgBoxGenericCompleted(file.Name + " restored");
                            LoadSavedWorlds();

                            // Log("restoreSelectedBackupFileToolStripMenuItem_Click: could not find " + worldDir.FullName);
                            // MessageBox.Show("Sorry, could not find " + worldDir.FullName +"\r\nRemeber the backup file must have the following format:\r\n <WORLDNAME>_<anythingelse>.zip" );
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
                    string temp_loc = Ini.GetSection("settings").GetKey("backup_loc").GetValue().Trim() + "\\" + backupzip.Name.Replace(".zip", "");

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

        private void restartMCMyVaultToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Restart();
        }

        private void backupToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            try
            {
                Cursor.Current = Cursors.WaitCursor;
                //Get this selected object, is file or directory?
                List<Object> selectedObj = null;
                String currentTPObj = "";
                CurrentTPObjects.TryGetValue(lboxCurrentTP.SelectedItem.ToString().Trim().Replace(" [v1.5+]", "").Replace(" [v1.4.x]", "").Trim(), out selectedObj);
                if (selectedObj != null)
                {
                    string filebackupname = "";
                    foreach (object item in selectedObj)
                    {
                        if (item.GetType().ToString() == "System.IO.FileInfo")
                        {
                            //then we must have previously unzipped this in the temp folder
                            if (Directory.Exists(System.IO.Path.GetTempPath() + ((FileInfo)item).Name))
                            {
                                //change background color
                                //  tabCurrentTP.BackgroundImage = ;
                                currentTPObj = System.IO.Path.GetTempPath() + ((FileInfo)item).Name;
                            }
                            //lets just copy this file
                            string uniquemark = "_" + DateTime.Now.ToString("MMddyyyyHHmmss") + ".zip";
                            filebackupname = Ini.GetKeyValue("settings", "backup_loc_textpacks") + "\\" + ((FileInfo)item).Name.Replace(".zip", uniquemark);
                            File.Copy(((FileInfo)item).FullName, filebackupname);
                        }
                        else
                        {
                            //object if "System.IO.DirectoryInfo"
                            //read from the origin folder
                            currentTPObj = ((DirectoryInfo)item).FullName;
                            string uniqueFilename = ((DirectoryInfo)item).Name + "_" + DateTime.Now.ToString("MMddyyyyHHmmss") + ".zip";
                            //Only backup the textpack we selected
                            CreateZipFile(Ini.GetKeyValue("settings", "backup_loc_textpacks") + "\\" + uniqueFilename, currentTPObj);
                        }
                    }
                    Log("Saved texturepack : " + filebackupname);
                }
                else
                {
                    Log("[backupToolStripMenuItem1_Click]: Could not find '" + lboxCurrentTP.SelectedItem.ToString().Trim().Replace(" [v1.5+]", "").Replace(" [v1.4.x]", "").Trim() + "'. you may need to rename the pack.txt name and try again.");
                }

                Cursor.Current = Cursors.Default;
                ShowMsgBoxGenericCompleted("Backup of texturepack");
            }
            catch (Exception ex)
            {
                MessageBox.Show("backupToolStripMenuItem1_Click ERROR: " + ex.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void restoreToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                //choose the zip file
                //extract to minecraft_textpacks
                Cursor.Current = Cursors.WaitCursor;
                //Get this selected object, is file or directory?
                List<Object> selectedObj = null;
                CurrentTPObjects.TryGetValue(lboxCurrentTP.SelectedItem.ToString().Trim().Replace(" [v1.5+]", "").Replace(" [v1.4.x]", "").Trim(), out selectedObj);
                string selecteditem = selectedObj[0].GetType().ToString();//     "System.IO.FileInfo",  "System.IO.DirectoryInfo"
                if (selecteditem == "System.IO.FileInfo")
                {
                    ////should be .zip only
                    ZipFile tpZip = new ZipFile(((FileInfo)selectedObj[0]).FullName);
                    tpZip.ExtractAll(Ini.GetKeyValue("settings", "minecraft_textpacks") + "\\" + ((FileInfo)selectedObj[0]).Name.Replace(".zip", ""));
                    Log("Restored texturepack : " + ((FileInfo)selectedObj[0]).FullName);
                }
                else
                {
                    //blah, smart users may just manually copy directories in the backup location so
                    //  we better take care of them
                    DirUtils.CopyDirectory(((DirectoryInfo)selectedObj[0]).FullName, Ini.GetKeyValue("settings", "minecraft_textpacks") + "\\" + ((DirectoryInfo)selectedObj[0]).Name.Replace(".zip", ""), false);
                    Log("Restored texturepack : " + ((DirectoryInfo)selectedObj[0]).FullName);
                }


                Cursor.Current = Cursors.Default;
                ShowMsgBoxGenericCompleted("Restore of texturepack");
            }
            catch (Exception ex)
            {
                MessageBox.Show("restoreToolStripMenuItem_Click ERROR: " + ex.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void deleteThisTexturepackToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                //choose the zip file
                //extract to minecraft_textpacks
                Cursor.Current = Cursors.WaitCursor;
                //Get this selected object, is file or directory?
                List<Object> selectedObj = null;
                CurrentTPObjects.TryGetValue(lboxCurrentTP.SelectedItem.ToString().Trim().Replace(" [v1.5+]", "").Replace(" [v1.4.x]", "").Trim(), out selectedObj);
                string selecteditem = selectedObj[0].GetType().ToString();//     "System.IO.FileInfo",  "System.IO.DirectoryInfo"

                string NAME = "";
                if (selecteditem == "System.IO.FileInfo")
                {
                    NAME = ((FileInfo)selectedObj[0]).Name;
                }
                else
                    NAME = ((DirectoryInfo)selectedObj[0]).Name;

                DialogResult dResult = new DialogResult();
                if (radioButtonCurrentTP.Checked)
                    dResult = MessageBox.Show("Are you sure you want to DELETE TEXTUREPACK\r\n" + NAME + "\r\n from Minecraft?\r\n(Make sure you have backed it up first)", "DELETE TEXTUREPACK", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Warning);
                else
                    dResult = MessageBox.Show("Are you sure you want to DELETE TEXTUREPACK\r\n" + NAME + "\r\n from backups?", "DELETE TEXTUREPACK BACKUP", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Warning);
                if (dResult == DialogResult.Yes)
                {
                    if (selecteditem == "System.IO.FileInfo")
                    {
                        //should be .zip only
                        ((FileInfo)selectedObj[0]).Delete();
                        Log("Deleted texturepack : " + ((FileInfo)selectedObj[0]).FullName);
                    }
                    else
                    {
                        //Directory
                        ((DirectoryInfo)selectedObj[0]).Delete(true);
                        Log("Deleted texturepack : " + ((DirectoryInfo)selectedObj[0]).FullName);
                    }

                    RefreshtabCurrentTP();
                    ShowMsgBoxGenericCompleted("Deletion of texturepack");
                }
                else if (dResult == DialogResult.No)
                {

                }
                else if (dResult == DialogResult.Cancel)
                {

                }
                Cursor.Current = Cursors.Default;
            }
            catch (Exception ex)
            {
                MessageBox.Show("restoreToolStripMenuItem_Click ERROR: " + ex.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void exploreBackuptexturpacksLocationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                System.Diagnostics.Process.Start(Ini.GetKeyValue("settings", "backup_loc_textpacks"));
            }
            catch (Exception ex)
            {
                Log(ex.Message);
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void purgekeepXDaysForYToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                if (listBoxBackups.SelectedItems.Count == 1)
                {
                    FileInfo file = (FileInfo)listBoxBackups.SelectedItems[0];
                    string backupfilePath = file.FullName;
                    string WorldName = file.Name.Split('_')[0];
                    string cleanupdays = Ini.GetKeyValue("settings", "cleanupdays");
                    int ItemsDeleted = 0;
                    long TotalBytes = 0;
                    //Get the oldest File DateTime
                    TimeSpan DaysBack = TimeSpan.FromDays(Convert.ToDouble(cleanupdays));
                    DateTime PurgeOlderThanDateTime = DateTime.Now.Subtract(DaysBack);

                    DialogResult dResult = MessageBox.Show("Are you sure you want to PURGE Backups for '" + WorldName + "' for the last " + cleanupdays + " days?\r\n(This still keeps the oldest backup file for each day in the " + cleanupdays + " days)", "CLEANUP BACKUPS FOR WORLD " + WorldName, MessageBoxButtons.YesNoCancel, MessageBoxIcon.Warning);
                    if (dResult == DialogResult.Yes)
                    {
                        Log("Backup Cleanup: STARTING " + WorldName + " [" + PurgeOlderThanDateTime.ToString() + " to " + DateTime.Now.ToString() + "]");
                        List<FileInfo> FilesToKeepList = new List<FileInfo>();
                        List<FileInfo> TheWorldsFiles = new List<FileInfo>();
                        List<FileInfo> TheDaysFiles = new List<FileInfo>();
                        //Find all the files in the listBoxBackups for this world
                        foreach (FileInfo backupFile in listBoxBackups.Items)
                        {
                            if (backupFile.Name.StartsWith(WorldName))
                            {
                                //only add if it is in the given window
                                if (backupFile.LastWriteTime > PurgeOlderThanDateTime)
                                    TheWorldsFiles.Add(backupFile);
                            }
                        }
                        //We have all the backup files for the World, now take day by day and keep oldest
                        //Get the oldest file for each day (+1 adds today also)
                        for (int i = 0; i < DaysBack.Days + 1; i++)
                        {
                            DateTime ExaminingDay = DateTime.Now.Subtract(TimeSpan.FromDays(i));
                            foreach (FileInfo WbackupFile in TheWorldsFiles)
                            {
                                //Only Examine the day we are looking at defined by the Window setting
                                if ((WbackupFile.LastWriteTime.DayOfYear == ExaminingDay.DayOfYear))
                                    TheDaysFiles.Add(WbackupFile);
                            }
                            if (TheDaysFiles.Count > 0)
                            {
                                FileInfo oldfile = GetOldestFile(TheDaysFiles);
                                FilesToKeepList.Add(oldfile);
                                Log("Backup Cleanup: GetOldestFile(" + ExaminingDay.ToShortDateString() + ") = " + oldfile.Name + " [" + oldfile.LastWriteTime.ToString() + "]");
                                TheDaysFiles.Clear();//Clear it ; Finished with that day
                            }
                        }
                        //Lets PURGE
                        if (FilesToKeepList.Count != TheWorldsFiles.Count)
                        {
                            //files to keep is different then what we atarted with!
                            foreach (FileInfo worldFile in TheWorldsFiles)
                            {
                                if (!FilesToKeepList.Contains(worldFile))
                                {
                                    ItemsDeleted = ItemsDeleted + 1;
                                    //bytes
                                    TotalBytes = TotalBytes + worldFile.Length;
                                    string LastWriteTime = worldFile.LastWriteTime.ToString();
                                    string fName = worldFile.Name;
                                    //Delete the old file
                                    FileSystem.DeleteFile(worldFile.FullName, UIOption.OnlyErrorDialogs, RecycleOption.SendToRecycleBin);
                                    //LOG
                                    Log("Backup Cleanup Deleted:" + fName + " |LastWriteTime: " + LastWriteTime);
                                }
                            }
                        }

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

                        //LOG
                        string message = "Backup Cleanup COMPLETED; TOTAL FILES DELETED: " + Convert.ToString(ItemsDeleted) + " [" + ToFileSize(TotalBytes) + "]";
                        Log(message);
                        Log("Backup Cleanup: ENDED");
                        //update
                        ShowMsgBoxGenericCompleted(message);
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

        private void checkForUpdateToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                Cursor.Current = Cursors.WaitCursor;
                Uri uri = new Uri(@"http://joeswammi.com/mcmyvault/version.txt");
                WebRequest http = HttpWebRequest.Create(uri);
                HttpWebResponse response = (HttpWebResponse)http.GetResponse();
                Stream stream = response.GetResponseStream();
                StreamReader rdr = new StreamReader(stream);
                string currentVersion = rdr.ReadToEnd();
                response.Close();
                Cursor.Current = Cursors.Default;

                //ask to close
                if (currentVersion != Application.ProductVersion)
                {
                    DialogResult dResult = MessageBox.Show("There is a newer version available: " + currentVersion + "\r\nWould you like to close MCMyVault and be directed to the downloads section of MCMyVault?", "PROCEED WITH UPGRADE?", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
                    if (dResult == DialogResult.Yes)
                    {
                        try
                        {
                            System.Diagnostics.Process.Start("https://code.google.com/p/mcmyvault/downloads/list");
                                               }
                        catch (UriFormatException)
                        {
                            Console.WriteLine("Invalid URL");
                        }
                        catch (IOException)
                        {
                            Console.WriteLine("Could not connect to URL");
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
                {
                    MessageBox.Show("No updates are currently available.");
                }
            }
            catch (Exception ex)
            {
                Log("checkForUpdateToolStripMenuItem_Click" + ex.Message);
                MessageBox.Show("Do you have internet connection?\r\nERROR: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
        /// <summary>
        /// I hack to guess if 64BitSystem OS
        /// </summary>
        /// <returns></returns>
        private bool Is64BitSystem()
        {
            if (Directory.Exists(Environment.GetEnvironmentVariable("ProgramFiles(x86)"))) return true;
            else return false;
        }

        /// <summary>
        /// Checks to see if a debug setting is enabled in the config.ini
        /// </summary>
        /// <returns></returns>
        private bool IsDebugOn()
        {
            string debug = Ini.GetKeyValue("settings", "debug");
            if (debug.ToLower().Trim() == "true")
                return true;
            else
                return false;
        }

        /// <summary>
        /// Properly shows a file length
        /// </summary>
        /// <param name="length">File length, in bytes</param>
        /// <returns></returns>
        public static string ToFileSize(long length)
        {
            // if lenght>1MB, show size in MB
            const long ONE_MB = 1024L * 1024L;
            if (length > ONE_MB)
                return (((double)((long)(length * 100) / ONE_MB))
                    / 100).ToString("#,##0.0") + " MB";

            // if length>1KB, show size in KB
            const long ONE_KB = 1024L;
            if (length > ONE_KB)
                return (((double)((long)(length * 100) / ONE_KB))
                    / 100).ToString("#,##0.0") + " KB";

            // show size in bytes
            return length.ToString("#,###,##0") + " bytes";
        }

        /// <summary>
        /// Simple checks to see if textpack and worlds are assigned same folder
        /// Messagebox only is throw to alert user to change.
        /// (using the current ini file settings)
        /// </summary>
        /// <returns></returns>
        private bool IsTextpack_WorldSameBackup()
        {
            bool bSame = false;
            if (Ini.GetKeyValue("settings", "backup_loc") == Ini.GetKeyValue("settings", "backup_loc_textpacks"))
            {
                MessageBox.Show("WARNING: WORLD & TEXTPACKS HAVE SAME BACKUP LOCATION!\r\nPlease assign them different locations by using the Settings tab.", "WARNING CHECK BACKUP LOCATIONS", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                GoToTab("tabSettings");
            }

            return bSame;
        }

        /// <summary>
        /// Simple checks to see if textpack and worlds are assigned same folder
        /// Messagebox only is throw to alert user to change.
        /// </summary>
        /// <returns></returns>
        private bool IsTextpack_WorldSameBackup(string possible_newloc, string oldlocation)
        {
            bool bSame = false;
            if (possible_newloc == oldlocation)
            {
                MessageBox.Show("WARNING: WORLD & TEXTPACKS HAVE SAME BACKUP LOCATION!\r\nPlease assign them different locations by using the Settings tab.", "WARNING CHECK BACKUP LOCATIONS", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                GoToTab("tabSettings");
            }

            return bSame;
        }

        private void DoRenameOfBackup()
        {
            //MessageBox.Show(Convert.ToString(dataGridView1.SelectedRows[0].Tag));
            //System.IO.File.Move("oldfilename", "newfilename");
            //open move box
            FileInfo file = new FileInfo(Ini.GetKeyValue("settings", "backup_loc") + "\\" + Convert.ToString(dataGridView1.SelectedRows[0].Cells["ColumnFilename"].Value));
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
        /// Opens the selected map in mcmap2
        /// </summary>
        private void OpenRenderingForSelected()
        {
            //lets try to open the rendering
            DirectoryInfo mcmap2_Dir = new DirectoryInfo(Ini.GetKeyValue("settings", "mcmapDZ_renders"));
            string filetoopen = mcmap2_Dir.FullName + "\\" + dataGridView1.CurrentRow.Cells["ColumnName"].Value.ToString() + ".png";
            try
            {
                if (File.Exists(filetoopen))
                    System.Diagnostics.Process.Start(filetoopen);
                else
                    MessageBox.Show(filetoopen + "\r\nFile not found. Run mcmapGUI2 for this map.", "Map not Rendered by mcmap", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                Log(ex.Message);
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
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
                if (IsDebugOn())
                    Log("DEBUG MyAppsSetup RUNNING");//DEBUG
                if (Ini.HasSection("myapps"))
                {
                    flowLayoutPanelMyApps.Controls.Clear();
                    int index = 0;//
                    int HeightPoint = 50;
                    int WidthPoint = 6;
                    foreach (IniFile.IniSection.IniKey app in Ini.GetSection("myapps").Keys)
                    {
                        Button btnmyapp = new Button();
                        btnmyapp.Width = 90;
                        btnmyapp.Height = 45;
                        btnmyapp.Text = app.Name;
                        toolTip1.SetToolTip(btnmyapp, app.Name);
                        iniButton(btnmyapp, app.GetValue());//draw the image on the button
                        btnmyapp.Tag = app.GetValue();//assign exe path to Tag
                        btnmyapp.Click += new EventHandler(exe_Command);
                        flowLayoutPanelMyApps.Controls.Add(btnmyapp);
                    }
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        /// <summary>
        /// Makes this key the selected tab for tabControl1
        /// </summary>
        /// <param name="key"></param>
        private void GoToTab(string key)
        {
            tabControl1.SelectedTab = tabControl1.TabPages[key];
        }

        /// <summary>
        /// Get the oldest LastWriteTime file from the given list
        /// </summary>
        /// <param name="thefiles"></param>
        /// <returns></returns>
        public static FileInfo GetOldestFile(List<FileInfo> thefiles)
        {
            if (thefiles.Count == 0)
                return null;

            FileInfo oldest = thefiles[0];
            foreach (var child in thefiles)
            {
                if (child.LastWriteTime > oldest.LastWriteTime)
                    oldest = child;
            }

            return oldest;
        }

        /// <summary>
        /// Reads the ini file for previous column settings (if found)
        /// it saves them or it saves the current widths to the ini file
        /// </summary>
        public void ReadColumnSettings()
        {
            try
            {
                //GET COLUMN SETTINGS
                //Ini.AddSection("settings").AddKey("map_col_width").SetValue(Convert.ToString(dataGridView1.Columns["ColumnMap"].Width));
                if (!String.IsNullOrEmpty(Ini.GetKeyValue("settings", "map_col_width")))
                {
                    dataGridView1.Columns["ColumnMap"].Width = Convert.ToInt32(Ini.GetKeyValue("settings", "map_col_width"));
                }
                else
                    Ini.AddSection("settings").AddKey("map_col_width").SetValue(Convert.ToString(dataGridView1.Columns["ColumnMap"].Width));
                //Ini.AddSection("settings").AddKey("name_col_width").SetValue(Convert.ToString(dataGridView1.Columns["ColumnName"].Width));
                if (!String.IsNullOrEmpty(Ini.GetKeyValue("settings", "name_col_width")))
                {
                    dataGridView1.Columns["ColumnName"].Width = Convert.ToInt32(Ini.GetKeyValue("settings", "name_col_width"));
                }
                else
                    Ini.AddSection("settings").AddKey("name_col_width").SetValue(Convert.ToString(dataGridView1.Columns["ColumnName"].Width));
                //Ini.AddSection("settings").AddKey("lastbackup_col_width").SetValue(Convert.ToString(dataGridView1.Columns["ColumnLastBackup"].Width));
                if (!String.IsNullOrEmpty(Ini.GetKeyValue("settings", "lastbackup_col_width")))
                {
                    dataGridView1.Columns["ColumnLastBackup"].Width = Convert.ToInt32(Ini.GetKeyValue("settings", "lastbackup_col_width"));
                }
                else
                    Ini.AddSection("settings").AddKey("lastbackup_col_width").SetValue(Convert.ToString(dataGridView1.Columns["ColumnLastBackup"].Width));
                //Ini.AddSection("settings").AddKey("filename_col_width").SetValue(Convert.ToString(dataGridView1.Columns["ColumnFilename"].Width));
                if (!String.IsNullOrEmpty(Ini.GetKeyValue("settings", "filename_col_width")))
                {
                    dataGridView1.Columns["ColumnFilename"].Width = Convert.ToInt32(Ini.GetKeyValue("settings", "filename_col_width"));
                }
                else
                    Ini.AddSection("settings").AddKey("filename_col_width").SetValue(Convert.ToString(dataGridView1.Columns["ColumnFilename"].Width));

                Ini.Save(Common.ConfigIniFile);

            }
            catch (Exception ex)
            {
                Log("ReadColumnSettings" + ex.Message);
            }
        }

        /// <summary>
        /// Enables or disables exploreToolStripMenuItem
        /// </summary>
        /// <returns>Returns true if cloud_backup_loc setting is NOT IsNullOrWhiteSpace</returns>
        private bool UpdateExploreCloudBackupMenuItem()
        {
            try
            {
                //Let's update exploreToolStripMenuItem.Text
                if (String.IsNullOrWhiteSpace(Ini.GetKeyValue("settings", "cloud_backup_loc")))
                {
                    exploreToolStripMenuItem.Enabled = false;
                }
                else
                {
                    exploreToolStripMenuItem.Enabled = true;
                }
            }
            catch (Exception ex)
            {
                Log("" + ex.Message);
            }
            return exploreToolStripMenuItem.Enabled;
        }

        private System.Drawing.Bitmap Combine(string[] files)
        {
            //read all images into memory
            List<System.Drawing.Bitmap> images = new List<System.Drawing.Bitmap>();
            System.Drawing.Bitmap finalImage = null;

            try
            {
                int image_square_size = 64;

                //CREATE IMAGE THUMBNAILS
                foreach (string image in files)
                {
                    //only add images!!!!!!
                    if (image.Contains(".png") || image.Contains(".jpg"))
                    {
                        try
                        {
                            //create a Bitmap from the file and add it to the list
                            using (var bitmap = new System.Drawing.Bitmap(image))
                            {
                                images.Add(new Bitmap(bitmap.GetThumbnailImage(image_square_size, image_square_size, null, IntPtr.Zero)));
                            }
                        }
                        catch (Exception)
                        {
                            Log("[UpdateTexturePackPreview]:( There is something wrong with an image in the texture pack at location: " + image);
                        }
                    }
                }

                //create a bitmap to hold the combined image
                finalImage = new System.Drawing.Bitmap(1280, 1280);

                //get a graphics object from the image so we can draw on it
                using (System.Drawing.Graphics g = System.Drawing.Graphics.FromImage(finalImage))
                {
                    //set background color
                    g.Clear(System.Drawing.Color.Black);

                    //go through each image and draw it on the final image
                    int x = 0;
                    int y = 0;
                    foreach (System.Drawing.Bitmap image in images)
                    {
                        g.DrawImage(image, x, y);//width not correct
                        if (x < 1281)
                        {
                            x = x + image_square_size;
                        }
                        else
                        {
                            y = y + image_square_size;
                            x = 0;
                        }
                    }
                }
                return finalImage;
            }
            catch (Exception ex)
            {
                if (finalImage != null)
                    finalImage.Dispose();

                throw ex;
            }
            // finally
            //  {
            //clean up memory
            foreach (System.Drawing.Bitmap image in images)
            {
                image.Dispose();
            }
            // }
        }

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

        private bool Check4Minecraft_jar(string dir_path)
        {
            if (File.Exists(dir_path + "\\bin\\minecraft.jar"))
                return true;
            else
            {
                MessageBox.Show("Could not find minecraft.jar @ \r\n" + dir_path + "\\bin\\minecraft.jar");
                return false;
            }
        }

        /// <summary>
        /// Finds the level.dat files and makes sure it has the 
        /// </summary>
        /// <param name="worldfolder"></param>
        private void CheckSavedFolderStructure(string worldfolder)
        {
            try
            {
                DirectoryInfo WorldDir = new DirectoryInfo(worldfolder);
                foreach (FileInfo file in WorldDir.GetFiles("level.dat", System.IO.SearchOption.AllDirectories))
                {
                    if (file.DirectoryName != WorldDir.FullName)
                    {
                        //we need to move this folder to "minecraft_saved"
                        foreach (FileInfo dir_file in file.Directory.GetFiles())
                        {
                            dir_file.MoveTo(WorldDir + "\\" + dir_file.Name);
                        }

                        foreach (DirectoryInfo dir in file.Directory.GetDirectories())
                        {
                            dir.MoveTo(WorldDir + "\\" + dir.Name);
                        }

                        Log("CheckSavedFolderStructure: Had to move " + file.DirectoryName + " to " + WorldDir + "\\");

                    }
                }
            }
            catch (Exception ex)
            {
                Log("CheckSavedFolderStructure: " + ex.Message);
            }
        }

        private bool SavedFolderHasLevel_dat(string worldfolder)
        {
            bool bFound = false;
            try
            {
                DirectoryInfo WorldDir = new DirectoryInfo(worldfolder);
                foreach (FileInfo file in WorldDir.GetFiles())
                {
                    if (file.Name == "level.dat")
                    {
                        //This folder has a level.dat so it is a valid minecraft world
                        if (IsDebugOn())
                            Log("DEBUG SavedFolderHasLevel_dat: " + WorldDir.Name + " is valid Minecraft World; found " + file.FullName);
                        bFound = true;
                        break;
                    }
                }

                if (bFound == false)
                {
                    Log("SavedFolderHasLevel_dat: " + WorldDir.Name + " is not a valid Minecraft World; did not find level.dat file.");
                }
            }
            catch (Exception ex)
            {
                Log("CheckSavedFolderStructure: " + ex.Message);
            }
            return bFound;
        }

        private String IsVersion15(string pathOfTexturepack)
        {
            //is this a zip or folder?
            string response = "";
            bool IsFolder = DirUtils.isDirectory(pathOfTexturepack);
            if (IsFolder)
            {
                //THIS IS A FOLDER/DIRECTORY
                //is if it has some of the main 1.5 elements [.\textures , .\title]
                DirectoryInfo DIRINFO = new DirectoryInfo(pathOfTexturepack);
                foreach (DirectoryInfo folder in DIRINFO.GetDirectories())
                {
                    if (folder.Name == "textures")
                    {
                        response = "[v1.5+]";
                        break;
                    }
                    else if (folder.Name == "assets")
                    {
                        response = "[v1.6+]";
                        break;
                    }
                }

                if (response != "[v1.5+]" && response != "[v1.6+]")
                    response = "[v1.4.x]";
            }
            else
            {
                //THIS IS A FOLDER/DIRECTORY
                //see if zip file then take a look inside
                FileInfo FILEINFO = new FileInfo(pathOfTexturepack);
                if (FILEINFO.FullName.EndsWith(".zip"))
                {
                    //take a look
                    //is if it has some of the main 1.5 elements [.\textures , .\title]

                    //only extract if dir doesn't exist
                    string uniquetempFoldername = System.IO.Path.GetTempPath() + FILEINFO.Name;
                    if (!Directory.Exists(uniquetempFoldername))
                    {
                        using (ZipFile zip = ZipFile.Read(FILEINFO.FullName))
                        {
                            //find the pack.txt and place into temp memory
                            Log("IsVersion15: Extracting temp to " + uniquetempFoldername);
                            zip.ExtractAll(uniquetempFoldername);
                            CleanUpDirs.Add(new DirectoryInfo(uniquetempFoldername));//add to cleanup on close
                        }
                    }
                    else
                    {
                        //Hey this already exists, lets look
                        //is if it has some of the main 1.5 elements [.\textures , .\title]
                        DirectoryInfo DIRINFO = new DirectoryInfo(uniquetempFoldername);
                        foreach (DirectoryInfo folder in DIRINFO.GetDirectories())
                        {
                            if (folder.Name == "textures")
                            {
                                response = "[v1.5+]";
                                break;
                            }
                            else if (folder.Name == "assets")
                            {
                                response = "[v1.6+]";
                                break;
                            }
                        }

                        if (response != "[v1.5+]" && response != "[v1.6+]")
                            response = "[v1.4.x]";
                    }
                }
                else
                {
                    Log("Not a valid zip file to examine: " + pathOfTexturepack);
                }

            }

            return response;
        }

        /// <summary>
        /// Reads the ini path for the "minecraft_saved"
        /// and learns all the world names.
        /// </summary>
        /// <returns>List of Strings</returns>
        private List<String> GetWorldNames()
        {
            List<String> WORLDNAMESLIST = new List<string>();
            //Now list each saved World and then match a rendered image
            DirectoryInfo savedDir = new DirectoryInfo(Ini.GetKeyValue("settings", "minecraft_saved"));
            DirectoryInfo[] subdirectorysaved0 = savedDir.GetDirectories();
            if (subdirectorysaved0.Length > 0)
            {
                foreach (DirectoryInfo world in subdirectorysaved0)
                {
                    WORLDNAMESLIST.Add(world.Name);
                }
            }
            return WORLDNAMESLIST;
        }

        /// <summary>
        /// Reads all the current minecraft texturepacks
        /// Adds each to CurrentTPObjects Dictionary
        /// Extracts zips to temp path (gets deleted when program closing.)
        /// </summary>
        private void RefreshtabCurrentTP()
        {
            try
            {
                Cursor.Current = Cursors.WaitCursor;
                CurrentTPObjects.Clear();
                lboxCurrentTP.BeginUpdate();
                lboxCurrentTP.Items.Clear();
                string currentsettingLoc = "";
                if (radioButtonCurrentTP.Checked)
                    currentsettingLoc = Ini.GetKeyValue("settings", "minecraft_textpacks");
                else
                    currentsettingLoc = Ini.GetKeyValue("settings", "backup_loc_textpacks");

                //get all the zip files
                foreach (string filepath in Directory.GetFiles(currentsettingLoc))
                {
                    Log("Examining " + filepath);
                    FileInfo file = new FileInfo(filepath);
                    if (file.Name.Contains(".zip"))
                    {
                        string uniquetempFoldername = System.IO.Path.GetTempPath() + file.Name;
                        //only extract if dir doesn't exist
                        if (!Directory.Exists(uniquetempFoldername))
                        {
                            using (ZipFile zip = ZipFile.Read(file.FullName))
                            {
                                //find the pack.txt and place into temp memory
                                Log("RefreshtabCurrentTP: Extracting temp to " + uniquetempFoldername);
                                zip.ExtractAll(uniquetempFoldername);
                                CleanUpDirs.Add(new DirectoryInfo(uniquetempFoldername));//add to cleanup on close
                            }
                        }
                        else
                        {
                            //Directory does exist, lets still make sure we clean this up later
                            CleanUpDirs.Add(new DirectoryInfo(uniquetempFoldername));//add to cleanup on close
                        }

                        string text = "";
                        string pack_txt = uniquetempFoldername + "\\pack.txt";
                        if (File.Exists(pack_txt))
                        {
                            //this is a 1.5 and below
                            text = File.ReadAllText(uniquetempFoldername + "\\pack.txt").Trim();
                        }
                        else
                        {
                            if (!File.Exists(uniquetempFoldername + "\\pack.mcmeta"))
                            {
                                //looks like it is missing pack.mcmeta and a pack.txt, it must be a legacy texturepack
                                text = file.Name.Replace(".zip", "");
                            }
                            else
                            {
                                //this is 1.6+ pack.mcmeta
                                text = File.ReadAllText(uniquetempFoldername + "\\pack.mcmeta").Trim();
                                text = stripTPNameFrom(text);
                            }
                        }
                        if (CurrentTPObjects.ContainsKey(text))
                        {
                            //it might be a badly written pack.txt, let's just Log it and let the end user know about it
                            Log("ERROR: Adding texturepack '" + text + "' failed because it was already previously added. If this is really a different texturepack, change the name in 'pack.txt' and try again.");
                        }
                        else
                        {
                            //Is texturepack 1.4.7, 1.5+, 1.6+?
                            List<Object> fileandtype = new List<object>();
                            fileandtype.Add(file);
                            CurrentTPObjects.Add(text.Trim(), fileandtype);
                        }

                    }
                }
                //get all the directories
                string[] alldir = Directory.GetDirectories(currentsettingLoc);
                foreach (string dir in Directory.GetDirectories(currentsettingLoc))
                {
                    DirectoryInfo DIR = new DirectoryInfo(dir);
                    string dirtp_name = "";
                    if (Directory.Exists(DIR.FullName))
                    {
                        if (File.Exists(DIR.FullName + "\\pack.txt"))
                        {
                            dirtp_name = File.ReadAllText(DIR.FullName + "\\pack.txt");
                            dirtp_name = dirtp_name.Trim();
                        }
                        else
                        {
                            //lets just use dir name
                            dirtp_name = DIR.Name;
                        }

                        //make sure no errors when adding
                        if (!CurrentTPObjects.ContainsKey(dirtp_name))
                        {
                            //Is texturepack 1.4.7 or 1.5+?
                            List<Object> fileandtype = new List<object>();
                            fileandtype.Add(DIR);
                            //now add...
                            CurrentTPObjects.Add(dirtp_name, fileandtype);//one is a folder and one is a zip file
                        }
                        else
                        {
                            //lets log this just incase
                            Log("There is a previous texturepack pack.txt with the same name of: " + dirtp_name);
                        }
                    }
                }

                //** Check to see if 1.4.7 texturepack or not
                //Add all to listbox, with the name from pack.txt
                foreach (KeyValuePair<string, List<Object>> pair in CurrentTPObjects)
                {
                    lboxCurrentTP.Items.Add(pair.Key + " " + IsVersion15(Convert.ToString(pair.Value[0])));
                }

                //Close shop...
                lboxCurrentTP.EndUpdate();
                // lboxCurrentTP.Focus();
                lboxCurrentTP.Update();
                if (lboxCurrentTP.Items.Count > 0)
                {
                    groupBoxCurrentView.ForeColor = Color.White;
                    lboxCurrentTP.SelectedIndex = 0;
                }
                else
                    groupBoxCurrentView.ForeColor = DefaultForeColor;

                Cursor.Current = Cursors.Default;
            }
            catch (Exception ex)
            {
                Log("RefreshlboxCurrentTP: " + ex.Message);
            }

        }

        /// <summary>
        /// Get the name from a pack.mcmeta text
        /// </summary>
        /// <param name="chunk"></param>
        private string stripTPNameFrom(string chunk)
        {
            string NAME = "";
            string key = "\"description\":";
            int start_index = chunk.IndexOf(key);
            chunk = chunk.Trim().Substring(start_index, chunk.Length - start_index);
            chunk = chunk.Trim().Replace(key, "").Trim();
            chunk = chunk.Trim().Remove(0, 1);//remove the first quote
            int nextquoteindex = chunk.IndexOf('"');
            NAME = chunk.Trim().Remove(nextquoteindex, chunk.Length - nextquoteindex);
            return NAME;
        }

        /// <summary>
        /// Gets called by lboxCurrentTP_SelectedIndexChanged
        /// Attempts to find selected object in CurrentTPObjects Dictionary
        /// then updates the previews
        /// </summary>
        private void UpdateTexturePackPreview()
        {
            try
            {
                //get the selected item in the list (Is it a DirectoryInfo or FileInfo?)
                List<Object> selectedObj = null;
                string currentDir = "";
                string name = lboxCurrentTP.SelectedItem.ToString().Trim().Replace(" [v1.5+]", "").Replace(" [v1.4.x]", "").Replace(" [v1.6+]", "").Trim();
                CurrentTPObjects.TryGetValue(name, out selectedObj);//the out has one or the other object
                string selecteditem = selectedObj[0].GetType().ToString();//     "System.IO.FileInfo",  "System.IO.DirectoryInfo"
                if (selecteditem == "System.IO.FileInfo")
                {
                    //then we must have previously unzipped this in the temp folder
                    if (Directory.Exists(System.IO.Path.GetTempPath() + ((FileInfo)selectedObj[0]).Name))
                    {
                        //change background color
                        //  tabCurrentTP.BackgroundImage = ;
                        currentDir = System.IO.Path.GetTempPath() + ((FileInfo)selectedObj[0]).Name;
                    }
                }
                else
                {
                    //object if "System.IO.DirectoryInfo"
                    //read from the origin folder
                    currentDir = ((DirectoryInfo)selectedObj[0]).FullName;
                }

                if (!File.Exists(currentDir + "\\gui\\background.png"))
                {
                    //whoa stop. this looks like a 1.6 resourcepack, lets check
                    if (File.Exists(currentDir + "\\assets\\minecraft\\textures\\gui\\options_background.png"))
                    {
                        currentDir = currentDir + "\\assets\\minecraft\\textures";
                    }
                    else
                    {
                        Log("UpdateTexturePackPreview(): Not sure what type of resource or texture pack this is. ??");
                    }

                }

                if (!File.Exists(currentDir + "\\gui\\options_background.png"))
                {
                    using (var tempImg = new Bitmap(currentDir + "\\gui\\background.png"))
                    {
                        tabControl1.TabPages["tabTexturepacks"].BackgroundImage = new Bitmap(tempImg);
                    }
                }
                else
                {
                    using (var tempImg = new Bitmap(currentDir + "\\gui\\options_background.png"))
                    {
                        tabControl1.TabPages["tabTexturepacks"].BackgroundImage = new Bitmap(tempImg);
                    }
                }

                //Look for the best logo
                if (File.Exists(currentDir + "\\gui\\crash_logo.png"))
                {
                    using (var logoImg = new Bitmap(currentDir + "\\gui\\crash_logo.png"))
                    {
                        picBoxlogo.Image = new Bitmap(logoImg);
                    }
                }
                else if (File.Exists(currentDir + "\\gui\\logo.png"))
                {
                    using (var logoImg = new Bitmap(currentDir + "\\gui\\logo.png"))
                    {
                        picBoxlogo.Image = new Bitmap(logoImg);
                    }
                }
                else
                    picBoxlogo.Image = global::MCMyVault.Properties.Resources.crash_logo;

                //look for the world image preview
                if (File.Exists(currentDir + "\\pack.png"))
                {
                    using (var packImg = new Bitmap(currentDir + "\\pack.png"))
                    {
                        picBoxWorldPreview.Image = new Bitmap(packImg);
                    }
                }
                else
                {
                    //lets check for 1.6 location
                    //C:\Users\Joe\AppData\Local\Temp\Ashikabki1Favorites-1_6.zip\assets\minecraft\textures
                    string pack16_Loc = currentDir.Replace("assets\\minecraft\\textures", "");
                    if (File.Exists(pack16_Loc + "\\pack.png"))
                    {
                        using (var packImg = new Bitmap(pack16_Loc + "\\pack.png"))
                        {
                            picBoxWorldPreview.Image = new Bitmap(packImg);
                        }
                    }
                }

                //*** ONLY FOR MC v1.4.x *** look for the items.png *** ONLY FOR MC v1.4.x ***
                if (File.Exists(currentDir + "\\gui\\items.png"))
                {
                    //picBoxItems.Image = System.Drawing.Image.FromFile(currentDir + "\\gui\\items.png");
                    using (var packImg = new Bitmap(currentDir + "\\gui\\items.png"))
                    {
                        //picBoxItems.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
                        picBoxItems.Image = new Bitmap(packImg);
                    }
                }
                else
                {
                    //This must be a v1.5 or above so combine all
                    //get all the files in a directory
                    List<String> files = new List<string>();
                    //might be 1.6+
                    if (Directory.Exists(currentDir + "\\textures\\items"))
                    {
                        files = new List<string>(Directory.GetFiles(currentDir + "\\textures\\items"));
                        files.AddRange(Directory.GetFiles(currentDir + "\\textures\\blocks"));
                    }
                    else
                    {
                        //1.6
                        files = new List<string>(Directory.GetFiles(currentDir + "\\items"));
                        files.AddRange(Directory.GetFiles(currentDir + "\\blocks"));
                    }


                    CleanUpDirs.Add(new DirectoryInfo(currentDir));

                    string GoodNameForSaving = Regex.Replace(name, "[\\/?:*\"\"><|]+, ", "", RegexOptions.Compiled);
                    GoodNameForSaving = GoodNameForSaving.Replace("\r\n", "").Replace("\"", "-").Replace(":", "");

                    //only do this part if necessary (time consuming)
                    if (!File.Exists(currentDir + "\\mcmyvault-" + GoodNameForSaving + ".jpg"))
                    {
                        //combine them into one image
                        System.Drawing.Bitmap stitchedImage = Combine(files.ToArray());//Create the colliage
                        //save the new image
                        stitchedImage.Save(currentDir + "\\mcmyvault-" + GoodNameForSaving + ".jpg", System.Drawing.Imaging.ImageFormat.Jpeg);
                    }

                    using (var packImg = new Bitmap(currentDir + "\\mcmyvault-" + GoodNameForSaving + ".jpg"))
                    {
                        // picBoxItems.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
                        picBoxItems.Image = new Bitmap(packImg);
                    }
                }
            }
            catch (Exception ex)
            {
                Log("UpdateTexturePackPreview: " + ex.Message);
            }
        }

        private void cboxProfilesRefresh()
        {
            try
            {
                int cur_index = 0;
                int x = 0;
                string startconfigfullpath = Common.ConfigIniFile;
                IniFile Startupini = new IniFile();
                //save this as the new currentconfig
                if (File.Exists(Common.StartupConfigIniFile))
                {
                    Startupini.Load(Common.StartupConfigIniFile);
                    startconfigfullpath = Common.AppDataPathName + "\\" + Startupini.GetKeyValue("settings", "currentconfig");
                }

                FileInfo startupfilename = new FileInfo(startconfigfullpath);
                List<FileInfo> FilesList = new List<FileInfo>();
                foreach (string filepath in Directory.GetFiles(Common.AppDataPathName))
                {
                    FileInfo file = new FileInfo(filepath);
                    if (file.Name.ToLower().EndsWith(".ini") && file.Name.ToLower() != "startupconfig.ini")
                    {
                        //this is an ini file
                        cboxProfiles.Items.Add(file.Name);
                        if (startupfilename.Name == file.Name)
                            cur_index = x;
                        x = x + 1;
                    }
                }
                cboxProfiles.SelectedIndex = cur_index;
                UpdateLauncherpBox();
            }
            catch (Exception ex)
            {
                Log("cboxProfilesRefresh: " + ex.Message);
            }
        }

        private void UpdateLauncherpBox()
        {
            try
            {
                Image image = Icon.ExtractAssociatedIcon(Ini.GetKeyValue("settings", "minecraft")).ToBitmap();
                pBoxProfile.Image = image;
                pBoxProfile.Update();
            }
            catch (Exception ex)
            {
                Log("UpdateLauncherpBox: " + ex.Message);
            }
        }

        /// <summary>
        /// Populates and re-populates the dataGridView1.Rows
        /// list each saved World and then match a rendered image
        /// </summary>
        private void LoadSavedWorlds()
        {
            //  dataGridView1.BeginUpdate();
            dataGridView1.Rows.Clear();
            ImageListWorlds.Images.Clear();
            //the settings should be ok...
            DirectoryInfo savedDir = new DirectoryInfo(Ini.GetKeyValue("settings", "minecraft_saved"));
            DirectoryInfo mcmap2Dir = new DirectoryInfo(Ini.GetKeyValue("settings", "mcmapDZ_renders"));

            Dictionary<String, FileInfo> MapImageDict = new Dictionary<string, FileInfo>();

            //GET THE CURRENT WORLDS
            foreach (string worldname in GetWorldNames())
            {
                MapImageDict.Add(worldname, null);
            }

            if (Directory.Exists(mcmap2Dir.FullName))
            {

                //foreach rendered world, get one image at location 
                // C:\Users\Joe\Documents\MCMyVaultMC161\mcmap2 etc
                List<FileInfo> FileEntries = new List<FileInfo>();
                foreach (FileInfo item in mcmap2Dir.GetFiles())
                {
                    if (item.Extension.ToLower().EndsWith("png"))
                    {
                        FileEntries.Add(item);
                    }
                }

                if (FileEntries.Count > 0)
                {
                    foreach (string worldname in GetWorldNames())
                    {
                        //So some files exists, lets match to the worldlist name
                        string current_map_file = mcmap2Dir + "\\" + worldname + ".png";
                        if (File.Exists(current_map_file))
                        {
                            MapImageDict[worldname] = new FileInfo(current_map_file);

                            Image img = Image.FromFile(current_map_file);
                            img.Tag = worldname;
                            ImageListWorlds.Images.Add(worldname, img);
                        }
                    }
                }
                else
                {
                    //No subdirectories found in renders, ask user if they wish to render all their worlds....

                    DialogResult dResult = MessageBox.Show("Can't display the maps of your worlds.\r\nDo you want to auto render now using mcmap2?\r\n(this may take a few minutes depending upon how many worlds you have)", "Create images of the map for all Worlds using mcmapDZ?", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Information);
                    if (dResult == DialogResult.Yes)
                    {
                        //ok, send the commandline to mcmap2
                        //lets try to open 
                        string mcmap_exe = Ini.GetKeyValue("settings", "mcmapDZ_renders") + "\\mcmap.exe";
                        //foreach world
                        //Now list each saved World and then match a rendered image
                        DirectoryInfo[] subdirectorysaved0 = savedDir.GetDirectories();
                        if (subdirectorysaved0.Length > 0)
                        {
                            foreach (DirectoryInfo world in subdirectorysaved0)
                            {
                                // Use ProcessStartInfo class
                                ProcessStartInfo startInfo = new ProcessStartInfo();
                                startInfo.CreateNoWindow = false;
                                startInfo.UseShellExecute = false;
                                startInfo.WorkingDirectory = Ini.GetKeyValue("settings", "mcmapDZ_renders");
                                startInfo.FileName = mcmap_exe;
                                //startInfo.WindowStyle = ProcessWindowStyle.Hidden;
                                startInfo.Arguments = "-day -skylight -file" + " \"" + world.Name + ".png\" \"" + world.FullName + "\"";
                                Log("Auto-mcmapDZ executing: mcmap2 " + startInfo.Arguments);
                                // Start the process with the info we specified.
                                // Call WaitForExit and then the using statement will close.
                                using (Process exeProcess = Process.Start(startInfo))
                                {
                                    exeProcess.WaitForExit();
                                }

                                //assign this image now
                                string tempfilename = Environment.GetFolderPath(Environment.SpecialFolder.InternetCache) + "\\" + DateTime.Now.Ticks.ToString() + ".png";
                                Thread.Sleep(10);
                                File.Copy(Ini.GetKeyValue("settings", "mcmapDZ_renders") + "\\" + world.Name + ".png", tempfilename);
                                if (File.Exists(tempfilename))
                                {
                                    Image img = Image.FromFile(tempfilename);
                                    img.Tag = world.Name;
                                    ImageListWorlds.Images.Add(world.Name, img);
                                }
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

                //let's get the largest sizes
                int larget_height = 128;//start out with our default size
                int larget_width = 128;
                //foreach (Image image in ImageListWorlds.Images)
                //{
                //    //lets sum all first
                //    if(image.Height > larget_height)
                //        larget_height = image.Height;
                //    if(image.Width > larget_width)
                //        larget_width = image.Width;
                //}
                ////Now fix b/c no larger than 256
                //if (larget_height > 256)
                //    larget_height = 256;
                //if (larget_width > 256)
                //    larget_width = 256;

                ImageListWorlds.ImageSize = new Size(larget_width, larget_height);

                if (Directory.Exists(savedDir.FullName))
                {
                    //Now list each saved World and then match a rendered image
                    DirectoryInfo[] subdirectorysaved = savedDir.GetDirectories();
                    if (subdirectorysaved.Length > 0)
                    {
                        int i = 0;
                        foreach (DirectoryInfo world in subdirectorysaved)
                        {
                            if (SavedFolderHasLevel_dat(world.FullName))
                            {
                                FileInfo file = null;
                                string lastbackedupdate = FindLastBackup(world.Name, out file);
                                DateTime lastbackedupdate_DT = new DateTime();
                                if (DateTime.TryParse(lastbackedupdate, out lastbackedupdate_DT))
                                { }
                                else
                                {
                                    Log("Cannot DateTime.TryParse(lastbackedupdate)= " + lastbackedupdate + "; for world: " + world.Name + ". Try renaming your Minecraft saved world folder to the real name of the file world name. OR there is simply no existing backup for this world.");
                                }
                                if (file != null)
                                {
                                    //dataGridView1.Items.Add(new ListViewItem(new string[] { world.Name, lastbackedupdate, file.Name }, world.Name));
                                    if ((ImageListWorlds.Images[world.Name] != null))
                                        dataGridView1.Rows.Add(ImageListWorlds.Images[world.Name].GetThumbnailImage(128, 128, null, IntPtr.Zero), world.Name, lastbackedupdate_DT, file.Name);
                                    else
                                    {
                                        //This saved folder world has no render image, so give it the tmep
                                        //no render dir so lets give it a temp
                                        Image img = MCMyVault.Properties.Resources.noworldmap.GetThumbnailImage(128, 128, null, IntPtr.Zero);
                                        img.Tag = world.Name.Clone();
                                        dataGridView1.Rows.Add(img, world.Name, lastbackedupdate_DT, file.Name);
                                    }

                                    //lets add the file path to Tag for later use
                                    dataGridView1.CurrentRow.Tag = file.FullName.Clone();

                                }
                                else
                                {
                                    if ((ImageListWorlds.Images[world.Name] != null))
                                        dataGridView1.Rows.Add(ImageListWorlds.Images[world.Name].GetThumbnailImage(128, 128, null, IntPtr.Zero), world.Name, lastbackedupdate_DT, "no backup found");
                                    else
                                    {
                                        //there was no backup found so probably no render yet either for image; display temp
                                        //no render dir so lets give it a temp
                                        Image img = MCMyVault.Properties.Resources.noworldmap.GetThumbnailImage(128, 128, null, IntPtr.Zero);
                                        img.Tag = world.Name;
                                        dataGridView1.Rows.Add(img, world.Name, lastbackedupdate_DT, "no backup found");
                                    }
                                    //lets add the file path to Tag for later use
                                    dataGridView1.CurrentRow.Tag = "no backup found";

                                }
                            }
                            i = i + 1;
                        }
                    }
                }
                else
                {
                    //guess there is no \saved folder yet, which is A O K
                }

                //dataGridView1.Columns["ColumnLastBackup"].ValueType = typeof(DateTime);
                //dataGridView1.Columns["ColumnLastBackup"].DefaultCellStyle.Format = "MMddyyyyHHmmss";

                dataGridView1.Sort(dataGridView1.Columns["ColumnLastBackup"], ListSortDirection.Descending);
                //Get the Column settings
                ReadColumnSettings();
                // dataGridView1.EndUpdate();
                dataGridView1.Update();
            }
            else
            {
                DialogResult dResulttp = MessageBox.Show("Path to world images is no longer valid (mcmapDZ has been retired for use with MCMyVault since MCv1.7.4).\r\nDo you want to use the default mcmap2 path?", "mcmap path no longer valid", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                if (dResulttp == DialogResult.No)
                {
                    Ini.SetKeyValue("settings", "mcmapDZ_renders", Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\MCMyVault\\mcmap2");//we need a value or it keeps prompting us.
                    Ini.Save(Common.ConfigIniFile);//save right away!!
                }
                else if (dResulttp == DialogResult.Yes)
                {
                    string mcmapDZ_path = BrowseToFolder(Environment.SpecialFolder.MyDocuments);
                    //          txtbMCMAPDZLoc.Text = mcmapDZ_path;
                    Ini.SetKeyValue("settings", "mcmapDZ_renders", mcmapDZ_path);
                    Ini.Save(Common.ConfigIniFile);//save right away!!
                }

                //
                //txtbMCMAPDZLoc.Text = mcmapDZ_path;
                //Ini.SetKeyValue("settings", "mcmapDZ_renders", mcmapDZ_path);
                //Ini.Save(Common.ConfigIniFile);//save right away!!
            }
        }
        #endregion Misc

        #region Events
        private void DoViewEihort()
        {
            try
            {
                //lets try to open 
                FileInfo eihort_exe = new FileInfo(Ini.GetKeyValue("settings", "eihort"));
                string world_to_open = "\"" + Ini.GetSection("settings").GetKey("minecraft_saved").GetValue().Trim() + "\\" + dataGridView1.CurrentRow.Cells["ColumnName"].Value.ToString() + "\"";

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
                string mcmapDZGUI_exe = Ini.GetKeyValue("settings", "mcmapDZ_renders") + "\\mcmapGUI2.exe";

                // Use ProcessStartInfo class
                ProcessStartInfo startInfo = new ProcessStartInfo();
                startInfo.WorkingDirectory = Ini.GetKeyValue("settings", "mcmapDZ_renders");
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

        private void txtBoxSnapshotsLoc_TextChanged(object sender, EventArgs e)
        {
            bSave.Enabled = true;
        }

        private void txtTexturepackbackups_TextChanged(object sender, EventArgs e)
        {
            bSave.Enabled = true;
        }

        private void txtbMCtexturepacks_TextChanged(object sender, EventArgs e)
        {
            bSave.Enabled = true;
        }

        private void tabMain_Enter(object sender, EventArgs e)
        {
            //Refresh but only if it is not the first Enter
            if (!bOnFirstEnter)
                LoadSavedWorlds();
        }

        private void tabBackups_Enter(object sender, EventArgs e)
        {
            listBoxBackups.BeginUpdate();
            listBoxBackups.Items.Clear();
            try
            {
                foreach (string filepath in Directory.GetFiles(Ini.GetKeyValue("settings", "backup_loc")))
                {
                    if (File.Exists(filepath))
                    {
                        FileInfo file = new FileInfo(filepath);
                        listBoxBackups.Items.Add(file);
                    }
                    else
                        MessageBox.Show("Can't locate backup file: " + Environment.NewLine + filepath + Environment.NewLine + "Please check your backup_loc setting on the Settings tab and restart MCMyVault.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Can't locate backup file." + Environment.NewLine + "Please check your backup_loc setting on the Settings tab and restart MCMyVault.");
                Log("tabBackups_Enter: Can't locate backup file. Please check your backup_loc setting on the Settings tab and restart MCMyVault.");
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
                        //Let's update purgekeepXDaysForYToolStripMenuItem.Text FIRST!
                        //get the first part of the text
                        int iForIndex = purgekeepXDaysForYToolStripMenuItem.Text.IndexOf("for ") + 4;
                        string prefix = purgekeepXDaysForYToolStripMenuItem.Text.Substring(0, iForIndex);
                        purgekeepXDaysForYToolStripMenuItem.Text = prefix + ((FileInfo)listBoxBackups.SelectedItem).Name.Split('_')[0];

                        //Let's update sendCopyToCloudStorageToolStripMenuItem.Text FIRST!
                        if (String.IsNullOrWhiteSpace(Ini.GetKeyValue("settings", "cloud_backup_loc")))
                            sendCopyToCloudStorageToolStripMenuItem.Enabled = false;
                        else
                            sendCopyToCloudStorageToolStripMenuItem.Enabled = true;
                        UpdateExploreCloudBackupMenuItem();
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

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            //need to reset this or it right-clicks everywhere with wrong contextmenu
            this.ContextMenuStrip = null;
            if (tabControl1.SelectedTab == tabControl1.TabPages["tabTexturepacks"])
            {
                tabControl1.SelectedTab.Focus();
                this.Update();
                RefreshtabCurrentTP();
            }
            else if (tabControl1.SelectedTab == tabControl1.TabPages["tabPageCustoms"])
            {

            }
            else if (tabControl1.SelectedTab == tabControl1.TabPages["tabMain"])
            {
                UpdateLauncherpBox();
            }
            else if (tabControl1.SelectedTab == tabControl1.TabPages["tabPageConfigs"])
            {
                tabControl1.SelectedTab.Focus();
                this.Update();
                listBoxConfigs.Items.Clear();
                string config_folder = Ini.GetKeyValue("settings", "minecraft_saved").Replace("\\saves", "\\config");

                if (Directory.Exists(config_folder))
                {
                    //Get the main config folder
                    foreach (string filepath in Directory.GetFiles(config_folder))
                    {
                        //make sure only .cfg
                        if (filepath.EndsWith(".cfg"))
                        {
                            FileInfo file2 = new FileInfo(filepath);
                            listBoxConfigs.Items.Add(file2.Name);
                        }
                    }

                    //Get the subfolders
                    foreach (string filepath in Directory.GetDirectories(config_folder))
                    {
                        DirectoryInfo subDir = new DirectoryInfo(filepath);

                        foreach (string subfilepath in Directory.GetFiles(subDir.FullName))
                        {
                            //make sure only .cfg
                            if (subfilepath.EndsWith(".cfg"))
                            {
                                FileInfo file3 = new FileInfo(subfilepath);
                                listBoxConfigs.Items.Add(subDir.Name + "->" + file3.Name);
                            }
                        }
                    }

                    //if C:\Users\%USER%\eihort.config exists then add to the list
                    string usr_eihortconfig = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile) + "\\eihort.config";
                    if (File.Exists(usr_eihortconfig))
                    {
                        listBoxConfigs.Items.Add((new FileInfo(usr_eihortconfig)).Name);
                    }
                }
                else
                {
                    //guess there is no \config folder yet, which is A O K
                }
            }
        }

        private void exploreMinecraftTexturepacksLocationsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                System.Diagnostics.Process.Start(Ini.GetKeyValue("settings", "minecraft_textpacks"));
            }
            catch (Exception ex)
            {
                Log(ex.Message);
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void radioButtonCurrentTP_CheckedChanged(object sender, EventArgs e)
        {
            RadioButton rb = sender as RadioButton;
            if (rb != null)
            {
                if (rb.Checked)
                {
                    // Only one radio button will be checked
                    radioButtonCurrentTP.Checked = true;
                    radioButtonInactiveTP.Checked = false;
                    tabControl1.TabPages["tabTexturepacks"].BackgroundImage = null;
                    picBoxlogo.Image = null;
                    picBoxWorldPreview.Image = null;
                    picBoxItems.Image = null;
                    RefreshtabCurrentTP();
                }
            }

        }

        private void radioButtonInactiveTP_CheckedChanged(object sender, EventArgs e)
        {
            RadioButton rb = sender as RadioButton;
            if (rb != null)
            {
                if (rb.Checked)
                {
                    // Only one radio button will be checked
                    radioButtonCurrentTP.Checked = false;
                    radioButtonInactiveTP.Checked = true;
                    tabControl1.TabPages["tabTexturepacks"].BackgroundImage = null;
                    picBoxlogo.Image = null;
                    picBoxWorldPreview.Image = null;
                    picBoxItems.Image = null;
                    RefreshtabCurrentTP();
                }
            }
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                //unlock any images
                //tabControl1.SelectedTab = tabControl1.TabPages["tabMain"];
                //this.Update();
                //Thread.Sleep(1000);
                //Temp cleanup
                foreach (DirectoryInfo dir in CleanUpDirs)
                {
                    //just make sure it exists because CleanUpDirs might have some duplicates
                    if (Directory.Exists(dir.FullName))
                    {
                        Log("Form1_FormClosing; Cleaning up..deleting " + dir.FullName);
                        dir.Delete(true);
                    }
                }

                //Save Column widths
                Ini.Load(Common.ConfigIniFile);
                Ini.SetKeyValue("settings", "map_col_width", Convert.ToString(dataGridView1.Columns["ColumnMap"].Width));
                Ini.SetKeyValue("settings", "name_col_width", Convert.ToString(dataGridView1.Columns["ColumnName"].Width));
                Ini.SetKeyValue("settings", "lastbackup_col_width", Convert.ToString(dataGridView1.Columns["ColumnLastBackup"].Width));
                Ini.SetKeyValue("settings", "filename_col_width", Convert.ToString(dataGridView1.Columns["ColumnFilename"].Width));
                Ini.Save(Common.ConfigIniFile);
            }
            catch (Exception ex)
            {
                Log(ex.Message);
            }
        }

        private void lboxCurrentTP_MouseDown(object sender, MouseEventArgs e)
        {
            try
            {
                //Copy the node selected...
                #region If RIGHT-CLICK...
                //If RIGHT-CLICK...
                if (e.Button == MouseButtons.Right)
                {
                    if (((ListBox)sender) != null)
                    {
                        //now show RIGHTCLICK MENU...
                        if (radioButtonCurrentTP.Checked)
                        {
                            restoreToolStripMenuItem.Enabled = false;
                            backupToolStripMenuItem1.Enabled = true;
                        }
                        else
                        {
                            //looking at backups
                            restoreToolStripMenuItem.Enabled = true;
                            backupToolStripMenuItem1.Enabled = false;
                        }
                        this.ContextMenuStrip = contextMenuStripTP;
                    }
                }
                #endregion If RIGHT-CLICK...
            }
            catch (Exception ex)
            {
                MessageBox.Show("lboxCurrentTP_MouseDown ERROR: " + ex.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {
            try
            {
                string value = Ini.GetKeyValue("settings", "cleanupdays");
                string nowvalue = Convert.ToString(numericUpDown1.Value);
                if (nowvalue == "1")
                    purgekeepXDaysForYToolStripMenuItem.Text = "Cleanup last " + nowvalue + " day for Y";
                else
                    purgekeepXDaysForYToolStripMenuItem.Text = "Cleanup last " + nowvalue + " days for Y";

                if (nowvalue != value)
                    bSave.Enabled = true;
            }
            catch (Exception ex)
            {
                Log(ex.Message);
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void lboxCurrentTP_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                //Refresh all the Textbox previews
                UpdateTexturePackPreview();
            }
            catch (Exception ex)
            {
                Log("lboxCurrentTP_SelectedIndexChanged: " + ex.Message);
            }
        }

        private void txtBoxCloudBackup_TextChanged(object sender, EventArgs e)
        {
            bSave.Enabled = true;
        }

        private void sendCopyToCloudStorageToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                string cloud_backup_loc = Ini.GetKeyValue("settings", "cloud_backup_loc");
                foreach (var item in listBoxBackups.SelectedItems)
                {
                    if (item.GetType().ToString() == "System.IO.FileInfo")
                    {
                        string newfilename = cloud_backup_loc + "\\" + ((FileInfo)listBoxBackups.SelectedItem).Name;
                        if (File.Exists(newfilename))
                        {
                            DialogResult dResult = MessageBox.Show("File already exists. Do you wish to overwrite:\r\n" + newfilename, "OVERWITE?", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Warning);
                            if (dResult == DialogResult.Yes)
                            {
                                File.Copy(((FileInfo)listBoxBackups.SelectedItem).FullName, newfilename, true);
                                Log("sendCopyToCloudStorage: Copied file= " + ((FileInfo)listBoxBackups.SelectedItem).FullName);
                                ShowMsgBoxGenericCompleted("Copy To Cloud Storage");
                            }
                            else if (dResult == DialogResult.No)
                            {

                            }
                            else if (dResult == DialogResult.Cancel)
                            {
                                ShowMsgBoxGenericCompleted("Cancelled");
                            }
                        }
                        else
                        {
                            File.Copy(((FileInfo)listBoxBackups.SelectedItem).FullName, newfilename);
                            Log("sendCopyToCloudStorage: Copied file= " + ((FileInfo)listBoxBackups.SelectedItem).FullName);
                        }
                    }
                    else
                    {
                        MessageBox.Show("Backup is not a zip file. Try making a zip file first before copying to cloud storage.", "Backup is not a zip file", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            catch (Exception ex)
            {
                Log(ex.Message);
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            OpenRenderingForSelected();
        }

        private void dataGridView1_MouseDown(object sender, MouseEventArgs e)
        {
            try
            {
                //Copy the node selected...
                //selectedTreeNode = (TreeNode)(treeView1.GetNodeAt(e.X, e.Y).Clone());
                #region If RIGHT-CLICK...
                //If RIGHT-CLICK...
                if (e.Button == MouseButtons.Right)
                {
                    //if (((DataGridView)sender).CurrentCell.Value.ToString() != null)
                    //we must select this row!
                    Point grvScreenLocation = dataGridView1.PointToScreen(dataGridView1.Location);
                    int tempX = DataGridView.MousePosition.X - grvScreenLocation.X + dataGridView1.Left;
                    int tempY = DataGridView.MousePosition.Y - grvScreenLocation.Y + dataGridView1.Top;
                    DataGridView.HitTestInfo hit = dataGridView1.HitTest(tempX, tempY);
                    if (hit != null && hit.RowIndex >= 0)
                    {
                        this.tabMain.Focus();
                        dataGridView1.Rows[hit.RowIndex].Selected = true;
                        this.tabMain.Focus();
                        this.tabMain.Update();
                    }

                    //now show RIGHTCLICK MENU...
                    this.ContextMenuStrip = contextMenuStrip1;

                }
                #endregion If RIGHT-CLICK...
            }
            catch (Exception ex)
            {
                MessageBox.Show("treeView1_MouseDown ERROR: " + ex.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Form1_SizeChanged(object sender, EventArgs e)
        {
            foreach (DataGridViewColumn col in dataGridView1.Columns)
            {
                if (this.WindowState == FormWindowState.Normal)
                {
                    // Console.WriteLine("FormWindowState.Normal");
                    col.DefaultCellStyle.Font = new System.Drawing.Font("Consolas", 12.0F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                    //dataGridView1.RowTemplate.Height = 65;
                }
                if (this.WindowState == FormWindowState.Maximized)
                {
                    // Console.WriteLine("FormWindowState.Maximized");
                    col.DefaultCellStyle.Font = new System.Drawing.Font("Consolas", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                    // dataGridView1.RowTemplate.Height = 129;
                }
            }

            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                if (this.WindowState == FormWindowState.Normal)
                {
                    row.Cells["ColumnMap"].Value = ((Bitmap)row.Cells["ColumnMap"].Value).GetThumbnailImage(64, 64, null, IntPtr.Zero);
                }
                if (this.WindowState == FormWindowState.Maximized)
                {
                    row.Cells["ColumnMap"].Value = ((Bitmap)row.Cells["ColumnMap"].Value).GetThumbnailImage(128, 128, null, IntPtr.Zero);
                }
            }
        }

        private void dataGridView1_MouseHover(object sender, EventArgs e)
        {
            try
            {
                //Select the row on mouseover
                //This still doesn't work all the time 
                Point grvScreenLocation = dataGridView1.PointToScreen(dataGridView1.Location);
                int tempX = DataGridView.MousePosition.X - grvScreenLocation.X + dataGridView1.Left;
                int tempY = DataGridView.MousePosition.Y - grvScreenLocation.Y + dataGridView1.Top;
                DataGridView.HitTestInfo hit = dataGridView1.HitTest(tempX, tempY);
                if (hit != null && hit.RowIndex >= 0)
                {
                    this.tabMain.Focus();
                    dataGridView1.Rows[hit.RowIndex].Selected = true;
                    this.tabMain.Focus();
                    this.tabMain.Update();
                }
            }
            catch (Exception ex)
            {
                Log("dataGridView1_RowEnter: " + ex.Message);
            }
        }

        private void exploreToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                System.Diagnostics.Process.Start(Ini.GetKeyValue("settings", "cloud_backup_loc"));
            }
            catch (Exception ex)
            {
                Log(ex.Message);
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
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
                System.Diagnostics.Process.Start("https://github.com/joesox/mcmyvault/wiki");
            }
            catch (Exception ex)
            {
                Log(ex.Message);
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void readMeFileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                System.Diagnostics.Process.Start(Common.ReadMe);
            }
            catch (Exception ex)
            {
                Log(ex.Message);
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void exploreMinecraftModFolderToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                System.Diagnostics.Process.Start(Ini.GetKeyValue("settings", "minecraft_saved").Replace("\\saves", "\\mods"));
            }
            catch (Exception ex)
            {
                Log(ex.Message);
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void tabSnapshots_Enter(object sender, EventArgs e)
        {
            listBoxSnapshots.BeginUpdate();
            listBoxSnapshots.Items.Clear();
            if (Directory.Exists(Ini.GetKeyValue("settings", "snapshots_loc")))
            {
                foreach (string filepath in Directory.GetFiles(Ini.GetKeyValue("settings", "snapshots_loc")))
                {
                    FileInfo file = new FileInfo(filepath);
                    listBoxSnapshots.Items.Add(file);

                }
                listBoxSnapshots.EndUpdate();
                listBoxSnapshots.Update();
            }
            else
            {
                MessageBox.Show("Bad snapshot directory at \r\n" + Ini.GetKeyValue("settings", "snapshots_loc") + "\r\nEither create it or change the settings for it.");
                Log("[tabSnapshots_Enter]: Where is the snapshot location? Not found: " + Ini.GetKeyValue("settings", "snapshots_loc"));
            }
        }

        private void listBoxSnapshots_MouseDown(object sender, MouseEventArgs e)
        {
            try
            {
                //Copy the node selected...
                //selectedTreeNode = (TreeNode)(treeView1.GetNodeAt(e.X, e.Y).Clone());
                #region If RIGHT-CLICK...
                //If RIGHT-CLICK...
                if (e.Button == MouseButtons.Right)
                {
                    if (listBoxSnapshots.SelectedIndex >= 0)
                    {
                        ////now show RIGHTCLICK MENU...
                        this.ContextMenuStrip = contextMenuStrip3;
                    }
                }
                #endregion If RIGHT-CLICK...
            }
            catch (Exception ex)
            {
                MessageBox.Show("treeView1_MouseDown ERROR: " + ex.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnBrowse2SnapshotsLoc_Click(object sender, EventArgs e)
        {
            String snapshots_path = BrowseToFolder(Environment.SpecialFolder.UserProfile);
            if (!String.IsNullOrEmpty(snapshots_path))
            {
                //         txtBoxSnapshotsLoc.Text = snapshots_path;
                Ini.SetKeyValue("settings", "snapshots_loc", snapshots_path);
                bSave.Enabled = true;
            }
        }

        private void toolStripMenuRestoreSnap_Click(object sender, EventArgs e)
        {
            //Prompt the user ARE YOU SURE? TAKE A SNAPSHOT FIRST BEFORE RESTORING!
            FileInfo file = (FileInfo)listBoxSnapshots.SelectedItems[0];
            string backupfilePath = file.FullName;
            DialogResult dResult = MessageBox.Show("Are you sure you want to RESTORE " + file.Name + "\r\n SNAPSHOT? (this will DELETE your current .minecraft folder)\r\n" + backupfilePath + "?\r\n(Take a snapshot now just in case, you have been warned)", "RESTORE SNAPSHOT " + file.Name, MessageBoxButtons.YesNoCancel, MessageBoxIcon.Warning);
            if (dResult == DialogResult.Yes)
            {
                //first Delete the .minecraft dir
                DirectoryInfo MCDir = new DirectoryInfo(Ini.GetSection("settings").GetKey("minecraft_saved").GetValue().Replace("\\saves", ""));
                Cursor.Current = Cursors.WaitCursor;
                if (Directory.Exists(MCDir.FullName))
                {
                    Directory.Delete(MCDir.FullName, true);
                    //now recreate it from the zip file
                    Directory.CreateDirectory(MCDir.FullName);
                    ZipFile backupZip = new ZipFile(backupfilePath);
                    backupZip.ExtractAll(MCDir.FullName);
                    //CheckSavedFolderStructure
                    Check4Minecraft_jar(MCDir.FullName);
                    Cursor.Current = Cursors.Default;
                    //update
                    ShowMsgBoxGenericCompleted(file.Name + " restored");
                    LoadSavedWorlds();
                }
                else
                {
                    //Might be a new world so create directory
                    //now recreate it from the zip file
                    Directory.CreateDirectory(MCDir.FullName);
                    ZipFile backupZip = new ZipFile(backupfilePath);
                    backupZip.ExtractAll(MCDir.FullName);
                    //CheckSavedFolderStructure
                    Check4Minecraft_jar(MCDir.FullName);
                    Cursor.Current = Cursors.Default;
                    //update
                    ShowMsgBoxGenericCompleted(file.Name + " restored");
                    LoadSavedWorlds();

                    // Log("restoreSelectedBackupFileToolStripMenuItem_Click: could not find " + worldDir.FullName);
                    // MessageBox.Show("Sorry, could not find " + worldDir.FullName +"\r\nRemeber the backup file must have the following format:\r\n <WORLDNAME>_<anythingelse>.zip" );
                }
            }
            else if (dResult == DialogResult.No)
            {

            }
            else if (dResult == DialogResult.Cancel)
            {

            }

            //If yes then delete current .minecraft

            //Unzip the selected file to .minecraft (check to see if there is a minecraft.jar)
        }

        private void btnCreateSnapshot_Click(object sender, EventArgs e)
        {
            //(check to see if there is a minecraft.jar)
            // if FALSE tell user, 
            try
            {
                Cursor.Current = Cursors.WaitCursor;
                DirectoryInfo worldDir = new DirectoryInfo(Ini.GetSection("settings").GetKey("minecraft_saved").GetValue().Replace("\\saves", ""));
                string uniqueFilename = worldDir.Name + "_" + DateTime.Now.ToString("MMddyyyyHHmmss") + ".zip";
                //Only backup the textpack we selected
                CreateZipFile(Ini.GetKeyValue("settings", "snapshots_loc") + "\\" + uniqueFilename, worldDir.FullName);
                Log("btnCreateSnapshot_Click(): " + Ini.GetKeyValue("settings", "snapshots_loc") + "\\" + uniqueFilename);
                tabSnapshots_Enter(null, null);//refresh view
                Cursor.Current = Cursors.Default;
                ShowMsgBoxGenericCompleted("Snapshot");
            }
            catch (Exception ex)
            {
                Log("[btnCreateSnapshot_Click]: " + ex.Message);
            }
        }

        private void toolStripMenuRenamesnap_Click(object sender, EventArgs e)
        {
            try
            {
                FileInfo file = new FileInfo(((FileInfo)listBoxSnapshots.SelectedItems[0]).FullName);
                RenameForm newf = new RenameForm(file.Name);
                DialogResult r = newf.ShowDialog();
                if (r == System.Windows.Forms.DialogResult.OK)
                {
                    string fullnew = file.FullName.Replace(file.Name, newf.NewName);
                    System.IO.File.Move(file.FullName, fullnew);
                    ShowMsgBoxGenericCompleted("Rename");
                    //Now reload the listview
                    tabSnapshots_Enter(null, null);
                }
                else
                    MessageBox.Show("Rename cancelled.");
            }
            catch (Exception ex)
            {
                Log("[toolStripMenuRenamesnap_Click]: " + ex.Message);
            }
        }

        private void toolStripMenuDeleteSnap_Click(object sender, EventArgs e)
        {
            try
            {
                try
                {
                    DialogResult dResult = MessageBox.Show("Are you sure you want to DELETE?", "DELETE SNAPSHOT ", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Warning);
                    if (dResult == DialogResult.Yes)
                    {
                        DirectoryInfo snapshotsDir = new DirectoryInfo(Ini.GetSection("settings").GetKey("snapshots_loc").GetValue());
                        if (Directory.Exists(snapshotsDir.FullName))
                        {
                            List<FileInfo> FilesList = new List<FileInfo>();
                            foreach (FileInfo item in listBoxSnapshots.SelectedItems)
                            {
                                FilesList.Add(item);
                            }

                            foreach (FileInfo f in FilesList)
                            {
                                //File.Delete(f.FullName);
                                FileSystem.DeleteFile(f.FullName, UIOption.OnlyErrorDialogs, RecycleOption.SendToRecycleBin);
                                listBoxSnapshots.Items.Remove(f);
                            }
                            listBoxSnapshots.Update();
                            ShowMsgBoxGenericCompleted("Deleted");
                        }
                        else
                        {
                            Log("deleteToolStripMenuItem_Click: could not find " + snapshotsDir.FullName);
                            MessageBox.Show("Sorry, could not find " + snapshotsDir.FullName);
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

        private void linkLabelopenconfig_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            try
            {
                System.Diagnostics.Process.Start(((LinkLabel)sender).Tag.ToString());
            }
            catch (Exception ex)
            {
                Log(ex.Message);
                MessageBox.Show("[linkLabelopenconfig_LinkClicked]: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void cboxProfiles_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                IniFile Startupini = new IniFile();
                //save this as the new currentconfig
                if (!File.Exists(Common.StartupConfigIniFile))
                {
                    Startupini.AddSection("settings");
                    Startupini.SetKeyValue("settings", "currentconfig", "config.ini");
                }
                else
                {
                    //It does exist so update the new desired config
                    Startupini.SetKeyValue("settings", "currentconfig", cboxProfiles.SelectedItem.ToString());
                }
                Startupini.Save(Common.StartupConfigIniFile);

                ReadSettings();
                InitIniSettings();//load settings into GUI

                //Now reload the datagridview
                LoadSavedWorlds();

                UpdateLauncherpBox();
            }
            catch (Exception ex)
            {
                Log("cboxProfiles_SelectedIndexChanged: " + ex.Message);
            }
        }

        private void llCreateNewProfile_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            try
            {
                Properties.Settings.Default.AppPath = tBoxAppPath.Text;
                Properties.Settings.Default.Save();

                IniFile NewProfileIni = new IniFile();

                //read the controls and save the selections
                //NewProfileIni.SetKeyValue("settings", "minecraft_server", txtbMCServerLoc.Text.Trim());//Future use?
                ////NewProfileIni.SetKeyValue("settings", "minecraft", txtbMCExeLoc.Text.Trim());
                ////NewProfileIni.SetKeyValue("settings", "mcmapDZ_renders", txtbMCMAPDZLoc.Text.Trim());
                ////NewProfileIni.SetKeyValue("settings", "minecraft_saved", txtbMCsaved.Text.Trim());
                ////NewProfileIni.SetKeyValue("settings", "minecraft_textpacks", txtbMCtexturepacks.Text.Trim());
                ////NewProfileIni.SetKeyValue("settings", "eihort", txtbEihortLoc.Text.Trim());
                ////NewProfileIni.SetKeyValue("settings", "backup_loc", txtbBackupsLoc.Text.Trim());
                ////NewProfileIni.SetKeyValue("settings", "backup_loc_textpacks", txtTexturepackbackups.Text.Trim());
                ////NewProfileIni.SetKeyValue("settings", "cleanupdays", numericUpDown1.Text.Trim());
                ////NewProfileIni.SetKeyValue("settings", "cloud_backup_loc", txtBoxCloudBackup.Text.Trim());
                ////NewProfileIni.SetKeyValue("settings", "snapshots_loc", txtBoxSnapshotsLoc.Text.Trim());

                NewProfileForm newf = new NewProfileForm("MCversion161");
                DialogResult r = newf.ShowDialog();
                string fullnew = "";
                if (r == System.Windows.Forms.DialogResult.OK)
                {
                    fullnew = Common.AppDataPathName + "\\" + newf.NewName + ".ini";
                }
                else
                    MessageBox.Show("New Profile cancelled.");

                //Save this with NEW file name from input
                NewProfileIni.Save(fullnew);
                cboxProfiles.Items.Add(newf.NewName + ".ini");
                //assuming it will be the last one
                cboxProfiles.SelectedIndex = cboxProfiles.Items.Count - 1;
                bSave.Enabled = false;
                UpdateExploreCloudBackupMenuItem();
            }
            catch (Exception ex)
            {
                Log(ex.Message);
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void deleteAllBackupsEXCEPTThisWorldToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                string world_keeping = ((FileInfo)listBoxBackups.SelectedItems[0]).Name.Split('_')[0].ToString();//World name must be in first split of filename

                DialogResult dResult = MessageBox.Show("Are you sure you want to DELETE all your backups except " + world_keeping + "?", "DELETE ALL BACKUPS ", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Warning);
                if (dResult == DialogResult.Yes)
                {
                    DirectoryInfo backupDir = new DirectoryInfo(Ini.GetSection("settings").GetKey("backup_loc").GetValue());
                    if (Directory.Exists(backupDir.FullName))
                    {
                        //Create the list of all the files we must delete (read all backup DIR
                        List<FileInfo> FilesList = new List<FileInfo>();
                        foreach (FileInfo item in backupDir.GetFiles())
                        {
                            //if filename startswith "world_keeping" then SKIP
                            if (!item.Name.StartsWith(world_keeping))
                                FilesList.Add(item);
                            else
                                Log("DELETE ALL BACKUPS: Keeping " + item.Name);
                        }

                        foreach (FileInfo f in FilesList)
                        {
                            //File.Delete(f.FullName);
                            FileSystem.DeleteFile(f.FullName, UIOption.OnlyErrorDialogs, RecycleOption.SendToRecycleBin);
                            listBoxBackups.Items.Remove(f);
                        }

                        //Refresh list
                        tabBackups_Enter(null, null);

                        Log("DELETE ALL BACKUPS: Finished! Check Recycle Bin to restore if necessary.");
                        listBoxBackups.Update();
                        ShowMsgBoxGenericCompleted("Deleted! [Check Recycle Bin to restore if necessary.]");
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
                Log("deleteAllBackupsEXCEPTThisWorldToolStripMenuItem_Click: " + ex.Message);
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void listBoxConfigs_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                Cursor.Current = Cursors.WaitCursor;
                flowLayoutPanel1.Controls.Clear();
                label14.Text = "Double-Click on a section below to edit it";
                string FILE = Ini.GetKeyValue("settings", "minecraft_saved").Replace("\\saves", "\\config\\") + listBoxConfigs.SelectedItem.ToString();
                //IF FILE has ">" in name then we need to convert to real path 
                if (FILE.Contains("->"))
                {
                    FILE = FILE.Replace("->", "\\");
                }

                if (File.Exists(FILE))
                {
                    MCConfig config = new MCConfig(FILE);
                    linkLabelopenconfig.Text = "open " + config.Fileinfo.Name;
                    linkLabelopenconfig.Tag = config.Fileinfo.FullName;
                    foreach (List<String> section in config.Sections)
                    {
                        Label l = new Label();
                        l.AutoSize = true;
                        l.Text = MCConfig.PPrint(section);
                        if (section.Count > 0)
                        {
                            if (section[0].IndexOf('{') != -1)
                                l.Name = section[0].Substring(0, section[0].IndexOf('{')).Trim();
                        }
                        l.DoubleClick += l_DoubleClick;
                        l.Tag = section;
                        flowLayoutPanel1.Controls.Add(l);
                    }
                }
                else
                {
                    //This maybe eihort.config so let's check
                    //if C:\Users\%USER%\eihort.config exists then add to the list
                    string usr_eihortconfig = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile) + "\\eihort.config";
                    if (File.Exists(usr_eihortconfig))
                    {
                        //Display items on left
                        //-- Path to the minecraft jar - this must exist for Eihort to function
                        //-- JPS3: You must match the path of the version you are currently using
                        //-- JPS3: You may comment out all lines except version you are currently using
                        //-- JPS3: C:\Users\<USERNAME>\AppData\Roaming\.minecraft\versions
                        //-- JPS3: C:\Users\<USERNAME>\AppData\Roaming\.technic\modpacks\<MODPACKNAME>\versions
                        //   minecraft_jar = minecraft_path .. "versions/1.6.1/1.6.1.jar";
                        Label l = new Label();
                        l.AutoSize = true;
                        l.Text = "minecraft_jar";
                        l.Tag = "Payload";
                        flowLayoutPanel1.Controls.Add(l);

                        //-- Fill in the following line to use a texture pack:
                        //   texture_pack = minecraft_path .. "texturepacks/???.zip";


                    }
                }

                Cursor.Current = Cursors.Default;
            }
            catch (Exception ex)
            {
                Log("listBoxConfigs_SelectedIndexChanged: " + ex.Message);
            }
        }

        void l_DoubleClick(object sender, EventArgs e)
        {
            if (sender.GetType().ToString() == "System.Windows.Forms.Label")
            {
                string FILE = Ini.GetKeyValue("settings", "minecraft_saved").Replace("\\saves", "\\config\\") + listBoxConfigs.SelectedItem.ToString();
                //IF FILE has ">" in name then we need to convert to real path 
                if (FILE.Contains("->"))
                {
                    FILE = FILE.Replace("->", "\\");
                }
                SectionEditorForm newf = new SectionEditorForm(FILE, ((System.Windows.Forms.Label)sender).Name, ((List<string>)((System.Windows.Forms.Label)sender).Tag));
                DialogResult r = newf.ShowDialog();
                if (r == System.Windows.Forms.DialogResult.OK)
                {
                    //force update
                    listBoxConfigs_SelectedIndexChanged(null, null);
                }
            }
        }

        private void btnAddCustomFile_Click(object sender, EventArgs e)
        {
            try
            {
                string fullname = BrowseTo("*", Common.MinecraftDefaultFolderWin.Replace("\\saves", "\\mods\\"));
                listbCustomFiles.Items.Add(fullname);
                listbCustomFiles.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                Log("btnAddCustomFile_Click: " + ex.Message);
            }
        }

        private void listbCustomFiles_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                //We need to update the ini file
                //make sure it has a section first [customfiles]
                if (Ini.HasSection("customfiles"))
                {
                    //Lets delete all that is there
                    Ini.RemoveSection("customfiles");
                }
                //Time to rebuild from current listbCustomFiles
                Ini.AddSection("customfiles");
                int number = 1;
                foreach (var item in listbCustomFiles.Items)
                {
                    Ini.SetKeyValue("customfiles", "custom" + Convert.ToString(number), item.ToString());
                }
                Ini.Save(Common.ConfigIniFile);
            }
            catch (Exception ex)
            {
                Log("listbCustomFiles_SelectedIndexChanged: " + ex.Message);
            }
        }

        private void btnAddNewFolder_Click(object sender, EventArgs e)
        {
            try
            {
                string fullname = BrowseToFolder(Common.MinecraftDefaultFolderWin.Replace("\\saves", "\\mods\\"));
                listbCustomFolders.Items.Add(fullname);
                listbCustomFolders.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                Log("btnAddNewFolder_Click: " + ex.Message);
            }
        }

        private void listbCustomFolders_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                //We need to update the ini file
                //make sure it has a section first [customfiles]
                if (Ini.HasSection("customfolders"))
                {
                    //Lets delete all that is there
                    Ini.RemoveSection("customfolders");
                }
                //Time to rebuild from current listbCustomFiles
                Ini.AddSection("customfolders");
                int number = 1;
                foreach (var item in listbCustomFolders.Items)
                {
                    Ini.SetKeyValue("customfolders", "custom" + Convert.ToString(number), item.ToString());
                }
                Ini.Save(Common.ConfigIniFile);
            }
            catch (Exception ex)
            {
                Log("listbCustomFolders_SelectedIndexChanged: " + ex.Message);
            }
        }

        private void btnBackUpCustoms_Click(object sender, EventArgs e)
        {
            try
            {
                string cloud_backup_loc = Ini.GetKeyValue("settings", "cloud_backup_loc");
                //send all files
                if (cboxCustomFiles.Checked && listbCustomFiles.Items.Count > 0)
                {
                    foreach (var item in listbCustomFiles.Items)
                    {
                        FileInfo filetwocopy = new FileInfo(item.ToString());
                        string newfilename = cloud_backup_loc + "\\" + filetwocopy.Name;
                        if (File.Exists(newfilename))
                        {
                            DialogResult dResult = MessageBox.Show("File already exists. Do you wish to overwrite:\r\n" + newfilename, "OVERWITE?", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Warning);
                            if (dResult == DialogResult.Yes)
                            {
                                File.Copy(filetwocopy.FullName, newfilename, true);
                                Log("btnBackUpCustoms_Click: Copied file= " + filetwocopy.FullName);
                            }
                            else if (dResult == DialogResult.No)
                            {

                            }
                            else if (dResult == DialogResult.Cancel)
                            {
                                ShowMsgBoxGenericCompleted("Cancelled");
                            }
                        }
                        else
                        {
                            File.Copy(filetwocopy.FullName, newfilename);
                            Log("btnBackUpCustoms_Click: Copied file= " + filetwocopy.FullName);
                        }
                    }
                    ShowMsgBoxGenericCompleted("Copy of Files to Cloud/Custom Storage");
                }

                //send all folders
                if (cboxCustomFolders.Checked && listbCustomFolders.Items.Count > 0)
                {
                    foreach (var folder in listbCustomFolders.Items)
                    {
                        DirectoryInfo foldertwocopy = new DirectoryInfo(folder.ToString());
                        string newfoldername = cloud_backup_loc + "\\" + foldertwocopy.Name;
                        if (Directory.Exists(newfoldername))
                        {
                            DialogResult dResult = MessageBox.Show("Folder already exists. Do you wish to overwrite:\r\n" + newfoldername, "OVERWITE?", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Warning);
                            if (dResult == DialogResult.Yes)
                            {
                                Directory.Delete(newfoldername, true);
                                DirUtils.CopyDirectory(foldertwocopy.FullName, newfoldername, true);
                                Log("btnBackUpCustoms_Click: Copied folder= " + foldertwocopy.FullName);
                            }
                            else if (dResult == DialogResult.No)
                            {

                            }
                            else if (dResult == DialogResult.Cancel)
                            {
                                ShowMsgBoxGenericCompleted("Cancelled");
                            }
                        }
                        else
                        {
                            DirUtils.CopyDirectory(foldertwocopy.FullName, newfoldername, true);
                            Log("btnBackUpCustoms_Click: Copied folder= " + foldertwocopy.FullName);
                        }
                    }
                    ShowMsgBoxGenericCompleted("Copy of Folders to Cloud/Custom Storage");
                }
            }
            catch (Exception ex)
            {
                Log("btnBackUpCustoms_Click: " + ex.Message);
            }
        }

        private void btnRemoveFile_Click(object sender, EventArgs e)
        {
            try
            {
                object currentobj = listbCustomFiles.SelectedItem;
                listbCustomFiles.Items.Remove(currentobj);
                if (listbCustomFiles.Items.Count > 0)
                    listbCustomFiles.SelectedIndex = 0;
                //force update
                listBoxConfigs_SelectedIndexChanged(null, null);
            }
            catch (Exception ex)
            {
                Log("btnRemoveFile_Click: " + ex.Message);
            }
        }

        private void btnRemoveFolder_Click(object sender, EventArgs e)
        {
            try
            {
                object currentobj = listbCustomFolders.SelectedItem;
                listbCustomFolders.Items.Remove(currentobj);
                if (listbCustomFolders.Items.Count > 0)
                    listbCustomFolders.SelectedIndex = 0;
                //force update
                listBoxConfigs_SelectedIndexChanged(null, null);
            }
            catch (Exception ex)
            {
                Log("btnRemoveFolder_Click: " + ex.Message);
            }
        }

        /// <summary>
        /// Double-clicked on Settings Row to update
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dataGridViewSettings_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                DataGridView Grid = (DataGridView)sender;
                DataGridViewCellEventArgs cellargs = (DataGridViewCellEventArgs)e;
                String SETTINGNAME = Grid.Rows[cellargs.RowIndex].Cells["Settings"].Value.ToString();
                String SETTINGVALUE = Grid.Rows[cellargs.RowIndex].Cells["Value"].Value.ToString();

                //minecraft_textpacks=C:\Users\Joe\AppData\Roaming\.minecraft\resourcepacks
                //backup_loc=C:\Users\Joe\Documents\MCMyVault\backups
                //backup_loc_textpacks=C:\Users\Joe\Documents\MCMyVault\backups-texturepacks
                //minecraft=C:\Program Files (x86)\Minecraft\Minecraft.exe
                //minecraft_saved=C:\Users\Joe\AppData\Roaming\.minecraft\saves
                //snapshots_loc=C:\Users\Joe\Documents\MCMyVault\snapshots
                //eihort=C:\Users\Joe\Documents\MCMyVault\eihort-0.3.14-win64\eihort.exe
                //mcmapDZ_renders=C:\Users\Joe\Documents\MCMyVault\mcmap2
                //cloud_backup_loc=C:\Users\Joe\Documents\MCMyVault\backups
                //NO BROWSE:
                //name_col_width=336
                //cleanupdays=7
                //lastbackup_col_width=335
                //map_col_width=129
                //filename_col_width=453

                switch (SETTINGNAME)
                {
                    case "cloud_backup_loc":
                        String cloud_backup_loc_path = BrowseToFolder(Environment.SpecialFolder.UserProfile);
                        if (!IsTextpack_WorldSameBackup(cloud_backup_loc_path, Ini.GetKeyValue("settings", "cloud_backup_loc")) &&
                            !String.IsNullOrEmpty(cloud_backup_loc_path))
                        {
                            Grid.Rows[cellargs.RowIndex].Cells["Value"].Value = cloud_backup_loc_path;
                            SETTINGVALUE = cloud_backup_loc_path;
                            Ini.SetKeyValue("settings", "cloud_backup_loc", cloud_backup_loc_path);
                            bSave.Enabled = true;
                            MessageBox.Show("NOTE: Settings are not saved yet. Remember to click Save button.", "Settings update", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                        }
                        break;
                    case "mcmapDZ_renders":
                        String mcmapDZ_renders_path = BrowseToFolder(Environment.SpecialFolder.MyDocuments);
                        if (!IsTextpack_WorldSameBackup(mcmapDZ_renders_path, Ini.GetKeyValue("settings", "mcmapDZ_renders")) &&
                            !String.IsNullOrEmpty(mcmapDZ_renders_path))
                        {
                            Grid.Rows[cellargs.RowIndex].Cells["Value"].Value = mcmapDZ_renders_path;
                            SETTINGVALUE = mcmapDZ_renders_path;
                            Ini.SetKeyValue("settings", "mcmapDZ_renders", mcmapDZ_renders_path);
                            bSave.Enabled = true;
                            MessageBox.Show("NOTE: Settings are not saved yet. Remember to click Save button.", "Settings update", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                        }
                        break;
                    case "eihort":
                        String eihort_path = BrowseToFolder(Environment.SpecialFolder.MyDocuments);
                        if (!IsTextpack_WorldSameBackup(eihort_path, Ini.GetKeyValue("settings", "eihort")) &&
                            !String.IsNullOrEmpty(eihort_path))
                        {
                            Grid.Rows[cellargs.RowIndex].Cells["Value"].Value = eihort_path;
                            SETTINGVALUE = eihort_path;
                            Ini.SetKeyValue("settings", "eihort", eihort_path);
                            bSave.Enabled = true;
                            MessageBox.Show("NOTE: Settings are not saved yet. Remember to click Save button.", "Settings update", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                        }
                        break;
                    case "snapshots_loc":
                        String snapshots_loc_path = BrowseToFolder(Environment.SpecialFolder.MyDocuments);
                        if (!IsTextpack_WorldSameBackup(snapshots_loc_path, Ini.GetKeyValue("settings", "snapshots_loc")) &&
                            !String.IsNullOrEmpty(snapshots_loc_path))
                        {
                            Grid.Rows[cellargs.RowIndex].Cells["Value"].Value = snapshots_loc_path;
                            SETTINGVALUE = snapshots_loc_path;
                            Ini.SetKeyValue("settings", "snapshots_loc", snapshots_loc_path);
                            bSave.Enabled = true;
                            MessageBox.Show("NOTE: Settings are not saved yet. Remember to click Save button.", "Settings update", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                        }
                        break;
                    case "minecraft_saved":
                        String minecraft_saved_path = BrowseToFolder(Environment.SpecialFolder.ApplicationData);
                        if (!IsTextpack_WorldSameBackup(minecraft_saved_path, Ini.GetKeyValue("settings", "minecraft_saved")) &&
                            !String.IsNullOrEmpty(minecraft_saved_path))
                        {
                            Grid.Rows[cellargs.RowIndex].Cells["Value"].Value = minecraft_saved_path;
                            SETTINGVALUE = minecraft_saved_path;
                            Ini.SetKeyValue("settings", "minecraft_saved", minecraft_saved_path);
                            bSave.Enabled = true;
                            MessageBox.Show("NOTE: Settings are not saved yet. Remember to click Save button.", "Settings update", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                        }
                        break;
                    case "minecraft_textpacks":
                        String minecraft_textpacks_path = BrowseToFolder(Environment.SpecialFolder.ApplicationData);
                        if (!IsTextpack_WorldSameBackup(minecraft_textpacks_path, Ini.GetKeyValue("settings", "minecraft_textpacks")) &&
                            !String.IsNullOrEmpty(minecraft_textpacks_path))
                        {
                            Grid.Rows[cellargs.RowIndex].Cells["Value"].Value = minecraft_textpacks_path;
                            SETTINGVALUE = minecraft_textpacks_path;
                            Ini.SetKeyValue("settings", "minecraft_textpacks", minecraft_textpacks_path);
                            bSave.Enabled = true;
                            MessageBox.Show("NOTE: Settings are not saved yet. Remember to click Save button.", "Settings update", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                        }
                        break;
                    case "backup_loc":
                        String backup_loc_path = BrowseToFolder(Environment.SpecialFolder.MyDocuments);
                        if (!IsTextpack_WorldSameBackup(backup_loc_path, Ini.GetKeyValue("settings", "backup_loc")) &&
                            !String.IsNullOrEmpty(backup_loc_path))
                        {
                            Grid.Rows[cellargs.RowIndex].Cells["Value"].Value = backup_loc_path;
                            SETTINGVALUE = backup_loc_path;
                            Ini.SetKeyValue("settings", "backup_loc", backup_loc_path);
                            bSave.Enabled = true;
                            MessageBox.Show("NOTE: Settings are not saved yet. Remember to click Save button.", "Settings update", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                        }
                        break;
                    case "minecraft":
                        String minecraft_path = "";
                        if (String.IsNullOrEmpty(SETTINGVALUE))
                            minecraft_path = BrowseTo("exe", Environment.GetFolderPath(Environment.SpecialFolder.ProgramFilesX86));
                        else
                        {
                            FileInfo mcexe = new FileInfo(SETTINGVALUE);
                            minecraft_path = BrowseTo("exe", mcexe.DirectoryName);
                            bSave.Enabled = true;
                            MessageBox.Show("NOTE: Settings are not saved yet. Remember to click Save button.", "Settings update", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                        }
                        if (!String.IsNullOrEmpty(minecraft_path))
                        {
                            Grid.Rows[cellargs.RowIndex].Cells["Value"].Value = minecraft_path;
                            SETTINGVALUE = minecraft_path;
                            Ini.SetKeyValue("settings", "minecraft", minecraft_path);
                        }
                        break;
                    case "backup_loc_textpacks":
                        String textpacksbackups_path = BrowseToFolder(Environment.SpecialFolder.MyDocuments);
                        if (!IsTextpack_WorldSameBackup(textpacksbackups_path, Ini.GetKeyValue("settings", "backup_loc")) &&
                            !String.IsNullOrEmpty(textpacksbackups_path))
                        {
                            Grid.Rows[cellargs.RowIndex].Cells["Value"].Value = textpacksbackups_path;
                            SETTINGVALUE = textpacksbackups_path;
                            Ini.SetKeyValue("settings", "backup_loc_textpacks", textpacksbackups_path);
                            bSave.Enabled = true;
                            MessageBox.Show("NOTE: Settings are not saved yet. Remember to click Save button.", "Settings update", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                        }
                        break;
                    default:
                        //Let's limit the scope of this event to speed it up a bit as I noticed better performance after encapsulation
                        if (e.ColumnIndex == 0 || e.ColumnIndex == 1)
                        {
                            string cellvalue = dataGridViewSettings.Rows[e.RowIndex].Cells["Value"].Value.ToString();
                            string settingName = dataGridViewSettings.Rows[e.RowIndex].Cells["Settings"].Value.ToString();
                            SettingschangeForm newf = new SettingschangeForm(cellvalue);
                            DialogResult r = newf.ShowDialog();
                            if (r == System.Windows.Forms.DialogResult.OK)
                            {
                                dataGridViewSettings.Rows[e.RowIndex].Cells["Value"].Value = newf.NewValue;
                                MessageBox.Show("NOTE: Settings are not saved yet. Remember to click Save button.", "Settings update", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                                bSave.Enabled = true;
                                //Need to make sure Settingsview is up to date.
                                UpdateSettingsView(false);
                            }
                            else
                            {
                                MessageBox.Show("Settings update cancelled.");
                                bSave.Enabled = false;
                            }
                        }
                        break;
                }
            }
            catch (Exception ex)
            {
                Log("dataGridViewSettings_CellDoubleClick: " + ex.Message);
            }
        }

        /// <summary>
        /// Add ToolTips to empty value cells
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dataGridViewSettings_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            try
            {
                //We only need to do this if we are working with first column.
                if (e.ColumnIndex == 0)
                {
                    DataGridViewCell cell = dataGridViewSettings.Rows[e.RowIndex].Cells[e.ColumnIndex];
                    //string c1 = Convert.ToString(dataGridViewSettings.Rows[e.RowIndex].Cells[e.ColumnIndex].Value);
                    string c2 = Convert.ToString(dataGridViewSettings.Rows[e.RowIndex].Cells[e.ColumnIndex + 1].Value);

                    if (String.IsNullOrWhiteSpace(c2))
                    {
                        //let's try and show a tooltip.
                        switch (Convert.ToString(e.Value))
                        {
                            case "minecraft":
                                cell.ToolTipText = "Where is your Minecraft.exe?";
                                break;
                            case "backup_loc":
                                cell.ToolTipText = "Where is your main Backup location?";
                                break;
                            case "backup_loc_textpacks":
                                cell.ToolTipText = "Where is your Backup ResourcePack/Texturepack location?";
                                break;
                            case "minecraft_server":
                                cell.ToolTipText = "Not used; You may remove from config.ini manually.";
                                break;
                            default:
                                break;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Log("dataGridViewSettings_CellFormatting: " + ex.Message);
            }
        }

        private void listBoxConfigs_MouseMove(object sender, MouseEventArgs e)
        {
            try
            {
                ListBox lb = (ListBox)sender;
                int index = lb.IndexFromPoint(e.Location);

                if (index >= 0 && index < lb.Items.Count)
                {
                    string toolTipString = lb.Items[index].ToString();

                    // check if tooltip text coincides with the current one,
                    // if so, do nothing
                    if (toolTip1.GetToolTip(lb) != toolTipString)
                        toolTip1.SetToolTip(lb, toolTipString);
                }
                else
                    toolTip1.Hide(lb);
            }
            catch (Exception ex)
            {
                Log("listBoxConfigs_MouseMove: " + ex.Message);
            }
        }

        private void openContainingFolderToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                FileInfo snapshot_fullname = new FileInfo(listBoxSnapshots.Items[listBoxSnapshots.SelectedIndex].ToString());
                System.Diagnostics.Process.Start(snapshot_fullname.DirectoryName);
            }
            catch (Exception ex)
            {
                Log(ex.Message);
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
            foreach (DataGridViewRow item in dataGridView1.SelectedRows)
            {
                string uniqueFilename = item.Cells["ColumnName"].Value.ToString() + "_" + DateTime.Now.ToString("MMddyyyyHHmmss") + ".zip";
                foreach (DirectoryInfo world in subdirectorysaved)
                {
                    //Only backup the world we selected
                    if (world.Name == item.Cells["ColumnName"].Value.ToString())
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
            if (Directory.Exists(savedDir.FullName))
            {
                //Now list each saved World and then match a rendered image
                DirectoryInfo[] subdirectorysaved = savedDir.GetDirectories();
                //do it for all
                string uniqueFilename = "_" + DateTime.Now.ToString("MMddyyyyHHmmss") + ".zip";
                foreach (DirectoryInfo world in subdirectorysaved)
                {
                    uniqueFilename = "_" + DateTime.Now.ToString("MMddyyyyHHmmss") + ".zip";
                    uniqueFilename = world.Name + uniqueFilename;
                    CreateZipFile(Ini.GetKeyValue("settings", "backup_loc") + "\\" + uniqueFilename, world.FullName);
                    Log("DoBackup(): " + Ini.GetKeyValue("settings", "backup_loc") + "\\" + uniqueFilename);
                }
            }
            else
            {
                //guess there is no \saved folder yet, which is A O K
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
            string backupfilePath = Ini.GetSection("settings").GetKey("backup_loc").GetValue() + "\\" + Convert.ToString(dataGridView1.SelectedRows[0].Cells["ColumnFilename"].Value);
            DialogResult dResult = MessageBox.Show("Are you sure you want to RESTORE " + dataGridView1.SelectedRows[0].Cells["ColumnFilename"].Value.ToString() + "\r\n World to Minecraft from file\r\n" + backupfilePath + "?\r\n(Make sure you have backed it up first as this deletes the current world)", "RESTORE WORLD " + dataGridView1.SelectedRows[0].Cells["ColumnName"].Value.ToString(), MessageBoxButtons.YesNoCancel, MessageBoxIcon.Warning);
            if (dResult == DialogResult.Yes)
            {
                //first Delete the saved dir
                DirectoryInfo worldDir = new DirectoryInfo(Ini.GetSection("settings").GetKey("minecraft_saved").GetValue() + "\\" + dataGridView1.SelectedRows[0].Cells["ColumnName"].Value.ToString());
                if (Directory.Exists(worldDir.FullName))
                {
                    Directory.Delete(worldDir.FullName, true);
                    //now recreate it from the zip file
                    Directory.CreateDirectory(worldDir.FullName);
                    ZipFile backupZip = new ZipFile(backupfilePath);
                    backupZip.ExtractAll(worldDir.FullName);
                    CheckSavedFolderStructure(worldDir.FullName);

                    //update
                    ShowMsgBoxGenericCompleted(dataGridView1.SelectedRows[0].Cells["ColumnName"].Value.ToString() + " restored");
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

        #endregion Backups

        /// <summary>
        /// Browse to a favorite application to add to MyApps
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnAddanApp_Click(object sender, EventArgs e)
        {
            try
            {
                //txtbEihortLoc
                String mynewmyapp_path = "";
                List<String> filetypeList = new List<string>();
                filetypeList.Add("exe"); filetypeList.Add("jar"); filetypeList.Add("txt"); filetypeList.Add("chm");
                mynewmyapp_path = BrowseTo(filetypeList.ToArray(), Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments));
                if (String.IsNullOrEmpty(mynewmyapp_path))
                {
                    //null action
                }
                else
                {
                    if (File.Exists(mynewmyapp_path))
                    {
                        //C:\Users\Joe\Documents\minecraft tools\MinecraftStructurePlanner.exe
                        List<string> fullpathList = new List<string>();
                        fullpathList.AddRange(mynewmyapp_path.Split('\\'));
                        int lastsection = fullpathList.Count - 1;
                        int count = 0;
                        foreach (string section in fullpathList)
                        {
                            if (count == lastsection)
                            {
                                //last section so this is the app
                                if (File.Exists(mynewmyapp_path))
                                {
                                    //save to myapps ini section
                                    Ini.SetKeyValue("myapps", Convert.ToString(fullpathList[count].Split('.')[0]), mynewmyapp_path);
                                }
                            }
                            count++;
                        }

                        Ini.Save(Common.ConfigIniFile);
                        //create the button
                        MyAppsSetup();
                    }
                    else
                    {
                        MessageBox.Show("Path Doesn't Exist. Sorry try again.", "Path Doesn't Exist", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            catch (Exception ex)
            {
                Log("btnAddanApp_Click: " + ex.Message);
            }
        }

        private void UpdateSettingsView(bool bForce)
        {
            try
            {
                //don't do twice please...
                if (dataGridViewSettings.Columns.Count <= 0 || bForce == true)
                {
                    //Delete existing rows
                    if (dataGridViewSettings.Rows.Count >= 1)
                    {
                        dataGridViewSettings.Rows.Clear();
                        dataGridViewSettings.Update();
                    }
                    else 
                    {
                        //there were NOT existing rows so lets do this
                        dataGridViewSettings.Columns.Add("Settings", "Settings");
                        dataGridViewSettings.Columns.Add("Value", "Value");
                    }

                    // dataGridView1.Columns["ColumnMap"].Width = Convert.ToInt32(Ini.GetKeyValue("settings", "map_col_width"));

                    //Format the columns
                    foreach (DataGridViewColumn col in dataGridViewSettings.Columns)
                    {
                        col.DefaultCellStyle.Font = new System.Drawing.Font("Verdana", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                        //col.DefaultCellStyle.ForeColor = System.Drawing.SystemColors.bl;
                    }

                    //ADD THE ROWS FROM THE CURRENT config.ini 
                    foreach (IniFile.IniSection.IniKey k in Ini.GetSection("settings").Keys)
                    {
                        //If this setting that is read is null add the row with name and no value OR add row name and value
                        if (k.GetValue() == null)
                        {
                            dataGridViewSettings.Rows.Add(k.Name, "");

                        }
                        else
                            dataGridViewSettings.Rows.Add(k.Name, k.GetValue());

                        //tooltip???
                        //  dataGridViewSettings.
                    }
                }

                dataGridViewSettings.Sort(dataGridViewSettings.Columns["Settings"], ListSortDirection.Ascending);
                //make sure the top row is selected
                if (dataGridViewSettings.Rows.Count > 0)
                    dataGridViewSettings.Rows[0].Selected = true;
                dataGridViewSettings.Update();
            }
            catch (Exception ex)
            {
                Log("UpdateSettingsView: " + ex.Message);
            }
        }

        private void openFileLocationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                FileInfo snapshot_fullname = new FileInfo(listBoxBackups.Items[listBoxBackups.SelectedIndex].ToString());
                System.Diagnostics.Process.Start(snapshot_fullname.DirectoryName);
            }
            catch (Exception ex)
            {
                Log(ex.Message);
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void listBoxBackups_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete)
            {
                DeleteBackupFile();
                e.Handled = true;
            }
        }

    }
}