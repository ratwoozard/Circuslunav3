namespace CirkusLuna.Core.Models
{
    /// <summary>
    /// En kunde som kan lave reservationer.
    /// </summary>
    public class Kunde
    {
        public int Id { get; set; }
        public string Navn { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Telefon { get; set; } = string.Empty;
        
        // Alle reservationer kunden har lavet.
        public List<Reservation> Reservationer { get; set; } = new();
    }
}
