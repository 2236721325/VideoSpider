namespace VideoSpiderCli.Models
{
    public class VideoPlayDetail
    {
        public List<PlayerAddressCollection> PlayerAddressCollections { get; set; }
        public VideoPlayDetail(List<PlayerAddressCollection> playerAddressCollections)
        {
            PlayerAddressCollections = playerAddressCollections;
        }
    }



}