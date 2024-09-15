using Trees.Models;
using Trees.Services;
using Microsoft.Maui.Controls;
using System;

namespace Trees.Views
{
    public partial class DodajCenePage : ContentPage
    {
        private readonly DatabaseService _databaseService;
        private readonly Gatunek _selectedGatunek;
        private readonly Wielkosc _selectedWielkosc;
        private readonly Stoisko _stoisko;

        public DodajCenePage(Gatunek selectedGatunek, Wielkosc selectedWielkosc, Stoisko stoisko)
        {
            InitializeComponent();
            _databaseService = new DatabaseService("Data Source=christmastreessofijowka.database.windows.net;Initial Catalog=Trees;User ID=mikolaj;Password=Qwerty123!;Connect Timeout=30;Encrypt=True;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False");
            _selectedGatunek = selectedGatunek;
            _selectedWielkosc = selectedWielkosc;
            _stoisko = stoisko;
        }

        private async void OnDodajSprzedazClicked(object sender, EventArgs e)
        {
            warning.IsVisible = false; // Ukryj ostrze¿enia, jeœli by³y wczeœniej widoczne

            // Sprawdzenie, czy wprowadzone wartoœci to liczby
            if (!decimal.TryParse(CenaEntry.Text, out decimal cena))
            {
                warning.Text = "WprowadŸ poprawn¹ liczbê w polu Cena.";
                warning.IsVisible = true;
                return;
            }

            if (!int.TryParse(IloscEntry.Text, out int ilosc))
            {
                warning.Text = "WprowadŸ poprawn¹ liczbê w polu Iloœæ.";
                warning.IsVisible = true;
                return;
            }

            // Sprawdzenie, czy cena i iloœæ nie s¹ ujemne
            if (cena <= 0)
            {
                warning.Text = "Cena musi byæ wiêksza ni¿ 0.";
                warning.IsVisible = true;
                return;
            }

            if (ilosc <= 0)
            {
                warning.Text = "Iloœæ musi byæ wiêksza ni¿ 0.";
                warning.IsVisible = true;
                return;
            }

            // Tworzenie obiektu Sprzedaz
            var sprzedaz = new Sprzedaz
            {
                UserID = Preferences.Get("UserID", 0),
                GatunekID = _selectedGatunek.GatunekID,
                WielkoscID = _selectedWielkosc.WielkoscID,
                Cena = cena,
                Ilosc = ilosc,
                CalkowitaCena = cena * ilosc,
                DataSprzedazy = DateTime.Now,
                StoiskoID = _stoisko.StoiskoID
            };

            // Dodanie sprzeda¿y do bazy danych
            await _databaseService.AddSprzedazAsync(sprzedaz);

            // Przekierowanie do strony sukcesu
            await Navigation.PushAsync(new SukcesPage(_stoisko));
        }
    }
}
