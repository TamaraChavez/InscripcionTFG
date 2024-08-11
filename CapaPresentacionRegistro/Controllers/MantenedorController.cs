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

        public ActionResult Modalidades() { 
            return View();
        }

       



    }
}