﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CapaEntidad;
using CapaNegocio;

namespace CapaPresentacionRegistro.Controllers
{
    [Authorize] //requiere autenticacion para acceder //en el web config en la parte de system.web se agrega una parte de autentication
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Usuarios()
        {
            return View();
        }

        [HttpGet]
        public JsonResult ListarUsuarios()
        {

            List<Usuario> oLista = new List<Usuario>();
            oLista = new CN_Usuario().Listar();

            return Json(new { data = oLista }, JsonRequestBehavior.AllowGet);

        } //termina el listar 

        [HttpPost]
        public JsonResult GuardarUsuario(Usuario objeto)
        {
            object resultado;
            string mensaje = string.Empty;

            if (objeto.IdUsuario == 0)
            {
                resultado = new CN_Usuario().Registrar(objeto, out mensaje);
            }
            else
            {

                resultado = new CN_Usuario().Editar(objeto, out mensaje);
            }

            return Json(new { resultado = resultado, mensaje = mensaje }, JsonRequestBehavior.AllowGet);

        }//termina el guardar usuario 

        //Eliminar 
        [HttpPost]
        public JsonResult EliminarUsuario(int id)
        {

            bool respuesta = false;
            string mensaje = string.Empty;

            respuesta = new CN_Usuario().Eliminar(id, out mensaje);

            return Json(new { resultado = respuesta, mensaje = mensaje }, JsonRequestBehavior.AllowGet);

        }//termina el eliminar
    }
}