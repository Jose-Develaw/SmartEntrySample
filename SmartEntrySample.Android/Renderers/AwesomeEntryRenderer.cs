using System;
using System.ComponentModel;
using Android.Content;
using Android.Graphics.Drawables;
using SmartEntrySample.Droid.Renderers;
using SmartEntrySample.CustomControls;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(AwesomeEntry), typeof(AwesomeEntryRenderer))]

namespace SmartEntrySample.Droid.Renderers
{
    public class AwesomeEntryRenderer : EntryRenderer
    {
        public AwesomeEntryRenderer(Context context): base (context)
        {
        }

        //No podemos sobreescribir las propierdades de Element, así que lo duplicamos para poder acceder a nuestras BindableProperties
        public AwesomeEntry ElementV2 => Element as AwesomeEntry;

        protected override FormsEditText CreateNativeControl()
        {
            //Seguimos creando un EditText (con base) sólo que en el momento de crearlo ya actualizamos su background
            var control = base.CreateNativeControl();
            UpdateBackground(control);
            return control;
        }

        protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == AwesomeEntry.CornerRadiusProperty.PropertyName)
            {
                UpdateBackground();
            }
            else if (e.PropertyName == AwesomeEntry.BorderThicknessProperty.PropertyName)
            {
                UpdateBackground();
            }
            else if (e.PropertyName == AwesomeEntry.BorderColorProperty.PropertyName)
            {
                UpdateBackground();
            } 

            //Llamamos a la clase base para no perder la funcionalidad de actualización del resto de properties
            base.OnElementPropertyChanged(sender, e);
        }

        protected override void UpdateBackgroundColor()
        {
            UpdateBackground();
        }

        protected void UpdateBackground(FormsEditText control)
        {
            if (control == null)
                return;

            var gd = new GradientDrawable();

            //ToAndroid aplica sobre la clase Color de Xamarin.Forms y lo transforma en la clase Color que necesita Android.
            gd.SetCornerRadius(Context.ToPixels(ElementV2.CornerRadius));
            gd.SetColor(Element.BackgroundColor.ToAndroid());
            gd.SetStroke((int)Context.ToPixels(ElementV2.BorderThickness), ElementV2.BorderColor.ToAndroid());
            gd.SetGradientRadius(Context.ToPixels(ElementV2.CornerRadius));
            control.SetBackground(gd);

            //ToPixels convierte la unidad que utilizamos en Xamarin.Forms que es independiente de la densidad de la pantalla a pixeles que son lo que usamos en Android
            var padTop = (int)Context.ToPixels(ElementV2.Padding.Top);
            var padBottom = (int)Context.ToPixels(ElementV2.Padding.Bottom);
            var padLeft = (int)Context.ToPixels(ElementV2.Padding.Left);
            var padRight = (int)Context.ToPixels(ElementV2.Padding.Right);

            control.SetPadding(padLeft, padTop, padRight, padBottom);
        }
        protected void UpdateBackground()
        {
            UpdateBackground(Control);
        }
    }
}
