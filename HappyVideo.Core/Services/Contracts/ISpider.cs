using HappyVideo.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HappyVideo.Core.Services.Contracts
{
    public  interface ISpider
    {
        public void Init(SpiderRule rule);
        public Task<SearchResult> SearchAsync(string name);

    }
}
