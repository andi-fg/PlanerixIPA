using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using PlanerixIPA.Models;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace PlanerixIPA.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly PlanerixContext _context;
        private readonly IConfiguration _configuration;
        public LoginController(PlanerixContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }
        [HttpPost]
        public async Task<IActionResult> Login(Benutzer b)
        {
            //Benutzer mit Benutzernamen suchen
            var benutzer = _context.Benutzers.Where(benutzer => benutzer.Benutzername.Equals(b.Benutzername)).FirstOrDefault();
            if (benutzer == null)
            {
                return new BadRequestObjectResult("Logindaten ungültig.");
            }
            //Passwort hashen und überprüfen
            var hashedPasswort = GetHash(b.Passwort, benutzer.Salt);
            if (!hashedPasswort.Equals(benutzer.Passwort))
            {
                return new BadRequestObjectResult("Logindaten ungültig.");
            }
            //Überprüfen ob User aktiv ist
            if (!benutzer.Aktiv)
            {
                return new BadRequestObjectResult("Benutzer ist nicht aktiv.");
            }
            //Logindaten OK Token generieren und zurückgeben
            var authClaims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Iss, _configuration["Jwt:ValidIssuer"]),
                new Claim(JwtRegisteredClaimNames.Email, benutzer.Email),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            };
            
            var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Secret"]));

            var token = new JwtSecurityToken(
                issuer: _configuration["JWT:ValidIssuer"],
                audience: _configuration["JWT:ValidAudience"],
                expires: DateTime.Now.AddHours(10),
                claims: authClaims,
                signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
             );
            var t = new JwtSecurityTokenHandler().WriteToken(token);
            return Ok(new
            {
                token = t,
                expiration = token.ValidTo
            });
        }
        //Passwort hashen mit bestehenden Salt
        public static string GetHash(string pw, byte[] salt)
        {
            return Convert.ToBase64String(KeyDerivation.Pbkdf2(pw, salt, KeyDerivationPrf.HMACSHA1, 10000, 32));
        }
    }
}
