using System;
using System.Collections.Generic;
using System.IO;

namespace FileSystem
{
    public interface IFindFileSystem
    {
        #region FluentAPI
        IFindFileSystem WithOptions(FindFilesOptions options);
        IFindFileSystem WithPath(string path);
        IFindFileSystem WithPattern(string pattern);
        IFindFileSystem WithRegexPattern(string regex);
        IFindFileSystem Recursive();
        IFindFileSystem AddFilter(Func<FileSystemInfo, bool> filter);

        #endregion

        FindFilesOptions Options { get; set; }
        IEnumerable<FileSystemInfo> Enumerate();
    }
}
