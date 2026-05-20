using Microsoft.AspNetCore.Mvc.RazorPages;
using CirkusLuna.Core.Interfaces;
using CirkusLuna.Core.Models;

namespace CirkusLuna.Web.Pages
{
    public class IndexModel : PageModel
    {
        private readonly IForestillingService _forestillingService;

        public IndexModel(IForestillingService forestillingService)
        {
            _forestillingService = forestillingService;
        }

        public int AntalForestillinger { get; set; }
        public Forestilling? NaesteForestilling { get; set; }

        public void OnGet()
        {
            var upcoming = _forestillingService.GetUpcomingForestillinger();
            AntalForestillinger = upcoming.Count;
            NaesteForestilling = upcoming.OrderBy(f => f.Dato).FirstOrDefault();
        }
    }
}
