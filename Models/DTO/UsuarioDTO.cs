namespace prueba_api.Models.DTO
{
    public class UsuarioDTO
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string Cedula { get; set; }
        public string Sexo { get; set; }
        public string Edad { get; set; }
        public string CorreoElectronico { get; set; }
        public string Contrasena { get; set; }
        public string Direccion { get; set; }
        public string LugarNacimiento { get; set; }
        public string Status { get; set; }
    }
}