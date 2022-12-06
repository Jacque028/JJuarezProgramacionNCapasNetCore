using Microsoft.AspNetCore.Mvc;

namespace PL.Controllers
{
    public class DepartamentoController : Controller
    {
        [HttpGet]
        public ActionResult GetAll()
        {
            ML.Result result = BL.Departamento.GetAll();
            ML.Departamento departamento = new ML.Departamento();

            if (result.Correct)
            {
                departamento.Departamentos = result.Objects;
            }
            else
            {
                ViewBag.Message = "Error al hacer la consulta :";
            }
            return View(departamento);
        }

        [HttpGet]
        public ActionResult Form(int? IdDepartamento)
        {
            ML.Departamento departamento = new ML.Departamento();

            departamento.Area = new ML.Area();


            ML.Result resultArea = BL.Area.GetAll();

            if (IdDepartamento == null)
            {

                departamento.Area.Areas = resultArea.Objects;

                return View(departamento);
            }
            else
            {
                ML.Result result = BL.Departamento.DepartamentoGetByIdArea(IdDepartamento.Value);

                if (result.Correct)
                {
                    departamento = (ML.Departamento)result.Object;
                    departamento.Area.Areas = resultArea.Objects;
                }
                else
                {
                    ViewBag.Message = "Ocurrio un error al consultar el Usuario seleccionado";
                }
                return View(departamento);
            }

        }

        [HttpPost]

        public ActionResult Form(ML.Departamento departamento)
        {
            if (departamento.IdDepartamento == 0)
            {
                ML.Result result = BL.Departamento.Add(departamento);
                if (result.Correct)
                {
                    ViewBag.Message = "Se ha registrado el Departamento";
                    return PartialView("Modal");
                }
                else
                {
                    ViewBag.Message = "No se ha registrado el Departamento" + result.ErrorMessage;
                    return PartialView("Modal");
                }
            }
            else
            {
                ML.Result result = BL.Departamento.Update(departamento);
                if (result.Correct)
                {

                    ViewBag.Message = "Se ha Actualizado el Departamento";
                    return PartialView("Modal");
                }
                else
                {
                    ViewBag.Message = "No ha registrado el Departamento" + result.ErrorMessage;
                    return PartialView("Modal");
                }
            }

        }


        // DELETE

        [HttpGet]
        public ActionResult Delete(int IdDepartamento)
        {
            ML.Result result = new ML.Result();

            result = BL.Departamento.Delete(IdDepartamento);
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
        public JsonResult GetDepartamento(int IdArea)
        {
            var result = BL.Departamento.DepartamentoGetByIdArea(IdArea);

            return Json(result.Objects);
        }
        //[HttpGet]
        //public ActionResult Form(int? IdDepartamento)
        //{
        //    ML.Departamento departamento = new ML.Departamento();
        //    departamento.Area = new ML.Area();

        //    ML.Result resultArea = BL.Area.GetAll();


        //    if (IdDepartamento == null)
        //    {
        //        //Lista de cascada de area
        //        departamento.Area.Areas = resultArea.Objects;

        //        return View(departamento);
        //    }
        //    else
        //    {
        //        //GetById
        //        ML.Result result = BL.Departamento.DepartamentoGetByIdArea(IdDepartamento.Value);

        //        if (result.Correct)
        //        {
        //            departamento = (ML.Departamento)result.Object;

        //            //Lista de cascada de paises
        //            departamento.Area.Areas = resultArea.Objects;
        //        }
        //        else
        //        {
        //            ViewBag.Message = "Error al consultar el producto seleccionado";
        //        }
        //        return View(departamento);
        //    }
        //}

        //[HttpPost]
        //public ActionResult Form(ML.Departamento departamento)
        //{

        //    if (departamento.IdDepartamento == 0)
        //    {
        //        //add Agregar
        //        ML.Result result = BL.Departamento.Add(departamento);

        //        if (result.Correct)
        //        {
        //            ViewBag.Message = "Se ha registrado el departamento";
        //            return PartialView("Modal");
        //        }
        //        else
        //        {
        //            ViewBag.Message = "Error al agregar el departamento" + result.ErrorMessage;
        //            return PartialView("Modal");
        //        }
        //    }
        //    else
        //    {
        //        //update Actualizar
        //        if (departamento != null)
        //        {
        //            ML.Result result = BL.Departamento.Update(departamento);
        //            if (result.Correct)
        //            {
        //                ViewBag.Message = "Se ha actualizado el departamento seleccionado";
        //                return PartialView("Modal");
        //            }
        //            else
        //            {
        //                ViewBag.Message = "Error al actualizar el departamento seleccionado";
        //                return PartialView("Modal");
        //            }
        //        }
        //    }
        //    return PartialView("Modal");
        //}


        // DELETE

        //[HttpGet]
        //public ActionResult Delete(ML.Departamento departamento)
        //{
        //    if (departamento != null)
        //    {
        //        ML.Result result = new ML.Result();
        //        result = BL.Departamento.Delete(departamento);
        //        if (result.Correct)
        //        {
        //            ViewBag.Message = "Se ha elimnado el registro correctamento";
        //            return PartialView("Modal");
        //        }
        //        else
        //        {
        //            ViewBag.Message = "Error no se elimino el registro" + result.ErrorMessage;
        //            return PartialView("Modal");
        //        }
        //    }
        //    else
        //    {
        //        return PartialView("Modal");
        //    }
        //}


        //public JsonResult GetDepartamento(int IdArea)
        //{
        //    var result = BL.Departamento.DepartamentoGetByIdArea(IdArea);

        //    return Json(result.Objects);
        //}


    }
}
