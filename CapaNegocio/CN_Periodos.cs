﻿using CapaDatos;
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
    }
}
