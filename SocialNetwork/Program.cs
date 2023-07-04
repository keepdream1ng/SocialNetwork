using SocialNetwork.BLL.Exceptions;
using SocialNetwork.BLL.Models;
using SocialNetwork.BLL.Services;
using SocialNetwork.DAL.Repositories;
using Microsoft.Extensions;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using SocialNetwork.PLL;
using SocialNetwork.PLL.Views;

namespace SocialNetwork
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var host = CreateHostBuilder().Build();
            host.Services.GetRequiredService<IUI>().Run();
        }
        private static IHostBuilder CreateHostBuilder()
        {
            return Host.CreateDefaultBuilder()
                .ConfigureServices((context, services) =>
                {
                    services.AddSingleton<IUserRepository, UserRepository>();
                    services.AddSingleton<IRegistrationService, UserService>();
                    services.AddSingleton<IUserAuthenticationService, UserService>();
                    services.AddSingleton<IUserUpdaterService, UserService>();
                    services.AddSingleton<RegistrationView>();
                    services.AddSingleton<UserAuthentificationView>();
                    services.AddSingleton<UserDataUpdateView>();
                    services.AddSingleton<ProfileInfoView>();
                    services.AddSingleton<UserMenuView>();
                    services.AddSingleton<EnterView>();
                    services.AddSingleton<IUI, UI>();
                });
        }
    }
}