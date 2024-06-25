using Trees.Models;
using Trees.Services;
using Microsoft.Maui.Controls;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Trees.Views
{
    public partial class ZobaczSprzedazPage : ContentPage
    {
        private readonly DatabaseService _databaseService;

        public ZobaczSprzedazPage()
        {
            InitializeComponent();
            _databaseService = new DatabaseService("Data Source=christmastreessofijowka.database.windows.net;Initial Catalog=Trees;User ID=mikolaj;Password=Qwerty123!;Connect Timeout=30;Encrypt=True;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False");
            LoadSprzedaz();
        }

        private async void LoadSprzedaz()
        {
            var sprzedazList = await _databaseService.GetSprzedazWithGatunkiAsync();
            SprzedazCollectionView.ItemsSource = sprzedazList;
        }

    }
}
