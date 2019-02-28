namespace BinaryRemover
{
    using System.Collections.Generic;

    public class FileManagerExtensions: IFileManagerExtension
    {
        public IEnumerable<string> GetFileExtensions()
        {
            foreach (var item in new[] { "exe", "cs", "dll", "json", "cache", "xml", "pdb", "config" })
            {
                yield return $".{item}";
            }
        }
    }
}