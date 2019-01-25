using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace WpfBiblioteca.Model
{
    [Table(name: "Ejemplares")]
    public class Ejemplar
    {
        [Key, ForeignKey("Libro"), Column(Order = 0)]
        public int LibroId { get; set; }

        [Required(ErrorMessage = "Nº Ejemplar obligatorio.")]
        [Key, Column(Order = 1)]
        public int NumeroEjemplar { get; set; }

        [Required(ErrorMessage = "Fecha obligatoria.")]
        public Nullable<System.DateTime> FechaPublicacion { get; set; }
        public string Estado { get; set; }
        public virtual Libro Libro { get; set; }
        public virtual ICollection<Prestamo> Prestamos{ get; set; }

        public Ejemplar()
        {
            Prestamos = new HashSet<Prestamo>();
        }
    }
}
