using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestorTiendaInformatica.Model
{
    [Table(name: "Proveedores")]
    public class Proveedor : PropertyValidateModel
    {
        public int ProveedorId { get; set; }

        [Required(ErrorMessage = "Nombre obligatorio.")]
        [StringLength(20, MinimumLength = 2)]
        public string Nombre { get; set; }

        [Required(ErrorMessage = "Telefono obligatorio.")]
        [StringLength(13, MinimumLength = 9)]
        public string Telefono { get; set; }

        [Required(ErrorMessage = "Descripción obligatoria.")]
        [StringLength(50, MinimumLength = 3)]
        public string Direccion { get; set; }

        [Required(ErrorMessage = "Nombre de contacto obligatorio.")]
        [StringLength(30, MinimumLength = 2)]
        public string NombreContacto { get; set; }

        public virtual ICollection<Producto> Productos { get; set; }
    }
}
