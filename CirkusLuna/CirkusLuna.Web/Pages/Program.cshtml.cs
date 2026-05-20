using Microsoft.AspNetCore.Mvc.RazorPages;
using CirkusLuna.Core.Interfaces;
using CirkusLuna.Core.Models;

namespace CirkusLuna.Web.Pages
{
    public class ProgramModel : PageModel
    {
        private readonly IForestillingService _forestillingService;

        public ProgramModel(IForestillingService forestillingService)
        {
            _forestillingService = forestillingService;
        }

        public List<Forestilling> Forestillinger { get; set; } = new();
        public string? Search { get; set; }

        public void OnGet(string? search)
        {
            Search = search;
            
            if (!string.IsNullOrWhiteSpace(search))
            {
                Forestillinger = _forestillingService.SearchByCity(search);
            }
            else
            {
                Forestillinger = _forestillingService.GetUpcomingForestillinger();
            }
        }
    }
}
