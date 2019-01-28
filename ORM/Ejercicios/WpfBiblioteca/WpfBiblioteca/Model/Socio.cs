using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace WpfBiblioteca.Model
{
    [Table(name: "Socios")]
    public class Socio
    {
        [Key]
        public int SocioId { get; set; }

        [Required(ErrorMessage = "DNI obligatorio.")]
        [Index(IsUnique = true)]
        [StringLength(9, MinimumLength = 9)]
        public string Dni { get; set; }

        [Required(ErrorMessage = "Nombre obligatorio.")]
        [StringLength(20, MinimumLength = 3)]
        public string Nombre { get; set; }

        [Required(ErrorMessage = "Apellido obligatorio.")]
        [StringLength(20, MinimumLength = 3)]
        public string Apellidos { get; set; }

        [Required(ErrorMessage = "Direccion obligatoria.")]
        [StringLength(50, MinimumLength = 3)]
        public string Direccion { get; set; }

        [Required(ErrorMessage = "Correo obligatorio.")]
        [StringLength(50, MinimumLength = 3)]
        public string Correo { get; set; }

        [Required(ErrorMessage = "Nombre obligatorio.")]
        [StringLength(12, MinimumLength = 9)]
        public string Telefono { get; set; }

        [Required(ErrorMessage = "Nombre obligatorio.")]
        [StringLength(3, MinimumLength = 12)]
        public string Password { get; set; }
        public virtual ICollection<Prestamo> Prestamos { get; set; }

        public Socio()
        {
            Prestamos = new HashSet<Prestamo>();
        }

    }
}
