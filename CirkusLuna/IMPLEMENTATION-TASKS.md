# Cirkus Luna - Implementation Tasks

**Generated from:** MVP Implementation Plan  
**Target:** 1st Semester Exam-Ready System  
**Timeline:** 4 weeks  
**Strict MVP Scope:** 6 models, 2 services, Console App, Razor Pages

---

## Task Organization

**Critical (⭐):** Must complete for exam  
**High Priority:** Important for functioning system  
**Optional:** Nice-to-have if time permits

---

## Group 1: Solution Setup (Day 1)

### Task 1.1: Create Visual Studio Solution ⭐
**Project:** N/A  
**Location:** Root  
**Action:**
1. Open Visual Studio 2022
2. Create new Blank Solution
3. Name: `CirkusLuna`
4. Save location: Choose appropriate directory
5. Create solution folder structure

**Deliverable:** `CirkusLuna.sln` file exists

---

### Task 1.2: Create Core Class Library ⭐
**Project:** CirkusLuna.Core  
**Location:** Solution root  
**Action:**
1. Right-click solution → Add → New Project
2. Select "Class Library"
3. Name: `CirkusLuna.Core`
4. Framework: .NET 8.0
5. Delete default `Class1.cs`

**Deliverable:** `CirkusLuna.Core.csproj` compiles

---

### Task 1.3: Create Console App ⭐
**Project:** CirkusLuna.ConsoleApp  
**Location:** Solution root  
**Action:**
1. Add New Project → Console App
2. Name: `CirkusLuna.ConsoleApp`
3. Framework: .NET 8.0
4. Add project reference to `CirkusLuna.Core`

**Deliverable:** Console app runs and references Core

---

### Task 1.4: Create Razor Pages Web App ⭐
**Project:** CirkusLuna.Web  
**Location:** Solution root  
**Action:**
1. Add New Project → ASP.NET Core Web App (Razor Pages)
2. Name: `CirkusLuna.Web`
3. Framework: .NET 8.0
4. Authentication: None
5. HTTPS: Yes
6. Add project reference to `CirkusLuna.Core`

**Deliverable:** Web app runs (F5) and references Core

---

### Task 1.5: Create Folder Structure in Core ⭐
**Project:** CirkusLuna.Core  
**Location:** Project root  
**Action:**
Create these folders:
- `Models/`
- `Interfaces/`
- `Repositories/`
- `Services/`
- `Exceptions/`
- `SeedData/`

**Deliverable:** All folders visible in Solution Explorer

---

### Task 1.6: Create README Skeleton
**Project:** N/A  
**Location:** Solution root  
**Action:**
Create `README.md` with sections:
- Project title
- How to run Console App
- How to run Web App
- Project structure
- Key features

**Deliverable:** `README.md` exists with basic structure

---

## Group 2: Core Models (Day 1-2)

### Task 2.1: Create By Model ⭐
**Project:** CirkusLuna.Core  
**Location:** `Models/By.cs`  
**Action:**
```csharp
namespace CirkusLuna.Core.Models
{
    public class By
    {
        public int Id { get; set; }
        public string Navn { get; set; } = string.Empty;
        public string Region { get; set; } = string.Empty;
    }
}
```

**Deliverable:** By.cs compiles

---

### Task 2.2: Create Artist Model ⭐
**Project:** CirkusLuna.Core  
**Location:** `Models/Artist.cs`  
**Action:**
```csharp
namespace CirkusLuna.Core.Models
{
    public class Artist
    {
        public int Id { get; set; }
        public string Navn { get; set; } = string.Empty;
        public string Specialitet { get; set; } = string.Empty;
        public List<Forestilling> Forestillinger { get; set; } = new();
    }
}
```

**Deliverable:** Artist.cs compiles

---

### Task 2.3: Create Kunde Model ⭐
**Project:** CirkusLuna.Core  
**Location:** `Models/Kunde.cs`  
**Action:**
```csharp
namespace CirkusLuna.Core.Models
{
    public class Kunde
    {
        public int Id { get; set; }
        public string Navn { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Telefon { get; set; } = string.Empty;
        public List<Reservation> Reservationer { get; set; } = new();
    }
}
```

**Deliverable:** Kunde.cs compiles

---

### Task 2.4: Create Billettype Enum ⭐
**Project:** CirkusLuna.Core  
**Location:** `Models/Billettype.cs`  
**Action:**
```csharp
namespace CirkusLuna.Core.Models
{
    public enum Billettype
    {
        Normal = 0,
        Barn = 1,
        VIP = 2
    }
}
```

**Deliverable:** Billettype.cs compiles

---

### Task 2.5: Create BillettypePris Helper ⭐
**Project:** CirkusLuna.Core  
**Location:** `Models/BillettypePris.cs`  
**Action:**
```csharp
namespace CirkusLuna.Core.Models
{
    public static class BillettypePris
    {
        public static decimal GetPris(Billettype type)
        {
            return type switch
            {
                Billettype.Normal => 120m,
                Billettype.Barn => 80m,
                Billettype.VIP => 250m,
                _ => 0m
            };
        }
    }
}
```

**Deliverable:** BillettypePris.cs compiles, GetPris returns correct prices

---

### Task 2.6: Create Forestilling Model ⭐
**Project:** CirkusLuna.Core  
**Location:** `Models/Forestilling.cs`  
**Action:**
```csharp
namespace CirkusLuna.Core.Models
{
    public class Forestilling
    {
        public int Id { get; set; }
        public string Titel { get; set; } = string.Empty;
        public DateTime Dato { get; set; }
        public TimeSpan Tidspunkt { get; set; }
        
        public int ById { get; set; }
        public By By { get; set; } = null!;
        
        public int TotalKapacitet { get; set; } = 150;
        public int VIPKapacitet { get; set; } = 10;
        
        public List<Artist> Artister { get; set; } = new();
        public List<Reservation> Reservationer { get; set; } = new();
        
        // Calculated properties
        public int AntalReserveredePladser => 
            Reservationer?.Sum(r => r.AntalBilletter) ?? 0;
        public int LedigePladser => TotalKapacitet - AntalReserveredePladser;
        
        public int AntalReserveredeVIPPladser => 
            Reservationer?.Where(r => r.Billettype == Billettype.VIP)
                         .Sum(r => r.AntalBilletter) ?? 0;
        public int LedigeVIPPladser => VIPKapacitet - AntalReserveredeVIPPladser;
    }
}
```

**Deliverable:** Forestilling.cs compiles with capacity calculations

---

### Task 2.7: Create Reservation Model ⭐
**Project:** CirkusLuna.Core  
**Location:** `Models/Reservation.cs`  
**Action:**
```csharp
namespace CirkusLuna.Core.Models
{
    public class Reservation
    {
        public int Id { get; set; }
        
        public int KundeId { get; set; }
        public Kunde Kunde { get; set; } = null!;
        
        public int ForestillingId { get; set; }
        public Forestilling Forestilling { get; set; } = null!;
        
        public int AntalBilletter { get; set; }
        public Billettype Billettype { get; set; }
        public DateTime ReservationsDato { get; set; }
        
        // Calculated property
        public decimal TotalPris => BillettypePris.GetPris(Billettype) * AntalBilletter;
    }
}
```

**Deliverable:** Reservation.cs compiles with TotalPris calculation

---

## Group 3: Repository Interfaces (Day 2)

### Task 3.1: Create Generic IRepository Interface ⭐
**Project:** CirkusLuna.Core  
**Location:** `Interfaces/IRepository.cs`  
**Action:**
```csharp
namespace CirkusLuna.Core.Interfaces
{
    public interface IRepository<T> where T : class
    {
        List<T> GetAll();
        T? GetById(int id);
        void Add(T entity);
        void Update(T entity);
        void Delete(int id);
    }
}
```

**Deliverable:** IRepository<T> compiles

---

### Task 3.2: Create IByRepository Interface ⭐
**Project:** CirkusLuna.Core  
**Location:** `Interfaces/IByRepository.cs`  
**Action:**
```csharp
namespace CirkusLuna.Core.Interfaces
{
    public interface IByRepository : IRepository<By>
    {
        List<By> GetCitiesSortedAlphabetically();
        By? GetByName(string navn);
    }
}
```

**Deliverable:** IByRepository compiles

---

### Task 3.3: Create IArtistRepository Interface ⭐
**Project:** CirkusLuna.Core  
**Location:** `Interfaces/IArtistRepository.cs`  
**Action:**
```csharp
namespace CirkusLuna.Core.Interfaces
{
    public interface IArtistRepository : IRepository<Artist>
    {
        // Standard CRUD only
    }
}
```

**Deliverable:** IArtistRepository compiles

---

### Task 3.4: Create IKundeRepository Interface ⭐
**Project:** CirkusLuna.Core  
**Location:** `Interfaces/IKundeRepository.cs`  
**Action:**
```csharp
namespace CirkusLuna.Core.Interfaces
{
    public interface IKundeRepository : IRepository<Kunde>
    {
        Kunde? GetByEmail(string email);
    }
}
```

**Deliverable:** IKundeRepository compiles

---

### Task 3.5: Create IForestillingRepository Interface ⭐
**Project:** CirkusLuna.Core  
**Location:** `Interfaces/IForestillingRepository.cs`  
**Action:**
```csharp
namespace CirkusLuna.Core.Interfaces
{
    public interface IForestillingRepository : IRepository<Forestilling>
    {
        List<Forestilling> SearchByCity(string byNavn);
        List<Forestilling> GetByDate(DateTime dato);
        List<Forestilling> GetUpcomingPerformances();
        List<Forestilling> GetPerformancesInCity(int byId);
    }
}
```

**Deliverable:** IForestillingRepository compiles

---

### Task 3.6: Create IReservationRepository Interface ⭐
**Project:** CirkusLuna.Core  
**Location:** `Interfaces/IReservationRepository.cs`  
**Action:**
```csharp
namespace CirkusLuna.Core.Interfaces
{
    public interface IReservationRepository : IRepository<Reservation>
    {
        List<Reservation> GetByForestillingId(int forestillingId);
        List<Reservation> GetByKundeId(int kundeId);
    }
}
```

**Deliverable:** IReservationRepository compiles

---

## Group 4: Repository Implementations (Day 2-3)

### Task 4.1: Implement InMemoryByRepository with Bubble Sort ⭐
**Project:** CirkusLuna.Core  
**Location:** `Repositories/InMemoryByRepository.cs`  
**Action:**
Implement with:
- Private `List<By> _byer`
- Standard CRUD methods
- `GetByName(string navn)` method
- **CRITICAL:** `GetCitiesSortedAlphabetically()` using bubble sort (manual implementation, NOT LINQ OrderBy)

**Code:**
```csharp
public List<By> GetCitiesSortedAlphabetically()
{
    List<By> sortedCities = _byer.ToList();
    int n = sortedCities.Count;
    
    // Bubble sort implementation
    for (int i = 0; i < n - 1; i++)
    {
        for (int j = 0; j < n - i - 1; j++)
        {
            if (string.Compare(sortedCities[j].Navn, 
                              sortedCities[j + 1].Navn, 
                              StringComparison.OrdinalIgnoreCase) > 0)
            {
                By temp = sortedCities[j];
                sortedCities[j] = sortedCities[j + 1];
                sortedCities[j + 1] = temp;
            }
        }
    }
    
    return sortedCities;
}
```

**Deliverable:** InMemoryByRepository compiles, bubble sort works correctly

---

### Task 4.2: Implement InMemoryForestillingRepository with Manual Search ⭐
**Project:** CirkusLuna.Core  
**Location:** `Repositories/InMemoryForestillingRepository.cs`  
**Action:**
Implement with:
- Private `List<Forestilling> _forestillinger`
- Standard CRUD methods
- **CRITICAL:** `SearchByCity(string byNavn)` using manual loop (NOT just LINQ Where)
- `GetByDate(DateTime dato)` method
- `GetUpcomingPerformances()` method
- `GetPerformancesInCity(int byId)` method

**Code for SearchByCity:**
```csharp
public List<Forestilling> SearchByCity(string byNavn)
{
    List<Forestilling> results = new List<Forestilling>();
    
    // Manual loop-based search (not just LINQ)
    foreach (var forestilling in _forestillinger)
    {
        if (forestilling.By != null && 
            forestilling.By.Navn.Equals(byNavn, StringComparison.OrdinalIgnoreCase))
        {
            results.Add(forestilling);
        }
    }
    
    return results;
}
```

**Deliverable:** InMemoryForestillingRepository compiles, manual search works

---

### Task 4.3: Implement InMemoryArtistRepository ⭐
**Project:** CirkusLuna.Core  
**Location:** `Repositories/InMemoryArtistRepository.cs`  
**Action:**
Standard implementation with:
- Private `List<Artist> _artister`
- Standard CRUD methods only
- No special algorithms needed

**Deliverable:** InMemoryArtistRepository compiles

---

### Task 4.4: Implement InMemoryKundeRepository ⭐
**Project:** CirkusLuna.Core  
**Location:** `Repositories/InMemoryKundeRepository.cs`  
**Action:**
Standard implementation with:
- Private `List<Kunde> _kunder`
- Standard CRUD methods
- `GetByEmail(string email)` method

**Deliverable:** InMemoryKundeRepository compiles

---

### Task 4.5: Implement InMemoryReservationRepository ⭐
**Project:** CirkusLuna.Core  
**Location:** `Repositories/InMemoryReservationRepository.cs`  
**Action:**
Standard implementation with:
- Private `List<Reservation> _reservationer`
- Standard CRUD methods
- `GetByForestillingId(int forestillingId)` method
- `GetByKundeId(int kundeId)` method

**Deliverable:** InMemoryReservationRepository compiles

---

## Group 5: Seed Data (Day 3)

### Task 5.1: Create DataSeeder Class ⭐
**Project:** CirkusLuna.Core  
**Location:** `SeedData/DataSeeder.cs`  
**Action:**
Create static class with methods:
- `SeedAll()` - calls all seed methods
- `SeedCities()` - 8 Danish cities
- `SeedArtists()` - 5 artists
- `SeedCustomers()` - 3 test customers
- `SeedPerformances()` - 10-12 performances

**Deliverable:** DataSeeder.cs compiles

---

### Task 5.2: Seed Danish Cities ⭐
**Project:** CirkusLuna.Core  
**Location:** `SeedData/DataSeeder.cs` → `SeedCities()`  
**Action:**
Add 8 cities:
- København (Sjælland)
- Aarhus (Jylland)
- Odense (Fyn)
- Aalborg (Jylland)
- Esbjerg (Jylland)
- Roskilde (Sjælland)
- Kolding (Jylland)
- Horsens (Jylland)

**Deliverable:** 8 cities seeded correctly

---

### Task 5.3: Seed Artists ⭐
**Project:** CirkusLuna.Core  
**Location:** `SeedData/DataSeeder.cs` → `SeedArtists()`  
**Action:**
Add 5 artists:
- Lars Henriksen (Trapez)
- Maria Sørensen (Jonglør)
- Peter Nielsen (Klovn)
- Anna Andersen (Akrobat)
- Thomas Jensen (Tryllekunstner)

**Deliverable:** 5 artists seeded correctly

---

### Task 5.4: Seed Test Customers ⭐
**Project:** CirkusLuna.Core  
**Location:** `SeedData/DataSeeder.cs` → `SeedCustomers()`  
**Action:**
Add 3 customers:
- Jens Hansen (jens@mail.dk, 12345678)
- Anne Jensen (anne@mail.dk, 23456789)
- Morten Olsen (morten@mail.dk, 34567890)

**Deliverable:** 3 customers seeded correctly

---

### Task 5.5: Seed Performances ⭐
**Project:** CirkusLuna.Core  
**Location:** `SeedData/DataSeeder.cs` → `SeedPerformances()`  
**Action:**
Add 10-12 performances:
- Spread across 8 cities
- Mix of dates (at least 1 past, rest future)
- Various times (15:00, 18:00, 19:00, 20:00)
- Assign 2-4 artists per performance
- Include enough data to test capacity limits

**Example:**
- København, 2026-06-01 19:00, "Den Store Cirkus Show" (3 artists)
- Aarhus, 2026-06-05 18:00, "Magisk Aften" (2 artists)
- Include one past performance: 2026-05-01 for testing rejection

**Deliverable:** 10-12 performances seeded with variety

---

### Task 5.6: Add Sample Reservations for Testing
**Project:** CirkusLuna.Core  
**Location:** `SeedData/DataSeeder.cs` → `SeedReservations()`  
**Action:**
Add 3-5 test reservations:
- Mix of Normal, Barn, VIP tickets
- One performance with 145 seats taken (for capacity testing)
- One performance with 9 VIP seats taken (for VIP testing)

**Deliverable:** Sample reservations seeded for testing

---

## Group 6: Custom Exceptions (Day 3)

### Task 6.1: Create ReservationFullException ⭐
**Project:** CirkusLuna.Core  
**Location:** `Exceptions/ReservationFullException.cs`  
**Action:**
```csharp
namespace CirkusLuna.Core.Exceptions
{
    public class ReservationFullException : Exception
    {
        public ReservationFullException(string message) : base(message) { }
    }
}
```

**Deliverable:** ReservationFullException compiles

---

### Task 6.2: Create VIPCapacityExceededException ⭐
**Project:** CirkusLuna.Core  
**Location:** `Exceptions/VIPCapacityExceededException.cs`  
**Action:**
```csharp
namespace CirkusLuna.Core.Exceptions
{
    public class VIPCapacityExceededException : Exception
    {
        public VIPCapacityExceededException(string message) : base(message) { }
    }
}
```

**Deliverable:** VIPCapacityExceededException compiles

---

### Task 6.3: Create PastPerformanceException ⭐
**Project:** CirkusLuna.Core  
**Location:** `Exceptions/PastPerformanceException.cs`  
**Action:**
```csharp
namespace CirkusLuna.Core.Exceptions
{
    public class PastPerformanceException : Exception
    {
        public PastPerformanceException(string message) : base(message) { }
    }
}
```

**Deliverable:** PastPerformanceException compiles

---

## Group 7: Service Layer (Day 4)

### Task 7.1: Create IForestillingService Interface ⭐
**Project:** CirkusLuna.Core  
**Location:** `Interfaces/IForestillingService.cs`  
**Action:**
```csharp
namespace CirkusLuna.Core.Interfaces
{
    public interface IForestillingService
    {
        List<Forestilling> GetAllForestillinger();
        Forestilling? GetForestillingById(int id);
        List<Forestilling> SearchByCity(string byNavn);
        List<Forestilling> GetUpcomingForestillinger();
        List<By> GetCitiesSortedAlphabetically();
    }
}
```

**Deliverable:** IForestillingService compiles

---

### Task 7.2: Implement ForestillingService ⭐
**Project:** CirkusLuna.Core  
**Location:** `Services/ForestillingService.cs`  
**Action:**
Implement service with:
- Constructor injection of IForestillingRepository and IByRepository
- Implement all interface methods
- Delegate to repositories (no business logic duplication)

**Deliverable:** ForestillingService compiles and works

---

### Task 7.3: Create IReservationService Interface ⭐
**Project:** CirkusLuna.Core  
**Location:** `Interfaces/IReservationService.cs`  
**Action:**
```csharp
namespace CirkusLuna.Core.Interfaces
{
    public interface IReservationService
    {
        Reservation CreateReservation(int kundeId, int forestillingId, 
                                     int antalBilletter, Billettype billettype);
        bool CanReserve(int forestillingId, int antalBilletter, Billettype billettype);
        List<Reservation> GetReservationsByKunde(int kundeId);
        decimal CalculateTotalPrice(int antalBilletter, Billettype billettype);
    }
}
```

**Deliverable:** IReservationService compiles

---

### Task 7.4: Implement ReservationService - Basic Structure ⭐
**Project:** CirkusLuna.Core  
**Location:** `Services/ReservationService.cs`  
**Action:**
Create service class with:
- Constructor injection of IReservationRepository, IForestillingRepository, IKundeRepository
- Empty method stubs for all interface methods

**Deliverable:** ReservationService compiles

---

### Task 7.5: Implement CreateReservation with Validations ⭐
**Project:** CirkusLuna.Core  
**Location:** `Services/ReservationService.cs` → `CreateReservation()`  
**Action:**
Implement method with these validations:
1. Check forestilling exists
2. Check performance is in the future (throw PastPerformanceException)
3. Check VIP capacity if VIP tickets (throw VIPCapacityExceededException)
4. Check total capacity if normal tickets (throw ReservationFullException)
5. Check kunde exists
6. Create and save reservation

**Deliverable:** CreateReservation works with all validations

---

### Task 7.6: Implement CanReserve Method
**Project:** CirkusLuna.Core  
**Location:** `Services/ReservationService.cs` → `CanReserve()`  
**Action:**
Implement method that returns bool:
- Check if forestilling exists
- Check if date is future
- Check if sufficient capacity (VIP or normal)

**Deliverable:** CanReserve works correctly

---

### Task 7.7: Implement CalculateTotalPrice Method
**Project:** CirkusLuna.Core  
**Location:** `Services/ReservationService.cs` → `CalculateTotalPrice()`  
**Action:**
```csharp
public decimal CalculateTotalPrice(int antalBilletter, Billettype billettype)
{
    return BillettypePris.GetPris(billettype) * antalBilletter;
}
```

**Deliverable:** Price calculation works (Normal=120, Barn=80, VIP=250)

---

## Group 8: Console App (Day 4-5)

### Task 8.1: Setup Console App Program.cs ⭐
**Project:** CirkusLuna.ConsoleApp  
**Location:** `Program.cs`  
**Action:**
In Main method:
1. Instantiate all repositories
2. Call DataSeeder.SeedAll()
3. Instantiate both services
4. Call MainMenu.Run()

**Deliverable:** Console app starts and seeds data

---

### Task 8.2: Create MainMenu Class Structure ⭐
**Project:** CirkusLuna.ConsoleApp  
**Location:** `Menus/MainMenu.cs`  
**Action:**
Create static class with:
- `Run()` method accepting services and repositories
- While loop with menu display
- Switch statement for 11 options (1-10 + 0 to exit)

**Deliverable:** Menu displays and loops

---

### Task 8.3: Implement Option 1 - Show All Performances ⭐
**Project:** CirkusLuna.ConsoleApp  
**Location:** `Menus/MainMenu.cs`  
**Action:**
Display all performances with:
- Date, time, city, title, available seats

**Deliverable:** Option 1 displays performances

---

### Task 8.4: Implement Option 2 - Search by City (Self-Written Algorithm) ⭐
**Project:** CirkusLuna.ConsoleApp  
**Location:** `Menus/MainMenu.cs`  
**Action:**
1. Prompt user for city name
2. Call service.SearchByCity()
3. Display results
4. Show message if no results
5. **Add comment:** "// Uses self-written search algorithm in repository"

**Deliverable:** Option 2 searches and displays results

---

### Task 8.5: Implement Option 3 - Search by Date
**Project:** CirkusLuna.ConsoleApp  
**Location:** `Menus/MainMenu.cs`  
**Action:**
1. Prompt user for date
2. Parse date
3. Call repository method
4. Display results

**Deliverable:** Option 3 searches by date

---

### Task 8.6: Implement Option 4 - Show Cities Sorted (Self-Written Algorithm) ⭐
**Project:** CirkusLuna.ConsoleApp  
**Location:** `Menus/MainMenu.cs`  
**Action:**
1. Call service.GetCitiesSortedAlphabetically()
2. Display cities in order
3. **Add comment:** "// Uses self-written bubble sort algorithm in repository"

**Deliverable:** Option 4 displays sorted cities

---

### Task 8.7: Implement Option 5 - Show All Artists
**Project:** CirkusLuna.ConsoleApp  
**Location:** `Menus/MainMenu.cs`  
**Action:**
Display all artists with:
- Name, specialty

**Deliverable:** Option 5 displays artists

---

### Task 8.8: Implement Option 6 - Create Customer
**Project:** CirkusLuna.ConsoleApp  
**Location:** `Menus/MainMenu.cs`  
**Action:**
1. Prompt for name, email, phone
2. Create Kunde object
3. Call repository.Add()
4. Display confirmation

**Deliverable:** Option 6 creates customers

---

### Task 8.9: Implement Option 7 - Create Reservation ⭐
**Project:** CirkusLuna.ConsoleApp  
**Location:** `Menus/MainMenu.cs`  
**Action:**
1. Display upcoming performances
2. Prompt for forestilling ID
3. Prompt for kunde ID
4. Prompt for antal billetter
5. Prompt for billettype (0=Normal, 1=Barn, 2=VIP)
6. Call reservationService.CreateReservation()
7. Display success with total price
8. Handle exceptions and display error messages

**Deliverable:** Option 7 creates reservations with validation

---

### Task 8.10: Implement Option 8 - Test Normal Capacity Limit ⭐
**Project:** CirkusLuna.ConsoleApp  
**Location:** `Menus/MainMenu.cs`  
**Action:**
1. Find performance with <5 seats available
2. Try to reserve 10 seats
3. Catch ReservationFullException
4. Display error message
5. Show that validation works

**Deliverable:** Option 8 demonstrates capacity validation

---

### Task 8.11: Implement Option 9 - Test VIP Capacity Limit ⭐
**Project:** CirkusLuna.ConsoleApp  
**Location:** `Menus/MainMenu.cs`  
**Action:**
1. Find performance with <2 VIP seats
2. Try to reserve 5 VIP tickets
3. Catch VIPCapacityExceededException
4. Display error message
5. Show that VIP validation works

**Deliverable:** Option 9 demonstrates VIP capacity validation

---

### Task 8.12: Implement Option 10 - Test Past Performance Rejection ⭐
**Project:** CirkusLuna.ConsoleApp  
**Location:** `Menus/MainMenu.cs`  
**Action:**
1. Find past performance (seeded with date < today)
2. Try to reserve tickets
3. Catch PastPerformanceException
4. Display error message
5. Show that date validation works

**Deliverable:** Option 10 demonstrates past performance validation

---

## Group 9: Razor Pages - Basic Structure (Day 6-7)

### Task 9.1: Register Services in Program.cs ⭐
**Project:** CirkusLuna.Web  
**Location:** `Program.cs`  
**Action:**
Add before `var app = builder.Build();`:
```csharp
// Register repositories as singletons
builder.Services.AddSingleton<IByRepository, InMemoryByRepository>();
builder.Services.AddSingleton<IArtistRepository, InMemoryArtistRepository>();
builder.Services.AddSingleton<IKundeRepository, InMemoryKundeRepository>();
builder.Services.AddSingleton<IForestillingRepository, InMemoryForestillingRepository>();
builder.Services.AddSingleton<IReservationRepository, InMemoryReservationRepository>();

// Register services
builder.Services.AddScoped<IForestillingService, ForestillingService>();
builder.Services.AddScoped<IReservationService, ReservationService>();
```

Add after `var app = builder.Build();`:
```csharp
// Seed data on startup
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var byRepo = services.GetRequiredService<IByRepository>();
    var artistRepo = services.GetRequiredService<IArtistRepository>();
    var kundeRepo = services.GetRequiredService<IKundeRepository>();
    var forestillingRepo = services.GetRequiredService<IForestillingRepository>();
    
    DataSeeder.SeedAll(byRepo, artistRepo, kundeRepo, forestillingRepo);
}
```

**Deliverable:** Services registered, data seeds on app start

---

### Task 9.2: Update _Layout.cshtml ⭐
**Project:** CirkusLuna.Web  
**Location:** `Pages/Shared/_Layout.cshtml`  
**Action:**
Replace default layout with:
- Bootstrap navbar with Danish labels
- Navigation links: Forside, Program, Artister, Reserver
- Simple footer
- Link to site.css

**Deliverable:** Layout has circus branding and Danish navigation

---

### Task 9.3: Create Index.cshtml (Homepage) ⭐
**Project:** CirkusLuna.Web  
**Location:** `Pages/Index.cshtml + Index.cshtml.cs`  
**Action:**
Create page with:
- Hero section: "Velkommen til Cirkus Luna"
- Display next 4 upcoming performances
- CTA buttons: "Se Program", "Reserver Billet"

**Deliverable:** Homepage displays and shows performances

---

### Task 9.4: Create Program.cshtml (List Performances) ⭐
**Project:** CirkusLuna.Web  
**Location:** `Pages/Program.cshtml + Program.cshtml.cs`  
**Action:**
Create page with:
- Page title: "Program"
- Search form (by city)
- Display all upcoming performances in cards
- Show: Date badge, city, title, available seats
- Links to details and reservation

**Deliverable:** Program page lists and searches performances

---

### Task 9.5: Create Detaljer.cshtml (Performance Details) ⭐
**Project:** CirkusLuna.Web  
**Location:** `Pages/Detaljer.cshtml + Detaljer.cshtml.cs`  
**Action:**
Create page with:
- Accept forestillingId as query parameter
- Display full performance details
- List artists performing
- Show available seats (total and VIP)
- "Reserver Billet" button

**Deliverable:** Details page shows performance info

---

### Task 9.6: Create Reserver.cshtml (Reservation Form) ⭐
**Project:** CirkusLuna.Web  
**Location:** `Pages/Reserver.cshtml + Reserver.cshtml.cs`  
**Action:**
Create page with:
- Accept forestillingId as optional query parameter
- Form fields:
  - Select forestilling (dropdown)
  - Kunde information (name, email, phone)
  - Antal billetter (number input)
  - Billettype (radio buttons: Normal, Barn, VIP)
- Display calculated price
- Model validation
- OnPost: Create reservation via service
- Exception handling with user-friendly messages
- Redirect to confirmation on success

**Deliverable:** Reservation form creates reservations

---

### Task 9.7: Create Bekraeftelse.cshtml (Confirmation) ⭐
**Project:** CirkusLuna.Web  
**Location:** `Pages/Bekraeftelse.cshtml + Bekraeftelse.cshtml.cs`  
**Action:**
Create page with:
- Accept reservationId as query parameter
- Display reservation details:
  - Performance info
  - Customer name
  - Number of tickets
  - Ticket type
  - Total price
- Success message
- Links: "Se Program", "Forside"

**Deliverable:** Confirmation page displays reservation

---

### Task 9.8: Create Artister.cshtml (List Artists)
**Project:** CirkusLuna.Web  
**Location:** `Pages/Artister.cshtml + Artister.cshtml.cs`  
**Action:**
Create page with:
- Page title: "Vores Artister"
- Display all artists in cards
- Show: Name, specialty
- Optional: Link to performances they appear in

**Deliverable:** Artists page displays artist info

---

## Group 10: Styling (Day 7-8)

### Task 10.1: Add Bootstrap 5 CSS
**Project:** CirkusLuna.Web  
**Location:** `wwwroot/lib/` or CDN in _Layout.cshtml  
**Action:**
Add Bootstrap 5.3 via:
- Download and place in wwwroot/lib/bootstrap/, OR
- Use CDN link in _Layout.cshtml

**Deliverable:** Bootstrap CSS loads

---

### Task 10.2: Create site.css with Color Variables
**Project:** CirkusLuna.Web  
**Location:** `wwwroot/css/site.css`  
**Action:**
Add CSS:
```css
:root {
    --burgundy: #8B1C1C;
    --gold: #F4C542;
    --cream: #FAF8F3;
    --dark-gray: #2B2B2B;
}

.bg-burgundy {
    background-color: var(--burgundy) !important;
}

.bg-cream {
    background-color: var(--cream);
}
```

**Deliverable:** Color variables defined

---

### Task 10.3: Style Buttons
**Project:** CirkusLuna.Web  
**Location:** `wwwroot/css/site.css`  
**Action:**
Add:
```css
.btn-gold {
    background-color: var(--gold);
    border-color: var(--gold);
    color: var(--dark-gray);
    font-weight: 600;
}

.btn-gold:hover {
    background-color: #E0B035;
    border-color: #E0B035;
}
```

**Deliverable:** Gold buttons styled

---

### Task 10.4: Style Performance Cards
**Project:** CirkusLuna.Web  
**Location:** `wwwroot/css/site.css`  
**Action:**
Add:
```css
.performance-card {
    background-color: var(--cream);
    border: none;
    border-radius: 8px;
    box-shadow: 0 2px 8px rgba(0, 0, 0, 0.1);
    margin-bottom: 1.5rem;
}

.date-badge {
    background-color: #D32F2F;
    color: white;
    padding: 0.5rem 1rem;
    border-radius: 4px;
    font-weight: bold;
}
```

**Deliverable:** Performance cards styled

---

### Task 10.5: Style Hero Section
**Project:** CirkusLuna.Web  
**Location:** `wwwroot/css/site.css`  
**Action:**
Add:
```css
.hero {
    background: linear-gradient(135deg, var(--burgundy) 0%, #4A1F4A 100%);
    color: white;
    padding: 4rem 2rem;
    text-align: center;
}

.hero h1 {
    font-size: 3rem;
    font-weight: bold;
    margin-bottom: 1rem;
}
```

**Deliverable:** Hero section styled

---

### Task 10.6: Ensure Responsive Layout
**Project:** CirkusLuna.Web  
**Location:** All .cshtml pages  
**Action:**
Verify all pages use:
- `.container` or `.container-fluid`
- `.row` and `.col-*` for grids
- Bootstrap responsive utilities
- Test on mobile, tablet, desktop

**Deliverable:** UI is responsive

---

## Group 11: Documentation (Day 9-10)

### Task 11.1: Create Product Backlog ⭐
**Project:** Documentation  
**Location:** `docs/scrum/product-backlog.md`  
**Action:**
Create backlog with 8-10 user stories:
- High priority: Search, reserve, capacity validation, sorted cities
- Medium priority: View artists, performance details
- Low priority: Admin features (not implemented)

**Deliverable:** Product backlog exists

---

### Task 11.2: Write User Stories with Acceptance Criteria ⭐
**Project:** Documentation  
**Location:** `docs/scrum/user-stories.md`  
**Action:**
Write 5-8 user stories with format:
- As a [role]
- I want to [action]
- So that [benefit]
- Acceptance Criteria (3-5 criteria each)

Include:
- Search performances by city
- Reserve tickets
- Validate capacity
- Sort cities alphabetically
- View performance details

**Deliverable:** User stories with acceptance criteria

---

### Task 11.3: Create Domain Model UML Diagram ⭐
**Project:** Documentation  
**Location:** `docs/uml/domain-model.png`  
**Action:**
Create conceptual diagram showing:
- By, Artist, Kunde, Forestilling, Reservation, Billettype
- Relationships (1-to-many, many-to-many)
- Key attributes

**Tool:** Draw.io, PlantUML, or Lucidchart

**Deliverable:** Domain model PNG/PDF

---

### Task 11.4: Create Class Diagram (Focus on Core) ⭐
**Project:** Documentation  
**Location:** `docs/uml/class-diagram.png`  
**Action:**
Create detailed design diagram of CirkusLuna.Core:
- All 6 models with properties and methods
- All repository interfaces
- All service classes
- Relationships and dependencies

**Deliverable:** Class diagram PNG/PDF

---

### Task 11.5: Create Sequence Diagram for Reservation ⭐
**Project:** Documentation  
**Location:** `docs/uml/sequence-diagram-reservation.png`  
**Action:**
Create sequence diagram showing:
- User → Razor Page → ReservationService → Repositories
- CreateReservation flow
- Validation steps
- Exception handling

**Deliverable:** Sequence diagram PNG/PDF

---

### Task 11.6: Write Test Plan
**Project:** Documentation  
**Location:** `docs/test-plan.md`  
**Action:**
Document test cases for:
- Console App: All 10 menu options
- Web App: Key pages and flows
- Business rules: Capacity, VIP, past performance
- Algorithms: Search and sorting

**Deliverable:** Test plan document

---

### Task 11.7: Update README with Complete Instructions ⭐
**Project:** Documentation  
**Location:** `README.md`  
**Action:**
Finalize README with:
- Project description
- Prerequisites (Visual Studio, .NET 8.0)
- How to run Console App
- How to run Web App
- Project structure
- Key features (with mention of self-written algorithms)
- Contributors
- GitHub repository link

**Deliverable:** Complete README

---

### Task 11.8: Add Simply.com Deployment Note
**Project:** Documentation  
**Location:** `docs/deployment.md`  
**Action:**
Create brief guide:
- Note that deployment is optional
- If deploying to Simply.com:
  - Publish as self-contained
  - Upload to web hosting
  - Configure startup
- Note: In-memory data resets on restart (acceptable for school project)

**Deliverable:** Deployment note

---

### Task 11.9: Write Report ⭐
**Project:** Documentation  
**Location:** External (to submit via Wiseflow)  
**Action:**
Write report (max 10 pages + appendices):
1. Introduction (case, purpose)
2. SCRUM methodology (process)
3. System design (architecture, UML diagrams)
4. Implementation (key features, algorithms)
5. Individual contributions (who did what)
6. Conclusion
7. Include section "Ikke implementeret i MVP" with rationale
8. GitHub repository link

**Deliverable:** Report ready for submission

---

### Task 11.10: Push to GitHub and Make Repository Public ⭐
**Project:** Git  
**Location:** GitHub  
**Action:**
1. Initialize git repository (if not already)
2. Create `.gitignore` (exclude bin/, obj/, .vs/)
3. Commit all code and documentation
4. Create GitHub repository
5. Push code
6. Make repository public
7. Copy repository URL for documentation

**Deliverable:** Public GitHub repository with all code

---

## Testing Checklist (Before Demo Day)

### Console App Tests ⭐
- [ ] Option 1: Shows all performances
- [ ] Option 2: Searches by city using self-written algorithm
- [ ] Option 3: Searches by date
- [ ] Option 4: Shows cities sorted alphabetically using bubble sort
- [ ] Option 5: Shows all artists
- [ ] Option 6: Creates customer
- [ ] Option 7: Creates reservation successfully
- [ ] Option 8: Rejects reservation when normal capacity exceeded
- [ ] Option 9: Rejects reservation when VIP capacity exceeded
- [ ] Option 10: Rejects reservation for past performance

### Web App Tests ⭐
- [ ] Homepage loads and displays performances
- [ ] Program page lists performances
- [ ] Search by city works
- [ ] Performance details page shows info
- [ ] Reservation form validates input
- [ ] Reservation creates successfully
- [ ] Confirmation page displays reservation
- [ ] Exception messages display to user
- [ ] Artists page displays artists
- [ ] UI is responsive on mobile

### Code Quality ⭐
- [ ] Self-written search algorithm uses manual loop (not just LINQ)
- [ ] Self-written sorting algorithm uses bubble sort (not .OrderBy())
- [ ] All business logic is in Service layer (not Razor Pages)
- [ ] Capacity limits enforced (150 total, 10 VIP)
- [ ] Custom exceptions thrown correctly
- [ ] Code has comments on business rules
- [ ] Price calculation works (Normal=120, Barn=80, VIP=250)

### Documentation ⭐
- [ ] README has complete setup instructions
- [ ] Domain Model diagram created
- [ ] Class Diagram created (focus on Core)
- [ ] Sequence Diagram created (reservation flow)
- [ ] Product Backlog created
- [ ] User Stories with Acceptance Criteria written
- [ ] Report written (max 10 pages)
- [ ] "Ikke implementeret i MVP" section included
- [ ] GitHub repository is public
- [ ] Repository link in documentation

---

## Summary

**Total Tasks:** ~80 concrete tasks organized into 11 groups

**Critical Path (⭐ tasks):** ~60 must-have tasks for exam

**Timeline:**
- Days 1-3: Foundation (setup, models, repositories, algorithms, seed data)
- Days 4-5: Services and Console App
- Days 6-8: Razor Pages and styling
- Days 9-10: Documentation

**Scope Control:**
- 6 models only (no Person hierarchy, no Medarbejder, no Nyhed, no Plads)
- 2 services only
- In-memory repositories (no database)
- Simple Razor Pages (no admin dashboard unless time permits)
- Focus on self-written algorithms and Console App (exam priority)

**This task list stays strictly within MVP scope and is realistic for a 4-week 1st semester assignment.**
