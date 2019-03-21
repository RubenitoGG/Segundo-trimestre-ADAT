using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoCine.MODEL
{
	[Table("Pelicula")]
	public class Pelicula : PropertyValidateModel
    {
        public Pelicula()
        {
            Funciones = new HashSet<Funcion>();
        }

		[Key]
		[Required(ErrorMessage ="Necesita un codigo de pelicula.")]
		public string CodigoPelicula { get; set; }
		[Required(ErrorMessage = "Necesita un nombre de pelicula.")]
		public string nombre { get; set; }
		[Required(ErrorMessage = "Necesita una duracion de pelicula.")]
		public string duracion { get; set; }
		[Required(ErrorMessage = "Necesita una fecha de estreno de pelicula.")]
		public string fechaEstreno { get; set; }
		[Required(ErrorMessage = "Necesita una imagen de pelicula")]
		public string imagen { get; set; }

        [Required(ErrorMessage = "Necesita un Codigo de Productora de pelicula")]
        public string CodigoProductora { get; set; }

        [Required(ErrorMessage = "Necesita un Codigo de Categoria de pelicula")]
        public string CodigoCategoriaPelicula { get; set; }
        public bool enCartelera { get; set; }
        public string descripcion { get; set; }

        public virtual CategoriaPelicula Categoria { get; set; }
		public virtual Productora Productora { get; set; }

        public virtual ICollection<Funcion> Funciones { get; set; }
    }
}
