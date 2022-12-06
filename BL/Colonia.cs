using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public class Colonia 
    {
        public static ML.Result GetByIdMunicipio(int IdColonia)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL.JjuarezProgramacionNcapasContext context = new DL.JjuarezProgramacionNcapasContext())
                {
                    var usuarios = context.Colonia.FromSqlRaw($"ColoniaGetByIdMunicipio {IdColonia}").AsEnumerable().ToList();
                    result.Objects = new List<object>();
                    if (usuarios != null)
                    {
                        foreach (var objColonia in usuarios)
                        {

                            ML.Colonia colonia = new ML.Colonia();
                            colonia.IdColonia = objColonia.IdColonia;
                            colonia.Nombre = objColonia.Nombre;
                            colonia.CodigoPostal = objColonia.CodigoPostal;




                            result.Objects.Add(colonia);

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
