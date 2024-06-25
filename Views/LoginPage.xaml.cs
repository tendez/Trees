// Views/LoginPage.xaml.cs
using Microsoft.Maui.Controls;

using System;
using Microsoft.Data.SqlClient;
using System.Threading.Tasks;

namespace Trees.Views
{
    public partial class LoginPage : ContentPage
    {
        public LoginPage()
        {
            InitializeComponent();
        }

        private async void OnLoginButtonClicked(object sender, EventArgs e)
        {
            string username = UsernameEntry.Text;
            string password = PasswordEntry.Text;

            if (await ValidateUser(username, password))
            {
                // Ustawienie stanu zalogowania
                Preferences.Set("IsLoggedIn", true);

                // Pomyœlne logowanie, przekierowanie do strony g³ównej
                await Navigation.PushAsync(new MainPage());
            }
            else
            {
                // B³êdne dane logowania
                LoginStatusLabel.Text = "Nieprawid³owa nazwa u¿ytkownika lub has³o.";
            }
        }

        private async Task<bool> ValidateUser(string username, string password)
        {
            bool isValid = false;
            string hashedPassword = HashPassword(password);

            using (SqlConnection connection = new SqlConnection("Data Source=christmastreessofijowka.database.windows.net;Initial Catalog=Trees;User ID=mikolaj;Password=Qwerty123!;Connect Timeout=30;Encrypt=True;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False"))
            {
                string query = "SELECT COUNT(1) FROM Uzytkownicy WHERE Login=@username AND Password=@password";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@username", username);
                command.Parameters.AddWithValue("@password", hashedPassword);
                connection.Open();

                int count = (int)await command.ExecuteScalarAsync();
                isValid = count > 0;
            }

            return isValid;
        }

        private string HashPassword(string password)
        {
            using (var sha256 = System.Security.Cryptography.SHA256.Create())
            {
                byte[] bytes = sha256.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                var builder = new System.Text.StringBuilder();
                for (int i = 0; i < bytes.Length; i++)
                {
                    builder.Append(bytes[i].ToString("x2"));
                }
                return builder.ToString();
            }
        }
    }
}
