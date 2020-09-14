using System;
using System.Text.RegularExpressions;
using SmartEntrySample.CustomControls;
using Xamarin.Forms;

namespace SmartEntrySample.Behaviors
{
    public class SpanishPostalCodeValidator : SmartBehavior
    {
        Regex reg = new Regex("0[1-9]|([1-4][0-9])|5[0-2]");
        public SpanishPostalCodeValidator(string ErrorText) : base(ErrorText) {}
        public override bool IsTextValid(string text)
        {
            return text.Length == 5 && reg.IsMatch(text.Substring(0, 2));
        }
    }
}
