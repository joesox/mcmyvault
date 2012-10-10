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
        public static String mcmapDZ_renders = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\mcmapDZ\\renders";
        public static String gLogFile = Application.StartupPath + "\\Log.txt";
        public static String sxAdminGuide = Application.StartupPath + "\\AdminGuide.pdf";
        public static String ReadMe = Application.StartupPath + "\\ReadMe.txt";
        public static String InstallReadMe = Application.StartupPath + "\\INSTALLATION README.txt";
        public static String ConfigIniFile = Application.StartupPath + "\\config.ini";
    }
}
