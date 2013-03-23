//2013-03-18
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace MCMyVault
{
    class DirUtils
    {
        public static List<String> ret = new List<string>();

        /// <summary>
        /// Copy the directory and its subfolders
        /// </summary>
        /// <param name="SourcePath"></param>
        /// <param name="DestinationPath"></param>
        /// <param name="overwriteexisting"></param>
        /// <returns></returns>
        public static bool CopyDirectory(string SourcePath, string DestinationPath, bool overwriteexisting)
        {
            bool ret = false;
            try
            {
                SourcePath = SourcePath.EndsWith(@"\") ? SourcePath : SourcePath + @"\";
                DestinationPath = DestinationPath.EndsWith(@"\") ? DestinationPath : DestinationPath + @"\";

                if (Directory.Exists(SourcePath))
                {
                    if (Directory.Exists(DestinationPath) == false)
                        Directory.CreateDirectory(DestinationPath);

                    foreach (string fls in Directory.GetFiles(SourcePath))
                    {
                        FileInfo flinfo = new FileInfo(fls);
                        flinfo.CopyTo(DestinationPath + flinfo.Name, overwriteexisting);
                    }
                    foreach (string drs in Directory.GetDirectories(SourcePath))
                    {
                        DirectoryInfo drinfo = new DirectoryInfo(drs);
                        if (CopyDirectory(drs, DestinationPath + drinfo.Name, overwriteexisting) == false)
                            ret = false;
                    }
                }
                ret = true;
            }
            catch (Exception)
            {
                ret = false;
            }
            return ret;
        }

        public static List<String> GetDirectorysToCopy(string dir)
        {
            List<String> dirs = new List<string>();
            dirs.Add(dir);
            return dirs;
        }

        /// <summary>
        /// Recursive searching of folders and their subfolders
        /// </summary>
        /// <param name="d"></param>
        public static void getDirsFiles(DirectoryInfo d)
        {
            FileInfo[] files;
            //get all files for the current directory

            files = d.GetFiles("*.*");

            //iterate through the directory 
            /*
            foreach (FileInfo file in files)
            {
                //get details of each file using file object
                TotalBytes += file.Length;// TOTAL IT!
                progressBar1.PerformStep();
                progressBar1.Focus();
                this.Update();
            }
            */
            //get sub-folders for the current directory

            DirectoryInfo[] dirs = d.GetDirectories("*.*");

            foreach (DirectoryInfo directory in dirs)
            {
                getDirsFiles(directory);
            }
        }

        public static List<String> GetAllDirFiles(string SourcePath)
        {
            try
            {
                SourcePath = SourcePath.EndsWith(@"\") ? SourcePath : SourcePath + @"\";

                if (Directory.Exists(SourcePath))
                {
                    foreach (string fls in Directory.GetFiles(SourcePath))
                    {
                        FileInfo flinfo = new FileInfo(fls);
                        ret.Add(flinfo.FullName);
                    }
                    foreach (string drs in Directory.GetDirectories(SourcePath))
                    {
                        DirectoryInfo drinfo = new DirectoryInfo(drs);
                        GetAllDirFiles(drinfo.FullName);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return ret;
        }

        public static bool isDirectory(string path)
        {
            bool result = true;
            System.IO.FileInfo fileTest = new System.IO.FileInfo(path);
            if (fileTest.Exists == true)
            {
                result = false;
            }
            else
            {
                if (fileTest.Extension != "")
                {
                    result = false;
                }
            }
            return result;
        }
    }
}
