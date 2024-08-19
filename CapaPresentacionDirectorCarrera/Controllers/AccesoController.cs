using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using CapaEntidad;
using CapaNegocio;

namespace CapaPresentacionDirectorCarrera.Controllers
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
                if (!oUsuario.Reestablecer) /*valida si esta accediendo por primera vez y debe cambiar la contrasenia*/
                {
                    TempData["IdUsuario"] = oUsuario.IdUsuario; /*almacena temporalmente el idUsuario  y lo envia a la vista*/
                    return RedirectToAction("CambiarClave");
                }
                // Guardar datos del usuario en la sesión

                FormsAuthentication.SetAuthCookie(oUsuario.Correo, false); //requiere autenticarse mediante el correo

                ViewBag.Error = null;
                return RedirectToAction("Index", "Home");
            }
        }//termina el index de tipo post 
    }
}