using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SL.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {
        // GET: api/<UsuarioController>

        // GETALL GET
        [HttpGet("GetAll")]
        public IActionResult GetAll()
        {
            ML.Usuario usuario = new ML.Usuario();
            usuario.Rol = new ML.Rol();
            ML.Result result = BL.Usuario.GetAll(usuario);

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

        //GETALL POST
        //[HttpPost("GetAll")]
        // public IActionResult GetAll(string? nombre, string? apellidopaterno, byte? rol) 
        // {

        //     ML.Usuario usuario = new ML.Usuario();

        //     //alumno.Nombre = nombre;
        //     usuario.Nombre = (nombre == null) ? "" : nombre;
        //     usuario.ApellidoPaterno = (apellidopaterno == null) ? "" : apellidopaterno;
        //     usuario.Rol.IdRol = (byte)((rol == null) ? 0 : rol);

        //     usuario.Rol = new ML.Rol();
        //     ML.Result result = BL.Usuario.GetAll(usuario);

        //     if (result.Correct)
        //     {
        //         return Ok(result);
        //     }
        //     else
        //     {
        //         return NotFound();
        //     }

        // }

        [HttpPost("GetAll")]
        public IActionResult GetAll(ML.Usuario usuario)
        {
            ML.Result result = BL.Usuario.GetAll(usuario);
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
            ML.Usuario usuario = new ML.Usuario();
            usuario.Rol = new ML.Rol();
            ML.Result result = BL.Usuario.GetById(id);

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
        public IActionResult Add([FromBody] ML.Usuario usuario)
        {
            ML.Result result = BL.Usuario.Add(usuario);
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
        public IActionResult Put([FromBody] ML.Usuario usuario)
        {
            if (usuario.IdUsuario != null && usuario.IdUsuario != 0)
            {
                ML.Result result = BL.Usuario.Update(usuario);
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
        public IActionResult Delete(byte idUsuario)
        {
            ML.Usuario usuario = new ML.Usuario();
            usuario.Rol = new ML.Rol();
            ML.Result result = BL.Usuario.Delete(idUsuario);

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
