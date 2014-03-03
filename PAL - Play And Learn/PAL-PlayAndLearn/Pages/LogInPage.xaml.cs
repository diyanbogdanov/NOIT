using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using PAL_PlayAndLearn.ViewModels;

namespace PAL_PlayAndLearn.Pages
{
    /// <summary>
    /// Interaction logic for LogInPage.xaml
    /// </summary>
    public partial class LogInPage : UserControl, ISwitchable
    {
        public LogInPage()
        {
            InitializeComponent();
        }

        private void FocusOnUserName(object sender, MouseButtonEventArgs e)
        {
            Focus(sender, e);
        }

        private void FocusOnUserPassword(object sender, MouseButtonEventArgs e)
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
