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
    public class FriendsService
    {
        private readonly IFriendRepository _friendRepository;
        private readonly IUserRepository _userRepository;

        public FriendsService(
            IFriendRepository friendRepository,
            IUserRepository userRepository
            )
        {
            _friendRepository = friendRepository;
            _userRepository = userRepository;
        }

        public void AddToFriends(FriendGenerationData friendGenerationData)
        {
            UserEntity newFriend = _userRepository.FindByEmail(friendGenerationData.FriendEmail);
            if (newFriend == null)
            {
                throw new UserNotFoundException();
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
            List<IUser> friendsList = new();
            friendsList.AddRange(
                _friendRepository.FindAllByUserId(user.Id)
                    .Select(friendEntity => _userRepository.FindById(friendEntity.friend_id))
                    .Select(userEntity => ConstructIUserModel(userEntity))
                );
            return friendsList;
        }

        //public void RemoveFromFriendsList(IUser user, IUser friend)
        //{

        //}

        private IUser ConstructIUserModel(UserEntity user)
        {
            return _userRepository.FindByEmail(user.email) as IUser;
        }
    }
}
