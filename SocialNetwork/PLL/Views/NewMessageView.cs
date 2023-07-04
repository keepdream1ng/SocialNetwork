using SocialNetwork.BLL.Exceptions;
using SocialNetwork.BLL.Models;
using SocialNetwork.BLL.Services;
using SocialNetwork.PLL.Highlights;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialNetwork.PLL.Views
{
    public class NewMessageView
    {
        private readonly IMessageService _messageService;

        public NewMessageView(IMessageService messageService)
        {
            _messageService = messageService;
        }

        public void Show(User user)
        {
            var messageData = new MessageGenerationData();
            messageData.SenderId = user.Id;

            Console.Write("Enter recipient email: ");
            messageData.RecipientEmail = Console.ReadLine();

            Console.Write("Enter message content: ");
            messageData.Content = Console.ReadLine();

            try
            {
                _messageService.AddMessageToRepository(messageData);
                SuccessMessage.Show("Message sended successfully!");
            }
            catch (ArgumentNullException ex)
            {
                AlertMessage.Show("Message is empty.");
            }
            catch (ArgumentOutOfRangeException ex)
            {
                AlertMessage.Show("Message is too long.");
            }
            catch (UserNotFoundException ex)
            {
                AlertMessage.Show("This recipient had not registered yet.");
            }
            catch (Exception ex)
            {
                AlertMessage.Show($"Error occured: {ex.Message}");
            }
        }
    }
}
