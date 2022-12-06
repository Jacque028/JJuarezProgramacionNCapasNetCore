using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public class Rol 
    {
        public static ML.Result GetAll()
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL.JjuarezProgramacionNcapasContext context = new DL.JjuarezProgramacionNcapasContext())
                {
                    var usuarios = context.Rols.FromSqlRaw("RolGetAll").ToList();
                    result.Objects = new List<object>();
                    
                    if (usuarios != null)
                    {
                        foreach (var objRol in usuarios)
                        {

                            ML.Rol rol = new ML.Rol();
                            rol.IdRol = objRol.IdRol;
                            rol.Nombre = objRol.Nombre;

                            result.Objects.Add(rol);

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
