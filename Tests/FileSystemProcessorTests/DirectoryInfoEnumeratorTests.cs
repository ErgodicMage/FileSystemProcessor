namespace FileSystemProcessorTests;

public class DirectoryInfoEnumeratorTests
{
    public DirectoryInfoEnumeratorTests()
    {
        TestUtilities.LoadAppSettings();
    }

    [Fact]
    [Trait("Category", TestCategories.FunctionalTest)]
    public void EnumerateFiles()
    {
        DirectoryInfo info = new(TestUtilities.Config?["ScanFolder"]!);
        var files = info.EnumerateFiles();
        
        List<string> values = new();
        foreach (var f in files)
            values.Add(f.FullName);

        TestUtilities.WriteToFile(TestUtilities.Config?["FilesFileName"], values);

        Assert.True(File.Exists(TestUtilities.Config?["FilesFileName"]));
    }

    [Fact]
    [Trait("Category", TestCategories.FunctionalTest)]
    public void EnumerateFilesRecursive()
    {
        DirectoryInfo info = new(TestUtilities.Config?["ScanFolder"]!);

        EnumerationOptions options = new()
        {
            IgnoreInaccessible = true,
            RecurseSubdirectories = true,
            AttributesToSkip = FileAttributes.System
        };

        var files = info.EnumerateFiles("*", options);

        List<string> values = new();
        foreach (var f in files)
            values.Add(f.FullName);

        TestUtilities.WriteToFile(TestUtilities.Config?["FilesFileName"], values);

        Assert.True(File.Exists(TestUtilities.Config?["FilesFileName"]));
    }

    [Fact]
    [Trait("Category", TestCategories.FunctionalTest)]
    public void EnumerateDirectories()
    {
        DirectoryInfo info = new(TestUtilities.Config?["ScanFolder"]!);
        var files = info.EnumerateDirectories();

        List<string> values = new();
        foreach (var f in files)
            values.Add(f.FullName);

        TestUtilities.WriteToFile(TestUtilities.Config?["DirectoriesFileName"], values);

        Assert.True(File.Exists(TestUtilities.Config?["DirectoriesFileName"]));
    }

    [Fact]
    [Trait("Category", TestCategories.FunctionalTest)]
    public void EnumerateDirectoriesRecursive()
    {
        DirectoryInfo info = new(TestUtilities.Config?["ScanFolder"]!);

        EnumerationOptions options = new()
        {
            IgnoreInaccessible = true,
            RecurseSubdirectories = true,
            AttributesToSkip = FileAttributes.System
        };

        var files = info.EnumerateDirectories("*", options);

        List<string> values = new();
        foreach (var f in files)
            values.Add(f.FullName);

        TestUtilities.WriteToFile(TestUtilities.Config?["DirectoriesFileName"], values);

        Assert.True(File.Exists(TestUtilities.Config?["DirectoriesFileName"]));
    }

    [Fact]
    [Trait("Category", TestCategories.FunctionalTest)]
    public void EnumerateFileSystemEntries()
    {
        DirectoryInfo info = new(TestUtilities.Config?["ScanFolder"]!);
        var files = info.EnumerateFileSystemInfos();

        List<string> values = new();
        foreach (var f in files)
            values.Add(f.FullName);

        TestUtilities.WriteToFile(TestUtilities.Config?["FileSystemFileName"], values);

        Assert.True(File.Exists(TestUtilities.Config?["FileSystemFileName"]));
    }

    [Fact]
    [Trait("Category", TestCategories.FunctionalTest)]
    public void EnumerateFileSystemEntriesRecursive()
    {
        DirectoryInfo info = new(TestUtilities.Config?["ScanFolder"]!);

        EnumerationOptions options = new()
        {
            IgnoreInaccessible = true,
            RecurseSubdirectories = true,
            AttributesToSkip = FileAttributes.System
        };

        var files = info.EnumerateFileSystemInfos("*", options);

        List<string> values = new();
        foreach (var f in files)
            values.Add(f.FullName);

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

        DirectoryInfo info = new(TestUtilities.Config?["ScanFolder"]!);

        List<string> values = new();
            
        DoDirectory(values, info, enumerationOptions);

        TestUtilities.WriteToFile(TestUtilities.Config?["FilesDirectoriesFileName"], values);

        Assert.True(File.Exists(TestUtilities.Config?["FilesDirectoriesFileName"]));
    }

    private static void DoDirectory(IList<string> values, DirectoryInfo info, EnumerationOptions enumerationOptions, string searchPattern="*")
    {
        var files = info.EnumerateFileSystemInfos(searchPattern, enumerationOptions);
        foreach (var f in files)
            values.Add(f.FullName);

        var directories = info.EnumerateDirectories(searchPattern, enumerationOptions);
        foreach (var d in directories)
            DoDirectory(values, d, enumerationOptions);
    }
}
