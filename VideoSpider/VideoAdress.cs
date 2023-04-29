namespace VideoSpider
{
    public record VideoAdress(string Title, string VideoDescriptionUrl, string Type, string Infos,string Actors);
    public record PlayAdress(string Url,string Title);


    public record PlayerAddressCollection(string SourceType,List<PlayAdress> PlayAdresses);
 


    public class VideoPlayDetail
    {
        public VideoAdress? VideoAdress { get; set; }

        public List<PlayerAddressCollection> PlayerAddressCollections { get; set; }
        public VideoPlayDetail(List<PlayerAddressCollection> playerAddressCollections, VideoAdress videoAdress=null)
        {
            PlayerAddressCollections = playerAddressCollections;
            VideoAdress = videoAdress;
        }
    }

    

}