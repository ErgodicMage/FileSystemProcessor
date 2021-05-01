using System.Collections.Generic;
using System.IO;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FileSystemUnitTests
{
    [TestClass]
    public class DirectoryEnumeratorTests
    {
        readonly string scanFolder = @"C:\Development"; //@"\\compservicesftp-test.ecomad.int\Projects"; // @"C:\"; // @"c:\Development\Temp";
        readonly string filesFileName = @"C:\Development\Temp\FileSystemProcessor\Files.txt";
        readonly string directoriesFileName = @"C:\Development\Temp\FileSystemProcessor\Directories.txt";
        readonly string filesystemFileName = @"C:\Development\Temp\FileSystemProcessor\FileSystem.txt";
        readonly string filesdirectoriesFileName = @"C:\Development\Temp\FileSystemProcessor\FilesDirectoriesSystem.txt";

        [TestMethod]
        public void EnumerateFiles()
        {
            if (File.Exists(filesFileName))
                File.Delete(filesFileName);

            var files = Directory.EnumerateFiles(scanFolder);
            IList<string> values = new List<string>();

            foreach (var f in files)
                values.Add(f);

            TestUtilities.WriteToFile(filesFileName, values);

            Assert.IsTrue(File.Exists(filesFileName));
        }

        [TestMethod]
        public void EnumerateFilesRecursive()
        {
            if (File.Exists(filesFileName))
                File.Delete(filesFileName);

            EnumerationOptions options = new EnumerationOptions();
            options.IgnoreInaccessible = true;
            options.RecurseSubdirectories = true;
            options.AttributesToSkip = FileAttributes.System;

            var files = Directory.EnumerateFiles(scanFolder, "*", options);

            IList<string> values = new List<string>();

            foreach (var f in files)
                values.Add(f);

            TestUtilities.WriteToFile(filesFileName, values);

            Assert.IsTrue(File.Exists(filesFileName));
        }


        [TestMethod]
        public void EnumerateDirectories()
        {
            if (File.Exists(directoriesFileName))
                File.Delete(directoriesFileName);

            var files = Directory.EnumerateDirectories(scanFolder);

            IList<string> values = new List<string>();

            foreach (var f in files)
                values.Add(f);

            TestUtilities.WriteToFile(directoriesFileName, values);

            Assert.IsTrue(File.Exists(directoriesFileName));
        }

        [TestMethod]
        public void EnumerateDirectoriesRecursive()
        {
            if (File.Exists(directoriesFileName))
                File.Delete(directoriesFileName);

            EnumerationOptions options = new EnumerationOptions();
            options.IgnoreInaccessible = true;
            options.RecurseSubdirectories = true;
            options.AttributesToSkip = FileAttributes.System;

            var files = Directory.EnumerateDirectories(scanFolder, "*", options);

            IList<string> values = new List<string>();

            foreach (var f in files)
                values.Add(f);

            TestUtilities.WriteToFile(directoriesFileName, values);

            Assert.IsTrue(File.Exists(directoriesFileName));
        }

        [TestMethod]
        public void EnumerateFileSystemEntries()
        {
            if (File.Exists(filesystemFileName))
                File.Delete(filesystemFileName);

            var files = Directory.EnumerateFileSystemEntries(scanFolder);

            IList<string> values = new List<string>();

            foreach (var f in files)
                values.Add(f);

            TestUtilities.WriteToFile(filesystemFileName, values);

            Assert.IsTrue(File.Exists(filesystemFileName));
        }

        [TestMethod]
        public void EnumerateFileSystemEntriesRecursive()
        {
            if (File.Exists(filesystemFileName))
                File.Delete(filesystemFileName);

            EnumerationOptions options = new EnumerationOptions();
            options.IgnoreInaccessible = true;
            options.RecurseSubdirectories = true;
            options.AttributesToSkip = FileAttributes.System;

            var files = Directory.EnumerateFileSystemEntries(scanFolder, "*", options);

            IList<string> values = new List<string>();

            foreach (var f in files)
                values.Add(f);

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

            IList<string> values = new List<string>();

            DoDirectory(values, scanFolder);

            TestUtilities.WriteToFile(filesdirectoriesFileName, values);
            Assert.IsTrue(File.Exists(filesdirectoriesFileName));
        }

        public void DoDirectory(IList<string> values, string directory, string searchPattern = "*")
        {
            var files = Directory.EnumerateFiles(directory, searchPattern, enumerateOptions);
            foreach (var f in files)
                values.Add(f);

            var directories = Directory.EnumerateDirectories(directory, searchPattern, enumerateOptions);
            foreach (var d in directories)
                DoDirectory(values, d);
        }
    }
}
