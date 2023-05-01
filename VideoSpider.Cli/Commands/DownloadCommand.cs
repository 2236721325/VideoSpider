using Spectre.Console.Cli;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using VideoSpiderCli.Services;

namespace VideoSpiderCli.Commands
{
    public class DownloadCommand : Command<DownloadCommand.Settings>
    {
        public override int Execute([NotNull] CommandContext context, [NotNull] Settings settings)
        {
            var result = DownloadService.DownloadAsync(settings.Url,settings.SaveName).GetAwaiter().GetResult();

            return result;
        }
        public class Settings : CommandSettings
        {
            [CommandArgument(0, "[Url]")]
            public string Url { get; set; }

            [CommandOption("--saveName")]
            public string? SaveName { get; set; }

        }
    }
}
