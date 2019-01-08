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
        public string Dni { get; set; }
        public string Nombre { get; set; }
        public string Apellidos { get; set; }
        public string Direccion { get; set; }
        public string Correo { get; set; }
        public string Telefono { get; set; }
        public string Password { get; set; }
        public virtual ICollection<Prestamo> Prestamos { get; set; }

        public Socio()
        {
            Prestamos = new HashSet<Prestamo>();
        }

    }
}
