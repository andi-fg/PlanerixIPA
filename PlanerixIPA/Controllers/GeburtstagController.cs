using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PlanerixIPA.Models;
using PlanerixIPA.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PlanerixIPA.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class GeburtstagController : ControllerBase
    {
        private readonly PlanerixContext _context;
        public GeburtstagController(PlanerixContext context)
        {
            _context = context;
        }
        [HttpGet]
        //Link : api/geburtstag?von=2022-03-28&bis=2022-03-30&abteilungen=Kurhotel im Park&abteilungen=Service
        public async Task<IActionResult> GetGeburtstagsliste(DateTime? von, DateTime? bis,[FromQuery] string[]abteilungen)
        {
            //überprüfen ob Pflichfelder angegeben
            if(von == null || bis == null)
            {
                return new BadRequestObjectResult("Von und bis dürfen nicht null sein.");
            }
            if (von.Value.Date > bis.Value.Date)
            {
                return new BadRequestObjectResult("Von muss kleiner oder gleich Bis sein.");
            }
            //Filtern der Daten
            var mitarbeiter = new List<GeburtstagslisteViewModel>();
            for(int jahr = von.Value.Year; jahr <= bis.Value.Year; jahr++)
            {
                //Wenn bis in einem anderen Jahr
                var bisFilter = new DateTime();
                if (bis.Value.Year != jahr)
                {
                    bisFilter = new DateTime(jahr, 12, 31);
                }
                else
                {
                    bisFilter = bis.Value;
                }
                //Geburtstage heruasfinden
                var jahrGeburtstag = _context.Mitarbeiters.Where(mit => (mit.Geburtsdatum.Value.Month > von.Value.Month || (mit.Geburtsdatum.Value.Day >= von.Value.Day && mit.Geburtsdatum.Value.Month == von.Value.Month))
                    && (mit.Geburtsdatum.Value.Month < bisFilter.Month || (mit.Geburtsdatum.Value.Day <= bisFilter.Day && mit.Geburtsdatum.Value.Month == bisFilter.Month)))
                    .OrderBy(mit => mit.Geburtsdatum.Value.Month).ThenBy(mit => mit.Geburtsdatum.Value.Day)
                    .Include(mit => mit.AbteilungMitarbeiters).ThenInclude(am => am.Abteilung).ToList();
                //Abteilungern heruasfinden
                if(abteilungen.Length > 0)
                {
                    jahrGeburtstag = jahrGeburtstag.Where(mit => mit.AbteilungMitarbeiters.Any(am => abteilungen.Any(abt => abt == am.Abteilung.Bezeichnung))).ToList();
                }
                mitarbeiter.AddRange(GetMitarbeiterAlsViewModel(jahrGeburtstag,jahr));
                von = new DateTime(jahr + 1, 01, 01);
            }
            if(mitarbeiter.Count == 0)
            {
                return new BadRequestObjectResult("Keine Geburtstage gefunden.");
            }
            return Ok(mitarbeiter);
        }
        //View Model generieren für ausgabe
        private List<GeburtstagslisteViewModel> GetMitarbeiterAlsViewModel(List<Mitarbeiter> mitarbeiter, int jahr)
        {
            List<GeburtstagslisteViewModel> gvml = new List<GeburtstagslisteViewModel>();
            foreach(Mitarbeiter m in mitarbeiter)
            {
                var gvm = new GeburtstagslisteViewModel();
                gvm.Name = m.Name;
                gvm.Vorname = m.Vorname;
                gvm.Datum = m.Geburtsdatum.Value.Day + "." + m.Geburtsdatum.Value.Month + "." + jahr;
                gvm.Alter = jahr - m.Geburtsdatum.Value.Year;
                var abteilungen = new List<string>();
                foreach (AbteilungMitarbeiter am in m.AbteilungMitarbeiters)
                {
                    abteilungen.Add(am.Abteilung.Bezeichnung);
                }
                gvm.Abteilungen = abteilungen;
                gvml.Add(gvm);
            }
            return gvml;
        }
    }
}
