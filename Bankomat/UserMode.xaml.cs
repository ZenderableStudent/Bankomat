using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace Bankomat
{
    public sealed partial class UserMode : Page
    {
        public UserMode()
        {
            this.InitializeComponent();
            ustawStatus();
        }
        private void ustawStatus()
        {
           tbStatus.Text = MoneyContainers.status;
           tbStatus.Foreground = MoneyContainers.kolorStatusu;
        }
        private void TrybSerwisowy_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(ServiceMode));
        }
        private void Wyplata_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(Withdrawal));
        }
    }
}