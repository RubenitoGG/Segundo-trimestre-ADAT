using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _02___ModeloPrueba.Model
{
    public class Venta
    {
        public int VentaId { get; set; }
        public Nullable<System.DateTime> FechaVentana { get; set; }
        public int EmpleadoId { get; set; }
        public int ClienteId { get; set; }

        public virtual ICollection<LineaVenta> LineaVentas { get; set; }
        public virtual Cliente Cliente { get; set; }

        public Venta()
        {
            LineaVentas = new HashSet<LineaVenta>();
        }

    }
}
