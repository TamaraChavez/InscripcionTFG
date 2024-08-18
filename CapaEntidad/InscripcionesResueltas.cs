using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaEntidad
{
    public class InscripcionesResueltas
    {
        public int idInscipcionResuelta { get; set; }
        public int idUsuario { get; set; }
        public int idUsuarioDirector { get; set; }
        public string estado { get; set; }
        public int idUsuarioTutor { get; set; }

    }
}
