using Spectre.Console;
using Spectre.Console.Cli;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using VideoSpiderCli.Models;
using VideoSpiderCli.Services;

namespace VideoSpiderCli.Commands
{
    public class BatchDownloadCommand:Command<BatchDownloadCommand.Settings>
    {
        public override int Execute([NotNull] CommandContext context, [NotNull] Settings settings)
        {

            var detail = VideoSpiderService.LookDetailAsync(settings.Url)
                                        .GetAwaiter().GetResult();

            PlayerAddressCollection? target = null;
            if (settings.SourceType == null)
            {
                target = detail.PlayerAddressCollections.First();
            }
            else
            {
                target = detail.PlayerAddressCollections.Where(e => e.SourceType.Contains(settings.SourceType)).Single();
            }
            foreach (var adress in target.PlayAdresses)
            {
                AnsiConsole.WriteLine($"开始下载--{adress.Title}");
                var result=DownloadService.DownloadAsync(adress.Url, adress.Title).GetAwaiter().GetResult();
                AnsiConsole.WriteLine($"下载完成--{adress.Title}");

                AnsiConsole.WriteLine();

            }
            return 1;
        }

        public class Settings:CommandSettings
        {
            [CommandArgument(0, "[Url]")]
            public string Url { get; set; }

            [CommandOption("--all")]
            [DefaultValue(true)]
            public bool IsDownloadAll { get; set; }

            [CommandOption("--sourceType")]
            public string? SourceType { get; set; }

        }
    }
}
