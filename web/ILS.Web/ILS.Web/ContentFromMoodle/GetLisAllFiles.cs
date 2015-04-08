using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ILS.Web.ContentFromMoodle
{
    public static class GetLisAllFiles
    {
        public static FTPUtil ftpClient;
        public static List<string> listPathFile = new List<string>();
        public static List<string> listPathDir = new List<string>();

        public static void Run()
        {
            string pathTo_moodledata = "ftp://distance.itschool.ssau.ru/htdocs/moodledata/";
            ftpClient = new FTPUtil(@"ftp://distance.itschool.ssau.ru/htdocs/moodledata/", "distanceitschool", "4tXeKbwK");

            string[] files = FTPGetFiles("/");
            for (int i = 0; i < files.Length; i++)
            {              
                if (!listPathFile.Contains(files[i]))
                    listPathFile.Add(pathTo_moodledata + files[i]);
            }

            string[] dirs = FTPGetDirectories("/");            
            foreach (string s in dirs)
            {               
                if (!listPathDir.Contains(s))
                    listPathDir.Add(s);
            }

            while (true)
            {
                string pathDir;
                if (listPathDir.Count > 0)
                    pathDir = listPathDir[0];
                else
                    break;
                
                files = FTPGetFiles(pathDir);
                for (int i = 0; i < files.Length; i++)
                {
                    Console.WriteLine(files[i]);
                    if (!listPathFile.Contains(pathDir + "/" + files[i]))
                        listPathFile.Add(pathTo_moodledata + pathDir + "/" + files[i]);
                }

                dirs = FTPGetDirectories(pathDir);                
                foreach (string s in dirs)
                {                    
                    if (!listPathDir.Contains(pathDir + "/" + s))
                        listPathDir.Add(pathDir + "/" + s);
                }
                listPathDir.Remove(pathDir);
            }            

            ///* Release Resources */
            ftpClient = null;
        }

        public static string[] FTPGetDirectories(string dir)
        {
            List<string> dirs = new List<string>();
            /* Get Contents of a Directory (Names Only) */
            string[] simpleDirectoryListing = ftpClient.directoryListDetailed(dir);
            for (int i = 0; i < simpleDirectoryListing.Count(); i++)
            {
                string[] s = simpleDirectoryListing[i].Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);  
                string nameDir = "";
                if (simpleDirectoryListing[i] != "")
                {
                    nameDir = s[8];
                    nameDir = nameDir.Trim();
                    if ((nameDir.IndexOf(".zip") > -1) || (nameDir.IndexOf(".rar") > -1))
                    {
                        if (nameDir.Length >= 4)
                            nameDir = nameDir.Substring(nameDir.Length - 4, 4);
                    }
                    if (s[0] != "-rw-r-----" && s[8] != "." && s[8] != ".." && s[0] != "-rw-rw-rw-" && s[0] != "-rw-------" && nameDir != ".zip" && nameDir != ".rar")
                        dirs.Add(s[8]);
                }               
            }
            return dirs.ToArray();
        }

        public static string[] FTPGetFiles(string dir)
        {
            List<string> files = new List<string>();
            /* Get Contents of a Directory (Names Only) */
            string[] simpleDirectoryListing = ftpClient.directoryListDetailed(dir);
            for (int i = 0; i < simpleDirectoryListing.Count(); i++)
            {
                string[] s = simpleDirectoryListing[i].Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);               
                if (simpleDirectoryListing[i] != "")
                    if (s[0] == "-rw-r-----" || s[0] == "-rw-rw-rw-")
                        files.Add(s[8]);              
            }
            return files.ToArray();
        }       
    }
}