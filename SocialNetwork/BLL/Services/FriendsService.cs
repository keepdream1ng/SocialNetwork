using SocialNetwork.BLL.Exceptions;
using SocialNetwork.BLL.Models;
using SocialNetwork.DAL.Entities;
using SocialNetwork.DAL.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialNetwork.BLL.Services
{
    public class FriendsService : IFriendsService
    {
        private readonly IFriendRepository _friendRepository;
        private readonly IUserRepository _userRepository;
        private readonly IUserAuthenticationService _userAuthenticationService;

        public FriendsService(
            IFriendRepository friendRepository,
            IUserRepository userRepository,
            IUserAuthenticationService userAuthenticationService
            )
        {
            _friendRepository = friendRepository;
            _userRepository = userRepository;
            _userAuthenticationService = userAuthenticationService;
        }

        public void AddToFriends(FriendGenerationData friendGenerationData)
        {
            UserEntity newFriend = _userRepository.FindByEmail(friendGenerationData.FriendEmail);
            if (newFriend == null)
            {
                throw new UserNotFoundException();
            }
            if (IsInRepository(friendGenerationData))
            {
                throw new ArgumentException("Friend is already added", nameof(friendGenerationData.FriendEmail));
            }

            var friendRecord = new FriendEntity()
            {
                user_id = friendGenerationData.UserId,
                friend_id = newFriend.id
            };

            if (_friendRepository.Create(friendRecord) == 0)
            {
                throw new Exception("Adding friend failed");
            }
        }

        public List<IUser> GetFriendsList(IUser user)
        {
            List<IUser> friendsList = _friendRepository.FindAllByUserId(user.Id)
                    .Select(friendEntity => _userRepository.FindById(friendEntity.friend_id))
                    .Select(userEntity => ConstructIUserModel(userEntity))
                    .ToList();
            return friendsList;
        }

        public void RemoveFromFriendsList(IUser user, IUser unfriendedUser)
        {
            FriendEntity friendsRecord = _friendRepository.FindAllByUserId(user.Id)
                .Where(friendEntity => friendEntity.friend_id == unfriendedUser.Id)
                .FirstOrDefault();
            if (_friendRepository.Delete(friendsRecord.id) == 0)
            {
                throw new Exception("Removing from friends failed");
            }
        }

        private IUser ConstructIUserModel(UserEntity user)
        {
            return _userAuthenticationService.FindByEmail(user.email);
        }

        private bool IsInRepository(FriendGenerationData friendRecord)
        {
            var record = _friendRepository.FindAllByUserId(friendRecord.UserId)
                .Select(friendEntity => _userRepository.FindById(friendEntity.friend_id))
                .Where(userEntity => userEntity.email == friendRecord.FriendEmail)
                .FirstOrDefault();

            return (record is not null);
        }
    }
}