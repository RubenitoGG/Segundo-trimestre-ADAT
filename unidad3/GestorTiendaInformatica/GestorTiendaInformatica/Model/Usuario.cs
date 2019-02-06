using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestorTiendaInformatica.Model
{
    public class Usuario : PropertyValidateModel
    {
        public int UsuarioId { get; set; }

        [Required(ErrorMessage = "Tipo de cuenta obligatorio.")]
        [RegularExpression("Usuario|Administrador")]
        public string TipoCuenta { get; set; }

        [Required(ErrorMessage = "Nombre obligatorio.")]
        [StringLength(20, MinimumLength = 2)]
        public string Nombre { get; set; }

        [Required(ErrorMessage = "Usuario obligatorio.")]
        [StringLength(20, MinimumLength = 2)]
        public string user { get; set; }

        [Required(ErrorMessage = "Contraseña obligatoria.")]
        [StringLength(20, MinimumLength = 3)]
        public string password { get; set; }

        public virtual ICollection<Venta> Ventas { get; set; }
    }
}
