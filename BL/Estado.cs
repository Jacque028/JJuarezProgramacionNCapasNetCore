using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public class Estado 
    {
        public static ML.Result GetByIdPais(int IdEstado)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL.JjuarezProgramacionNcapasContext context = new DL.JjuarezProgramacionNcapasContext())
                {
                    var usuarios = context.Estados.FromSqlRaw($"EstadoGetByIdPais {IdEstado}").AsEnumerable().ToList();
                    result.Objects = new List<object>();
                    if (usuarios != null)
                    {
                        foreach (var objEstado in usuarios)
                        {

                            ML.Estado estado = new ML.Estado();
                            estado.IdEstado = objEstado.IdEstado;
                            estado.Nombre = objEstado.Nombre;



                            result.Objects.Add(estado);

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
