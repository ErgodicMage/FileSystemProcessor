﻿using System;
using System.IO;

namespace FileSystem
{
    public interface IFileSystemProcessor
    {
        #region Fluent API
        IFileSystemProcessor WithOptions(FindFilesOptions options);
        IFileSystemProcessor WithPath(string path);
        IFileSystemProcessor WithPattern(string pattern);
        IFileSystemProcessor WithRegexPattern(string regex);
        IFileSystemProcessor Recursive();
        IFileSystemProcessor WithFilter(Predicate<FileSystemInfo> filter);
        IFileSystemProcessor WithAction(Action<FileSystemInfo> action);
        #endregion

        FindFilesOptions Options { get; set; }
        Action<FileSystemInfo> Action { get; set; }
        void DoProcess();

    }
}
