using CapaDatos;
using CapaEntidad;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaNegocio
{
    public class CN_Inscripcion
    {
        CD_Inscripcion objInscripcion = new CD_Inscripcion();

        public string CrearInscripcion(Inscripcion inscripcion)
        {
            return objInscripcion.Registrar(inscripcion);
        }
    }
}
