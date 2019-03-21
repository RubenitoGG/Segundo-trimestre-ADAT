using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoCine.MODEL
{
	[Table("Empleado")]
	public class Empleado : PropertyValidateModel
    {
		public Empleado()
		{
			Entradas = new HashSet<Entrada>();

            Ventas = new HashSet<Venta>();
        }

		[Key]
		[Required(ErrorMessage = "Necesita un código de empleado.")]
		public string CodigoEmpleado { get; set; }
		[Required(ErrorMessage = "Necesita un rango de empleado.")]
		public string rango { get; set; }
		[Required(ErrorMessage = "Necesita un nombre de empleado.")]
		public string nombre { get; set; }
		[Required(ErrorMessage = "Necesita unos apellidos de empleado.")]
		public string apellidos { get; set; }
		[Required(ErrorMessage = "Necesita un usuario de empleado.")]
		public string usuario { get; set; }
		[Required(ErrorMessage = "Necesita una contraseña de empleado.")]
		[StringLength(16, MinimumLength = 5, ErrorMessage = "La contraseña debe tener minimo 6 caracteres y maximo 16.")]
		public string contrasena { get; set; }
		[Required(ErrorMessage = "Necesita un NIF de empleado.")]
        [StringLength(9,MinimumLength =9)]
		public string nif { get; set; }
        //Añadido nuevo hacer migracion 
        [Required(ErrorMessage = "Necesita un teléfono de empleado.")]
        [StringLength(9, MinimumLength = 9)]
        public string telefono { get; set; }
        [Required(ErrorMessage = "Necesita un correo de empleado.")]
        [EmailAddress]
        public string correo { get; set; }
        [Required(ErrorMessage = "Necesita la dirección de empleado.")]
        public string direccion { get; set; }
        [Required(ErrorMessage = "Necesita una Fecha de alta de empleado")]
		public string fechaAlta { get; set; }

		public virtual ICollection<Entrada> Entradas { get; set; }

        public virtual ICollection<Venta> Ventas { get; set; }
    }
}
