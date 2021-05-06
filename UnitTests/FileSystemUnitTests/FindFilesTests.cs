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
            IFindFileSystem ff = new FindFiles().WithPath(TestUtilities.Config["ScanFolder"]);

            IList<string> values = TestUtilities.RunEnumerationToList(ff);

            TestUtilities.WriteToFile(TestUtilities.Config["FilesFileName"], values);

            Assert.IsTrue(File.Exists(TestUtilities.Config["FilesFileName"]));
        }

        [TestMethod]
        [TestCategory(TestCategories.FunctionalTest)]
        public void FindFilesRecursiveTest()
        {            
            IFindFileSystem ff = new FindFiles()
                                    .WithPath(TestUtilities.Config["ScanFolder"])
                                    .Recursive();

            IList<string> values = TestUtilities.RunEnumerationToList(ff);

            TestUtilities.WriteToFile(TestUtilities.Config["FilesFileName"], values);

            Assert.IsTrue(File.Exists(TestUtilities.Config["FilesFileName"]));
        }

        [TestMethod]
        [TestCategory(TestCategories.FunctionalTest)]
        public void FindFilesPDFPatternTest()
        {            
            IFindFileSystem ff = new FindFiles()
                                    .WithPath(TestUtilities.Config["ScanFolder"])
                                    .WithPattern("*.pdf")
                                    .Recursive();

            IList<string> values = TestUtilities.RunEnumerationToList(ff);

            TestUtilities.WriteToFile(TestUtilities.Config["FilesFileName"], values);

            Assert.IsTrue(File.Exists(TestUtilities.Config["FilesFileName"]));
        }

        [TestMethod]
        [TestCategory(TestCategories.FunctionalTest)]
        public void FindFilesRegexTest()
        {
            IFindFileSystem ff = new FindFiles()
                                    .WithPath(TestUtilities.Config["ScanFolder"])
                                    .WithRegexPattern(@".+\.pdf$")
                                    .Recursive();

            IList<string> values = TestUtilities.RunEnumerationToList(ff);

            TestUtilities.WriteToFile(TestUtilities.Config["FilesFileName"], values);

            Assert.IsTrue(File.Exists(TestUtilities.Config["FilesFileName"]));
        }

        [TestMethod]
        [TestCategory(TestCategories.FunctionalTest)]
        public void FindFilesFilterIsPDFTest()
        {
            IFindFileSystem ff = new FindFiles()
                                    .WithPath(TestUtilities.Config["ScanFolder"])                                    
                                    .Recursive()
                                    .AddFilter(TestUtilities.FileIsPDF);

            IList<string> values = TestUtilities.RunEnumerationToList(ff);

            TestUtilities.WriteToFile(TestUtilities.Config["FilesFileName"], values);

            Assert.IsTrue(File.Exists(TestUtilities.Config["FilesFileName"]));
        }

        [TestMethod]
        [TestCategory(TestCategories.FunctionalTest)]
        public void FindFilesFilter1MBTest()
        {
            IFindFileSystem ff = new FindFiles()
                                    .WithPath(TestUtilities.Config["ScanFolder"])                                    
                                    .Recursive()
                                    .AddFilter(TestUtilities.FileGreaterThan1MB);

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
            IFindFileSystem ff = new FindFiles()
                                    .WithPath(TestUtilities.Config["ScanFolder"])                                    
                                    .Recursive()
                                    .AddFilter(TestUtilities.FileIsPDF)
                                    .AddFilter(TestUtilities.FileGreaterThan1MB);

            IList<string> values = TestUtilities.RunEnumerationToList(ff);

            TestUtilities.WriteToFile(TestUtilities.Config["FilesFileName"], values);

            Assert.IsTrue(File.Exists(TestUtilities.Config["FilesFileName"]));
        }
    }
}
