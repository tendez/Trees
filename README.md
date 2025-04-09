# 🌲 Trees – System Zarządzania Sprzedażą Drzewek

## 📋 Przegląd

**Trees** to aplikacja mobilna zbudowana przy użyciu .NET MAUI, zaprojektowana do zarządzania sprzedażą drzewek na stoiskach handlowych. Umożliwia śledzenie sprzedaży, zarządzanie magazynem, generowanie statystyk oraz obsługę różnych poziomów dostępu użytkowników.

---

## 🔑 Funkcje

### 👥 Zarządzanie Użytkownikami

- System logowania z różnymi poziomami dostępu (użytkownik, administrator)
- Bezpieczne przechowywanie haseł z wykorzystaniem SHA-256
- Sesje użytkowników z zapisem preferencji

### 💰 Zarządzanie Sprzedażą

- Dodawanie nowych transakcji sprzedaży
- Edycja i usuwanie istniejących wpisów sprzedaży
- Filtrowanie sprzedaży według daty
- Podgląd własnej sprzedaży dla zalogowanego użytkownika
- Podgląd całkowitej sprzedaży dla stoiska

### 🏷️ Zarządzanie Magazynem

- Śledzenie stanu magazynowego dla różnych gatunków i wielkości drzewek
- Automatyczna aktualizacja stanu magazynowego przy sprzedaży
- Możliwość ręcznego zwiększania/zmniejszania ilości w magazynie

### 🛠️ Panel Administracyjny

- Dostęp do statystyk sprzedaży
- Zarządzanie zwrotami
- Pełny dostęp do magazynu

---

## 🧱 Architektura Aplikacji

### 🗂️ Model Danych

- **Gatunek** – reprezentuje gatunki drzewek (np. Świerk Pospolity, Jodła)
- **Wielkosc** – określa dostępne rozmiary drzewek
- **Stoisko** – reprezentuje punkty sprzedaży
- **Uzytkownicy** – przechowuje dane użytkowników i ich role
- **Sprzedaz** – zawiera informacje o transakcjach sprzedaży
- **Magazyn** – śledzi stan magazynowy dla każdego gatunku i wielkości

### 🧩 Usługi

- **DatabaseService** – centralna usługa do komunikacji z bazą danych SQL Server
  - Implementuje wzorzec repozytorium dla wszystkich operacji CRUD

### 🖼️ Widoki

- `LoginPage` – ekran logowania
- `MainPage` – główny ekran aplikacji
- `AdminView` – panel administracyjny
- `DodajSprzedazPage` – dodawanie nowej sprzedaży
- `WyborStoiskaPage` – wybór stoiska
- `WyborWielkosciPage` – wybór wielkości drzewka
- `ZobaczSprzedazPage` – przegląd sprzedaży
- `MojaSprzedazPage` – przegląd własnej sprzedaży
- `WarehousePage` – zarządzanie magazynem

---

## ⚙️ Technologie

- [.NET MAUI](https://learn.microsoft.com/en-us/dotnet/maui/) – framework do aplikacji wieloplatformowych
- **C#** – język programowania
- **SQL Server** – relacyjna baza danych
- **Dapper** – mikro-ORM do mapowania obiektowo-relacyjnego
- **Microsoft.Extensions.DependencyInjection** – do wstrzykiwania zależności

---

## 🚀 Instalacja

1. **Sklonuj repozytorium**
   ```bash
   git clone https://github.com/twoj-login/trees.git
   ```

2. **Otwórz projekt w Visual Studio 2022 lub nowszym**

3. **Skonfiguruj połączenie z bazą danych** w pliku `DatabaseService.cs`:

   ```csharp
   string _connectionString = "Data Source=192.168.0.106;Initial Catalog=Trees;User ID=sa;Password=12345;Connect Timeout=30;Encrypt=True;Trust Server Certificate=True;Application Intent=ReadWrite;Multi Subnet Failover=False";
   ```

   🔧 Dostosuj parametry do własnego środowiska.

4. **Uruchom aplikację** na wybranej platformie (Android, iOS, Windows)

---

## 🔄 Przepływ Pracy

1. Użytkownik loguje się do aplikacji
2. Wybiera stoisko, na którym pracuje
3. Dodaje nową sprzedaż (gatunek, wielkość, cena)
4. Może przeglądać swoją sprzedaż lub całkowitą sprzedaż stoiska
5. Administrator uzyskuje dostęp do pełnych funkcji zarządzania

---

## 🔐 Bezpieczeństwo

- Hasła hashowane z użyciem SHA-256
- Lokalne przechowywanie sesji użytkowników
- Role użytkowników kontrolują dostęp do funkcji

---

## 📌 Status

Projekt aktywnie rozwijany.  
Możliwe przyszłe rozszerzenia:
- Wersja offline
- Eksport danych do CSV
- Powiadomienia o niskim stanie magazynowym
