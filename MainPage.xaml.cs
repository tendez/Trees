using Microsoft.Maui.Controls;

using System;
using Trees.Models;
using Trees.Services;
using Trees.Views;

namespace Trees
{
    public partial class MainPage : ContentPage
    {
        public Stoisko _selectedStoisko;
     
        public MainPage()
        {
            InitializeComponent();

            if (Preferences.ContainsKey("IsLoggedIn") && Preferences.Get("IsLoggedIn", false))
            {
                
             
                ShowLoggedInUI();
            }
            else
            {
           
                Navigation.PushAsync(new Views.LoginPage());
            }
        }




        private void ShowLoggedInUI()
        {
            
            WybierzStoiskoButton.IsVisible = true;
            WybierzStoiskoButton.Clicked += OnWybierzStoiskoClicked;

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
            
            ShowActionsUI();
        }

        private async void OnDodajSprzedazClicked(object sender, EventArgs e)
        {
            if (_selectedStoisko != null)
            {
                await Navigation.PushAsync(new Views.DodajSprzedazPage(_selectedStoisko));
            }
            else
            {
           
                await DisplayAlert("Wybór stoiska", "Proszę wybrać stoisko przed dodaniem sprzedaży.", "OK");
            }
        }

        private async void OnZobaczSprzedazClicked(object sender, EventArgs e)
        {
            if (_selectedStoisko != null)
            {
                await Navigation.PushAsync(new Views.ZobaczSprzedazPage(_selectedStoisko));
            }
            else
            {
                
                await DisplayAlert("Wybór stoiska", "Proszę wybrać stoisko przed przeglądaniem sprzedaży.", "OK");
            }
        }

        private async void OnLogoutButtonClicked(object sender, EventArgs e)
        {
           
            Preferences.Set("IsLoggedIn", false);

            
            await Navigation.PushAsync(new Views.LoginPage());
        }
    }
}
