using System;
using SmartEntrySample.CustomControls;
using Xamarin.Forms;

namespace SmartEntrySample.Behaviors
{
    public abstract class SmartBehavior : Behavior<CompleteEntry>
    {
        private bool _wasRequired = false;
        private string _errorText;

        public abstract bool IsTextValid(string text);

        public SmartBehavior(string ErrorText) : base()
        {
            _errorText = ErrorText;
        }

        protected override void OnAttachedTo(CompleteEntry bindable)
        {
            bindable.AwesomeEntry.Unfocused += Bindable_Unfocused;
            bindable.AwesomeEntry.TextChanged += Bindable_TextChange;
            base.OnAttachedTo(bindable);
        }

        protected override void OnDetachingFrom(CompleteEntry bindable)
        {
            bindable.AwesomeEntry.TextChanged -= Bindable_Unfocused;
            bindable.AwesomeEntry.TextChanged -= Bindable_TextChange;
            base.OnDetachingFrom(bindable);
        }

        void Bindable_TextChange(object sender, EventArgs e)
        {
            var entry = (AwesomeEntry)sender;
            var complete = (CompleteEntry)entry.Parent.Parent;
            complete.Error.IsVisible = false;
            if (_wasRequired)
            {
                complete.Required.IsVisible = true;
            }
        }

        void Bindable_Unfocused(object sender, EventArgs e)
        {

            var entry = (AwesomeEntry)sender;
            var complete = (CompleteEntry)entry.Parent.Parent;
            complete.Error.Text = _errorText;

            if (!string.IsNullOrEmpty(entry.Text))
            {
                if (!IsTextValid(entry.Text))
                {
                    showErrorMessage(complete);
                }
                else
                {
                    hideErrorMessage(complete);

                }
            }
            else
            {
                hideErrorMessage(complete);
            }

        }

        void hideErrorMessage(CompleteEntry complete) {
            complete.Error.IsVisible = false;
            showRequired(complete);
        }

        void showErrorMessage(CompleteEntry complete) {
            complete.Error.IsVisible = true;
            setRequired(complete);
        }

        void showRequired(CompleteEntry complete) {
            if (_wasRequired)
            {
                complete.Required.IsVisible = true;
            }
        }

        void setRequired(CompleteEntry complete) {
            if (complete.Required.IsVisible)
            {
                _wasRequired = true;
                complete.Required.IsVisible = false;
            }
        }

        
    }
}
