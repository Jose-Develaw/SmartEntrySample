using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using SmartEntrySample.Behaviors;
using Xamarin.Forms;

namespace SmartEntrySample.CustomControls
{
    public partial class CompleteEntry : ContentView
    {
        int _placeholderFontSize = 18;
        int _titleFontSize = 14;
        int _topMargin = -24;
   

        public event EventHandler Completed;
        protected Color _aux;

        public static BindableProperty BehaveAsProperty = BindableProperty.Create(
            propertyName: nameof(BehaveAs),
            returnType: typeof(string),
            declaringType: typeof(CompleteEntry),
            defaultValue: string.Empty,
            defaultBindingMode: BindingMode.OneWay,
            propertyChanged: BehaveAsPropertyChanged);

       
        public static BindableProperty BackgroundEntryColorProperty = BindableProperty.Create(nameof(BackgroundEntryColor), typeof(Color), typeof(CompleteEntry), Color.Transparent);
        public static BindableProperty TextColorProperty = BindableProperty.Create(nameof(TextColor), typeof(Color), typeof(CompleteEntry), Color.Black);
        public static readonly BindableProperty IsRequiredProperty = BindableProperty.Create(nameof(IsRequired), typeof(bool), typeof(CompleteEntry), default(bool));
        public static readonly BindableProperty HasErrorProperty = BindableProperty.Create(nameof(HasError), typeof(bool), typeof(CompleteEntry), default(bool));
        public static readonly BindableProperty RequiredTextProperty = BindableProperty.Create(nameof(RequiredText), typeof(string), typeof(string), "*Required field", BindingMode.TwoWay, null);
        public static BindableProperty CornerRadiusProperty = BindableProperty.Create(nameof(CornerRadius), typeof(int), typeof(CompleteEntry), 5);
        public static BindableProperty BorderThicknessProperty = BindableProperty.Create(nameof(BorderThickness), typeof(int), typeof(CompleteEntry), 1);
        public static BindableProperty PaddingProperty = BindableProperty.Create(nameof(Padding), typeof(Thickness), typeof(CompleteEntry), new Thickness(5));
        public static BindableProperty BorderColorProperty = BindableProperty.Create(nameof(BorderColor), typeof(Color), typeof(CompleteEntry), Color.Gray);
        public static BindableProperty FocusedBorderColorProperty = BindableProperty.Create(nameof(FocusedBorderColor), typeof(Color), typeof(CompleteEntry), Color.Gray);
        public static readonly BindableProperty TextProperty = BindableProperty.Create("Text", typeof(string), typeof(string), string.Empty, BindingMode.TwoWay, null, HandleBindingPropertyChangedDelegate);
        public static BindableProperty PlaceholderColorProperty = BindableProperty.Create(nameof(PlaceholderColor), typeof(Color), typeof(CompleteEntry), Color.Gray);
        public static readonly BindableProperty TitleProperty = BindableProperty.Create("Title", typeof(string), typeof(string), string.Empty, BindingMode.TwoWay, null);
        public static readonly BindableProperty ReturnTypeProperty = BindableProperty.Create(nameof(ReturnType), typeof(ReturnType), typeof(CompleteEntry), ReturnType.Next);
        public static readonly BindableProperty IsPasswordProperty = BindableProperty.Create("IsPassword", typeof(bool), typeof(CompleteEntry), default(bool));
        public static readonly BindableProperty KeyboardProperty = BindableProperty.Create("Keyboard", typeof(Keyboard), typeof(CompleteEntry), Keyboard.Default, coerceValue: (o, v) => (Keyboard)v ?? Keyboard.Default);


        public CompleteEntry()
        {
            InitializeComponent();
            AwesomeEntry = EntryField;
            Error = ErrorLabel;
            Required = RequiredLabel;
            LabelTitle.TranslationX = 10;
            LabelTitle.FontSize = _placeholderFontSize;
            

            // var parentColor= (Color) Parent.GetValue(BackgroundColorProperty);
        }

        static async void HandleBindingPropertyChangedDelegate(BindableObject bindable, object oldValue, object newValue)
        {
            var control = bindable as CompleteEntry;
            if (!control.EntryField.IsFocused)
            {
                if (!string.IsNullOrEmpty((string)newValue))
                {
                    await control.TransitionToTitle(false);
                }
                else
                {
                    await control.TransitionToPlaceholder(false);
                }
            }
        }

        public AwesomeEntry AwesomeEntry;
        public Label Error;
        public Label Required;


        public enum BehaveAsEnum
        {
            Default,
            Email,
            ES_NIF,
            ES_CIF,
            ES_PosCode,
      
        }

        public BehaveAsEnum BehaveAs
        {
            get => (BehaveAsEnum)GetValue(BehaveAsProperty);
            set => SetValue(BehaveAsProperty, value);
        }

        private static void BehaveAsPropertyChanged(BindableObject bindable, object oldValue, object newValue)
        {

            CompleteEntry targetView;
            targetView = (CompleteEntry)bindable;
            targetView.Behaviors.Clear();
            if (targetView != null)
            {
                var behavior = (string)newValue;

                switch (behavior)
                {
                    case "Email":
                        targetView.Behaviors.Add(new EmailValidator("Email no válido"));
                        targetView.Keyboard = Keyboard.Email;
                        break;
                    case "ES_NIF":
                        targetView.Behaviors.Add(new SpanishNIFValidator("NIF no válido"));
                        targetView.Keyboard = Keyboard.Default;
                        break;
                    case "ES_CIF":
                        targetView.Behaviors.Add(new SpanishCIFValidator("CIF no válido"));
                        targetView.Keyboard = Keyboard.Default;
                        break;
                    case "ES_PosCode":
                        targetView.Behaviors.Add(new SpanishPostalCodeValidator("Código Postal no válido"));
                        targetView.Keyboard = Keyboard.Numeric;
                        break;
                }
            }

        }

        public string Text
        {
            get => (string)GetValue(TextProperty);
            set => SetValue(TextProperty, value);
        }

        public Color TextColor
        {
            get => (Color)GetValue(TextColorProperty);
            set => SetValue(TextColorProperty, value);
        }

        public bool IsRequired
        {
            get { return (bool)GetValue(IsRequiredProperty); }
            set { SetValue(IsRequiredProperty, value); }
        }

        public string RequiredText
        {
            get => (string)GetValue(RequiredTextProperty);
            set => SetValue(RequiredTextProperty, value);
        }

        public bool HasError
        {
            get { return (bool)GetValue(HasErrorProperty); }
            set { SetValue(HasErrorProperty, value); }
        }

        public Color BackgroundEntryColor
        {
            get => (Color)GetValue(BackgroundEntryColorProperty);
            set => SetValue(BackgroundEntryColorProperty, value);
        }

        public string Title
        {
            get => (string)GetValue(TitleProperty);
            set => SetValue(TitleProperty, value);
        }

        public ReturnType ReturnType
        {
            get => (ReturnType)GetValue(ReturnTypeProperty);
            set => SetValue(ReturnTypeProperty, value);
        }

        public bool IsPassword
        {
            get { return (bool)GetValue(IsPasswordProperty); }
            set { SetValue(IsPasswordProperty, value); }
        }

        public Keyboard Keyboard
        {
            get { return (Keyboard)GetValue(KeyboardProperty); }
            set { SetValue(KeyboardProperty, value); }
        }

        public int CornerRadius
        {
            get => (int)GetValue(CornerRadiusProperty);
            set => SetValue(CornerRadiusProperty, value);
        }

        public int BorderThickness
        {
            get => (int)GetValue(BorderThicknessProperty);
            set => SetValue(BorderThicknessProperty, value);
        }
        public Color BorderColor
        {
            get => (Color)GetValue(BorderColorProperty);
            set => SetValue(BorderColorProperty, value);
        }

        public Color FocusedBorderColor
        {
            get => (Color)GetValue(FocusedBorderColorProperty);
            set => SetValue(FocusedBorderColorProperty, value);
        }

        public Color PlaceholderColor
        {
            get => (Color)GetValue(PlaceholderColorProperty);
            set => SetValue(PlaceholderColorProperty, value);
        }


        /// <summary>
        /// This property cannot be changed at runtime in iOS.
        /// </summary>
        public Thickness Padding
        {
            get => (Thickness)GetValue(PaddingProperty);
            set => SetValue(PaddingProperty, value);
        }

        public new void Focus()
        {
            if (IsEnabled)
            {
                EntryField.Focus();
            }
        }

        private Element findBackgroundColor(Element element) {

            if (element.Parent != null)
            {

                if (Object.ReferenceEquals(element.Parent.GetType(), typeof(Page))) {

                    element = null;

                }
                else
                {
                    var color = (Color)element.Parent.GetValue(BackgroundColorProperty);
                    if (color.Equals(Color.Default))
                    {
                        element = findBackgroundColor(element.Parent);
                    }
                    else
                    {
                        element = element.Parent;
                    }
                }
            }
            else
            {
                element = null;
            }

            return element;
        }

        async void Handle_Focused(object sender, FocusEventArgs e)
        {

            if (string.IsNullOrEmpty(Text))
            {
                await TransitionToTitle(true);

                Element backgroundElement = findBackgroundColor(this);

                if (backgroundElement == null)
                {
                    if (Device.RuntimePlatform == Device.Android)
                    {
                        LabelTitle.BackgroundColor = App.Current.RequestedTheme == OSAppTheme.Light ? Color.FromHex("#FAFAFA") : Color.FromHex("#303030");
                    }
                    else
                    {
                        LabelTitle.BackgroundColor = App.Current.RequestedTheme == OSAppTheme.Light ? Color.White : Color.Black;
                    }
                    
                }
                else
                {
                    LabelTitle.BackgroundColor = (Color)backgroundElement.GetValue(BackgroundColorProperty);
                }

                LabelTitle.Padding = new Thickness(2, 0, 2, 0);

            }

            _aux = BorderColor;
            BorderColor = FocusedBorderColor; 
            LabelTitle.TextColor = BorderColor; 
        }

        async void Handle_Unfocused(object sender, FocusEventArgs e)
        {
            if (string.IsNullOrEmpty(Text))
            {
                await TransitionToPlaceholder(true);
                LabelTitle.BackgroundColor = Color.Transparent;
                LabelTitle.TextColor = PlaceholderColor;
                LabelTitle.Padding = new Thickness(0, 0, 0, 0);
                if (_aux != null)
                {
                    BorderColor = _aux;
                }
            }
            else
            {
                if (_aux != null)
                {
                    BorderColor = _aux;
                    LabelTitle.TextColor = BorderColor;
                }
            }

            
        }

        async Task TransitionToTitle(bool animated)
        {
            if (animated)
            {
                var t1 = LabelTitle.TranslateTo(10, _topMargin, 100);
                var t2 = SizeTo(_titleFontSize);
                await Task.WhenAll(t1, t2);
            }
            else
            {
                LabelTitle.TranslationX = 10;
                LabelTitle.TranslationY = -30;
                LabelTitle.FontSize = 14;
            }
        }

        async Task TransitionToPlaceholder(bool animated)
        {
            if (animated)
            {
                var t1 = LabelTitle.TranslateTo(10, 0, 100);
                var t2 = SizeTo(_placeholderFontSize);
                await Task.WhenAll(t1, t2);
            }
            else
            {
                LabelTitle.TranslationX = 10;
                LabelTitle.TranslationY = 0;
                LabelTitle.FontSize = _placeholderFontSize;
            }
        }

        void Handle_Tapped(object sender, EventArgs e)
        {
            if (IsEnabled)
            {
                EntryField.Focus();
            }
        }

        Task SizeTo(int fontSize)
        {
            var taskCompletionSource = new TaskCompletionSource<bool>();

            // setup information for animation
            Action<double> callback = input => { LabelTitle.FontSize = input; };
            double startingHeight = LabelTitle.FontSize;
            double endingHeight = fontSize;
            uint rate = 5;
            uint length = 100;
            Easing easing = Easing.Linear;

            // now start animation with all the setup information
            LabelTitle.Animate("invis", callback, startingHeight, endingHeight, rate, length, easing, (v, c) => taskCompletionSource.SetResult(c));

            return taskCompletionSource.Task;
        }

        void Handle_Completed(object sender, EventArgs e)
        {
            Completed?.Invoke(this, e);
        }

        protected override void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            base.OnPropertyChanged(propertyName);

            if (propertyName == nameof(IsEnabled))
            {
                EntryField.IsEnabled = IsEnabled;
            }
        }
    }
}
