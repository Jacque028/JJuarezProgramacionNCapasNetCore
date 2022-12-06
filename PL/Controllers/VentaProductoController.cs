using Microsoft.AspNetCore.Mvc;

namespace PL.Controllers
{
    public class VentaProductoController : Controller
    {
        private readonly IConfiguration _configuration;

        private readonly Microsoft.AspNetCore.Hosting.IHostingEnvironment _hostingEnvironment;

        public VentaProductoController(IConfiguration configuration, Microsoft.AspNetCore.Hosting.IHostingEnvironment hostingEnvironment)
        {
            _configuration = configuration;
            _hostingEnvironment = hostingEnvironment;
        }



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
        public ActionResult CardPost(ML.Producto producto)
        {
            ML.VentaProducto ventaProducto = new ML.VentaProducto();
            ventaProducto.VentasProductos = new List<object>();

            if (HttpContext.Session.GetString("Producto") == null)
            {
                producto.Cantidad = producto.Cantidad = 1;
                producto.Total = producto.Total = (int)(producto.Cantidad * producto.PrecioUnitario);

                ventaProducto.VentasProductos.Add(producto);
                HttpContext.Session.SetString("Producto", Newtonsoft.Json.JsonConvert.SerializeObject(ventaProducto.VentasProductos));
                var session = HttpContext.Session.GetString("Producto");
            }
            else
            {
                var ventaSession = Newtonsoft.Json.JsonConvert.DeserializeObject<List<object>>(HttpContext.Session.GetString("Producto"));

                foreach (var obj in ventaSession)
                {
                    ML.Producto objProducto = Newtonsoft.Json.JsonConvert.DeserializeObject<ML.Producto>(obj.ToString());
                    ventaProducto.VentasProductos.Add(objProducto);
                }

                foreach (ML.Producto venta in ventaProducto.VentasProductos.ToList())
                {
                    if (producto.IdProducto == venta.IdProducto)
                    {
                        venta.Cantidad = venta.Cantidad + 1;
                    }
                    else
                    {
                        producto.Cantidad = producto.Cantidad = 1;
                        ventaProducto.VentasProductos.Add(producto);
                    }
                }
                HttpContext.Session.SetString("Producto", Newtonsoft.Json.JsonConvert.SerializeObject(ventaProducto.VentasProductos));
            }
            if (HttpContext.Session.GetString("Producto") != null)
            {
                ViewBag.Message = "Se ha agregado el producto al carrito";
                return View(producto);
            }
            else
            {
                ViewBag.Message = "No se pudo agregar el producto ):";
                return View(producto);
            }

        }

        [HttpGet]
        public ActionResult ResumenCompra(ML.VentaProducto ventaProducto)
        {
            if (HttpContext.Session.GetString("Producto") == null)
            {
                return View();
            }
            else
            {
                var ventaSession = Newtonsoft.Json.JsonConvert.DeserializeObject<List<object>>(HttpContext.Session.GetString("Producto"));
                ventaProducto.VentasProductos = new List<object>();

                foreach (var obj in ventaSession)
                {
                    ML.Producto objProducto = Newtonsoft.Json.JsonConvert.DeserializeObject<ML.Producto>(obj.ToString());
                    ventaProducto.VentasProductos.Add(objProducto);
                }

            }

            return View(ventaProducto);
        }





    }
}
