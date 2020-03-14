using System;
using System.Collections.Generic;
using System.Linq;

namespace NLP
{
    public class NLPConverter
    {
        private Dictionary<string, long> units = new Dictionary<string, long>();
        private string[] extra = new string[] { "de", "da" };

        public NLPConverter()
        {
            this.Fill();
        }

        private void Fill()
        {
            units.Add("bir", 1);
            units.Add("iki", 2);
            units.Add("üç", 3);
            units.Add("dört", 4);
            units.Add("beş", 5);
            units.Add("altı", 6);
            units.Add("yedi", 7);
            units.Add("sekiz", 8);
            units.Add("dokuz", 9);
            units.Add("on", 10);
            units.Add("on bir", 11);
            units.Add("on iki", 12);
            units.Add("on üç", 13);
            units.Add("on dört", 14);
            units.Add("on beş", 15);
            units.Add("on altı", 16);
            units.Add("on yedi", 17);
            units.Add("on sekiz", 18);
            units.Add("on dokuz", 19);
            units.Add("yirmi", 20);
            units.Add("otuz", 30);
            units.Add("kırk", 40);
            units.Add("elli", 50);
            units.Add("altmış", 60);
            units.Add("yetmiş", 70);
            units.Add("seksen", 80);
            units.Add("doksan", 90);
            units.Add("yüz", 100);
            units.Add("bin", 1000);
            units.Add("milyon", 100000);
            units.Add("milyar", 1000000000);
            units.Add("trilyon", 1000000000000);
        }

        /// <summary>
        /// -de/-da/-te için kullanılır.
        /// </summary>
        /// <param name="word">Kelime</param>
        /// <returns></returns>
        private string To(string word)
        {
            long tmp = 0;
            string result = string.Empty;
            
            if (word.Length > 2 && Array.Find(extra, word.ToLower().Contains) != null)
            {
                string newWord = string.Empty,
                    _extra = string.Empty;

                for (int i = 0; i < extra.Length; i++)
                {
                    if (word.Contains(extra[i]))
                    {
                        newWord = word;
                        newWord = newWord.Replace(extra[i], string.Empty);
                        _extra = extra[i];
                        break;
                    }
                }

                string key = units
                    .Where(c => c.Key.Replace(" ", string.Empty).Contains(newWord))
                    .Select(c => c.Key)
                    .FirstOrDefault();

                if (!string.IsNullOrEmpty(key))
                    if (units.TryGetValue(key, out tmp))
                        result += " " + AddNumbers(tmp.ToString()) + " " + _extra;
            }

            return result;
        }

        /// <summary>
        /// Verilen cümle içerisinde sayı arar.
        /// </summary>
        /// <param name="input">Çevrilmesi gereken cümle</param>
        /// <returns></returns>
        public string FindNumbers(string input)
        {
            string result = input;
            if (!string.IsNullOrEmpty(result))
            {
                long tmp = 0;
                string tmpout = string.Empty,
                    output = string.Empty;

                foreach (string word in result.Split(' '))
                {
                    string to = To(word);
                    if (!string.IsNullOrEmpty(to))
                    {
                        output += to;
                    }
                    else
                    {
                        if (units.TryGetValue(word.ToLower(), out tmp))
                        {
                            if (!string.IsNullOrEmpty(tmpout)) tmpout += " ";
                            tmpout += tmp;
                        }
                        else
                        {
                            if (!string.IsNullOrEmpty(tmpout)) output += " " + AddNumbers(tmpout);
                            tmpout = "";
                            if (!string.IsNullOrEmpty(output)) output += " ";
                            output += word;
                        }
                    }
                }

                if (!string.IsNullOrEmpty(tmpout))
                {
                    tmpout = AddNumbers(tmpout);
                    if (!string.IsNullOrEmpty(output)) output += " ";
                    output += tmpout;
                }
                result = output.Trim();
            }
            return result;
        }

        /// <summary>
        /// Verilen değerleri hesaplayıp çarpma veya toplama işlemi yapar.
        /// </summary>
        /// <param name="input">Sayılar</param>
        /// <returns></returns>
        private string AddNumbers(string input)
        {
            long multiplication = 0, addition = 0;
            foreach (string num in input.Split(' '))
            {
                if (multiplication > 999)
                {
                    addition = multiplication;
                    multiplication = 0;
                }
                if (long.Parse(num) > 99)
                {
                    if (multiplication > 0)
                        multiplication *= long.Parse(num);
                    else
                        multiplication = long.Parse(num);
                }
                else
                {
                    multiplication += long.Parse(num);
                }
            }
            return (multiplication + addition).ToString();
        }
    }
}