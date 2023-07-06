using SocialNetwork.BLL.Models;
using SocialNetwork.BLL.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialNetwork.PLL.Views
{
    public class UserMenuView
    {
        private readonly IMessageService _messageService;
        private readonly ProfileInfoView _profileInfoView;
        private readonly UserDataUpdateView _userDataUpdateView;
        private readonly UserIncomingMessageView _userIncomingMessageView;
        private readonly FriendsListView _friendsListView;
        private readonly NewMessageView _newMessageView;
        public User User { get; set; }
        private List<Message> _incomingMessages => _messageService.GetIncomingMessagesByRecipient(User.Id);

        public UserMenuView(ProfileInfoView profileInfoView,
            UserDataUpdateView userDataUpdateView,
            IMessageService messageService,
            UserIncomingMessageView userIncomingMessageView,
            FriendsListView friendsListView,
            NewMessageView newMessageView)
        {
            _profileInfoView = profileInfoView;
            _userDataUpdateView = userDataUpdateView;
            _messageService = messageService;
            _userIncomingMessageView = userIncomingMessageView;
            _friendsListView = friendsListView;
            _newMessageView = newMessageView;
        }
        public void Show(User user)
        {
            User = user;
            while (true)
            {
                Console.WriteLine($"You have {_incomingMessages.Count} messages.");

                Console.WriteLine("See your profile info (press 1)");
                Console.WriteLine("Edit profile (press 2)");
                Console.WriteLine("Friends list (press 3)");
                Console.WriteLine("Write a message (press 4)");
                Console.WriteLine("See incoming messages (press 5)");
                Console.WriteLine("Exit profile (press 6)");

                switch (Console.ReadLine())
                {
                    case "1":
                        {
                            _profileInfoView.Show(user);
                            break;
                        }
                    case "2":
                        {
                            _userDataUpdateView.Show(user);
                            break;
                        }
                    case "3":
                        {
                            _friendsListView.Show(user);
                            break;
                        }
                    case "4":
                        {
                            _newMessageView.Show(user);
                            break;
                        }
                    case "5":
                        {
                            _userIncomingMessageView.Show(_incomingMessages);
                            break;
                        }
                    case "6":
                        {
                            return;
                        }
                    default: break;
                }
            }
        }
    }
}
