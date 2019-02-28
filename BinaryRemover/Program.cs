namespace BinaryRemover
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;

    class Program
    {
        static void Main(string[] args)
        {
            var directoryRootPath = args[0];

            var directoryInfo = new DirectoryInfo(directoryRootPath);

            var binaryDirectories = GetBinaryDirectories(directoryInfo);
            var binaryFilesToRemove = GetFilesToRemoveFromDirectories(binaryDirectories).ToList();

            RemoveFiles(binaryFilesToRemove);
        }

        private static IEnumerable<DirectoryInfo> GetBinaryDirectories(DirectoryInfo directoryInfo) =>
            directoryInfo.GetDirectories("*", SearchOption.AllDirectories)
              .Where(q => q.FullName.Contains("bin\\Debug", StringComparison.InvariantCultureIgnoreCase) ||
                          q.FullName.Contains("bin\\Release", StringComparison.InvariantCultureIgnoreCase) ||
                          q.FullName.Contains("bin", StringComparison.InvariantCultureIgnoreCase) ||
                          q.FullName.Contains("obj", StringComparison.InvariantCultureIgnoreCase));

        private static IEnumerable<FileInfo> GetFilesToRemoveFromDirectories(IEnumerable<DirectoryInfo> binaryDirectories) =>
             binaryDirectories.SelectMany(d => d.GetFiles()).Where(f => IsRemovableExtension(f));

        private static bool IsRemovableExtension(FileInfo file)
        {
            var fileManagerExtension = DependencyContainer.Get<IFileManagerExtension>();

            return fileManagerExtension
                .GetFileExtensions()
                .Contains(
                    Path.GetExtension(file.FullName));
        }

        private static void RemoveFiles(IEnumerable<FileInfo> files)
        {
            foreach (var file in files)
            {
                try
                {
                    File.Delete(file.FullName);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.StackTrace);
                }
            }
        }
    }
}
