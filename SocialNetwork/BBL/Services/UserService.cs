using SocialNetwork.BBL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using SocialNetwork.DAL.Repositories;
using SocialNetwork.DAL.Entities;

namespace SocialNetwork.BBL.Services
{
    public class UserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public void Register(UserRegistrationData userRegistrationData)
        {
            if (String.IsNullOrEmpty(userRegistrationData.FirstName))
            {
                throw new ArgumentNullException(nameof(userRegistrationData.FirstName));
            }
            if (String.IsNullOrEmpty(userRegistrationData.LastName))
            {
                throw new ArgumentNullException(nameof(userRegistrationData.LastName));
            }
            if (String.IsNullOrEmpty(userRegistrationData.Password))
            {
                throw new ArgumentNullException(nameof(userRegistrationData.Password));
            }
            if (String.IsNullOrEmpty(userRegistrationData.Email))
            {
                throw new ArgumentNullException(nameof(userRegistrationData.Email));
            }

            if (userRegistrationData.Password.Length < 8)
            {
                throw new ArgumentException("Wrong password lenght, less than 8 characters", nameof(userRegistrationData.Password));
            }
            if (!new EmailAddressAttribute().IsValid(userRegistrationData.Email))
            {
                throw new ArgumentException("Email is not valid", nameof(userRegistrationData.Email));
            }

            if (_userRepository.FindByEmail(userRegistrationData.Email) is not null)
            {
                throw new ArgumentException("Email is already registered", nameof(userRegistrationData.Email));
            }

            var userEntity = new UserEntity()
            {
                firstname = userRegistrationData.FirstName,
                lastname = userRegistrationData.LastName,
                email = userRegistrationData.Email,
                password = userRegistrationData.Password
            };

            if (_userRepository.Create(userEntity) == 0)
            {
                throw new Exception("Registration failed");
            }
        }
    }
}
