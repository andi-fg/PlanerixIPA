using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PlanerixIPA.Models;
using PlanerixIPA.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PlanerixIPA.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EinAustrittmonatController : ControllerBase
    {
        private readonly PlanerixContext _context;
        public EinAustrittmonatController(PlanerixContext context)
        {
            _context = context;
        }
        [HttpGet]
        public async Task<IActionResult> GetEinAustrittMonat(DateTime? von, DateTime? bis, [FromQuery] string[] abteilungen)
        {
            //überprüfen ob Pflichfelder angegeben
            if (von == null || bis == null)
            {
                return new BadRequestObjectResult("Von und bis dürfen nicht null sein.");
            }
            if (von.Value.Date > bis.Value.Date)
            {
                return new BadRequestObjectResult("Von darf nicht später als Bis sein.");
            }
            List<EinAustrittmonatViewModel> eavml = new List<EinAustrittmonatViewModel>();
            //Nach monat suchen
            for (int jahr = von.Value.Year; jahr <= bis.Value.Year; jahr++)
            {
                var bisFilter = bis.Value;
                if (bisFilter.Year != jahr)
                {
                    bisFilter = new DateTime(jahr, 12, 31);
                }
                //Monate durchgehen
                for(int monat = von.Value.Month; monat <= bisFilter.Month; monat++)
                {
                    EinAustrittmonatViewModel eavm = new EinAustrittmonatViewModel();
                    var bisMonatEnde = bisFilter;
                    if(bisMonatEnde.Month != monat)
                    {
                        bisMonatEnde = new DateTime(jahr, monat + 1, 1).AddDays(-1);
                    }
                    var eintritte = _context.Mitarbeiters.Where(mit => mit.Eintritt.Value.Date >= von.Value.Date && mit.Eintritt.Value.Date <= bisMonatEnde.Date
                            && (abteilungen.Length == 0 || mit.AbteilungMitarbeiters.Any(am => abteilungen.Any(abt => abt.Equals(am.Abteilung.Bezeichnung))))).Count();
                    var austritte = _context.Mitarbeiters.Where(mit => mit.Austritt.Value.Date >= von.Value.Date && mit.Austritt.Value.Date <= bisMonatEnde.Date
                            && (abteilungen.Length == 0 || mit.AbteilungMitarbeiters.Any(am => abteilungen.Any(abt => abt.Equals(am.Abteilung.Bezeichnung))))).Count();
                    eavm.Austritte = austritte;
                    eavm.Eintritte = eintritte;
                    eavm.Monat = monat + "." + jahr;
                    if(monat != 12)
                    {
                        von = von.Value.AddMonths(1);
                    }
                    eavml.Add(eavm); 
                }
                von = new DateTime(jahr + 1, 1, 1);
            }
            return Ok(eavml);
        }
    }
}
