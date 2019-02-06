using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestorTiendaInformatica.Model
{
    public class Categoria : PropertyValidateModel
    {
        public int CategoriaId { get; set; }

        [Required(ErrorMessage = "Nombre obligatorio.")]
        [StringLength(20, MinimumLength = 2)]
        public string Nombre { get; set; }

        [Required(ErrorMessage = "Imagen obligatoria.")]
        public string Imagen { get; set; }

        [Required(ErrorMessage = "Descripción obligatoria.")]
        [StringLength(100, MinimumLength = 0)]
        public string Descripcion { get; set; }

        public virtual ICollection<Producto> Productos { get; set; }
    }
}
