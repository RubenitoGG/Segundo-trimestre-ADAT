using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProyectoCine.MODEL;

namespace ProyectoCine.DAL
{
    public class RepositorioPeliculas : GenericRepository<Pelicula>
    {
        public RepositorioPeliculas(Context context) : base(context)
        {
        }
    }
}
