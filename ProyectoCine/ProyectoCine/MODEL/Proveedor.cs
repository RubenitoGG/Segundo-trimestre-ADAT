using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoCine.MODEL
{
    [Table("Proveedor")]
    public class Proveedor : PropertyValidateModel
    {
        public Proveedor()
        {
            Productos = new HashSet<Producto>();
        }
        [Key]
        [Required(ErrorMessage = "Necesita un código de proveedor.")]
        public string CodigoProveedor{ get; set; }
        [Required(ErrorMessage = "Necesita un tipo de proveedor.")]
        public string tipo { get; set; }
        [Required(ErrorMessage = "Necesita el nombre del proveedor.")]
        public string nombre { get; set; }
        [Required(ErrorMessage = "Necesita unos apellidos de proveedor.")]
        public string apellidos { get; set; }
        [Required(ErrorMessage = "Necesita un NIF de proveedor.")]
        [StringLength(9, MinimumLength = 9)]
        public string nif { get; set; }
        [Required(ErrorMessage = "Necesita el télefono de contacto del proveedor.")]
        [StringLength(9, MinimumLength = 9)]
        public string telefono { get; set; }
        [Required(ErrorMessage = "Necesita un correo de proveedor.")]
        [EmailAddress]
        public string correo { get; set; }
        [Required(ErrorMessage = "Necesita la dirección de contacto del proveedor.")]
        public string direccion { get; set; }
        public string personaContacto { get; set; }
        public virtual ICollection<Producto> Productos { get; set; }
    }
}
