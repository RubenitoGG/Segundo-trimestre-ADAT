using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _02___ModeloPrueba.Model
{
    public class Producto
    {
        public int ProductoId { get; set; }
        public double Precio { get; set; }
        public string NombreProducto { get; set; }
        public string NombreMarca { get; set; }
        public string AnimalDirigido { get; set; }
        public double Peso { get; set; }
        public double Tamaño { get; set; }
        public string Imagen { get; set; }
        public int Stock { get; set; }
        public string Categoria { get; set; }
        public int ProveedorId { get; set; }
        public bool Habilitado { get; set; }

        public virtual Proveedor Proveedor { get; set; }
        public virtual ICollection<LineaVenta> LineaVentas { get; set; }

        public Producto()
        {
            LineaVentas = new HashSet<LineaVenta>();
        }
    }
}
