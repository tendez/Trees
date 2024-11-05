using Trees.Models;
using Trees.Services;
using Microsoft.Maui.Controls;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Trees.Views
{
    public partial class WyborWielkosciPage : ContentPage
    {
        private readonly DatabaseService _databaseService;
        private readonly Gatunek _selectedGatunek;
        private readonly Stoisko _stoisko;

        public WyborWielkosciPage(Gatunek selectedGatunek, Stoisko stoisko)
        {
            InitializeComponent();
            _databaseService = new DatabaseService("Data Source=christmastreessofijowka.database.windows.net;Initial Catalog=Trees;User ID=mikolaj;Password=Qwerty123!;Connect Timeout=30;Encrypt=True;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False");

            _selectedGatunek = selectedGatunek;
            _stoisko = stoisko;
            LoadWielkosci();
        }

        private async void LoadWielkosci()
        {
            // Pobierz wszystkie wielkosci z bazy danych
            var wielkosci = await _databaseService.GetWielkosciAsync();
            // Przefiltruj wielkosci na podstawie wybranego gatunku
            WielkoscCollectionView.ItemsSource = FilterWielkosci(wielkosci);
        }

        // Metoda filtruj¹ca wielkosci na podstawie wybranego gatunku
        private IEnumerable<Wielkosc> FilterWielkosci(IEnumerable<Wielkosc> wielkosci)
        {
            // Filtracja w zaleznosci od wybranego gatunku, tylko dla okreslonych kolorow
            if (_selectedGatunek.NazwaGatunku == "Swierk Pospolity Donica")
            {
                return wielkosci.Where(w => w.OpisWielkosci == "Zolta" || w.OpisWielkosci == "Niebieska" || w.OpisWielkosci == "Biala" || w.OpisWielkosci == "Czerwona" || w.OpisWielkosci == "Zielona");
            }
            else if (_selectedGatunek.NazwaGatunku == "Swierk Srebrny Donica")
            {
                return wielkosci.Where(w => w.OpisWielkosci == "Zolta" || w.OpisWielkosci == "Czarna" || w.OpisWielkosci == "Biala");
            }
            else if (_selectedGatunek.NazwaGatunku == "Swierk Pospolity")
            {
                return wielkosci.Where(w => w.OpisWielkosci == "Pomaranczowa" || w.OpisWielkosci == "Czarna" || w.OpisWielkosci == "Biala");
            }
            // W przypadku braku dopasowania do zadnego gatunku, zwracamy wszystkie wielkosci
            return wielkosci;
        }

        private void OnWielkoscSelected(object sender, SelectionChangedEventArgs e)
        {
            Wielkosc? selectedWielkosc = e.CurrentSelection.FirstOrDefault() as Wielkosc;
            if (selectedWielkosc != null)
            {
                DalejButton.IsEnabled = true;
            }
        }

        private async void OnDalejClicked(object sender, EventArgs e)
        {
            Wielkosc? selectedWielkosc = WielkoscCollectionView.SelectedItem as Wielkosc;
            if (selectedWielkosc != null)
            {
                await Navigation.PushAsync(new DodajCenePage(_selectedGatunek, selectedWielkosc, _stoisko));
            }
        }
    }
}
