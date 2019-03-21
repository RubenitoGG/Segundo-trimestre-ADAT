using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProyectoCine.MODEL;

namespace ProyectoCine.DAL
{
    public class RepositorioEntradas : GenericRepository<Entrada>
    {
        public RepositorioEntradas(Context context) : base(context)
        {
        }
    }
}
