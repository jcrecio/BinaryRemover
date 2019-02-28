namespace BinaryRemover
{
    using System.Collections.Generic;

    public interface IFileManagerExtension
    {
        IEnumerable<string> GetFileExtensions();
    }
}