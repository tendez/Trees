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

        public WyborWielkosciPage(Gatunek selectedGatunek,Stoisko  stoisko)
        {
            InitializeComponent();
            _databaseService = new DatabaseService("Data Source=christmastreessofijowka.database.windows.net;Initial Catalog=Trees;User ID=mikolaj;Password=Qwerty123!;Connect Timeout=30;Encrypt=True;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False");

            _selectedGatunek = selectedGatunek;
            _stoisko = stoisko;
            LoadWielkosci();
            
        }

        private async void LoadWielkosci()
        {
            var wielkosci = await _databaseService.GetWielkosciAsync();
            WielkoscCollectionView.ItemsSource = wielkosci;
            var stoisko = new Stoisko();
            
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
                await Navigation.PushAsync(new DodajCenePage(_selectedGatunek, selectedWielkosc,_stoisko));
            }
        }
    }
}
