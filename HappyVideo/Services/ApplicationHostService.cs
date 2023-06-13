using HappyVideo.Core.Models;
using HappyVideo.Core.Services.Contracts;
using HappyVideo.Models;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace HappyVideo.Services
{
    public class ApplicationHostService : IHostedService
    {
        private readonly IOptions<AppConfig> options;


        private readonly IServiceProvider _serviceProvider;

        private readonly ISpider _ISpider;

        public ApplicationHostService(IOptions<AppConfig> options, ISpider iSpider, IServiceProvider serviceProvider)
        {
            this.options = options;
            _ISpider = iSpider;
            _serviceProvider = serviceProvider;
        }
        public async Task StartAsync(CancellationToken cancellationToken)
        {
            //throw new NotImplementedException();

            var str = options.Value.CurrentRuleFilePath;

            var path = Path.Combine(Directory.GetCurrentDirectory(), options.Value.CurrentRuleFilePath);
            _ISpider.Init(
                JsonConvert.DeserializeObject<SpiderRule>(
                    File.ReadAllText(path)
                        )!
                    );


            await HandleActivationAsync();

            await Task.CompletedTask;
        }

        public async Task StopAsync(CancellationToken cancellationToken)
        {
            //throw new NotImplementedException();
            await Task.CompletedTask;
        }


        public async Task HandleActivationAsync()
        {
            var window = _serviceProvider.GetService(typeof(MainWindow)) as MainWindow;

            await window.ViewModel.InitAsync();

            App.Current.MainWindow = window;

            window.Show();

            await Task.CompletedTask;

        }
    }
}
