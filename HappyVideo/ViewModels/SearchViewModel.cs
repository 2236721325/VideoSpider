using CommunityToolkit.Mvvm.ComponentModel;
using HappyVideo.Core.Models;
using HappyVideo.Core.Services.Contracts;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HappyVideo.ViewModels
{
    public partial class SearchViewModel : ObservableObject, IViewModel
    {
        private readonly ISpider _Spider;
        public SearchViewModel(ISpider spider)
        {
            _Spider = spider;
        }

        [ObservableProperty]
        SearchResult searchResult;

        public async Task InitAsync()
        {

            SearchResult = await _Spider.SearchAsync("海绵宝宝");

            Debug.WriteLine(message: "hello world ");
        }
    }
}
