using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading;
using System.Web;

namespace ILS.Web.ContentFromMoodle
{
    // Многопоточный поиск файла
    public class SearchFiles
    {
        public SearchFiles(DirectoryInfo[] di, string Name)
        {
            this.DirInf = di;
            this.FileName = Name;
            this.BegDate = DateTime.MinValue;
            this.EndDate = DateTime.MaxValue;
            this.MinSize = 0;
            this.MaxSize = Int32.MaxValue;
            this.FullPath = new List<string>();

            foreach (DirectoryInfo dinf in this.DirInf)
            {
                this.DoItParalell(dinf);
            }
        }

        //приватные переменные соответствующие параметрам поиска и массиву директорий для поиска
        //Инициализируются в конструкторе
        private DirectoryInfo[] DirInf;
        private string FileName;
        private DateTime BegDate;
        private DateTime EndDate;
        private int MinSize;
        private int MaxSize;

        public List<string> FullPath;

        //Переменная для хранения списка екземпляров класса Searcher
        private List<Searcher> Searchers;
        //Список потоков созданных для поиска
        private List<Thread> Threads;
        //Список полученных файлов
        public List<FileInfo> FindedFiles;

        //Метод класса который реализует многопоточность
        //Создает колекции классов Searcher и Thread (поток)
        //Передает в потоки делегаты метода Searcher.StartSearch
        //Который реализует поиск файлов по введенных критериях
        private void DoItParalell(DirectoryInfo di)
        {
            //если в директории есть вложенные папки выполнится следующий код
            if (di.GetDirectories().Length != 0)
            {
                //создадим новый список екземпляров поискового класса
                Searchers = new List<Searcher>();
                //создадим новый список потоков
                Threads = new List<Thread>();

                //для каждой папки в корневой выполним создание поискового класса и потока
                //для передачи ему делегата на метод поиска
                foreach (DirectoryInfo subdir in di.GetDirectories())
                {
                    Searcher s1 = new Searcher(subdir, this.FileName, this.BegDate, this.EndDate, this.MinSize, this.MaxSize);
                    this.Searchers.Add(s1);
                    Thread t1 = new Thread(new ThreadStart(s1.StartSearch));
                    this.Threads.Add(t1);
                }
                //запускаем потоки на выполнение
                foreach (Thread item in this.Threads)
                {
                    item.Start();
                }
                //ждем завершения работы всех потоков
                while (true)
                {
                    int endCount = 0;
                    foreach (Thread item in this.Threads)
                    {
                        //проверка состояния потока
                        if (item.ThreadState == ThreadState.Running)
                        {
                            endCount++;
                        }
                    }
                    //если вторичных потоков больше нет основной поток продолжает работу
                    //прерывая текущий цикл
                    if (endCount == 0) break;
                }


                //Для каждого поискового класса читаем список найденных файлов
                foreach (Searcher si in Searchers)
                {
                    //Добавляем информацию про каждый полученный файл в список на форме
                    foreach (FileInfo fi in si.FindedFiles)
                    {
                        FullPath.Add(fi.DirectoryName);
                    }
                }
                //Создаем екземпляр поискового класса для поиска в корен дисков/входных директорий
                Searcher ls = new Searcher(di, this.FileName, this.BegDate, this.EndDate, this.MinSize, this.MaxSize);
                //Ищем файлы только внутри корневой директории
                List<FileInfo> localsFile = ls.SearchFile(di);
                //Добавляем найденные файлы в визуальный список на форме
                foreach (FileInfo fi in localsFile)
                {
                    FullPath.Add(fi.DirectoryName);
                }
            }
            //этот код выполняется если в корневом каталоге нет вложенных
            else
            {
                //Создается единый екземпляр класс Searcher
                //Его аттрибуты инициализируются привтаными переменными текущей формы
                Searcher singleSearch = new Searcher(di, this.FileName, this.BegDate, this.EndDate, this.MinSize, this.MaxSize);
                //Проводится поиск в одном потоке
                singleSearch.StartSearch();
                //Получаем все отысканные файлы и выводим в список на форме
                foreach (FileInfo fi in singleSearch.FindedFiles)
                {
                    FullPath.Add(fi.DirectoryName);
                }
            }

        }
    }
    /* Клас соответсвующий обьекту-потоку поиска
     * Аттрибуты класа содержат приватные переменные которые
     * соответствуют параметрам поиска. У класа присудствуют два конструктора и два публичных
     * метода StartSearch - метод реализует поиск в всех ветках и подкаталогах директории
     * заданной как Searcher.parentDirectory, SearchFile - реализует поиск файлов за
     * заданными критериями отбора в каталоге который передается как параметр метода
     */
    public class Searcher
    {
        private DirectoryInfo parentDirectory;
        private string fileName;
        private DateTime crTimeBeg;
        private DateTime crTimeEnd;
        private double sizeInKbEnd;
        private double sizeInKbBegin;

        //Разширенный конструктор - позволяет инициализировать приватные переменные класса
        public Searcher(DirectoryInfo parentDirectory, string fileName, DateTime crTimeBeg, DateTime crTimeEnd, double sizeBegin, double sizeEnd)
        {
            this.parentDirectory = parentDirectory;
            this.fileName = fileName;
            this.crTimeBeg = crTimeBeg;
            this.crTimeEnd = crTimeEnd;
            this.sizeInKbBegin = sizeBegin;
            this.sizeInKbEnd = sizeEnd;
            this.FindedFiles = new List<FileInfo>();
        }

        //переменная видиамя извне класса - список полученных файлов
        public List<FileInfo> FindedFiles;
        //Метод который ищет все файлы в переданной в виде параметра директории
        //что отвечают критериям поиска
        public List<FileInfo> SearchFile(DirectoryInfo dir)
        {
            string pattern = this.fileName;
            List<FileInfo> SearchedFile = new List<FileInfo>();
            //Отбираем файлы которые соответствуют строке поиска
            FileInfo[] inDest = dir.GetFiles(pattern, SearchOption.TopDirectoryOnly);
            foreach (FileInfo fi in inDest)
            {
                //Попытка доступа к файлам, если успешно - сравниваем даты и размеры с еталонными
                try
                {
                    if (fi.CreationTime.Date >= this.crTimeBeg && fi.CreationTime.Date <= this.crTimeEnd && ((fi.Length) / 1000) <= this.sizeInKbEnd && ((fi.Length / 1000)) >= this.sizeInKbBegin)
                    {
                        try
                        {
                            SearchedFile.Add(fi);
                        }
                        catch
                        {
                            Thread.Sleep(1);
                        }
                    }
                }
                catch
                {
                    Thread.Sleep(1);
                }
            }
            //Возвращаем список файлов
            return SearchedFile;
        }

        public void StartSearch()
        {
            bool AllOk = true;
            //попытка доступа к директории - если успешно то все остается неизменным
            try
            {
                parentDirectory.GetDirectories();
            }
            //Если возникло исключение - переменная  AllOk получает значение false
            catch
            {
                AllOk = false;
            }
            //Если доступ был успешным то получаем подкаталоги
            if (AllOk)
            {
                if (parentDirectory.GetDirectories().Length != 0)
                {
                    //Для каждого подкаталога запускаем новый поиск
                    foreach (DirectoryInfo di in parentDirectory.GetDirectories())
                    {
                        Searcher second = new Searcher(di, fileName, crTimeBeg, crTimeEnd, sizeInKbBegin, sizeInKbEnd);
                        second.StartSearch();
                        //Получаем список файлов удовлетворяющих условия для подкаталога 
                        this.FindedFiles.AddRange(second.FindedFiles);
                    }
                    //Находим файлы удовлетворяющие условия в самом каталоге
                    this.SearchFile(parentDirectory);
                }
                else
                {
                    //Если подкаталогов нет просто ищем файлы в текущем
                    this.FindedFiles.AddRange(this.SearchFile(this.parentDirectory));
                }
            }
        }
    }
}