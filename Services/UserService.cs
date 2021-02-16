using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using prueba_api.Models;
using prueba_api.Models.DTO;
using System.Threading.Tasks;
using prueba_api.Helpers;
using prueba_api.Data;

namespace agenda_web_api.Services
{
    public interface IUserService
    {
        AuthenticateResponseDTO Authenticate(AuthenticateRequestDTO model);
        IEnumerable<Usuario> GetAll();
        Usuario GetById(int id);
    }

    public class UserService : IUserService
    {
        
        private readonly AppSettings _appSettings;

        PruebaContext _context;
        public UserService(PruebaContext context, IOptions<AppSettings> appSettings){
            _context = context;
            _appSettings = appSettings.Value;
        }

        public AuthenticateResponseDTO Authenticate(AuthenticateRequestDTO model)
        {
            var user = _context.Usuarios.SingleOrDefault(x => x.CorreoElectronico == model.CorreoElectronico && x.Contrasena == model.Contrasena);

            if (user == null) return null;

            var token = generateJwtToken(user);

            return new AuthenticateResponseDTO(token);
        }

        public IEnumerable<Usuario> GetAll()
        {
            return _context.Usuarios.ToList();
        }

        public Usuario GetById(int id)
        {
            return _context.Usuarios.FirstOrDefault(x => x.Id == id);
        }

        
        private string generateJwtToken(Usuario user){
           
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[] { new Claim("id", user.Id.ToString()) }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}