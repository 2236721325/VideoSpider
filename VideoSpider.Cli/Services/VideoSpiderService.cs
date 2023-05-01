using HtmlAgilityPack;
using System.Text.Json;
using System.Text.Json.Nodes;
using VideoSpiderCli.Models;
using VideoSpiderCli.Models;
using VideoSpiderCli.Services.M3UParser;

namespace VideoSpiderCli.Services
{
    public static class VideoSpiderService
    {

        private static string _search_url = "https://www.netfly.tv/vod/search/-------------.html?wd={0}";

        private static string _base_url = "https://www.netfly.tv";
        //public static async Task<List<VideoPlayDetail>?> SearchAsync(string video_name)
        //{
        //    var search_url = string.Format(_search_url, video_name);

        //    using (var client = new HttpClient())
        //    {
        //        var response = await client.GetAsync(search_url);


        //        HtmlDocument doc = new HtmlDocument();
        //        doc.Load(await response.Content.ReadAsStreamAsync());


        //        var div_result = doc.DocumentNode.SelectSingleNode("//div[@class='module-items module-card-items']");
        //        if (div_result == null)
        //        {
        //            return null;
        //        }


        //        var div_results = div_result.SelectNodes("./div[@class='module-card-item module-item']");
        //        var results = new List<VideoPlayDetail>();
        //        int i = 0;
        //        foreach (var node in div_results)
        //        {
        //            var type = node.SelectSingleNode("./div[@class='module-card-item-class']").InnerHtml.Trim();
        //            var url = _base_url + node.SelectSingleNode("./a[@class='module-card-item-poster']").Attributes["href"].Value;
        //            var div_info = node.SelectSingleNode("./div[@class='module-card-item-info']");
        //            var title = div_info.SelectSingleNode("./div[@class='module-card-item-title']").InnerText.Trim();
        //            var info_node = div_info.SelectNodes(".//div[@class='module-info-item-content']");
        //            var infos = info_node[0].InnerText.Trim();
        //            var actors = info_node[1].InnerText.Trim();


        //            var video_adress = new VideoAdress(title, url, type, infos, actors);

        //            var video_detail = await GetAllPlayAdressAsync(url);

        //            results.Add(video_detail);
        //            if (i == 2) break;
        //            i++;

        //        }

        //        return results;
        //    }
        //}


        public static async Task<List<VideoSimple>?> SimpleSearchAsync(string video_name,int count)
        {
            var search_url = string.Format(_search_url, video_name);

            using (var client = new HttpClient())
            {
                var response = await client.GetAsync(search_url);


                HtmlDocument doc = new HtmlDocument();
                doc.Load(await response.Content.ReadAsStreamAsync());


                var div_result = doc.DocumentNode.SelectSingleNode("//div[@class='module-items module-card-items']");
                if (div_result == null)
                {
                    return null;
                }


                var div_results = div_result.SelectNodes("./div[@class='module-card-item module-item']");
                var results = new List<VideoSimple>();
                int i = 0;
                 foreach (var node in div_results)
                {
                    if (i == 3) break;
                    i++;
                    var type = node.SelectSingleNode("./div[@class='module-card-item-class']").InnerHtml.Trim();
                    var a = node.SelectSingleNode("./a[@class='module-card-item-poster']");
                    var url = _base_url + a.Attributes["href"].Value;
                    var img_url = a.SelectSingleNode(".//img").Attributes["data-original"].Value;
                    var div_info = node.SelectSingleNode("./div[@class='module-card-item-info']");
                    var title = div_info.SelectSingleNode("./div[@class='module-card-item-title']").InnerText.Trim();
                    title += " - " + a.SelectSingleNode(".//div[@class='module-item-note']").InnerText.Trim();

                    var info_node = div_info.SelectNodes(".//div[@class='module-info-item-content']");
                    var infos = info_node[0].InnerText.Trim();
                    var actors = info_node[1].InnerText.Trim();


                    var video_simple = new VideoSimple(title, url, type, infos, actors, img_url);



                    results.Add(video_simple);

                }

                return results;
            }
        }

        public static async Task<VideoPlayDetail> LookDetailAsync(string detail_url)
        {
            using (var client = new HttpClient())
            {
                var response = await client.GetAsync(detail_url);


                HtmlDocument doc = new HtmlDocument();
                doc.Load(await response.Content.ReadAsStreamAsync());


                var div_module = doc.DocumentNode.SelectSingleNode("//div[@class='module']");
                if (div_module == null)
                {
                    return null;
                }

                var sources_div = div_module.SelectNodes(".//div[@id='y-playList']/div");
                var result_url = div_module.SelectNodes("./div[@id='panel1']");

                var playerAddressCollection = new List<PlayerAddressCollection>();
                for (int i = 0; i < sources_div.Count; i++)
                {
                    var source = sources_div[i].SelectSingleNode("./span").InnerText.Trim();
                    var address_List = result_url[i].SelectNodes(".//a[@class='module-play-list-link']")
                        .Select(e =>
                        {
                            return new PlayAdress(_base_url + e.Attributes["href"].Value, e.Attributes["title"].Value);
                        });

                    playerAddressCollection.Add(new PlayerAddressCollection(source, address_List.ToList()));

                }


                return new VideoPlayDetail(playerAddressCollection);
            }
        }

        public static async Task<VideoPlayDetail> GetAllPlayAdressAsync(string video_desrciption_url)
        {
            using (var client = new HttpClient())
            {
                var response = await client.GetAsync(video_desrciption_url);


                HtmlDocument doc = new HtmlDocument();
                doc.Load(await response.Content.ReadAsStreamAsync());


                var div_module = doc.DocumentNode.SelectSingleNode("//div[@class='module']");
                if (div_module == null)
                {
                    return null;
                }

                var sources_div = div_module.SelectNodes(".//div[@id='y-playList']/div");
                var result_url = div_module.SelectNodes("./div[@id='panel1']");

                var playerAddressCollection = new List<PlayerAddressCollection>();
                for (int i = 0; i < sources_div.Count; i++)
                {
                    var source = sources_div[i].SelectSingleNode("./span").InnerText.Trim();
                    var address_List = result_url[i].SelectNodes(".//a[@class='module-play-list-link']")
                        .Select(e =>
                        {
                            return new PlayAdress(_base_url + e.Attributes["href"].Value, e.Attributes["title"].Value);
                        });

                    playerAddressCollection.Add(new PlayerAddressCollection(source, address_List.ToList()));

                }


                return new VideoPlayDetail(playerAddressCollection);
            }
        }

        public static async Task<string> GetM3u8UrlAsync(string video_url)
        {
            try
            {
                using (var client = new HttpClient())
                    
                {
                    var response = await client.GetAsync(video_url);


                    HtmlDocument doc = new HtmlDocument();
                    doc.Load(await response.Content.ReadAsStreamAsync());


                    var iframe = doc.DocumentNode.SelectSingleNode("//div[@class='main']");
                    var script = iframe.SelectSingleNode(".//script[contains(.,'player_aaaa')]").InnerHtml.Replace("var player_aaaa=", "");

                    var node = JsonSerializer.Deserialize<JsonObject>(json: script);
                    var url_m3u8 = node["url"]?.ToString();

                    var index_m3u8_response = await client.GetAsync(url_m3u8);

                    M3UFile? index_m3u8 = M3UReader.Parse(await index_m3u8_response.Content.ReadAsStringAsync());

                    var mixed_url = url_m3u8.Replace("index.m3u8", "") + index_m3u8.Streams[0].Path;
                    return mixed_url;

                }

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message );
                return "报错了！！！----- 这不是俺的问题 这是 。。。 奈飞官网的这些地址就没法播放 我也很无奈！";
            }



        }
    }
}