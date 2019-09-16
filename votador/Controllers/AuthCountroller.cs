using backend.Models;
using backend.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using votador.Helpers;
using votador.Request;

namespace votador.Controllers
{
    [ApiController]
    public class AuthCountroller : ControllerBase
    {
        private UsuarioService Service;
        private readonly IOptions<JWTSettings> config;

        public AuthCountroller(IOptions<JWTSettings> config)
        {
            this.Service = new UsuarioService();
            this.config = config;
        }
        [HttpPost]
        [Route("api/auth")]
        public IActionResult Authenticate([FromBody]UsuarioRequest usuario)
        {

            Usuario user = Service.Autenticar(usuario.Email, usuario.Senha);

            if (user == null)
                return BadRequest(new { message = "Username or password is incorrect" });

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = config.Value.Secret;

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, user.ID.ToString()),
                    new Claim(ClaimTypes.Role, user.Perfil.ToString())
                }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(Encoding.ASCII.GetBytes(key)), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);

            return Ok(new { token = tokenHandler.WriteToken(token), user });
        }
    }
}
