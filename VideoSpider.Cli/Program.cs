using Spectre.Console;
using Spectre.Console.Cli;
using VideoSpiderCli.Commands;

namespace VideoSpiderCli
{
    internal class Program
    {
        static int Main(string[] args)
        {
            var app = new CommandApp();

            app.Configure(config =>
            {
                config.AddCommand<SearchCommand>("search");
                config.AddCommand<LookCommand>("look");
                config.AddCommand<DownloadCommand>("download");
                config.AddCommand<BatchDownloadCommand>("batchDownload");





                config.SetExceptionHandler(ex =>
                {
                    AnsiConsole.WriteException(ex, ExceptionFormats.ShortenEverything);
                    return -99;
                });
            });

            return app.Run(args);
        }
    }
}