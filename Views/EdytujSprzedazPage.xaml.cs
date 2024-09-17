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
                // SprawdŸ, czy pola CenaEntry i IloscEntry nie s¹ puste
                if (string.IsNullOrWhiteSpace(CenaEntry.Text) || string.IsNullOrWhiteSpace(IloscEntry.Text))
                {
                    await DisplayAlert("B³¹d", "Cena i iloœæ nie mog¹ byæ puste.", "OK");
                    return;
                }

                // SprawdŸ, czy wartoœci s¹ poprawnymi liczbami
                if (!decimal.TryParse(CenaEntry.Text, out decimal nowaCena) || !int.TryParse(IloscEntry.Text, out int nowaIlosc))
                {
                    await DisplayAlert("B³¹d", "Wprowadzone wartoœci musz¹ byæ liczbami.", "OK");
                    return;
                }

                // SprawdŸ, czy cena i iloœæ s¹ dodatnie
                if (nowaCena <= 0)
                {
                    await DisplayAlert("B³¹d", "Cena musi byæ wiêksza od zera.", "OK");
                    return;
                }

                if (nowaIlosc <= 0)
                {
                    await DisplayAlert("B³¹d", "Iloœæ musi byæ wiêksza od zera.", "OK");
                    return;
                }

                // Aktualizacja danych sprzeda¿y
                _sprzedaz.Cena = nowaCena;
                _sprzedaz.Ilosc = nowaIlosc;

                // Zaktualizuj sprzeda¿ i magazyn
                await _databaseService.UpdateSprzedazAsync(_sprzedaz);

                await DisplayAlert("Zapisano", "Zmiany zosta³y zapisane.", "OK");
                await Navigation.PopAsync();
            }
            catch (InvalidOperationException ex)
            {
                // Jeœli stan magazynu jest za niski
                await DisplayAlert("B³¹d", ex.Message, "OK");
            }
            catch (Exception ex)
            {
                // Ogólny b³¹d
                await DisplayAlert("B³¹d", $"Wyst¹pi³ b³¹d: {ex.Message}", "OK");
            }
        }



    }
}
