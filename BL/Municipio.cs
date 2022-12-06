using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public class Municipio 
    {
        public static ML.Result GetByIdEstado(int IdMunicipio)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL.JjuarezProgramacionNcapasContext context = new DL.JjuarezProgramacionNcapasContext())
                {
                    var usuarios = context.Municipios.FromSqlRaw($"MunicipioGetByIdEstado {IdMunicipio}").AsEnumerable().ToList();
                   
                    result.Objects = new List<object>();
                    if (usuarios != null)
                    {
                        foreach (var objMunicipio in usuarios)
                        {

                            ML.Municipio municipio = new ML.Municipio();
                            municipio.IdMunicipio = objMunicipio.IdMunicipio;
                            municipio.Nombre = objMunicipio.Nombre;



                            result.Objects.Add(municipio);

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
