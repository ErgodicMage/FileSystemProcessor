using System;
using System.IO;

namespace FileSystem
{
    public class FileProcessor
    {
        #region Constructors
        public FileProcessor(string path)
        {
            _findfiles = new FindFiles(path);
        }

        public FileProcessor(FindFilesOptions options)
        {
            _findfiles = new FindFiles(options);
        }

        public FileProcessor(string path, string pattern, bool recursive, string regexpattern = "", Predicate<FileSystemInfo> filter = null)
        {
            _findfiles = new FindFiles(path, pattern, recursive, regexpattern, filter);
        }

        public FileProcessor(FindFiles ff)
        {
            _findfiles = ff;
        }
        #endregion

        #region Properties
        protected FindFiles _findfiles;
        #endregion

        #region Methods
        public virtual void DoProcess(Action<FileSystemInfo> action)
        {
            if (action == null)
                throw new ArgumentNullException("action");

            foreach (FileSystemInfo fsi in _findfiles.Enumerate())
            {
                // note this is assuming the action will handle any exception!
                action(fsi);
            }
        }
        #endregion
    }
}
