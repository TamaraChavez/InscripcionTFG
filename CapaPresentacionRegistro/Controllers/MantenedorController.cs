using CapaEntidad;
using CapaNegocio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CapaPresentacionRegistro.Controllers
{
    [Authorize]
    public class MantenedorController : Controller
    {
        // GET: Mantenedor
        public ActionResult PeriodosInscripcion()
        {
            return View();
        }

        public ActionResult SolicitudesInscripcion()
        {
            return View();
        }

        public ActionResult Carreras()
        {
            return View();
        }


        private CN_Periodos objNegocio = new CN_Periodos();

        [HttpPost]
        public JsonResult GuardarPeriodo(Periodo objeto)
        {
            string mensaje = objNegocio.Registrar(objeto);
            return Json(new { mensaje = mensaje }, JsonRequestBehavior.AllowGet);
        }


    }
}