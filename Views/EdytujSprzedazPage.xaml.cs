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
                    await DisplayAlert("B³¹d", "Cena  nie moze byæ pusta.", "OK");
                    return;
                }

               
                if (!decimal.TryParse(CenaEntry.Text, out decimal nowaCena) )
                {
                    await DisplayAlert("B³¹d", "Wprowadzone wartoœci musz¹ byæ liczbami.", "OK");
                    return;
                }

              
                if (nowaCena <= 0)
                {
                    await DisplayAlert("B³¹d", "Cena musi byæ wiêksza od zera.", "OK");
                    return;
                }

              
              
                _sprzedaz.Cena = nowaCena;
              

             
                await _databaseService.UpdateSprzedazAsync(_sprzedaz);

                await DisplayAlert("Zapisano", "Zmiany zosta³y zapisane.", "OK");
                await Navigation.PopAsync();
            }
            catch (InvalidOperationException ex)
            {
          
                await DisplayAlert("B³¹d", ex.Message, "OK");
            }
            catch (Exception ex)
            {
                
                await DisplayAlert("B³¹d", $"Wyst¹pi³ b³¹d: {ex.Message}", "OK");
            }
        }



    }
}
