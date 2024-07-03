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

        public DodajCenePage(Gatunek selectedGatunek, Wielkosc selectedWielkosc,Stoisko stoisko)
        {
            InitializeComponent();
            _databaseService = new DatabaseService("Data Source=christmastreessofijowka.database.windows.net;Initial Catalog=Trees;User ID=mikolaj;Password=Qwerty123!;Connect Timeout=30;Encrypt=True;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False");
            _selectedGatunek = selectedGatunek;
            _selectedWielkosc = selectedWielkosc;
            _stoisko = stoisko;
        }

        private async void OnDodajSprzedazClicked(object sender, EventArgs e)
        {
            var sprzedaz = new Sprzedaz
            {
                UserID = Preferences.Get("UserID", 0),
                GatunekID = _selectedGatunek.GatunekID,
                WielkoscID = _selectedWielkosc.WielkoscID,
                Cena = decimal.Parse(CenaEntry.Text),
                Ilosc = int.Parse(IloscEntry.Text),
                CalkowitaCena = decimal.Parse(CenaEntry.Text) * int.Parse(IloscEntry.Text),
                DataSprzedazy = DateTime.Now,
                StoiskoID = _stoisko.StoiskoID
            };

            await _databaseService.AddSprzedazAsync(sprzedaz);

            await Navigation.PushAsync(new SukcesPage(_stoisko));
        }
    }
}
