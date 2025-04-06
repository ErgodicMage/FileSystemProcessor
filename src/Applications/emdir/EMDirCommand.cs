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

        AnsiConsole.WriteLine($"Recursive {settings.Recurse}");

        return 0;
    }
}
