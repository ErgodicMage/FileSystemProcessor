using System.Collections.Generic;
using System.IO;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using FileSystem;

namespace FileSystemUnitTests
{
    [TestClass]
    public class FindFoldersTests
    {
        readonly string scanFolder = @"\\compservicesftp-test.ecomad.int\Projects"; //@"c:\Development";
        readonly string directoriesFileName = @"C:\Development\Temp\FileSystemProcessor\Directories.txt";

        [TestMethod]
        public void FindDirectoriesTest()
        {
            FindFolders fd = new FindFolders(scanFolder);

            IList<string> values = TestUtilities.RunEnumerationToList(fd);

            TestUtilities.WriteToFile(directoriesFileName, values);

            Assert.IsTrue(File.Exists(directoriesFileName));
        }

        [TestMethod]
        public void FindDirectoriesRecursiveTest()
        {
            FindFilesOptions options = new FindFilesOptions() { Path = scanFolder, Pattern = "*", Recursive = true };
            FindFolders fd = new FindFolders(options);

            IList<string> values = TestUtilities.RunEnumerationToList(fd);

            TestUtilities.WriteToFile(directoriesFileName, values);

            Assert.IsTrue(File.Exists(directoriesFileName));
        }

        [TestMethod]
        public void FindDirectoriesRegexTest()
        {
            FindFilesOptions options = new FindFilesOptions() { Path = scanFolder, Pattern = "*", Recursive = true, RegExPattern=@"\b(\w*CServeWS\w*)\b" };
            FindFolders fd = new FindFolders(options);

            IList<string> values = TestUtilities.RunEnumerationToList(fd);

            TestUtilities.WriteToFile(directoriesFileName, values);

            Assert.IsTrue(File.Exists(directoriesFileName));
        }


        [TestMethod]
        public void FindDirectoriesFilterIsCServeWSTest()
        {
            FindFilesOptions options = new FindFilesOptions() { Path = scanFolder, Pattern = "*", Recursive = true, Filter=TestUtilities.IsCServeWS };
            FindFolders fd = new FindFolders(options);

            IList<string> values = TestUtilities.RunEnumerationToList(fd);

            TestUtilities.WriteToFile(directoriesFileName, values);

            Assert.IsTrue(File.Exists(directoriesFileName));
        }

        [TestMethod]
        public void FindDirectoriesFilterIsCServeWSOrDDPEATest()
        {
            FindFilesOptions options = new FindFilesOptions() { Path = scanFolder, Pattern = "*", Recursive = true, Filter = TestUtilities.IsCServeWSOrDDPEA };
            FindFolders fd = new FindFolders(options);

            IList<string> values = TestUtilities.RunEnumerationToList(fd);

            TestUtilities.WriteToFile(directoriesFileName, values);

            Assert.IsTrue(File.Exists(directoriesFileName));
        }

        [TestMethod]
        public void FindDirectoriesQRCodesTest()
        {
            FindFilesOptions options = new FindFilesOptions() { Path = scanFolder, Pattern = "*", Recursive = true, Filter = TestUtilities.DirectoryHasQRCodes };
            FindFolders fd = new FindFolders(options);

            IList<string> values = TestUtilities.RunEnumerationToList(fd); ;

            TestUtilities.WriteToFile(directoriesFileName, values);

            Assert.IsTrue(File.Exists(directoriesFileName));
        }
    }
}
