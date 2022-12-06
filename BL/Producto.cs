using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public class Producto 
    {
        // ADD 
        public static ML.Result Add(ML.Producto producto)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL.JjuarezProgramacionNcapasContext context = new DL.JjuarezProgramacionNcapasContext())                
                {
                    var query = context.Database.ExecuteSqlRaw($"ProductoAdd '{producto.Nombre}', {producto.PrecioUnitario}, {producto.Stock}, {producto.Proveedor.IdProveedor}, {producto.Departamento.IdDepartamento}, '{producto.Descripcion}', '{producto.Imagen}'");

                    if (query > 0)
                    {
                        result.Correct = true;
                    }
                    else
                    {
                        result.Correct = false;
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


        // UPDATE 
        public static ML.Result Update(ML.Producto producto)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL.JjuarezProgramacionNcapasContext context = new DL.JjuarezProgramacionNcapasContext())
                {
                    var query = context.Database.ExecuteSqlRaw($"ProductoUpdate {producto.IdProducto}, '{producto.Nombre}', {producto.PrecioUnitario}, {producto.Stock}, {producto.Proveedor.IdProveedor}, {producto.Departamento.IdDepartamento}, '{producto.Descripcion}', '{producto.Imagen}'");
                    if (query > 0)
                    {
                        result.Correct = true;
                    }
                    else
                    {
                        result.Correct = false;
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


        // GETALL
        //public static ML.Result GetAll()
        //{
        //    ML.Result result = new ML.Result();
        //    try
        //    {
        //        using (DL.JjuarezProgramacionNcapasContext context = new DL.JjuarezProgramacionNcapasContext())
        //        {
        //            var query = context.Productos.FromSqlRaw("ProductoGetAll").ToList();

        //            result.Objects = new List<object>();

        //            if (query != null)
        //            {
        //                foreach (var item in query)
        //                {

        //                    ML.Producto producto = new ML.Producto();
        //                    producto.IdProducto = item.IdProducto;
        //                    producto.Nombre = item.Nombre;
        //                    producto.PrecioUnitario = item.PrecioUnitario;
        //                    producto.Stock = item.Stock;

        //                    producto.Proveedor = new ML.Proveedor();
        //                    producto.Proveedor.IdProveedor = item.IdProveedor.Value;
        //                    producto.Proveedor.Nombre = item.NombreProveedor;


        //                    producto.Departamento = new ML.Departamento();
        //                    producto.Departamento.IdDepartamento = item.IdDepartamento.Value;
        //                    producto.Departamento.Nombre = item.NombreDepartamento;

        //                    producto.Descripcion = item.Descripcion;
        //                    producto.Imagen = item.Imagen;  

        //                    result.Objects.Add(producto);
        //                }
        //                result.Correct = true;
        //            }
        //            else
        //            {
        //                result.Correct = false;
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        result.Correct = false;
        //        result.ErrorMessage = ex.Message;
        //        result.Ex = ex;

        //    }
        //    return result;
        //}



        


        // GETALL
        public static ML.Result GetAll(ML.Producto producto) //GETALL
        {
            ML.Result result = new ML.Result();
            try
            {
                //OPERADOR TERNARIO
                producto.Proveedor.IdProveedor = (byte)((producto.Proveedor.IdProveedor == null) ? 0 : producto.Proveedor.IdProveedor);
                using (DL.JjuarezProgramacionNcapasContext context = new DL.JjuarezProgramacionNcapasContext())
                {
                    var query = context.Productos.FromSqlRaw($"ProductoGetAll '{producto.Nombre}', {producto.Proveedor.IdProveedor}").ToList();

                    result.Objects = new List<object>();

                    if (query != null)
                    {
                        foreach (var item in query)
                        {

                            ML.Producto productoobj = new ML.Producto();
                            productoobj.IdProducto = item.IdProducto;
                            productoobj.Nombre = item.Nombre;
                            productoobj.PrecioUnitario = item.PrecioUnitario;
                            productoobj.Stock = item.Stock;

                            productoobj.Proveedor = new ML.Proveedor();
                            productoobj.Proveedor.IdProveedor = item.IdProveedor.Value;
                            productoobj.Proveedor.Nombre = item.NombreProveedor;


                            productoobj.Departamento = new ML.Departamento();
                            productoobj.Departamento.IdDepartamento = item.IdDepartamento.Value;
                            productoobj.Departamento.Nombre = item.NombreDepartamento;

                            productoobj.Descripcion = item.Descripcion;
                            productoobj.Imagen = item.Imagen;

                            result.Objects.Add(productoobj);
                        }
                        result.Correct = true;
                    }
                    else
                    {
                        result.Correct = false;
                    }
                }
            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.ErrorMessage = ex.Message;
                result.Ex = ex;

            }
            return result;
        }






        // GETBYID 
        public static ML.Result GetById(int IdProducto)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL.JjuarezProgramacionNcapasContext context = new DL.JjuarezProgramacionNcapasContext())                
                {

                    var objProducto = context.Productos.FromSqlRaw($"ProductoGetById {IdProducto}").AsEnumerable().FirstOrDefault();

                    result.Objects = new List<object>();

                    if (objProducto != null)
                    {
                        ML.Producto producto = new ML.Producto();
                        producto.IdProducto = objProducto.IdProducto;
                        producto.Nombre = objProducto.Nombre;
                        producto.PrecioUnitario = objProducto.PrecioUnitario;
                        producto.Stock = objProducto.Stock;

                        producto.Proveedor = new ML.Proveedor();
                        producto.Proveedor.IdProveedor = objProducto.IdProveedor.Value;
                        producto.Proveedor.Nombre = objProducto.NombreProveedor;


                        producto.Departamento = new ML.Departamento();
                        producto.Departamento.IdDepartamento = objProducto.IdDepartamento.Value;
                        producto.Departamento.Nombre = objProducto.NombreDepartamento;

                        producto.Descripcion = objProducto.Descripcion; 
                        producto.Imagen = objProducto.Imagen;



                        result.Object = producto;

                        result.Correct = true;
                    }
                    else
                    {
                        result.Correct = false;
                    }
                }
            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.ErrorMessage = ex.Message;
                result.Ex = ex;
            }
            return result;
        }


        // DELETE 
        public static ML.Result Delete(int idProducto)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL.JjuarezProgramacionNcapasContext context = new DL.JjuarezProgramacionNcapasContext())
                {
                    int query = context.Database.ExecuteSqlRaw($"UsuarioDelete '{idProducto}'");

                    if (query > 0)
                    {
                        result.ErrorMessage = "Se elimino el producto correctamente";
                    }
                }
                result.Correct = true;
            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.Ex = ex;
                result.ErrorMessage = "Ocurrio un error al eliminar el producto" + result.Ex;
            }
            return result;
        }





    }
}
