using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GestorTiendaInformatica.Model;

namespace GestorTiendaInformatica.DAL
{
    public class ProveedorRepositorio : GenericRepository<Proveedor>
    {
        public ProveedorRepositorio(TiendaInformaticaContext context) : base(context) { }
    }
}
