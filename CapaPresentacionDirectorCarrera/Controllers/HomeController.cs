using CapaEntidad;
using CapaNegocio;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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
            Debug.WriteLine("Comenzando a listar inscripciones");
            List<InscripcionesResueltas> oLista = new CN_AsigTutores().Listar();
            Debug.WriteLine($"HomeController.ListarInscripciones() - Se encontraron {oLista.Count} inscripciones.");
            foreach (var inscripcion in oLista)
            {
                Debug.WriteLine($"Inscripción ID: {inscripcion.idInscripcionResuelta}, ID Inscripcion: {inscripcion.idInscripcion}, ID Director: {inscripcion.idUsuarioDirector}, Estado: {inscripcion.estado}, ID Tutor: {inscripcion.idUsuarioTutor}");
            }
            return Json(new { data = oLista }, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult ListarTutores()
        {
            // Listar los tutores disponibles
            List<Usuario> tutores = new CN_AsigTutores().ListarTutoresCarrera();
            return Json(tutores, JsonRequestBehavior.AllowGet);
        }

        //[HttpPost]
        //public JsonResult AsignarTutor(int idInscipcionResuelta, int idUsuarioTutor)
        //{
        //    string mensaje = string.Empty;

        //    // Actualiza la inscripción resuelta con el tutor seleccionado
        //    bool resultado = new CN_AsigTutores().Editar(new InscripcionesResueltas
        //    {
        //        idInscripcionResuelta = idInscipcionResuelta,
        //        idUsuarioTutor = idUsuarioTutor
        //        // No se asigna idUsuarioDirector desde la sesión, se asume que ya está en la base de datos
        //    }, out mensaje);

        //    return Json(new { resultado = resultado, mensaje = mensaje }, JsonRequestBehavior.AllowGet);
        //}

    }
}