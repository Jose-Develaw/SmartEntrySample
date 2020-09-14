using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace SmartEntrySample.Behaviors
{
    public class SpanishNIFValidator : SmartBehavior
    {
        public SpanishNIFValidator(string ErrorText) : base(ErrorText)
        {
            
        }

        public override bool IsTextValid(string text) 
        {
                if (String.IsNullOrEmpty(text) || text.Length < 8)
                    return false;

                var initialLetter = text.Substring(0, 1).ToUpper();
                if (Char.IsLetter(text, 0))
                {
                    switch (initialLetter)
                    {
                        case "X":
                        text = "0" + text.Substring(1, text.Length - 1);
                            return validarNIF(text);
                        case "Y":
                        text = "1" + text.Substring(1, text.Length - 1);
                            return validarNIF(text);
                        case "Z":
                        text = "2" + text.Substring(1, text.Length - 1);
                            return validarNIF(text);
                        default:
                            return false;
                            break;
                    }
                }
                else if (Char.IsLetter(text, text.Length - 1))
                {
                    if (new Regex("[0-9]{8}[A-Za-z]").Match(text).Success || new Regex("[0-9]{7}[A-Za-z]").Match(text).Success)
                        return validarNIF(text);
                }
                return false;
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
    }
}
