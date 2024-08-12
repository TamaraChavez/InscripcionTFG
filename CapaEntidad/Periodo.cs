using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaEntidad
{
    public class Periodo
    {
        public int IdPeriodo { get; set; }
        public int Activo { get; set; }
        public string Cuatrimestre { get; set; }
        public DateTime FechaInicio { get; set; }
        public TimeSpan HoraInicio { get; set; }
        public DateTime FechaFin { get; set; }
        public TimeSpan HoraFin { get; set; }
    }
}
