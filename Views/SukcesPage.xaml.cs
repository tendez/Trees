using Microsoft.Maui.Controls;

namespace Trees.Views
{
    public partial class SukcesPage : ContentPage
    {
        public SukcesPage()
        {
            InitializeComponent();
        }

        private async void OnDodajKolejnaSprzedazClicked(object sender, EventArgs e)
        {
            await Navigation.PopToRootAsync();
        }

        private async void OnZobaczSprzedazClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new ZobaczSprzedazPage());
        }
    }
}
