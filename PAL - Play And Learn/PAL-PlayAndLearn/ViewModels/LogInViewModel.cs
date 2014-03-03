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
    public class LogInViewModel: ViewModelBase
    {
        private ICommand loginCommand;
        private ICommand openRegFormCommand;

        public string UserEMail { get; set; }
 
        public ICommand LogIn
        {
            get
            {
                if (this.loginCommand == null)
                {
                    this.loginCommand = new RelayCommand(this.HandleLoginCommand);
                }
                return this.loginCommand;
            }
        }

        public ICommand OpenRegisterForm
        {
            get
            {
                if (this.openRegFormCommand == null)
                {
                    this.openRegFormCommand = new RelayCommand(this.HandleOpenRegFormCommand);
                }
                return this.openRegFormCommand;
            }
        }

        public void HandleOpenRegFormCommand(object parameter)
        {
            Switcher.Switch(Switcher.StartUpProgramSwitcher,new RegisterPage());
        }

        private void HandleLoginCommand(object parameter)
        {
            var passwordBox = parameter as PasswordBox;
            var password = passwordBox.Password;
            if (UserEMail == "" || password == "")
            {
                WPFMessageBox.MessageBox.ShowError("Не сте въвели всичките задължителни полета.");
                return;
            }
            if ((UserEMail.Length < 6 || UserEMail.Length > 30) || password.Length < 6)
            {
                string msgError = "";
                if ((UserEMail.Length < 6 || UserEMail.Length > 30))
                {
                    msgError += "Потребилеското име трябва да е в границите от 6 до 30 знака.";
                }
                else
                {
                    msgError += "Паролата трябва е повече от 6 символа.";
                }
                WPFMessageBox.MessageBox.ShowError(msgError, "Error");
                return;
            }
            LogInRegisterCommand.LogInWithValues(UserEMail, password);
        }
    }
}
