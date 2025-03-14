namespace ErgodicMage.FileSystemProcessor;

public class FindFilesOptions
{
    public FindFilesOptions()
    {
        Pattern = "*";
    }

    public string? Path { get; set; } = string.Empty;
    public string? Pattern { get; set; } = string.Empty;
    public string? RegExPattern { get; set; } = string.Empty;
    public bool Recursive { get; set; }
    public Predicate<FileSystemInfo>? Filter {get; set; }
    public EnumerationOptions? Options { get; set; }

    internal readonly static EnumerationOptions DefaultEnumerationOptions = new() 
        { IgnoreInaccessible = true, AttributesToSkip = FileAttributes.Hidden | FileAttributes.System };
}
