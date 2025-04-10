using Microsoft.Maui.Controls;
using System;
using System.Linq;
using System.Threading.Tasks;
using Trees.Models;
using Trees.Services;

namespace Trees.Views
{
    public partial class WarehousePage : ContentPage
    {
        private readonly DatabaseService _databaseService;
        private Stoisko _selectedStoisko;

        public WarehousePage()
        {

            InitializeComponent();
            _databaseService = new DatabaseService();
            LoadStoiska();

        }


        private async void LoadStoiska()
        {
            var stoiska = await _databaseService.GetStoiskaAsync();
            StoiskoPicker.ItemsSource = stoiska.ToList();
            StoiskoPicker.ItemDisplayBinding = new Binding("StoiskoNazwa");
        }

        private async void OnStoiskoSelected(object sender, EventArgs e)
        {
            _selectedStoisko = (Stoisko)StoiskoPicker.SelectedItem;
            if (_selectedStoisko != null)
            {
                await LoadMagazyn(_selectedStoisko.StoiskoID);
            }
        }

        private async Task LoadMagazyn(int stoiskoId)
        {
            var warehouseItems = await _databaseService.GetMagazynAsync(stoiskoId);
            MagazynCollectionView.ItemsSource = warehouseItems;
            MagazynCollectionView.IsVisible = true;
        }

        private async void OnIncreaseQuantityClicked(object sender, EventArgs e)
        {
            if (sender is Button button && button.CommandParameter is WarehouseItem item)
            {
                var parent = button.Parent as StackLayout;
                var entry = parent?.FindByName<Entry>("EntryIlosc");
                if (entry != null && int.TryParse(entry.Text, out int quantity))
                {
                    item.Ilosc += quantity;

                    // Zaktualizuj w bazie danych
                    await _databaseService.UpdateMagazynAsync(new Magazyn
                    {
                        Ilosc = item.Ilosc,
                        MagazynID = item.MagazynID
                    });

                    // Od�wie� CollectionView
                    await RefreshCollectionViewAsync();

                    // Wymuszenie "roz��czenia" pickera po klikni�ciu
                    ResetPickerFocus();
                }
            }
        }

        private async void OnDecreaseQuantityClicked(object sender, EventArgs e)
        {
            if (sender is Button button && button.CommandParameter is WarehouseItem item)
            {
                var parent = button.Parent as StackLayout;
                var entry = parent?.FindByName<Entry>("EntryIlosc");
                if (entry != null && int.TryParse(entry.Text, out int quantity))
                {
                    item.Ilosc -= quantity;

                    // Zaktualizuj w bazie danych
                    await _databaseService.UpdateMagazynAsync(new Magazyn
                    {
                        Ilosc = item.Ilosc,
                        MagazynID = item.MagazynID
                    });

                    // Od�wie� CollectionView
                    await RefreshCollectionViewAsync();

                    // Wymuszenie "roz��czenia" pickera po klikni�ciu
                    ResetPickerFocus();
                }
            }
        }

        /// <summary>
        /// Zapobiega rozwijaniu pickera przez wymuszenie "utracenia" jego fokusu.
        /// </summary>
        private void ResetPickerFocus()
        {
            // Odwo�anie do pickera
            if (StoiskoPicker != null)
            {
                StoiskoPicker.IsEnabled = false;
                StoiskoPicker.IsEnabled = true; // Przywracamy mo�liwo�� interakcji
            }
        }

        /// <summary>
        /// Od�wie�a dane w CollectionView bez resetowania Pickera.
        /// </summary>
        private async Task RefreshCollectionViewAsync()
        {
            // Ustaw nowe �r�d�o danych dla CollectionView
         
            MagazynCollectionView.ItemsSource = await _databaseService.GetMagazynAsync(_selectedStoisko.StoiskoID);
        }


    }
}
