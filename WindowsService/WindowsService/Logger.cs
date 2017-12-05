using System;
using System.IO;
using System.Text.RegularExpressions;
using System.Threading;

namespace WindowsService
{
    public class Logger
    {
        FileSystemWatcher watcher;
        private StreamReader file;
        object obj = new object();
        bool enabled = true;

        public Logger()
        {
            watcher = new FileSystemWatcher(@"D:\\Sales");
            watcher.NotifyFilter = NotifyFilters.LastWrite | NotifyFilters.FileName;
            watcher.Filter = "*.csv";

            watcher.Deleted += Watcher_Deleted;
            watcher.Created += Watcher_Created;
            watcher.Changed += Watcher_Changed;
            watcher.Renamed += Watcher_Renamed;
        }

        public void Start()
        {
            watcher.EnableRaisingEvents = true;
            while (enabled)
            {
                Thread.Sleep(1000);
            }
        }
        public void Stop()
        {
            watcher.EnableRaisingEvents = false;
            enabled = false;
        }

        private void Watcher_Renamed(object sender, RenamedEventArgs e)
        {
            string fileEvent = "переименован в " + e.FullPath;
            string filePath = e.OldFullPath;
            RecordEntry(fileEvent, filePath);
        }

        private void Watcher_Changed(object sender, FileSystemEventArgs e)
        {
            string fileEvent = "изменен";
            string filePath = e.FullPath;
            RecordEntry(fileEvent, filePath);
        }

        private void Watcher_Created(object sender, FileSystemEventArgs e)
        {
            string fileEvent = "создан";
            string filePath = e.FullPath;
            RecordEntry(fileEvent, filePath);

            string fileName = "";
            string Name = "";
            string Date = "";
            string[] substrings; 


            FileInfo fileInf = new FileInfo(filePath);

            if (fileInf.Exists)
            {
                fileName = fileInf.Name;

                int n1 = 0;
                int n2 = 0;

                for (int i = 1; i < fileName.Length; i++)
                {
                    if (fileName[i] == '_') n1 = i;
                    else if (fileName[i] == '.') n2 = i;
                }

                Name = fileName.Substring(1, n1 - 1);
                Date = fileName.Substring(n1 + 1, n2 - n1 - 1);
            }


            using (file = new StreamReader(e.FullPath, System.Text.Encoding.Default))
            {
                string line = null;
                while ((line = file.ReadLine()) != null)
                {
                    substrings = Regex.Split(line, ";");

                }
            }
        }

        private void Watcher_Deleted(object sender, FileSystemEventArgs e)
        {
            string fileEvent = "удален";
            string filePath = e.FullPath;
            RecordEntry(fileEvent, filePath);
        }

        private void RecordEntry(string fileEvent, string filePath)
        {
            lock (obj)
            {
                using (StreamWriter writer = new StreamWriter(@"D:\\Service\log.txt", true))
                {
                    writer.WriteLine(String.Format("{0} файл {1} был {2}",
                        DateTime.Now.ToString("dd/MM/yyyy hh:mm:ss"), filePath, fileEvent));
                    writer.Flush();
                }
            }
        }
    }
}
