using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Proyecto_Final___Pedro_Treviño.Models
{
    public class Empleados
    {

        public int id_empleado { get; set; }
        public string apellido { get; set; }
        public string nombre { get; set; }
        public string correo { get; set; }
        public string usuario { get; set; }
        public string contrasena { get; set; }
        public int rol { get; set; }

        public int categoria { get; set; }

        public decimal sueldo { get; set; }

    }
}