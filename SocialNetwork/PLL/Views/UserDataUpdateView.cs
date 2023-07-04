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
    public class UserDataUpdateView
    {
        private readonly IUserUpdaterService _userUpdater;

        public UserDataUpdateView(IUserUpdaterService userUpdater)
        {
            _userUpdater = userUpdater;
        }
        public void Show(User user)
        {
            Console.Write("My name is: ");
            user.FirstName = Console.ReadLine();

            Console.Write("My last name is: ");
            user.LastName = Console.ReadLine();

            Console.Write("Link to my photo: ");
            user.Photo = Console.ReadLine();

            Console.Write("My favorite movie is: ");
            user.FavoriteMovie = Console.ReadLine();

            Console.Write("My favorite book is: ");
            user.FavoriteBook = Console.ReadLine();

            _userUpdater.Update(user);

            SuccessMessage.Show("Profile details updated succesfully!");
        }
    }
}
