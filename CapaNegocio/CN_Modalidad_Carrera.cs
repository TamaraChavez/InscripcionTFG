using CapaDatos;
using CapaEntidad;
using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaNegocio
{
    public class CN_Modalidad_Carrera
    {
        public List<Modalidad> ObtenerModalidadesPorCarrera(string idCarrera)
        {
            CD_Modalidad_Carreras modalidades = new CD_Modalidad_Carreras();
            return modalidades.ObtenerModalidadesPorCarrera(idCarrera);
        }
    }
}
