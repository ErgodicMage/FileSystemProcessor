using System.Collections.Generic;
using System.IO;

namespace FileSystem
{
    public interface IFindFileSystem
    {
        FindFilesOptions Options { get; set; }
        IEnumerable<FileSystemInfo> Enumerate();
    }
}
