using SocialNetwork.BLL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialNetwork.PLL.Views
{
    public class ProfileInfoView
    {
        public void Show(User user)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("User profile info");
            Console.WriteLine("User Id: {0}", user.Id);
            Console.WriteLine("First name: {0}", user.FirstName);
            Console.WriteLine("Last name: {0}", user.LastName);
            Console.WriteLine("Email: {0}", user.Email);
            Console.WriteLine("Photo link: {0}", user.Photo);
            Console.WriteLine("Favorite movie: {0}", user.FavoriteMovie);
            Console.WriteLine("Favorite book: {0}", user.FavoriteBook);
            Console.ForegroundColor = ConsoleColor.White;
        }
    }
}
