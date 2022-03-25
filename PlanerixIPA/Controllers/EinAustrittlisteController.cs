using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
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
    public class EinAustrittlisteController : ControllerBase
    {
        private readonly PlanerixContext _context;

        public EinAustrittlisteController(PlanerixContext context)
        {
            _context = context;
        }
        [HttpGet]
        //Link : api/geburtstag?von=2022-03-28&bis=2022-03-30&abteilungen=Kurhotel im Park&abteilungen=Service
        public async Task<IActionResult> GetEinAustrittliste(DateTime? von, DateTime? bis, [FromQuery] string[] abteilungen)
        {
            //überprüfen ob Pflichfelder angegeben
            if (von == null || bis == null)
            {
                return new BadRequestObjectResult("Von und bis dürfen nicht null sein.");
            }
            if (von.Value.Date > bis.Value.Date)
            {
                return new BadRequestObjectResult("Von muss kleiner oder gleich Bis sein.");
            }
            //Nach den Ein- und Austritte in diesem Zeitpunkt filtern
            var mitarbeiter = _context.Mitarbeiters.Where(mit => mit.Eintritt.Value.Date >= von.Value.Date && mit.Eintritt.Value.Date <= bis.Value.Date
                        || mit.Austritt.Value.Date >= von.Value.Date && mit.Austritt.Value.Date <= bis.Value.Date)
                        .Include(mit => mit.AbteilungMitarbeiters).ThenInclude(am => am.Abteilung).ToList();
            //Abteilungen überprüfen
            if(abteilungen.Length > 0)
            {
                mitarbeiter = mitarbeiter.Where(mit => mit.AbteilungMitarbeiters.Any(am => abteilungen.Any(abt => abt == am.Abteilung.Bezeichnung))).ToList();
            }
            if (mitarbeiter.Count == 0)
            {
                return new BadRequestObjectResult("Keine Daten gefunden.");
            }
            //ViewModel erstellen für Ausgabe
            List<EinAustrittlisteViewModel> eavml = new List<EinAustrittlisteViewModel>();
            foreach(Mitarbeiter m in mitarbeiter){
                EinAustrittlisteViewModel eavm = new EinAustrittlisteViewModel();
                eavm.Name = m.Name;
                eavm.Vorname = m.Vorname;
                eavm.Eintritt = m.Eintritt.Value.Day + "." + m.Eintritt.Value.Month + "." + m.Eintritt.Value.Year;
                if (m.Austritt != null)
                {
                    eavm.Austritt = m.Austritt.Value.Day + "." + m.Austritt.Value.Month + "." + m.Austritt.Value.Year;
                }
                else
                {
                    eavm.Austritt = "-";
                }
                List<string> abteilung = new List<string>();
                foreach(AbteilungMitarbeiter am in m.AbteilungMitarbeiters)
                {
                    abteilung.Add(am.Abteilung.Bezeichnung);
                }
                eavm.Abteilungen = abteilung;
                eavml.Add(eavm);
            }
            return Ok(eavml);
        }
    }
}
