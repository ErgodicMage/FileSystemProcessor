namespace ErgodicMage.FileSystemProcessor;

internal enum EnumerateType
{
    None,
    Files,
    Directories
}

internal static class GetEnumerableFileSystemInfo
{
    internal static IEnumerable<FileSystemInfo> GetEnumerable(FindFilesOptions options, EnumerateType enumerateType)
    {
        EnumerationOptions enumerationoptions = options.Options ?? FindFilesOptions.DefaultEnumerationOptions;
        if (options.Recursive)
        {
            enumerationoptions = new EnumerationOptions()
            {
                AttributesToSkip = FindFilesOptions.DefaultEnumerationOptions.AttributesToSkip,
                IgnoreInaccessible = FindFilesOptions.DefaultEnumerationOptions.IgnoreInaccessible,
                RecurseSubdirectories = true
            };
        }

        DirectoryInfo directoryinfo = new(options.Path ?? string.Empty);
        string pattern = options.Pattern ?? string.Empty;
        Regex? regex = string.IsNullOrEmpty(options.RegExPattern) ? null : new(options.RegExPattern, RegexOptions.Compiled);

        IEnumerable<FileSystemInfo> enumerable = enumerateType switch
        {
            EnumerateType.Files => directoryinfo.EnumerateFiles(pattern, enumerationoptions),
            EnumerateType.Directories => directoryinfo.EnumerateDirectories(pattern, enumerationoptions),
            _ => Enumerable.Empty<FileSystemInfo>()
        };

        return options switch
        {
            var o when regex is null && o.Filter is null => enumerable,
            var o when regex is not null && o.Filter is null => enumerable.Where(file => regex.IsMatch(file.FullName)),
            var o when regex is null && o.Filter is not null => enumerable.Where(file => o.Filter(file)),
            var o when regex is not null && o.Filter is not null => enumerable.Where(file => regex.IsMatch(file.FullName) && o.Filter(file)),
            _ => Enumerable.Empty<FileSystemInfo>()
        };
    }
}
