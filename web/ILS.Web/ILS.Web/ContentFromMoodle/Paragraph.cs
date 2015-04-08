using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Text.RegularExpressions;
using System.IO;
using System.Reflection;
using System.Web;

namespace ILS.Web.ContentFromMoodle
{
    public class Paragraph
    {
        //конструктор, когда база данных MySQL расположена на том же компьютере(для теста)
        public Paragraph(string textParagraph, string nameParagraph, string fullPathDirectoryToMoodledata)
        {
            this.textParagraph = textParagraph;
            this.nameParagraph = nameParagraph;
            this.fullPathDirectoryToMoodledata = fullPathDirectoryToMoodledata;
            ReplaceAllImg();
            ListInString();
        }

        public Paragraph(string textParagraph, string nameParagraph, List<string> listPaths)
        {
            this.textParagraph = textParagraph;
            this.nameParagraph = nameParagraph;
            this.listPaths = listPaths;
            ReplaceAllImg();
            ListInString();
        }

        //имя параграфа
        public string nameParagraph;
        //текст параграфа
        public string textParagraph;
        //полный путь к диррекории moodledata, хранящей
        //все файлы
        private string fullPathDirectoryToMoodledata;
        //url_ьки картинок, запись в список поочередно, т.е. подстрока
        //в тексте См.Рис.1 соотв-ет первому элементу в списке
        private List<string> pictures;
        private List<string> name_pictures;
        public string[] s_pictures;
        public string[] s_name_pictures;

        //
        List<string> listPaths;

        private void ListInString()
        {
            if (pictures.Count != 0)
            {
                s_pictures = new string[pictures.Count];
                s_name_pictures = new string[name_pictures.Count];
                for (int i = 0; i < pictures.Count; i++)
                {
                    s_pictures[i] = pictures[i];
                    s_name_pictures[i] = name_pictures[i];
                }
            }
            else
            {
                s_pictures = null;
                s_name_pictures = null;
            }
        }

        private string GetReplacedImg(string text, string img_changed, string img_true)
        {
            return text.Replace(img_changed, img_true);
        }

        private List<string> GetAllImgTegs(string text)
        {
            //ищем значение для тега src  
            Regex re = new Regex(
               @"(?<=img\s+.+src\=[\x27\x22])(?<Url>[^\x27\x22]*)(?=[\x27\x22])");
            //@"(?<=img\s+src\=[\x27\x22])(?<Url>[^\x27\x22]*)(?=[\x27\x22])");
            //?<=img\s+src\=[\x27\x22])(?<Url>[^\x27\x22]*)(?=[\x27\x22]
            //или   
            //@"\< *[img][^\>]*[src] *= *[\"\']{0,1}([^\"\'\ >]*)" 

            Regex re2 = new Regex(
               @"(?<=<img .*?src\s*=\s*"")[^""]+(?="".*?>)");

            //получаем набор значений для аттрибута src       
            MatchCollection mc = re.Matches(text);
            MatchCollection mc2 = re2.Matches(text);

            List<string> al = new List<string>();
            List<string> al2 = new List<string>();
            foreach (Match m in mc)
                al.Add(m.Value);
            foreach (Match m in mc2)
                al2.Add(m.Value);
            if (al.Count > al2.Count) return al;
            else return al2;
        }

        private void DownloadFileToWebClient(string url, string name_file)
        {
            //url = "http://mysite.com/myfile.txt";
            WebClient webClient = new WebClient();
            webClient.DownloadFile(url, @"dirforFiles\" + name_file);
        }

        //Метод заменяет тег <img ...> со всеми своими атрибутами
        //на текст: "См.Рис.X", где X - номер картинки в параграфе        
        private void DeleteImgTegs(string url, string number)
        {
            #region

            //textParagraph = textParagraph.Replace(url, " См.Рис." + number + " ");

            //Замена первого вхождения
            int iw = textParagraph.IndexOf(url);
            string result = textParagraph.Remove(iw, url.Length).Insert(iw, " См.Рис." + number + " ");
            textParagraph = result;

            //Regex reg = new Regex(url);
            //textParagraph = reg.Replace(textParagraph, url, 1);

            int n = textParagraph.IndexOf(" См.Рис." + number + " ");
            int z = 4;
            int poz = 0;
            string tr = "";
            for (int i = n; i > 0; i--)
            {
                if (textParagraph[i] == 'g' || z < 4)
                {
                    tr += textParagraph[i];
                    z--;
                    if (z == 0)
                    {
                        z = 4;
                        if (tr == "gmi<")
                        {
                            poz = i;
                            tr = "";
                            break;
                        }
                        else { z = 4; tr = ""; }

                    }
                }
            }
            textParagraph = textParagraph.Remove(poz, n - poz);
            z = 2; poz = 0; tr = "";
            n = textParagraph.IndexOf(" См.Рис." + number + " ");
            int nach = n + 9 + Convert.ToInt32((number.ToString()).Length);

            char fgfh = 'n';
            for (int i = nach; i < textParagraph.Length; i++)
            {
                fgfh = textParagraph[i];
                if (textParagraph[i] == '>')
                {
                    poz = i;
                    break;
                }


            }
            /*string s = "";
            for (int i = 0; i < textParagraph.Length; i++)
            {
                if (i == nach) { s += "X"; continue; }
                if (i == poz - n - 3) { s += "X"; continue; }
                s += textParagraph[i];
            }
            textParagraph = s;*/
            char cdf = 'd';
            for (int fg = nach; fg < poz; fg++)
            {
                cdf = textParagraph[fg];
            }
            textParagraph = textParagraph.Remove(nach, poz - nach + 1);
            #endregion
        }

        private void ReplaceAllImg()
        {
            List<string> img;
            pictures = new List<string>();
            name_pictures = new List<string>();

            img = GetAllImgTegs(textParagraph);
            //DirectoryInfo[] di = new DirectoryInfo[1];
            //di[0] = new DirectoryInfo(fullPathDirectoryToMoodledata);
            string name_file = null;
            string pathToPicture = null;
            for (int i = 0; i < img.Count; i++)
            {
                name_file = img[i];
                #region Закоменченное
                /*
                //В этом цикле выдираем имя из ссылки
                for (int j = 0; j < img[i].Length; j++)
                {
                    if (img[i][img[i].Length - j - 1] != '/')
                        img_name_file += img[i][img[i].Length - j - 1];
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
                if (sf.FullPath.Count != 0)
                {
                    string pathToFile = sf.FullPath[0] + "\\" + img_name_file;

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

                    //File.WriteAllText(@"E:\WriteText1.txt", ListPages[iList]);
                    textParagraph = GetReplacedImg(textParagraph, img[i], path);
                }
                if (sf.FullPath.Count != 0)
                {
                    pictures.Add(img[i]);
                    DeleteImgTegs(img[i], (i+1).ToString());
                }
                img_name_file = "";*/
                #endregion                
                if (img[i].IndexOf("=") > -1)
                {
                    int pozZnakaRavno = img[i].IndexOf("=");
                    name_file = name_file.Substring(pozZnakaRavno + 1);
                }
                else
                    name_file = GetNameFile(img[i]);

                if (img[i].IndexOf("http://") > -1)
                    name_file = img[i];

                //когда сайт расположен локально
                //pathToPicture = GetFullPathToFile.GetFullPathToFile_Method(name_file, fullPathDirectoryToMoodledata);
            
                //когда изображения находятся удаленно на FTP сервере
                pathToPicture = null;
                foreach (string path in listPaths)
                {
                    if (path.IndexOf(name_file) > -1)
                    {
                        pathToPicture = path;
                        break;
                    }
                }

                if (pathToPicture != null)
                {
                    pictures.Add(pathToPicture);
                    DeleteImgTegs(img[i], (i + 1).ToString());
                }
                else
                {
                    pictures.Add(img[i]);
                    DeleteImgTegs(img[i], (i + 1).ToString());
                }
                          
                //name_pictures.Add(name_file);
                name_pictures.Add(GetNameFile(img[i]));                
            }

            //Удаляем все html теги
            textParagraph = Regex.Replace(textParagraph, "<[^>]*?>", string.Empty, RegexOptions.IgnoreCase);
            textParagraph = textParagraph.Replace("&quot;", string.Empty);
            textParagraph = textParagraph.Replace("\\", string.Empty);            
            //textParagraph = textParagraph.Replace("]", string.Empty); 

            /*
            //Потом убрать
            textParagraph = "<h1 align=\"center\">" + nameParagraph + "</h1>" + textParagraph;
            string er = nameParagraph.Replace(' ', '_') + ".html";
            File.WriteAllText(@"E:\dir\" + er, textParagraph);
            */
        }
        
        string GetNameFile(string name)
        {
            string name_file = "";
            //В этом цикле выдираем имя из name_file - 
            //Это в случае если имя файла содержит имена\имя директорий
            for (int j = 0; j < name.Length; j++)
            {
                if (name[name.Length - j - 1] != '/')
                    name_file += name[name.Length - j - 1];
                else
                    break;
            }
            //Инвертируем имя файла
            char[] s = new char[name_file.Length];
            for (int j = 0; j < name_file.Length; j++)
            {
                s[j] = name_file[name_file.Length - 1 - j];
            }
            name_file = new string(s);
            return name_file;
        }
    }
}