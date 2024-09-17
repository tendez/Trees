using Microsoft.Maui.Controls;
using Trees.Models;
using Trees.Services;
using Trees.Views;

namespace Trees
{
    public partial class MainPage : ContentPage
    {
        public Stoisko _selectedStoisko;
        private readonly DatabaseService _databaseService;

        public MainPage()
        {
            InitializeComponent();
            _databaseService = new DatabaseService("Data Source=christmastreessofijowka.database.windows.net;Initial Catalog=Trees;User ID=mikolaj;Password=Qwerty123!;Connect Timeout=30;Encrypt=True;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False");

            if (Preferences.ContainsKey("IsLoggedIn") && Preferences.Get("IsLoggedIn", false))
            {
           
                if (Preferences.ContainsKey("SelectedStoiskoID"))
                {
                    int stoiskoId = Preferences.Get("SelectedStoiskoID", 0);
                    LoadSelectedStoisko(stoiskoId);
                }
                else
                {
                    ShowLoggedInUI(); 
                }
            }
            else
            {
                Navigation.PushAsync(new Views.LoginPage());
            }
        }

        private async void LoadSelectedStoisko(int stoiskoId)
        {
            _selectedStoisko = await _databaseService.GetStoiskoByIdAsync(stoiskoId); 
            if (_selectedStoisko != null)
            {
                NazwaStoiskaLabel.Text = _selectedStoisko.StoiskoNazwa;
                NazwaStoiskaLabel.IsVisible = true;
                ShowActionsUI(); 
            }
            else
            {
                ShowLoggedInUI(); 
            }
        }

        private void ShowLoggedInUI()
        {
            WybierzStoiskoButton.IsVisible = true;
            DodajSprzedazButton.IsVisible = false;
            ZobaczSprzedazButton.IsVisible = false;
            LogoutButton.IsVisible = false;
        }

        private void ShowActionsUI()
        {
            DodajSprzedazButton.IsVisible = true;
            ZobaczSprzedazButton.IsVisible = true;
            LogoutButton.IsVisible = true;
            WybierzStoiskoButton.IsVisible = true;
        }

        private async void OnWybierzStoiskoClicked(object sender, EventArgs e)
        {
            var stoiskoPage = new Views.WyborStoiskaPage();
            stoiskoPage.StoiskoSelected += OnStoiskoSelected;
            await Navigation.PushAsync(stoiskoPage);
        }

        private void OnStoiskoSelected(object sender, Stoisko selectedStoisko)
        {
            _selectedStoisko = selectedStoisko;
            NazwaStoiskaLabel.Text = _selectedStoisko.StoiskoNazwa;
            NazwaStoiskaLabel.IsVisible = true;

        
            Preferences.Set("SelectedStoiskoID", _selectedStoisko.StoiskoID);

            ShowActionsUI();
        }

        private async void OnDodajSprzedazClicked(object sender, EventArgs e)
        {
            if (_selectedStoisko == null)
            {
                await DisplayAlert("Błąd", "Proszę wybrać stoisko przed dodaniem sprzedaży.", "OK");
                return;
            }
            await Navigation.PushAsync(new Views.DodajSprzedazPage(_selectedStoisko));
        }

        private async void OnZobaczSprzedazClicked(object sender, EventArgs e)
        {
            if (_selectedStoisko == null)
            {
                await DisplayAlert("Błąd", "Proszę wybrać stoisko przed przeglądaniem sprzedaży.", "OK");
                return;
            }
            await Navigation.PushAsync(new Views.ZobaczSprzedazPage(_selectedStoisko));
        }


        private async void OnLogoutButtonClicked(object sender, EventArgs e)
        {
            Preferences.Set("IsLoggedIn", false);
            Preferences.Remove("SelectedStoiskoID");
            await Navigation.PushAsync(new Views.LoginPage());
        }
    }
}
