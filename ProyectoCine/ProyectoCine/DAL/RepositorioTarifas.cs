using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProyectoCine.MODEL;

namespace ProyectoCine.DAL
{
    public class RepositorioTarifas : GenericRepository<Tarifa>
    {
        public RepositorioTarifas(Context context) : base(context)
        {
        }
    }
}
