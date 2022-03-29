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
            var mitarbeiterliste = _context.Mitarbeiters.Where(mit => datum.Value.Date >= mit.Eintritt.Value.Date &&(mit.Austritt == null || datum.Value.Date <= mit.Austritt.Value.Date)
                     && (name == null || mit.Name.Equals(name)) //Name filtern
                     && (vorname == null || mit.Vorname.Equals(vorname)) //Vorname filtern
                     && (abteilungen.Length == 0 || mit.AbteilungMitarbeiters.Any(am => abteilungen.Any(abt => abt.Equals(am.Abteilung.Bezeichnung)))) //Abteilung filtern
                     && (programme.Length == 0 || mit.ProgrammMitarbeiters.Any(pm => programme.Any(prog => prog.Equals(pm.Programm.Bezeichnung)))) //Programm filtern
                     && (funktionen.Length == 0 || mit.FunktionMitarbeiters.Any(fm => funktionen.Any(funk => funk.Equals(fm.Funktion.Bezeichnung))))) //Funktion filtern
                    .Include(mit => mit.AbteilungMitarbeiters).ThenInclude(am => am.Abteilung)
                    .Include(mit => mit.ProgrammMitarbeiters).ThenInclude(pm => pm.Programm)
                    .Include(mit => mit.FunktionMitarbeiters).ThenInclude(fm => fm.Funktion).ToList();
            if (mitarbeiterliste.Count == 0)
            {
                return new BadRequestObjectResult("Keine Mitarbeitende zu diesen Parametern.");
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
