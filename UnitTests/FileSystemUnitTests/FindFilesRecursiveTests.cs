using System.Collections.Generic;
using System.IO;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using FileSystem;

namespace FileSystemUnitTests
{
    [TestClass]
    public class FindFilesRecursiveTests
    {
        //readonly string scanFolder = @"C:\Development\Temp\FileSystemProcessor";
        //readonly string scanFolder = @"C:\Development\Temp\FileSystemProcessor\src\Components\FileSystem"; 
        readonly string scanFolder = @"c:\Development";
        //readonly string scanFolder = @"C:\\Development\Temp";
        readonly string filesFileName = @"C:\Development\Temp\FileSystemProcessor\Files.txt";

        [TestMethod]
        public void FindFilesRecursiveTest()
        {
            FindFilesRecursive ffr = new FindFilesRecursive(scanFolder);

            IList<string> values = TestUtilities.RunEnumerationToList(ffr);

            TestUtilities.WriteToFile(filesFileName, values);

            Assert.IsTrue(File.Exists(filesFileName));
        }
    }
}
