using CirkusLuna.Core.Exceptions;
using CirkusLuna.Core.Interfaces;
using CirkusLuna.Core.Models;
using CirkusLuna.Core.Repositories;
using CirkusLuna.Core.SeedData;
using CirkusLuna.Core.Services;

namespace CirkusLuna.ConsoleApp
{
    /// <summary>
    /// Console application for demonstrating and testing Cirkus Luna core functionality
    /// This is the primary demonstration tool for the exam (exam does NOT focus on Razor Pages)
    /// </summary>
    class Program
    {
        private static IForestillingService? _forestillingService;
        private static IReservationService? _reservationService;
        private static IKundeRepository? _kundeRepo;
        private static IArtistRepository? _artistRepo;
        
        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            
            // Setup repositories
            var byRepo = new InMemoryByRepository();
            var artistRepo = new InMemoryArtistRepository();
            var kundeRepo = new InMemoryKundeRepository();
            var forestillingRepo = new InMemoryForestillingRepository();
            var reservationRepo = new InMemoryReservationRepository();
            
            // Seed data
            DataSeeder.SeedAll(byRepo, artistRepo, kundeRepo, forestillingRepo);
            
            // Setup services
            _forestillingService = new ForestillingService(forestillingRepo, byRepo);
            _reservationService = new ReservationService(reservationRepo, forestillingRepo, kundeRepo);
            _kundeRepo = kundeRepo;
            _artistRepo = artistRepo;
            
            // Run main menu
            MainMenu();
        }
        
        static void MainMenu()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("╔═══════════════════════════════════════════════════════════╗");
                Console.WriteLine("║         CIRKUS LUNA - KONSOL APP (EKSAMENSDEMO)         ║");
                Console.WriteLine("╚═══════════════════════════════════════════════════════════╝");
                Console.WriteLine();
                Console.WriteLine("DEMONSTRATION AF KERNE-FUNKTIONALITET:");
                Console.WriteLine();
                Console.WriteLine("  1. Vis alle forestillinger");
                Console.WriteLine("  2. Søg forestillinger efter by (selvskrevet algoritme)");
                Console.WriteLine("  3. Søg forestillinger efter dato");
                Console.WriteLine("  4. Vis byer alfabetisk sorteret (bubble sort)");
                Console.WriteLine("  5. Vis alle artister");
                Console.WriteLine("  6. Opret kunde");
                Console.WriteLine("  7. Opret reservation");
                Console.WriteLine("  8. Test: Kapacitetsgrænse (150 pladser)");
                Console.WriteLine("  9. Test: VIP kapacitetsgrænse (10 pladser)");
                Console.WriteLine(" 10. Test: Afvis tidligere forestilling");
                Console.WriteLine("  0. Afslut");
                Console.WriteLine();
                Console.WriteLine("═══════════════════════════════════════════════════════════");
                Console.Write("Vælg (0-10): ");
                
                var choice = Console.ReadLine();
                Console.WriteLine();
                
                switch (choice)
                {
                    case "1":
                        ShowAllPerformances();
                        break;
                    case "2":
                        SearchByCity();
                        break;
                    case "3":
                        SearchByDate();
                        break;
                    case "4":
                        ShowCitiesSorted();
                        break;
                    case "5":
                        ShowAllArtists();
                        break;
                    case "6":
                        CreateCustomer();
                        break;
                    case "7":
                        CreateReservation();
                        break;
                    case "8":
                        TestCapacityLimit();
                        break;
                    case "9":
                        TestVIPCapacityLimit();
                        break;
                    case "10":
                        TestPastPerformanceRejection();
                        break;
                    case "0":
                        Console.WriteLine("Afslutter programmet. Farvel!");
                        return;
                    default:
                        Console.WriteLine("Ugyldigt valg. Tryk Enter for at prøve igen.");
                        Console.ReadLine();
                        break;
                }
            }
        }
        
        static void ShowAllPerformances()
        {
            Console.WriteLine("═══ ALLE FORESTILLINGER ═══");
            Console.WriteLine();
            
            var forestillinger = _forestillingService!.GetAllForestillinger();
            
            foreach (var f in forestillinger)
            {
                Console.WriteLine($"ID: {f.Id}");
                Console.WriteLine($"Titel: {f.Titel}");
                Console.WriteLine($"Dato: {f.Dato:dd/MM/yyyy} kl. {f.Tidspunkt:hh\\:mm}");
                Console.WriteLine($"By: {f.By.Navn} ({f.By.Region})");
                
                // Build artist list without lambda
                string artistNames = "";
                for (int i = 0; i < f.Artister.Count; i++)
                {
                    if (i > 0)
                    {
                        artistNames += ", ";
                    }
                    artistNames += f.Artister[i].Navn;
                }
                
                Console.WriteLine($"Artister: {artistNames}");
                Console.WriteLine($"Kapacitet: {f.LedigePladser}/{f.TotalKapacitet} ledige pladser");
                Console.WriteLine($"VIP: {f.LedigeVIPPladser}/{f.VIPKapacitet} ledige VIP-pladser");
                Console.WriteLine();
            }
            
            Console.WriteLine("─────────────────────────────────────────────");
            Console.WriteLine($"Total: {forestillinger.Count} forestillinger");
            Console.WriteLine();
            Console.Write("Tryk Enter for at fortsætte...");
            Console.ReadLine();
        }
        
        static void SearchByCity()
        {
            Console.WriteLine("═══ SØG FORESTILLINGER EFTER BY ═══");
            Console.WriteLine("(Bruger selvskrevet søgealgoritme med manuel loop)");
            Console.WriteLine();
            Console.Write("Indtast bynavn: ");
            var byNavn = Console.ReadLine() ?? "";
            Console.WriteLine();
            
            // Uses self-written search algorithm in repository
            var results = _forestillingService!.SearchByCity(byNavn);
            
            if (results.Count == 0)
            {
                Console.WriteLine($"Ingen forestillinger fundet i {byNavn}.");
            }
            else
            {
                Console.WriteLine($"Fundet {results.Count} forestilling(er) i {byNavn}:");
                Console.WriteLine();
                
                foreach (var f in results)
                {
                    Console.WriteLine($"- {f.Titel}");
                    Console.WriteLine($"  Dato: {f.Dato:dd/MM/yyyy} kl. {f.Tidspunkt:hh\\:mm}");
                    Console.WriteLine($"  Ledige pladser: {f.LedigePladser}/{f.TotalKapacitet}");
                    Console.WriteLine();
                }
            }
            
            Console.Write("Tryk Enter for at fortsætte...");
            Console.ReadLine();
        }
        
        static void SearchByDate()
        {
            Console.WriteLine("═══ SØG FORESTILLINGER EFTER DATO ═══");
            Console.WriteLine();
            Console.Write("Indtast dato (dd/mm/yyyy): ");
            var dateInput = Console.ReadLine();
            
            if (DateTime.TryParse(dateInput, out DateTime dato))
            {
                // Manual search without lambda
                var allForestillinger = _forestillingService!.GetAllForestillinger();
                var results = new List<Forestilling>();
                
                foreach (var f in allForestillinger)
                {
                    if (f.Dato.Date == dato.Date)
                    {
                        results.Add(f);
                    }
                }
                
                Console.WriteLine();
                if (results.Count == 0)
                {
                    Console.WriteLine($"Ingen forestillinger fundet den {dato:dd/MM/yyyy}.");
                }
                else
                {
                    Console.WriteLine($"Fundet {results.Count} forestilling(er) den {dato:dd/MM/yyyy}:");
                    Console.WriteLine();
                    
                    foreach (var f in results)
                    {
                        Console.WriteLine($"- {f.Titel} i {f.By.Navn}");
                        Console.WriteLine($"  Tidspunkt: {f.Tidspunkt:hh\\:mm}");
                        Console.WriteLine();
                    }
                }
            }
            else
            {
                Console.WriteLine();
                Console.WriteLine("Ugyldig dato format.");
            }
            
            Console.WriteLine();
            Console.Write("Tryk Enter for at fortsætte...");
            Console.ReadLine();
        }
        
        static void ShowCitiesSorted()
        {
            Console.WriteLine("═══ BYER ALFABETISK SORTERET ═══");
            Console.WriteLine("(Bruger selvskrevet bubble sort algoritme)");
            Console.WriteLine();
            
            // Uses self-written bubble sort algorithm in repository
            var sortedCities = _forestillingService!.GetCitiesSortedAlphabetically();
            
            Console.WriteLine("Sorteret liste over byer:");
            Console.WriteLine();
            
            for (int i = 0; i < sortedCities.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {sortedCities[i].Navn} ({sortedCities[i].Region})");
            }
            
            Console.WriteLine();
            Console.WriteLine("─────────────────────────────────────────────");
            Console.WriteLine("Bemærk: Denne liste er sorteret alfabetisk");
            Console.WriteLine("ved brug af bubble sort algoritmen (ikke LINQ).");
            Console.WriteLine();
            Console.Write("Tryk Enter for at fortsætte...");
            Console.ReadLine();
        }
        
        static void ShowAllArtists()
        {
            Console.WriteLine("═══ ALLE ARTISTER ═══");
            Console.WriteLine();
            
            var artister = _artistRepo!.GetAll();
            
            foreach (var a in artister)
            {
                Console.WriteLine($"ID: {a.Id} - {a.Navn}");
                Console.WriteLine($"  Specialitet: {a.Specialitet}");
                Console.WriteLine();
            }
            
            Console.WriteLine("─────────────────────────────────────────────");
            Console.WriteLine($"Total: {artister.Count} artister");
            Console.WriteLine();
            Console.Write("Tryk Enter for at fortsætte...");
            Console.ReadLine();
        }
        
        static void CreateCustomer()
        {
            Console.WriteLine("═══ OPRET NY KUNDE ═══");
            Console.WriteLine();
            
            Console.Write("Navn: ");
            var navn = Console.ReadLine() ?? "";
            
            Console.Write("Email: ");
            var email = Console.ReadLine() ?? "";
            
            Console.Write("Telefon: ");
            var telefon = Console.ReadLine() ?? "";
            
            var kunde = new Kunde
            {
                Navn = navn,
                Email = email,
                Telefon = telefon
            };
            
            _kundeRepo!.Add(kunde);
            
            Console.WriteLine();
            Console.WriteLine($"✓ Kunde oprettet med ID: {kunde.Id}");
            Console.WriteLine();
            Console.Write("Tryk Enter for at fortsætte...");
            Console.ReadLine();
        }
        
        static void CreateReservation()
        {
            Console.WriteLine("═══ OPRET RESERVATION ═══");
            Console.WriteLine();
            
            // Show upcoming performances
            var upcoming = _forestillingService!.GetUpcomingForestillinger();
            Console.WriteLine("Kommende forestillinger:");
            
            // Take first 5 without lambda
            int count = 0;
            foreach (var f in upcoming)
            {
                if (count >= 5)
                {
                    break;
                }
                Console.WriteLine($"  {f.Id}. {f.Titel} - {f.By.Navn} ({f.Dato:dd/MM})");
                Console.WriteLine($"     Ledige: {f.LedigePladser} normal, {f.LedigeVIPPladser} VIP");
                count++;
            }
            Console.WriteLine();
            
            Console.Write("Vælg forestilling ID: ");
            if (!int.TryParse(Console.ReadLine(), out int forestillingId))
            {
                Console.WriteLine("Ugyldig ID.");
                Console.ReadLine();
                return;
            }
            
            // Show customers
            var kunder = _kundeRepo!.GetAll();
            Console.WriteLine();
            Console.WriteLine("Kunder:");
            foreach (var k in kunder)
            {
                Console.WriteLine($"  {k.Id}. {k.Navn} ({k.Email})");
            }
            Console.WriteLine();
            
            Console.Write("Vælg kunde ID: ");
            if (!int.TryParse(Console.ReadLine(), out int kundeId))
            {
                Console.WriteLine("Ugyldig ID.");
                Console.ReadLine();
                return;
            }
            
            Console.WriteLine();
            Console.Write("Antal billetter: ");
            if (!int.TryParse(Console.ReadLine(), out int antal))
            {
                Console.WriteLine("Ugyldigt antal.");
                Console.ReadLine();
                return;
            }
            
            Console.WriteLine();
            Console.WriteLine("Billettype:");
            Console.WriteLine("  0 = Normal (120 DKK)");
            Console.WriteLine("  1 = Barn (80 DKK)");
            Console.WriteLine("  2 = VIP (250 DKK)");
            Console.Write("Vælg type (0-2): ");
            if (!int.TryParse(Console.ReadLine(), out int typeInt) || typeInt < 0 || typeInt > 2)
            {
                Console.WriteLine("Ugyldig type.");
                Console.ReadLine();
                return;
            }
            var billettype = (Billettype)typeInt;
            
            try
            {
                var reservation = _reservationService!.CreateReservation(
                    kundeId, forestillingId, antal, billettype);
                
                Console.WriteLine();
                Console.WriteLine("✓ RESERVATION OPRETTET!");
                Console.WriteLine($"  Reservation ID: {reservation.Id}");
                Console.WriteLine($"  Antal billetter: {reservation.AntalBilletter}");
                Console.WriteLine($"  Type: {reservation.Billettype}");
                Console.WriteLine($"  Total pris: {reservation.TotalPris} DKK");
            }
            catch (PastPerformanceException ex)
            {
                Console.WriteLine();
                Console.WriteLine($"✗ FEJL: {ex.Message}");
            }
            catch (ReservationFullException ex)
            {
                Console.WriteLine();
                Console.WriteLine($"✗ FEJL: {ex.Message}");
            }
            catch (VIPCapacityExceededException ex)
            {
                Console.WriteLine();
                Console.WriteLine($"✗ FEJL: {ex.Message}");
            }
            catch (Exception ex)
            {
                Console.WriteLine();
                Console.WriteLine($"✗ FEJL: {ex.Message}");
            }
            
            Console.WriteLine();
            Console.Write("Tryk Enter for at fortsætte...");
            Console.ReadLine();
        }
        
        static void TestCapacityLimit()
        {
            Console.WriteLine("═══ TEST: KAPACITETSGRÆNSE (150 PLADSER) ═══");
            Console.WriteLine();
            Console.WriteLine("Denne test demonstrerer validering af max 150 pladser.");
            Console.WriteLine();
            
            var forestilling = _forestillingService!.GetUpcomingForestillinger().FirstOrDefault();
            if (forestilling == null)
            {
                Console.WriteLine("Ingen forestillinger tilgængelige.");
                Console.ReadLine();
                return;
            }
            
            Console.WriteLine($"Forestilling: {forestilling.Titel}");
            Console.WriteLine($"Ledige pladser: {forestilling.LedigePladser}/{forestilling.TotalKapacitet}");
            Console.WriteLine();
            Console.WriteLine($"Forsøger at reservere {forestilling.LedigePladser + 10} billetter...");
            Console.WriteLine();
            
            try
            {
                var kunde = _kundeRepo!.GetAll().First();
                _reservationService!.CreateReservation(
                    kunde.Id,
                    forestilling.Id,
                    forestilling.LedigePladser + 10,
                    Billettype.Normal);
                
                Console.WriteLine("✗ FEJL: Reservation blev godkendt (burde være afvist!)");
            }
            catch (ReservationFullException ex)
            {
                Console.WriteLine($"✓ SUCCES: Reservation afvist korrekt!");
                Console.WriteLine($"  Besked: {ex.Message}");
            }
            
            Console.WriteLine();
            Console.Write("Tryk Enter for at fortsætte...");
            Console.ReadLine();
        }
        
        static void TestVIPCapacityLimit()
        {
            Console.WriteLine("═══ TEST: VIP KAPACITETSGRÆNSE (10 PLADSER) ═══");
            Console.WriteLine();
            Console.WriteLine("Denne test demonstrerer validering af max 10 VIP-pladser.");
            Console.WriteLine();
            
            var forestilling = _forestillingService!.GetUpcomingForestillinger().FirstOrDefault();
            if (forestilling == null)
            {
                Console.WriteLine("Ingen forestillinger tilgængelige.");
                Console.ReadLine();
                return;
            }
            
            Console.WriteLine($"Forestilling: {forestilling.Titel}");
            Console.WriteLine($"Ledige VIP-pladser: {forestilling.LedigeVIPPladser}/{forestilling.VIPKapacitet}");
            Console.WriteLine();
            Console.WriteLine($"Forsøger at reservere {forestilling.LedigeVIPPladser + 5} VIP-billetter...");
            Console.WriteLine();
            
            try
            {
                var allKunder2 = _kundeRepo!.GetAll();
                Kunde? kunde2 = null;
                if (allKunder2.Count > 0)
                {
                    kunde2 = allKunder2[0];
                }
                
                if (kunde2 == null)
                {
                    Console.WriteLine("Ingen kunder tilgængelige.");
                    Console.ReadLine();
                    return;
                }
                
                _reservationService!.CreateReservation(
                    kunde2.Id,
                    forestilling.Id,
                    forestilling.LedigeVIPPladser + 5,
                    Billettype.VIP);
                
                Console.WriteLine("✗ FEJL: VIP-reservation blev godkendt (burde være afvist!)");
            }
            catch (VIPCapacityExceededException ex)
            {
                Console.WriteLine($"✓ SUCCES: VIP-reservation afvist korrekt!");
                Console.WriteLine($"  Besked: {ex.Message}");
            }
            
            Console.WriteLine();
            Console.Write("Tryk Enter for at fortsætte...");
            Console.ReadLine();
        }
        
        static void TestPastPerformanceRejection()
        {
            Console.WriteLine("═══ TEST: AFVIS TIDLIGERE FORESTILLING ═══");
            Console.WriteLine();
            Console.WriteLine("Denne test demonstrerer validering af kun fremtidige forestillinger.");
            Console.WriteLine();
            
            var allPerformances = _forestillingService!.GetAllForestillinger();
            
            // Find past performance without lambda
            Forestilling? pastPerformance = null;
            foreach (var f in allPerformances)
            {
                if (f.Dato < DateTime.Now)
                {
                    pastPerformance = f;
                    break;
                }
            }
            
            if (pastPerformance == null)
            {
                Console.WriteLine("Ingen tidligere forestillinger i systemet.");
                Console.ReadLine();
                return;
            }
            
            Console.WriteLine($"Forestilling: {pastPerformance.Titel}");
            Console.WriteLine($"Dato: {pastPerformance.Dato:dd/MM/yyyy} (tidligere dato)");
            Console.WriteLine();
            Console.WriteLine("Forsøger at reservere billetter...");
            Console.WriteLine();
            
            try
            {
                var kunde = _kundeRepo!.GetAll().First();
                _reservationService!.CreateReservation(
                    kunde.Id,
                    pastPerformance.Id,
                    2,
                    Billettype.Normal);
                
                Console.WriteLine("✗ FEJL: Reservation blev godkendt (burde være afvist!)");
            }
            catch (PastPerformanceException ex)
            {
                Console.WriteLine($"✓ SUCCES: Reservation til tidligere forestilling afvist korrekt!");
                Console.WriteLine($"  Besked: {ex.Message}");
            }
            
            Console.WriteLine();
            Console.Write("Tryk Enter for at fortsætte...");
            Console.ReadLine();
        }
    }
}
