using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace ProyectoCine
{
	/// <summary>
	/// Lógica de interacción para Listas.xaml
	/// </summary>
	public partial class Listas : Window
	{
		public Listas()
		{
			InitializeComponent();

			switch (MainWindow.tipoListaLeer)
			{
				case "Empleados":
					grListaEmpleados.IsEnabled = true;
					grListaEmpleados.Visibility = Visibility.Visible;
					dtgrEmpleados.ItemsSource = MainWindow.unit.RepositorioEmpleados.GetAll();
					break;
				case "Clientes":
					grListaCliente.IsEnabled = true;
					grListaCliente.Visibility = Visibility.Visible;
					dtgrClientes.ItemsSource = MainWindow.unit.RepositorioClientes.GetAll();
					break;
				case "Funciones":
					grListaFunciones.IsEnabled = true;
					grListaFunciones.Visibility = Visibility.Visible;
					dtgrFunciones.ItemsSource = MainWindow.unit.RepositorioFunciones.GetAll();
					break;
				case "Horarios":
					grListaHorarios.IsEnabled = true;
					grListaHorarios.Visibility = Visibility.Visible;
					dtgrHorarios.ItemsSource = MainWindow.unit.RepositorioHorarios.GetAll();
					break;
				case "Salas":
					grListaSalas.IsEnabled = true;
					grListaSalas.Visibility = Visibility.Visible;
					dtgrSalas.ItemsSource = MainWindow.unit.RepositorioSalas.GetAll();
					break;
				case "Peliculas":
					grListaPeliculas.IsEnabled = true;
					grListaPeliculas.Visibility = Visibility.Visible;
					dtgrPeliculas.ItemsSource = MainWindow.unit.RepositorioPeliculas.GetAll();
					break;
				case "Productoras":
					grListaProductoras.IsEnabled = true;
					grListaProductoras.Visibility = Visibility.Visible;
					dtgrProductoras.ItemsSource = MainWindow.unit.RepositorioProductoras.GetAll();
					break;
				case "Categorias":
					grListaCategoriasPeliculas.IsEnabled = true;
					grListaCategoriasPeliculas.Visibility = Visibility.Visible;
					dtgrCategorias.ItemsSource = MainWindow.unit.RepositorioCategorias.GetAll();
					break;
                case "Proveedores":
                    grListaProveedores.IsEnabled = true;
                    grListaProveedores.Visibility = Visibility.Visible;
                    dtgrProveedores.ItemsSource = MainWindow.unit.RepositorioProveedores.GetAll();
                    break;
                case "CategoriasProductos":
                    grListaCategoriasProductos.IsEnabled = true;
                    grListaCategoriasProductos.Visibility = Visibility.Visible;
                    dtgrCategoriasProductos.ItemsSource = MainWindow.unit.RepositorioCategoriasProductos.GetAll();
                    break;
                case "Productos":
                    grListaProductos.IsEnabled = true;
                    grListaProductos.Visibility = Visibility.Visible;
                    dtgrProductos.ItemsSource = MainWindow.unit.RepositorioProductos.GetAll();
                    break;
			}
		}
	}
}
