using Microsoft.Maui.Controls;
using Trees.Models;
using Trees.Services;
using System;
using System.Linq;

namespace Trees.Views
{
    public partial class ZobaczSprzedazPage : ContentPage
    {
        private readonly DatabaseService _databaseService;
        private readonly Stoisko _stoisko;
        private float totalSprzedaz;
        public ZobaczSprzedazPage(Stoisko stoisko)
        {
            InitializeComponent();
            _databaseService = new DatabaseService("Data Source=christmastreessofijowka.database.windows.net;Initial Catalog=Trees;User ID=mikolaj;Password=Qwerty123!;Connect Timeout=30;Encrypt=True;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False");
            _stoisko = stoisko;
            LoadTotalSprzedaz(stoisko, DateTime.Now);
            LoadSprzedaz(stoisko, DateTime.Now);
        }


        async void LoadTotalSprzedaz(Stoisko stoisko, DateTime date)
        {
            try
            {
                totalSprzedaz = await _databaseService.GetTotalSprzedazByStoiskoAndDateAsync(stoisko.StoiskoID,date);
                TotalSprzedazLabel.Text = $"Suma sprzeda�y: {totalSprzedaz} z�";
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Wyst�pi� b��d podczas �adowania danych: {ex.Message}");
                await DisplayAlert("B��d", "Wyst�pi� b��d podczas �adowania danych. Spr�buj ponownie p�niej.", "OK");
            }
        }

         async void LoadSprzedaz(Stoisko stoisko, DateTime date)
        {
            try
            {
                var sprzedazList = await _databaseService.GetSprzedazWithDetailsAndDateAsync(stoisko,date);
                SprzedazCollectionView.ItemsSource = sprzedazList;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Wyst�pi� b��d podczas �adowania danych: {ex.Message}");
                await DisplayAlert("B��d", "Wyst�pi� b��d podczas �adowania danych. Spr�buj ponownie p�niej.", "OK");
            }
        }
        protected override async void OnAppearing()
        {
            base.OnAppearing();
            LoadSprzedaz(_stoisko, DateTime.Now); 
        }
        private async void OnEditClicked(object sender, EventArgs e)
        {
            var button = sender as Button;
            Sprzedaz? sprzedaz = button?.CommandParameter as Sprzedaz;

            if (sprzedaz != null)
            {
              
               
                await Navigation.PushAsync(new EdytujSprzedazPage(sprzedaz)); 
            }
        }

        private async void OnDeleteClicked(object sender, EventArgs e)
        {
            var button = sender as Button;
            Sprzedaz? sprzedaz = button?.CommandParameter as Sprzedaz;

            if (sprzedaz != null)
            {
                var confirm = await DisplayAlert("Usu�", "Czy na pewno chcesz usun�� ten wpis?", "Tak", "Nie");
                if (confirm)
                {
            
                    await _databaseService.DeleteSprzedazAsync(sprzedaz.SprzedazID);
                    await DisplayAlert("Usuni�to", "Wpis zosta� usuni�ty.", "OK");
                    LoadSprzedaz(_stoisko, DateTime.Now);
                }
            }
        }
        private void OnDateSelected(object sender, DateChangedEventArgs e)
        {
            LoadSprzedaz(_stoisko,  e.NewDate);
            LoadTotalSprzedaz(_stoisko,  e.NewDate);
        }


    }
}
