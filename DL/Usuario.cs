using System;
using System.Collections.Generic;

namespace DL;

public partial class Usuario
{
    public int IdUsuario { get; set; }

    public string? Nombre { get; set; }

    public string? ApellidoPaterno { get; set; }

    public string? ApellidoMaterno { get; set; }

    public DateTime? FechaNacimiento { get; set; }

    public string? UserName { get; set; }

    public string Password { get; set; } = null!;

    public string Sexo { get; set; } = null!;

    public string Telefono { get; set; } = null!;

    public string? Celular { get; set; }

    public string? Curp { get; set; }

    public string Email { get; set; } = null!;

    public byte? IdRol { get; set; }

    public string? Imagen { get; set; }

    public bool? Status { get; set; }

    public virtual ICollection<Direccion> Direccions { get; } = new List<Direccion>();

    public virtual Rol? IdRolNavigation { get; set; }


    ////  A G R E G A D A S DL Usuario 

    //Agregadas
    public string NombreRol { get; set; } // ALIAS DE ROL NOMBRE  

    public int IdDireccion { get; set; } // TABLA DE DIRECCION 
    public string Calle { get; set; }
    public string NumeroInterior { get; set; }
    public string NumeroExterior { get; set; }


    public int IdColonia { get; set; } // TABLA DE COLONIA
    public string NombreColonia { get; set; } // ALIAS DE COLONIA NOMBRE 
    public string CodigoPostal { get; set; }

    public int IdMunicipio { get; set; } // TABLA DE MUNICIPIO
    public string NombreMunicipio { get; set; } // ALIAS DE MUNICIPIO NOMBRE  

    public int IdEstado { get; set; } // TABLA DE ESTADO
    public string NombreEstado { get; set; } // ALIAS DE ESTADO NOMBRE 

    public int IdPais { get; set; } // TABLA DE PAIS 
    public string NombrePais { get; set; } // ALIAS DE PAIS NOMBRE 

}
