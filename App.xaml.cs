using ASI_GuessTheNumber.ViewModel;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Http;
using System.Configuration;
using System.Data;
using System.Windows;

namespace ASI_GuessTheNumber
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton<IGuessApiService, GuessApiService>(); 
            services.AddHttpClient<GuessApiService>();

           // services.AddHttpClient<IGuessApiService, GuessApiService>(client => { client.BaseAddress = new Uri("http://localhost:5000"); });

        }
    }

}
