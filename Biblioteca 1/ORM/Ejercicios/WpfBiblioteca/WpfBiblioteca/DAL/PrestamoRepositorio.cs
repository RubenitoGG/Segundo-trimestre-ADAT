using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfBiblioteca.Model;
namespace WpfBiblioteca.DAL
{
   public  class PrestamoRepositorio:GenericRepository<Prestamo>
    {
        public PrestamoRepositorio(BibliotecaContext context)
            : base(context)
        { }
    }
}
