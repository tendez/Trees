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
                // Sprawd�, czy pola CenaEntry i IloscEntry nie s� puste
                if (string.IsNullOrWhiteSpace(CenaEntry.Text) || string.IsNullOrWhiteSpace(IloscEntry.Text))
                {
                    await DisplayAlert("B��d", "Cena i ilo�� nie mog� by� puste.", "OK");
                    return;
                }

                // Sprawd�, czy warto�ci s� poprawnymi liczbami
                if (!decimal.TryParse(CenaEntry.Text, out decimal nowaCena) || !int.TryParse(IloscEntry.Text, out int nowaIlosc))
                {
                    await DisplayAlert("B��d", "Wprowadzone warto�ci musz� by� liczbami.", "OK");
                    return;
                }

                // Sprawd�, czy cena i ilo�� s� dodatnie
                if (nowaCena <= 0)
                {
                    await DisplayAlert("B��d", "Cena musi by� wi�ksza od zera.", "OK");
                    return;
                }

                if (nowaIlosc <= 0)
                {
                    await DisplayAlert("B��d", "Ilo�� musi by� wi�ksza od zera.", "OK");
                    return;
                }

                // Aktualizacja danych sprzeda�y
                _sprzedaz.Cena = nowaCena;
                _sprzedaz.Ilosc = nowaIlosc;

                // Zaktualizuj sprzeda� i magazyn
                await _databaseService.UpdateSprzedazAsync(_sprzedaz);

                await DisplayAlert("Zapisano", "Zmiany zosta�y zapisane.", "OK");
                await Navigation.PopAsync();
            }
            catch (InvalidOperationException ex)
            {
                // Je�li stan magazynu jest za niski
                await DisplayAlert("B��d", ex.Message, "OK");
            }
            catch (Exception ex)
            {
                // Og�lny b��d
                await DisplayAlert("B��d", $"Wyst�pi� b��d: {ex.Message}", "OK");
            }
        }



    }
}
