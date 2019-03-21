using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProyectoCine.MODEL;

namespace ProyectoCine.DAL
{
	public class UnitOfWork
	{
		Context context = new Context();

        private RepositorioPeliculas repositorioPeliculas;
        private RepositorioCategoriasPeliculas repositorioCategorias;
        private RepositorioProductoras repositorioProductoras;
        private RepositorioFunciones repositorioFunciones;
        private RepositorioSalas repositorioSalas;
        private RepositorioHorarios repositorioHorarios;
        private RepositorioEntradas repositorioEntradas;
        private RepositorioEmpleados repositorioEmpleados;
        private RepositorioClientes repositorioClientes;
        private RepositorioTarifas repositorioTarifas;

        private RepositorioCategoriasProductos repositorioCategoriasProductos;
        private RepositorioProductos repositorioProductos;
        private RepositorioProveedores repositorioProveedores;
        private RepositoriosLineasVentas repositoriosLineasVentas;
        private RepositorioVentas repositorioVentas;

        public RepositorioPeliculas RepositorioPeliculas
        {
            get
            {
                if (this.repositorioPeliculas == null)
                {
                    this.repositorioPeliculas = new RepositorioPeliculas(context);
                }

                return repositorioPeliculas;
            }
        }
        public RepositorioCategoriasPeliculas RepositorioCategorias
        {
            get
            {
                if (this.repositorioCategorias == null)
                {
                    this.repositorioCategorias = new RepositorioCategoriasPeliculas(context);
                }

                return repositorioCategorias;
            }
        }
        public RepositorioProductoras RepositorioProductoras
        {
            get
            {
                if (this.repositorioProductoras == null)
                {
                    this.repositorioProductoras = new RepositorioProductoras(context);
                }

                return repositorioProductoras;
            }
        }
        public RepositorioFunciones RepositorioFunciones
        {
            get
            {
                if (this.repositorioFunciones == null)
                {
                    this.repositorioFunciones = new RepositorioFunciones(context);
                }

                return repositorioFunciones;
            }
        }
        public RepositorioSalas RepositorioSalas
        {
            get
            {
                if (this.repositorioSalas == null)
                {
                    this.repositorioSalas = new RepositorioSalas(context);
                }

                return repositorioSalas;
            }
        }
        public RepositorioHorarios RepositorioHorarios
        {
            get
            {
                if (this.repositorioHorarios == null)
                {
                    this.repositorioHorarios = new RepositorioHorarios(context);
                }

                return repositorioHorarios;
            }
        }
        public RepositorioEntradas RepositorioEntradas
        {
            get
            {
                if (this.repositorioEntradas == null)
                {
                    this.repositorioEntradas = new RepositorioEntradas(context);
                }

                return repositorioEntradas;
            }
        }
        public RepositorioEmpleados RepositorioEmpleados
        {
            get
            {
                if (this.repositorioEmpleados == null)
                {
                    this.repositorioEmpleados = new RepositorioEmpleados(context);
                }

                return repositorioEmpleados;
            }
        }
        public RepositorioClientes RepositorioClientes
        {
            get
            {
                if (this.repositorioClientes == null)
                {
                    this.repositorioClientes = new RepositorioClientes(context);
                }

                return repositorioClientes;
            }
        }
        public RepositorioTarifas RepositorioTarifas
        {
            get
            {
                if (this.repositorioTarifas == null)
                {
                    this.repositorioTarifas = new RepositorioTarifas(context);
                }

                return repositorioTarifas;
            }
        }

        public RepositorioCategoriasProductos RepositorioCategoriasProductos
        {
            get
            {
                if (this.repositorioCategoriasProductos == null)
                {
                    this.repositorioCategoriasProductos = new RepositorioCategoriasProductos(context);
                }

                return repositorioCategoriasProductos;
            }
        }

        public RepositorioProductos RepositorioProductos
        {
            get
            {
                if (this.repositorioProductos == null)
                {
                    this.repositorioProductos = new RepositorioProductos(context);
                }

                return repositorioProductos;
            }
        }

        public RepositorioProveedores RepositorioProveedores
        {
            get
            {
                if (this.repositorioProveedores == null)
                {
                    this.repositorioProveedores = new RepositorioProveedores(context);
                }

                return repositorioProveedores;
            }
        }

        public RepositoriosLineasVentas RepositoriosLineasVentas
        {
            get
            {
                if (this.repositoriosLineasVentas == null)
                {
                    this.repositoriosLineasVentas = new RepositoriosLineasVentas(context);
                }

                return repositoriosLineasVentas;
            }
        }

        public RepositorioVentas RepositorioVentas
        {
            get
            {
                if (this.repositorioVentas == null)
                {
                    this.repositorioVentas = new RepositorioVentas(context);
                }

                return repositorioVentas;
            }
        }
    }
}
