namespace CirkusLuna.Core.Models
{
    /// <summary>
    /// Represents a city where Cirkus Luna performs
    /// </summary>
    public class By
    {
        public int Id { get; set; }
        public string Navn { get; set; } = string.Empty;
        public string Region { get; set; } = string.Empty;
    }
}
