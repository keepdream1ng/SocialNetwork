using SocialNetwork.BLL.Models;

namespace SocialNetwork.BLL.Services
{
    public interface IRegistrationService
    {
        void Register(UserRegistrationData userRegistrationData);
    }
}