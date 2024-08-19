using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaEntidad
{
    public class InscripcionesResueltas
    {
        public int idInscripcionResuelta { get; set; }
        public int idInscripcion { get; set; }
        public int idUsuarioDirector { get; set; }
        public string estado { get; set; }
        public int idUsuarioTutor { get; set; }

    }
}
