using CapaDatos;
using CapaEntidad;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace CapaNegocio
{
    public class CN_Usuario
    {
        private CD_Usuarios objCapaDato = new CD_Usuarios();

        public List<Usuario> Listar()
        {

            return objCapaDato.Listar();
        }//termina listar

        //REGISTRAR 
        public int Registrar(Usuario obj, out string Mensaje)
        {
            Mensaje = string.Empty;

            /*validaciones de campos obligatorios*/

            if (string.IsNullOrEmpty(obj.Nombre) || string.IsNullOrWhiteSpace(obj.Nombre))
            {
                Mensaje = "Debe ingresar el nombre del usuario";
            }
            else if (string.IsNullOrEmpty(obj.Apellido1) || string.IsNullOrWhiteSpace(obj.Apellido1))
            {
                Mensaje = "Debe ingresar el apellido del usuario";
            }
            else if (string.IsNullOrEmpty(obj.Apellido2) || string.IsNullOrWhiteSpace(obj.Apellido2))
            {
                Mensaje = "Debe ingresar el apellido del usuario";
            }
            else if (string.IsNullOrEmpty(obj.Correo) || string.IsNullOrWhiteSpace(obj.Correo))
            {
                Mensaje = "Debe ingresar el correo del usuario";
            }
            else if (string.IsNullOrEmpty(obj.TipoUsuario) || string.IsNullOrWhiteSpace(obj.TipoUsuario))
            {
                Mensaje = "Debe indicar el tipo de Usuario";
            }

            if (string.IsNullOrEmpty(Mensaje))
            {
                /*enviarle un correo con la contrasenia autogenerada */
                string clave = CN_Recursos.GenerarClave();
                string asunto = "Creacion de cuenta Sistema de Inscripcion TFG";
                string mensaje_correo = "<h3>Su cuenta fue creada correctamente</h3></br><p>Su contraseña para acceder es: ¡clave!</p>";
                mensaje_correo = mensaje_correo.Replace("¡clave!", clave);

                bool respuesta = CN_Recursos.EnviarCorreo(obj.Correo, asunto, mensaje_correo);

                //en caso que se haya enviado el correo 

                if (respuesta)
                {
                    //encriptar la contrasenia
                    obj.clave = CN_Recursos.ConvertirSha256(clave);

                    /*hacer el registro de la clave encriptada en la BD*/
                    return objCapaDato.Registrar(obj, out Mensaje);

                }
                else //si no se envio el correo
                {
                    Mensaje = "No se puede enviar el correo";
                    return 0;
                }
            }
            else
            {
                return 0;
            }
        }//termina el registrar 

        //EDITAR
        public bool Editar(Usuario obj, out string Mensaje)
        {


            Mensaje = string.Empty;

            /*validaciones de campos obligatorios*/

            if (string.IsNullOrEmpty(obj.Nombre) || string.IsNullOrWhiteSpace(obj.Nombre))
            {
                Mensaje = "Debe ingresar el nombre del usuario";
            }
            else if (string.IsNullOrEmpty(obj.Apellido1) || string.IsNullOrWhiteSpace(obj.Apellido1))
            {
                Mensaje = "Debe ingresar el apellido del usuario";
            }
            else if (string.IsNullOrEmpty(obj.Apellido2) || string.IsNullOrWhiteSpace(obj.Apellido2))
            {
                Mensaje = "Debe ingresar el apellido del usuario";
            }
            else if (string.IsNullOrEmpty(obj.Correo) || string.IsNullOrWhiteSpace(obj.Correo))
            {
                Mensaje = "Debe ingresar el correo del usuario";
            }

            if (string.IsNullOrEmpty(Mensaje))
            {
                return objCapaDato.Editar(obj, out Mensaje);

            }
            else
            {
                return false;
            }

        }//termina el editar

        //ELIMINAR 
        public bool Eliminar(int id, out string Mensaje)
        {

            return objCapaDato.Eliminar(id, out Mensaje);

        }//Termina Eliminar

        //CambiarClave 
        public bool CambiarClave(int idUsuario, string nuevaClave, out string Mensaje)
        {

            return objCapaDato.CambiarClave(idUsuario, nuevaClave, out Mensaje);

        }//Termina CambiarClave

        //ReestablecerClave 
        public bool ReestablecerClave(int idUsuario, string correo,  out string Mensaje)
        {
            Mensaje = string.Empty;
            string nuevaClave = CN_Recursos.GenerarClave();
            bool resultado = objCapaDato.ReestablecerClave(idUsuario, CN_Recursos.ConvertirSha256(nuevaClave),out Mensaje);

            if (resultado) {
                /*enviarle un correo con la contrasenia autogenerada */

                string asunto = "Sistema de Inscripcion TFG Informa: Contraseña Reestablecida";
                string mensaje_correo = "<h3>Su cuenta fue reestablecida correctamente</h3></br><p>Su contraseña para acceder ahora es: ¡clave!</p>";
                mensaje_correo = mensaje_correo.Replace("¡clave!", nuevaClave);
                bool respuesta = CN_Recursos.EnviarCorreo(correo, asunto, mensaje_correo);

                if (respuesta)
                {
                    return true;

                }
                else //si no se envio el correo
                {
                    Mensaje = "No se pudo enviar el correo";
                    return false;
                }//termina el if respuesta
            }//termina el if resultado 
            else
            {
                Mensaje = "No se pudo reestablecer la contrasenia";
                return false;
            }//termina el else

          
        }//termina el ReestablecerClave 

    }
}
