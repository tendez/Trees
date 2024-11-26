using Microsoft.Maui.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Trees.Models;
using Trees.Services;

namespace Trees.Views
{
    public partial class WarehousePage : ContentPage
    {
        private readonly DatabaseService _databaseService;
        private Stoisko _selectedStoisko;

        public WarehousePage()
        {
            InitializeComponent();
            _databaseService = new DatabaseService("Data Source=christmastreessofijowka.database.windows.net;Initial Catalog=Trees;User ID=mikolaj;Password=Qwerty123!;Connect Timeout=30;Encrypt=True;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False");
            LoadStoiska();
        }

        private async void LoadStoiska()
        {
            var stoiska = await _databaseService.GetStoiskaAsync();
            StoiskoPicker.ItemsSource = stoiska.ToList();
            StoiskoPicker.ItemDisplayBinding = new Binding("StoiskoNazwa");
        }

        private async void OnStoiskoSelected(object sender, EventArgs e)
        {
            _selectedStoisko = (Stoisko)StoiskoPicker.SelectedItem;
            if (_selectedStoisko != null)
            {
                await LoadMagazyn(_selectedStoisko.StoiskoID);
            }
        }

        private async Task LoadMagazyn(int stoiskoId)
        {
            var warehouseItems = await _databaseService.GetMagazynAsync(stoiskoId);
            MagazynCollectionView.ItemsSource = warehouseItems;
            MagazynCollectionView.IsVisible = true;
        }



        private async void OnIncreaseQuantityClicked(object sender, EventArgs e)
        {
            var button = (Button)sender;
            var magazyn = (Magazyn)button.BindingContext;

            magazyn.Ilosc += 1;
            await _databaseService.UpdateMagazynAsync(magazyn);
            await LoadMagazyn(_selectedStoisko.StoiskoID);
        }

        private async void OnDecreaseQuantityClicked(object sender, EventArgs e)
        {
            var button = (Button)sender;
            var magazyn = (Magazyn)button.BindingContext;

            if (magazyn.Ilosc > 0)
            {
                magazyn.Ilosc -= 1;
                await _databaseService.UpdateMagazynAsync(magazyn);
                await LoadMagazyn(_selectedStoisko.StoiskoID);
            }
        }
    }
}
