using System.ComponentModel.DataAnnotations;

namespace prueba_api.Models.DTO
{
    public class AuthenticateRequestDTO
    {
        [Required]
        public string CorreoElectronico { get; set; }

        [Required]
        public string Contrasena { get; set; }
    }
}