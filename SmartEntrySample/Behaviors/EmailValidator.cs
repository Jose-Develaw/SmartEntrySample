using System;
using System.Text.RegularExpressions;
using WorkBench.UI.CustomControls;
using Xamarin.Forms;

namespace WorkBench.UI.Behaviors
{
    public class EmailValidator : Behavior<CompleteEntry>
    {

        const string emailRegex = @"^(?("")("".+?(?<!\\)""@)|(([0-9a-z]((\.(?!\.))|[-!#\$%&'\*\+/=\?\^`\{\}\|~\w])*)(?<=[0-9a-z])@))" +
            @"(?(\[)(\[(\d{1,3}\.){3}\d{1,3}\])|(([0-9a-z][-\w]*[0-9a-z]*\.)+[a-z0-9][\-a-z0-9]{0,22}[a-z0-9]))$";
        bool wasRequired = false;

        protected override void OnAttachedTo(CompleteEntry bindable)
        {
            bindable.AwesomeEntry.Unfocused += Bindable_EmailUnfocused;
            bindable.AwesomeEntry.TextChanged += Bindable_TextChange;
            base.OnAttachedTo(bindable);
        }

        protected override void OnDetachingFrom(CompleteEntry bindable)
        {
            bindable.AwesomeEntry.TextChanged -= Bindable_EmailUnfocused;
            bindable.AwesomeEntry.TextChanged -= Bindable_TextChange;
            base.OnDetachingFrom(bindable);
        }


        void Bindable_TextChange(object sender, EventArgs e)
        {
            var entry = (AwesomeEntry)sender;
            var complete = (CompleteEntry)entry.Parent.Parent;
            complete.Error.IsVisible = false;
            if (wasRequired)
            {
                complete.Required.IsVisible = true;
            }
        }

        void Bindable_EmailUnfocused(object sender, EventArgs e) {

            
            var entry = (AwesomeEntry)sender;
            var complete = (CompleteEntry)entry.Parent.Parent;
            complete.Error.Text = "El email no es válido";

            if (!string.IsNullOrEmpty(entry.Text))
            {
                if (!Regex.IsMatch(entry.Text, emailRegex, RegexOptions.IgnoreCase, TimeSpan.FromMilliseconds(250)))
                {
                    complete.Error.IsVisible = true;
                    if (complete.Required.IsVisible)
                    {
                        wasRequired = true;
                        complete.Required.IsVisible = false;
                    }
                }
                else
                {
                    complete.Error.IsVisible = false;
                    if (wasRequired)
                    {
                        complete.Required.IsVisible = true;
                    }

                }
            }
            else
            {
                complete.Error.IsVisible = false;
                if (wasRequired)
                {
                    complete.Required.IsVisible = true;
                }
            }
            
        }
    }
}
