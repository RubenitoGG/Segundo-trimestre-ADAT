using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestorTiendaInformatica.Model
{
    public class Venta : PropertyValidateModel
    {
        public int VentaId { get; set; }

        public Nullable<System.DateTime> Fecha { get; set; }

        public virtual Cliente cliente { get; set; }

        public virtual Usuario usuario { get; set; }

        public virtual ICollection<LineaVenta> LineaVentas { get; set; }
    }
}
