using System;
using System.Text.RegularExpressions;
using SmartEntrySample.CustomControls;
using Xamarin.Forms;

namespace SmartEntrySample.Behaviors
{
    public class CIFValidator : Behavior<CompleteEntry>
    {
        bool wasRequired = false;

        protected override void OnAttachedTo(CompleteEntry bindable)
        {
            bindable.AwesomeEntry.Unfocused += Bindable_CifUnfocused;
            bindable.AwesomeEntry.TextChanged += Bindable_TextChange;
            base.OnAttachedTo(bindable);
        }

        protected override void OnDetachingFrom(CompleteEntry bindable)
        {
            bindable.AwesomeEntry.TextChanged -= Bindable_CifUnfocused;
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

        void Bindable_CifUnfocused(object sender, EventArgs e)
        {

            var entry = (AwesomeEntry)sender;
            var complete = (CompleteEntry)entry.Parent.Parent;
            complete.Error.Text = "El CIF no es válido";

            if (!string.IsNullOrEmpty(entry.Text))
            {
                if (!valida_CIF(entry.Text))
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



        public static Boolean valida_CIF(string data)
        {
            if (String.IsNullOrEmpty(data) || data.Length < 8)
                return false;

            var initialLetter = data.Substring(0, 1).ToUpper();
            if (Char.IsLetter(data, 0))
            {

                if (new Regex("[A-Za-z][0-9]{7}[A-Za-z0-9]{1}$").Match(data).Success)
                    return validaCIF(data);

                else
                    return false;
            }
            else
            {
                return false;

            }
            
        }

        public static bool validaCIF(string data)
        {
            try
            {
                int pares = 0;
                int impares = 0;
                int suma;
                string ultima;
                int unumero;
                string[] uletra = new string[] { "A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M", "N", "0" };
                string[] fletra = new string[] { "A", "B", "C", "D", "E", "F", "G", "H", "I", "J" };
                int[] fletra1 = new int[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 0 };
                string xxx;

                /*
                * T      P      P      N  N  N  N  N  C
                Siendo:
                T: Letra de tipo de Organización, una de las siguientes: A,B,C,D,E,F,G,H,K,L,M,N,P,Q,S.
                P: Código provincial.
                N: Númeración secuenial dentro de la provincia.
                C: Dígito de control, un número ó letra: Aó1,Bó2,Có3,Dó4,Eó5,Fó6,Gó7,Hó8,Ió9,Jó0.
                *
                *
                A.    Sociedades anónimas.
                B.    Sociedades de responsabilidad limitada.
                C.    Sociedades colectivas.
                D.    Sociedades comanditarias.
                E.    Comunidades de bienes y herencias yacentes.
                F.    Sociedades cooperativas.
                G.    Asociaciones.
                H.    Comunidades de propietarios en régimen de propiedad horizontal.
                I.    Sociedades civiles, con o sin personalidad jurídica.
                J.    Corporaciones Locales.
                K.    Organismos públicos.
                L.    Congregaciones e instituciones religiosas.
                M.    Órganos de la Administración del Estado y de las Comunidades Autónomas.
                N.    Uniones Temporales de Empresas.
                O.    Otros tipos no definidos en el resto de claves.

                */
                data = data.ToUpper();

                ultima = data.Substring(8, 1);

                int cont = 1;
                for (cont = 1; cont < 7; cont++)
                {
                    xxx = (2 * int.Parse(data.Substring(cont++, 1))) + "0";
                    impares += int.Parse(xxx.ToString().Substring(0, 1)) + int.Parse(xxx.ToString().Substring(1, 1));
                    pares += int.Parse(data.Substring(cont, 1));
                }

                xxx = (2 * int.Parse(data.Substring(cont, 1))) + "0";
                impares += int.Parse(xxx.Substring(0, 1)) + int.Parse(xxx.Substring(1, 1));

                suma = pares + impares;
                unumero = int.Parse(suma.ToString().Substring(suma.ToString().Length - 1, 1));
                unumero = 10 - unumero;
                if (unumero == 10)
                    unumero = 0;

                if ((ultima == unumero.ToString()) || (ultima == uletra[unumero - 1]))
                    return true;
                else
                    return false;

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }
    }
}
