using Spectre.Console;
using Spectre.Console.Cli;
using Spectre.Console.Json;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.Text.Encodings.Web;
using System.Text.Json;
using System.Text.Unicode;
using VideoSpiderCli.Services;

namespace VideoSpiderCli.Commands
{
    public class LookCommand : Command<LookCommand.Settings>
    {
        public override int Execute([NotNull] CommandContext context, [NotNull] Settings settings)
        {
            if (!settings.IsGetM3u8)
            {
                var result = VideoSpiderService.LookDetailAsync(settings.Url).GetAwaiter().GetResult();
                var json = JsonSerializer.Serialize(result, new JsonSerializerOptions()
                {
                    Encoder = JavaScriptEncoder.Create(UnicodeRanges.All),
                    WriteIndented = true
                });
                var json_text = new JsonText(json);


                AnsiConsole.Write(
                    new Panel(json_text)
                        .Header("Look Result!")
                        .Collapse()
                        .RoundedBorder()
                        .BorderColor(Color.Yellow));
                return 0;

            }

            var m3u8 = VideoSpiderService.GetM3u8UrlAsync(settings.Url).GetAwaiter().GetResult();

            Console.WriteLine("m3u8地址为：" + m3u8);
            return 0;



        }

        public class Settings : CommandSettings
        {
            [CommandArgument(0, "[Url]")]
            public string Url { get; set; }

            [CommandOption("--m3u8")]
            [DefaultValue(false)]
            public bool IsGetM3u8 { get; set; }
        }
    }
}
