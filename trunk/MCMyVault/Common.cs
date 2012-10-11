using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace MCMyVault
{
    class Common
    {
        public static String MinecraftDefaultFolderWin = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\.minecraft\\saves";
        //InnoSetup will create the MyDocuments\MineCraft MyVault\ Directory
        public static String MinecraftToolsBackupsFolder = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\" + Application.ProductName.Replace("_", " ") + "\\backups";
        public static String mcmapDZ_renders = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\MCMyVault\\mcmapDZ\\renders";
        public static String mcmapDZ_renders32bit = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\MCMyVault\\mcmapDZ_latest-32bit\\mcmapDZ\\renders";
        public static String eihort = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\MCMyVault\\eihort-0.3.11-win64\\eihort.exe";
        public static String eihort32bit = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\MCMyVault\\eihort-0.3.11-win32\\eihort.exe";
        public static String gLogFile = Application.StartupPath + "\\Log.txt";
        public static String sxAdminGuide = Application.StartupPath + "\\AdminGuide.pdf";
        public static String ReadMe = Application.StartupPath + "\\ReadMe.txt";
        public static String InstallReadMe = Application.StartupPath + "\\INSTALLATION README.txt";
        public static String ConfigIniFile = Application.StartupPath + "\\config.ini";
    }
}
