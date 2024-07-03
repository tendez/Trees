using Trees.Models;
using Trees.Services;
using Microsoft.Maui.Controls;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Trees.Views
{
    public partial class ZobaczSprzedazPage : ContentPage
    {
        private readonly DatabaseService _databaseService;
       
        public ZobaczSprzedazPage(Stoisko stoisko)
        {
            InitializeComponent();
            _databaseService = new DatabaseService("Data Source=christmastreessofijowka.database.windows.net;Initial Catalog=Trees;User ID=mikolaj;Password=Qwerty123!;Connect Timeout=30;Encrypt=True;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False");
            LoadSprzedaz(stoisko);
        
        }

        private async void LoadSprzedaz(Stoisko stoisko)
        {
            try
            {
                var sprzedazList = await _databaseService.GetSprzedazWithDetailsAsync(stoisko);
                SprzedazCollectionView.ItemsSource = sprzedazList;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Wyst�pi� b��d podczas �adowania danych: {ex.Message}");
                await DisplayAlert("B��d", "Wyst�pi� b��d podczas �adowania danych. Spr�buj ponownie p�niej.", "OK");
            }
        }

    }
}
