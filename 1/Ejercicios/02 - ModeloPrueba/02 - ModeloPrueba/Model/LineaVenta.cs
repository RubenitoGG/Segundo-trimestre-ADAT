using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _02___ModeloPrueba.Model
{
    public class LineaVenta
    {
        public int LineaVentaId { get; set; }
        public int Cantidad { get; set; }
        public int ProductoId { get; set; }
        public int VentaId { get; set; }

        public virtual Producto Producto { get; set; }
        public virtual Venta Venta { get; set; }
    }
}
