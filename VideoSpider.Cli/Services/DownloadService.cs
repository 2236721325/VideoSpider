using CliWrap;
using Spectre.Console;

namespace VideoSpiderCli.Services
{
    public static class DownloadService
    {
        public static async Task<int> DownloadAsync(string video_play_url,string? saveName)
        {
            var m3u8 = await VideoSpiderService.GetM3u8UrlAsync(video_play_url);

            var dir = Path.Combine(Directory.GetCurrentDirectory(), "tool");
            var arg = "\"" + m3u8 + "\"" + $" --workDir {dir}";
            if (saveName!=null)
            {
                arg += $" --saveName {saveName}";
            }
            var cmd = await Cli.Wrap("tool/N_m3u8DL-CLI_v3.0.2.exe")
                        .WithArguments(arg)
                        .WithStandardOutputPipe(PipeTarget.ToDelegate(AnsiConsole.WriteLine))
                        .WithStandardErrorPipe(PipeTarget.ToDelegate(AnsiConsole.WriteLine))
                        .ExecuteAsync();

            return 0;
        }
    }
}