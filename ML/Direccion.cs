using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ML
{
    public class Direccion
    {
        public int IdDireccion { get; set; }
        //[Required]
        //[StringLength(50)]
        public string? Calle { get; set; }
        //[Required]
        //[StringLength(20)]
        public string? NumeroInterior { get; set; }
        //[Required]
        //[StringLength(20)]
        public string? NumeroExterior { get; set; }
        public List<object>? Direcciones { get; set; }

        //Propiedad de navegacion a Colonia
        public ML.Colonia? Colonia { get; set; }
    }
}
