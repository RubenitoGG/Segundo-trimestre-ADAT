using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoCine.MODEL
{
	[Table("Funcion")]
	public class Funcion : PropertyValidateModel
    {
		public Funcion()
		{
			Horarios = new HashSet<Horario>();
		}

        [Key]
        [Required(ErrorMessage = "Necesita un código de función.")]
        public string CodigoFuncion { get; set; }
        [Required(ErrorMessage = "Necesita un formato de función.")]
        public string formato { get; set; }
        [Required(ErrorMessage = "Necesita un código de sala de función.")]
        public string CodigoSala { get; set; }
        [Required(ErrorMessage = "Necesita un código de película de función.")]
        public string CodigoPelicula { get; set; }
		[Required(ErrorMessage = "Necesita un fecha de inicio de función.")]
		public string fechaInicio { get; set; }
		[Required(ErrorMessage = "Necesita un fecha de final de función.")]
		public string fechaFin { get; set; }

		public bool activo { get; set; }

		public virtual Pelicula Pelicula { get; set; }
        public virtual Sala Sala { get; set; }

		public virtual ICollection<Horario> Horarios { get; set; }
	}
}
