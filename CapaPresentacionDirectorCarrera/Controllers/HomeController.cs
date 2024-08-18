using CapaEntidad;
using CapaNegocio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CapaPresentacionDirectorCarrera.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult AsigTutor()
        {
            return View();
        }
        [HttpGet]
        public JsonResult ListarInscripciones()
        {
            // Obtén las inscripciones resueltas
            List<InscripcionesResueltas> oLista = new CN_AsigTutores().Listar();
            return Json(new { data = oLista }, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult ListarTutores()
        {
            // Listar los tutores disponibles
            List<Usuario> tutores = new CN_AsigTutores().ListarTutoresCarrera();
            return Json(tutores, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult AsignarTutor(int idInscipcionResuelta, int idUsuarioTutor)
        {
            string mensaje = string.Empty;
            // Actualiza la inscripción resuelta con el tutor seleccionado
            bool resultado = new CN_AsigTutores().Editar(new InscripcionesResueltas
            {
                idInscipcionResuelta = idInscipcionResuelta,
                idUsuarioTutor = idUsuarioTutor,
                idUsuarioDirector = Convert.ToInt32(Session["idUsuario"]) // Asignar el director desde la sesión
            }, out mensaje);

            return Json(new { resultado = resultado, mensaje = mensaje }, JsonRequestBehavior.AllowGet);
        }
    }
}