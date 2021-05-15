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

            process.FileAction = (fsi) => {listText.Add($"{fsi.Name} {(fsi as FileInfo)?.Length}"); };
            process.EnterFolderAction = (fsi) => listText.Add($"Enter {fsi.FullName}");
            process.ExitFolderAction = (fsi) => listText.Add($"Exit {fsi.FullName}");

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

            process.FileAction = (fsi) => { listText.Add($"Delete {fsi.Name}"); fsi.Delete(); };
            process.EnterFolderAction = (fsi) => listText.Add($"Enter {fsi.Name}");
            process.ExitFolderAction = (fsi) => { listText.Add($"Exit {fsi.Name}"); fsi.Delete(); };

            process.DoProcess();

            TestUtilities.WriteToFile(TestUtilities.Config["FilesFileName"], listText);

            Assert.IsTrue(File.Exists(TestUtilities.Config["FilesFileName"]));
        }
    }
}
