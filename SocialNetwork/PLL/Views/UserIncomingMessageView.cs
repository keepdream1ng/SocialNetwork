using SocialNetwork.BLL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialNetwork.PLL.Views
{
    public class UserIncomingMessageView
    {
        public void Show(List<Message> messages)
        {
            if (messages.Count == 0)
            {
                Console.WriteLine("You have no messages!");
                return;
            }
            Console.WriteLine(String.Join("\n", messages));
        }
    }
}
