using CapaDatos;
using CapaEntidad;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace CapaNegocio
{
    public class CN_AsigTutores
    {
        private CD_AsigTutor objCapaDato = new CD_AsigTutor();

        public List<InscripcionesResueltas> Listar()
        {
            return objCapaDato.Listar();
        }
        // Método para listar tutores por carrera
        public List<Usuario> ListarTutoresCarrera()
        {
            List<Usuario> tutores = objCapaDato.ListarTutoresCarrera();
            if (tutores == null || tutores.Count == 0)
            {
                Debug.WriteLine("ListarTutoresCarrera - No se encontraron tutores disponibles.");
            }
            else
            {
                Debug.WriteLine($"ListarTutoresCarrera - Se encontraron {tutores.Count} tutores.");
            }
            return tutores;
        }


        public bool Editar(InscripcionesResueltas obj, out string Mensaje)
        {
            Mensaje = string.Empty;

            // Obtener el estado actual de la inscripción desde la base de datos
            var inscripcionActual = objCapaDato.Listar().FirstOrDefault(x => x.idInscripcionResuelta == obj.idInscripcionResuelta);

            if (inscripcionActual == null)
            {
                Mensaje = "No se encontró la inscripción especificada.";
                return false;
            }

            Debug.WriteLine($"Editar - Comenzando la validación para idInscipcionResuelta: {obj.idInscripcionResuelta}");
            if (inscripcionActual.estado.Trim() != "Aprobado")
            {
                Mensaje = "No se puede asignar un tutor porque el estado no es 'Aprobado'.";
                Debug.WriteLine($"Editar - Estado no es 'Aprobado'. Estado actual: {inscripcionActual.estado}");
                return false;
            }

            Debug.WriteLine("Editar - Estado es 'Aprobado', procediendo a la actualización.");

            // Proceder con la actualización
            int resultado = objCapaDato.Editar(obj, out Mensaje);
            Debug.WriteLine($"Editar - Resultado de la operación de edición: {resultado}");

            if (resultado > 0)
            {
                Debug.WriteLine("Editar - Actualización realizada exitosamente.");
                return true;
            }
            else
            {
                Debug.WriteLine("Editar - No se realizó ninguna actualización.");
                return false;
            }
        }







    }
}
