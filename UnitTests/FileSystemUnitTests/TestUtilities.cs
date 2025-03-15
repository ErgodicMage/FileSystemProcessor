﻿using ErgodicMage.FileSystemProcessor;
using Microsoft.Extensions.Configuration;

namespace FileSystemUnitTests;

public class TestCategories
{
    public const string UnitTest = "UnitTest";
    public const string FunctionalTest = "FunctionalTest";
}

public static class TestUtilities
{
    #region Configuration
    public static IConfiguration Config { get; private set; }

    public static void LoadAppSettings()
    {
        Config = new ConfigurationBuilder()
            .AddJsonFile("appsettings.json")
            .Build();
    }
    #endregion

    #region Process Functionality
    public static IList<string> RunEnumerationToList(IFindFileSystem ffs) => 
        ffs.Enumerate().Select(f => f.FullName).ToList();
    #endregion

    #region Filters
    public static bool FileIsPDF(FileSystemInfo info) => info.Name.EndsWith(".pdf");
    public static bool FileGreaterThan1MB(FileSystemInfo info) => ((FileInfo)info).Length > 1048576;
    public static bool FileIsPDFGreaterThan1MB(FileSystemInfo info) => FileIsPDF(info) && FileGreaterThan1MB(info);
    public static bool DirectoryHasQRCodes(FileSystemInfo info) => info.Name.Equals("QRCodes");
    public static bool IsFileSystemProcessor(FileSystemInfo info) => info.IsDirectory()&&info.FullName.Contains(@"FileSystemProcessor");
    public static bool IsStellarMap(FileSystemInfo info) => info.IsDirectory()&&info.FullName.Contains(@"StellarMap");

    public static bool IsFileSystemProcessorOrStellarMap(FileSystemInfo info) => IsFileSystemProcessor(info) || IsStellarMap(info);
    #endregion

    #region Write functionality
    public static void WriteToFile(string path, IList<string> values)
    {
        if (File.Exists(path))
            File.Delete(path);

        using StreamWriter writer = new(path);
        WriteToStreamWriter(writer, values);
    }
    
    public static void WriteToStreamWriter(StreamWriter writer, IList<string> values)
    {
        foreach (var v in values)
            writer.WriteLine(v);
    }
    #endregion

    #region Resource Functionality
    const string testnamespace = "FileSystemUnitTests";

    public static string ReadResource(string folder, string resourcefile)
    {
        string filename = folder.Replace(" ", "_").Replace("\\", ".").Replace("/", ".") + "." + resourcefile;
        return ReadResource(filename);
    }

    public static string ReadResource(string resourcefile)
    {
        string filename = testnamespace + "." + resourcefile;

        string retString = string.Empty;
        using (StreamReader reader = LoadResourceFile(filename))
        {
            retString = reader.ReadToEnd();
        }

        return retString;
    }
    public static StreamReader LoadResourceFile(string resourcefile)
    {
        StreamReader reader = new(System.Reflection.Assembly.GetExecutingAssembly().GetManifestResourceStream(resourcefile));
        return reader;
    }
    #endregion
}
