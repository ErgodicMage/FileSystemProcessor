using System.Collections.Generic;
using System.IO;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using FileSystem;

namespace FileSystemUnitTests
{
    [TestClass]
    public class FindFilesRecursiveTests
    {
        public FindFilesRecursiveTests()
        {
            TestUtilities.LoadAppSettings();
        }

        [TestMethod]
        [TestCategory(TestCategories.FunctionalTest)]
        public void FindFilesRecursiveTest()
        {
            IFindFileSystem ffr = new FindFilesRecursive()
                                    .WithPath(TestUtilities.Config["ScanFolder"]);

            IList<string> values = TestUtilities.RunEnumerationToList(ffr);

            TestUtilities.WriteToFile(TestUtilities.Config["FilesFileName"], values);

            Assert.IsTrue(File.Exists(TestUtilities.Config["FilesFileName"]));
        }
    }
}
