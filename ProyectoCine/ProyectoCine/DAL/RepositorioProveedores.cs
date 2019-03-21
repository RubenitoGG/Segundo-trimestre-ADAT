using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ProyectoCine.MODEL;
using System.Threading.Tasks;

namespace ProyectoCine.DAL
{
    public class RepositorioProveedores : GenericRepository<Proveedor>
    {
        public RepositorioProveedores(Context context) : base(context)
        {
        }
    }
}
