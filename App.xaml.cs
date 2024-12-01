using Microsoft.Extensions.Configuration;

namespace Trees
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();
          
            
            MainPage = new AppShell();
        }
    }
} 