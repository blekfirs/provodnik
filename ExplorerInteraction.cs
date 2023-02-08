using System.IO;

namespace PR7
{
    static class ExplorerInteraction
    {
        public static void CreateFile(string path)=>
            File.Create(path);
        public static void CreateFolder(string path) =>
            Directory.CreateDirectory(path);
        public static void Delete(string path)
        {
            if (Path.HasExtension(path))
                File.Delete(path);
            else
                Directory.Delete(path);
        }
    }
}
