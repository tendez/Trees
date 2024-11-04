using Microsoft.Maui.Controls;
using Trees.Models;
using Trees.Services;
using System;
using System.Linq;

namespace Trees.Views
{
    public partial class MojaSprzedazPage : ContentPage
    {
        private readonly DatabaseService _databaseService;
        private readonly Stoisko _stoisko;
        private float totalSprzedaz;
        private int _loggedInUserId;

        public MojaSprzedazPage(Stoisko stoisko)
        {
            InitializeComponent();
            _databaseService = new DatabaseService("Data Source=christmastreessofijowka.database.windows.net;Initial Catalog=Trees;User ID=mikolaj;Password=Qwerty123!;Connect Timeout=30;Encrypt=True;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False");
            _stoisko = stoisko;

            // Pobierz ID zalogowanego u�ytkownika
            _loggedInUserId = Preferences.Get("UserID", -1);

            if (_loggedInUserId == -1)
            {
                DisplayAlert("B��d", "Nie mo�na za�adowa� danych u�ytkownika. Prosz� spr�bowa� ponownie.", "OK");
                return;
            }

            LoadTotalSprzedaz(_stoisko, _loggedInUserId, DateTime.Now);
            LoadSprzedaz(_stoisko, _loggedInUserId, DateTime.Now);
        }

        async void LoadTotalSprzedaz(Stoisko stoisko, int userId, DateTime date)
        {
            try
            {
                float totalSprzedaz = await _databaseService.GetTotalSprzedazByStoiskoAndUserAndDateAsync(stoisko.StoiskoID, userId, date);
                TotalSprzedazLabel.Text = $"Suma sprzeda�y: {totalSprzedaz} z�";
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Wyst�pi� b��d podczas �adowania danych: {ex.Message}");
                await DisplayAlert("B��d", "Wyst�pi� b��d podczas �adowania danych. Spr�buj ponownie p�niej.", "OK");
            }
        }

        async void LoadSprzedaz(Stoisko stoisko, int userId, DateTime date)
        {
            try
            {
                var sprzedazList = await _databaseService.GetSprzedazWithDetailsAndUserAndDateAsync(stoisko, userId, date);
                SprzedazCollectionView.ItemsSource = sprzedazList;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Wyst�pi� b��d podczas �adowania danych: {ex.Message}");
                await DisplayAlert("B��d", "Wyst�pi� b��d podczas �adowania danych. Spr�buj ponownie p�niej.", "OK");
            }
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            LoadSprzedaz(_stoisko, _loggedInUserId, DateTime.Now);
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
                    LoadSprzedaz(_stoisko, _loggedInUserId, DateTime.Now);
                }
            }
        }
        private void OnDateSelected(object sender, DateChangedEventArgs e)
        {
            LoadSprzedaz(_stoisko, _loggedInUserId, e.NewDate);
            LoadTotalSprzedaz(_stoisko, _loggedInUserId, e.NewDate);
        }
    }
}
