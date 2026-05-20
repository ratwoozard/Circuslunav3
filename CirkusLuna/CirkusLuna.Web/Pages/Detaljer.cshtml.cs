using Microsoft.AspNetCore.Mvc.RazorPages;
using CirkusLuna.Core.Interfaces;
using CirkusLuna.Core.Models;

namespace CirkusLuna.Web.Pages
{
    public class DetaljerModel : PageModel
    {
        private readonly IForestillingService _forestillingService;

        public DetaljerModel(IForestillingService forestillingService)
        {
            _forestillingService = forestillingService;
        }

        public Forestilling? Forestilling { get; set; }

        public void OnGet(int id)
        {
            Forestilling = _forestillingService.GetForestillingById(id);
        }
    }
}
