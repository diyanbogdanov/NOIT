using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Controls;
using System.Globalization;
using System.Net.Mail;

namespace PAL_PlayAndLearn.ValidationRules
{
    class EMail : ValidationRule
    {
        public override ValidationResult Validate(object value, System.Globalization.CultureInfo cultureInfo)
        {
            string eMail = (string)value;
            if (IsValid(eMail))
            {
                return new ValidationResult(true, null);
            }
            else
            {
                return new ValidationResult(false, "Този Имейл не е правилен.");
            }
        }

        private bool IsValid(string eMail)
        {
            try
            {
                MailAddress m = new MailAddress(eMail);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
