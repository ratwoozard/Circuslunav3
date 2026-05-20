using Microsoft.AspNetCore.Mvc.RazorPages;
using CirkusLuna.Core.Interfaces;
using CirkusLuna.Core.Models;

namespace CirkusLuna.Web.Pages
{
    public class BekraeftelseModel : PageModel
    {
        private readonly IReservationRepository _reservationRepository;

        public BekraeftelseModel(IReservationRepository reservationRepository)
        {
            _reservationRepository = reservationRepository;
        }

        public Reservation? Reservation { get; set; }

        public void OnGet(int id)
        {
            Reservation = _reservationRepository.GetById(id);
        }
    }
}
