using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Reflection;
using System.Net;
using System.Web;

namespace ILS.Web.ContentFromMoodle
{
    /// <summary>
    /// Класс для получения полного пути к файлам. Работает только тогда, когда сайт
    /// расположен локально
    /// </summary>
    public static class GetFullPathToFile
    {
        public static string GetFullPathToFile_Method(string name_file, string fullPathDirectoryToMoodledata)
        {
            string pathToFile = "";
            DirectoryInfo[] di = new DirectoryInfo[1];
            di[0] = new DirectoryInfo(fullPathDirectoryToMoodledata);
            string img_name_file = "";

            //В этом цикле выдираем имя из name_file - 
            //Это в случае если имя файла содержит имена\имя директорий
            for (int j = 0; j < name_file.Length; j++)
            {
                if (name_file[name_file.Length - j - 1] != '/')
                    img_name_file += name_file[name_file.Length - j - 1];
                else
                    break;
            }
            //Инвертируем имя файла
            char[] s = new char[img_name_file.Length];
            for (int j = 0; j < img_name_file.Length; j++)
            {
                s[j] = img_name_file[img_name_file.Length - 1 - j];
            }
            img_name_file = new string(s);

            SearchFiles sf = new SearchFiles(di, img_name_file);
            string sd = "";
            if (sf.FullPath.Count != 0)
            {                
                for (int i = 0; i < sf.FullPath.Count; i++)
                {
                    sd = "";
                    for (int j = 0; j < sf.FullPath[i].Length; j++)
                    {
                        if (sf.FullPath[i][sf.FullPath[i].Length - j - 1] != '\\')
                            sd += sf.FullPath[i][sf.FullPath[i].Length - j - 1];
                        else
                            break;
                    }
                    //Инвертируем имя файла
                    char[] s2 = new char[sd.Length];
                    for (int j = 0; j < sd.Length; j++)
                    {
                        s2[j] = sd[sd.Length - 1 - j];
                    }
                    sd = new string(s2);
                    sd += "/" + img_name_file;

                    if (sd == name_file)
                    {
                        pathToFile = sf.FullPath[i] + "\\" + img_name_file;
                        return pathToFile;
                    }
                }
                return null;
                

                //************************************************
                //Эта чать пока для проверки
                DownloadFileToWebClient(pathToFile, img_name_file);
                String path = Assembly.GetExecutingAssembly().Location; // получаем полный путь к исполняемому файлу
                string[] mass = path.Split('\\');
                path = "";
                for (int j = 1; j < mass.Length - 1; j++)
                {
                    path += mass[j] + "/";
                }
                path = "../" + path;
                path += "dirforFiles/" + img_name_file; // ВАЖНО! ПУТЬ К ФАЙЛУ ОБРАЗУЕТСЯ
                // С УСЛОВИЕМ НА БУДУЩЕЕ, ЧТО HTML БУДУТ В КОРНЕ ДИСКА E:
                //************************************************                  
            }
            else return null;
        }

        private static void DownloadFileToWebClient(string url, string name_file)
        {
            //url = "http://mysite.com/myfile.txt";
            WebClient webClient = new WebClient();
            webClient.DownloadFile(url, @"dirforFiles\" + name_file);
        }
    }
}