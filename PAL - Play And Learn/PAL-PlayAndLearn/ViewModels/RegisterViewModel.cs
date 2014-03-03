using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;
using PAL_PlayAndLearn.Behavior;
using PAL_PlayAndLearn.LogInRegisterCommands;
using System.Windows;
using PAL_PlayAndLearn.Pages;

namespace PAL_PlayAndLearn.ViewModels
{
    public class RegisterViewModel : ViewModelBase
    {
        private ICommand registerCommand;

        private ICommand openLogIn;

        public string UserName { get; set; }

        public static string Password { get; set; }

        public static string PasswordReply { get; set; }

        public string EMail { get; set; }

        public bool IsChekboxChecked { get; set; }

        public ICommand Register
        {
            get
            {
                if (this.registerCommand == null)
                {
                    this.registerCommand = new RelayCommand(this.HandleRegisterCommand);
                }
                return this.registerCommand;
            }
        }

        public ICommand OpenLogIn
        {
            get
            {
                if (this.openLogIn == null)
                {
                    this.openLogIn = new RelayCommand(this.HandleOpenLogInCommand);
                }
                return this.openLogIn;
            }
        }

        private void HandleOpenLogInCommand(object parameter)
        {
            Switcher.Switch(Switcher.StartUpProgramSwitcher,new LogInPage());
        }

        private void HandleRegisterCommand(object parameter)
        {
            var commandParameterValues = (object[])parameter;
            var firstPass = commandParameterValues[0] as PasswordBox;
            var firstPassValue = firstPass.Password;
            var secondPass = commandParameterValues[1] as PasswordBox;
            var secondPassValue = secondPass.Password;
            var UEmail = commandParameterValues[2] as TextBox;
            var UserEMailValue = UEmail.Text;
            if (UserName == "" || UserEMailValue == "" || firstPassValue == "" || secondPassValue == "")
            {
                WPFMessageBox.MessageBox.ShowError("Не сте въвели всичките задължителни полета.");
                return;
            }
            if (Validation.GetHasError(secondPass) || Validation.GetHasError(UEmail) || (firstPassValue != secondPassValue) || firstPassValue.Length < 6 || (UserName.Length < 6 || UserName.Length > 30))
            {
                string msgError = "";
                if (UserName.Length < 6 || UserName.Length > 30)
                {
                    msgError += "Потребилеското име трябва да е в границите от 6 до 30 знака.";
                }
                if (firstPassValue.Length < 6)
                {
                    msgError += "Паролата трябва е повече от 6 символа.";
                }
                if (Validation.GetHasError(secondPass))
                {
                    var errors = Validation.GetErrors(secondPass);
                    msgError += (string)errors[0].ErrorContent;
                }
                else if (firstPassValue != secondPassValue)
                {
                    msgError += "Двете пароли не си съвпадат.";
                }
                if (Validation.GetHasError(UEmail))
                {
                    var errors = Validation.GetErrors(UEmail);
                    msgError += (string)errors[0].ErrorContent;
                }
                WPFMessageBox.MessageBox.ShowError(msgError);
                return;
            }
            PALEntities db = new PALEntities();
            var isUsed = db.UserRegistrations.Where(u => u.UserEMail == UserEMailValue).Select(u => u.ID).SingleOrDefault();
            if (isUsed == 0)
            {
                LogInRegisterCommand.RegisterAndLogInWithValues(db, UserName, firstPassValue, UserEMailValue);
            }
            else
            {
                WPFMessageBox.MessageBox.ShowError("Вече съществува потребител с същите данни.");
            }            
        }
    }
}
