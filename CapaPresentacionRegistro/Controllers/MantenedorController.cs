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

        //Carga la informacion del usuario que este logueado directamente al ir a la pagina
        public ActionResult SolicitudesInscripcion()
        {
            // Verifica si el usuario está autenticado
            if (User.Identity.IsAuthenticated)
            {
                string correo = User.Identity.Name; // Obtener el correo del usuario autenticado

                // Buscar el usuario en la base de datos usando el correo
                Usuario oUsuario = new CN_Usuario().Listar().Where(u => u.Correo == correo).FirstOrDefault();
                CN_Usuarios_Carrera cnUsuarioCarrera = new CN_Usuarios_Carrera();
                CN_Periodos cN_Periodos = new CN_Periodos();

                if (oUsuario != null)
                {
                    int idUsuario = oUsuario.IdUsuario; // Obtener el Id del estudiante
                    string nombreUsuario = oUsuario.Nombre;
                    string apellidosUsuario = oUsuario.Apellido1 + " " + oUsuario.Apellido2;
                    string correoUsuario = oUsuario.Correo;
                    List<Carrera> carreras = cnUsuarioCarrera.ObtenerCarrerasPorUsuario(idUsuario);
                    Periodo periodoActivo = cN_Periodos.ObtenerPeriodoActivo();
                    bool periodoHabilitado = cN_Periodos.EsPeriodoHabilitado(periodoActivo); 

                    // Pasar el Id a la vista
                    ViewBag.IdUsuario = idUsuario;
                    ViewBag.Nombre = nombreUsuario;
                    ViewBag.Apellidos = apellidosUsuario;
                    ViewBag.Correo = correoUsuario;
                    ViewBag.Carreras = carreras;
                    ViewBag.Periodo = periodoActivo;
                    ViewBag.PeriodoHabilitado = periodoHabilitado;
                    ViewBag.FechaActual = DateTime.Now;
                }
                else
                {
                    // Redirigir al login si no se encuentra el usuario
                    return RedirectToAction("Index", "Acceso");
                }
            }
            else
            {
                // Redirigir al login si no está autenticado
                return RedirectToAction("Index", "Acceso");
            }
            return View();
        }


        //Carga las modalidades basado en la carrera seleccionada 
        public ActionResult ObtenerModalidades(string idCarrera)
        {
            // Verifica si el usuario está autenticado
            if (User.Identity.IsAuthenticated)
            {
                // Obtener las modalidades de la carrera seleccionada
                List<Modalidad> modalidades = new CN_Modalidad_Carrera().ObtenerModalidadesPorCarrera(idCarrera);

                // Retorna las modalidades como JSON
                return Json(modalidades, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new { error = "Usuario no autenticado" }, JsonRequestBehavior.AllowGet);
            }
        }
        public ActionResult Carreras()
        {
            return View();
        }




        //Objeto de la capa de negocios de Periodo
        private CN_Periodos objNegocioPeriodo = new CN_Periodos();
        //Objeto de la capa de negocios de la empresa 
        private CN_Empresa objNegocioEmpresa = new CN_Empresa();
        private CN_Inscripcion objNegocioInscripcion = new CN_Inscripcion();

        //Metodo Post para guardar un periodo en la BD
        [HttpPost]
        public JsonResult GuardarPeriodo(Periodo objeto)
        {
            string mensaje = objNegocioPeriodo.Registrar(objeto);
            return Json(new { mensaje = mensaje }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult CrearEmpresa(Empresa empresa)
        {
            int idEmpresa = objNegocioEmpresa.CrearEmpresa(empresa);
            return Json(new { idEmpresa = idEmpresa }, JsonRequestBehavior.AllowGet);

        }

        [HttpPost]
        public JsonResult CrearInscripcion(Inscripcion inscripcion)
        {
            string mensaje = objNegocioInscripcion.CrearInscripcion(inscripcion);
            return Json(new { mensaje = mensaje }, JsonRequestBehavior.AllowGet);

        }
    
    }
}