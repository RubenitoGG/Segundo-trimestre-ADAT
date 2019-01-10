using System;
using System.Linq;


namespace WpfBiblioteca.DAL
{
    public class UnitOfWork : IDisposable
    {
        private BibliotecaContext context = new BibliotecaContext();
        private bool disposed = false;

        
        private SociosRepository sociosRepository;
        private LibrosRepositorio librosRepositorio;
        private  PrestamoRepositorio prestamosRepositorio;


        public PrestamoRepositorio PrestamosRepositorio
        {
            get
            {
                if (prestamosRepositorio == null)
                {
                    prestamosRepositorio =
                        new PrestamoRepositorio(context);
                }

                return prestamosRepositorio;
            }
        }
       public  SociosRepository SociosRepository
        {
            get
            {
                if (sociosRepository == null)
                {
                    sociosRepository =
                        new SociosRepository(context);
                }

                return sociosRepository;
            }
        }



        public LibrosRepositorio LibrosRepositorio
        {
            get
            {
                if (librosRepositorio == null)
                {
                    librosRepositorio = new LibrosRepositorio(context);
                }

                return librosRepositorio;
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
                {
                    context.Dispose();
                }
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