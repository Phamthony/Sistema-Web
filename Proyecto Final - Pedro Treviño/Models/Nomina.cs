using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace Proyecto_Final___Pedro_Treviño.Models
{
    public class Nomina
    {
        [Display(Name = "ID Empleado")]
        public int id_empleado { get; set; }

        [Display(Name = "Fecha de Entrada")]
        public DateTime FechaEntrada { get; set; }

        [Display(Name = "Fecha de Salida")]
        public DateTime FechaSalida { get; set; }

        public string Mensaje { get; set; }
        public decimal SueldoCalculado { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public double HorasTrabajadas { get; set; }
        public string TipoSueldo { get; set; }
        public decimal SueldoBase { get; set; }
    }
}