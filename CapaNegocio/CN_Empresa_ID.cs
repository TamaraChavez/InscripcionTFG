using CapaDatos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaNegocio
{
    public class CN_Empresa_ID
    {
        public int ObtenerIdEmpresa(string nomEmpresa)
        { 
            CD_Empresa_ID idEmpresa = new CD_Empresa_ID();
            return idEmpresa.ObtenerIdEmpresaPorNombre(nomEmpresa);
        }
    }
}
