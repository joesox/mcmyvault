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
        /// Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\MCMyVault\\mcmapDZ\\renders"
        /// </summary>
        public static String mcmapDZ_renders = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\MCMyVault\\mcmapDZ\\renders";

        /// <summary>
        /// Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\MCMyVault\\mcmapDZ_latest-32bit\\mcmapDZ\\renders"
        /// </summary>
        public static String mcmapDZ_renders32bit = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\MCMyVault\\mcmapDZ_latest-32bit\\mcmapDZ\\renders";

        /// <summary>
        /// Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\MCMyVault\\eihort-0.3.12-win64\\eihort.exe"
        /// </summary>
        public static String eihort = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\MCMyVault\\eihort-0.3.12-win64\\eihort.exe";

        /// <summary>
        /// Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\MCMyVault\\eihort-0.3.12-win32\\eihort.exe"
        /// </summary>
        public static String eihort32bit = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\MCMyVault\\eihort-0.3.12-win32\\eihort.exe";
        
        public static String gLogFile = Application.StartupPath + "\\Log.txt";
        public static String sxAdminGuide = Application.StartupPath + "\\AdminGuide.pdf";
        public static String ReadMe = Application.StartupPath + "\\ReadMe.txt";
        public static String InstallReadMe = Application.StartupPath + "\\INSTALLATION README.txt";
        public static String ConfigIniFile = Application.StartupPath + "\\config.ini";
    }

}
