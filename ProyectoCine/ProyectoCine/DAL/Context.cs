using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProyectoCine.MODEL;
using ProyectoCine.Migrations;
using System.Data.Entity.Migrations;

namespace ProyectoCine.DAL
{
	public class Context : DbContext
	{
		public Context() :
			base("CineAureaEntities")
		{
			if (Database.Exists())
			{
				Database.SetInitializer(new MigrateDatabaseToLatestVersion<Context, Configuration>());
			}
			else
			{
				Database.SetInitializer(new creardb());
			}
		}

        public DbSet<Pelicula> Peliculas { get; set; }
        public DbSet<CategoriaPelicula> CategoriasPeliculas { get; set; }
        public DbSet<Productora> Productoras { get; set; }
        public DbSet<Funcion> Funciones { get; set; }
        public DbSet<Sala> Salas { get; set; }
        public DbSet<Horario> Horarios { get; set; }
        public DbSet<Entrada> Entradas { get; set; }
        public DbSet<Empleado> Empleados { get; set; }
        public DbSet<Cliente> Clientes { get; set; }
        public DbSet<Tarifa> Tarifas { get; set; }

        public DbSet<CategoriaProducto> CategoriasProductos { get; set; }
        public DbSet<Producto> Productos { get; set; }
        public DbSet<Proveedor> Proveedores { get; set; }
        public DbSet<LineaVenta> LineasVentas { get; set; }
        public DbSet<Venta> Ventas { get; set; }

        class creardb : CreateDatabaseIfNotExists<Context>
		{
			protected override void Seed(Context context)
			{
                List<Empleado> empleados = new List<Empleado>()
                {
                    new Empleado { CodigoEmpleado = "admin", nombre = "Benigno", apellidos = "Feijoo Otero", usuario = "admin", contrasena = "admin", fechaAlta = "21/02/2018", nif = "39921398T", rango = "Super Administrador", telefono = "629120432", correo="cineaurea@gmail.com", direccion="Calle Murillo nº 36 bajo" },
                    new Empleado { CodigoEmpleado = "adminproductos", nombre = "Carlos", apellidos = "Gómez Fernández", usuario = "adminproductos", contrasena = "adminproductos", fechaAlta = "08/03/2018", nif = "98234492V", rango = "Administrador departamento comestibles", telefono = "722419834", correo="admincomestibles@gmail.com", direccion="Calle Raimundo nº 9, 1º" },
                    new Empleado { CodigoEmpleado = "adminentradas", nombre = "Helena", apellidos = "López Molina", usuario = "adminentradas", contrasena = "adminentradas", fechaAlta = "09/04/2018", nif = "28836742C", rango = "Administrador departamento cine", telefono = "672113286", correo="adminentradas@gmail.com", direccion="Calle Santa Lucia nº 32, 5º" },
                    new Empleado { CodigoEmpleado = "empproductos", nombre = "Gabriel", apellidos = "Guios Barroso", usuario = "empproductos", contrasena = "empproductos", fechaAlta = "09/05/2018", nif = "75230982A", rango = "Empleado tienda comestibles", telefono = "644329852", correo="empcomestibles@gmail.com", direccion="Calle Greco nº 24, bajo" },
                    new Empleado { CodigoEmpleado = "empentradas", nombre = "Jason", apellidos = "Franco Danilo", usuario = "empentradas", contrasena = "empentradas", fechaAlta = "15/04/2018", nif = "89924763G", rango = "Empleado tienda cine", telefono = "632944157", correo="empentradas@gmail.com", direccion="Calle Gran Vía nº 128, 4º A" },
                    new Empleado { CodigoEmpleado = "nereall", nombre = "Nerea", apellidos = "López López", usuario = "nereall", contrasena = "abc123.", fechaAlta = "12/04/2018", nif = "43924763L", rango = "Administrador departamento cine", telefono = "632947421", correo="nereall@gmail.com", direccion="Calle Valor nº 15, 4º A" },
                    new Empleado { CodigoEmpleado = "aitorff", nombre = "Aitor", apellidos = "Fernández Fernández", usuario = "aitorff", contrasena = "abc123.", fechaAlta = "21/09/2018", nif = "92014927T", rango = "Administrador departamento comestibles", telefono = "723981029", correo="aitorff@gmail.com", direccion="Calle Concordato nº 2, 4º A" }
                };

				foreach(Empleado emp in empleados)
                {
                    context.Empleados.AddOrUpdate(emp);
                    context.SaveChanges();
                }

                List<Cliente> clientes = new List<Cliente>()
                {
                    new Cliente {tipo = "Normal", nombre = "Cliente Generico", apellidos = "Generico", nif = "00000000A", correo = "clientegenerico@gmail.com", telefono = "000000000", direccion="Generico" },
                    new Cliente {tipo = "Socio", nombre = "Ricardo", apellidos = "Pazos do Muiño", nif = "72452292F", correo = "richardo@gmail.com", telefono = "618672912", direccion="Calle Nobel nº 12, 3º" },
                    new Cliente {tipo = "Socio", nombre = "Daniel", apellidos = "Vázquez Molina", nif = "52334516X", correo = "comodios@gmail.com", telefono = "752993210", direccion="Calle Lejana nº 44, 4º" },
                    new Cliente {tipo = "Socio", nombre = "Victor", apellidos = "Santalices Feijoo", nif = "39102948P", correo = "santalices@gmail.com", telefono = "698019274", direccion="Calle Tranquila nº 61, 4º" },
                    new Cliente {tipo = "Socio", nombre = "Ximena", apellidos = "Gallardo Díaz", nif = "87209123Z", correo = "number23@gmail.com", telefono = "621082398", direccion="Calle Zapatilla nº 23, 4º" },
                    new Cliente {tipo = "Socio", nombre = "Clara", apellidos = "López Sánchez", nif = "62901642O", correo = "tattooadiccion@gmail.com", telefono = "&28192073", direccion="Calle Moderna nº 1, 4º" }
                };

                foreach(Cliente cli in clientes)
                {
                    context.Clientes.AddOrUpdate(cli);
                    context.SaveChanges();
                }

                List<Sala> salas = new List<Sala>()
                {
                    new Sala { CodigoSala = "generico", numero = 1, numFilas = 10, numColumnas = 10, formato = "Normal" },
                    new Sala { CodigoSala = "salanormalpequeña", numero = 2, numFilas = 8, numColumnas = 6, formato = "Normal" },
                    new Sala { CodigoSala = "salanormalmediana", numero = 3, numFilas = 12, numColumnas = 10, formato = "Normal" },
                    new Sala { CodigoSala = "salanormalgrande", numero = 4, numFilas = 16, numColumnas = 14, formato = "Normal" },
                    new Sala { CodigoSala = "salavippequeña", numero = 5, numFilas = 6, numColumnas = 4, formato = "Vip" },
                    new Sala { CodigoSala = "salavipmediana", numero = 6, numFilas = 10, numColumnas = 8, formato = "Vip" },
                    new Sala { CodigoSala = "salavipgrande", numero = 7, numFilas = 14, numColumnas = 12, formato = "Vip" },
                    new Sala { CodigoSala = "salaestreno", numero = 8, numFilas = 20, numColumnas = 18, formato = "Vip" }
                };

                foreach(Sala sal in salas)
                {
                    context.Salas.AddOrUpdate(sal);
                    context.SaveChanges();
                }

                List<CategoriaPelicula> categoriaPeliculas = new List<CategoriaPelicula>
                {
                    new CategoriaPelicula { CodigoCategoriaPelicula = "generico", nombre = "Categoria Generica", descripcion = "Categoría por defecto" },
                    new CategoriaPelicula { CodigoCategoriaPelicula = "cataccion", nombre = "Acción", descripcion = "Contiene películas de acción" },
                    new CategoriaPelicula { CodigoCategoriaPelicula = "catficcion", nombre = "Ficción", descripcion = "Contiene películas de ficción" },
                    new CategoriaPelicula { CodigoCategoriaPelicula = "catanimacion", nombre = "Animación", descripcion = "Contiene películas de animación" },
                    new CategoriaPelicula { CodigoCategoriaPelicula = "catsuspense", nombre = "Suspense", descripcion = "Contiene películas de suspense" },
                    new CategoriaPelicula { CodigoCategoriaPelicula = "catdrama", nombre = "Drama", descripcion = "Contiene películas de drama" },
                    new CategoriaPelicula { CodigoCategoriaPelicula = "catcomedia", nombre = "Comedia", descripcion = "Contiene películas de comedia" },
                    new CategoriaPelicula { CodigoCategoriaPelicula = "catterror", nombre = "Terror", descripcion = "Contiene películas de terror" }
                };
                foreach(CategoriaPelicula catPel in categoriaPeliculas)
                {
                    context.CategoriasPeliculas.AddOrUpdate(catPel);
                    context.SaveChanges();
                }

                List<Productora> productoras = new List<Productora>()
                {
                     new Productora { CodigoProductora = "generico", nombre = "Productora Generica", pais = "España" },
                     new Productora { CodigoProductora = "prowarner", nombre = "Warner Bros", pais = "EE.UU" },
                     new Productora { CodigoProductora = "promarvel", nombre = "Marvel Studios", pais = "EE.UU" },
                     new Productora { CodigoProductora = "profox", nombre = "FOX", pais = "EE.UU" },
                     new Productora { CodigoProductora = "prohbo", nombre = "HBO", pais = "EE.UU" },
                     new Productora { CodigoProductora = "pronetflix", nombre = "Netflix", pais = "EE.UU" }
                };
                foreach(Productora pro in productoras)
                {
                    context.Productoras.AddOrUpdate(pro);
                    context.SaveChanges();
                }

                List<Proveedor> proveedores = new List<Proveedor>()
                {
                    new Proveedor { CodigoProveedor = "generico", nombre = "Pedro",apellidos="Martínez Alonso", telefono = "689251329", direccion = "Calle Fina nº 21, 2º", correo="proveedorgenerico@gmail.com", nif="43921082R", tipo="Particular", personaContacto="Generico"},
                    new Proveedor { CodigoProveedor = "prvJonathan", nombre = "Jonathan",apellidos="Moro Bahamonde", telefono = "712954023", direccion = "Calle Conocida nº 32, 2º", correo="jonmoro@gmail.com", nif="43124553N", tipo="Particular", personaContacto="Jonathan"},
                    new Proveedor { CodigoProveedor = "prvGalaxia", nombre = "Arturo",apellidos="Otero Diaz", telefono = "693559942", direccion = "Calle Grandiosa nº 45, 5º", correo="arturo@gmail.com", nif="75469281A", tipo="Empresa", personaContacto="Arturo"},
                    new Proveedor { CodigoProveedor = "prvPlenux", nombre = "Iria",apellidos="Fernández López", telefono = "719082743", direccion = "Calle Generosa nº 45, 5º", correo="iria@gmail.com", nif="91820281T", tipo="Empresa", personaContacto="Iria"},
                    new Proveedor { CodigoProveedor = "prvMaria", nombre = "Maria del Carmen",apellidos="Otero Fernandez", telefono = "623901826", direccion = "Calle Blanco nº 42, 2º", correo="maria@gmail.com", nif="71829321A", tipo="Particular", personaContacto="Maria"}
                };
                foreach(Proveedor prv in proveedores)
                {
                    context.Proveedores.AddOrUpdate(prv);
                    context.SaveChanges();
                }

                List<Tarifa> tarifas = new List<Tarifa>()
                {
                    new Tarifa { CodigoTarifa = "trfNormal", formatoSala = "Normal", formatoCliente = "Normal", formatoFuncion = "Normal", diaEspectador = false, precio = 7.90},
                    new Tarifa { CodigoTarifa = "trfNormalEspectador", formatoSala = "Normal", formatoCliente = "Normal", formatoFuncion = "Normal", diaEspectador = true, precio = 4.90},
                    new Tarifa { CodigoTarifa = "trfNormalSocio", formatoSala = "Normal", formatoCliente = "Socio", formatoFuncion = "Normal", diaEspectador = false, precio = 7.50},
                    new Tarifa { CodigoTarifa = "trfNormalSocioEspectador", formatoSala = "Normal", formatoCliente = "Socio", formatoFuncion = "Normal", diaEspectador = true, precio = 4.50},

                    new Tarifa { CodigoTarifa = "trf3D", formatoSala = "Normal", formatoCliente = "Normal", formatoFuncion = "3D", diaEspectador = false, precio = 9.90},
                    new Tarifa { CodigoTarifa = "trf3DEspectador", formatoSala = "Normal", formatoCliente = "Normal", formatoFuncion = "3D", diaEspectador = true, precio = 7.90},
                    new Tarifa { CodigoTarifa = "trf3DSocio", formatoSala = "Normal", formatoCliente = "Socio", formatoFuncion = "3D", diaEspectador = false, precio = 9.50},
                    new Tarifa { CodigoTarifa = "trf3DSocioEspectador", formatoSala = "Normal", formatoCliente = "Socio", formatoFuncion = "3D", diaEspectador = true, precio = 7.50},

                    new Tarifa { CodigoTarifa = "trfVip", formatoSala = "Vip", formatoCliente = "Normal", formatoFuncion = "Normal", diaEspectador = false, precio = 12.90},
                    new Tarifa { CodigoTarifa = "trfVipEspectador", formatoSala = "Vip", formatoCliente = "Normal", formatoFuncion = "Normal", diaEspectador = true, precio = 7.90},
                    new Tarifa { CodigoTarifa = "trfVipSocio", formatoSala = "Vip", formatoCliente = "Socio", formatoFuncion = "Normal", diaEspectador = false, precio = 10.50},
                    new Tarifa { CodigoTarifa = "trfVipSocioEspectador", formatoSala = "Vip", formatoCliente = "Socio", formatoFuncion = "Normal", diaEspectador = true, precio = 7.50},

                    new Tarifa { CodigoTarifa = "trfVip3D", formatoSala = "Vip", formatoCliente = "Normal", formatoFuncion = "3D", diaEspectador = false, precio = 16.90},
                    new Tarifa { CodigoTarifa = "trfVip3DEspectador", formatoSala = "Vip", formatoCliente = "Normal", formatoFuncion = "3D", diaEspectador = true, precio = 12.90},
                    new Tarifa { CodigoTarifa = "trfVip3DSocio", formatoSala = "Vip", formatoCliente = "Socio", formatoFuncion = "3D", diaEspectador = false, precio = 16.50},
                    new Tarifa { CodigoTarifa = "trfVip3DSocioEspectador", formatoSala = "Vip", formatoCliente = "Socio", formatoFuncion = "3D", diaEspectador = true, precio = 12.50},
                };
                foreach(Tarifa trf in tarifas)
                {
                    context.Tarifas.AddOrUpdate(trf);
                    context.SaveChanges();
                }

                base.Seed(context);
			}
		}

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
		{
			base.OnModelCreating(modelBuilder);
		}
	}
}
