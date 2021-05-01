using System.Collections.Generic;
using System.IO;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using FileSystem;

namespace FileSystemUnitTests
{
    [TestClass]
    public class RecursiveFileProcessorTests
    {
        readonly string scanFolder = @"C:\\Development";
        readonly string filesFileName = @"C:\Development\Temp\FileSystemProcessor\FileProcess.txt";

        [TestMethod]
        public void RecursiveListFilesFoldersTest()
        {
            RecursiveFileProcessor process = new RecursiveFileProcessor(scanFolder);

            IList<string> listText = new List<string>();

            process.FileProcess = (fsi) => listText.Add(fsi.Name);
            process.EnterFolderProcess = (fsi) => listText.Add($"Enter {fsi.Name}");
            process.ExitFolderProcess = (fsi) => listText.Add($"Exit {fsi.Name}");

            process.DoProcess();

            TestUtilities.WriteToFile(filesFileName, listText);

            Assert.IsTrue(File.Exists(filesFileName));
        }

        [TestMethod]
        public void AnthemPDFsTest()
        {
            RecursiveFileProcessor process = new RecursiveFileProcessor(scanFolder);
            process.FileOptions.Filter = (fsi) => fsi.Name.EndsWith(".pdf", System.StringComparison.OrdinalIgnoreCase);
            process.FolderOptions.Filter = (fsi) => fsi.Name.Contains("anthem", System.StringComparison.OrdinalIgnoreCase);

            IList<string> listText = new List<string>();

            process.FileProcess = (fsi) => listText.Add(fsi.Name);
            process.EnterFolderProcess = (fsi) => listText.Add($"Enter {fsi.Name}");
            process.ExitFolderProcess = (fsi) => listText.Add($"Exit {fsi.Name}");

            process.DoProcess();

            TestUtilities.WriteToFile(filesFileName, listText);

            Assert.IsTrue(File.Exists(filesFileName));
        }

        [TestMethod]
        public void DoDeleteMeTest()
        {
            RecursiveFileProcessor process = new RecursiveFileProcessor(scanFolder);
            process.FileOptions.Filter = (fsi) => fsi.Name.EndsWith("DeleteMe.txt", System.StringComparison.OrdinalIgnoreCase);
            process.FolderOptions.Filter = (fsi) => fsi.Name.Contains("DeleteMe", System.StringComparison.OrdinalIgnoreCase);

            IList<string> listText = new List<string>();

            process.FileProcess = (fsi) => { listText.Add($"Delete {fsi.Name}"); fsi.Delete(); };
            process.EnterFolderProcess = (fsi) => listText.Add($"Enter {fsi.Name}");
            process.ExitFolderProcess = (fsi) => { listText.Add($"Exit {fsi.Name}"); fsi.Delete(); };

            process.DoProcess();

            TestUtilities.WriteToFile(filesFileName, listText);

            Assert.IsTrue(File.Exists(filesFileName));
        }
    }
}
