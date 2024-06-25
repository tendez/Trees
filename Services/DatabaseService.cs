using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Dapper;
using Microsoft.Data.SqlClient;
using Trees.Models;

namespace Trees.Services
{
    public class DatabaseService
    {
        private readonly string _connectionString = "Data Source=christmastreessofijowka.database.windows.net;Initial Catalog=Trees;User ID=mikolaj;Password=Qwerty123!;Connect Timeout=30;Encrypt=True;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False";

        public DatabaseService(string connectionString)
        {
            _connectionString = connectionString;
        }


        public async Task<IEnumerable<Gatunek>> GetGatunkiAsync()
        {
            using var connection = new SqlConnection(_connectionString);
            return await connection.QueryAsync<Gatunek>("SELECT * FROM Gatunek");
        }

        public async Task<IEnumerable<Wielkosc>> GetWielkosciAsync()
        {
            using var connection = new SqlConnection(_connectionString);
            return await connection.QueryAsync<Wielkosc>("SELECT * FROM Wielkosc");
        }

        public async Task<IEnumerable<Sprzedaz>> GetSprzedazAsync()
        {
            using var connection = new SqlConnection(_connectionString);
            var sql = "SELECT * FROM Sprzedaz";
            return await connection.QueryAsync<Sprzedaz>(sql);
        }

        public async Task AddSprzedazAsync(Sprzedaz sprzedaz)
        {
            using var connection = new SqlConnection(_connectionString);
            var sql = "INSERT INTO Sprzedaz (GatunekID, WielkoscID, Cena, Ilosc, CalkowitaCena, DataSprzedazy) VALUES (@GatunekID, @WielkoscID, @Cena, @Ilosc, @CalkowitaCena, @DataSprzedazy)";
            await connection.ExecuteAsync(sql, sprzedaz);
        }
        public async Task<IEnumerable<Sprzedaz>> GetSprzedazWithGatunkiAsync()
        {
            using var connection = new SqlConnection(_connectionString);
            var sql = @"
        SELECT s.*, g.NazwaGatunku 
        FROM Sprzedaz s
        INNER JOIN Gatunek g ON s.GatunekID = g.GatunekID";

            return await connection.QueryAsync<Sprzedaz>(sql);
        }

        public void PrintConnectionString()
        {
            Console.WriteLine(_connectionString);
        }
    }
}
