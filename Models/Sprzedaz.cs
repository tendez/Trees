namespace Trees.Models
{
    public class Sprzedaz
    {
        public int SprzedazID { get; set; }
        public int GatunekID { get; set; }
        public int WielkoscID { get; set; }
        public decimal Cena { get; set; }
        public int Ilosc { get; set; }
        public decimal CalkowitaCena { get; set; }
        public DateTime DataSprzedazy { get; set; }
        public string NazwaGatunku { get; set; } // If you are joining with Gatunek table
        public string OpisWielkosc { get; set; } // If you are joining with Wielkosc table
    }
}
