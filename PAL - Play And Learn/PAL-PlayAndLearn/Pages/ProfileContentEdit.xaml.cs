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
    /// Interaction logic for ProfileContentEdit.xaml
    /// </summary>
    public partial class ProfileContentEdit : UserControl
    {
        public ProfileContentEdit()
        {
            InitializeComponent();
        }

        public static int SelectedClass
        {
            get
            {
                return 10;
            }
        }

        private void CancleEditing(object sender, RoutedEventArgs e)
        {
            Switcher.Switch(Switcher.ProfileContentSwitcher, new ProfileContent());
        }

        private void SaveChanges(object sender, RoutedEventArgs e)
        {
            PALEntities dataContex = new PALEntities();
            UserPersonalInformation userPersonalInfo = GetUserById(dataContex, ViewModels.ViewModel.ID);
            if (!(string.IsNullOrEmpty(CityNameTextBox.Text) && string.IsNullOrWhiteSpace(CityNameTextBox.Text)))
            {
                userPersonalInfo.CityName = CityNameTextBox.Text;
            }
            if (!(string.IsNullOrEmpty(SchoolNameTextBox.Text) && string.IsNullOrWhiteSpace(SchoolNameTextBox.Text)))
            {
                userPersonalInfo.SchoolName = SchoolNameTextBox.Text;
            }
            if (!(string.IsNullOrEmpty(YearsOldTextBox.Text) && string.IsNullOrWhiteSpace(YearsOldTextBox.Text)))
            {
                userPersonalInfo.YearsOld = YearsOldTextBox.Text;
            }
            if (!(ClassComboBox.SelectedIndex < 0))
            {
                ComboBoxItem cbItem = (ComboBoxItem)ClassComboBox.SelectedItem;
                userPersonalInfo.Class = cbItem.Content.ToString();
            }
            if (!(SexComboBox.SelectedIndex < 0))
            {
                //var editt = (TextBox)SexComboBox.Template.FindName("ClassComboBox", SexComboBox);
                ComboBoxItem cbItem = (ComboBoxItem)SexComboBox.SelectedItem;
                userPersonalInfo.Sex = cbItem.Content.ToString();
            }            
            dataContex.SaveChanges();
            Switcher.Switch(Switcher.ProfileContentSwitcher, new ProfileContent());
        }

        private UserPersonalInformation GetUserById(PALEntities dataContex, int id)
        {
            UserPersonalInformation userPersonalInfo = dataContex.UserPersonalInformations.FirstOrDefault(p => p.UserRegID == id);
            return userPersonalInfo;
        }
    }
}
