using HappyVideo.ViewModels;

namespace HappyVideo.Views
{
    public interface IPage
    {
        public IViewModel ViewModel { get; set; }
    }
}
