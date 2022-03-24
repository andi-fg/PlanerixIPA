using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PlanerixIPA.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PlanerixIPA.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class AbteilungController : ControllerBase
    {
        private readonly PlanerixContext _context;

        public AbteilungController(PlanerixContext context)
        {
            _context = context;
        }
        [HttpGet]
        public async Task<IActionResult> GetAbteilung()
        {
            var abteilung = _context.Abteilungs.ToList();
            return Ok(abteilung);
        }
    }
}
