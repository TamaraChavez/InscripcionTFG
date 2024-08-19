using CapaDatos;
using CapaEntidad;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaNegocio
{
    public class CN_Periodos
    {
        private CD_Periodos objCapaDatos = new CD_Periodos();

        public string Registrar(Periodo obj)
        {
            return objCapaDatos.Registrar(obj);
        }

        public Periodo ObtenerPeriodoActivo()
        { 
            
            return objCapaDatos.ObtenerPeriodoActivo();
        }

        public bool EsPeriodoHabilitado(Periodo periodo)
        {
            DateTime fechaActual = DateTime.Now;
            TimeSpan horaActual = DateTime.Now.TimeOfDay;

            if (fechaActual >= periodo.FechaInicio && fechaActual <= periodo.FechaFin)
            {
                if (fechaActual == periodo.FechaInicio && horaActual < periodo.HoraInicio)
                {
                    return false;
                }
                if (fechaActual == periodo.FechaFin && horaActual > periodo.HoraFin)
                {
                    return false;
                }
                return true;
            }
            return false;
        }
    }
}
