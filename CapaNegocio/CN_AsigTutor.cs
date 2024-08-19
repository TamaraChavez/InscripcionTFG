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
        private CN_AsigTutores objCapaDato = new CN_AsigTutores();

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

        //EDITAR
        public bool Editar(InscripcionesResueltas obj, out string Mensaje)
        {
            Mensaje = string.Empty;

            // Verificar si el estado es aprobado antes de asignar el tutor
            Debug.WriteLine($"Editar - Comenzando la validación para idInscipcionResuelta: {obj.idInscripcionResuelta}");
            if (obj.estado != "Aprobado")
            {
                Mensaje = "No se puede asignar un tutor porque el estado no es 'Aprobado'.";
                Debug.WriteLine($"Editar - Estado no es 'Aprobado'. Estado actual: {obj.estado}");
                return false;
            }

            Debug.WriteLine("Editar - Estado es 'Aprobado', procediendo a la actualización.");

            if (string.IsNullOrEmpty(Mensaje))
            {
                Debug.WriteLine("Editar - Mensaje de error está vacío, llamando a la capa de datos para editar.");
                bool resultado = objCapaDato.Editar(obj, out Mensaje); // Cambiar a 'bool' ya que el método devuelve 'bool'
                Debug.WriteLine($"Editar - Resultado de la operación de edición: {resultado}");
                if (resultado)
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
            else
            {
                Debug.WriteLine($"Editar - Mensaje de error no está vacío: {Mensaje}");
                return false;
            }
        }


  


    }
}
