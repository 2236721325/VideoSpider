using HappyVideo.Core.Models;
using Newtonsoft.Json;

namespace ConsoleApp2
{
   
    internal class Program
    {
        static void Main(string[] args)
        {
            var rule = new SpiderRule()
            {
                SourceUrl = "/vod/search/-------------.html?wd={key}",
                SourceName = "奈菲影视",
                SearchUrl = "https://www.naifei.im",
            };
            var json=JsonConvert.SerializeObject(rule);
            Console.WriteLine(json);
        }
    }
}