using System;
using System.Collections.Generic;
using System.IO;
using menelabs.core;

namespace FileSystemWatcherCore
{
    class Program
    {
        public string key = "";
        public string PathFile = "";
        public string fileSize = "";
        public string systemprocess = "";
        public string file_from = "";
        public string file_to = "";
        public string user = "";
       

        static void Main(string[] args)
        {

            Console.WriteLine("Type Enter to exit:::");
            StartWatchers();
            Console.ReadLine();
            Console.WriteLine("Press enter to exit.");
            Console.ReadLine();

           
        }



        public static void StartWatchers()
        {
            try
            {
                string[] ArrayPaths = new string[2];
                List<FileSystemWatcher> watchers = new List<FileSystemWatcher>();
                //ArrayPaths[0] = @"C:\";
                ArrayPaths[0] = @"E:\";
                ArrayPaths[1] = @"D:\";
                //key = General.GetTimeServer();

                Global.key = General.GetTimeServer();

                int i = 0;
                foreach (String String in ArrayPaths)
                {
                    watchers.Add(MyWatcherFatory(ArrayPaths[i]));
                    i++;
                }

                foreach (FileSystemWatcher watcher in watchers)
                {
                    watcher.EnableRaisingEvents = true;
                    watcher.InternalBufferSize = 100 * 1024;

                    Console.WriteLine("Watching this folder {0}", watcher.Path);
                    i++;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            

        }
        public static FileSystemWatcher MyWatcherFatory(string path)
        {
            
            FileSystemWatcher watcher = new FileSystemWatcher(path);

            try
            {

                watcher.NotifyFilter = NotifyFilters.Attributes
                                     | NotifyFilters.CreationTime
                                     | NotifyFilters.DirectoryName
                                     | NotifyFilters.FileName
                                     | NotifyFilters.LastAccess
                                     | NotifyFilters.LastWrite
                                     | NotifyFilters.Security
                                     | NotifyFilters.Size;


                watcher.Changed += OnChanged;
                watcher.Created += OnCreated;
                watcher.Deleted += OnDeleted;
                watcher.Renamed += OnRenamed;
                watcher.Error += OnError;
                watcher.Path = path;
                //watcher.Filter = "*.pdf";
                watcher.Filters.Add("*.pdf");
                watcher.Filters.Add("*.xls");
                watcher.Filters.Add("*.xlsx");
                watcher.Filters.Add("*.iso");
                watcher.Filters.Add("*.pptx");
                watcher.IncludeSubdirectories = true;

                
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

            return watcher;
        }

        //private static void Watcher_Created(object sender, FileSystemEventArgs e)
        //{
        //    System.Threading.Thread.Sleep(1000);
        //    FileInfo fileInfo = new FileInfo(e.FullPath);
        //    Console.WriteLine("File Created!! :: {0}", e.FullPath);
        //}


        public static void OnChanged(object sender, FileSystemEventArgs e)
        {
            try
            {
                //PathFile = "";
                if (e.ChangeType != WatcherChangeTypes.Changed)
                    if (e.ChangeType != WatcherChangeTypes.Changed)
                    {

                        return;
                    }
                Console.WriteLine($"Changed: {e.FullPath}" + " on :" + Global.key);
                var info = new FileInfo(e.FullPath);
                var theSize = info.Length;
                if (e.FullPath.Contains("$RECYCLE.BIN") == false)
                {
                    string hasil = DataProcess.InsertFileMonitoring(e.FullPath.ToString(), e.Name, theSize.ToString(), Global.key, "Changed", "", "", "leo", "DESKTOP-RVETU0U");
                    Console.WriteLine(hasil);
                }
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            
           
               
        }

        private static void OnCreated(object sender, FileSystemEventArgs e)
        {
            try
            {
                string value = $"Created: {e.FullPath}" + " on :" + Global.key;
                Console.WriteLine(value);
                var info = new FileInfo(e.FullPath);
                var theSize = info.Length;
                if (e.FullPath.Contains("$RECYCLE.BIN") == false)
                {
                    string hasil = DataProcess.InsertFileMonitoring(e.FullPath.ToString(), info.Name, theSize.ToString(), Global.key, "Created", "", "", "leo", "DESKTOP-RVETU0U");
                    Console.WriteLine(hasil);
                }
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            
               
        }
        private static void OnDeleted(object sender, FileSystemEventArgs e)
        {
            try
            {
                Console.WriteLine($"Deleted: {e.FullPath}" + " on :" + Global.key);
                var info = new FileInfo(e.FullPath);
                if (e.FullPath.Contains("$RECYCLE.BIN") == false)
                {
                    string hasil = DataProcess.InsertFileMonitoring(e.FullPath.ToString(), info.Name, "0", Global.key, "Deleted", "", "", "leo", "DESKTOP-RVETU0U");
                    Console.WriteLine(hasil);
                }
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            
            
        }
        //private static void OnDeleted(object sender, FileSystemEventArgs e) =>
        //    Console.WriteLine($"Deleted: {e.FullPath}" + " on :" + General.GetTimeServer());


        private static void OnRenamed(object sender, RenamedEventArgs e)
        {
            try
            {
                Console.WriteLine($"Renamed:");
                Console.WriteLine($"    Old: {e.OldFullPath}" + " on :" + Global.key);
                Console.WriteLine($"    New: {e.FullPath}" + " on :" + Global.key);
                var info = new FileInfo(e.FullPath);
                var info_old = new FileInfo(e.OldFullPath);
                var theSize = "";
                if (info.Extension != ".tmp")
                {
                    theSize = info.Length.ToString();

                }
                else
                {
                    theSize = "0";
                }

                string hasil = DataProcess.InsertFileMonitoring(e.FullPath.ToString(), info.Name, theSize.ToString(), Global.key, "Renamed", e.OldFullPath.ToString(), e.FullPath.ToString(), "leo", "DESKTOP-RVETU0U");
                Console.WriteLine(hasil);
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            

        }

        private static void OnError(object sender, ErrorEventArgs e) =>
            PrintException(e.GetException());

        private static void PrintException(Exception? ex)
        {
            if (ex != null)
            {
                Console.WriteLine($"Message: {ex.Message}");
                Console.WriteLine("Stacktrace:");
                Console.WriteLine(ex.StackTrace);
                Console.WriteLine();
                PrintException(ex.InnerException);
            }
        }


    }

    class Global
    {
        public static string key;

    }
}
