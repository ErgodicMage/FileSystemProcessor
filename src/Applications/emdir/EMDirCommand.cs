using System.Diagnostics.CodeAnalysis;
using Spectre.Console;

namespace emdir;

public class EMDirCommand : Command<EMDirSettings>
{
    public override int Execute(CommandContext context, EMDirSettings settings)
    {
        if (settings is null) return 1;

        AnsiConsole.WriteLine($"Path {settings.Path ?? Directory.GetCurrentDirectory()}");

        AnsiConsole.WriteLine($"Pattern {settings.Pattern ?? "*"}");

        AnsiConsole.WriteLine($"Regex {settings.Regex ?? string.Empty}");

        AnsiConsole.WriteLine($"Recursive {settings.Recursive}");

        IProcessor processor = GetProcessor(settings);
        processor.Run();

        return 0;
    }

    private FindFilesOptions GetOptions(EMDirSettings setting) =>
        new()
        {
            Path = setting.Path,
            Pattern = setting.Pattern,
            RegExPattern = setting.Regex,
            Recursive = setting.Recursive,
        };

    private IProcessor GetProcessor(EMDirSettings settings)
    {
        FindFilesOptions options = GetOptions(settings);

        return settings.Recursive switch
        {
            true => new RecursiveFileProcessor().WithFileOptions(options).WithFolderOptions(options).WithFileAction(FileAction)
                            .WithEnterFolderAction(EnterFolderAction).WithExitFolderAction(ExitFolderAction),
            _ => new FileProcessor().WithOptions(options).WithAction(FileSystemAction)
        };
    }

    private void FileAction(FileInfo? fi) => AnsiConsole.WriteLine($"{fi?.Name} {fi?.Length}");
    private void FileSystemAction(FileSystemInfo fsi) => AnsiConsole.WriteLine($"{fsi.Name} {(fsi as FileInfo)?.Length}");

    private void EnterFolderAction(DirectoryInfo di) => AnsiConsole.WriteLine($"Entering {di.Name}");
    private void ExitFolderAction(DirectoryInfo di) => AnsiConsole.WriteLine($"Exiting {di.Name}");
}
