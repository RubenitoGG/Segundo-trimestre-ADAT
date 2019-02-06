using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GestorTiendaInformatica.Model;

namespace GestorTiendaInformatica.DAL
{
    public class CategoriaRepositorio : GenericRepository<Categoria>
    {
        public CategoriaRepositorio(TiendaInformaticaContext context) : base(context) { }
    }
}
