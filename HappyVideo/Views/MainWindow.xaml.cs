using HappyVideo.ViewModels;
using HappyVideo.Views;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace HappyVideo
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public IServiceProvider _IProvider { get; set; }
        public MainWindow(MainViewModel viewModel, IServiceProvider iProvider)
        {
            InitializeComponent();
            DataContext = viewModel;
            ViewModel = viewModel;
            _IProvider = iProvider;
        }

        public MainViewModel ViewModel { get; set; }

        private async void Window_Loaded(object sender, RoutedEventArgs e)
        {
            MainFrame.Navigated += async (s, e) =>
            {

                if(e.Content is IPage page)
                {
                   await page.ViewModel.InitAsync();
                }

            };


            var page_object  = _IProvider.GetService(typeof(SearchPage));
            
            MainFrame.Navigate(page_object);
        }
    }
}
