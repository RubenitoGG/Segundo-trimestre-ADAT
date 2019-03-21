using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProyectoCine.MODEL;

namespace ProyectoCine.DAL
{
    public class RepositorioVentas : GenericRepository<Venta>
    {
        public RepositorioVentas(Context context) : base(context)
        {
        }
    }
}
