using SocialNetwork.BLL.Exceptions;
using SocialNetwork.BLL.Models;
using SocialNetwork.BLL.Services;
using SocialNetwork.DAL.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SociaNetworkTest.BLL
{
    public class FriendsSecrviceTests
    {
        [Fact]
        public void AddToFriends_ShouldThrowUserNotFoundException()
        {
            FriendGenerationData friendData = new FriendGenerationData() { UserId = 1, FriendEmail = "test@mail.com" };

            var moqUserRepository = new Mock<IUserRepository>();
            moqUserRepository.Setup(r => r.FindByEmail(friendData.FriendEmail)).Returns<User>(null);

            FriendsService friendsService = new FriendsService(null, moqUserRepository.Object, null);

            Assert.Throws<UserNotFoundException>(() => friendsService.AddToFriends(friendData));
        }
    }
}
