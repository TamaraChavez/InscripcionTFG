using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using CapaEntidad;
using CapaNegocio;

namespace CapaPresentacionDocente.Controllers
{
    public class AccesoController : Controller
    {
        // GET: Acceso
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Index(string correo, string clave)
        {
            Usuario oUsuario = new Usuario();
            oUsuario = new CN_Usuario().Listar().Where(u => u.Correo == correo && u.clave == CN_Recursos.ConvertirSha256(clave)).FirstOrDefault();

            if (oUsuario == null)
            {
                ViewBag.Error = "Correo o clave incorrecta"; /*almacena temporalmente el mensaje del error y lo envia a la vista*/
                return View();
            }
            else
            {

                FormsAuthentication.SetAuthCookie(oUsuario.Correo, false); //requiere autenticarse mediante el correo

                ViewBag.Error = null;
                return RedirectToAction("Index", "Home");
            }
        }//termina el index de tipo post 
    }
}