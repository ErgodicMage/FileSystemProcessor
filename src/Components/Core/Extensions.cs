namespace ErgodicMage.FileSystemProcessor;

public static class Extensions
{
    public static bool IsFile(this FileSystemInfo info) => (info.Attributes & FileAttributes.Normal) != 0;

    public static bool IsDirectory(this FileSystemInfo info) => (info.Attributes & FileAttributes.Directory) != 0;
    
    public static bool SafeDelete(this FileInfo fi)
    {
        try
        {
            fi.Delete();
            return true;
        }
        catch
        {
            return false;
        }
    }

    public static bool SafeDelete(this DirectoryInfo di)
    {
        try
        {
            di.Delete();
            return true;
        }
        catch
        {
            return false;
        }
    }
}
