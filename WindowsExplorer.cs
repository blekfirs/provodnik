using System;
using System.Diagnostics;
using System.IO;
using System.Linq;

namespace PR7
{
    static class WindowsExplorer
    {
        //распечатать и вернуть список папок и файлов
        public static string[] GetAndShowDirectoryContent(string path)
        {
            Console.Clear();
            if (path == null)
                return GetAndShowDrives();
            DirectoryInfo info = new DirectoryInfo(path);

            Console.WriteLine(
                new string(' ', 50) + 
                "Папка " +info.Name + '\n' + 
                new string('=', 76));

            Console.WriteLine("   " + "{0,40}|{1,20}|{2,10}\n{3}", "Название", "Дата создания", "Тип", new string('-',76));

            DirectoryInfo[] dirs = info.GetDirectories();
            foreach (var dir in dirs)
                Console.WriteLine("   {0,-40}|{1,-20}|{2,10}", Shorten(dir.Name, 40), dir.CreationTime, string.Empty);

            FileInfo[] files = info.GetFiles();
            foreach (var file in files)
                Console.WriteLine("   {0,-40}|{1,-20}|{2,-10}", Shorten(file.Name, 40), file.CreationTime, Shorten(file.Extension, 10));

            return dirs.Select(x => x.FullName).Concat(files.Select(x=>x.FullName)).ToArray();
        }

        //укоротить строку
        private static string Shorten(string name, int max_len)
        {
            if (name.Length <= max_len)
                return name;
            return name.Remove(max_len-3) + "...";
        }

        //распечатать и вернуть список дисков
        public static string[] GetAndShowDrives()
        {
            DriveInfo[] drives = DriveInfo.GetDrives();
            Console.WriteLine(
                new string(' ', 50) +
                "Диски" + '\n' +
                new string('=', 76));

            foreach (var drive in drives)
                Console.WriteLine($"   {drive.Name} {BytesToGbytes(drive.TotalFreeSpace)} ГБ " +
                    $"свободно из {BytesToGbytes(drive.TotalSize)} ГБ");

            return drives.Select(x => x.Name).ToArray();
        }

        //байты в гигабайты
        private static int BytesToGbytes(long bytes) =>
            (int)(bytes / Math.Pow(1024, 3));

        //открывает папку/файл
        public static string[] Enter(string path)
        {
            string[] paths = null;
            if (path!=null && Path.HasExtension(path))
                Process.Start(path);
            else
                paths = GetAndShowDirectoryContent(path);
            return paths;
        }
        //возвращает родителя каталога
        public static string GetParentRoot(string path)
        {
            if (path.Count(x => x == '\\') == 1)
                return null;
            else
                return 
                    path.Remove(path.LastIndexOf('\\'));
        }
    }
}
