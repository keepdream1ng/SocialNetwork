using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using SocialNetwork.DAL.Repositories;
using SocialNetwork.DAL.Entities;
using SocialNetwork.BLL.Exceptions;
using SocialNetwork.BLL.Models;

namespace SocialNetwork.BLL.Services
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

        public User Authenticate(UserAuthenticationData userAuthenticationData)
        {
            var findUserEntity = _userRepository.FindByEmail(userAuthenticationData.Email);
            if (findUserEntity is null) throw new UserNotFoundException();

            if (findUserEntity.password != userAuthenticationData.Password)
                throw new WrongPasswordException();

            return ConstructUserModel(findUserEntity);
        }

        public User FindByEmail(string email)
        {
            var findUserEntity = _userRepository.FindByEmail(email);
            if (findUserEntity is null) throw new UserNotFoundException();

            return ConstructUserModel(findUserEntity);
        }

        public void Update(User user)
        {
            var updatableUserEntity = new UserEntity()
            {
                id = user.Id,
                firstname = user.FirstName,
                lastname = user.LastName,
                password = user.Password,
                email = user.Email,
                photo = user.Photo,
                favorite_movie = user.FavoriteMovie,
                favorite_book = user.FavoriteBook
            };

            if (_userRepository.Update(updatableUserEntity) == 0)
                throw new Exception();
        }

        private User ConstructUserModel(UserEntity userEntity)
        {
            return new User(userEntity.id,
                          userEntity.firstname,
                          userEntity.lastname,
                          userEntity.password,
                          userEntity.email,
                          userEntity.photo,
                          userEntity.favorite_movie,
                          userEntity.favorite_book);
        }
    }
}
