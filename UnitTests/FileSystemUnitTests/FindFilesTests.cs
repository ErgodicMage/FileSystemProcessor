using System.Collections.Generic;
using System.IO;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using FileSystem;

namespace FileSystemUnitTests
{
    [TestClass]
    public class FindFilesTests
    {
        readonly string scanFolder = @"C:\Development";
        readonly string filesFileName = @"C:\Development\Temp\FileSystemProcessor\Files.txt";

        [TestMethod]
        public void FindFilesTest()
        {
            FindFiles ff = new FindFiles(scanFolder);

            IList<string> values = TestUtilities.RunEnumerationToList(ff);

            TestUtilities.WriteToFile(filesFileName, values);

            Assert.IsTrue(File.Exists(filesFileName));
        }

        [TestMethod]
        public void FindFilesRecursiveTest()
        {
            FindFilesOptions options = new FindFilesOptions() { Path = scanFolder, Pattern = "*", Recursive = true };
            FindFiles ff = new FindFiles(options);

            IList<string> values = TestUtilities.RunEnumerationToList(ff);

            TestUtilities.WriteToFile(filesFileName, values);

            Assert.IsTrue(File.Exists(filesFileName));
        }

        [TestMethod]
        public void FindFilesRegexTest()
        {
            FindFilesOptions options = new FindFilesOptions() { Path = scanFolder, Pattern = "*", RegExPattern = @".+\.pdf$", Recursive = true };
            FindFiles ff = new FindFiles(options);

            IList<string> values = TestUtilities.RunEnumerationToList(ff);

            TestUtilities.WriteToFile(filesFileName, values);

            Assert.IsTrue(File.Exists(filesFileName));
        }

        [TestMethod]
        public void FindFilesFilterIsPDFTest()
        {
            FindFilesOptions options = new FindFilesOptions() { Path = scanFolder, Pattern = "*", Recursive = true, Filter = TestUtilities.FileIsPDF };
            FindFiles ff = new FindFiles(options);

            IList<string> values = TestUtilities.RunEnumerationToList(ff);

            TestUtilities.WriteToFile(filesFileName, values);

            Assert.IsTrue(File.Exists(filesFileName));
        }

        [TestMethod]
        public void FindFilesFilter1MBTest()
        {
            FindFilesOptions options = new FindFilesOptions() { Path = scanFolder, Pattern = "*", Recursive = true, Filter=TestUtilities.FileGreaterThan1MB };
            FindFiles ff = new FindFiles(options);

            IList<string> values = TestUtilities.RunEnumerationToList(ff);

            TestUtilities.WriteToFile(filesFileName, values);

            Assert.IsTrue(File.Exists(filesFileName));
        }

        [TestMethod]
        public void FindFilesFilterPDF1MBTest()
        {
            FindFilesOptions options = new FindFilesOptions() { Path = scanFolder, Pattern = "*", Recursive = true, Filter = TestUtilities.FileIsPDFGreaterThan1MB };
            FindFiles ff = new FindFiles(options);

            IList<string> values = TestUtilities.RunEnumerationToList(ff);

            TestUtilities.WriteToFile(filesFileName, values);

            Assert.IsTrue(File.Exists(filesFileName));
        }
    }
}
