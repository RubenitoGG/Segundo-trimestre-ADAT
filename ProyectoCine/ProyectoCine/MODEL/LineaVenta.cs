using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoCine.MODEL
{
    [Table("LineaVenta")]
    public class LineaVenta : PropertyValidateModel
    {
        [Key]
        [Column(Order = 1)]
        public int CodigoLineaVenta { get; set; }
        public int Cantidad { get; set; }
        [Required(ErrorMessage = "Necesita el producto de la linea de venta")]
        public string CodigoProducto { get; set; }
        [Required(ErrorMessage = "Necesita la venta de la linea de venta")]
        [Key]
        [Column(Order = 2)]
        public int VentaId { get; set; }
        public virtual Venta Venta { get; set; }
        public virtual Producto Producto { get; set; }
    }
}
