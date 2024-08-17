using CapaDatos;
using CapaEntidad;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaNegocio
{
    public class CN_Empresa
    {
        private CD_Empresa objCapaDatos = new CD_Empresa();
        public int CrearEmpresa(Empresa obj)
        {
            return objCapaDatos.Registrar(obj);
        }
    }
}
