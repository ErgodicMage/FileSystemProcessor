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

    private FindFilesOptions GetFileOptions(EMDirSettings settings) =>
        new()
        {
            Path = settings.Path ?? Directory.GetCurrentDirectory(),
            Pattern = settings.Pattern,
            RegExPattern = settings.Regex,
            Recursive = settings.Recursive,
        };

    private FindFilesOptions GetFolderOptions(EMDirSettings settings) =>
        new()
        {
            Path = settings.Path ?? Directory.GetCurrentDirectory(),
        };

    private IProcessor GetProcessor(EMDirSettings settings)
    {
        return settings.Recursive switch
        {
            true => new RecursiveFileProcessor().WithFileOptions(GetFileOptions(settings)).WithFolderOptions(GetFolderOptions(settings))
                            .WithFileAction(FileAction)
                            .WithEnterFolderAction(EnterFolderAction).WithExitFolderAction(ExitFolderAction),
            _ => new FileProcessor().WithOptions(GetFileOptions(settings)).WithAction(FileSystemAction)
        };
    }

    private void FileAction(FileInfo? fi) => AnsiConsole.MarkupLine($"[blue]{fi?.Name}[/] [green]{fi?.Length}[/]");
    private void FileSystemAction(FileSystemInfo fsi) => AnsiConsole.MarkupLine($"[blue]{fsi.Name}[/] [green]{(fsi as FileInfo)?.Length}[/]");

    private void EnterFolderAction(DirectoryInfo di) => AnsiConsole.MarkupLine($"[yellow]Entering {di.Name}[/]");
    private void ExitFolderAction(DirectoryInfo di) => AnsiConsole.MarkupLine($"[yellow]Exiting {di.Name}[/]");
}
