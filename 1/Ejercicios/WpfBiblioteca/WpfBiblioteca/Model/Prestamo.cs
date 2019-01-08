using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace WpfBiblioteca.Model
{
    [Table(name: "Prestamos")]
    public class Prestamo
    {
        [Key, Column(Order = 0)]
        public int LibroId { get; set; }
        [Key, Column(Order = 1)]
        public short NumeroEjemp { get; set; }
        [Key, Column(Order = 2)]
        public int SocioId { get; set; }
        public System.DateTime FechaPrestamo { get; set; }
        public Nullable<System.DateTime> FechaDevolucion { get; set; }
        public virtual Socio Socio { get; set; }
        public virtual Ejemplar Ejemplar { get; set; }
    }
}
