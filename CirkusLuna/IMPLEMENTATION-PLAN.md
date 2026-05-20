# Cirkus Luna - MVP Implementation Plan

**Version:** 1.0  
**Target:** 1st Semester Exam-Ready MVP  
**Timeline:** 4 weeks  
**Focus:** Core assignment requirements, not full feature set

---

## ⚠️ MVP Scope Philosophy

**This is NOT the full specification. This is the MINIMUM VIABLE PRODUCT.**

The full specification in `docs/SPECIFICATION.md` lists all possible features. This implementation plan focuses on:

1. ✅ **Core assignment requirements** (self-written algorithms, Console App, Class Library)
2. ✅ **Exam-ready demonstration** (understandable, explainable, extendable)
3. ✅ **Buildable in 4 weeks** by 3-4 first-semester students
4. ❌ **NOT a production system** or complete feature set

**Key Principle:** "En simpel implementering kan virke som et lige så godt grundlag for eksamen, som en meget avanceret løsning"

---

## 1. Solution Structure

### Visual Studio Solution (.NET 8.0)

```
CirkusLuna.sln
│
├── CirkusLuna.Core/                           [Class Library - EXAM FOCUS]
│   ├── Models/
│   │   ├── By.cs                             (City entity)
│   │   ├── Artist.cs                         (Performer)
│   │   ├── Kunde.cs                          (Customer)
│   │   ├── Billettype.cs                     (Ticket Type enum)
│   │   ├── Forestilling.cs                   (Performance)
│   │   └── Reservation.cs                    (Booking)
│   │
│   ├── Interfaces/
│   │   ├── IRepository.cs                    (Generic base)
│   │   ├── IByRepository.cs
│   │   ├── IArtistRepository.cs
│   │   ├── IKundeRepository.cs
│   │   ├── IForestillingRepository.cs
│   │   ├── IReservationRepository.cs
│   │   ├── IForestillingService.cs
│   │   └── IReservationService.cs
│   │
│   ├── Repositories/
│   │   ├── InMemoryByRepository.cs           (Cities with sorting algorithm)
│   │   ├── InMemoryArtistRepository.cs
│   │   ├── InMemoryKundeRepository.cs
│   │   ├── InMemoryForestillingRepository.cs (Performances with search algorithm)
│   │   └── InMemoryReservationRepository.cs
│   │
│   ├── Services/
│   │   ├── ForestillingService.cs            (Performance business logic)
│   │   └── ReservationService.cs             (Reservation validation & creation)
│   │
│   ├── Exceptions/
│   │   ├── ReservationFullException.cs       (No seats available)
│   │   ├── VIPCapacityExceededException.cs   (No VIP seats available)
│   │   └── PastPerformanceException.cs       (Cannot book past performances)
│   │
│   └── SeedData/
│       └── DataSeeder.cs                     (Initial data for demonstration)
│
├── CirkusLuna.ConsoleApp/                     [Console App - EXAM DEMONSTRATION]
│   ├── Program.cs
│   └── Menus/
│       ├── MainMenu.cs
│       ├── ForestillingMenu.cs
│       └── ReservationMenu.cs
│
└── CirkusLuna.Web/                            [ASP.NET Core Razor Pages]
    ├── Pages/
    │   ├── Index.cshtml + .cshtml.cs         (Homepage)
    │   ├── Program.cshtml + .cshtml.cs       (List performances)
    │   ├── Detaljer.cshtml + .cshtml.cs      (Performance details)
    │   ├── Reserver.cshtml + .cshtml.cs      (Create reservation)
    │   ├── Bekraeftelse.cshtml + .cshtml.cs  (Confirmation)
    │   ├── Artister.cshtml + .cshtml.cs      (List artists)
    │   └── Shared/
    │       ├── _Layout.cshtml
    │       └── _ValidationScriptsPartial.cshtml
    ├── wwwroot/
    │   ├── css/
    │   │   └── site.css                      (Custom CSS - burgundy/gold/cream)
    │   ├── js/
    │   │   └── site.js                       (Minimal JS if needed)
    │   └── images/
    │       └── logo.png                      (Optional circus logo)
    ├── Program.cs
    └── appsettings.json
```

### Project References

- `CirkusLuna.Web` → references → `CirkusLuna.Core`
- `CirkusLuna.ConsoleApp` → references → `CirkusLuna.Core`

**No circular references. No cross-references between Web and ConsoleApp.**

---

## 2. Core Models (CirkusLuna.Core/Models)

### 2.1 By (City)

```csharp
namespace CirkusLuna.Core.Models
{
    public class By
    {
        public int Id { get; set; }
        public string Navn { get; set; } = string.Empty;
        public string Region { get; set; } = string.Empty; // Optional: Sjælland, Jylland, Fyn
    }
}
```

**Why simplified:** We don't need full Lokation/Venue entities for MVP. City is enough to demonstrate sorting and search.

### 2.2 Artist

```csharp
namespace CirkusLuna.Core.Models
{
    public class Artist
    {
        public int Id { get; set; }
        public string Navn { get; set; } = string.Empty;
        public string Specialitet { get; set; } = string.Empty; // e.g., "Trapez", "Jonglør", "Klovn"
        
        // Navigation property for many-to-many
        public List<Forestilling> Forestillinger { get; set; } = new();
    }
}
```

### 2.3 Kunde (Customer)

```csharp
namespace CirkusLuna.Core.Models
{
    public class Kunde
    {
        public int Id { get; set; }
        public string Navn { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Telefon { get; set; } = string.Empty;
        
        // Navigation property
        public List<Reservation> Reservationer { get; set; } = new();
    }
}
```

### 2.4 Billettype (Ticket Type - Enum)

```csharp
namespace CirkusLuna.Core.Models
{
    public enum Billettype
    {
        Normal = 0,      // Regular ticket
        Barn = 1,        // Children ticket
        VIP = 2          // VIP ticket
    }
}
```

### 2.5 BillettypePris (Ticket Type Pricing - Helper Class)

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

**Usage in ReservationService:**
```csharp
decimal totalPris = BillettypePris.GetPris(billettype) * antalBilletter;
```

### 2.6 Forestilling (Performance)

```csharp
namespace CirkusLuna.Core.Models
{
    public class Forestilling
    {
        public int Id { get; set; }
        public string Titel { get; set; } = string.Empty;
        public DateTime Dato { get; set; }
        public TimeSpan Tidspunkt { get; set; }
        
        // Foreign key
        public int ById { get; set; }
        public By By { get; set; } = null!;
        
        // Capacity constraints
        public int TotalKapacitet { get; set; } = 150;
        public int VIPKapacitet { get; set; } = 10;
        
        // Navigation properties
        public List<Artist> Artister { get; set; } = new();
        public List<Reservation> Reservationer { get; set; } = new();
        
        // Calculated properties
        public int AntalReserveredePladser => Reservationer.Sum(r => r.AntalBilletter);
        public int LedigePladser => TotalKapacitet - AntalReserveredePladser;
        
        public int AntalReserveredeVIPPladser => Reservationer
            .Where(r => r.Billettype == Billettype.VIP)
            .Sum(r => r.AntalBilletter);
        public int LedigeVIPPladser => VIPKapacitet - AntalReserveredeVIPPladser;
    }
}
```

### 2.7 Reservation (Booking)

```csharp
namespace CirkusLuna.Core.Models
{
    public class Reservation
    {
        public int Id { get; set; }
        
        // Foreign keys
        public int KundeId { get; set; }
        public Kunde Kunde { get; set; } = null!;
        
        public int ForestillingId { get; set; }
        public Forestilling Forestilling { get; set; } = null!;
        
        // Reservation details
        public int AntalBilletter { get; set; }
        public Billettype Billettype { get; set; }
        public DateTime ReservationsDato { get; set; }
        
        // Calculated property
        public decimal TotalPris => BillettypePris.GetPris(Billettype) * AntalBilletter;
    }
}
```

---

## 3. Repository Layer (CirkusLuna.Core/Repositories)

### 3.1 Generic Interface

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

### 3.2 IByRepository (City Repository)

```csharp
namespace CirkusLuna.Core.Interfaces
{
    public interface IByRepository : IRepository<By>
    {
        // ⭐ CRITICAL: Self-written alphabetical sorting algorithm
        List<By> GetCitiesSortedAlphabetically();
        
        By? GetByName(string navn);
    }
}
```

**Implementation: InMemoryByRepository.cs**

```csharp
namespace CirkusLuna.Core.Repositories
{
    public class InMemoryByRepository : IByRepository
    {
        private List<By> _byer = new();
        private int _nextId = 1;
        
        public InMemoryByRepository()
        {
            // Will be seeded by DataSeeder
        }
        
        public List<By> GetAll() => _byer.ToList();
        
        public By? GetById(int id) => _byer.FirstOrDefault(b => b.Id == id);
        
        public void Add(By entity)
        {
            entity.Id = _nextId++;
            _byer.Add(entity);
        }
        
        public void Update(By entity)
        {
            var existing = GetById(entity.Id);
            if (existing != null)
            {
                existing.Navn = entity.Navn;
                existing.Region = entity.Region;
            }
        }
        
        public void Delete(int id)
        {
            var entity = GetById(id);
            if (entity != null) _byer.Remove(entity);
        }
        
        public By? GetByName(string navn)
        {
            return _byer.FirstOrDefault(b => 
                b.Navn.Equals(navn, StringComparison.OrdinalIgnoreCase));
        }
        
        // ⭐ CRITICAL: Self-written sorting algorithm (Bubble Sort)
        public List<By> GetCitiesSortedAlphabetically()
        {
            List<By> sortedCities = _byer.ToList();
            int n = sortedCities.Count;
            
            // Bubble sort implementation
            for (int i = 0; i < n - 1; i++)
            {
                for (int j = 0; j < n - i - 1; j++)
                {
                    // Compare city names alphabetically
                    if (string.Compare(sortedCities[j].Navn, 
                                      sortedCities[j + 1].Navn, 
                                      StringComparison.OrdinalIgnoreCase) > 0)
                    {
                        // Swap
                        By temp = sortedCities[j];
                        sortedCities[j] = sortedCities[j + 1];
                        sortedCities[j + 1] = temp;
                    }
                }
            }
            
            return sortedCities;
        }
    }
}
```

### 3.3 IForestillingRepository (Performance Repository)

```csharp
namespace CirkusLuna.Core.Interfaces
{
    public interface IForestillingRepository : IRepository<Forestilling>
    {
        // ⭐ CRITICAL: Self-written search algorithm using loops
        List<Forestilling> SearchByCity(string byNavn);
        
        List<Forestilling> GetByDate(DateTime dato);
        List<Forestilling> GetUpcomingPerformances();
        List<Forestilling> GetPerformancesInCity(int byId);
    }
}
```

**Implementation: InMemoryForestillingRepository.cs**

```csharp
namespace CirkusLuna.Core.Repositories
{
    public class InMemoryForestillingRepository : IForestillingRepository
    {
        private List<Forestilling> _forestillinger = new();
        private int _nextId = 1;
        
        public List<Forestilling> GetAll() => _forestillinger.ToList();
        
        public Forestilling? GetById(int id) => _forestillinger.FirstOrDefault(f => f.Id == id);
        
        public void Add(Forestilling entity)
        {
            entity.Id = _nextId++;
            _forestillinger.Add(entity);
        }
        
        public void Update(Forestilling entity)
        {
            var existing = GetById(entity.Id);
            if (existing != null)
            {
                existing.Titel = entity.Titel;
                existing.Dato = entity.Dato;
                existing.Tidspunkt = entity.Tidspunkt;
                existing.ById = entity.ById;
                existing.TotalKapacitet = entity.TotalKapacitet;
                existing.VIPKapacitet = entity.VIPKapacitet;
            }
        }
        
        public void Delete(int id)
        {
            var entity = GetById(id);
            if (entity != null) _forestillinger.Remove(entity);
        }
        
        // ⭐ CRITICAL: Self-written search algorithm using manual loop
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
        
        public List<Forestilling> GetByDate(DateTime dato)
        {
            return _forestillinger.Where(f => f.Dato.Date == dato.Date).ToList();
        }
        
        public List<Forestilling> GetUpcomingPerformances()
        {
            return _forestillinger.Where(f => f.Dato >= DateTime.Now).ToList();
        }
        
        public List<Forestilling> GetPerformancesInCity(int byId)
        {
            return _forestillinger.Where(f => f.ById == byId).ToList();
        }
    }
}
```

### 3.4 Other Repositories (Simplified)

**IArtistRepository + InMemoryArtistRepository**
- Standard CRUD only
- No complex algorithms needed

**IKundeRepository + InMemoryKundeRepository**
- Standard CRUD only
- Optional: `GetByEmail(string email)`

**IReservationRepository + InMemoryReservationRepository**
- Standard CRUD only
- `GetByForestillingId(int forestillingId)`
- `GetByKundeId(int kundeId)`

---

## 4. Service Layer (CirkusLuna.Core/Services)

### 4.1 ForestillingService

```csharp
namespace CirkusLuna.Core.Services
{
    public interface IForestillingService
    {
        List<Forestilling> GetAllForestillinger();
        Forestilling? GetForestillingById(int id);
        List<Forestilling> SearchByCity(string byNavn);
        List<Forestilling> GetUpcomingForestillinger();
        List<By> GetCitiesSortedAlphabetically();
    }
    
    public class ForestillingService : IForestillingService
    {
        private readonly IForestillingRepository _forestillingRepo;
        private readonly IByRepository _byRepo;
        
        public ForestillingService(
            IForestillingRepository forestillingRepo,
            IByRepository byRepo)
        {
            _forestillingRepo = forestillingRepo;
            _byRepo = byRepo;
        }
        
        public List<Forestilling> GetAllForestillinger()
        {
            return _forestillingRepo.GetAll();
        }
        
        public Forestilling? GetForestillingById(int id)
        {
            return _forestillingRepo.GetById(id);
        }
        
        public List<Forestilling> SearchByCity(string byNavn)
        {
            // Uses self-written search algorithm in repository
            return _forestillingRepo.SearchByCity(byNavn);
        }
        
        public List<Forestilling> GetUpcomingForestillinger()
        {
            return _forestillingRepo.GetUpcomingPerformances();
        }
        
        public List<By> GetCitiesSortedAlphabetically()
        {
            // Uses self-written sorting algorithm in repository
            return _byRepo.GetCitiesSortedAlphabetically();
        }
    }
}
```

### 4.2 ReservationService

```csharp
namespace CirkusLuna.Core.Services
{
    public interface IReservationService
    {
        Reservation CreateReservation(int kundeId, int forestillingId, 
                                     int antalBilletter, Billettype billettype);
        bool CanReserve(int forestillingId, int antalBilletter, Billettype billettype);
        List<Reservation> GetReservationsByKunde(int kundeId);
    }
    
    public class ReservationService : IReservationService
    {
        private readonly IReservationRepository _reservationRepo;
        private readonly IForestillingRepository _forestillingRepo;
        private readonly IKundeRepository _kundeRepo;
        
        public ReservationService(
            IReservationRepository reservationRepo,
            IForestillingRepository forestillingRepo,
            IKundeRepository kundeRepo)
        {
            _reservationRepo = reservationRepo;
            _forestillingRepo = forestillingRepo;
            _kundeRepo = kundeRepo;
        }
        
        public Reservation CreateReservation(
            int kundeId, 
            int forestillingId, 
            int antalBilletter, 
            Billettype billettype)
        {
            // Get forestilling
            var forestilling = _forestillingRepo.GetById(forestillingId);
            if (forestilling == null)
                throw new ArgumentException("Forestilling ikke fundet");
            
            // Business rule: Only future performances
            if (forestilling.Dato < DateTime.Now)
                throw new PastPerformanceException(
                    "Kan ikke reservere billetter til tidligere forestillinger");
            
            // Business rule: Check capacity
            if (billettype == Billettype.VIP)
            {
                if (forestilling.LedigeVIPPladser < antalBilletter)
                    throw new VIPCapacityExceededException(
                        $"Kun {forestilling.LedigeVIPPladser} VIP-pladser tilbage");
            }
            else
            {
                if (forestilling.LedigePladser < antalBilletter)
                    throw new ReservationFullException(
                        $"Kun {forestilling.LedigePladser} pladser tilbage");
            }
            
            // Get kunde
            var kunde = _kundeRepo.GetById(kundeId);
            if (kunde == null)
                throw new ArgumentException("Kunde ikke fundet");
            
            // Create reservation
            var reservation = new Reservation
            {
                KundeId = kundeId,
                Kunde = kunde,
                ForestillingId = forestillingId,
                Forestilling = forestilling,
                AntalBilletter = antalBilletter,
                Billettype = billettype,
                ReservationsDato = DateTime.Now
            };
            
            _reservationRepo.Add(reservation);
            return reservation;
        }
        
        public bool CanReserve(int forestillingId, int antalBilletter, Billettype billettype)
        {
            var forestilling = _forestillingRepo.GetById(forestillingId);
            if (forestilling == null) return false;
            if (forestilling.Dato < DateTime.Now) return false;
            
            if (billettype == Billettype.VIP)
                return forestilling.LedigeVIPPladser >= antalBilletter;
            else
                return forestilling.LedigePladser >= antalBilletter;
        }
        
        public List<Reservation> GetReservationsByKunde(int kundeId)
        {
            return _reservationRepo.GetByKundeId(kundeId);
        }
    }
}
```

---

## 5. Custom Exceptions (CirkusLuna.Core/Exceptions)

```csharp
namespace CirkusLuna.Core.Exceptions
{
    public class ReservationFullException : Exception
    {
        public ReservationFullException(string message) : base(message) { }
    }
    
    public class VIPCapacityExceededException : Exception
    {
        public VIPCapacityExceededException(string message) : base(message) { }
    }
    
    public class PastPerformanceException : Exception
    {
        public PastPerformanceException(string message) : base(message) { }
    }
}
```

---

## 6. Seed Data (CirkusLuna.Core/SeedData)

```csharp
namespace CirkusLuna.Core.SeedData
{
    public static class DataSeeder
    {
        public static void SeedAll(
            IByRepository byRepo,
            IArtistRepository artistRepo,
            IKundeRepository kundeRepo,
            IForestillingRepository forestillingRepo)
        {
            SeedCities(byRepo);
            SeedArtists(artistRepo);
            SeedCustomers(kundeRepo);
            SeedPerformances(forestillingRepo, byRepo, artistRepo);
        }
        
        private static void SeedCities(IByRepository repo)
        {
            var cities = new List<By>
            {
                new By { Navn = "København", Region = "Sjælland" },
                new By { Navn = "Aarhus", Region = "Jylland" },
                new By { Navn = "Odense", Region = "Fyn" },
                new By { Navn = "Aalborg", Region = "Jylland" },
                new By { Navn = "Esbjerg", Region = "Jylland" },
                new By { Navn = "Roskilde", Region = "Sjælland" },
                new By { Navn = "Kolding", Region = "Jylland" },
                new By { Navn = "Horsens", Region = "Jylland" }
            };
            
            foreach (var city in cities)
                repo.Add(city);
        }
        
        private static void SeedArtists(IArtistRepository repo)
        {
            var artists = new List<Artist>
            {
                new Artist { Navn = "Lars Henriksen", Specialitet = "Trapez" },
                new Artist { Navn = "Maria Sørensen", Specialitet = "Jonglør" },
                new Artist { Navn = "Peter Nielsen", Specialitet = "Klovn" },
                new Artist { Navn = "Anna Andersen", Specialitet = "Akrobat" },
                new Artist { Navn = "Thomas Jensen", Specialitet = "Tryllekunstner" }
            };
            
            foreach (var artist in artists)
                repo.Add(artist);
        }
        
        private static void SeedCustomers(IKundeRepository repo)
        {
            var customers = new List<Kunde>
            {
                new Kunde { Navn = "Jens Hansen", Email = "jens@mail.dk", Telefon = "12345678" },
                new Kunde { Navn = "Anne Jensen", Email = "anne@mail.dk", Telefon = "23456789" },
                new Kunde { Navn = "Morten Olsen", Email = "morten@mail.dk", Telefon = "34567890" }
            };
            
            foreach (var customer in customers)
                repo.Add(customer);
        }
        
        private static void SeedPerformances(
            IForestillingRepository forestillingRepo,
            IByRepository byRepo,
            IArtistRepository artistRepo)
        {
            var allArtists = artistRepo.GetAll();
            var allCities = byRepo.GetAll();
            
            var performances = new List<Forestilling>
            {
                new Forestilling
                {
                    Titel = "Den Store Cirkus Show",
                    Dato = new DateTime(2026, 6, 1),
                    Tidspunkt = new TimeSpan(19, 0, 0),
                    By = allCities[0], // København
                    ById = allCities[0].Id,
                    Artister = new List<Artist> { allArtists[0], allArtists[1], allArtists[2] }
                },
                new Forestilling
                {
                    Titel = "Magisk Aften",
                    Dato = new DateTime(2026, 6, 5),
                    Tidspunkt = new TimeSpan(18, 0, 0),
                    By = allCities[1], // Aarhus
                    ById = allCities[1].Id,
                    Artister = new List<Artist> { allArtists[3], allArtists[4] }
                },
                new Forestilling
                {
                    Titel = "Familie Forestilling",
                    Dato = new DateTime(2026, 6, 10),
                    Tidspunkt = new TimeSpan(15, 0, 0),
                    By = allCities[2], // Odense
                    ById = allCities[2].Id,
                    Artister = new List<Artist> { allArtists[2], allArtists[3] }
                },
                // Add more performances across different cities...
            };
            
            foreach (var performance in performances)
                forestillingRepo.Add(performance);
        }
    }
}
```

**Seed Data Plan:**
- 8 cities (København, Aarhus, Odense, Aalborg, Esbjerg, Roskilde, Kolding, Horsens)
- 5 artists (mix of permanent and guest)
- 3 test customers
- 10-12 performances spread across cities and dates (some past for testing, most future)

---

## 7. Console App (CirkusLuna.ConsoleApp)

### 7.1 Program.cs

```csharp
using CirkusLuna.Core.Interfaces;
using CirkusLuna.Core.Repositories;
using CirkusLuna.Core.Services;
using CirkusLuna.Core.SeedData;
using CirkusLuna.Core.Models;
using CirkusLuna.Core.Exceptions;

namespace CirkusLuna.ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            // Setup repositories
            var byRepo = new InMemoryByRepository();
            var artistRepo = new InMemoryArtistRepository();
            var kundeRepo = new InMemoryKundeRepository();
            var forestillingRepo = new InMemoryForestillingRepository();
            var reservationRepo = new InMemoryReservationRepository();
            
            // Seed data
            DataSeeder.SeedAll(byRepo, artistRepo, kundeRepo, forestillingRepo);
            
            // Setup services
            var forestillingService = new ForestillingService(forestillingRepo, byRepo);
            var reservationService = new ReservationService(
                reservationRepo, forestillingRepo, kundeRepo);
            
            // Run menu
            MainMenu.Run(forestillingService, reservationService, kundeRepo, artistRepo);
        }
    }
}
```

### 7.2 Main Menu Structure

```csharp
public static class MainMenu
{
    public static void Run(
        IForestillingService forestillingService,
        IReservationService reservationService,
        IKundeRepository kundeRepo,
        IArtistRepository artistRepo)
    {
        while (true)
        {
            Console.Clear();
            Console.WriteLine("=== CIRKUS LUNA - KONSOL APP ===");
            Console.WriteLine();
            Console.WriteLine("1. Vis alle forestillinger");
            Console.WriteLine("2. Søg forestillinger efter by");
            Console.WriteLine("3. Søg forestillinger efter dato");
            Console.WriteLine("4. Vis byer alfabetisk sorteret (selvskrevet algoritme)");
            Console.WriteLine("5. Vis alle artister");
            Console.WriteLine("6. Opret kunde");
            Console.WriteLine("7. Opret reservation");
            Console.WriteLine("8. Test: Kapacitetsgrænse (150 pladser)");
            Console.WriteLine("9. Test: VIP kapacitetsgrænse (10 pladser)");
            Console.WriteLine("10. Test: Afvis tidligere forestilling");
            Console.WriteLine("0. Afslut");
            Console.WriteLine();
            Console.Write("Vælg: ");
            
            var choice = Console.ReadLine();
            
            switch (choice)
            {
                case "1":
                    ShowAllPerformances(forestillingService);
                    break;
                case "2":
                    SearchByCity(forestillingService);
                    break;
                case "3":
                    SearchByDate(forestillingService);
                    break;
                case "4":
                    ShowCitiesSorted(forestillingService);
                    break;
                case "5":
                    ShowAllArtists(artistRepo);
                    break;
                case "6":
                    CreateCustomer(kundeRepo);
                    break;
                case "7":
                    CreateReservation(reservationService, forestillingService, kundeRepo);
                    break;
                case "8":
                    TestCapacityLimit(reservationService, forestillingService, kundeRepo);
                    break;
                case "9":
                    TestVIPCapacityLimit(reservationService, forestillingService, kundeRepo);
                    break;
                case "10":
                    TestPastPerformanceRejection(reservationService, kundeRepo);
                    break;
                case "0":
                    return;
            }
        }
    }
    
    // Implementation of each menu option...
}
```

**Console App Must Demonstrate:**
1. ✅ Show all performances
2. ✅ Search by city (using self-written search algorithm)
3. ✅ Search by date
4. ✅ Show cities alphabetically sorted (using self-written sorting algorithm)
5. ✅ Show all artists
6. ✅ Create customer
7. ✅ Create reservation
8. ✅ Test capacity limit (150 normal seats)
9. ✅ Test VIP capacity limit (10 VIP seats)
10. ✅ Test past performance rejection

---

## 8. Razor Pages (CirkusLuna.Web/Pages)

### 8.1 Page List (MVP Scope)

**Core Pages (MUST HAVE):**

1. **Index.cshtml** (Homepage)
   - Hero section with Cirkus Luna branding
   - Next 3-4 upcoming performances
   - CTA buttons: "Se Program", "Reserver Billet"

2. **Program.cshtml** (List all performances)
   - Display all upcoming performances
   - Search form (by city)
   - Show: Date, City, Title, Available seats
   - Link to details and reservation

3. **Detaljer.cshtml** (Performance details)
   - Show full performance information
   - List of artists
   - Available seats (total and VIP)
   - "Reserver" button

4. **Reserver.cshtml** (Create reservation)
   - Form: Customer selection/creation, Number of tickets, Ticket type
   - Validation
   - Submit creates reservation

5. **Bekraeftelse.cshtml** (Reservation confirmation)
   - Show reservation details
   - Confirmation message

6. **Artister.cshtml** (List artists)
   - Display all artists with specialties
   - Optional: Link to performances they appear in

**Admin Pages (OPTIONAL - only if time):**

7. **Admin/Forestillinger.cshtml** (Manage performances)
8. **Admin/Artister.cshtml** (Manage artists)

### 8.2 _Layout.cshtml (Master Layout)

```html
<!DOCTYPE html>
<html lang="da">
<head>
    <meta charset="utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0" />
    <title>@ViewData["Title"] - Cirkus Luna</title>
    <link rel="stylesheet" href="~/css/bootstrap.min.css" />
    <link rel="stylesheet" href="~/css/site.css" />
</head>
<body>
    <header>
        <nav class="navbar navbar-expand-lg navbar-dark bg-burgundy">
            <div class="container">
                <a class="navbar-brand" href="/">Cirkus Luna</a>
                <button class="navbar-toggler" type="button" data-bs-toggle="collapse" 
                        data-bs-target="#navbarNav">
                    <span class="navbar-toggler-icon"></span>
                </button>
                <div class="collapse navbar-collapse" id="navbarNav">
                    <ul class="navbar-nav ms-auto">
                        <li class="nav-item">
                            <a class="nav-link" href="/">Forside</a>
                        </li>
                        <li class="nav-item">
                            <a class="nav-link" href="/Program">Program</a>
                        </li>
                        <li class="nav-item">
                            <a class="nav-link" href="/Artister">Artister</a>
                        </li>
                        <li class="nav-item">
                            <a class="nav-link btn btn-gold" href="/Reserver">Reserver</a>
                        </li>
                    </ul>
                </div>
            </div>
        </nav>
    </header>
    
    <main role="main" class="pb-3">
        @RenderBody()
    </main>
    
    <footer class="footer mt-auto py-3 bg-light">
        <div class="container text-center">
            <span class="text-muted">© 2026 Cirkus Luna</span>
        </div>
    </footer>
    
    <script src="~/js/bootstrap.bundle.min.js"></script>
    @await RenderSectionAsync("Scripts", required: false)
</body>
</html>
```

### 8.3 Custom CSS (wwwroot/css/site.css)

```css
/* Color variables */
:root {
    --burgundy: #8B1C1C;
    --gold: #F4C542;
    --cream: #FAF8F3;
    --dark-gray: #2B2B2B;
}

/* Backgrounds */
.bg-burgundy {
    background-color: var(--burgundy) !important;
}

.bg-cream {
    background-color: var(--cream);
}

/* Buttons */
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

/* Performance cards */
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

/* Hero section */
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

### 8.4 Service Registration (Program.cs)

```csharp
var builder = WebApplication.CreateBuilder(args);

builder.Services.AddRazorPages();

// Register repositories as singletons (in-memory data)
builder.Services.AddSingleton<IByRepository, InMemoryByRepository>();
builder.Services.AddSingleton<IArtistRepository, InMemoryArtistRepository>();
builder.Services.AddSingleton<IKundeRepository, InMemoryKundeRepository>();
builder.Services.AddSingleton<IForestillingRepository, InMemoryForestillingRepository>();
builder.Services.AddSingleton<IReservationRepository, InMemoryReservationRepository>();

// Register services
builder.Services.AddScoped<IForestillingService, ForestillingService>();
builder.Services.AddScoped<IReservationService, ReservationService>();

var app = builder.Build();

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

app.UseStaticFiles();
app.UseRouting();
app.MapRazorPages();

app.Run();
```

---

## 9. Implementation Phases

### Phase 1: Foundation (Week 1) - CRITICAL

**Tasks:**
1. ✅ Create Visual Studio solution with 3 projects
2. ✅ Create all model classes (6 models)
3. ✅ Create all repository interfaces (6 interfaces)
4. ✅ Create generic `IRepository<T>` interface
5. ✅ Implement `InMemoryByRepository` with sorting algorithm
6. ✅ Implement `InMemoryForestillingRepository` with search algorithm
7. ✅ Implement other repositories (Artist, Kunde, Reservation)
8. ✅ Create `DataSeeder` with seed data
9. ✅ Test: Run Console App, verify seed data loads

**Deliverable:** Core library compiles, seed data works

### Phase 2: Business Logic (Week 2) - CRITICAL

**Tasks:**
1. ✅ Create custom exceptions (3 exception classes)
2. ✅ Implement `ForestillingService`
3. ✅ Implement `ReservationService` with all validation rules
4. ✅ Test services in Console App
5. ✅ Verify self-written algorithms work correctly
6. ✅ Test capacity limits (150 normal, 10 VIP)
7. ✅ Test past performance rejection

**Deliverable:** Services work, business rules enforced

### Phase 3: Console App (Week 2-3) - CRITICAL FOR EXAM

**Tasks:**
1. ✅ Create `MainMenu.cs` with all 10 menu options
2. ✅ Implement option 1: Show all performances
3. ✅ Implement option 2: Search by city (demonstrate self-written search)
4. ✅ Implement option 4: Show cities sorted (demonstrate self-written sort)
5. ✅ Implement option 7: Create reservation
6. ✅ Implement test options 8, 9, 10 (capacity and validation tests)
7. ✅ Add exception handling with user-friendly messages
8. ✅ Test all features thoroughly

**Deliverable:** Console App can demonstrate all core features

### Phase 4: Razor Pages Basic (Week 3) - MEDIUM PRIORITY

**Tasks:**
1. ✅ Create `_Layout.cshtml` with navigation
2. ✅ Create `Index.cshtml` (homepage)
3. ✅ Create `Program.cshtml` (list performances)
4. ✅ Create `Detaljer.cshtml` (performance details)
5. ✅ Create `Reserver.cshtml` (reservation form)
6. ✅ Create `Bekraeftelse.cshtml` (confirmation)
7. ✅ Register services in `Program.cs`
8. ✅ Test Razor Pages work correctly

**Deliverable:** Basic web UI functional

### Phase 5: UI Polish (Week 3-4) - LOW PRIORITY

**Tasks:**
1. ✅ Add Bootstrap 5 CSS
2. ✅ Create `site.css` with custom styles (burgundy, gold, cream)
3. ✅ Style performance cards
4. ✅ Create hero section on homepage
5. ✅ Add date badges
6. ✅ Ensure responsive layout
7. ✅ Add `Artister.cshtml` page

**Deliverable:** UI looks clean and professional

### Phase 6: Documentation (Week 4-5) - CRITICAL FOR SUBMISSION

**Tasks:**
1. ✅ Create README.md with setup instructions
2. ✅ Create Product Backlog with User Stories
3. ✅ Write User Stories with Acceptance Criteria
4. ✅ Create UML Domain Model (conceptual)
5. ✅ Create UML Class Diagram (focus on CirkusLuna.Core)
6. ✅ Create UML Sequence Diagram (reservation flow)
7. ✅ Write report (max 10 pages)
8. ✅ Indicate who implemented what
9. ✅ Test entire system end-to-end
10. ✅ Push to GitHub, make repository public

**Deliverable:** Complete documentation and working system

---

## 10. Testing Strategy

### 10.1 Console App Tests (Manual)

**Test Cases:**

1. **Test Search Algorithm**
   - Search for "København" → should return all København performances
   - Search for "NonExistent" → should return empty list
   - Verify manual loop is used (code inspection)

2. **Test Sorting Algorithm**
   - Display cities alphabetically
   - Verify order: Aalborg, Aarhus, Esbjerg, Horsens, Kolding, København, Odense, Roskilde
   - Verify bubble sort is used (code inspection)

3. **Test Capacity Limit (150 seats)**
   - Find performance with <5 seats available
   - Try to reserve 10 seats → should throw `ReservationFullException`

4. **Test VIP Capacity Limit (10 seats)**
   - Find performance with <2 VIP seats
   - Try to reserve 5 VIP → should throw `VIPCapacityExceededException`

5. **Test Past Performance Rejection**
   - Try to reserve tickets for past performance → should throw `PastPerformanceException`

6. **Test Valid Reservation**
   - Reserve 2 regular tickets for future performance
   - Verify reservation is created
   - Verify available seats decreased by 2

### 10.2 Web UI Tests (Manual)

**Test Cases:**

1. Navigate to homepage → should display
2. Click "Program" → should list performances
3. Search for city → should filter performances
4. Click performance details → should show info
5. Create reservation → should validate and confirm
6. Try invalid reservation → should show error message

### 10.3 Code Review Checklist

- ✅ All business logic is in Service layer (not Razor Pages)
- ✅ Self-written search algorithm uses manual loop
- ✅ Self-written sorting algorithm uses bubble/selection/insertion sort
- ✅ Capacity limits are enforced (150 total, 10 VIP)
- ✅ Custom exceptions are thrown for business rule violations
- ✅ Code is commented where business rules are implemented
- ✅ Every class has a clear responsibility
- ✅ No circular dependencies

---

## 11. Documentation Plan

### 11.1 README.md

```markdown
# Cirkus Luna - Reservation System

1st semester computer science assignment - UCL Erhvervsakademi

## How to Run

### Prerequisites
- Visual Studio 2022
- .NET 8.0 SDK

### Steps
1. Clone repository
2. Open CirkusLuna.sln in Visual Studio
3. Restore NuGet packages
4. Run Console App: Set CirkusLuna.ConsoleApp as startup project, press F5
5. Run Web App: Set CirkusLuna.Web as startup project, press F5

## Project Structure
- CirkusLuna.Core: Class Library (exam focus)
- CirkusLuna.ConsoleApp: Console demonstration
- CirkusLuna.Web: Razor Pages web application

## Key Features
- Self-written search algorithm (manual loop)
- Self-written alphabetical city sorting (bubble sort)
- Capacity management (150 total, 10 VIP)
- Reservation validation
- Exception handling
```

### 11.2 Product Backlog

**High Priority User Stories:**
1. As a customer, I want to search performances by city
2. As a customer, I want to reserve tickets
3. As a system, I want to enforce capacity limits
4. As a customer, I want to see cities alphabetically sorted
5. As a customer, I want to view performance details

**Medium Priority:**
6. As an employee, I want to manage artists
7. As a customer, I want to view artist information

**Low Priority (Out of MVP):**
8. As an employee, I want to create news posts
9. As a customer, I want to see seating chart

### 11.3 UML Diagrams to Create

1. **Domain Model** - Conceptual relationships (By, Artist, Forestilling, Kunde, Reservation)
2. **Class Diagram** - Detailed design of CirkusLuna.Core (all classes, methods, properties)
3. **Sequence Diagram** - Create Reservation flow (Customer → ReservationService → Repositories)

---

## 12. Success Criteria Checklist

### CRITICAL (Must Have for Exam)

- ✅ Three-project structure (Core, ConsoleApp, Web)
- ✅ All models defined (6 entities)
- ✅ All repositories implemented (6 repositories)
- ✅ Self-written search algorithm (manual loop in ForestillingRepository)
- ✅ Self-written sorting algorithm (bubble sort in ByRepository)
- ✅ Two service classes (ForestillingService, ReservationService)
- ✅ Three custom exceptions
- ✅ Business rules enforced (capacity, future only, VIP limits)
- ✅ Console App with 10 menu options working
- ✅ Console App can demonstrate all features
- ✅ Seed data with 8 cities, 5 artists, 10+ performances
- ✅ Code comments on business rules
- ✅ UML diagrams (Domain Model, Class Diagram, Sequence Diagram)
- ✅ Product Backlog with User Stories
- ✅ GitHub repository (public)
- ✅ README with setup instructions

### HIGH PRIORITY (Important)

- ✅ Razor Pages: Index, Program, Detaljer, Reserver, Bekraeftelse
- ✅ Basic Bootstrap layout
- ✅ Custom CSS (burgundy, gold, cream)
- ✅ Danish labels throughout
- ✅ Forms with validation
- ✅ Exception handling in web UI

### NICE TO HAVE (Optional)

- ✅ Artister.cshtml page
- ✅ Admin pages (if time permits)
- ✅ Advanced filtering
- ✅ Seating chart visualization
- ✅ Simply.com deployment

---

## 13. Features Not Implemented in MVP (Future Work)

### 13.1 Mentioned in Assignment but Deferred

The following features are mentioned in the assignment PDFs but are NOT implemented in this MVP:

**1. Medarbejder (Employee Management)**
- **Assignment reference:** "Oprette og administrere kunder, medarbejdere og artister"
- **MVP status:** Not implemented
- **Rationale:** Focus on core programming requirements (Class Library, repositories, algorithms, reservations). Customer and Artist management demonstrate the same architectural patterns that would be used for employees.

**2. Nyheder/Blog (News/Blog Posts)**
- **Assignment reference:** "Oprette og administrere nyheder/blogindlæg om cirkusset"
- **MVP status:** Not implemented
- **Rationale:** Lower priority feature. The core reservation system, search algorithms, and capacity validation are more important for demonstrating exam-relevant programming skills.

**3. Pladsoversigt (Detailed Seating Chart)**
- **Assignment reference:** "Vise en oversigt over siddepladser i teltet"
- **MVP status:** Not implemented
- **Rationale:** Capacity tracking is implemented (150 total, 10 VIP), but individual seat selection/visualization is deferred. The current implementation demonstrates capacity validation, which is the core requirement.

**4. Lokation (Separate Venue Entity)**
- **Assignment reference:** Performances happen at specific venues in cities
- **MVP status:** Simplified - City entity only
- **Rationale:** For MVP, City is sufficient to demonstrate many-to-many relationships and search/sorting algorithms. A full Venue entity with address and capacity per venue adds complexity without adding to the core exam requirements.

### 13.2 Out of Assignment Scope

The following are NOT mentioned in the assignment and are NOT implemented:

❌ **Payment processing** - Not in assignment scope
❌ **Email notifications** - Not in assignment scope
❌ **User authentication/authorization** - Not in assignment scope
❌ **Real database** - In-memory is sufficient per assignment
❌ **API endpoints** - Razor Pages is required, not API-first
❌ **Mobile app** - Not in assignment scope
❌ **PDF ticket generation** - Not in assignment scope
❌ **Integration with external services** - Not in assignment scope

### 13.3 How to Extend for Exam

At the exam, students may be asked to extend the system. Here are likely extension points:

**Easy to add:**
- Medarbejder model (copy Kunde pattern)
- Nyhed model (standard CRUD entity)
- Additional filtering (by artist, by date range)
- Plads model for seating (extend Forestilling relationship)

**Medium complexity:**
- Full Lokation entity (replace By reference in Forestilling)
- Admin dashboard for managing entities
- More complex validation rules
- Reservation cancellation

**Would require significant refactoring:**
- Real database with Entity Framework
- User authentication
- Payment integration

### 13.4 Documentation Note

The project documentation (UML diagrams, User Stories) should focus on what IS implemented in the MVP. However, the report can include a "Future Work" or "Not Implemented" section briefly mentioning these deferred features with the rationale above.

**Example for report:**

> **Afgrænsning (Scope Limitation)**
> 
> Følgende funktioner fra opgavebeskrivelsen er ikke implementeret i denne MVP:
> - Medarbejderstyring (employee management)
> - Nyheder/blog-system
> - Detaljeret sædekort med individuel pladsvalg
> 
> **Begrundelse:** Projektet prioriterer de centrale programmeringskrav: Class Library-struktur, repository-lag med selvskrevne algoritmer, service-lag med forretningsregler, kapacitetsstyring, og demonstration gennem Console App. Disse krav demonstrerer de samme OOP-principper og arkitekturmønstre, som ville anvendes til de udeladte funktioner.

---

## 14. Time Allocation (4 Weeks)

**Week 1 (Foundation):**
- Day 1-2: Project setup, models, interfaces
- Day 3-4: Repository implementations, self-written algorithms
- Day 5: Seed data, initial testing

**Week 2 (Business Logic):**
- Day 1-2: Service layer implementation
- Day 3: Custom exceptions, validation rules
- Day 4-5: Console App menu system and testing

**Week 3 (Web UI):**
- Day 1-2: Razor Pages basic structure
- Day 3-4: Forms, validation, reservation flow
- Day 5: UI polish with CSS

**Week 4 (Documentation & Testing):**
- Day 1-2: UML diagrams
- Day 3: Product Backlog, User Stories
- Day 4: Report writing
- Day 5: Final testing, GitHub submission

---

## 15. GitHub Repository Structure

```
CirkusLuna/
├── .gitignore
├── README.md
├── CirkusLuna.sln
├── CirkusLuna.Core/
├── CirkusLuna.ConsoleApp/
├── CirkusLuna.Web/
└── docs/
    ├── SPECIFICATION.md
    ├── DESIGN-GUIDELINES.md
    ├── uml/
    │   ├── domain-model.png
    │   ├── class-diagram.png
    │   └── sequence-diagram-reservation.png
    └── scrum/
        ├── product-backlog.md
        └── user-stories.md
```

---

## 16. Final Implementation Checklist

**Before Demo Day:**

### Code
- [ ] All 6 models implemented and tested
- [ ] All 6 repositories implemented with CRUD
- [ ] Self-written search algorithm works (manual loop)
- [ ] Self-written sorting algorithm works (bubble sort)
- [ ] Both services implemented with business rules
- [ ] All 3 custom exceptions implemented
- [ ] Seed data populates correctly
- [ ] Console App has all 10 menu options working
- [ ] Console App can demonstrate search algorithm
- [ ] Console App can demonstrate sorting algorithm
- [ ] Console App can create reservations
- [ ] Console App can test capacity limits
- [ ] All 6 Razor Pages implemented
- [ ] Razor Pages use services (no business logic in pages)
- [ ] Forms validate correctly
- [ ] Exception messages display to user
- [ ] Custom CSS applied (burgundy, gold, cream)

### Documentation
- [ ] README.md with setup instructions
- [ ] Domain Model UML diagram
- [ ] Class Diagram UML diagram (focus on Core)
- [ ] Sequence Diagram (reservation flow)
- [ ] Product Backlog created
- [ ] User Stories with Acceptance Criteria
- [ ] Report written (max 10 pages)
- [ ] Individual contributions documented
- [ ] GitHub repository is public
- [ ] GitHub link in documentation

### Testing
- [ ] Console App tested: All menu options work
- [ ] Console App tested: Search algorithm demonstrated
- [ ] Console App tested: Sorting algorithm demonstrated
- [ ] Console App tested: Capacity limit (150) enforced
- [ ] Console App tested: VIP limit (10) enforced
- [ ] Console App tested: Past performance rejected
- [ ] Web App tested: Can view performances
- [ ] Web App tested: Can search by city
- [ ] Web App tested: Can create reservation
- [ ] Web App tested: Validation works
- [ ] Code reviewed: Business logic in services, not Razor Pages
- [ ] Code reviewed: Self-written algorithms are truly self-written

### Exam Readiness
- [ ] System runs on local PC (no cloud dependencies)
- [ ] Every group member understands their implemented parts
- [ ] Can explain self-written algorithms
- [ ] Can explain three-layer architecture
- [ ] Can walk through UML diagrams
- [ ] Can explain business rules
- [ ] Can extend the system (add new feature)
- [ ] Ready to demo Console App (primary focus)
- [ ] Ready to demo Web App (secondary)

---

## Summary

This MVP implementation plan scopes Cirkus Luna to a **realistic 1st semester assignment** that:

✅ **Meets all assignment requirements:**
- Three-project structure
- Self-written search algorithm
- Self-written sorting algorithm
- Console App for exam demonstration
- Class Library as exam focus
- Razor Pages web application
- SCRUM documentation
- UML diagrams

✅ **Stays simple and explainable:**
- 6 core entities (not 11)
- 2 services (not 5+)
- 6 basic pages (not complex dashboard)
- In-memory data (no database complexity)
- Manual constructor injection (no DI container)

✅ **Can be built in 4 weeks by 1st semester students:**
- Phase 1: Foundation (1 week)
- Phase 2: Business logic (1 week)
- Phase 3: Console App + Razor Pages (1 week)
- Phase 4: Documentation (1 week)

✅ **Focuses on exam readiness:**
- Console App demonstrates everything
- Business logic in Class Library
- Self-written algorithms are clearly identifiable
- Every part is understandable and explainable

**This is the buildable, exam-ready MVP - not the full 67-page specification.**
