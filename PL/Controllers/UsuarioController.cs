using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;


namespace PL.Controllers
{
    public class UsuarioController : Controller
    {

        // SESION
        private readonly IConfiguration _configuration;

        private readonly Microsoft.AspNetCore.Hosting.IHostingEnvironment _hostingEnvironment;

        public UsuarioController(IConfiguration configuration, Microsoft.AspNetCore.Hosting.IHostingEnvironment hostingEnvironment)
        {
            _configuration = configuration;
            _hostingEnvironment = hostingEnvironment;
        }


        // GETALL GET
        [HttpGet]
        public ActionResult GetAll()
        {
            ML.Result resultrol = BL.Rol.GetAll();
            ML.Usuario usuario = new ML.Usuario();
            usuario.Rol = new ML.Rol();
            ML.Result result = new ML.Result();
            result.Objects = new List<object>();
            
            try
            {
                string urlAPI = _configuration["UrlAPI"]; //MVC PL_MVC -> Asíncronos/Síncrono

                using (var user = new HttpClient())
                {
                    user.BaseAddress = new Uri(urlAPI); // Ruta del Swagger

                    var responseTask = user.GetAsync("Usuario/GetAll");
                    

                    responseTask.Wait();

                    var resultServicio = responseTask.Result;

                    if (resultServicio.IsSuccessStatusCode)
                    {
                        var readTask = resultServicio.Content.ReadAsAsync<ML.Result>();
                        readTask.Wait();

                        foreach (var resultItem in readTask.Result.Objects)
                        {
                            ML.Usuario resultItemList = Newtonsoft.Json.JsonConvert.DeserializeObject<ML.Usuario>(resultItem.ToString());
                            result.Objects.Add(resultItemList);
                        }

                        result.Correct = true;
                    }
                }
            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.ErrorMessage = ex.Message;
            }
            if (result.Correct)
            {
                usuario.Rol.Roles = resultrol.Objects;
                usuario.Usuarios = result.Objects;
            }
            else
            {
                ViewBag.Message = "Ocurrio un error al realizar la consulta";
            }
            return View(usuario);
        }


        // BUSQUEDA ABIERTA GETALL POST
        [HttpPost]
        public ActionResult GetAll(ML.Usuario usuario)
        {
            //ML.Result result = BL.Usuario.GetAllEF(usuario);        
            ML.Result result = new ML.Result();
            try
            {
                using (HttpClient httpClient = new HttpClient())
                {
                    httpClient.DefaultRequestHeaders.Clear();
                    httpClient.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

                    string urlAPI = _configuration["UrlAPI"];
                    httpClient.BaseAddress = new Uri(urlAPI);

                    var request = httpClient.PostAsJsonAsync<ML.Usuario>("Usuario/GetAll", usuario);
                    request.Wait();

                    var response = request.Result;
                    if (response.IsSuccessStatusCode)
                    {
                        var readContent = response.Content.ReadAsStringAsync().Result;

                        ML.Result resultAPI = JsonConvert.DeserializeObject<ML.Result>(readContent);

                        result.ErrorMessage = resultAPI.ErrorMessage;

                        if (resultAPI.Correct)
                        {
                            result.Objects = new List<object>();
                            foreach (var item in resultAPI.Objects)
                            {
                                ML.Usuario user = JsonConvert.DeserializeObject<ML.Usuario>(item.ToString());
                                result.Objects.Add(user);
                            }
                            result.Correct = true;
                        }
                    }
                }
            }
            catch (Exception error)
            {
                result.Correct = false;
                result.ErrorMessage = error.Message;
            }
            usuario.Rol = new ML.Rol();
            ML.Result resultrol = BL.Rol.GetAll();
            if (result.Correct)
            {
                usuario.Usuarios = result.Objects;
                usuario.Rol.Roles = resultrol.Objects;
            }
            else
            {
                ViewBag.Message = "Ocurrio un error al realizar la consulta";
            }
            return View(usuario);
        }


        // FORMULARIO 
        [HttpGet]
        public ActionResult Form(int? IdUsuario)
        {
            ML.Result resultrol = BL.Rol.GetAll();
            ML.Usuario usuario = new ML.Usuario();
            usuario.Rol = new ML.Rol();

            ML.Result resultpais = BL.Pais.GetAll();
            usuario.Direccion = new ML.Direccion();
            usuario.Direccion.Colonia = new ML.Colonia();
            usuario.Direccion.Colonia.Municipio = new ML.Municipio();
            usuario.Direccion.Colonia.Municipio.Estado = new ML.Estado();
            usuario.Direccion.Colonia.Municipio.Estado.Pais = new ML.Pais();

            if (IdUsuario == null)
            {
                usuario.Direccion.Colonia.Municipio.Estado.Pais.Paises = resultpais.Objects;
                usuario.Rol.Roles = resultrol.Objects;

                return View(usuario);
            }
            else
            {
                // GETBYID
                //ML.Result result = BL.Usuario.GetById(IdUsuario.Value);
                ML.Result result = new ML.Result();
                result.Object = usuario;
                try
                {
                    using (HttpClient httpClient = new HttpClient())
                    {
                        httpClient.DefaultRequestHeaders.Clear();
                        httpClient.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

                        string urlAPI = _configuration["UrlAPI"];
                        httpClient.BaseAddress = new Uri(urlAPI);

                        var request = httpClient.GetAsync($"Usuario/GetById/{IdUsuario.Value}");
                        request.Wait();

                        var response = request.Result;
                        if (response.IsSuccessStatusCode)
                        {
                            var readContent = response.Content.ReadAsStringAsync().Result;

                            ML.Result resultAPI = JsonConvert.DeserializeObject<ML.Result>(readContent);

                            result.ErrorMessage = resultAPI.ErrorMessage;

                            if (resultAPI.Correct)
                            {
                                ML.Usuario user = JsonConvert.DeserializeObject<ML.Usuario>(resultAPI.Object.ToString());
                                result.Object = user;
                                result.Correct = true;
                            }
                        }
                    }
                }
                catch (Exception error)
                {
                    result.Correct = false;
                    result.ErrorMessage = error.Message;
                }
                if (result.Correct)
                {
                    usuario = (ML.Usuario)result.Object;
                    usuario.Direccion.Colonia.Municipio.Estado.Pais.Paises = resultpais.Objects;

                    ML.Result resultEstado = BL.Estado.GetByIdPais(usuario.Direccion.Colonia.Municipio.Estado.Pais.IdPais);
                    usuario.Direccion.Colonia.Municipio.Estado.Estados = resultEstado.Objects;
                    ML.Result resultMunicipio = BL.Municipio.GetByIdEstado(usuario.Direccion.Colonia.Municipio.Estado.IdEstado);
                    usuario.Direccion.Colonia.Municipio.Municipios = resultMunicipio.Objects;
                    ML.Result resultColonia = BL.Colonia.GetByIdMunicipio(usuario.Direccion.Colonia.Municipio.IdMunicipio);
                    usuario.Direccion.Colonia.Colonias = resultColonia.Objects;

                    usuario.Rol.Roles = resultrol.Objects;
                    return View(usuario);
                }
                else
                {
                    ViewBag.Message = "Ocurrio un error al buscar el usuario";
                }
                return View(usuario);
            }
        }

        /// FORMULARIO
        
        //[HttpPost]
        public ActionResult Form(ML.Usuario usuario)
        {
            IFormFile image = Request.Form.Files["FileImage"];
            if (image != null)
            {
                byte[] ImagenBytes = ConvertToBytes(image);
                usuario.Imagen = Convert.ToBase64String(ImagenBytes);
            }

            if (ModelState.IsValid)
            {

                //ADD
                if (usuario.IdUsuario == 0)
                {
                    //ML.Result result = BL.Usuario.Add(usuario);
                    ML.Result result = new ML.Result();
                    result.Object = usuario;
                    try
                    {
                        using (HttpClient httpClient = new HttpClient())
                        {
                            httpClient.DefaultRequestHeaders.Clear();
                            httpClient.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

                            string urlAPI = _configuration["UrlAPI"];
                            httpClient.BaseAddress = new Uri(urlAPI);

                            var request = httpClient.PostAsJsonAsync<ML.Usuario>("Usuario/Add", usuario);
                            request.Wait();

                            var response = request.Result;
                            if (response.IsSuccessStatusCode)
                            {
                                var readContent = response.Content.ReadAsStringAsync().Result;

                                if (result.Correct)
                                {
                                    result.Correct = true;
                                }
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        result.Correct = false;
                        result.ErrorMessage = ex.Message;
                    }
                    if (result.Correct)
                    {
                        //ViewBag.Message = result.ErrorMessage;
                        ViewBag.Message = "Se ha agregado el usuario seleccionado";
                        return PartialView("Modal");
                    }
                    else
                    {
                        //ViewBag.Message = "Error: " + result.ErrorMessage;
                        ViewBag.Message = "Error al agregar el usuario seleccionado";
                        return PartialView("Modal");


                    }
                }
                else
                {
                    //UPDATE
                    if (usuario != null)
                    {
                        //ML.Result result = BL.Usuario.Update(usuario);
                        ML.Result result = new ML.Result();
                        try
                        {
                            using (HttpClient httpClient = new HttpClient())
                            {
                                httpClient.DefaultRequestHeaders.Clear();
                                httpClient.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

                                string urlAPI = _configuration["UrlAPI"];
                                httpClient.BaseAddress = new Uri(urlAPI);


                                //Sending request to find web api REST service resource using HttpClient
                                var request = httpClient.PutAsJsonAsync<ML.Usuario>("Usuario/Update", usuario);
                                request.Wait();

                                //Checking the response is successful or not which is sent using HttpClient
                                var response = request.Result;
                                if (response.IsSuccessStatusCode)
                                {
                                    var readContent = response.Content.ReadAsStringAsync().Result;

                                    ML.Result resultAPI = JsonConvert.DeserializeObject<ML.Result>(readContent);

                                    result.ErrorMessage = resultAPI.ErrorMessage;

                                    if (resultAPI.Correct)
                                    {
                                        result.Correct = true;
                                    }
                                }
                            }
                        }
                        catch (Exception ex)
                        {
                            result.Correct = false;
                            result.ErrorMessage = ex.Message;
                        }
                        if (result.Correct)
                        {
                            //ViewBag.Message = result.ErrorMessage;
                            ViewBag.Message = "Se ha actualizado el usuario seleccionado";
                            return PartialView("Modal");
                        }
                        else
                        {
                            //ViewBag.Message = "Error: " + result.ErrorMessage;
                            ViewBag.Message = "Error al actualizar el usuario seleccionado";
                            return PartialView("Modal");


                        }
                    }
                }
                return PartialView("Modal");
            }
            else
            {
                var errors = ModelState.Values.SelectMany(x => x.Errors);
                ML.Result resultrol = BL.Rol.GetAll();

                ML.Result resultpais = BL.Pais.GetAll();
                usuario.Direccion.Colonia.Municipio.Estado.Pais.Paises = resultpais.Objects;
                ML.Result resultEstado = BL.Estado.GetByIdPais(usuario.Direccion.Colonia.Municipio.Estado.Pais.IdPais);
                usuario.Direccion.Colonia.Municipio.Estado.Estados = resultEstado.Objects;
                ML.Result resultMunicipio = BL.Municipio.GetByIdEstado(usuario.Direccion.Colonia.Municipio.Estado.IdEstado);
                usuario.Direccion.Colonia.Municipio.Municipios = resultMunicipio.Objects;
                ML.Result resultColonia = BL.Colonia.GetByIdMunicipio(usuario.Direccion.Colonia.Municipio.IdMunicipio);
                usuario.Direccion.Colonia.Colonias = resultColonia.Objects;
                usuario.Rol.Roles = resultrol.Objects;
                return View(usuario);
            }
        }

        // DELETE

        public ActionResult Delete(int idUsuario)
        {
            ML.Usuario usuario = new ML.Usuario();
            ML.Result result = new ML.Result();
            if (usuario != null)
            {
                //ML.Result result = BL.Usuario.DeleteEF(idUsuario);
                try
                {
                    using (HttpClient httpClient = new HttpClient())
                    {
                        httpClient.DefaultRequestHeaders.Clear();
                        httpClient.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

                        string urlAPI = _configuration["UrlAPI"];
                        httpClient.BaseAddress = new Uri(urlAPI); // URL DEL SWAGGER

                        var request = httpClient.DeleteAsync($"Usuario/Delete/{idUsuario}");
                        request.Wait();

                        var response = request.Result;
                        if (response.IsSuccessStatusCode)
                        {
                            var readContent = response.Content.ReadAsStringAsync().Result;

                            ML.Result resultAPI = JsonConvert.DeserializeObject<ML.Result>(readContent);

                            result.ErrorMessage = resultAPI.ErrorMessage;

                            if (resultAPI.Correct)
                            {
                                result.Correct = true;
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    result.Correct = false;
                    result.ErrorMessage = ex.Message;
                }
                if (result.Correct)
                {
                    ViewBag.Message = result.ErrorMessage;
                }
                else
                {
                    ViewBag.Message = "Error: " + result.ErrorMessage;
                }
            }
            else
            {
                return Redirect("/Usuario/GetAll");
            }
            return PartialView("Modal");
        }

        // Status 
        public JsonResult CambiarStatus(byte idUsuario, bool status)
        {
            var result = BL.Usuario.ChangeStatus(idUsuario, status);

            return Json(result.Objects);
        }

        //para que muestre el listado por medio de script Ajax
        public JsonResult GetEstado(int IdPais)
        {
            var result = BL.Estado.GetByIdPais(IdPais);

            return Json(result.Objects);
        }

        public JsonResult GetMunicipio(int IdEstado)
        {
            var result = BL.Municipio.GetByIdEstado(IdEstado);

            return Json(result.Objects);
        }

        public JsonResult GetColonia(int IdMunicipio)
        {
            var result = BL.Colonia.GetByIdMunicipio(IdMunicipio);

            return Json(result.Objects);
        }


        //IMAGEN 
        public static byte[] ConvertToBytes(IFormFile imagen)
        {

            using var fileStream = imagen.OpenReadStream();

            byte[] bytes = new byte[fileStream.Length];
            fileStream.Read(bytes, 0, (int)fileStream.Length);

            return bytes;
        }




        /// METODOS PASADOS 
        /// 

        // DELETE
        //[HttpGet]
        //public ActionResult Delete(int IdUsuario)
        //{
        //    ML.Result result = new ML.Result();

        //    using (var client = new HttpClient() )
        //    {

        //    }
        //    result = BL.Departamento.Delete(IdUsuario);
        //    if (result.Correct)
        //    {
        //        ViewBag.Mensaje = "Se ha elimnado el registro";
        //        return PartialView("Modal");
        //    }
        //    else
        //    {
        //        ViewBag.Mensaje = "No see ha elimnado el registro" + result.ErrorMessage;
        //        return PartialView("Modal");
        //    }
        //}


        // FORMULARIO
        //[HttpGet]
        //public ActionResult Form(int? IdUsuario)
        //{
        //    ML.Result resultRol = BL.Rol.GetAll();
        //    ML.Usuario usuario = new ML.Usuario();
        //    usuario.Rol = new ML.Rol();


        //    ML.Result resultPais = BL.Pais.GetAll();
        //    usuario.Direccion = new ML.Direccion();
        //    usuario.Direccion.Colonia = new ML.Colonia();
        //    usuario.Direccion.Colonia.Municipio = new ML.Municipio();
        //    usuario.Direccion.Colonia.Municipio.Estado = new ML.Estado();
        //    usuario.Direccion.Colonia.Municipio.Estado.Pais = new ML.Pais();

        //    if (IdUsuario == null)
        //    {
        //        //Lista de cascada de paises
        //        usuario.Direccion.Colonia.Municipio.Estado.Pais.Paises = resultPais.Objects;
        //        usuario.Rol.Roles = resultRol.Objects;
        //        return View(usuario);
        //    }
        //    else
        //    {

        //        ////GetbyId
        //        ML.Result result = BL.Usuario.GetById(IdUsuario.Value);

        //        if (result.Correct)
        //        {
        //            usuario = (ML.Usuario)result.Object;
        //            usuario.Rol.Roles = resultRol.Objects;

        //            //Lista de cascada de paises
        //            usuario.Direccion.Colonia.Municipio.Estado.Pais.Paises = resultPais.Objects;

        //            //GetById de Pais  con estado
        //            ML.Result resultEstado = BL.Estado.GetByIdPais(usuario.Direccion.Colonia.Municipio.Estado.Pais.IdPais);
        //            usuario.Direccion.Colonia.Municipio.Estado.Estados = resultEstado.Objects;
        //            //GetById de Municipio con Estado
        //            ML.Result resultMunicipio = BL.Municipio.GetByIdEstado(usuario.Direccion.Colonia.Municipio.Estado.IdEstado);
        //            usuario.Direccion.Colonia.Municipio.Municipios = resultMunicipio.Objects;
        //            //GetById de Colonia con Municipio 
        //            ML.Result resultColonia = BL.Colonia.GetByIdMunicipio(usuario.Direccion.Colonia.Municipio.IdMunicipio);
        //            usuario.Direccion.Colonia.Colonias = resultColonia.Objects;


        //        }
        //        else
        //        {
        //            ViewBag.Message = "Error al consultar el usuario seleccionado";
        //        }
        //        return View(usuario);
        //    }
        //}


        //[HttpPost]
        //public ActionResult Form(ML.Usuario usuario)
        //{
        //    IFormFile image = Request.Form.Files["IFImage"];


        //    //valido si traigo imagen
        //    if (image != null)
        //    {
        //        //llamar al metodo que convierte a bytes la imagen
        //        byte[] ImagenBytes = ConvertToBytes(image);
        //        //convierto a base 64 la imagen y la guardo en la propiedad
        //        //de imagen en el objeto alumno
        //        usuario.Imagen = Convert.ToBase64String(ImagenBytes);
        //    }

        //    if (usuario.IdUsuario == 0)
        //    {
        //        //add Agregar
        //        ML.Result result = BL.Usuario.Add(usuario);



        //        if (result.Correct)
        //        {
        //            //usuario = (ML.Usuario)result.Object;
        //            ViewBag.Message = "Se ha registrado el alumno";
        //            return PartialView("Modal");
        //        }
        //        else
        //        {
        //            ViewBag.Message = "Error al agregar el usuario" + result.ErrorMessage;
        //            return PartialView("Modal");
        //        }
        //    }
        //    else
        //    {
        //        //    //update Actualizar
        //        if (usuario != null)
        //        {
        //            ML.Result result = BL.Usuario.Update(usuario);
        //            if (result.Correct)
        //            {
        //                usuario = (ML.Usuario)result.Object;
        //                ViewBag.Message = "Se ha actualizado el alumno";
        //                return PartialView("Modal");
        //            }
        //            else
        //            {
        //                ViewBag.Message = "Error al actualizar el usuario";
        //                return PartialView("Modal");
        //            }
        //        }
        //    }
        //    return PartialView("Modal");
        //}





        //[HttpGet]
        //public ActionResult GetAll()
        //{
        //    ML.Result resultrol = BL.Rol.GetAll();
        //    ML.Usuario usuario = new ML.Usuario();
        //    usuario.Rol = new ML.Rol();
        //    ML.Result result = BL.Usuario.GetAll(usuario);

        //    if (result.Correct)
        //    {
        //        usuario.Usuarios = result.Objects;
        //        usuario.Rol.Roles = resultrol.Objects;
        //    }
        //    else
        //    {
        //        ViewBag.Message = "Ocurrio un error al realizar la consulta";
        //    }
        //    return View(usuario);
        //}


        //GETALL POST
        //[HttpPost]
        // public ActionResult GetAll(ML.Usuario usuario)
        // {
        //     ML.Result result = BL.Usuario.GetAll(usuario);
        //     ML.Result resultrol = BL.Rol.GetAll();
        //     usuario.Rol = new ML.Rol();
        //     if (result.Correct)
        //     {
        //         usuario.Usuarios = result.Objects;
        //         usuario.Rol.Roles = resultrol.Objects;
        //     }
        //     else
        //     {
        //         ViewBag.Message = "Ocurrio un error al realizar la consulta";
        //     }
        //     return View(usuario);
        // }


        

    }
}

