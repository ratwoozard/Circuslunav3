namespace CirkusLuna.Core.Models
{
    /// <summary>
    /// En forestilling i en bestemt by på en bestemt dato.
    /// </summary>
    public class Forestilling
    {
        public int Id { get; set; }
        public string Titel { get; set; } = string.Empty;
        public DateTime Dato { get; set; }
        public TimeSpan Tidspunkt { get; set; }
        
        // Kobling til byen forestillingen foregår i.
        public int ById { get; set; }
        public By By { get; set; } = null!;
        
        // Antal pladser vi har i alt.
        public int TotalKapacitet { get; set; } = 150;
        // Antal VIP-pladser.
        public int VIPKapacitet { get; set; } = 10;
        
        // Lister med relationer til artister og reservationer.
        public List<Artist> Artister { get; set; } = new();
        public List<Reservation> Reservationer { get; set; } = new();
        
        // Regner ud hvor mange pladser der allerede er reserveret.
        public int AntalReserveredePladser
        {
            get
            {
                if (Reservationer == null)
                {
                    return 0;
                }
                
                int total = 0;
                foreach (var reservation in Reservationer)
                {
                    total += reservation.AntalBilletter;
                }
                return total;
            }
        }
        
        // Regner ud hvor mange normale pladser der er tilbage.
        public int LedigePladser
        {
            get
            {
                return TotalKapacitet - AntalReserveredePladser;
            }
        }
        
        // Regner ud hvor mange VIP-pladser der er reserveret.
        public int AntalReserveredeVIPPladser
        {
            get
            {
                if (Reservationer == null)
                {
                    return 0;
                }
                
                int total = 0;
                foreach (var reservation in Reservationer)
                {
                    if (reservation.Billettype == Billettype.VIP)
                    {
                        total += reservation.AntalBilletter;
                    }
                }
                return total;
            }
        }
        
        // Regner ud hvor mange VIP-pladser der er tilbage.
        public int LedigeVIPPladser
        {
            get
            {
                return VIPKapacitet - AntalReserveredeVIPPladser;
            }
        }
    }
}
