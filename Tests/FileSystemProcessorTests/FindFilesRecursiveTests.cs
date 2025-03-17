namespace FileSystemProcessorTests;

public class FindFilesRecursiveTests
{
    public FindFilesRecursiveTests()
    {
        TestUtilities.LoadAppSettings();
    }

    [Fact]
    [Trait("Category", TestCategories.FunctionalTest)]
    public void FindFilesRecursiveTest()
    {
        IFindFileSystem ffr = new FindFilesRecursive()
                                .WithPath(TestUtilities.Config?["ScanFolder"]!);

        IList<string> values = TestUtilities.RunEnumerationToList(ffr);

        TestUtilities.WriteToFile(TestUtilities.Config?["FilesFileName"], values);

        Assert.True(File.Exists(TestUtilities.Config?["FilesFileName"]));
    }
}
