using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoCine.MODEL
{
	[Table("Sala")]
	public class Sala : PropertyValidateModel
    {
		public Sala()
        {
            Funciones = new HashSet<Funcion>();
        }

		[Key]
		[Required(ErrorMessage = "Necesita un codigo de sala.")]
		public string CodigoSala { get; set; }
		[Required(ErrorMessage = "Necesita un numero de sala.")]
		public int numero { get; set; }
		[Required(ErrorMessage = "Necesita un numero de filas de sala")]
		public int numFilas { get; set; }
		[Required(ErrorMessage = "Necesita un numero de columnas de sala")]
		public int numColumnas { get; set; }
		[Required(ErrorMessage = "Necesita un formato de sala")]
		public string formato { get; set; }

        public virtual ICollection<Funcion> Funciones { get; set; }
    }
}
