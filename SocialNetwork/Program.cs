using SocialNetwork.BLL.Exceptions;
using SocialNetwork.BLL.Models;
using SocialNetwork.BLL.Services;
using SocialNetwork.DAL.Repositories;

namespace SocialNetwork
{
    internal class Program
    {

        public static UserService userService = new UserService(new UserRepository());
        static void Main(string[] args)
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
                            UserAuthentification();
                            break;
                        }
                    case "2":
                        {
                            UserRegistrationPrompt();
                            break;
                        }

                    default: break;
                }
            }

            Console.ReadLine();
        }

        public static void UserRegistrationPrompt()
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
                userService.Register(userData);
                Console.WriteLine("Registration went successfully!");
            }
            catch (ArgumentNullException ex)
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

        public static void UserAuthentification()
        {
            var authenticationData = new UserAuthenticationData();

            Console.Write("Enter email: ");
            authenticationData.Email = Console.ReadLine();

            Console.Write("Enter password: ");
            authenticationData.Password = Console.ReadLine();

            try
            {
                User user = userService.Authenticate(authenticationData);

                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine($"You succesfully signed in, {user.FirstName}");
                Console.ForegroundColor = ConsoleColor.White;

                UserControlOptions();
            }
            catch (WrongPasswordException)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Wrong password!");
                Console.ForegroundColor = ConsoleColor.White;
            }
            catch (UserNotFoundException)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("This email is not registered!");
                Console.ForegroundColor = ConsoleColor.White;
            }
        }

        public static void UserControlOptions()
        {

        }
    }
}