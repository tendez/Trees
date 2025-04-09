# 🌲 Trees – Tree Sales Management System

## 📋 Overview

**Trees** is a mobile application built with .NET MAUI, designed to manage the sale of trees at market stands. It allows tracking of sales, inventory management, statistics generation, and supports multiple user roles with different access levels.

---

## 🔑 Features

### 👥 User Management

- Login system with role-based access (user, administrator)
- Secure password storage using SHA-256
- User sessions with preference persistence

### 💰 Sales Management

- Add new sales transactions
- Edit and delete existing sales records
- Filter sales by date
- View personal sales (per logged-in user)
- View total sales per stand

### 🏷️ Inventory Management

- Track stock levels for different tree types and sizes
- Automatic inventory updates after sales
- Manually increase/decrease inventory levels

### 🛠️ Admin Panel

- Access to overall sales statistics
- Manage product returns
- Full access to inventory across stands

---

## 🧱 Application Architecture

### 🗂️ Data Models

- **Gatunek (Species)** – represents tree types (e.g., Spruce, Fir)
- **Wielkosc (Size)** – available tree sizes
- **Stoisko (Stand)** – represents a sales location
- **Uzytkownicy (Users)** – user data and roles
- **Sprzedaz (Sales)** – sales transaction records
- **Magazyn (Inventory)** – inventory tracking by species and size

### 🧩 Services

- **DatabaseService** – central service for communicating with SQL Server
  - Implements repository pattern for all CRUD operations

### 🖼️ Views

- `LoginPage` – login screen
- `MainPage` – main dashboard
- `AdminView` – administrator panel
- `DodajSprzedazPage` – add new sales entry
- `WyborStoiskaPage` – select sales stand
- `WyborWielkosciPage` – select tree size
- `ZobaczSprzedazPage` – view all sales
- `MojaSprzedazPage` – view personal sales
- `WarehousePage` – inventory management

---

## ⚙️ Technologies Used

- [.NET MAUI](https://learn.microsoft.com/en-us/dotnet/maui/) – cross-platform application framework
- **C#** – programming language
- **SQL Server** – relational database
- **Dapper** – lightweight ORM for database access
- **Microsoft.Extensions.DependencyInjection** – dependency injection support

---

## 🚀 Installation

1. **Clone the repository**
   ```bash
   git clone https://github.com/your-username/trees.git
   ```

2. **Open the solution** in Visual Studio 2022 or newer.

3. **Configure the database connection** in `DatabaseService.cs`:
   ```csharp
   string _connectionString = "";
   ```
   ⚠️ Update the connection string parameters according to your environment.

4. **Run the application** on your target platform (Android, iOS, or Windows).

---

## 🔄 Workflow

1. The user logs in to the app.
2. Selects the stand they are assigned to.
3. Adds new sales by selecting species, size, and price.
4. Views their personal sales or total sales for the stand.
5. Admins get access to extended management features.

---

## 🔐 Security

- Passwords are hashed using SHA-256.
- User sessions are stored locally.
- Role-based access control ensures proper permissions.

---

## 📌 Project Status

Actively developed.  
Planned future features:

- Offline mode
- Export sales data to CSV
- Low stock notifications
