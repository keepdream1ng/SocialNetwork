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
    public class MessageService : IMessageService
    {
        private readonly IMessageRepository _messageRepository;
        private readonly IUserRepository _userRepository;

        public MessageService(IMessageRepository messageRepository, IUserRepository userRepository)
        {
            _messageRepository = messageRepository;
            _userRepository = userRepository;
        }

        public void AddMessageToRepository(MessageGenerationData messageGenerationData)
        {
            if (String.IsNullOrEmpty(messageGenerationData.Content))
            {
                throw new ArgumentNullException(nameof(messageGenerationData));
            }
            if (messageGenerationData.Content.Length > 5000)
            {
                throw new ArgumentOutOfRangeException(nameof(messageGenerationData.Content));
            }

            var recipientEntity = _userRepository.FindByEmail(messageGenerationData.RecipientEmail);
            if (recipientEntity is null)
            {
                throw new UserNotFoundException();
            }

            MessageEntity newMessage = new MessageEntity()
            {
                sender_id = messageGenerationData.SenderId,
                content = messageGenerationData.Content,
                recipient_id = recipientEntity.id
            };

            if (_messageRepository.Create(newMessage) == 0)
            {
                throw new Exception("Adding message to database failed");
            }
        }

        public List<Message> GetOutgoingMessagesBySender(int senderId)
        {
            return _messageRepository.FindBySenderId(senderId)
                .Select(m => ConstructMessageModel(m))
                .ToList();
        }

        public List<Message> GetIncomingMessagesByRecipient(int recipientId)
        {
            return _messageRepository.FindByRecipientId(recipientId)
                .Select(m => ConstructMessageModel(m))
                .ToList();
        }

        private Message ConstructMessageModel(MessageEntity m)
        {
            return new Message(
                m.id,
                m.content,
                _userRepository.FindById(m.sender_id).email,
                _userRepository.FindById(m.recipient_id).email
                );
        }
    }
}
