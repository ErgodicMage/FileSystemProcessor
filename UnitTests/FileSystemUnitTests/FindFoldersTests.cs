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
            FindFolders fd = new FindFolders(TestUtilities.Config["ScanFolder"]);

            IList<string> values = TestUtilities.RunEnumerationToList(fd);

            TestUtilities.WriteToFile(TestUtilities.Config["DirectoriesFileName"], values);

            Assert.IsTrue(File.Exists(TestUtilities.Config["DirectoriesFileName"]));
        }

        [TestMethod]
        [TestCategory(TestCategories.FunctionalTest)]
        public void FindDirectoriesRecursiveTest()
        {
            FindFilesOptions options = new FindFilesOptions() 
            { 
                Path = TestUtilities.Config["ScanFolder"], 
                Pattern = "*", 
                Recursive = true 
            };
            FindFolders fd = new FindFolders(options);

            IList<string> values = TestUtilities.RunEnumerationToList(fd);

            TestUtilities.WriteToFile(TestUtilities.Config["DirectoriesFileName"], values);

            Assert.IsTrue(File.Exists(TestUtilities.Config["DirectoriesFileName"]));
        }

        [TestMethod]
        [TestCategory(TestCategories.FunctionalTest)]
        public void FindDirectoriesRegexTest()
        {
            FindFilesOptions options = new FindFilesOptions() 
            { 
                Path = TestUtilities.Config["ScanFolder"], 
                Pattern = "*", 
                Recursive = true, 
                RegExPattern=@"\b(\w*FileSystemProcessor\w*)\b" 
            };
            FindFolders fd = new FindFolders(options);

            IList<string> values = TestUtilities.RunEnumerationToList(fd);

            TestUtilities.WriteToFile(TestUtilities.Config["DirectoriesFileName"], values);

            Assert.IsTrue(File.Exists(TestUtilities.Config["DirectoriesFileName"]));
        }


        [TestMethod]
        [TestCategory(TestCategories.FunctionalTest)]
        public void FindDirectoriesFilterIsFileSystemProcessorTest()
        {
            FindFilesOptions options = new FindFilesOptions() 
            { 
                Path = TestUtilities.Config["ScanFolder"], 
                Pattern = "*", 
                Recursive = true, 
                Filter=TestUtilities.IsFileSystemProcessor 
            };
            FindFolders fd = new FindFolders(options);

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
            FindFolders fd = new FindFolders(options);

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
