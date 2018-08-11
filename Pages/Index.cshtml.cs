using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Rechentrainer.Models;

namespace Rechentrainer.Pages
{
    public class IndexModel : PageModel
    {
        [BindProperty]
        public RechnenClass Rechnen { get; set; }

        public void OnGet()
        {
            Rechnen = new RechnenClass();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            Rechnen.Ergebnis.Update(Rechnen.GibMirPunkte(), Rechnen.GibMirGeschwindigkeit());
            Rechnen.ShowResult = true;
            return Page();
        }
    }
}
