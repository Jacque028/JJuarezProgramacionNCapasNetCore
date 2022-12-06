using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SL.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductoController : ControllerBase
    {
        // GET: api/<ProductoController>
        // GETALL GET
        [HttpGet("GetAll")]
        public IActionResult GetAll()
        {
            ML.Producto producto = new ML.Producto();
            producto.Proveedor = new ML.Proveedor();
            ML.Result result = BL.Producto.GetAll(producto);
            
            if (result.Correct)
            {
                return Ok(result); //Codigo de status OK=200 si es Correcto
            }
            else
            {
                return NotFound(); // Codigo de status NOTFOUND=400 si no es encontrado 
            }
            //return new string[] { "value1", "value2" };
        }

        // GETALL POST
        [HttpPost("GetAll")]
        public IActionResult GetAll(string? nombre, decimal? PrecioUnitario )
        {

            ML.Producto producto = new ML.Producto();

            //alumno.Nombre = nombre;
            producto.Nombre = (nombre == null) ? "" : nombre;
            producto.PrecioUnitario = (decimal)((PrecioUnitario == null) ? 0 : PrecioUnitario);
            

            producto.Proveedor = new ML.Proveedor();
            ML.Result result = BL.Producto.GetAll(producto);

            if (result.Correct)
            {
                return Ok(result);
            }
            else
            {
                return NotFound();
            }

        }

        // GETBYID
        // GET api/<UsuarioController>/5
        [HttpGet("GetById/{id}")]
        public IActionResult GetById(int id)
        {
            ML.Producto producto = new ML.Producto();
            producto.Proveedor = new ML.Proveedor();
            ML.Result result = BL.Producto.GetById(id);

            if (result.Correct)
            {
                return Ok(result); //Codigo de status OK=200 si es Correcto
            }
            else
            {
                return NotFound(); // Codigo de status NOTFOUND=400 si no es encontrado 
            }
            //return new string[] { "value1", "value2" };
            //return "value";
        }

        // POST api/<UsuarioController>
        [HttpPost("Add")]
        public IActionResult Add([FromBody] ML.Producto producto)
        {
            ML.Result result = BL.Producto.Add(producto);
            if (result.Correct)
            {
                return Ok(result);
            }
            else
            {
                return BadRequest(result.ErrorMessage);
            }
        }

        // PUT api/<UsuarioController>/5
        [HttpPut("Update")]
        public IActionResult Put([FromBody] ML.Producto producto
            )
        {
            if (producto.IdProducto != null && producto.IdProducto != 0)
            {
                ML.Result result = BL.Producto.Update(producto);
                if (result.Correct)
                {
                    return Ok(result);
                }
                else
                {
                    return BadRequest(result.ErrorMessage);
                }
            }
            else
            {
                return BadRequest("Especifica el Id del objeto a actualizar");
            }
        }

        // DELETE 
        // DELETE api/<UsuarioController>/5

        [HttpDelete("Delete/{idUsuario}")]
        public IActionResult Delete(int idProducto)
        {
            ML.Producto producto = new ML.Producto();
            producto.Proveedor = new ML.Proveedor();
            ML.Result result = BL.Producto.Delete(idProducto);

            if (result.Correct)
            {
                return Ok(result); //Codigo de status OK=200 si es Correcto
            }
            else
            {
                return NotFound(); // Codigo de status NOTFOUND=400 si no es encontrado o correcto
            }
        }
    }
}
