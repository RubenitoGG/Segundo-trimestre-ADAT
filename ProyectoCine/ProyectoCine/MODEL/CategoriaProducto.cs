using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoCine.MODEL
{
    [Table("CategoriaProducto")]
    public class CategoriaProducto : PropertyValidateModel
    {
        public CategoriaProducto()
        {
            Productos = new HashSet<Producto>();
        }
        [Key]
        [Required(ErrorMessage = "Necesita un código de categoría de producto.")]
        public string CodigoCategoriaProducto { get; set; }
        [Required(ErrorMessage = "Necesita el nombre de una categoría de producto.")]
        public string nombre { get; set; }
        [Required(ErrorMessage = "Necesita la imagen de una categoría de producto")]
        public string imagen { get; set; }
        public string descripcion { get; set; }
        public virtual ICollection<Producto> Productos { get; set; }
    }
}
