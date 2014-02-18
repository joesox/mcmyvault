using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace MCMyVault
{
    class Common
    {
        //NOTE: InnoSetup will create the MyDocuments\MineCraft MyVault\ Directory

        /// <summary>
        /// Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\.minecraft\\saves"
        /// </summary>
        public static String MinecraftDefaultFolderWin = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\.minecraft\\saves";
        /// <summary>
        /// MinecraftDefaultFolderWin.Replace("saves", "texturepacks")
        /// </summary>
        public static String MinecraftDefaultTextureFolder = MinecraftDefaultFolderWin.Replace("saves", "texturepacks");
        
        /// <summary>
        /// Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\" + Application.ProductName.Replace("_", " ") + "\\backups"
        /// </summary>
        public static String MinecraftToolsBackupsFolder = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\" + Application.ProductName.Replace("_", " ") + "\\backups";

        /// <summary>
        /// Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\" + Application.ProductName.Replace("_", " ") + "\\backups"
        /// </summary>
        public static String MinecraftToolssnapshotssFolder = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\" + Application.ProductName.Replace("_", " ") + "\\snapshots";
        
        /// <summary>
        /// Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\MCMyVault\\mcmap2"
        /// </summary>
        public static String mcmap2_dir = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\MCMyVault\\mcmap2";

        /// <summary>
        /// Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\MCMyVault\\eihort-0.3.12-win64\\eihort.exe"
        /// </summary>
        public static String eihort = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\MCMyVault\\eihort-0.3.13-win64\\eihort.exe";

        /// <summary>
        /// Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\MCMyVault\\eihort-0.3.12-win32\\eihort.exe"
        /// </summary>
        public static String eihort32bit = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\MCMyVault\\eihort-0.3.13-win32\\eihort.exe";

        /// <summary>
        /// Get Top direcory's name
        /// </summary>
        /// <param name="fullnamefolder">Full path of a Directory</param>
        /// <returns>Top direcory's name</returns>
        public static String GetTopFolderName(string fullnamefolder)
        {
            string NAME = "";
            string[] splitname = fullnamefolder.Split('\\');
            NAME = splitname[splitname.Length - 1].Trim();
            return NAME;
        }

        public static String ConfigIniFileOLD = Environment.GetFolderPath(Environment.SpecialFolder.ProgramFilesX86) + "\\MCMyVault\\config.ini";
        public static String ReadMe = Application.StartupPath + "\\ReadMe.txt";

        public static String AppDataPathName = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\" + GetTopFolderName(Application.StartupPath);
        public static String ConfigIniFile = Common.AppDataPathName + "\\config.ini";
        public static String gLogFile = Common.AppDataPathName + "\\Log.txt";
        public static String StartupConfigIniFile = Common.AppDataPathName + "\\startupconfig.ini";//default is config.ini

    }

}
