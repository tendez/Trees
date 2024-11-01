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
            try
            {
           
                if (string.IsNullOrWhiteSpace(CenaEntry.Text) )
                {
                    await DisplayAlert("B��d", "Cena  nie moze by� pusta.", "OK");
                    return;
                }

               
                if (!decimal.TryParse(CenaEntry.Text, out decimal nowaCena) )
                {
                    await DisplayAlert("B��d", "Wprowadzone warto�ci musz� by� liczbami.", "OK");
                    return;
                }

              
                if (nowaCena <= 0)
                {
                    await DisplayAlert("B��d", "Cena musi by� wi�ksza od zera.", "OK");
                    return;
                }

              
              
                _sprzedaz.Cena = nowaCena;
              

             
                await _databaseService.UpdateSprzedazAsync(_sprzedaz);

                await DisplayAlert("Zapisano", "Zmiany zosta�y zapisane.", "OK");
                await Navigation.PopAsync();
            }
            catch (InvalidOperationException ex)
            {
          
                await DisplayAlert("B��d", ex.Message, "OK");
            }
            catch (Exception ex)
            {
                
                await DisplayAlert("B��d", $"Wyst�pi� b��d: {ex.Message}", "OK");
            }
        }



    }
}
