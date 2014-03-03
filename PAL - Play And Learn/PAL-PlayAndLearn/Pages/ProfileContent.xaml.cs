using PAL_PlayAndLearn.Behavior;
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
    /// Interaction logic for ProfileContent.xaml
    /// </summary>
    public partial class ProfileContent : UserControl
    {
        public ProfileContent()
        {
            InitializeComponent();
        }

        private void OpenProfile(object sender, RoutedEventArgs e)
        {
            Switcher.Switch(Switcher.ProfileContentSwitcher, new ProfileContentEdit());
        }
    }
}
