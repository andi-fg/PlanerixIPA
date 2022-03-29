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
    [Route("api/[controller]")]
    [ApiController]
    public class MitarbeiterlisteController : ControllerBase
    {
        private readonly PlanerixContext _context;
        public MitarbeiterlisteController(PlanerixContext context)
        {
            _context = context;
        }
        [HttpGet]
        public async Task<IActionResult> GetMitarbeiterliste(DateTime? datum, [FromQuery] string[]abteilungen, [FromQuery] string[]programme, [FromQuery] string[]funktionen, string name, string vorname)
        {
            if(datum == null)
            {
                return new BadRequestObjectResult("Stichpunkttag ist ein Pflichtfeld");
            }
            var mitarbeiterliste = _context.Mitarbeiters.Where(mit => datum.Value.Date >= mit.Eintritt.Value.Date &&(mit.Austritt == null || datum.Value.Date <= mit.Austritt.Value.Date))
                    .Include(mit => mit.AbteilungMitarbeiters).ThenInclude(am => am.Abteilung)
                    .Include(mit => mit.ProgrammMitarbeiters).ThenInclude(pm => pm.Programm)
                    .Include(mit => mit.FunktionMitarbeiters).ThenInclude(fm => fm.Funktion).ToList();
            //Nach Name sortieren
            if(name != null)
            {
                mitarbeiterliste = mitarbeiterliste.Where(mit => mit.Name.Equals(name)).ToList();
            }
            //Nach Vorname sortieren
            if (vorname != null)
            {
                mitarbeiterliste = mitarbeiterliste.Where(mit => mit.Vorname.Equals(vorname)).ToList();
            }
            //Nach Abteilung sortieren
            if(abteilungen.Length > 0)
            {
                mitarbeiterliste = mitarbeiterliste.Where(mit => mit.AbteilungMitarbeiters.Any(am => abteilungen.Any(abt => am.Abteilung.Bezeichnung.Equals(abt)))).ToList();
            }
            //Nach Funktionen sortieren
            if (funktionen.Length > 0)
            {
                mitarbeiterliste = mitarbeiterliste.Where(mit => mit.FunktionMitarbeiters.Any(fm => funktionen.Any(funk => fm.Funktion.Bezeichnung.Equals(funk)))).ToList();
            }
            //Nach Programme sortieren
            if (programme.Length > 0)
            {
                mitarbeiterliste = mitarbeiterliste.Where(mit => mit.ProgrammMitarbeiters.Any(pm => programme.Any(prog => pm.Programm.Bezeichnung.Equals(prog)))).ToList();
            }
            if(mitarbeiterliste.Count() == 0)
            {
                return new BadRequestObjectResult("Keine Mitarbeitende zu diesen Parametern");
            }
            //ViewModel erstellen
            List<MitarbeiterlisteViewModel> mlvml = new List<MitarbeiterlisteViewModel>();
            foreach(Mitarbeiter m in mitarbeiterliste)
            {
                MitarbeiterlisteViewModel mlvm = new MitarbeiterlisteViewModel();
                mlvm.Name = m.Name;
                mlvm.Vorname = m.Vorname;
                mlvm.Benutzername = m.Kurzzeichen;
                mlvm.Email = m.Email;
                mlvm.Eintritt = m.Eintritt.Value.Day + "." + m.Eintritt.Value.Month + "." + m.Eintritt.Value.Year;
                if (m.Austritt != null)
                {
                    mlvm.Austritt = m.Austritt.Value.Day + "." + m.Austritt.Value.Month + "." + m.Austritt.Value.Year;
                }
                else
                {
                    mlvm.Austritt = "-";
                }
                var abteilung = new List<string>();
                foreach (AbteilungMitarbeiter am in m.AbteilungMitarbeiters)
                {
                    abteilung.Add(am.Abteilung.Bezeichnung);
                }
                mlvm.Abteilungen = abteilung;
                var funktion = new List<string>();
                foreach (FunktionMitarbeiter fm in m.FunktionMitarbeiters)
                {
                    funktion.Add(fm.Funktion.Bezeichnung);
                }
                mlvm.Funktionen = funktion;
                var programm = new List<string>();
                foreach (ProgrammMitarbeiter pm in m.ProgrammMitarbeiters)
                {
                    programm.Add(pm.Programm.Bezeichnung);
                }
                mlvm.Programme = programm;
                mlvml.Add(mlvm);
            }
            return Ok(mlvml);
        }
    }
}
