using Microsoft.AspNetCore.Mvc;

namespace PL.Controllers
{
    public class ProductoController : Controller
    {
        [HttpGet]
        public ActionResult GetAll()
        {
            ML.Producto producto = new ML.Producto();
            ML.Result result = new ML.Result();
            producto.Proveedor = new ML.Proveedor();

            ML.Result resultproveedor = BL.Proveedor.GetAll();
            result = BL.Producto.GetAll(producto);


            if (result.Correct)
            {
                producto.Productos = result.Objects;
                producto.Proveedor.Proveedores = resultproveedor.Objects;
            }
            else
            {
                ViewBag.Message = "Error al hacer la consulta :";
            }
            return View(producto);

         }

        [HttpPost]
        public ActionResult GetAll(ML.Producto producto)
        {
            ML.Result result = new ML.Result();

            ML.Result resultproveedor = BL.Proveedor.GetAll();

            result = BL.Producto.GetAll(producto);

            //ML.Producto producto = new ML.Producto();

            if (result.Correct)
            {
                producto.Productos = result.Objects;
                producto.Proveedor.Proveedores = resultproveedor.Objects;
            }
            else
            {
                ViewBag.Message = "Error al hacer la consulta :";
            }
            return View(producto);

        }


        [HttpGet]
        public ActionResult Form(int? IdProducto)
        {
            

            ML.Producto producto = new ML.Producto();

            ML.Result resultProveedor = BL.Proveedor.GetAll();
            producto.Proveedor = new ML.Proveedor();

            ML.Result resultDepartamento = BL.Departamento.GetAll();

            //ML.Departamento departamento = new ML.Departamento();
            producto.Departamento = new ML.Departamento();

            if (IdProducto == null)
            {
                producto.Proveedor.Proveedores = resultProveedor.Objects;
                producto.Departamento.Departamentos = resultDepartamento.Objects;
                return View(producto);
            }
            else
            {
                //GetById
                ML.Result result = BL.Producto.GetById(IdProducto.Value);

                if (result.Correct)
                {
                    producto = (ML.Producto)result.Object;
                    producto.Proveedor.Proveedores = resultProveedor.Objects;

                    producto.Departamento.Departamentos = resultDepartamento.Objects;

                }
                else
                {
                    ViewBag.Message = "Error al consultar el producto seleccionado";
                }
                return View(producto);
            }
        }

        [HttpPost]
        public ActionResult Form(ML.Producto producto)
        {
            IFormFile image = Request.Form.Files["IFImage"];


            //valido si traigo imagen
            if (image != null)
            {
                //llama al metodo que convierte a bytes la imagen 

                byte[] ImagenBytes = ConvertToBytes(image);
                
                //Se convierte a base 64 la imagen y la guardo en la propiedad de imagen en el objeto alumno
                
                producto.Imagen = Convert.ToBase64String(ImagenBytes);
            } 


            if (producto.IdProducto == 0)
            {
                //add Agregar
                ML.Result result = BL.Producto.Add(producto);
                if (result.Correct)
                {
                    ViewBag.Message = "Se ha registrado el producto";
                    return PartialView("Modal");
                }
                else
                {
                    ViewBag.Message = "Error al agregar el producto" + result.ErrorMessage;
                    return PartialView("Modal");
                }
            }
            else
            {
                //update Actualizar
                if (producto != null)
                {
                    ML.Result result = BL.Producto.Update(producto);
                    if (result.Correct)
                    {
                        ViewBag.Message = "Se ha actualizado el producto seleccionado";
                        return PartialView("Modal");
                    }
                    else
                    {
                        ViewBag.Message = "Error al actualizar el producto seleccionado";
                        return PartialView("Modal");
                    }
                }
            }
            return PartialView("Modal");
        }

        // DELETE
        [HttpGet]
        public ActionResult Delete(int IdUsuario)
        {
            ML.Result result = new ML.Result();

            result = BL.Departamento.Delete(IdUsuario);
            if (result.Correct)
            {
                ViewBag.Mensaje = "Se ha elimnado el registro";
                return PartialView("Modal");
            }
            else
            {
                ViewBag.Mensaje = "No see ha elimnado el registro" + result.ErrorMessage;
                return PartialView("Modal");
            }
        }


        //IMAGEN 
        public static byte[] ConvertToBytes(IFormFile imagen)
        {

            using var fileStream = imagen.OpenReadStream();

            byte[] bytes = new byte[fileStream.Length];
            fileStream.Read(bytes, 0, (int)fileStream.Length);

            return bytes;
        }


    }
}
