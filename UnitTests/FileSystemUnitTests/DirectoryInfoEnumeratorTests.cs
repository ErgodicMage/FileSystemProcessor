using System.Collections.Generic;
using System.IO;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FileSystemUnitTests
{
    [TestClass]
    public class DirectoryInfoEnumeratorTests
    {
        readonly string scanFolder = @"\\compservicesftp-test.ecomad.int\Projects"; //@"C:\"; // @"c:\Development\Temp";
        readonly string filesFileName = @"C:\Development\Temp\FileSystemProcessor\Files.txt";
        readonly string directoriesFileName = @"C:\Development\Temp\FileSystemProcessor\Directories.txt";
        readonly string filesystemFileName = @"C:\Development\Temp\FileSystemProcessor\FileSystem.txt";
        readonly string filesdirectoriesFileName = @"C:\Development\Temp\FileSystemProcessor\FilesDirectoriesSystem.txt";

        [TestMethod]
        public void EnumerateFiles()
        {
            if (File.Exists(filesFileName))
                File.Delete(filesFileName);

            DirectoryInfo info = new DirectoryInfo(scanFolder);
            var files = info.EnumerateFiles();
            
            IList<string> values = new List<string>();
            foreach (var f in files)
                values.Add(f.FullName);

            TestUtilities.WriteToFile(filesFileName, values);

            Assert.IsTrue(File.Exists(filesFileName));
        }

        [TestMethod]
        public void EnumerateFilesRecursive()
        {
            if (File.Exists(filesFileName))
                File.Delete(filesFileName);

            DirectoryInfo info = new DirectoryInfo(scanFolder);
            EnumerationOptions options = new EnumerationOptions();
            options.IgnoreInaccessible = true;
            options.RecurseSubdirectories = true;
            options.AttributesToSkip = FileAttributes.System;

            var files = info.EnumerateFiles("*", options);

            IList<string> values = new List<string>();
            foreach (var f in files)
                values.Add(f.FullName);

            TestUtilities.WriteToFile(filesFileName, values);

            Assert.IsTrue(File.Exists(filesFileName));
        }


        [TestMethod]
        public void EnumerateDirectories()
        {
            if (File.Exists(directoriesFileName))
                File.Delete(directoriesFileName);

            DirectoryInfo info = new DirectoryInfo(scanFolder);
            var files = info.EnumerateDirectories();

            IList<string> values = new List<string>();
            foreach (var f in files)
                values.Add(f.FullName);

            TestUtilities.WriteToFile(directoriesFileName, values);

            Assert.IsTrue(File.Exists(directoriesFileName));
        }

        [TestMethod]
        public void EnumerateDirectoriesRecursive()
        {
            if (File.Exists(directoriesFileName))
                File.Delete(directoriesFileName);

            DirectoryInfo info = new DirectoryInfo(scanFolder);
            EnumerationOptions options = new EnumerationOptions();
            options.IgnoreInaccessible = true;
            options.RecurseSubdirectories = true;
            options.AttributesToSkip = FileAttributes.System;

            var files = info.EnumerateDirectories("*", options);

            IList<string> values = new List<string>();
            foreach (var f in files)
                values.Add(f.FullName);

            TestUtilities.WriteToFile(directoriesFileName, values);

            Assert.IsTrue(File.Exists(directoriesFileName));
        }

        [TestMethod]
        public void EnumerateFileSystemEntries()
        {
            if (File.Exists(filesystemFileName))
                File.Delete(filesystemFileName);

            DirectoryInfo info = new DirectoryInfo(scanFolder);
            var files = info.EnumerateFileSystemInfos();

            IList<string> values = new List<string>();
            foreach (var f in files)
                values.Add(f.FullName);

            TestUtilities.WriteToFile(filesystemFileName, values);

            Assert.IsTrue(File.Exists(filesystemFileName));
        }

        [TestMethod]
        public void EnumerateFileSystemEntriesRecursive()
        {
            if (File.Exists(filesystemFileName))
                File.Delete(filesystemFileName);

            DirectoryInfo info = new DirectoryInfo(scanFolder);
            EnumerationOptions options = new EnumerationOptions();
            options.IgnoreInaccessible = true;
            options.RecurseSubdirectories = true;
            options.AttributesToSkip = FileAttributes.System;

            var files = info.EnumerateFileSystemInfos("*", options);

            IList<string> values = new List<string>();
            foreach (var f in files)
                values.Add(f.FullName);

            TestUtilities.WriteToFile(filesystemFileName, values);

            Assert.IsTrue(File.Exists(filesystemFileName));
        }


        EnumerationOptions enumerateOptions = new EnumerationOptions();

        [TestMethod]
        public void DoFilesDirecories()
        {
            if (File.Exists(filesdirectoriesFileName))
                File.Delete(filesdirectoriesFileName);

            enumerateOptions.IgnoreInaccessible = true;
            //enumerateOptions.RecurseSubdirectories = false;
            //enumerateOptions.AttributesToSkip = FileAttributes.System;

            DirectoryInfo info = new DirectoryInfo(scanFolder);

            IList<string> values = new List<string>();
                
            DoDirectory(values, info);

            TestUtilities.WriteToFile(filesdirectoriesFileName, values);

            Assert.IsTrue(File.Exists(filesdirectoriesFileName));
        }

        public void DoDirectory(IList<string> values, DirectoryInfo info, string searchPattern="*")
        {
            var files = info.EnumerateFiles(searchPattern, enumerateOptions);
            foreach (var f in files)
                values.Add(f.FullName);

            var directories = info.EnumerateDirectories(searchPattern, enumerateOptions);
            foreach (var d in directories)
                DoDirectory(values, d);
        }
    }
}
