using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Controls;

namespace ChatClient.Services
{
    public class NavigationService
    {
        public event EventHandler<UserControl> ContentChanged;

        private UserControl content;

        public UserControl Content
        {
            get { return content; }
            private set 
            { 
                content = value;
                ContentChanged(this, value);
            }
        }

        public void Navigate(UserControl userControl)
        {
            Content = userControl;
        }
    }
}
