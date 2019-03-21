using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoCine.MODEL
{
    [Table("Entrada")]
    public class Entrada : PropertyValidateModel
    {
        public int EntradaId { get; set; }
		[Required(ErrorMessage = "Necesita una butaca.")]
		public string butaca { get; set; }
		[Required(ErrorMessage = "Necesita un codigo de empleado.")]
        public string CodigoEmpleado { get; set; }
        [Required(ErrorMessage = "Necesita un nombre de empleado.")]
        public string nombreEmpleado { get; set; }
        [Required(ErrorMessage = "Necesita un codigo de cliente.")]
        public int ClienteId { get; set; }
        [Required(ErrorMessage = "Necesita un codigo de tarifa.")]
        public string CodigoTarifa { get; set; }
        [Required(ErrorMessage = "Necesita un codigo de funcion.")]
        public string CodigoFuncion { get; set; }
        [Required(ErrorMessage = "Necesita una hora de sesión.")]
        public string hora { get; set; }
        [Required(ErrorMessage = "Necesita una fecha de sesión.")]
        public string fecha { get; set; }

        public virtual Empleado Empleado { get; set; }
        public virtual Cliente Cliente { get; set; }
        public virtual Tarifa Tarifa { get; set; }
        public virtual Funcion Funcion { get; set; }
    }
}
