using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoCine.MODEL
{
	[Table("Productora")]
	public class Productora : PropertyValidateModel
    {
		[Key]
		[Required(ErrorMessage = "Necesita un codigo de productora.")]
		public string CodigoProductora { get; set; }
		[Required(ErrorMessage = "Necesita un nombre de productora")]
        public string nombre { get; set; }
		public string pais { get; set; }

		public virtual ICollection<Pelicula> Peliculas { get; set; }
	}
}
