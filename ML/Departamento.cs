﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ML
{
    public class Departamento
    {
        public int IdDepartamento { get; set; }
        public string Nombre { get; set; }

        //Lista 
        public List<object> Departamentos { get; set; }

        //Propiedad de navegacion
        public ML.Area Area { get; set; }
    }
}
