using Microsoft.Win32;
using PAL_PlayAndLearn.ViewModels;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
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
    /// Interaction logic for Profile.xaml
    /// </summary>
    public partial class Profile : UserControl
    {
        public Profile()
        {
            InitializeComponent();
            Switcher.ProfileContentSwitcher = this.InformationContentControl;
            Switcher.Switch(Switcher.ProfileContentSwitcher, new ProfileContent());
        }

        private void EdditPicture(object sender, MouseButtonEventArgs e)
        {
            OpenFileDialog op = new OpenFileDialog();
            op.Title = "Избери изображение";
            op.Filter = "All supported graphics|*.jpg;*.jpeg;*.png|" +
                "JPEG (*.jpg;*.jpeg)|*.jpg;*.jpeg|" +
                "Portable Network Graphic (*.png)|*.png";
            if (op.ShowDialog() == true)
            {
                string url = op.FileName;
                string uploadedImageURL = UploadImage(url);
                if (!string.IsNullOrEmpty(uploadedImageURL))
                {
                    PALEntities dataContex = new PALEntities();
                    UserPersonalInformation userPersonalInfo = GetUserById(dataContex, ViewModels.ViewModel.ID);
                    userPersonalInfo.AvatarPicture = uploadedImageURL;                    
                    dataContex.SaveChanges();
                    ImageBrush.ImageSource = new BitmapImage(new Uri(uploadedImageURL));
                }
            }
        }

        private static string UploadImage(string imageURL)
        {
            string ClientId = "0ad329854c9fb71";
            WebClient w = new WebClient();
            w.Headers.Add("Authorization", "Client-ID " + ClientId);
            System.Collections.Specialized.NameValueCollection Keys = new System.Collections.Specialized.NameValueCollection();
            try
            {
                Keys.Add("image", Convert.ToBase64String(File.ReadAllBytes(imageURL)));
                byte[] responseArray = w.UploadValues("https://api.imgur.com/3/image", Keys);
                dynamic result = Encoding.ASCII.GetString(responseArray);
                System.Text.RegularExpressions.Regex reg = new System.Text.RegularExpressions.Regex("link\":\"(.*?)\"");
                Match match = reg.Match(result);
                string url = match.ToString().Replace("link\":\"", "").Replace("\"", "").Replace("\\/", "/");
                return url;
            }
            catch (Exception s)
            {
                WPFMessageBox.MessageBox.ShowWarning("Имахме проблем с качаването на изображението.\nМоля опитайте по-късно\n" + s.Message);
                return null;
            }
        }

        private UserPersonalInformation GetUserById(PALEntities dataContex, int id)
        {
            UserPersonalInformation userPersonalInfo = dataContex.UserPersonalInformations.FirstOrDefault(p => p.UserRegID == id);
            return userPersonalInfo;
        }
    }
}
