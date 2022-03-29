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
    public class ProgrammController : ControllerBase
    {
        private readonly PlanerixContext _context;

        public ProgrammController(PlanerixContext context)
        {
            _context = context;
        }
        [HttpGet]
        public async Task<IActionResult> GetProgramm()
        {
            var programm = _context.Programms.ToList();
            return Ok(programm);
        }
    }
}
