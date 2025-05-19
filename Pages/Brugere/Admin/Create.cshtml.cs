using Event_Applikation.Models;
using Event_Applikation.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Event_Applikation.Pages.Brugere.Admin
{
    public class CreateModel : PageModel
    {
		private IBrugerRepository _brugerRepo;
		private readonly mvp2_dk_db_eventapplikationContext _context;

		[BindProperty]
		public Bruger NyBruger { get; set; }

        public SelectList RolleList { get; set; }

        public SelectList CampusList { get; set; }

        public CreateModel(mvp2_dk_db_eventapplikationContext context, IBrugerRepository brugerrepo)
		{
			_context = context;
			_brugerRepo = brugerrepo;
		}

        /// <summary>
        /// Når Create-siden hentes kaldes metoden: "LoadRollerCampus", 
        /// så de kan benyttes på sidens dropdown.
        /// </summary>
        public void OnGet()
        {
			LoadRollerCampus();
        }

        /// <summary>
        /// Håndterer at kontrollere og indsende input, for at oprette en ny bruger.
        /// </summary>
        public IActionResult OnPostSubmit()
		{
			//Kontrol af gyldig data
			if (!ModelState.IsValid || NyBruger == null)
			{
				OnGet();
				return Page();
			}

			//Opretter en ny bruger og omdirigerer til Forsiden
			_brugerRepo.Create(NyBruger);
			return RedirectToPage("/Index");
        }

        /// <summary>
        /// Indlæser roller og Campus fra databasen, for at de kan anvendes
        /// i drop-down menu i applikationen. Kaldes i OnGet.
        /// </summary>
        public void LoadRollerCampus()
        {
            RolleList = new SelectList(_context.Rolles, "Id", "Brugertype");
            CampusList = new SelectList(_context.Campuses, "Id", "By");
        }
    }
}
