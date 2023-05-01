using SixLabors.ImageSharp.Memory;
using Spectre.Console;
using Spectre.Console.Cli;
using Spectre.Console.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Text.Encodings.Web;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Text.Unicode;
using System.Threading.Tasks;
using VideoSpiderCli.Services;

namespace VideoSpiderCli.Commands
{
    public class SearchCommand : Command<SearchCommand.Settings>
    {
        public override int Execute([NotNull] CommandContext context, [NotNull] Settings settings)
        {
            var results = VideoSpiderService.SimpleSearchAsync(settings.Name, settings.Count)
                .GetAwaiter().GetResult();
            var json = JsonSerializer.Serialize(results, new JsonSerializerOptions()
            {
                Encoder = JavaScriptEncoder.Create(UnicodeRanges.All),
                WriteIndented = true
            });



            using var client = new HttpClient();
            client.Timeout = TimeSpan.FromSeconds(2);
            List<Layout>? rows = new List<Layout>();
            foreach (var result in results)
            {
                CanvasImage? image = null;
                try
                {
                    var response = client.GetAsync(result.ImageUrl).GetAwaiter().GetResult();
                    var stream = response.Content.ReadAsStreamAsync().GetAwaiter().GetResult();

                    image = new CanvasImage(stream);
                    image.PixelWidth(1);
                    image.BilinearResampler();


                }
                catch (Exception ex)
                {
                    AnsiConsole.WriteException(ex);
                }





                IRenderable content = image;
                if (image == null)
                {
                    content = Align.Center(new Text("图片请求失败！"), VerticalAlignment.Middle);
                }
                var pannel = new Panel(content)
                {
                    Header = new PanelHeader("图片", Justify.Center),
                    Height = 27,
                    Width = 27
                };

                var rights = new Panel(Align.Center(new Rows(

                             Align.Center(new Text(result.Title)),
                             Align.Center(new Text(result.Type)),
                             Align.Center(new Text(result.Infos)),
                             Align.Center(new Text(result.Actors)),
                             Align.Center(new Text(result.VideoDetailUrl))
                             ), VerticalAlignment.Middle))
                  ;
                rights.Header = new PanelHeader("详细信息", Justify.Center);
                rights.Height = 27;


                rows.Add(
                    new Layout("Root")
                    .SplitColumns(
                        new Layout("Left")
                        .Update(pannel),
                        new Layout("Right")
                        .Update(rights)
                        )
                    );





            }
            // Render the layout
            AnsiConsole.Write(new Rows(
                rows
            ));



            return 0;

        }

        public class Settings : CommandSettings
        {
            [CommandArgument(0, "[Name]")]
            public string Name { get; set; }

            [CommandOption("--resultCount")]
            [DefaultValue(3)]
            public int Count { get; set; }
        }



    }
}
