using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Controls;
using PAL_PlayAndLearn.ViewModels;

namespace PAL_PlayAndLearn.ValidationRules
{
    class PasswordValidation : ValidationRule
    {
        public override ValidationResult Validate(object value, System.Globalization.CultureInfo cultureInfo)
        {
            string pass = (string)value;
            if (AreEqual(pass))
            {
                return new ValidationResult(true, null);
            }
            return new ValidationResult(false, "Двете пароли не си съвпадат.");
        }

        private bool AreEqual(string pass)
        {
            if (RegisterViewModel.Password != pass)
            {
                return false;
            }
            return true;
        }
    }
}
