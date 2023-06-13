using CommunityToolkit.Mvvm.ComponentModel;
using HappyVideo.Core.Models;
using HappyVideo.Core.Services.Contracts;
using System;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HappyVideo.ViewModels
{
    public partial class MainViewModel : ObservableObject, IViewModel
    {
        public async Task InitAsync()
        {
            await Task.CompletedTask;
        }
    }
}
