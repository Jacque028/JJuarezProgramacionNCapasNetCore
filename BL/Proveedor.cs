using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public class Proveedor 
    {
        public static ML.Result GetAll()
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL.JjuarezProgramacionNcapasContext context = new DL.JjuarezProgramacionNcapasContext())
                {
                    var productos = context.Proveedors.FromSqlRaw("ProveedorGetAll").ToList();
                    result.Objects = new List<object>();
                    if (productos != null)
                    {
                        foreach (var objProveedor in productos)
                        {

                            ML.Proveedor proveedor = new ML.Proveedor();
                            proveedor.IdProveedor = objProveedor.IdProveedor;
                            proveedor.Telefono = objProveedor.Telefono;
                            proveedor.Nombre = objProveedor.Nombre;

                            result.Objects.Add(proveedor);

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
