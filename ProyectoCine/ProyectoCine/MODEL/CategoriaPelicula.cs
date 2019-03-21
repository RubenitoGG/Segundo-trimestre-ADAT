using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoCine.MODEL
{
	[Table("CategoriaPelicula")]
	public class CategoriaPelicula : PropertyValidateModel
    {
		public CategoriaPelicula()
		{
			Peliculas = new HashSet<Pelicula>();
		}

		[Key]
		[Required(ErrorMessage = "Necesita un código de categoría de película.")]
		public string CodigoCategoriaPelicula { get; set; }
		[Required(ErrorMessage = "Necesita un nombre de categoría de película")]
        public string nombre { get; set; }
		public string descripcion { get; set; }

		public virtual ICollection<Pelicula> Peliculas { get; set; }
	}
}
