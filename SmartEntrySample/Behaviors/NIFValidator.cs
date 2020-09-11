using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using SmartEntrySample.CustomControls;
using Xamarin.Forms;

namespace SmartEntrySample.Behaviors
{
    public class NIFValidator : Behavior<CompleteEntry>
    {
        bool wasRequired = false;

        protected override void OnAttachedTo(CompleteEntry bindable)
        {
            bindable.AwesomeEntry.Unfocused += Bindable_NifUnfocused;
            bindable.AwesomeEntry.TextChanged += Bindable_TextChange;
            base.OnAttachedTo(bindable);
        }

        protected override void OnDetachingFrom(CompleteEntry bindable)
        {
            bindable.AwesomeEntry.TextChanged -= Bindable_NifUnfocused;
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

        void Bindable_NifUnfocused(object sender, EventArgs e)
        {

            var entry = (AwesomeEntry)sender;
            var complete = (CompleteEntry)entry.Parent.Parent;
            complete.Error.Text = "El NIF no es válido";

            if (!string.IsNullOrEmpty(entry.Text))
            {
                if (!valida_NIFCIFNIE(entry.Text))
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

        private static string getLetra(int id)
        {
            Dictionary<int, String> letras = new Dictionary<int, string>();
            letras.Add(0, "T");
            letras.Add(1, "R");
            letras.Add(2, "W");
            letras.Add(3, "A");
            letras.Add(4, "G");
            letras.Add(5, "M");
            letras.Add(6, "Y");
            letras.Add(7, "F");
            letras.Add(8, "P");
            letras.Add(9, "D");
            letras.Add(10, "X");
            letras.Add(11, "B");
            letras.Add(12, "N");
            letras.Add(13, "J");
            letras.Add(14, "Z");
            letras.Add(15, "S");
            letras.Add(16, "Q");
            letras.Add(17, "V");
            letras.Add(18, "H");
            letras.Add(19, "L");
            letras.Add(20, "C");
            letras.Add(21, "K");
            letras.Add(22, "E");
            return letras[id];
        }

        public static bool validarNIF(string data)
        {
            if (data == String.Empty)
                return false;
            try
            {
                String letra;
                letra = data.Substring(data.Length - 1, 1);
                data = data.Substring(0, data.Length - 1);
                int nifNum = int.Parse(data);
                int resto = nifNum % 23;
                string tmp = getLetra(resto);
                if (tmp.ToLower() != letra.ToLower())
                    return false;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
            return true;
        }


        public static Boolean valida_NIFCIFNIE(string data)
        {
            if (String.IsNullOrEmpty(data) || data.Length < 8)
                return false;

            var initialLetter = data.Substring(0, 1).ToUpper();
            if (Char.IsLetter(data, 0))
            {
                switch (initialLetter)
                {
                    case "X":
                        data = "0" + data.Substring(1, data.Length - 1);
                        return validarNIF(data);
                    case "Y":
                        data = "1" + data.Substring(1, data.Length - 1);
                        return validarNIF(data);
                    case "Z":
                        data = "2" + data.Substring(1, data.Length - 1);
                        return validarNIF(data);
                    default:
                        return false;
                        break;
                }
            }
            else if (Char.IsLetter(data, data.Length - 1))
            {
                if (new Regex("[0-9]{8}[A-Za-z]").Match(data).Success || new Regex("[0-9]{7}[A-Za-z]").Match(data).Success)
                    return validarNIF(data);
            }
            return false;
        }
    }
}
