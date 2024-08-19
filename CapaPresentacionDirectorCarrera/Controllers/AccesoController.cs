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
                ViewBag.Error = "Correo o clave incorrecta"; // Almacena temporalmente el mensaje del error y lo envía a la vista
                return View();
            }
            else
            {
                if (oUsuario.TipoUsuario.Contains('1') || oUsuario.TipoUsuario.Contains('2') || oUsuario.TipoUsuario.Contains('3')) // Valida si tiene autorización de ingresar al módulo
                {
                    ViewBag.Error = "No tiene autorización para ingresar al módulo de registro."; // Almacena temporalmente el mensaje del error y lo envía a la vista
                    return View();
                }
                else if (!oUsuario.Reestablecer) // Valida si está accediendo por primera vez y debe cambiar la contraseña
                {
                    TempData["IdUsuario"] = oUsuario.IdUsuario; // Almacena temporalmente el idUsuario y lo envía a la vista
                    return RedirectToAction("CambiarClave", new { idUsuario = oUsuario.IdUsuario });
                }

                FormsAuthentication.SetAuthCookie(oUsuario.Correo, false); // Requiere autenticarse mediante el correo

                ViewBag.Error = null;
                return RedirectToAction("Index", "Home", new { idUsuario = oUsuario.IdUsuario });
            }
        }


        [HttpPost]
        public ActionResult CambiarClave(string IdUsuario, string claveActual, string nuevaClave, string ConfirmarClave)
        {
            Usuario oUsuario = new Usuario();
            oUsuario = new CN_Usuario().Listar().Where(u => u.IdUsuario == int.Parse(IdUsuario)).FirstOrDefault();

            if (oUsuario.clave != CN_Recursos.ConvertirSha256(claveActual)) /*valida si la clave actual es correcta*/
            {
                TempData["IdUsuario"] = IdUsuario;
                ViewData["vclave"] = "";
                ViewBag.Error = "La contrasenia actual no es correcta"; /*almacena temporalmente el mensaje del error y lo envia a la vista*/
                return View();

            }
            else if (nuevaClave != ConfirmarClave) /*valida si las nuevas claves coinciden*/
            {
                TempData["IdUsuario"] = IdUsuario;
                ViewData["vclave"] = claveActual;
                ViewBag.Error = "Las contrasenias no coinciden";
                return View();
            }

            ViewData["vclave"] = "";

            nuevaClave = CN_Recursos.ConvertirSha256(nuevaClave);

            string mensaje = string.Empty;

            bool respuesta = new CN_Usuario().CambiarClave(int.Parse(IdUsuario), nuevaClave, out mensaje);

            if (respuesta)
            {

                return RedirectToAction("Index");
            }
            else
            {
                TempData["IdUsuario"] = IdUsuario;
                ViewBag.Error = mensaje;
                return View();
            }
        }//termina el cambiarCalve de tipo post 

        [HttpPost]

        public ActionResult Reestablecer(string correo)
        {

            Usuario ousuario = new Usuario();

            ousuario = new CN_Usuario().Listar().Where(item => item.Correo == correo).FirstOrDefault(); /*con este metodo se busca al usuario especifico del correo*/

            //en caso que el correo indicado no se encuentre en la base de datos
            if (ousuario == null)
            {

                ViewBag.Error = "No se encontro un usuario asociado a ese correo";
                return View();
            }

            //si el correo si se encuentra en la base de datos, se procede a intentar reestablecer la contrasenia 

            string mensaje = string.Empty;

            bool respuesta = new CN_Usuario().ReestablecerClave(ousuario.IdUsuario, correo, out mensaje);

            //se valida si se reestablecio la contrasenia 

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
        }//termina el reestablecer de tipo post 


        public ActionResult CerrarSesion()
        {
            FormsAuthentication.SignOut(); //al cerrar sesion se invalidan las credenciales para seguir navegando
            return RedirectToAction("Index", "Acceso");
        }//termina el cerrar sesion 


    }
}