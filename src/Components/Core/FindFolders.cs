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
        public FindFolders(string path)
        {
            Options = new FindFilesOptions();
            Options.Path = path;
        }

        public FindFolders(FindFilesOptions options)
        {
            Options = options;
        }

        public FindFolders(string path, string pattern, bool recursive, string regexpattern = "", Func<FileSystemInfo, bool> filter = null)
        {
            Options = new FindFilesOptions() { Path = path, Pattern = pattern, RegExPattern = regexpattern, Filter = filter };
            Options.Options = new EnumerationOptions() { RecurseSubdirectories = recursive };

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
