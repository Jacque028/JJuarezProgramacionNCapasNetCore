﻿using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public class Pais 
    {
        public static ML.Result GetAll()
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL.JjuarezProgramacionNcapasContext context = new DL.JjuarezProgramacionNcapasContext())
                {
                   
                    var paises = context.Pais.FromSqlRaw("PaisGetAll").ToList();
                    result.Objects = new List<object>();

                    if (paises != null)
                    {
                        foreach (var objPais in paises)
                        {

                            ML.Pais pais = new ML.Pais();
                            pais.IdPais = objPais.IdPais;
                            pais.Nombre = objPais.Nombre;

                            result.Objects.Add(pais);

                        }
                        result.Correct = true;
                    }
                    else
                    {
                        result.Correct = false;
                        result.ErrorMessage = "No se ha podido realizar la consulta";

                    }
                }
            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.ErrorMessage = ex.Message;
            }
            return result;
        }

    }
}
