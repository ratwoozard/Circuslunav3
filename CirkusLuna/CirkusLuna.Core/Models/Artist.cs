namespace CirkusLuna.Core.Models
{
    /// <summary>
    /// Represents a circus performer/artist
    /// </summary>
    public class Artist
    {
        public int Id { get; set; }
        public string Navn { get; set; } = string.Empty;
        public string Specialitet { get; set; } = string.Empty; // e.g., "Trapez", "Jonglør", "Klovn"
        
        // Navigation property for many-to-many relationship
        public List<Forestilling> Forestillinger { get; set; } = new();
    }
}
