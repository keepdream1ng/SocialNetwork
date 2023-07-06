using SocialNetwork.BLL.Exceptions;
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
    public class FriendsListView
    {
        private readonly IFriendsService _friendsService;
        private readonly FriendView _friendView;

        private IUser _user { get; set; }
        private List<IUser> _friendsList => _friendsService.GetFriendsList(_user);

        public FriendsListView(
            IFriendsService friendsService,
            FriendView friendView
            )
        {
            _friendsService = friendsService;
            _friendView = friendView;
        }

        public void Show(IUser user)
        {
            _user = user;
            while (true)
            {
                Console.WriteLine($"{_friendsList.Count} friend(s) in the list.");
                Console.WriteLine("Add a new friend by email (press 0)");
                Console.WriteLine("Press any letter key to return");
                if (_friendsList.Count > 0)
                {
                    int i = 1;
                    foreach (IUser friend in _friendsList)
                    {
                        Console.WriteLine($"{i++}. {friend.FirstName} {friend.LastName} email: {friend.Email}");
                    }
                    Console.WriteLine("Press number to see more options for interacting with associated friend.");
                }
                string input = Console.ReadLine();
                if (uint.TryParse(input, out uint num))
                {
                    if (num == 0)
                    {
                        NewFriendCreation();
                    }
                    else
                    {
                        if (num <= _friendsList.Count)
                        {
                            _friendView.Show(user, _friendsList[(int)num - 1]);
                        }
                    }
                }
                else return;
            }
        }

        private void NewFriendCreation()
        {
            FriendGenerationData friendData = new();
            friendData.UserId = _user.Id;
            Console.Write("Incert friend email: ");
            friendData.FriendEmail = Console.ReadLine();

            try
            {
                _friendsService.AddToFriends(friendData);
                SuccessMessage.Show("Friend added successfully!");
            }
            catch (UserNotFoundException ex)
            {
                AlertMessage.Show("Cant find this profile");
            }
            catch (Exception ex)
            {
                AlertMessage.Show(ex.Message);
            }
        }
    }
}
