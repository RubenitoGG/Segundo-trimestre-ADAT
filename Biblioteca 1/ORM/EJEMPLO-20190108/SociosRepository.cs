using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WpfBiblioteca.Model;

namespace WpfBiblioteca.DAL
{
    public class SociosRepository : GenericRepository<Socio>
    {
        public SociosRepository(BibliotecaContext context)
            : base(context)
        {
        }
        public IEnumerable<Socio> GetFiltrado(String buscado)
        {
            if (!string.IsNullOrWhiteSpace(buscado))
            {
                return Get(filter: (socio => socio.Apellidos.ToUpper().Contains(buscado.ToUpper())
                                            || socio.Nombre.ToUpper().Contains(buscado.ToUpper())
                                            || socio.SocioId.ToUpper().Contains(buscado.ToUpper())
                                            || socio.Telefono.ToUpper().Contains(buscado.ToUpper())
                                            
                                            || socio.Dni.ToUpper().Contains(buscado.ToUpper())
                                            ),
                                includeProperties: "prestamo") ;
            }
            else return Get();
        }
        public Socio getValidar(string correo, string pass) {
            if (!string.IsNullOrWhiteSpace(correo) && !string.IsNullOrWhiteSpace(pass))
                return Single(socio => socio.Correo.ToUpper().Contains(correo.ToUpper()) && socio.Password.ToUpper().Contains(pass.ToUpper()));
            else return null;
        }
       
    }
}