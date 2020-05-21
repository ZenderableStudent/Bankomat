using System.Linq;
using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;

namespace Bankomat
{
    public sealed partial class ServiceMode : Page
    {
        public ServiceMode()
        {
            this.InitializeComponent();
			sprawdźIlośćGotówki();
			ustawStatus();
		}
        private void UserMode_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(UserMode));
        }
        private void sprawdźIlośćGotówki()
        {
            tb10.Text = (MoneyContainers.dziesiec).ToString();
            tb20.Text = (MoneyContainers.dwadziescia).ToString();
            tb50.Text = (MoneyContainers.piecdziesiat).ToString();
            tb100.Text = (MoneyContainers.sto).ToString();
            tb200.Text = (MoneyContainers.dwiescie).ToString();
			tbSumaGotówki.Text = (MoneyContainers.dziesiec * 10 + MoneyContainers.dwadziescia * 20 + MoneyContainers.piecdziesiat * 50 + MoneyContainers.sto * 100 + MoneyContainers.dwiescie * 200).ToString();
			MoneyContainers.suma = int.Parse(tbSumaGotówki.Text);
        }
        private void Napelnienie_Click(object sender, RoutedEventArgs e)
        {
            MoneyContainers.uzupełnijPojemniki();
            sprawdźIlośćGotówki();
			ustawStatus();
        }
        private void Oproznienie_Click(object sender, RoutedEventArgs e)
        {
            MoneyContainers.opróżnijPojemniki();
            sprawdźIlośćGotówki();
			ustawStatus();
        }
		private void ustawStatus()
		{
            if (MoneyContainers.status == "Działający")
            {
                tbStatus.Text = "Gotowy";
                tbStatus.Foreground = new SolidColorBrush(Colors.DarkGreen);
            }
            else
            {
                tbStatus.Text = MoneyContainers.status;
                tbStatus.Foreground = MoneyContainers.kolorStatusu;
            }
        }
        private void NapelnienieManualne_Click(object sender, RoutedEventArgs e)
        {
            MoneyContainers.dziesiec = (tbManual10.Text != "") ? int.Parse(tbManual10.Text) : int.Parse(tbManual10.PlaceholderText);
            MoneyContainers.dwadziescia = tbManual20.Text != "" ? int.Parse(tbManual20.Text) : int.Parse(tbManual20.PlaceholderText);
            MoneyContainers.piecdziesiat = tbManual50.Text != "" ? int.Parse(tbManual50.Text) : int.Parse(tbManual50.PlaceholderText);
            MoneyContainers.sto = tbManual100.Text != "" ? int.Parse(tbManual100.Text) : int.Parse(tbManual100.PlaceholderText);
            MoneyContainers.dwiescie = tbManual200.Text != "" ? int.Parse(tbManual200.Text) : int.Parse(tbManual200.PlaceholderText);
            sprawdźIlośćGotówki();
            if (MoneyContainers.dziesiec == 0 || MoneyContainers.dwadziescia == 0 || MoneyContainers.piecdziesiat == 0 || MoneyContainers.sto == 0 || MoneyContainers.dwiescie == 0)
            {
                MoneyContainers.status = "Działający";
                MoneyContainers.kolorStatusu = new SolidColorBrush(Colors.Orange);
                ustawStatus();
            }
            if(MoneyContainers.suma == 0)
            {
                MoneyContainers.status = "Pusty";
                MoneyContainers.kolorStatusu = new SolidColorBrush(Colors.Red);
                ustawStatus();
            }
        }
        private void tbManual10_BeforeTextChanging(TextBox sender, TextBoxBeforeTextChangingEventArgs args)
        {
            args.Cancel = args.NewText.Any(c => !char.IsDigit(c));
        }
        private void tbManual20_BeforeTextChanging(TextBox sender, TextBoxBeforeTextChangingEventArgs args)
        {
            args.Cancel = args.NewText.Any(c => !char.IsDigit(c));
        }
        private void tbManual50_BeforeTextChanging(TextBox sender, TextBoxBeforeTextChangingEventArgs args)
        {
            args.Cancel = args.NewText.Any(c => !char.IsDigit(c));
        }
        private void tbManual100_BeforeTextChanging(TextBox sender, TextBoxBeforeTextChangingEventArgs args)
        {
            args.Cancel = args.NewText.Any(c => !char.IsDigit(c));
        }
        private void tbManual200_BeforeTextChanging(TextBox sender, TextBoxBeforeTextChangingEventArgs args)
        {
            args.Cancel = args.NewText.Any(c => !char.IsDigit(c));
        }
    }
}
