using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfBiblioteca.Model;

namespace WpfBiblioteca.DAL
{
    public class LibrosRepositorio : GenericRepository<Libro>
    {
        public LibrosRepositorio(BibliotecaContext context): base(context)
        { }

        public Libro GetFiltrado(String buscado)
        {
            if (!string.IsNullOrWhiteSpace(buscado))
            {
                Libro libro = Get(filter: (l => l.Titulo.ToUpper().Equals(buscado.ToUpper())
                                                || l.Isbn.ToUpper().Equals(buscado.ToUpper())
                                                || l.LibroId.ToString().ToUpper().Equals(buscado.ToUpper())),
                                           
                                   includeProperties:"Ejemplares").FirstOrDefault();

                return libro;
            }
            else return null;
        }

    }
}
