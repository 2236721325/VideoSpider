using System.Text.Encodings.Web;
using System.Text.Json;
using System.Text.Unicode;

namespace VideoSpider
{

    internal class Program
    {
        static async Task Main(string[] args)
        {


            //Console.WriteLine("请输入电影名称？（此次为超级搜索 所以信息都包括 所以会很慢。。。。 好吧 是我懒得写了）只搜索前3项");

            //var name=Console.ReadLine();

            var spider = new VideoSpiderService();
            //var results = await spider.SearchAsync(name.Trim());
            //var json = JsonSerializer.Serialize(results, new JsonSerializerOptions()
            //{
            //    Encoder = JavaScriptEncoder.Create(UnicodeRanges.All),
            //    WriteIndented = true
            //});
            //Console.WriteLine("结果------------>");
            //Console.WriteLine(json);

            //Console.WriteLine("获取m3u8地址------------>");

            Console.WriteLine("请输入电影播放地址");
            var adress=Console.ReadLine();

            var result=await spider.GetM3u8UrlAsync(adress);

            Console.WriteLine("m3u8地址为");
            Console.WriteLine(result);

            Console.WriteLine();

            Console.WriteLine("然后 你只需要 用m3u8 下载器就可以把想看的视频下载下来了");


            while (true)
            {
                Console.ReadLine();
                Console.WriteLine("关不掉关不掉");
            }
            

        }
    }
}