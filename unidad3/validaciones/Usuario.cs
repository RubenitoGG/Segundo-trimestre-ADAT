using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoTPV.Model
{
    public class Usuario : PropertyValidateModel
    {
        public Usuario()
        {
            TicketVenta = new HashSet<TicketVenta>();
        }

        public int UsuarioId { get; set; }

        [Required(ErrorMessage = "Nombre obligatorio")]
        [StringLength(20, MinimumLength = 2)]
        public string Nombre { get; set; }

        [StringLength(40, MinimumLength = 0)]
        public string Apellidos { get; set; }


        [Required(ErrorMessage = "Login obligatorio")]
        [Index(IsUnique = true)]
        [StringLength(15, MinimumLength = 2)]
        public string Login { get; set; }

        [Required(ErrorMessage = "Pin obligatorio")]
        [StringLength(4, MinimumLength = 4, ErrorMessage = "Introduce un Pin de 4 cifras")]
        public string Pin { get; set; }

        [Required(ErrorMessage = "tipo de usuario obligatorio")]
        [RegularExpression("usuario|admin")]
        public string TipoUsuario { get; set; }

        [EmailAddress(ErrorMessage = "Introduce un correo válido")]
        public string Email { get; set; }

        public string RutaImagen { get; set; }

        //PROPIEDADES DE NAVEGACION

        public virtual ICollection<TicketVenta> TicketVenta { get; set; }
    }
}

