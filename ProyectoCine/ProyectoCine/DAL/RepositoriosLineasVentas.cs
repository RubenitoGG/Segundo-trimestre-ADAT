using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProyectoCine.MODEL;

namespace ProyectoCine.DAL
{
    public class RepositoriosLineasVentas : GenericRepository<LineaVenta>
    {
        public RepositoriosLineasVentas(Context context) : base(context)
        {
        }
    }
}
