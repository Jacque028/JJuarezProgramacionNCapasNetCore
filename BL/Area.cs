using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public class Area 
    {
        public static ML.Result GetAll()
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL.JjuarezProgramacionNcapasContext context = new DL.JjuarezProgramacionNcapasContext())
                {
                    var productos = context.Areas.FromSqlRaw("AreaGetAll").ToList();

                    result.Objects = new List<object>();
                    if (productos != null)
                    {
                        foreach (var objArea in productos)
                        {

                            ML.Area area = new ML.Area();
                            area.IdArea = objArea.IdArea;
                            area.Nombre = objArea.Nombre;

                            result.Objects.Add(area);

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
