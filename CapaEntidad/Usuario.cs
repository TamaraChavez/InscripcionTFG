using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaEntidad
{
    public class Usuario
    {
        public int IdUsuario { get; set; }

        public String TipoUsuario { get; set; }

        public String Nombre { get; set; }
        public String Apellido1 { get; set; }
        public String Apellido2 { get; set; }

        public String Correo { get; set; }

        public String clave { get; set; }

        public int Carrera { get; set; }

        public bool Reestablecer { get; set; }

        public bool Activo { get; set; }
    }
}
