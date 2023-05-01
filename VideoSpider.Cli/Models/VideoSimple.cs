namespace VideoSpiderCli.Models
{
    public class VideoSimple
    {
        public string Title { get; set; }
        public string VideoDetailUrl { get; set; }
        public string Type { get; set; }
        public string Infos { get; set; }
        public string Actors { get; set; }
        public string ImageUrl { get; set; }

        public VideoSimple(string title, string videoDetailUrl, string type, string infos, string actors, string imageUrl)
        {
            Title = title;
            VideoDetailUrl = videoDetailUrl;
            Type = type;
            Infos = infos;
            Actors = actors;
            ImageUrl = imageUrl;
        }
    }



}