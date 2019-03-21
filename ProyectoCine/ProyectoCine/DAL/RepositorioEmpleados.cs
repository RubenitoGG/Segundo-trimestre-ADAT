using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProyectoCine.MODEL;

namespace ProyectoCine.DAL
{
    public class RepositorioEmpleados : GenericRepository<Empleado>
    {
        public RepositorioEmpleados(Context context) : base(context)
        {
        }
    }
}
