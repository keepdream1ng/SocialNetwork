using SocialNetwork.BLL.Models;

namespace SocialNetwork.BLL.Services
{
    public interface IMessageService
    {
        void AddMessageToRepository(MessageGenerationData messageGenerationData);
        List<Message> GetIncomingMessagesByRecipient(int recipientId);
        List<Message> GetOutgoingMessagesBySender(int senderId);
    }
}