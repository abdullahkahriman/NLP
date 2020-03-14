using System;

namespace NLP
{
    public class Program
    {
        static void Main(string[] args)
        {
            NLPConverter converter = new NLPConverter();
            //string res = converter.FindNumbers("Yüz bin lira kredi kullanmak istiyorum");
            //string res = converter.FindNumbers("Bugün yirmi sekiz yaşına girdim");
            string res = converter.FindNumbers("Elli altı bin lira kredi alıp üç yılda geri ödeyeceğim");
            //string res = converter.FindNumbers("Seksen yedi bin iki yüz on altı lira borcum var");
            //string res = converter.FindNumbers("Bin yirmi dört lira eksiğim kaldı");
            //string res = converter.FindNumbers("Yarın saat onaltıda geleceğim");

            Console.WriteLine(res);
            Console.Read();
        }
    }
}