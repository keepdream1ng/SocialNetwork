using SocialNetwork.BBL.Models;
using SocialNetwork.BBL.Services;
using SocialNetwork.DAL.Repositories;

namespace SocialNetwork
{
    internal class Program
    {

        public static UserService userService = new UserService(new UserRepository());
        static void Main(string[] args)
        {
            while (true)
            {
                UserRegistrationPrompt();
            }

            Console.ReadLine();
        }

        public static void UserRegistrationPrompt()
        {
            Console.WriteLine("Welcome to our console social network");
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
                userService.Register(userData);
                Console.WriteLine("Registration went successfully!");
            }
            catch(ArgumentNullException ex)
            {
                Console.WriteLine($"{ex.ParamName} is empty");
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine($"Enter correct value of {ex.ParamName}");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}