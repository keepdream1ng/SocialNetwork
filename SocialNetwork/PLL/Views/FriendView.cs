using SocialNetwork.BLL.Models;
using SocialNetwork.BLL.Services;
using SocialNetwork.PLL.Highlights;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialNetwork.PLL.Views
{
    public class FriendView
    {
        private readonly IFriendsService _friendsService;
        private readonly NewMessageView _newMessageView;
        private readonly ProfileInfoView _profileInfoView;

        public FriendView(
            IFriendsService friendsService,
            NewMessageView newMessageView,
            ProfileInfoView profileInfoView
            )
        {
            _friendsService = friendsService;
            _newMessageView = newMessageView;
            _profileInfoView = profileInfoView;
        }

        public void Show(IUser user, IUser friend)
        {
            Console.WriteLine($"Welcome to the {friend.FirstName} {friend.LastName} page!");
            Console.WriteLine("See info (press 1)");
            Console.WriteLine("Send a message (press 2)");
            Console.WriteLine("Remove from list (press 3)");
            Console.WriteLine("Any other key to exit.");

            try
            {
                switch (Console.ReadLine())
                {
                    case "1":
                        {
                            _profileInfoView.Show(friend);
                            break;
                        }
                    case "2":
                        {
                            _newMessageView.Show(user, friend.Email);
                            break;
                        }
                    case "3":
                        {
                            _friendsService.RemoveFromFriendsList(user, friend);
                            break;
                        }
                    default: break;
                }
            }
            catch (Exception ex)
            {
                AlertMessage.Show(ex.Message);
            }
        }
    }
}
