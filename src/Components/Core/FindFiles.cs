﻿
using System.Runtime.InteropServices;

namespace ErgodicMage.FileSystemProcessor;

public class FindFiles : IFindFileSystem
{
    #region Constructors
    public FindFiles()
    {
        Options = new FindFilesOptions();
    }

    public FindFiles(string path)
    {
        Options = new FindFilesOptions()
        {
            Path = path
        };
    }

    public FindFiles(FindFilesOptions options)
    {
        Options = options;
    }

    public FindFiles(string path, string pattern, bool recursive, string regexpattern = "", Predicate<FileSystemInfo>? filter = null)
    {
        Options = new()
        {
            Path = path,
            Pattern = pattern,
            RegExPattern = regexpattern,
            Filter = filter,
            Options = new EnumerationOptions() { RecurseSubdirectories = recursive }
        };
    }

    #endregion

    #region Fluent API
    public IFindFileSystem WithOptions(FindFilesOptions options)
    {
        Options = options;
        return this;
    }

    public IFindFileSystem WithPath(string path)
    {
        Options.Path = path;
        return this;
    }

    public IFindFileSystem WithPattern(string pattern)
    {
        Options.Pattern = pattern;
        return this;
    }

    public IFindFileSystem WithRegexPattern(string regex)
    {
        Options.RegExPattern = regex;
        return this;            
    }

    public IFindFileSystem Recursive()
    {
        Options.Recursive = true;
        return this;            
    }

    public IFindFileSystem WithFilter(Predicate<FileSystemInfo> filter)
    {
        Options.Filter += filter;
        return this;            
    }
    #endregion

    #region Properties
    public FindFilesOptions Options { get; set; }
    #endregion

    #region Public Methods
    public IEnumerable<FileSystemInfo> Enumerate() => GetEnumerableFileSystemInfo.GetEnumerable(Options, EnumerateType.Files);
    #endregion
}
