using ProyectoCine.MODEL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoCine.DAL
{
    public class RepositorioCategoriasProductos : GenericRepository<CategoriaProducto>
    {
        public RepositorioCategoriasProductos(Context context) : base(context)
        {
        }
    }
}
