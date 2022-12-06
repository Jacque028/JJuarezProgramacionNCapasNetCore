using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ML
{
    public class Usuario 
    {

        //Propiedades  
        public int IdUsuario { get; set; }
        //[Required]   comenta estos 
        //[StringLength(20)]
        public string? Nombre { get; set; } 
        //[Required]
        //[StringLength(20)]
        //[DisplayName("ApellidoPaterno")]
        public string? ApellidoPaterno { get; set; }
        //[StringLength(20)]
        //[DisplayName("ApellidoMaterno")]
        public string? ApellidoMaterno { get; set; }
        //[Required]
        //[DataType(DataType.Date)]
        //[DisplayName("FechaNacimiento")]
        public string? FechaNacimiento { get; set; }
        //[Required]
        public string? UserName { get; set; }
        //[Required]
        //[StringLength(10)]
        public string? Password { get; set; }
        //[Required]
        public string? Sexo { get; set; }
        //[Required]
        //[StringLength(15)]
        public string? Telefono { get; set; }
        //[Required]
        //[StringLength(15)]
        public string? Celular { get; set; }
        //[Required]
        //[StringLength(50)]
        public string? Curp { get; set; }
        //[Required]
        //[DataType(DataType.EmailAddress)]
        //[EmailAddress]
        public string? Email { get; set; }

        public string? Imagen { get; set; } 
        public bool Status { get; set; }

        public List<object>? Usuarios { get; set; }


        //Propiedad de Navegación de rol// 
        public ML.Rol? Rol { get; set; }
        //Propiedad de Navegacion dirección
        public ML.Direccion? Direccion { get; set; }


        //Propiedad de Navegacion Pais 
        //public ML.Pais Pais { get; set; }


    }
}
