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
                    // Data access level.
                    services.AddSingleton<IUserRepository, UserRepository>();
                    services.AddSingleton<IMessageRepository, MessageRepository>();
                    services.AddSingleton<IFriendRepository, FriendRepository>();
                    // Bisiness logic level.
                    services.AddSingleton<IRegistrationService, UserService>();
                    services.AddSingleton<IUserAuthenticationService, UserService>();
                    services.AddSingleton<IUserUpdaterService, UserService>();
                    services.AddSingleton<IMessageService, MessageService>();
                    services.AddSingleton<IFriendsService, FriendsService>();
                    // Presentation logic level.
                    services.AddScoped<RegistrationView>();
                    services.AddScoped<UserAuthentificationView>();
                    services.AddScoped<UserDataUpdateView>();
                    services.AddScoped<ProfileInfoView>();
                    services.AddScoped<NewMessageView>();
                    services.AddScoped<UserIncomingMessageView>();
                    services.AddScoped<FriendView>();
                    services.AddScoped<FriendsListView>();
                    services.AddScoped<UserMenuView>();
                    services.AddScoped<EnterView>();
                    services.AddScoped<IUI, UI>();
                });
        }
    }
}