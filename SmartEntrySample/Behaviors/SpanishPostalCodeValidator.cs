using System;
using System.Text.RegularExpressions;
using WorkBench.UI.CustomControls;
using Xamarin.Forms;

namespace WorkBench.UI.Behaviors
{
    public class SpanishPostalCodeValidator : Behavior<CompleteEntry>
    {
        Regex reg = new Regex("0[1-9]|([1-4][0-9])|5[0-2]");
        bool wasRequired = false;

        protected override void OnAttachedTo(CompleteEntry bindable)
        {
            bindable.AwesomeEntry.Unfocused += Bindable_SpanishPostalCodeUnfocused;
            bindable.AwesomeEntry.TextChanged += Bindable_TextChange;
            base.OnAttachedTo(bindable);
        }

        protected override void OnDetachingFrom(CompleteEntry bindable)
        {
            bindable.AwesomeEntry.TextChanged -= Bindable_SpanishPostalCodeUnfocused;
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

        void Bindable_SpanishPostalCodeUnfocused(object sender, EventArgs e)
        {


            var entry = (AwesomeEntry)sender;
            var complete = (CompleteEntry)entry.Parent.Parent;
            complete.Error.Text = "El código postal no es válido";

            if (!string.IsNullOrEmpty(entry.Text))
            {
                if (!(entry.Text.Length == 5 && reg.IsMatch(entry.Text.Substring(0, 2))))
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
