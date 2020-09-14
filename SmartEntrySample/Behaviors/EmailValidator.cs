using System;
using System.Text.RegularExpressions;
using SmartEntrySample.CustomControls;
using Xamarin.Forms;

namespace SmartEntrySample.Behaviors
{
    public class EmailValidator : SmartBehavior
    {
        const string emailRegex = @"^(?("")("".+?(?<!\\)""@)|(([0-9a-z]((\.(?!\.))|[-!#\$%&'\*\+/=\?\^`\{\}\|~\w])*)(?<=[0-9a-z])@))" +
            @"(?(\[)(\[(\d{1,3}\.){3}\d{1,3}\])|(([0-9a-z][-\w]*[0-9a-z]*\.)+[a-z0-9][\-a-z0-9]{0,22}[a-z0-9]))$";

        public EmailValidator(string ErrorText) : base(ErrorText) { 
        }

        public override bool IsTextValid(string text)
        {
            return Regex.IsMatch(text, emailRegex, RegexOptions.IgnoreCase, TimeSpan.FromMilliseconds(250));
        }
      
    }
}
