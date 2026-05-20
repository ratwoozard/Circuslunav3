using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using CirkusLuna.Core.Interfaces;
using CirkusLuna.Core.Models;

namespace CirkusLuna.Web.Pages
{
    public class ReserverModel : PageModel
    {
        private readonly IForestillingService _forestillingService;
        private readonly IReservationService _reservationService;
        private readonly IKundeRepository _kundeRepository;

        public ReserverModel(
            IForestillingService forestillingService,
            IReservationService reservationService,
            IKundeRepository kundeRepository)
        {
            _forestillingService = forestillingService;
            _reservationService = reservationService;
            _kundeRepository = kundeRepository;
        }

        public Forestilling? Forestilling { get; set; }
        public string? ErrorMessage { get; set; }

        public void OnGet(int id)
        {
            Forestilling = _forestillingService.GetForestillingById(id);
        }

        public IActionResult OnPost(
            int id, 
            string kundeNavn, 
            string kundeEmail, 
            string kundeTelefon, 
            int billettype, 
            int antalBilletter)
        {
            Forestilling = _forestillingService.GetForestillingById(id);
            
            if (Forestilling == null)
            {
                return RedirectToPage("/Program");
            }

            try
            {
                // Create or find customer
                var kunde = new Kunde
                {
                    Navn = kundeNavn,
                    Email = kundeEmail,
                    Telefon = kundeTelefon
                };
                _kundeRepository.Add(kunde);

                var reservation = _reservationService.CreateReservation(
                    kunde.Id,
                    id,
                    antalBilletter,
                    (Billettype)billettype);

                return RedirectToPage("/Bekraeftelse", new { id = reservation.Id });
            }
            catch (Exception ex)
            {
                ErrorMessage = ex.Message;
                return Page();
            }
        }
    }
}
