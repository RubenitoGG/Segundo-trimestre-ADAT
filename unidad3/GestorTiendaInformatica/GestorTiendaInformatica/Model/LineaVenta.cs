using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestorTiendaInformatica.Model
{
    public class LineaVenta : PropertyValidateModel
    {
        public int LineaVentaId { get; set; }

        [Required(ErrorMessage = "Cantidad obligatoria.")]
        public int Cantidad { get; set; }

        public virtual Venta venta { get; set; }

        public virtual Producto producto { get; set; }
    }
}
