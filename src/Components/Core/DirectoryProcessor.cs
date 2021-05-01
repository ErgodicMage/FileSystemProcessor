using System;
using System.Collections.Generic;
using System.IO;

namespace FileSystem
{
    public class DirectoryProcessor
    {
        #region Constructors
        public DirectoryProcessor(string path)
        {
            fd = new FindFolders(path);
        }

        public DirectoryProcessor(FindFilesOptions options)
        {
            fd = new FindFolders(options);
        }

        public DirectoryProcessor(string path, string pattern, bool recursive, string regexpattern = "", Func<FileSystemInfo, bool> filter = null)
        {
            fd = new FindFolders(path, pattern, recursive, regexpattern, filter);
        }

        public DirectoryProcessor(FindFolders d)
        {
            fd = d;
        }
        #endregion

        #region Properties
        protected FindFolders fd;
        #endregion

        #region Methods
        public virtual void DoProcess(Action<FileSystemInfo> action)
        {
            if (action == null)
                throw new ArgumentNullException("action");

            foreach (FileSystemInfo fsi in fd.Enumerate())
            {
                // note this is assuming the action will handle any exception!
                action(fsi);
            }
        }
        #endregion
    }
}

