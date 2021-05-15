using System.Collections.Generic;
using System.IO;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using FileSystem;

namespace FileSystemUnitTests
{
    [TestClass]
    public class RecursiveFileProcessorTests
    {
        public RecursiveFileProcessorTests()
        {
            TestUtilities.LoadAppSettings();
        }

        [TestMethod]
        [TestCategory(TestCategories.FunctionalTest)]
        public void RecursiveListFilesFoldersTest()
        {
            RecursiveFileProcessor process = new RecursiveFileProcessor(TestUtilities.Config["ScanFolder"]);

            IList<string> listText = new List<string>();

            process.FileAction = (fi) => {listText.Add($"{fi.Name} {fi.Length}"); };
            process.EnterFolderAction = (di) => listText.Add($"Enter {di.FullName}");
            process.ExitFolderAction = (di) => listText.Add($"Exit {di.FullName}");

            process.DoProcess();

            TestUtilities.WriteToFile(TestUtilities.Config["FilesFileName"], listText);

            Assert.IsTrue(File.Exists(TestUtilities.Config["FilesFileName"]));
        }

        [TestMethod]
        [TestCategory(TestCategories.FunctionalTest)]
        public void DoDeleteMeTest()
        {
            RecursiveFileProcessor process = new RecursiveFileProcessor(TestUtilities.Config["ScanFolder"]);
            process.FileOptions.Filter = (fsi) => fsi.Name.EndsWith("DeleteMe.txt", System.StringComparison.OrdinalIgnoreCase);
            process.FolderOptions.Filter = (fsi) => fsi.Name.Contains("DeleteMe", System.StringComparison.OrdinalIgnoreCase);

            IList<string> listText = new List<string>();

            process.FileAction = (fi) => { listText.Add($"Delete {fi.Name}"); fi.Delete(); };
            process.EnterFolderAction = (di) => listText.Add($"Enter {di.Name}");
            process.ExitFolderAction = (di) => { listText.Add($"Exit {di.Name}"); di.Delete(); };

            process.DoProcess();

            TestUtilities.WriteToFile(TestUtilities.Config["FilesFileName"], listText);

            Assert.IsTrue(File.Exists(TestUtilities.Config["FilesFileName"]));
        }
    }
}
