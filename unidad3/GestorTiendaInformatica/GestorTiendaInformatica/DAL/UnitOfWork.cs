using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestorTiendaInformatica.DAL
{
    public class UnitOfWork : IDisposable
    {
        private TiendaInformaticaContext context = new TiendaInformaticaContext();
        private bool disposed = false;

        private CategoriaRepositorio categoriaRepositorio;
        private ClienteRepositorio clienteRepositorio;
        private LineaVentaRepositorio lineaVentaRepositorio;
        private ProductoRepositorio productoRepositorio;
        private ProveedorRepositorio proveedorRepositorio;
        private UsuarioRepositorio usuarioRepositorio;
        private VentaRepositorio ventaRepositorio;

        public CategoriaRepositorio CategoriaRepositorio
        {
            get
            {
                if (categoriaRepositorio == null)
                    categoriaRepositorio = new CategoriaRepositorio(context);

                return categoriaRepositorio;
            }
        }

        public ClienteRepositorio ClienteRepositorio
        {
            get
            {
                if (clienteRepositorio == null)
                    clienteRepositorio = new ClienteRepositorio(context);

                return clienteRepositorio;
            }
        }

        public LineaVentaRepositorio LineaVentaRepositorio
        {
            get
            {
                if (lineaVentaRepositorio == null)
                    lineaVentaRepositorio = new LineaVentaRepositorio(context);

                return lineaVentaRepositorio;
            }
        }
        public ProductoRepositorio ProductoRepositorio
        {
            get
            {
                if (productoRepositorio == null)
                    productoRepositorio = new ProductoRepositorio(context);

                return productoRepositorio;
            }
        }
        public ProveedorRepositorio ProveedorRepositorio
        {
            get
            {
                if (proveedorRepositorio == null)
                    proveedorRepositorio = new ProveedorRepositorio(context);

                return proveedorRepositorio;
            }
        }

        public UsuarioRepositorio UsuarioRepositorio
        {
            get
            {
                if (usuarioRepositorio == null)
                    usuarioRepositorio = new UsuarioRepositorio(context);

                return usuarioRepositorio;
            }
        }

        public VentaRepositorio VentaRepositorio
        {
            get
            {
                if (ventaRepositorio == null)
                    ventaRepositorio = new VentaRepositorio(context);

                return ventaRepositorio;
            }
        }

        public void Save()
        {
            context.SaveChanges();
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposed)
            {
                if (disposing)
                    context.Dispose();
            }
            disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
