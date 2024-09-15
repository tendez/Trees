using Trees.Models;
using Trees.Services;
using Microsoft.Maui.Controls;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Trees.Views
{
    public partial class WyborStoiskaPage : ContentPage
    {
        private readonly DatabaseService _databaseService;

        public event EventHandler<Stoisko> StoiskoSelected;

        public WyborStoiskaPage()
        {
            InitializeComponent();
            _databaseService = new DatabaseService("Data Source=christmastreessofijowka.database.windows.net;Initial Catalog=Trees;User ID=mikolaj;Password=Qwerty123!;Connect Timeout=30;Encrypt=True;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False");
            LoadStoiska();
        }

        private async void LoadStoiska()
        {
            var stoiska = await _databaseService.GetStoiskaAsync();
            StoiskaCollectionView.ItemsSource = stoiska;
        }

        private async void OnStoiskoSelected(object sender, SelectionChangedEventArgs e)
        {
            var selectedStoisko = e.CurrentSelection.FirstOrDefault() as Stoisko;
            if (selectedStoisko != null)
            {
                StoiskoSelected?.Invoke(this, selectedStoisko); // Wywo³anie zdarzenia dla MainPage
                await Navigation.PopAsync(); // Wraca do MainPage
            }
        }
    }
}
