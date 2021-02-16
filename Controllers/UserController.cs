using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using agenda_web_api.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using prueba_api.Data;
using prueba_api.Models;
using prueba_api.Models.DTO;

namespace prueba_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly PruebaContext _context;
        private readonly IUserService _userService;

        public UserController(PruebaContext context, IUserService userService)
        {
            _context = context;
            _userService = userService;
        }

        [HttpPost("authenticate")]
        public IActionResult Authenticate(AuthenticateRequestDTO model)
        {
            var response = _userService.Authenticate(model);

            if (response == null)
                return BadRequest(new { message = "El correo o la contraseña es incorrecta" });

            return Ok(response);
        }

        // GET: api/User
        [Authorize]
        [HttpGet]
        public ActionResult<UsuarioDTO[]> Get()
        {
            var users = _context.Usuarios.ToList();

            var usersDTO = users.Select(u => new UsuarioDTO 
            {
                               Id = u.Id,
                           Nombre = u.Nombre,
                         Apellido = u.Apellido,
                           Cedula = u.Cedula,
                             Sexo = u.Sexo,
                             Edad = u.Edad,
                CorreoElectronico = u.CorreoElectronico,
                       Contrasena = u.Contrasena,
                        Direccion = u.Direccion,
                  LugarNacimiento = u.LugarNacimiento,
                           Status = u.Status
            });

            return Ok(usersDTO);
        }

        // GET: api/User/5
        [Authorize]
        [HttpGet("{id}", Name = "Get")]
        public ActionResult<UsuarioDTO> Get(int id)
        {
            var user = _context.Usuarios.Find(id);

            if (user == null) return NotFound();

            var userDTO = new UsuarioDTO 
            {
                               Id = user.Id,
                           Nombre = user.Nombre,
                         Apellido = user.Apellido,
                           Cedula = user.Cedula,
                             Sexo = user.Sexo,
                             Edad = user.Edad,
                CorreoElectronico = user.CorreoElectronico,
                       Contrasena = user.Contrasena,
                        Direccion = user.Direccion,
                  LugarNacimiento = user.LugarNacimiento,
                           Status = user.Status
            };

            return Ok(userDTO);
        }

        // POST: api/User
        [Authorize]
        [HttpPost]
        public async Task<ActionResult<UsuarioDTO>> Post(UsuarioDTO userDto)
        {
            var user = new Usuario
            {
                           Nombre = userDto.Nombre,
                         Apellido = userDto.Apellido,
                           Cedula = userDto.Cedula,
                             Sexo = userDto.Sexo,
                             Edad = userDto.Edad,
                CorreoElectronico = userDto.CorreoElectronico,
                       Contrasena = userDto.Contrasena,
                        Direccion = userDto.Direccion,
                  LugarNacimiento = userDto.LugarNacimiento,
                           Status = userDto.Status
            };

            await _context.Usuarios.AddAsync(user);
            await _context.SaveChangesAsync();

            userDto.Id = user.Id;

            return CreatedAtAction(nameof(Get), new { id = userDto.Id }, userDto);
        }

        // PUT: api/User/5
        [Authorize]
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, UsuarioDTO userDto)
        {
            var userToUpdate =  await _context.Usuarios.FindAsync(id);

            if(userToUpdate == null) return NotFound();

            try
            {
                userToUpdate.Nombre = userDto.Nombre;
                userToUpdate.Apellido = userDto.Apellido;
                userToUpdate.Cedula = userDto.Cedula;
                userToUpdate.Sexo = userDto.Sexo;
                userToUpdate.Edad = userDto.Edad;
                userToUpdate.Direccion = userDto.Direccion;
                userToUpdate.LugarNacimiento = userDto.LugarNacimiento;
                userToUpdate.Status = userDto.Status;

                _context.Usuarios.Update(userToUpdate);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }

            return NoContent();
        }

        // DELETE: api/User/5
        [Authorize]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var userToDelete =  await _context.Usuarios.FindAsync(id);

            if(userToDelete == null) return NotFound();

            _context.Usuarios.Remove(userToDelete);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
