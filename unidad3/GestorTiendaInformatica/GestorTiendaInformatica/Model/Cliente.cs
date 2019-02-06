using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestorTiendaInformatica.Model
{
    public class Cliente: PropertyValidateModel
    {
        public int ClienteId { get; set; }

        [Required(ErrorMessage = "Nombre obligatorio.")]
        [StringLength(20, MinimumLength = 2)]
        public string Nombre { get; set; }

        [Required(ErrorMessage = "Telefono obligatorio.")]
        [StringLength(13, MinimumLength = 9)]
        public string Telefono { get; set; }

        [Required(ErrorMessage = "Email obligatorio.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Dirección obligatorio.")]
        public string Direccion { get; set; }

        public virtual ICollection<Venta> Ventas { get; set; }
    }
}
