using System.ComponentModel;

namespace emdir;

public class EMDirSettings : CommandSettings
{
    [Description("The path to search. Defaults to current directory.")]
    [CommandArgument(0, "[Path]")]

    public string? Path { get; init; }

    [Description("The pattern to for. Defaults to all files.")]
    [DefaultValue("*")]
    [CommandOption("-p|--pattern")]
    public string? Pattern { get; init; }

    [Description("Search regex pattern.")]
    [CommandOption("--reg|--regex")]
    public string? Regex { get; init; }

    [Description("Include subfolders. Default false.")]
    [CommandOption("-r|--recursive")]
    public bool Recursive { get; init; }
}
