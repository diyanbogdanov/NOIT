using PAL_PlayAndLearn.Pages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Windows;
using PAL_PlayAndLearn.ViewModels;

namespace PAL_PlayAndLearn.LogInRegisterCommands
{
    class LogInRegisterCommand
    {
        public static void RegisterAndLogInWithValues(PALEntities db, string UserName ,string Password, string EMail)
        {
            RegisterWithValues(db, UserName, Password, EMail);
            LogInFromRegister();
        }

        public static void LogInWithValues(string UName, string Password)
        {
            int currentID = LogInReturnId(UName, Password);
            if (currentID != 0)
            {
                ViewModel.ID = currentID;
                Switcher.Switch(Switcher.StartUpProgramSwitcher, new MainProgram());
            }
            else
            {
                WPFMessageBox.MessageBox.ShowError("Грешно име или парола.");
            }
            
        }

        protected static int LogInReturnId(string EMail, string Password)
        {
            Password = GetSha1HashData(Password);
            PALEntities db = new PALEntities();
            return db.UserRegistrations.Where(u => u.UserEMail == EMail && u.UserPassword == Password).Select(c => c.ID).SingleOrDefault();
        }

        protected static void RegisterWithValues(PALEntities db, string UserName, string Password, string EMail)
        {
            string Sha1Password = GetSha1HashData(Password);
            UserRegistration newUser = new UserRegistration
            {
                UserName = UserName,
                UserPassword = Sha1Password,
                UserEMail = EMail
            };
            db.UserRegistrations.Add(newUser);
            db.SaveChanges();
            ViewModel.ID = newUser.ID;
            UserPersonalInformation newUserPersonalInfo = new UserPersonalInformation
            {
                AvatarPicture = "http://i.imgur.com/xTMBZYr.jpg",
                UserRegID = ViewModel.ID
            };
            db.SaveChanges();
            db.UserPersonalInformations.Add(newUserPersonalInfo);
            UserGameInformation newUserGameInfo = new UserGameInformation
            {
                UserRegID = ViewModel.ID
            };
            db.UserGameInformations.Add(newUserGameInfo);
            db.SaveChanges();
        }

        protected static void LogInFromRegister()
        {
            Switcher.Switch(Switcher.StartUpProgramSwitcher, new MainProgram());
        }

        private static string GetSha1HashData(string passwrod)
        {
            SHA1CryptoServiceProvider sha1Algorithm = new SHA1CryptoServiceProvider();
            byte[] bytes = Encoding.UTF8.GetBytes(passwrod);
            string sha1Hash = BitConverter.ToString(sha1Algorithm.ComputeHash(bytes)).Replace("-", "");
            return sha1Hash;
        }
    }
}
