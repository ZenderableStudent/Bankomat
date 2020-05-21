using System;
using System.Linq;
using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;

namespace Bankomat
{
    public sealed partial class Withdrawal : Page
    {
        public Withdrawal()
        {
            this.InitializeComponent();
            ustawStatus();
        }
        private void ustawStatus()
        {
           tbStatus.Text = MoneyContainers.status;
           tbStatus.Foreground = MoneyContainers.kolorStatusu;
        } 
        private void tbWithdraw_BeforeTextChanging(TextBox sender, TextBoxBeforeTextChangingEventArgs args) //tylko cyfry w textboxie
        {
            args.Cancel = args.NewText.Any(c => !char.IsDigit(c));
        }
        private void TrybSerwisowy_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(ServiceMode));
        }
        int iloscGotowkiWBankomacie = MoneyContainers.suma;
        int poczatkowaIloscGotowki = MoneyContainers.suma;
        private async void Wypłać_Click(object sender, RoutedEventArgs e)
        {
            int kwotaWyplaty = Int32.Parse(tbWithdraw.Text);
            if (kwotaWyplaty < 10)
            {
                ContentDialog zbytniska = new ContentDialog()
                {
                    Title = "Kwota zbyt niska",
                    Content = "Bankomat nie obsługuje monet, należy wyjąć przynajmniej 10 zł",
                    CloseButtonText = "OK"
                };
                await zbytniska.ShowAsync();
            }
            else if (kwotaWyplaty > 50000)
            {
                ContentDialog zbytwysoka = new ContentDialog()
                {
                    Title = "Kwota zbyt wysoka",
                    Content = "Maksymalna ilość gotówki w bankomacie to 500000, lecz maksymalna kwota wypłaty to 50000 (wydajność + bezpieczeństwo)",
                    CloseButtonText = "OK"
                };
                await zbytwysoka.ShowAsync();
            }
            else if (kwotaWyplaty % 10 != 0)
            {
                ContentDialog zbytwysoka = new ContentDialog()
                {
                    Title = "Zaraza...",
                    Content = "Bankomat nie obsługuje monet, nalezy wyjąć kwotę gdzie reszta z dzielenia przez 10 jest równa 0",
                    CloseButtonText = "OK"
                };
                await zbytwysoka.ShowAsync();
            }
            else
            {
                if(iloscGotowkiWBankomacie > 0 && kwotaWyplaty <= iloscGotowkiWBankomacie)
                {
                    wypłaćPieniądze(kwotaWyplaty);
                    if (poczatkowaIloscGotowki == iloscGotowkiWBankomacie && iloscGotowkiWBankomacie != 0)
                    {
                        ContentDialog porazka = new ContentDialog()
                        {
                            Title = "Jasny gwint!",
                            Content = "Nie udało się wypłacić kwoty " + tbWithdraw.Text + " zł, zostało w bankomacie: " + iloscGotowkiWBankomacie.ToString() + " zł, brakuje pieniędzy w którymś z pojemników!",
                            CloseButtonText = "OK"
                        };
                        await porazka.ShowAsync();
                    }
                    else if (poczatkowaIloscGotowki != iloscGotowkiWBankomacie && iloscGotowkiWBankomacie != 0)
                    {
                        ContentDialog sukces = new ContentDialog()
                        {
                            Title = "Wypłacanie pieniędzy...",
                            Content = "Udało się pomyślnie wypłacić kwotę " + tbWithdraw.Text + " zł, zostało w bankomacie: " + iloscGotowkiWBankomacie.ToString() + " zł",
                            CloseButtonText = "OK"
                        };
                        await sukces.ShowAsync();
                    }
                    else if (iloscGotowkiWBankomacie == 0)
                    {
                        MoneyContainers.status = "Pusty";
                        MoneyContainers.kolorStatusu = new SolidColorBrush(Colors.Red);
                        ustawStatus();
                        ContentDialog sukces = new ContentDialog()
                        {
                            Title = "Wypłacanie pieniędzy...",
                            Content = "Udało się pomyślnie wypłacić kwotę " + tbWithdraw.Text + " zł, zostało w bankomacie: " + iloscGotowkiWBankomacie.ToString() + "zł",
                            CloseButtonText = "OK"
                        };
                        await sukces.ShowAsync();
                    }
                    else
                    {
                        ContentDialog sukces = new ContentDialog()
                        {
                            Title = "Wypłacanie pieniędzy...",
                            Content = "Udało się pomyślnie wypłacić kwotę " + tbWithdraw.Text + " zł, zostało w bankomacie: " + iloscGotowkiWBankomacie.ToString() + "zł",
                            CloseButtonText = "OK"
                        };
                        await sukces.ShowAsync();
                    }     
                }
                else
                {
                    ContentDialog porazka = new ContentDialog()
                    {
                        Title = "Motyla noga!",
                        Content = "Zbyt mało gotówki w bankomacie, aktualny stan: " + (iloscGotowkiWBankomacie).ToString() + " zł",
                        CloseButtonText = "OK"
                    };
                    await porazka.ShowAsync();
                }
            }
        }
        private void wypłaćPieniądze(int _iloscWyplaty)
        {
            int iloscWyplaty = _iloscWyplaty;
            
            if (MoneyContainers.dwiescie > 0)
            {
                if ((MoneyContainers.dwiescie - (iloscWyplaty / 200)) < 0)
                {
                    while (MoneyContainers.dwiescie > 0)
                    {
                        iloscWyplaty -= 200;
                        MoneyContainers.dwiescie--;
                    }
                }
                else
                {
                    int Banknot200;
                    if(MoneyContainers.dwiescie <= 8 || MoneyContainers.piecdziesiat == 0 || MoneyContainers.sto == 0 || MoneyContainers.dwadziescia == 0 || MoneyContainers.dziesiec == 0)
                    Banknot200 = iloscWyplaty / 200;
                    else
                        Banknot200 = iloscWyplaty / (200 * 4);
                    iloscWyplaty -= (Banknot200 * 200);
                    MoneyContainers.dwiescie -= Banknot200;
                }
            }

            if (MoneyContainers.sto > 0)
            {
                if ((MoneyContainers.sto - (iloscWyplaty / 100)) < 0)
                {
                    while (MoneyContainers.sto > 0)
                    {
                        iloscWyplaty -= 100;
                        MoneyContainers.sto--;
                    }
                }
                else
                {
                    int Banknot100;
                    if (MoneyContainers.sto <= 8 || MoneyContainers.dwiescie == 0 || MoneyContainers.piecdziesiat == 0 || MoneyContainers.dwadziescia == 0 || MoneyContainers.dziesiec == 0)
                        Banknot100 = iloscWyplaty / 100;
                    else
                        Banknot100 = iloscWyplaty / (100 * 4);
                    iloscWyplaty -= (Banknot100 * 100);
                    MoneyContainers.sto -= Banknot100;
                }
            }

            if (MoneyContainers.piecdziesiat > 0)
            {
                if ((MoneyContainers.piecdziesiat - (iloscWyplaty / 50)) < 0)
                {
                    while (MoneyContainers.piecdziesiat > 0)
                    {
                        iloscWyplaty -= 50;
                        MoneyContainers.piecdziesiat--;
                    }
                }
                else
                {
                    int Banknot50;
                    if(MoneyContainers.piecdziesiat <= 4 || MoneyContainers.dwiescie == 0 || MoneyContainers.sto == 0 || MoneyContainers.dwadziescia == 0 || MoneyContainers.dziesiec == 0)
                    Banknot50 = iloscWyplaty / 50;
                    else
                        Banknot50 = iloscWyplaty / (50 * 2);
                    iloscWyplaty -= (Banknot50 * 50);
                    MoneyContainers.piecdziesiat -= Banknot50;
                }
            }
            if (MoneyContainers.dwadziescia > 0)
            {
                if ((MoneyContainers.dwadziescia - (iloscWyplaty / 20)) < 0)
                {
                    while (MoneyContainers.dwadziescia > 0)
                    {
                        iloscWyplaty -= 20;
                        MoneyContainers.piecdziesiat--;
                    }
                }
                else
                {
                    int Banknot20 = iloscWyplaty / 20;
                    iloscWyplaty -= (Banknot20 * 20);
                    MoneyContainers.dwadziescia -= Banknot20;
                }
            }

            if (MoneyContainers.dziesiec > 0)
            {
                if ((MoneyContainers.dziesiec - (iloscWyplaty / 10)) < 0)
                {
                    while (MoneyContainers.dziesiec > 0)
                    {
                        iloscWyplaty -= 10;
                        MoneyContainers.dziesiec--;
                    }
                }
                else
                {
                    int Banknot10 = iloscWyplaty / 10;
                    iloscWyplaty -= (Banknot10 * 10);
                    MoneyContainers.dziesiec -= Banknot10;
                }
            }
            iloscGotowkiWBankomacie = MoneyContainers.dziesiec * 10 + MoneyContainers.dwadziescia * 20 + MoneyContainers.piecdziesiat * 50 + MoneyContainers.sto * 100 + MoneyContainers.dwiescie * 200;
            MoneyContainers.status = iloscGotowkiWBankomacie.ToString();
            if (MoneyContainers.dziesiec == 0 || MoneyContainers.dwadziescia == 0 || MoneyContainers.piecdziesiat == 0 || MoneyContainers.sto == 0 || MoneyContainers.dwiescie == 0)
            {
                MoneyContainers.status = "Działający";
                MoneyContainers.kolorStatusu = new SolidColorBrush(Colors.Orange);
                ustawStatus();
            }
            if (iloscGotowkiWBankomacie == 0)
            {
                
                MoneyContainers.status = "Pusty";
                MoneyContainers.kolorStatusu = new SolidColorBrush(Colors.Red);
                ustawStatus();
            }

        }
    }
}