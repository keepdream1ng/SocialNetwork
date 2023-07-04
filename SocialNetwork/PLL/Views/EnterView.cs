using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialNetwork.PLL.Views
{
    public class EnterView
    {
        private readonly UserAuthentificationView _userAuthentificationView;
        private readonly RegistrationView _registrationView;

        public EnterView(UserAuthentificationView userAuthentificationView, RegistrationView registrationView)
        {
            _userAuthentificationView = userAuthentificationView;
            _registrationView = registrationView;
        }
        public void Show()
        {
            Console.WriteLine("Welcome to our console social network");
            while (true)
            {
                Console.WriteLine("Sign in (press 1)");
                Console.WriteLine("Register (press 2)");
                switch (Console.ReadLine())
                {
                    case "1":
                        {
                            _userAuthentificationView.Show();
                            break;
                        }
                    case "2":
                        {
                            _registrationView.Show();
                            break;
                        }

                    default: break;
                }
            }
        }

    }
}
