using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
namespace FileSystem
{
    public class FindFolders : IFindFileSystem
    {
        #region Constructors
        public FindFolders()
        {
            Options = new FindFilesOptions();
        }

        public FindFolders(string path)
        {
            Options = new FindFilesOptions();
            Options.Path = path;
        }

        public FindFolders(FindFilesOptions options)
        {
            Options = options;
        }

        public FindFolders(string path, string pattern, bool recursive, string regexpattern = "", Predicate<FileSystemInfo> filter = null)
        {
            Options = new FindFilesOptions() { Path = path, Pattern = pattern, RegExPattern = regexpattern, Filter = filter };
            Options.Options = new EnumerationOptions() { RecurseSubdirectories = recursive };

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
            if (Options.Options == null)
                Options.Options = new EnumerationOptions();
            Options.Options.RecurseSubdirectories = true;
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

        #region Public Functions
        public IEnumerable<FileSystemInfo> Enumerate()
        {
            DirectoryInfo directoryinfo = new DirectoryInfo(Options.Path);

            EnumerationOptions enumerationoptions = Options.Options;
            if (enumerationoptions == null)
            {
                if (!Options.Recursive)
                    enumerationoptions = FindFilesOptions.DefaultEnumerationOptions;
                else
                {
                    enumerationoptions = new EnumerationOptions()
                    {
                        AttributesToSkip = FindFilesOptions.DefaultEnumerationOptions.AttributesToSkip,
                        IgnoreInaccessible = FindFilesOptions.DefaultEnumerationOptions.IgnoreInaccessible,
                        RecurseSubdirectories = true
                    };
                }
            }

            IEnumerable<FileSystemInfo> enumerable = null;

            if (string.IsNullOrEmpty(Options.RegExPattern) && Options.Filter == null)
                enumerable = (IEnumerable<FileSystemInfo>)directoryinfo.EnumerateDirectories(Options.Pattern, enumerationoptions);
            else if (!string.IsNullOrEmpty(Options.RegExPattern) && Options.Filter == null)
            {
                Regex regex = new Regex(Options.RegExPattern);
                enumerable = (IEnumerable<FileSystemInfo>)directoryinfo.EnumerateDirectories(Options.Pattern, enumerationoptions)
                    .Where(file => regex.IsMatch(file.FullName));
            }
            else if (string.IsNullOrEmpty(Options.RegExPattern) && Options.Filter != null)
            {
                enumerable = (IEnumerable<FileSystemInfo>)directoryinfo.EnumerateDirectories(Options.Pattern, enumerationoptions)
                    .Where(file => Options.Filter(file));
            }
            else if (!string.IsNullOrEmpty(Options.RegExPattern) && Options.Filter != null)
            {
                Regex regex = new Regex(Options.RegExPattern);
                enumerable = (IEnumerable<FileSystemInfo>)directoryinfo.EnumerateDirectories(Options.Pattern, enumerationoptions)
                    .Where(file => regex.IsMatch(file.FullName) && Options.Filter(file));
            }

            return enumerable;
        }
        #endregion
    }
}
