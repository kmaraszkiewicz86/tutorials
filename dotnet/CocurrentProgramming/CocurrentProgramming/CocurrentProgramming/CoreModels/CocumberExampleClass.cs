using System;
using System.IO;
using System.Threading.Tasks;

namespace CocurrentProgramming.CoreModels
{
    public class CocumberExampleClass
    {
        private static object _locker = new object();

        private string DirectoryPath => Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Personal), "Library", "Developer", "Repo", "InputFiles");

        private string FilePath => Path.Combine(DirectoryPath, "test.txt");

        public async Task Write(int currentThreadId)
        {
            await Task.Run(() =>
            {
                Directory.CreateDirectory(DirectoryPath);

                lock (_locker)
                {
                    using (var writer = new StreamWriter(FilePath))
                    {
                        for (var index = 0; index < 10000; index++)
                        {
                            writer.WriteLine($"{currentThreadId}: {index}");
                        }
                    }
                }
            });
        }

        public string Read()
        {
            lock (_locker)
            {
                if (!File.Exists(FilePath))
                    return "b/d";

                return File.ReadAllText(FilePath);
            }
        }
    }
}