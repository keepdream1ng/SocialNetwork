using SocialNetwork.BLL.Models;

namespace SocialNetwork.BLL.Services
{
    public interface IFriendsService
    {
        void AddToFriends(FriendGenerationData friendGenerationData);
        List<IUser> GetFriendsList(IUser user);
        void RemoveFromFriendsList(IUser user, IUser unfriendedUser);
    }
}