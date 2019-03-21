using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoCine.MODEL
{

    [Table("Venta")]
    public class Venta : PropertyValidateModel
    {
        public Venta()
        {
            LineasVentas = new HashSet<LineaVenta>();
        }
        public int VentaId { get; set; }
        [Required(ErrorMessage = "Se necesita la fecha de la venta")]
        public DateTime fecha { get; set; }
        [Required(ErrorMessage = "Se necesita el empleado de la venta")]
        public string CodigoEmpleado { get; set; }
        [Required(ErrorMessage = "Se necesita el cliente de la venta")]
        public int ClienteId { get; set; }

        public virtual Empleado Empleado { get; set; }
        public virtual Cliente Cliente { get; set; }
        public virtual ICollection<LineaVenta> LineasVentas { get; set; }
    }
}
