using System.Web.Mvc;
using CapaNegocio;
public class HomeController : Controller
{
    private CN_InscripcionesResueltas objNegocio = new CN_InscripcionesResueltas();

    public ActionResult Index()
    {

        return View();
    }

    [HttpGet]
    public JsonResult ListarUsuariosCarrera(int idDirector)
    {
        var resultado = objNegocio.Listar(idDirector);
        return Json(resultado, JsonRequestBehavior.AllowGet);
    }

    [HttpPost]
    public JsonResult ActualizarEstado(int idUsuario, string estado)
    {
        bool resultado = objNegocio.ActualizarEstadoInscripcion(idUsuario, estado);
        return Json(new { resultado = resultado });
    }

}
