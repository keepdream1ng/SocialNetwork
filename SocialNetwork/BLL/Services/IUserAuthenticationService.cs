using SocialNetwork.BLL.Models;

namespace SocialNetwork.BLL.Services
{
    public interface IUserAuthenticationService
    {
        User Authenticate(UserAuthenticationData userAuthenticationData);
        User FindByEmail(string email);
    }
}