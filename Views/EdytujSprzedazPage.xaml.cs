using Microsoft.Maui.Controls;
using Trees.Models;
using Trees.Services;

namespace Trees.Views
{
    public partial class EdytujSprzedazPage : ContentPage
    {
        private readonly DatabaseService _databaseService;
        private readonly Sprzedaz _sprzedaz;

        public EdytujSprzedazPage(Sprzedaz sprzedaz)
        {
            InitializeComponent();
            _databaseService = new DatabaseService("Data Source=christmastreessofijowka.database.windows.net;Initial Catalog=Trees;User ID=mikolaj;Password=Qwerty123!;Connect Timeout=30;Encrypt=True;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False");

            _sprzedaz = sprzedaz;
            BindingContext = _sprzedaz;
        }

        private async void OnSaveClicked(object sender, EventArgs e)
        {
            _sprzedaz.Cena = decimal.Parse(CenaEntry.Text);
            _sprzedaz.Ilosc = int.Parse(IloscEntry.Text);

            
            await _databaseService.UpdateSprzedazAsync(_sprzedaz);

            await DisplayAlert("Zapisano", "Zmiany zosta³y zapisane.", "OK");
            await Navigation.PopAsync(); 
        }
    }
}
