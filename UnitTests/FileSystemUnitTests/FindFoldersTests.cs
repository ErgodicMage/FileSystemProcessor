using System.Collections.Generic;
using System.IO;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using FileSystem;

namespace FileSystemUnitTests
{
    [TestClass]
    public class FindFoldersTests
    {
        public FindFoldersTests()
        {
            TestUtilities.LoadAppSettings();
        }

        [TestMethod]
        [TestCategory(TestCategories.FunctionalTest)]
        public void FindDirectoriesTest()
        {            
            IFindFileSystem fd = new FindFolders()
                                    .WithPath(TestUtilities.Config["ScanFolder"]);

            IList<string> values = TestUtilities.RunEnumerationToList(fd);

            TestUtilities.WriteToFile(TestUtilities.Config["DirectoriesFileName"], values);

            Assert.IsTrue(File.Exists(TestUtilities.Config["DirectoriesFileName"]));
        }

        [TestMethod]
        [TestCategory(TestCategories.FunctionalTest)]
        public void FindDirectoriesRecursiveTest()
        {
            IFindFileSystem fd = new FindFolders()
                                    .WithPath(TestUtilities.Config["ScanFolder"])
                                    .WithPattern("*")
                                    .Recursive();

            IList<string> values = TestUtilities.RunEnumerationToList(fd);

            TestUtilities.WriteToFile(TestUtilities.Config["DirectoriesFileName"], values);

            Assert.IsTrue(File.Exists(TestUtilities.Config["DirectoriesFileName"]));
        }

        [TestMethod]
        [TestCategory(TestCategories.FunctionalTest)]
        public void FindDirectoriesRegexTest()
        {
            IFindFileSystem fd = new FindFolders()
                                    .WithPath(TestUtilities.Config["ScanFolder"])
                                    .WithRegexPattern(@"\b(\w*FileSystemProcessor\w*)\b")
                                    .Recursive();

            IList<string> values = TestUtilities.RunEnumerationToList(fd);

            TestUtilities.WriteToFile(TestUtilities.Config["DirectoriesFileName"], values);

            Assert.IsTrue(File.Exists(TestUtilities.Config["DirectoriesFileName"]));
        }


        [TestMethod]
        [TestCategory(TestCategories.FunctionalTest)]
        public void FindDirectoriesFilterIsFileSystemProcessorTest()
        {
            IFindFileSystem fd = new FindFolders()
                                    .WithPath(TestUtilities.Config["ScanFolder"])
                                    .WithPattern("*")
                                    .Recursive()
                                    .AddFilter(TestUtilities.IsFileSystemProcessor);

            IList<string> values = TestUtilities.RunEnumerationToList(fd);

            TestUtilities.WriteToFile(TestUtilities.Config["DirectoriesFileName"], values);

            Assert.IsTrue(File.Exists(TestUtilities.Config["DirectoriesFileName"]));
        }

        [TestMethod]
        [TestCategory(TestCategories.FunctionalTest)]
        public void FindDirectoriesFilterIsFileSystemProcessorOrStellarMapTest()
        {
            FindFilesOptions options = new FindFilesOptions() 
            {
                Path = TestUtilities.Config["ScanFolder"], 
                Pattern = "*", 
                Recursive = true, 
                Filter = TestUtilities.IsFileSystemProcessorOrStellarMap 
            };
            IFindFileSystem fd = new FindFolders()
                                    .WithPath(TestUtilities.Config["ScanFolder"])
                                    .WithPattern("*")
                                    .Recursive()
                                    .AddFilter(TestUtilities.IsFileSystemProcessorOrStellarMap);

            IList<string> values = TestUtilities.RunEnumerationToList(fd);

            TestUtilities.WriteToFile(TestUtilities.Config["DirectoriesFileName"], values);

            Assert.IsTrue(File.Exists(TestUtilities.Config["DirectoriesFileName"]));
        }

        [TestMethod]
        [TestCategory(TestCategories.FunctionalTest)]
        public void FindDirectoriesQRCodesTest()
        {
            FindFilesOptions options = new FindFilesOptions() 
            { 
                Path = TestUtilities.Config["ScanFolder"], 
                Pattern = "*", 
                Recursive = true, 
                Filter = TestUtilities.DirectoryHasQRCodes 
            };
            FindFolders fd = new FindFolders(options);

            IList<string> values = TestUtilities.RunEnumerationToList(fd); ;

            TestUtilities.WriteToFile(TestUtilities.Config["DirectoriesFileName"], values);

            Assert.IsTrue(File.Exists(TestUtilities.Config["DirectoriesFileName"]));
        }
    }
}
