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
            Libro  libro= Get(filter: (l => l.Titulo.ToUpper().Contains(buscado.ToUpper())
                                            || l.Isbn.ToUpper().Contains(buscado.ToUpper())
                                            || l.LibroId.ToString().ToUpper().Contains(buscado.ToUpper())),
                                           
                                   includeProperties:"ejemplar").FirstOrDefault();

                return libro;
            }
            else return null;
        }

    
    }
}
