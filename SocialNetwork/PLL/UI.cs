using SocialNetwork.PLL.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialNetwork.PLL
{
    public class UI : IUI
    {
        private readonly EnterView _enterView;

        public UI(EnterView enterView)
        {
            _enterView = enterView;
        }

        public void Run()
        {
            _enterView.Show();
        }
    }
}
