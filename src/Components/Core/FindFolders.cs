namespace ErgodicMage.FileSystemProcessor;

public class FindFolders : IFindFileSystem
{
    #region Constructors
    public FindFolders()
    {
        Options = new FindFilesOptions();
    }

    public FindFolders(string path)
    {
        Options = new FindFilesOptions
        {
            Path = path
        };
    }

    public FindFolders(FindFilesOptions options)
    {
        Options = options;
    }

    public FindFolders(string path, string pattern, bool recursive, string regexpattern = "", Predicate<FileSystemInfo>? filter = null)
    {
        Options = new FindFilesOptions
        {
            Path = path,
            Pattern = pattern,
            RegExPattern = regexpattern,
            Filter = filter,
            Options = new EnumerationOptions() { RecurseSubdirectories = recursive }
        };

    }
    #endregion

    #region Fluent API
    public IFindFileSystem WithOptions(FindFilesOptions options)
    {
        Options = options;
        return this;
    }

    public IFindFileSystem WithPath(string path)
    {
        Options.Path = path;
        return this;
    }

    public IFindFileSystem WithPattern(string pattern)
    {
        Options.Pattern = pattern;
        return this;
    }

    public IFindFileSystem WithRegexPattern(string regex)
    {
        Options.RegExPattern = regex;
        return this;            
    }

    public IFindFileSystem Recursive()
    {
        if (Options.Options == null)
            Options.Options = new EnumerationOptions();
        Options.Options.RecurseSubdirectories = true;
        return this;            
    }

    public IFindFileSystem WithFilter(Predicate<FileSystemInfo> filter)
    {
        Options.Filter += filter;
        return this;            
    }
    #endregion


    #region Properties
    public FindFilesOptions Options { get; set; }
    #endregion

    #region Public Functions
    public IEnumerable<FileSystemInfo> Enumerate()
    {
        EnumerationOptions enumerationoptions = Options.Options ?? FindFilesOptions.DefaultEnumerationOptions;
        if (Options.Recursive)
        {
            enumerationoptions = new EnumerationOptions()
            {
                AttributesToSkip = FindFilesOptions.DefaultEnumerationOptions.AttributesToSkip,
                IgnoreInaccessible = FindFilesOptions.DefaultEnumerationOptions.IgnoreInaccessible,
                RecurseSubdirectories = true
            };
        }

        DirectoryInfo directoryinfo = new(Options.Path ?? string.Empty);
        string pattern = Options.Pattern ?? string.Empty;


        if (string.IsNullOrEmpty(Options.RegExPattern) && Options.Filter == null)
            return (IEnumerable<FileSystemInfo>)directoryinfo.EnumerateDirectories(pattern, enumerationoptions);
        else if (!string.IsNullOrEmpty(Options.RegExPattern) && Options.Filter == null)
        {
            Regex regex = new(Options.RegExPattern, RegexOptions.Compiled);
            return (IEnumerable<FileSystemInfo>)directoryinfo.EnumerateDirectories(pattern, enumerationoptions)
                .Where(file => regex.IsMatch(file.FullName));
        }
        else if (string.IsNullOrEmpty(Options.RegExPattern) && Options.Filter != null)
        {
            return (IEnumerable<FileSystemInfo>)directoryinfo.EnumerateDirectories(pattern, enumerationoptions)
                .Where(file => Options.Filter(file));
        }
        else if (!string.IsNullOrEmpty(Options.RegExPattern) && Options.Filter != null)
        {
            Regex regex = new(Options.RegExPattern);
            return (IEnumerable<FileSystemInfo>)directoryinfo.EnumerateDirectories(pattern, enumerationoptions)
                .Where(file => regex.IsMatch(file.FullName) && Options.Filter(file));
        }

        return Enumerable.Empty<FileSystemInfo>();
    }
    #endregion
}
