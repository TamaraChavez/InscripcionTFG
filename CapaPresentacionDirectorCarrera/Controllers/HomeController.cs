using System.Web.Mvc;
namespace CapaPresentacionDirectorCarrera.Controllers
{

    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }
using CapaNegocio;
public class HomeController : Controller
{
    private CN_InscripcionesResueltas objNegocio = new CN_InscripcionesResueltas();

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

        [HttpPost]
        public JsonResult AsignarTutor(int idInscripcionResuelta, int idUsuarioTutor)
        {
            string mensaje = string.Empty;

            try
            {
                Debug.WriteLine($"AsignarTutor - Iniciando asignación. ID Inscripción Resuelta: {idInscripcionResuelta}, ID Usuario Tutor: {idUsuarioTutor}");

                // Obtén la inscripción resuelta correspondiente para verificar su estado
                var inscripcion = new CN_AsigTutores().Listar().FirstOrDefault(x => x.idInscripcionResuelta == idInscripcionResuelta);

                if (inscripcion == null)
                {
                    mensaje = "No se encontró la inscripción especificada.";
                    return Json(new { resultado = false, mensaje = mensaje }, JsonRequestBehavior.AllowGet);
                }
    [HttpGet]
    public JsonResult ListarUsuariosCarrera(int idDirector)
    {
        var resultado = objNegocio.Listar(idDirector);
        return Json(resultado, JsonRequestBehavior.AllowGet);
    }

                // Actualiza la inscripción resuelta con el tutor seleccionado si el estado es aprobado
                if (inscripcion.estado.Trim() == "Aprobado")
                {
                    bool resultado = new CN_AsigTutores().Editar(new InscripcionesResueltas
                    {
                        idInscripcionResuelta = idInscripcionResuelta,
                        idUsuarioTutor = idUsuarioTutor
                    }, out mensaje);
    [HttpPost]
    public JsonResult ActualizarEstado(int idUsuario, string estado)
    {
        bool resultado = objNegocio.ActualizarEstadoInscripcion(idUsuario, estado);
        return Json(new { resultado = resultado });
    }

                    Debug.WriteLine($"AsignarTutor - Resultado de la operación: {resultado}, Mensaje: {mensaje}");
                    return Json(new { resultado = resultado, mensaje = mensaje }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    mensaje = "No se puede asignar un tutor porque el estado no es 'Aprobado'.";
                    return Json(new { resultado = false, mensaje = mensaje }, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"AsignarTutor - Ha ocurrido una excepción: {ex.Message}");
                mensaje = "Ocurrió un error durante la asignación del tutor.";
                return Json(new { resultado = false, mensaje = mensaje }, JsonRequestBehavior.AllowGet);
            }
        }
    }
}
}
