using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoCine.MODEL
{
    [Table("Producto")]
    public class Producto : PropertyValidateModel
    {
        public Producto()
        {
            LineasVentas = new HashSet<LineaVenta>();
        }
        [Key]
        [Required(ErrorMessage = "Necesita un código de producto.")]
        public string CodigoProducto { get; set; }
        [Required(ErrorMessage = "Necesita el nombre del producto")]
        public string nombre { get; set; }
        [Required(ErrorMessage = "Necesita el precio del producto")]
        public double precio { get; set; }
        [Required(ErrorMessage = "Necesita la imagen del producto")]
        public string imagen { get; set; }
        [Required(ErrorMessage = "Necesita el stock del producto")]
        public int stock { get; set; }
        [Required(ErrorMessage = "Necesita la categoría del producto")]
        public string CodigoCategoriaProducto { get; set; }
        [Required(ErrorMessage = "Necesita un proveedor del producto")]
        public string CodigoProveedor { get; set; }
        public virtual ICollection<LineaVenta> LineasVentas { get; set; }
        public virtual CategoriaProducto CategoriaProducto { get; set; }
        public virtual Proveedor Proveedor { get; set; }
    }
}
