using System.Collections.Generic;
using System.IO;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FileSystemUnitTests
{
    [TestClass]
    public class DirectoryEnumeratorTests
    {
        public DirectoryEnumeratorTests()
        {
            TestUtilities.LoadAppSettings();
        }

        [TestMethod]
        [TestCategory(TestCategories.FunctionalTest)]
        public void EnumerateFiles()
        {
            if (File.Exists(TestUtilities.Config["FilesFileName"]))
                File.Delete(TestUtilities.Config["FilesFileName"]);

            var files = Directory.EnumerateFiles(TestUtilities.Config["ScanFolder"]);
            IList<string> values = new List<string>();

            foreach (var f in files)
                values.Add(f);

            TestUtilities.WriteToFile(TestUtilities.Config["FilesFileName"], values);

            Assert.IsTrue(File.Exists(TestUtilities.Config["FilesFileName"]));
        }

        [TestMethod]
        [TestCategory(TestCategories.FunctionalTest)]
        public void EnumerateFilesRecursive()
        {
            if (File.Exists(TestUtilities.Config["FilesFileName"]))
                File.Delete(TestUtilities.Config["FilesFileName"]);

            EnumerationOptions options = new EnumerationOptions();
            options.IgnoreInaccessible = true;
            options.RecurseSubdirectories = true;
            options.AttributesToSkip = FileAttributes.System;

            var files = Directory.EnumerateFiles(TestUtilities.Config["ScanFolder"], "*", options);

            IList<string> values = new List<string>();

            foreach (var f in files)
                values.Add(f);

            TestUtilities.WriteToFile(TestUtilities.Config["FilesFileName"], values);

            Assert.IsTrue(File.Exists(TestUtilities.Config["FilesFileName"]));
        }


        [TestMethod]
        [TestCategory(TestCategories.FunctionalTest)]
        public void EnumerateDirectories()
        {
            if (File.Exists(TestUtilities.Config["DirectoriesFileName"]))
                File.Delete(TestUtilities.Config["DirectoriesFileName"]);

            var files = Directory.EnumerateDirectories(TestUtilities.Config["ScanFolder"]);

            IList<string> values = new List<string>();

            foreach (var f in files)
                values.Add(f);

            TestUtilities.WriteToFile(TestUtilities.Config["DirectoriesFileName"], values);

            Assert.IsTrue(File.Exists(TestUtilities.Config["DirectoriesFileName"]));
        }

        [TestMethod]
        [TestCategory(TestCategories.FunctionalTest)]
        public void EnumerateDirectoriesRecursive()
        {
            if (File.Exists(TestUtilities.Config["DirectoriesFileName"]))
                File.Delete(TestUtilities.Config["DirectoriesFileName"]);

            EnumerationOptions options = new EnumerationOptions();
            options.IgnoreInaccessible = true;
            options.RecurseSubdirectories = true;
            options.AttributesToSkip = FileAttributes.System;

            var files = Directory.EnumerateDirectories(TestUtilities.Config["ScanFolder"], "*", options);

            IList<string> values = new List<string>();

            foreach (var f in files)
                values.Add(f);

            TestUtilities.WriteToFile(TestUtilities.Config["DirectoriesFileName"], values);

            Assert.IsTrue(File.Exists(TestUtilities.Config["DirectoriesFileName"]));
        }

        [TestMethod]
        [TestCategory(TestCategories.FunctionalTest)]
        public void EnumerateFileSystemEntries()
        {
            if (File.Exists(TestUtilities.Config["FileSystemFileName"]))
                File.Delete(TestUtilities.Config["FileSystemFileName"]);

            var files = Directory.EnumerateFileSystemEntries(TestUtilities.Config["ScanFolder"]);

            IList<string> values = new List<string>();

            foreach (var f in files)
                values.Add(f);

            TestUtilities.WriteToFile(TestUtilities.Config["FileSystemFileName"], values);

            Assert.IsTrue(File.Exists(TestUtilities.Config["FileSystemFileName"]));
        }

        [TestMethod]
        [TestCategory(TestCategories.FunctionalTest)]
        public void EnumerateFileSystemEntriesRecursive()
        {
            if (File.Exists(TestUtilities.Config["FileSystemFileName"]))
                File.Delete(TestUtilities.Config["FileSystemFileName"]);

            EnumerationOptions options = new EnumerationOptions();
            options.IgnoreInaccessible = true;
            options.RecurseSubdirectories = true;
            options.AttributesToSkip = FileAttributes.System;

            var files = Directory.EnumerateFileSystemEntries(TestUtilities.Config["ScanFolder"], "*", options);

            IList<string> values = new List<string>();

            foreach (var f in files)
                values.Add(f);

            TestUtilities.WriteToFile(TestUtilities.Config["FileSystemFileName"], values);

            Assert.IsTrue(File.Exists(TestUtilities.Config["FileSystemFileName"]));
        }


        EnumerationOptions enumerateOptions = new EnumerationOptions();

        [TestMethod]
        [TestCategory(TestCategories.FunctionalTest)]
        public void DoFilesDirecories()
        {
            if (File.Exists(TestUtilities.Config["FilesDirectoriesFileName"]))
                File.Delete(TestUtilities.Config["FilesDirectoriesFileName"]);
            enumerateOptions.IgnoreInaccessible = true;

            IList<string> values = new List<string>();

            DoDirectory(values, TestUtilities.Config["ScanFolder"]);

            TestUtilities.WriteToFile(TestUtilities.Config["FilesDirectoriesFileName"], values);
            Assert.IsTrue(File.Exists(TestUtilities.Config["FilesDirectoriesFileName"]));
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
