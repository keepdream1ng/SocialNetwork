using SocialNetwork.BLL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialNetwork.PLL.Views
{
    public class UserMenuView
    {
        private readonly ProfileInfoView _profileInfoView;
        private readonly UserDataUpdateView _userDataUpdateView;

        public UserMenuView(ProfileInfoView profileInfoView, UserDataUpdateView userDataUpdateView)
        {
            _profileInfoView = profileInfoView;
            _userDataUpdateView = userDataUpdateView;
        }
        public void Show(User user)
        {
            while (true)
            {

                Console.WriteLine("See your profile info (press 1)");
                Console.WriteLine("Edit profile (press 2)");
                Console.WriteLine("Add a friend (press 3)");
                Console.WriteLine("Write a message (press 4)");
                Console.WriteLine("Exit profile (press 5)");

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
                            throw new NotImplementedException();
                            break;
                        }
                    case "4":
                        {
                            throw new NotImplementedException();
                            break;
                        }
                    case "5":
                        {
                            return;
                        }
                }
            }
        }
    }
}
