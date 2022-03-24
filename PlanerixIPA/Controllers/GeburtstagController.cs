using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PlanerixIPA.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PlanerixIPA.Controllers
{
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
            //29.Fall
            bis = bis.Value.AddDays(1);
            if (von.Value.Date > bis.Value.Date)
            {
                return new BadRequestObjectResult("Von muss kleiner oder gleich als bis sein.");
            }
            //Filtern der Daten
            var mitarbeier = new List<Mitarbeiter>();
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
                var jahrGeburtstag = _context.Mitarbeiters.Where(mit => mit.Geburtsdatum.Value.DayOfYear >= von.Value.DayOfYear && mit.Geburtsdatum.Value.DayOfYear < bisFilter.DayOfYear)
                    .OrderBy(mit => mit.Geburtsdatum.Value.Month).OrderBy(mit => mit.Geburtsdatum.Value.Day).ToList();
                mitarbeier.AddRange(jahrGeburtstag);
            }
            
           // HashSet<Mitarbeiter> mitarbeiter = new HashSet<Mitarbeiter>();

            return Ok(mitarbeier);
        }
    }
}
