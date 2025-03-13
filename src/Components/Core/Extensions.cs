namespace ErgodicMage.FileSystemProcessor;

public static class Extensions
{
    public static bool IsFile(this FileSystemInfo info) => (info.Attributes & FileAttributes.Normal) != 0;

    public static bool IsDirectory(this FileSystemInfo info) => (info.Attributes & FileAttributes.Directory) != 0;

}
