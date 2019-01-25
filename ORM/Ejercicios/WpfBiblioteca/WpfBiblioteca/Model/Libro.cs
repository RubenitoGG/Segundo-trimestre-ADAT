using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WpfBiblioteca.Model
{
    [Table(name: "Libros")]
    public class Libro
    {
        public int LibroId { get; set; }

        [Required(ErrorMessage = "Isbn obligatorio.")]
        [Index(IsUnique = true)]
        [StringLength(10)]
        public string Isbn { get; set; }

        [Required(ErrorMessage = "Titulo obligatorio.")]
        [StringLength(50, MinimumLength = 1)]
        public string Titulo { get; set; }

        [Required(ErrorMessage = "Editorial obligatoria.")]
        [StringLength(50, MinimumLength = 1)]
        public string Editorial { get; set; }

        [Required(ErrorMessage = "Descripción obligatoria.")]
        public string Descripcion { get; set; }
        public virtual ICollection<Ejemplar> Ejemplares { get; set; }

        public Libro()
        {
            Ejemplares = new HashSet<Ejemplar>();
        }
    }
}
