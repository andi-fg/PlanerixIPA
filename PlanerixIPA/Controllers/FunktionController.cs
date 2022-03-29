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
    public class FunktionController : ControllerBase
    {
        private readonly PlanerixContext _context;
        public FunktionController(PlanerixContext context)
        {
            _context = context;
        }
        [HttpGet]
        public async Task<IActionResult> GetFunktion()
        {
            var funktion = _context.Funktions.ToList();
            return Ok(funktion);
        }
    }
}
