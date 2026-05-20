using Microsoft.AspNetCore.Mvc.RazorPages;
using CirkusLuna.Core.Interfaces;
using CirkusLuna.Core.Models;

namespace CirkusLuna.Web.Pages
{
    public class ArtisterModel : PageModel
    {
        private readonly IArtistRepository _artistRepository;

        public ArtisterModel(IArtistRepository artistRepository)
        {
            _artistRepository = artistRepository;
        }

        public List<Artist> Artister { get; set; } = new();

        public void OnGet()
        {
            Artister = _artistRepository.GetAll();
        }
    }
}
