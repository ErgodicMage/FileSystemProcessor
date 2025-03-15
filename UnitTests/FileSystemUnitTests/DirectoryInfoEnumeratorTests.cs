namespace FileSystemUnitTests;

[TestClass]
public class DirectoryInfoEnumeratorTests
{
    public DirectoryInfoEnumeratorTests()
    {
        TestUtilities.LoadAppSettings();
    }

    [TestMethod]
    [TestCategory(TestCategories.FunctionalTest)]
    public void EnumerateFiles()
    {
        if (File.Exists(TestUtilities.Config["FilesFileName"]))
            File.Delete(TestUtilities.Config["FilesFileName"]);

        DirectoryInfo info = new(TestUtilities.Config["ScanFolder"]);
        var files = info.EnumerateFiles();
        
        IList<string> values = new List<string>();
        foreach (var f in files)
            values.Add(f.FullName);

        TestUtilities.WriteToFile(TestUtilities.Config["FilesFileName"], values);

        Assert.IsTrue(File.Exists(TestUtilities.Config["FilesFileName"]));
    }

    [TestMethod]
    [TestCategory(TestCategories.FunctionalTest)]
    public void EnumerateFilesRecursive()
    {
        if (File.Exists(TestUtilities.Config["FilesFileName"]))
            File.Delete(TestUtilities.Config["FilesFileName"]);

        DirectoryInfo info = new(TestUtilities.Config["ScanFolder"]);

        EnumerationOptions options = new()
        {
            IgnoreInaccessible = true,
            RecurseSubdirectories = true,
            AttributesToSkip = FileAttributes.System
        };

        var files = info.EnumerateFiles("*", options);

        IList<string> values = new List<string>();
        foreach (var f in files)
            values.Add(f.FullName);

        TestUtilities.WriteToFile(TestUtilities.Config["FilesFileName"], values);

        Assert.IsTrue(File.Exists(TestUtilities.Config["FilesFileName"]));
    }


    [TestMethod]
    [TestCategory(TestCategories.FunctionalTest)]
    public void EnumerateDirectories()
    {
        if (File.Exists(TestUtilities.Config["DirectoriesFileName"]))
            File.Delete(TestUtilities.Config["DirectoriesFileName"]);

        DirectoryInfo info = new(TestUtilities.Config["ScanFolder"]);
        var files = info.EnumerateDirectories();

        List<string> values = new();
        foreach (var f in files)
            values.Add(f.FullName);

        TestUtilities.WriteToFile(TestUtilities.Config["DirectoriesFileName"], values);

        Assert.IsTrue(File.Exists(TestUtilities.Config["DirectoriesFileName"]));
    }

    [TestMethod]
    [TestCategory(TestCategories.FunctionalTest)]
    public void EnumerateDirectoriesRecursive()
    {
        if (File.Exists(TestUtilities.Config["DirectoriesFileName"]))
            File.Delete(TestUtilities.Config["DirectoriesFileName"]);

        DirectoryInfo info = new(TestUtilities.Config["ScanFolder"]);

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

        TestUtilities.WriteToFile(TestUtilities.Config["DirectoriesFileName"], values);

        Assert.IsTrue(File.Exists(TestUtilities.Config["DirectoriesFileName"]));
    }

    [TestMethod]
    [TestCategory(TestCategories.FunctionalTest)]
    public void EnumerateFileSystemEntries()
    {
        if (File.Exists(TestUtilities.Config["FileSystemFileName"]))
            File.Delete(TestUtilities.Config["FileSystemFileName"]);

        DirectoryInfo info = new(TestUtilities.Config["ScanFolder"]);
        var files = info.EnumerateFileSystemInfos();

        List<string> values = new();
        foreach (var f in files)
            values.Add(f.FullName);

        TestUtilities.WriteToFile(TestUtilities.Config["FileSystemFileName"], values);

        Assert.IsTrue(File.Exists(TestUtilities.Config["FileSystemFileName"]));
    }

    [TestMethod]
    [TestCategory(TestCategories.FunctionalTest)]
    public void EnumerateFileSystemEntriesRecursive()
    {
        if (File.Exists(TestUtilities.Config["FileSystemFileName"]))
            File.Delete(TestUtilities.Config["FileSystemFileName"]);

        DirectoryInfo info = new(TestUtilities.Config["ScanFolder"]);

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

        TestUtilities.WriteToFile(TestUtilities.Config["FileSystemFileName"], values);

        Assert.IsTrue(File.Exists(TestUtilities.Config["FileSystemFileName"]));
    }




    [TestMethod]
    [TestCategory(TestCategories.FunctionalTest)]
    public void DoFilesDirecories()
    {
        if (File.Exists(TestUtilities.Config["FilesDirectoriesFileName"]))
            File.Delete(TestUtilities.Config["FilesDirectoriesFileName"]);

        EnumerationOptions enumerationOptions = new()
        {
            IgnoreInaccessible = true
        };

        DirectoryInfo info = new(TestUtilities.Config["ScanFolder"]);

        List<string> values = new();
            
        DoDirectory(values, info, enumerationOptions);

        TestUtilities.WriteToFile(TestUtilities.Config["FilesDirectoriesFileName"], values);

        Assert.IsTrue(File.Exists(TestUtilities.Config["FilesDirectoriesFileName"]));
    }

    public void DoDirectory(IList<string> values, DirectoryInfo info, EnumerationOptions enumerationOptions, string searchPattern="*")
    {
        var files = info.EnumerateFiles(searchPattern, enumerationOptions);
        foreach (var f in files)
            values.Add(f.FullName);

        var directories = info.EnumerateDirectories(searchPattern, enumerationOptions);
        foreach (var d in directories)
            DoDirectory(values, d, enumerationOptions);
    }
}
