using Microsoft.AspNetCore.Authorization;
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
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class JubiläumController : ControllerBase
    {
        private readonly PlanerixContext _context;
        public JubiläumController(PlanerixContext context)
        {
            _context = context;
        }
        [HttpGet]
        public async Task<IActionResult> GetJubiläumsliste([FromQuery] string[] abteilungen)
        {
            var heute = DateTime.Now;
            var mitarbeiter = _context.Mitarbeiters.Where(mit => mit.Eintritt.Value.Date <= heute.Date && (mit.Austritt.Value.Date >= heute.Date || mit.Austritt == null)
                && (abteilungen.Length == 0 || mit.AbteilungMitarbeiters.Any(am => abteilungen.Any(abt => abt.Equals(am.Abteilung.Bezeichnung)))))
                .OrderBy(mit => mit.Eintritt.Value).ToList();
            if(mitarbeiter.Count == 0)
            {
                return new BadRequestObjectResult("Keine Mitarbeitende gefunden");
            }
            //ViewModel erstellen
            List<JubiläumViewModel> jvml = new List<JubiläumViewModel>();
            foreach(Mitarbeiter m in mitarbeiter)
            {
                JubiläumViewModel jvm = new JubiläumViewModel();
                jvm.Name = m.Name;
                jvm.Vorname = m.Vorname;
                jvm.Eintritt = m.Eintritt.Value.Day + "." + m.Eintritt.Value.Month + "." + m.Eintritt.Value.Year;
                var jahre = heute.Year - m.Eintritt.Value.Year;
                if(m.Eintritt.Value.Month > heute.Month || (m.Eintritt.Value.Month == heute.Month && m.Eintritt.Value.Day > heute.Day))
                {
                    jahre -= 1;
                }
                jvm.Dienstjahre = jahre;
                //Zeit bis nächstes Jubiläum
                var jubiläumsdatum = m.Eintritt.Value.AddYears(10);
                if (jahre >= 10)
                {
                    var jahreMultiplikator = jahre / 5 +1;
                    jubiläumsdatum = m.Eintritt.Value.AddYears(5 * jahreMultiplikator);
                }
                var unterschiedJahr = jubiläumsdatum.Year - heute.Year;
                var unterschiedMonat = jubiläumsdatum.Month - heute.Month;
                var unterschiedTag = jubiläumsdatum.Day - heute.Day;
                Console.WriteLine(jvm.Name);
                if (unterschiedTag < 0)
                {
                    unterschiedMonat--;
                    //Überprüfen ob heute grösser als letzter Tag des Monats ist
                    var d = heute.Day;
                    var differenzLetzterTag = 0;
                    var letzterTagMonat = new DateTime(jubiläumsdatum.Year, jubiläumsdatum.Month + 1, 01).AddDays(-1).Day;
                    if (letzterTagMonat < d)
                    {
                        differenzLetzterTag = d - letzterTagMonat;
                        d = letzterTagMonat;
                    }
                    var hilfsdatum = new DateTime(jubiläumsdatum.Year, jubiläumsdatum.Month, d);
                    hilfsdatum = hilfsdatum.AddMonths(-1).AddDays(differenzLetzterTag);
                    var differenz = jubiläumsdatum - hilfsdatum;
                    unterschiedTag = differenz.Days;
                }
                if (unterschiedMonat < 0)
                {
                    unterschiedJahr--;
                    unterschiedMonat += 12;
                }
                var labelTag = unterschiedTag == 1 ? "Tag" : "Tage";
                var labelMonat = unterschiedMonat == 1 ? "Monat" : "Monate";
                var labelJahr = unterschiedJahr == 1 ? "Jahr" : "Jahre";
                jvm.Nächstes = unterschiedJahr + " " + labelJahr + " " + unterschiedMonat + " " + labelMonat + " " + unterschiedTag + " " + labelTag;
                jvml.Add(jvm);
            }
            return Ok(jvml);
        }
    }
}
