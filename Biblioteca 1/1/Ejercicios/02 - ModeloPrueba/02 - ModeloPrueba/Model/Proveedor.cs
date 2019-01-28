using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _02___ModeloPrueba.Model
{
    public class Proveedor
    {
        public int ProveedorId { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string Telefono { get; set; }
        public string Movil { get; set; }
        public string Direccion { get; set; }
        public string Email { get; set; }
        public bool Habilitado { get; set; }

        public virtual ICollection<Producto> Productos { get; set; }

        public Proveedor()
        {
            Productos = new HashSet<Producto>();
        }
    }
}
