
                SELECT s.*, g.NazwaGatunku, w.OpisWielkosc, u.Login, st.StoiskoNazwa 
                FROM Sprzedaz s
                INNER JOIN Gatunek g ON s.GatunekID = g.GatunekID
                INNER JOIN Wielkosc w ON s.WielkoscID = w.WielkoscID
                INNER JOIN Uzytkownicy u ON s.UserID = u.UserID
                INNER JOIN Stoisko st ON s.StoiskoID = st.StoiskoID