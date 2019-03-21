using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoCine.MODEL
{
	[Table("Categoria")]
	public class Categoria
	{
		public Categoria()
		{
			Peliculas = new HashSet<Pelicula>();
		}

		[Key]
		[Required(ErrorMessage = "Necesita un código de categoría.")]
		public string CodigoCategoria { get; set; }
		[Required(ErrorMessage = "Necesita un nombre de categoría")]
		public string nombre { get; set; }
		public string descripcion { get; set; }

		public virtual ICollection<Pelicula> Peliculas { get; set; }
	}
}
