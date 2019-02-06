using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GestorTiendaInformatica.Model;

namespace GestorTiendaInformatica.DAL
{
    public class ClienteRepositorio : GenericRepository<Cliente>
    {
        public ClienteRepositorio(TiendaInformaticaContext context) : base(context) { }
    }
}
