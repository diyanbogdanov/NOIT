using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace PAL_PlayAndLearn.Pages
{
    /// <summary>
    /// Interaction logic for RegisterPage.xaml
    /// </summary>
    public partial class RegisterPage : UserControl, ISwitchable
    {
        public RegisterPage()
        {
            InitializeComponent();
        }

        private void OpenLogInPage(object sender, MouseButtonEventArgs e)
        {
        }

        private void FocusOnUserEMail(object sender, MouseButtonEventArgs e)
        {
            Focus(sender, e);
        }

        private void FocusOnUserPasswordRegistrationReply(object sender, MouseButtonEventArgs e)
        {
            Focus(sender, e);
        }

        private void FocusOnUserPasswordRegistration(object sender, MouseButtonEventArgs e)
        {
            Focus(sender, e);
        }

        private void FocusOnUserNameRegistration(object sender, MouseButtonEventArgs e)
        {
            Focus(sender, e);
        }

        private void Focus(object sender, MouseButtonEventArgs e)
        {
            if (e.ClickCount == 1)
            {
                var label = (Label)sender;
                Keyboard.Focus(label.Target);
            }
        }

        public void UtilizeState(object state)
        {
            throw new NotImplementedException();
        }
    }
}
