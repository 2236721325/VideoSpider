using HappyVideo.Core.Models;
using HappyVideo.Core.Services.Contracts;
using HtmlAgilityPack;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using System.Xml.XPath;

namespace HappyVideo.Core.Services
{

    public class Spider:ISpider
    {
        private SpiderRule? _rule;

        private readonly HttpClient _client;

        public Spider(HttpClient client)
        {
            _client = client;
        }

        public void Init(SpiderRule rule)
        {
            _rule = rule;
        }


        private async Task<HtmlDocument> LoadAsync(string url)
        {
            var response = await _client.GetAsync(url);
            HtmlDocument doc = new HtmlDocument();
            doc.Load(await response.Content.ReadAsStreamAsync());
            return doc;
        }
        public async Task<SearchResult> SearchAsync(string name)
        {

            var target_url = _rule.SourceUrl + _rule.SearchUrl
                                    .Replace("{key}", name);


            var doc = await LoadAsync(target_url);

            var nodes=doc.DocumentNode.SelectNodes(_rule.SearchListRule.XPath);


            var result_list = new List<VideoSearchInfo>();
            foreach (var node in nodes)
            {
                var url_node = node.SelectSingleNode(_rule.UrlRule.XPath);
                string url = string.Empty;
                if(_rule.UrlRule.AttributeSelector is not null)
                {
                    url = url_node.Attributes[_rule.UrlRule.AttributeSelector].Value;
                }
                else
                {
                    url = url_node.InnerText;
                }
                var image_node = node.SelectSingleNode(_rule.ImagePathRule.XPath);
                string image_path= string.Empty;
                if(_rule.ImagePathRule.AttributeSelector is not null)
                {
                    image_path= image_node.Attributes[_rule.ImagePathRule.AttributeSelector].Value;
                }
                else
                {
                    image_path= image_node.InnerText;
                }

                string video_name = string.Empty;
                var video_name_node= node.SelectSingleNode(_rule.VideoNameRule.XPath);
                if(_rule.VideoNameRule.AttributeSelector is not null)
                {
                    video_name = video_name_node.Attributes[_rule.VideoNameRule.AttributeSelector].Value;
                }
                else
                {
                    video_name = video_name_node.InnerText;
                }


                result_list.Add(new VideoSearchInfo(name, url, image_path));


            }
            return new SearchResult(result_list,name);
        }
    }
}
