﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;


namespace CapaDatos
{
    public class Conexion
    {
        //el link de conexion se encuentra en el documento web config 

        public static string cn = ConfigurationManager.ConnectionStrings["cadena"].ToString();
    }
}
