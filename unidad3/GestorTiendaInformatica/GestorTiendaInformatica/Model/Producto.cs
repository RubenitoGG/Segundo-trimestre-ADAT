using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestorTiendaInformatica.Model
{
    [Table(name: "Productos")]
    public class Producto : PropertyValidateModel
    {
        public int ProductoId { get; set; }

        [Required(ErrorMessage = "Nombre obligatorio.")]
        [StringLength(20, MinimumLength = 2)]
        public string Nombre { get; set; }

        [Required(ErrorMessage = "Descripción obligatoria.")]
        [StringLength(100, MinimumLength = 0)]
        public string Descripcion { get; set; }

        [Required(ErrorMessage = "Imagen obligatoria.")]
        public string Imagen { get; set; }

        [Required(ErrorMessage = "Precio obligatorio.")]
        public double Precio { get; set; }

        [Required(ErrorMessage = "Stock obligatorio.")]
        public int Stock { get; set; }

        public ICollection<LineaVenta> lineaVentas { get; set; }

        public virtual Proveedor Proveedor { get; set; }

        public virtual Categoria Categoria { get; set; }
    }
}
