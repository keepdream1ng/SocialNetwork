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
    public class RegistrationView
    {
        private IRegistrationService _registrationService;

        public RegistrationView(IRegistrationService registrationService)
        {
            _registrationService = registrationService;
        }
        public void Show()
        {
            var userData = new UserRegistrationData();
            Console.Write("Incert first name for registration: ");
            userData.FirstName = Console.ReadLine();
            Console.Write("Now incert last name: ");
            userData.LastName = Console.ReadLine();
            Console.Write("Now incert password (8+ characters): ");
            userData.Password = Console.ReadLine();
            Console.Write("Now incert email: ");
            userData.Email = Console.ReadLine();

            try
            {
                _registrationService.Register(userData);
                SuccessMessage.Show("Registration went successfully!");
            }
            catch (ArgumentNullException ex)
            {
                AlertMessage.Show($"{ex.ParamName} is empty");
            }
            catch (ArgumentException ex)
            {
                AlertMessage.Show($"Enter correct value of {ex.ParamName}");
            }
            catch (Exception ex)
            {
                AlertMessage.Show(ex.Message);
            }
        }
    }
}
