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
                            if (UserAuthentification(out User user))
                            {
                                UserControlOptions(user);
                            }
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

        public static bool UserAuthentification(out User user)
        {
            var authenticationData = new UserAuthenticationData();

            Console.Write("Enter email: ");
            authenticationData.Email = Console.ReadLine();

            Console.Write("Enter password: ");
            authenticationData.Password = Console.ReadLine();

            try
            {
                user = userService.Authenticate(authenticationData);

                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine($"You succesfully signed in, {user.FirstName}");
                Console.ForegroundColor = ConsoleColor.White;

                return true;
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
            user = null;
            return false;
        }

        public static void UserControlOptions(User user)
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
                            ShowProfileInfo(user);
                            break;
                        }
                    case "2":
                        {
                            ChangeProfileDetails(user);
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

        public static void ShowProfileInfo(User user)
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

        public static void ChangeProfileDetails(User user)
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

            userService.Update(user);

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Profile details updated succesfully!");
            Console.ForegroundColor = ConsoleColor.White;
        }
    }
}