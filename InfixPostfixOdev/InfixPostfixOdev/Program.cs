using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InfixPostfixOdev
{
    class Program
    {
        public static string InfixdenPostFixe(string inFix)
        {
            // Çıktı
            string postFix = String.Empty;
            // İşlemlerin Olduğu yığın yapısı
            Stack<char> oprerator = new Stack<char>();
            // Girilen ifadenin uzunluğu kadar dönen bir döngü
            for (int i = 0; i < inFix.Length; i++)
            {
                char karakter = inFix[i];
                /* 
                 * Eğer girilen karakter bir sayıysa 
                 * Çıkış ifadesinin sonuna ekle
                 */
                if (char.IsDigit(karakter))
                {
                    // Birden fazla basamaklı olan sayılar için tekrar dön
                    while (i < inFix.Length && char.IsDigit(inFix[i]))
                    {
                        postFix += inFix[i];
                        i++;
                    }
                    i--;
                    continue;
                }
                // Eğer karakter sol parantez ise işlemlerin olduğu yığına ekle
                else if (karakter == '(')
                {
                    oprerator.Push(karakter);
                }
                /*
                 * Eğer girilen karakter bir işlemse sol parantez ifadesini görene kadar
                 * veya daha öncelikli bir işlem görene kadar çıkış ifadesinin sonuna ekle
                 * en sonda da ifadeyi işlem yığının sonuna ekle 
                 */
                else if (karakter == '*' || karakter == '+' || karakter == '-'
                          || karakter == '/' || karakter == '^')
                {
                    while (oprerator.Count != 0 && oprerator.Peek() != '(')
                    {
                        if (OncelikliMi(oprerator.Peek(), karakter))
                            postFix += oprerator.Pop();
                        else
                            break;
                    }
                    oprerator.Push(karakter);
                }
                /*
                 * Eğer girilen karakter sağ parantez ise sol parantez görene kadar
                 * işlemleri çıkış ifadesine yaz
                 */
                else if (karakter == ')')
                {
                    while (oprerator.Count != 0 && oprerator.Peek() != '(')
                    {
                        postFix += oprerator.Pop();
                    }
                    if (oprerator.Count != 0)
                    {
                        oprerator.Pop();
                    }

                }
            }
            // Kalan işlemleri de çıkış ifadesnin sonuna ekle
            while (oprerator.Count != 0)
            {
                postFix += oprerator.Pop();
            }
            return postFix;
        }
        
        private static bool OncelikliMi(char ilkIslem, char ikinciIslem)
        {
            // Girdi de var olabilecek işlemleri
            string oncelikSirasi = "(+-*/^";
            // Yukarıdeki oncelikSirasi değişkenine göre işlemlerin gerçek öncelikleri
            int[] oncelikler = { 0, 1, 1, 2, 2, 3 };

            // Girilen işlemlerin oncelikSirasi değişkeninde hangi indisde olduğunu bul
            int ilkIslemOnceligi = oncelikSirasi.IndexOf(ilkIslem);
            int ikinciIslemOnceligi = oncelikSirasi.IndexOf(ikinciIslem);

            // oncelikler dizisindeki öncelikler ile karşılaştırıp büyüklüğünü kontrol et
            return (oncelikler[ilkIslemOnceligi] >= oncelikler[ikinciIslemOnceligi]);
        }
    }
}
