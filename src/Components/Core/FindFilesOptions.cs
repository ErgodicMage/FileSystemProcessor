using System;
using System.IO;

namespace FileSystem
{
    public class FindFilesOptions
    {
        public FindFilesOptions()
        {
            Pattern = "*";
        }
        public string Path { get; set; }
        public string Pattern { get; set; }
        public string RegExPattern { get; set; }
        public bool Recursive { get; set; }
        public Predicate<FileSystemInfo> Filter {get;set; }
        public EnumerationOptions Options { get; set; }

        internal readonly static EnumerationOptions DefaultEnumerationOptions = new EnumerationOptions() { IgnoreInaccessible = true, AttributesToSkip = FileAttributes.Hidden | FileAttributes.System };
    }
}
