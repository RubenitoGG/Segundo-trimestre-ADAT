using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoCine.MODEL
{
	[Table("Cliente")]
	public class Cliente : PropertyValidateModel
    {
		public Cliente()
		{
			Entradas = new HashSet<Entrada>();
            Ventas = new HashSet<Venta>();
        }

        public int ClienteId { get; set; }
		[Required(ErrorMessage = "Necesita un tipo de cliente.")]
		public string tipo { get; set; }
		[Required(ErrorMessage = "Necesita un nombre de cliente.")]
		public string nombre { get; set; }
		[Required(ErrorMessage = "Necesita unos apellidos de cliente.")]
		public string apellidos { get; set; }
		[Required(ErrorMessage = "Necesita un NIF de cliente.")]
        [StringLength(9, MinimumLength = 9)]
        public string nif { get; set; }
		[Required(ErrorMessage = "Necesita un correo de cliente.")]
        [EmailAddress]
		public string correo{ get; set; }
        //Añadido nuevo hacer migracion 
        [Required(ErrorMessage = "Necesita un teléfono de cliente.")]
        [StringLength(9, MinimumLength = 9)]
        public string telefono { get; set; }
        [Required(ErrorMessage = "Necesita la dirección de cliente.")]
        public string direccion { get; set; }

        public virtual ICollection<Entrada> Entradas { get; set; }
        public virtual ICollection<Venta> Ventas { get; set; }

    }
}
