using System.Collections;
using System.Collections.Generic;

namespace HappyVideo.Core.Models
{
    public class SearchResult
    {
        public string SearchContent { get; set; }
        public IEnumerable<VideoSearchInfo> SearchResults { get; set; }

        public SearchResult(IEnumerable<VideoSearchInfo> searchResults, string searchContent)
        {
            SearchResults = searchResults;
            SearchContent = searchContent;
        }

    }
    public class VideoSearchInfo
    {
        public string Name { get; set; }
        public string Url { get; set; }
        public string ImagePath { get; set; }
        public VideoSearchInfo(string name, string url, string imagePath)
        {
            Name = name;
            Url = url;
            ImagePath = imagePath;
        }
    }



}
