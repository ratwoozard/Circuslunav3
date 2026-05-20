namespace CirkusLuna.Core.Models
{
    /// <summary>
    /// Lille hjælpeklasse til at finde billetpris ud fra type.
    /// </summary>
    public static class BillettypePris
    {
        public static decimal GetPris(Billettype type)
        {
            // Normal billet koster 120 kr.
            if (type == Billettype.Normal)
            {
                return 120m;
            }

            // Børnebillet koster 80 kr.
            if (type == Billettype.Barn)
            {
                return 80m;
            }

            // VIP billet koster 250 kr.
            if (type == Billettype.VIP)
            {
                return 250m;
            }

            // Hvis typen ikke kendes, returnerer vi 0.
            return 0m;
        }
    }
}
