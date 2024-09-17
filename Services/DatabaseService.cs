﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Maui.ApplicationModel.DataTransfer;
using Microsoft.Maui.Controls;
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
        public async Task<Stoisko> GetStoiskoByIdAsync(int stoiskoId)
        {
            using var connection = new SqlConnection(_connectionString);
            var sql = "SELECT * FROM Stoisko WHERE StoiskoID = @StoiskoID";
            return await connection.QueryFirstOrDefaultAsync<Stoisko>(sql, new { StoiskoID = stoiskoId });
        }
        
        public async Task<IEnumerable<Gatunek>> GetGatunkiAsync()
        {
            using var connection = new SqlConnection(_connectionString);
            return await connection.QueryAsync<Gatunek>("SELECT * FROM Gatunek");
        }

        public async Task<IEnumerable<Uzytkownicy>> GetUzytkownicyAsync()
        {
            using var connection = new SqlConnection(_connectionString);
            return await connection.QueryAsync<Uzytkownicy>("SELECT * FROM Uzytkownicy");
        }

        public async Task<IEnumerable<Wielkosc>> GetWielkosciAsync()
        {
            using var connection = new SqlConnection(_connectionString);
            return await connection.QueryAsync<Wielkosc>("SELECT * FROM Wielkosc");
        }

        public async Task<IEnumerable<Stoisko>> GetStoiskaAsync()
        {
            using var connection = new SqlConnection(_connectionString);
            return await connection.QueryAsync<Stoisko>("SELECT * FROM Stoisko");
        }

        public async Task<IEnumerable<Sprzedaz>> GetSprzedazeAsync()
        {
            using var connection = new SqlConnection(_connectionString);
            var sql = "SELECT * FROM Sprzedaz";
            return await connection.QueryAsync<Sprzedaz>(sql);
        }

        public async Task<IEnumerable<Magazyn>> GetMagazynyAsync()
        {
            using var connection = new SqlConnection(_connectionString);
            return await connection.QueryAsync<Magazyn>("SELECT * FROM Magazyn");
        }
        public async Task UpdateSprzedazAsync(Sprzedaz sprzedaz)
        {
            using var connection = new SqlConnection(_connectionString);
            var query = "UPDATE Sprzedaz SET Cena = @Cena, Ilosc = @Ilosc WHERE SprzedazID = @SprzedazID";
            var command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@Cena", sprzedaz.Cena);
            command.Parameters.AddWithValue("@Ilosc", sprzedaz.Ilosc);
            command.Parameters.AddWithValue("@SprzedazID", sprzedaz.SprzedazID);

            connection.Open();
            await command.ExecuteNonQueryAsync();
        }

      public async Task DeleteSprzedazAsync(int sprzedazId)
{
    var sql = @"DELETE FROM Sprzedaz WHERE SprzedazID = @SprzedazID";

    using (var connection = new SqlConnection(_connectionString))
    {
            await connection.ExecuteAsync(sql, new { SprzedazID = sprzedazId });
        
       
    }
}


        public async Task AddSprzedazAsync(Sprzedaz sprzedaz)
        {
            using var connection = new SqlConnection(_connectionString);
            var sql = @"INSERT INTO Sprzedaz (UserID, GatunekID, WielkoscID, Cena, Ilosc, CalkowitaCena, DataSprzedazy, StoiskoID) 
                        VALUES (@UserID, @GatunekID, @WielkoscID, @Cena, @Ilosc, @CalkowitaCena, @DataSprzedazy, @StoiskoID)";
            await connection.ExecuteAsync(sql, sprzedaz);
        }

        public async Task<Magazyn> GetMagazynAsync(int gatunekId, int wielkoscId, int stoiskoId)
        {
            using var connection = new SqlConnection(_connectionString);
            var sql = @"SELECT * FROM Magazyn WHERE GatunekID = @GatunekID AND WielkoscID = @WielkoscID AND StoiskoID = @StoiskoID";
            return await connection.QueryFirstOrDefaultAsync<Magazyn>(sql, new { GatunekID = gatunekId, WielkoscID = wielkoscId, StoiskoID = stoiskoId });
        }

        public async Task UpdateMagazynAsync(Magazyn magazyn)
        {
            using var connection = new SqlConnection(_connectionString);
            var sql = @"UPDATE Magazyn SET Ilosc = @Ilosc WHERE MagazynID = @MagazynID";
            await connection.ExecuteAsync(sql, new { Ilosc = magazyn.Ilosc, MagazynID = magazyn.MagazynID });
        }


        public async Task<IEnumerable<Sprzedaz>> GetSprzedazWithDetailsAsync(Stoisko stoisko)
        {
            using var connection = new SqlConnection(_connectionString);
            var sql = @"
        SELECT s.Cena,s.CalkowitaCena,s.DataSprzedazy,s.Ilosc, g.NazwaGatunku, w.OpisWielkosci, u.Login, st.StoiskoNazwa,s.sprzedazID,s.gatunekID,s.stoiskoID,s.WielkoscID 
        FROM Sprzedaz s
        INNER JOIN Gatunek g ON s.GatunekID = g.GatunekID
        INNER JOIN Wielkosc w ON s.WielkoscID = w.WielkoscID
        INNER JOIN Uzytkownicy u ON s.UserID = u.UserID
        INNER JOIN Stoisko st ON s.StoiskoID = st.StoiskoID
        WHERE s.StoiskoID = @StoiskoID";


       
         
            var parameters = new { StoiskoID = stoisko.StoiskoID };

            return await connection.QueryAsync<Sprzedaz>(sql, parameters);
        }


    

       
    }
}
