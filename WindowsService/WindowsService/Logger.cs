﻿using System;
using System.IO;
using System.Text.RegularExpressions;
using System.Threading;
using WindowsService.BLL;
using WindowsService.BLL.DTO;

namespace WindowsService
{
    public class Logger
    {
        FileSystemWatcher watcher;
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
            bool flag;
            string fileEvent = "создан";
            string filePath = e.FullPath;
            RecordEntry(fileEvent, filePath);

            ParserCSV parser = new ParserCSV();
            flag = parser.ParseFile(filePath);

            if (flag)
            {
                fileEvent = "успешно распаршен";
            }
            else
            {
                fileEvent = "не удалось распарсить";
            }
            RecordEntry(fileEvent, filePath);
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
