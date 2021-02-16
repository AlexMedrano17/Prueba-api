using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace prueba_api.Models
{
    [Table("Usuario")]
    [Index(nameof(Cedula), Name = "UQ__Usuario__06BB84488512F2AA", IsUnique = true)]
    public partial class Usuario
    {
        [Key]
        [Column("ID")]
        public int Id { get; set; }
        [Required]
        [Column("NOMBRE")]
        [StringLength(50)]
        public string Nombre { get; set; }
        [Required]
        [Column("APELLIDO")]
        [StringLength(50)]
        public string Apellido { get; set; }
        [Required]
        [Column("CEDULA")]
        [StringLength(50)]
        public string Cedula { get; set; }
        [Required]
        [Column("SEXO")]
        [StringLength(50)]
        public string Sexo { get; set; }
        [Required]
        [Column("EDAD")]
        [StringLength(50)]
        public string Edad { get; set; }
        [Required]
        [Column("CORREO_ELECTRONICO")]
        [StringLength(100)]
        public string CorreoElectronico { get; set; }
        [Required]
        [Column("CONTRASENA")]
        [StringLength(100)]
        public string Contrasena { get; set; }
        [Required]
        [Column("DIRECCION")]
        [StringLength(150)]
        public string Direccion { get; set; }
        [Required]
        [Column("LUGAR_NACIMIENTO")]
        [StringLength(150)]
        public string LugarNacimiento { get; set; }
        [Column("STATUS")]
        [StringLength(50)]
        public string Status { get; set; }
    }
}
