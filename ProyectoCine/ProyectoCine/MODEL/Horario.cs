using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoCine.MODEL
{
	[Table("Horario")]
	public class Horario : PropertyValidateModel
    {
		[Key]
        [Column(Order = 1)]
        [Required(ErrorMessage ="Necesita un dia de la semana para horario")]
		public string DiaSemana { get; set; }
        [Key]
        [Column(Order = 2)]
        [Required(ErrorMessage = "Necesita un codigo de funcion para horario")]
        public string CodigoFuncion { get; set; }
        [Required(ErrorMessage = "Necesita una hora de inicio para horario")]
		public string horaInicio { get; set; }
		[Required(ErrorMessage = "Necesita una hora de fin para horario")]
		public string horaFin { get; set; }

		public virtual Funcion Funcion { get; set; }
	}
}
