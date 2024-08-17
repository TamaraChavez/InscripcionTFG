using CapaDatos;
using CapaEntidad;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaNegocio
{
    public class CN_Usuarios_Carrera
    {
        public List<Carrera> ObtenerCarrerasPorUsuario(int idUsuario)
        {
            CD_Usuario_Carreras carreras = new CD_Usuario_Carreras();
            return carreras.ObtenerCarrerasPorUsuario(idUsuario);
        }
    }
}
