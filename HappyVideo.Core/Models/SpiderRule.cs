using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HappyVideo.Core.Models
{
    public class SpiderRule
    {
        public string SourceUrl { get; set; }
        public string SourceName { get; set; }
        public string SearchUrl { get; set; }
        public XPathRule SearchListRule { get; set; }
        public XPathRule UrlRule { get; set; }

        public XPathRule ImagePathRule { get; set; }


        public XPathRule VideoNameRule  { get; set; }
    }




}
