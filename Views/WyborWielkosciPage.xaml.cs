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

        public WyborWielkosciPage(Gatunek selectedGatunek)
        {
            InitializeComponent();
            _databaseService = new DatabaseService("Data Source=christmastreessofijowka.database.windows.net;Initial Catalog=Trees;User ID=mikolaj;Password=Qwerty123!;Connect Timeout=30;Encrypt=True;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False");

            _selectedGatunek = selectedGatunek;
            LoadWielkosci();
        }

        private async void LoadWielkosci()
        {
            var wielkosci = await _databaseService.GetWielkosciAsync();
            WielkoscCollectionView.ItemsSource = wielkosci;
        }

        private void OnWielkoscSelected(object sender, SelectionChangedEventArgs e)
        {
            var selectedWielkosc = e.CurrentSelection.FirstOrDefault() as Wielkosc;
            if (selectedWielkosc != null)
            {
                // Aktywuj przycisk "Dalej"
                DalejButton.IsEnabled = true;
            }
        }

        private async void OnDalejClicked(object sender, EventArgs e)
        {
            // PrzejdŸ do kolejnej strony z dodawaniem ceny i przeka¿ wybrany gatunek i wielkoœæ
            var selectedWielkosc = WielkoscCollectionView.SelectedItem as Wielkosc;
            if (selectedWielkosc != null)
            {
                await Navigation.PushAsync(new DodajCenePage(_selectedGatunek, selectedWielkosc));
            }
        }
    }
}
