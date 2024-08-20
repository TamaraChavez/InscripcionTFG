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
        public ActionResult Index(string correo, string clave, string claveActual, string nuevaClave, string ConfirmarClave)
        {
            Usuario oUsuario = new CN_Usuario().Listar()
                                .FirstOrDefault(u => u.Correo == correo &&
                                                     u.clave == CN_Recursos.ConvertirSha256(clave));

            if (oUsuario == null)
            {
                ViewBag.Error = "Correo o clave incorrecta";
                return View();
            }

            if (oUsuario.clave != CN_Recursos.ConvertirSha256(claveActual))
            {
                ViewBag.Error = "La contraseña actual no es correcta";
                return View();
            }

            if (nuevaClave != ConfirmarClave)
            {
                ViewBag.Error = "Las contraseñas no coinciden";
                return View();
            }

            nuevaClave = CN_Recursos.ConvertirSha256(nuevaClave);
            string mensaje;
            bool respuesta = new CN_Usuario().CambiarClave(oUsuario.IdUsuario, nuevaClave, out mensaje);

            if (respuesta)
            {
                return RedirectToAction("Index");
            }
            else
            {
                ViewBag.Error = mensaje;
                return View();
            }
        }

        [HttpPost]
        public ActionResult Reestablecer(string correo)
        {
            Usuario oUsuario = new CN_Usuario().Listar()
                                 .FirstOrDefault(u => u.Correo == correo);

            if (oUsuario == null)
            {
                ViewBag.Error = "No se encontró un usuario asociado a ese correo";
                return View();
            }

            string mensaje;
            bool respuesta = new CN_Usuario().ReestablecerClave(oUsuario.IdUsuario, correo, out mensaje);

            if (respuesta)
            {
                ViewBag.Error = null;
                return RedirectToAction("Index", "Acceso");
            }
            else
            {
                ViewBag.Error = mensaje;
                return View();
            }
        }

        public ActionResult CerrarSesion()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Index", "Acceso");
        }
    }
}
