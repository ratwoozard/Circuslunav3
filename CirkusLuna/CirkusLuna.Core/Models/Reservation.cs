namespace CirkusLuna.Core.Models
{
    /// <summary>
    /// En reservation af billetter til en forestilling.
    /// </summary>
    public class Reservation
    {
        public int Id { get; set; }
        
        // Kobling til den kunde der har lavet reservationen.
        public int KundeId { get; set; }
        public Kunde Kunde { get; set; } = null!;
        
        // Kobling til forestillingen der bliver reserveret til.
        public int ForestillingId { get; set; }
        public Forestilling Forestilling { get; set; } = null!;
        
        // Selve reservationsdata.
        public int AntalBilletter { get; set; }
        public Billettype Billettype { get; set; }
        public DateTime ReservationsDato { get; set; }
        
        // Samlet pris = pris pr. billet gange antal billetter.
        public decimal TotalPris
        {
            get
            {
                return BillettypePris.GetPris(Billettype) * AntalBilletter;
            }
        }
    }
}
