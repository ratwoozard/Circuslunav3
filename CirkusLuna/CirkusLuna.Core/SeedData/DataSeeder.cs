using CirkusLuna.Core.Interfaces;
using CirkusLuna.Core.Models;

namespace CirkusLuna.Core.SeedData
{
    /// <summary>
    /// Static class for seeding initial data for demonstration and testing
    /// </summary>
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
                // Past performance for testing rejection
                new Forestilling
                {
                    Titel = "Forårspremiere",
                    Dato = new DateTime(2026, 5, 1),
                    Tidspunkt = new TimeSpan(19, 0, 0),
                    By = allCities[0], // København
                    ById = allCities[0].Id,
                    Artister = new List<Artist> { allArtists[0], allArtists[1] }
                },
                
                // Future performances
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
                new Forestilling
                {
                    Titel = "Sommershow",
                    Dato = new DateTime(2026, 6, 15),
                    Tidspunkt = new TimeSpan(20, 0, 0),
                    By = allCities[3], // Aalborg
                    ById = allCities[3].Id,
                    Artister = new List<Artist> { allArtists[0], allArtists[2], allArtists[4] }
                },
                new Forestilling
                {
                    Titel = "Vestjysk Special",
                    Dato = new DateTime(2026, 6, 20),
                    Tidspunkt = new TimeSpan(19, 30, 0),
                    By = allCities[4], // Esbjerg
                    ById = allCities[4].Id,
                    Artister = new List<Artist> { allArtists[1], allArtists[3] }
                },
                new Forestilling
                {
                    Titel = "Roskilde Festival Special",
                    Dato = new DateTime(2026, 6, 25),
                    Tidspunkt = new TimeSpan(18, 30, 0),
                    By = allCities[5], // Roskilde
                    ById = allCities[5].Id,
                    Artister = new List<Artist> { allArtists[0], allArtists[1], allArtists[2], allArtists[3] }
                },
                new Forestilling
                {
                    Titel = "Sjove Timer",
                    Dato = new DateTime(2026, 7, 1),
                    Tidspunkt = new TimeSpan(16, 0, 0),
                    By = allCities[6], // Kolding
                    ById = allCities[6].Id,
                    Artister = new List<Artist> { allArtists[2], allArtists[4] }
                },
                new Forestilling
                {
                    Titel = "Trylleri og Akrobatik",
                    Dato = new DateTime(2026, 7, 5),
                    Tidspunkt = new TimeSpan(19, 0, 0),
                    By = allCities[7], // Horsens
                    ById = allCities[7].Id,
                    Artister = new List<Artist> { allArtists[3], allArtists[4] }
                },
                new Forestilling
                {
                    Titel = "Afslutninsshow",
                    Dato = new DateTime(2026, 8, 15),
                    Tidspunkt = new TimeSpan(20, 0, 0),
                    By = allCities[0], // København
                    ById = allCities[0].Id,
                    Artister = new List<Artist> { allArtists[0], allArtists[1], allArtists[2], allArtists[3], allArtists[4] }
                }
            };
            
            foreach (var performance in performances)
                forestillingRepo.Add(performance);
        }
    }
}
