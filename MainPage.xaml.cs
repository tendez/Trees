// MainPage.xaml.cs
using Microsoft.Maui.Controls;

using System;

namespace Trees
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();

            if (Preferences.ContainsKey("IsLoggedIn") && Preferences.Get("IsLoggedIn", false))
            {
                // Użytkownik zalogowany
            }
            else
            {
                // Przekierowanie do strony logowania
                Navigation.PushAsync(new Views.LoginPage());
            }
        }

        private async void OnDodajSprzedazClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new Views.DodajSprzedazPage());
        }

        private async void OnZobaczSprzedazClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new Views.ZobaczSprzedazPage());
        }

        private async void OnLogoutButtonClicked(object sender, EventArgs e)
        {
            Preferences.Set("IsLoggedIn", false);

            // Przekierowanie do strony logowania
            await Navigation.PushAsync(new Views.LoginPage());
        }
    }
}
