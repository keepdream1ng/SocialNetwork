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
    public class UserAuthentificationView
    {
        private IUserAuthenticationService _authenticationService;
        private readonly UserMenuView _userMenuView;

        public UserAuthentificationView(IUserAuthenticationService authenticationService, UserMenuView userMenuView)
        {
            _authenticationService = authenticationService;
            _userMenuView = userMenuView;
        }
        public void Show()
        {
            var authenticationData = new UserAuthenticationData();

            Console.Write("Enter email: ");
            authenticationData.Email = Console.ReadLine();

            Console.Write("Enter password: ");
            authenticationData.Password = Console.ReadLine();

            try
            {
                User user = _authenticationService.Authenticate(authenticationData);
                SuccessMessage.Show($"You succesfully signed in, {user.FirstName}");

                _userMenuView.Show(user);
            }
            catch (WrongPasswordException)
            {
                AlertMessage.Show("Wrong password!");
            }
            catch (UserNotFoundException)
            {
                AlertMessage.Show("This email is not registered!");
            }
        }
    }
}
