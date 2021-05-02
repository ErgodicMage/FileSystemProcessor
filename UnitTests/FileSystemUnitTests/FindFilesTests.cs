using System.Collections.Generic;
using System.IO;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using FileSystem;

namespace FileSystemUnitTests
{
    [TestClass]
    public class FindFilesTests
    {
        public FindFilesTests()
        {
            TestUtilities.LoadAppSettings();
        }

        [TestMethod]
        [TestCategory(TestCategories.FunctionalTest)]
        public void FindFilesTest()
        {
            FindFiles ff = new FindFiles(TestUtilities.Config["ScanFolder"]);

            IList<string> values = TestUtilities.RunEnumerationToList(ff);

            TestUtilities.WriteToFile(TestUtilities.Config["FilesFileName"], values);

            Assert.IsTrue(File.Exists(TestUtilities.Config["FilesFileName"]));
        }

        [TestMethod]
        [TestCategory(TestCategories.FunctionalTest)]
        public void FindFilesRecursiveTest()
        {
            FindFilesOptions options = new FindFilesOptions() 
            { 
                Path = TestUtilities.Config["ScanFolder"], 
                Pattern = "*", 
                Recursive = true 
            };
            FindFiles ff = new FindFiles(options);

            IList<string> values = TestUtilities.RunEnumerationToList(ff);

            TestUtilities.WriteToFile(TestUtilities.Config["FilesFileName"], values);

            Assert.IsTrue(File.Exists(TestUtilities.Config["FilesFileName"]));
        }

        [TestMethod]
        [TestCategory(TestCategories.FunctionalTest)]
        public void FindFilesRegexTest()
        {
            FindFilesOptions options = new FindFilesOptions() 
            { 
                Path = TestUtilities.Config["ScanFolder"], 
                Pattern = "*", 
                RegExPattern = @".+\.pdf$", 
                Recursive = true 
            };
            FindFiles ff = new FindFiles(options);

            IList<string> values = TestUtilities.RunEnumerationToList(ff);

            TestUtilities.WriteToFile(TestUtilities.Config["FilesFileName"], values);

            Assert.IsTrue(File.Exists(TestUtilities.Config["FilesFileName"]));
        }

        [TestMethod]
        [TestCategory(TestCategories.FunctionalTest)]
        public void FindFilesFilterIsPDFTest()
        {
            FindFilesOptions options = new FindFilesOptions() 
            { 
                Path = TestUtilities.Config["ScanFolder"], 
                Pattern = "*", 
                Recursive = true, 
                Filter = TestUtilities.FileIsPDF 
            };
            FindFiles ff = new FindFiles(options);

            IList<string> values = TestUtilities.RunEnumerationToList(ff);

            TestUtilities.WriteToFile(TestUtilities.Config["FilesFileName"], values);

            Assert.IsTrue(File.Exists(TestUtilities.Config["FilesFileName"]));
        }

        [TestMethod]
        [TestCategory(TestCategories.FunctionalTest)]
        public void FindFilesFilter1MBTest()
        {
            FindFilesOptions options = new FindFilesOptions() 
            { 
                Path = TestUtilities.Config["ScanFolder"], 
                Pattern = "*", 
                Recursive = true, 
                Filter=TestUtilities.FileGreaterThan1MB 
            };
            FindFiles ff = new FindFiles(options);

            IList<string> values = TestUtilities.RunEnumerationToList(ff);

            TestUtilities.WriteToFile(TestUtilities.Config["FilesFileName"], values);

            Assert.IsTrue(File.Exists(TestUtilities.Config["FilesFileName"]));
        }

        [TestMethod]
        [TestCategory(TestCategories.FunctionalTest)]
        public void FindFilesFilterPDF1MBTest()
        {
            FindFilesOptions options = new FindFilesOptions() 
            { 
                Path = TestUtilities.Config["ScanFolder"], 
                Pattern = "*", 
                Recursive = true, 
                Filter = TestUtilities.FileIsPDFGreaterThan1MB 
            };
            FindFiles ff = new FindFiles(options);

            IList<string> values = TestUtilities.RunEnumerationToList(ff);

            TestUtilities.WriteToFile(TestUtilities.Config["FilesFileName"], values);

            Assert.IsTrue(File.Exists(TestUtilities.Config["FilesFileName"]));
        }
    }
}
