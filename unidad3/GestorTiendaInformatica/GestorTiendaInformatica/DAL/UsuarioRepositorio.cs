using GestorTiendaInformatica.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestorTiendaInformatica.DAL
{
    public class UsuarioRepositorio : GenericRepository<Usuario>
    {
        public UsuarioRepositorio(TiendaInformaticaContext context) : base(context) { }
    }
}
