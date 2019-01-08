using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace WpfBiblioteca.Model
{
    [Table(name: "Libros")]
    public class Libro
    {
        public int LibroId { get; set; }
        public string Isbn { get; set; }
        public string Titulo { get; set; }
        public string Editorial { get; set; }
        public string Descripcion { get; set; }
        public virtual ICollection<Ejemplar> Ejemplares { get; set; }

        public Libro()
        {
            Ejemplares = new HashSet<Ejemplar>();
        }
    }
}
