namespace FileSystemProcessorTests;

public class DirectoryEnumeratorTests
{
    public DirectoryEnumeratorTests()
    {
        TestUtilities.LoadAppSettings();
    }

    [Fact]
    [Trait("Category", TestCategories.FunctionalTest)]
    public void EnumerateFiles()
    {
        var files = Directory.EnumerateFiles(TestUtilities.Config?["ScanFolder"]!);
        List<string> values = new();

        foreach (var f in files)
            values.Add(f);

        TestUtilities.WriteToFile(TestUtilities.Config?["FilesFileName"], values);

        Assert.True(File.Exists(TestUtilities.Config?["FilesFileName"]));
    }

    [Fact]
    [Trait("Category", TestCategories.FunctionalTest)]
    public void EnumerateFilesRecursive()
    {
        EnumerationOptions options = new()
        {
            IgnoreInaccessible = true,
            RecurseSubdirectories = true,
            AttributesToSkip = FileAttributes.System
        };

        var files = Directory.EnumerateFiles(TestUtilities.Config?["ScanFolder"]!, "*", options);

        List<string> values = new();

        foreach (var f in files)
            values.Add(f);

        TestUtilities.WriteToFile(TestUtilities.Config?["FilesFileName"], values);

        Assert.True(File.Exists(TestUtilities.Config?["FilesFileName"]));
    }

    [Fact]
    [Trait("Category", TestCategories.FunctionalTest)]
    public void EnumerateDirectories()
    {
        var files = Directory.EnumerateDirectories(TestUtilities.Config?["ScanFolder"]!);

        List<string> values = new();

        foreach (var f in files)
            values.Add(f);

        TestUtilities.WriteToFile(TestUtilities.Config?["DirectoriesFileName"], values);

        Assert.True(File.Exists(TestUtilities.Config?["DirectoriesFileName"]));
    }

    [Fact]
    [Trait("Category", TestCategories.FunctionalTest)]
    public void EnumerateDirectoriesRecursive()
    {
        EnumerationOptions options = new()
        {
            IgnoreInaccessible = true,
            RecurseSubdirectories = true,
            AttributesToSkip = FileAttributes.System
        };

        var files = Directory.EnumerateDirectories(TestUtilities.Config?["ScanFolder"]!, "*", options);

        List<string> values = new();

        foreach (var f in files)
            values.Add(f);

        TestUtilities.WriteToFile(TestUtilities.Config?["DirectoriesFileName"], values);

        Assert.True(File.Exists(TestUtilities.Config?["DirectoriesFileName"]));
    }

    [Fact]
    [Trait("Category", TestCategories.FunctionalTest)]
    public void EnumerateFileSystemEntries()
    {
        var files = Directory.EnumerateFileSystemEntries(TestUtilities.Config?["ScanFolder"]!);

        List<string> values = new();

        foreach (var f in files)
            values.Add(f);

        TestUtilities.WriteToFile(TestUtilities.Config?["FileSystemFileName"], values);

        Assert.True(File.Exists(TestUtilities.Config?["FileSystemFileName"]));
    }

    [Fact]
    [Trait("Category", TestCategories.FunctionalTest)]
    public void EnumerateFileSystemEntriesRecursive()
    {
         EnumerationOptions options = new()
        {
            IgnoreInaccessible = true,
            RecurseSubdirectories = true,
            AttributesToSkip = FileAttributes.System
        };

        var files = Directory.EnumerateFileSystemEntries(TestUtilities.Config?["ScanFolder"]!, "*", options);

        List<string> values = new();

        foreach (var f in files)
            values.Add(f);

        TestUtilities.WriteToFile(TestUtilities.Config?["FileSystemFileName"], values);

        Assert.True(File.Exists(TestUtilities.Config?["FileSystemFileName"]));
    }

    [Fact]
    [Trait("Category", TestCategories.FunctionalTest)]
    public void DoFilesDirecories()
    {
        EnumerationOptions enumerationOptions = new()
        {
            IgnoreInaccessible = true
        };

        List<string> values = new();

        DoDirectory(values, TestUtilities.Config?["ScanFolder"], enumerationOptions);

        TestUtilities.WriteToFile(TestUtilities.Config?["FilesDirectoriesFileName"], values);
        Assert.True(File.Exists(TestUtilities.Config?["FilesDirectoriesFileName"]));
    }

    private static void DoDirectory(IList<string> values, string? directory, EnumerationOptions enumerationOptions, string searchPattern = "*")
    {
        if (string.IsNullOrEmpty(directory))
        return;

        var files = Directory.EnumerateFiles(directory, searchPattern, enumerationOptions);
        foreach (var f in files)
            values.Add(f);

        var directories = Directory.EnumerateDirectories(directory, searchPattern, enumerationOptions);
        foreach (var d in directories)
            DoDirectory(values, d, enumerationOptions);
    }
}
