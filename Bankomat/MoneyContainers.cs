using Windows.UI;
using Windows.UI.Xaml.Media;

namespace Bankomat
{
    class MoneyContainers
    {
        public static int dziesiec = 10000;
        public static int dwadziescia = 5000;
        public static int piecdziesiat = 2000;
        public static int sto = 1000;
        public static int dwiescie = 500;
        public static string status = "Gotowy";
        public static SolidColorBrush kolorStatusu = new SolidColorBrush(Colors.DarkGreen);
        public static int suma = 500000;
        public static void uzupełnijPojemniki()
        {
            dziesiec = 10000;
            dwadziescia = 5000;
            piecdziesiat = 2000;
            sto = 1000;
            dwiescie = 500;
            status = "Gotowy";
            kolorStatusu = new SolidColorBrush(Colors.DarkGreen);
        }
        public static void opróżnijPojemniki()
        {
            dziesiec = 0;
            dwadziescia = 0;
            piecdziesiat = 0;
            sto = 0;
            dwiescie = 0;
            status = "Pusty";
            kolorStatusu = new SolidColorBrush(Colors.Red);
        }
    }
}
