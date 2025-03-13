namespace ErgodicMage.FileSystemProcessor;

public class FindFilesRecursive : IFindFileSystem
{
    #region Constructors
    public FindFilesRecursive()
    {
        Options = new FindFilesOptions();
        FolderOptions = new FindFilesOptions();
    }
    
    public FindFilesRecursive(string path)
    {
        Options = new FindFilesOptions();
        Options.Path = path;
        Options.Recursive = false;
        FolderOptions = new FindFilesOptions();
        FolderOptions.Path = path;
        FolderOptions.Recursive = false;
    }

    public FindFilesRecursive(FindFilesOptions options)
    {
        Options = options;
        Options.Recursive = false;
        FolderOptions = new FindFilesOptions();
        FolderOptions.Path = Options.Path;
        FolderOptions.Recursive = false;
    }

    public FindFilesRecursive(string path, string pattern, string regexpattern = "", Predicate<FileSystemInfo> filter = null)
    {
        Options = new FindFilesOptions() { Path = path, Pattern = pattern, Recursive=false, RegExPattern = regexpattern, Filter = filter };
        FolderOptions = new FindFilesOptions();
        FolderOptions.Path = Options.Path;
        FolderOptions.Recursive = false;
    }
    #endregion

    #region Fluent API
    public IFindFileSystem WithOptions(FindFilesOptions options)
    {
        Options = options;
        FolderOptions.Path = options.Path;
        FolderOptions.Recursive = false;
        return this;
    }

    public IFindFileSystem WithPath(string path)
    {
        Options.Path = path;
        FolderOptions.Path = path;
        FolderOptions.Recursive = false;
        return this;
    }

    public IFindFileSystem WithPattern(string pattern)
    {
        Options.Pattern = pattern;
        FolderOptions.Recursive = false;
        return this;
    }

    public IFindFileSystem WithRegexPattern(string regex)
    {
        Options.RegExPattern = regex;
        FolderOptions.Recursive = false;
        return this;            
    }

    public IFindFileSystem Recursive()
    {
        Options.Recursive = true;
        FolderOptions.Recursive = false;
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
    public FindFilesOptions FolderOptions { get; protected set; }
    #endregion

    #region Functions
    public IEnumerable<FileSystemInfo> Enumerate() => new FindFilesRecursiveEnumerable(Options, FolderOptions);
    #endregion
}
