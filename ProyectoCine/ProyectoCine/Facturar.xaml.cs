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
using ProyectoCine.MODEL;
using ProyectoCine.DAL;
using System.IO;
using System.Diagnostics;

namespace ProyectoCine
{
	/// <summary>
	/// Lógica de interacción para Facturar.xaml
	/// </summary>
	public partial class Facturar : Window
	{
        // Variable que nos permite saber si la venta se ha realizado o no en el MainWindow.
        public static bool ventaRealizada = false;
        Process abrirTXT;
        public Facturar()
		{
			InitializeComponent();
            // Dependiendo de la variable static declarada en MainWindow activará el grid del tiket de las entradas o el de la venta de productos.
            switch (MainWindow.tipoFactura)
            {
                case "Entradas":
                    // Activa el grid de FacturarEntrada y lo hace visible
                    grFacturaEntrada.IsEnabled = true;
                    grFacturaEntrada.Visibility = Visibility.Visible;
                    // Rellena el dataGrid con la lista static en MainWindow de entradas que va a ser vendida. 
                    dtgrFacturaEntradas.ItemsSource = MainWindow.entradasVender;
                    
                    double totalEntradas = 0;
                    // Calcula el precio total de las entradas recorriendo cada objeto de la lista tomando su precio.
                    foreach (Entrada item in MainWindow.entradasVender)
                    {
                        totalEntradas += MainWindow.tarifaVenta.precio;
                    }
                    // Establece el precio de cada entrada y el total en los label correspondientes.
                    lblPrecioEntradaFactura.Content = MainWindow.tarifaVenta.precio + "€";
                    lblTotalFactura.Content = Math.Round(totalEntradas, 2) + "€";
                    break;
                case "Productos":
                    // Activa el grid de FacturarProducto y lo hace visible.
                    grFacturarProducto.IsEnabled = true;
                    grFacturarProducto.Visibility = Visibility.Visible;
                    // Rellena el dataGrid con la lista LineaVentas del objeto venta static en MainWindow. 
                    dtgrFacturaProductos.ItemsSource = MainWindow.ventaGestionar.LineasVentas;

                    double totalVenta = 0;
                    // Calcula el precio total de las entradas recorriendo cada objeto de la lista tomando su precio.
                    foreach (LineaVenta item in MainWindow.ventaGestionar.LineasVentas)
                    {
                        totalVenta += item.Cantidad * item.Producto.precio ;
                    }
                    // Establece el precio total de la compra en el label correspondiente.
                    lblTotalVenta.Content = Math.Round(totalVenta, 2) + "€";
                    break;
            }
               

		}

		// Método que se ejecuta al darle al botón aceptar. 
		private void btnAceptarFactura_Click(object sender, RoutedEventArgs e)
		{
			try
			{
				// Recorre cada objeto entrada de la lista static de Entradas de MainWindow.
				foreach (Entrada item in MainWindow.entradasVender)
				{
					// Lo añade a la base de datos.
					MainWindow.unit.RepositorioEntradas.Add(item);
				}
				// Confirma que la venta de las entradas SÍ se ha realizado.
				MainWindow.facturado = true;
				// Muestra un mensaje.
				if (MessageBox.Show("Desea imprimir ticket?.", "Pregunta", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.No) {  }
				else
				{
					imprimirTicketEntrada();
				}

				MainWindow.entradasVender.Clear();
				// Activa MainWindow.
				MainWindow.mainWindow.IsEnabled = true;
				this.Close();
			}
			catch (Exception ex)
			{
				System.Windows.MessageBox.Show("Ha ocurrido un error. Pongase en contacto con el administrador.");
				Console.WriteLine("Error: " + ex.ToString());
			}
		}

        // Método que se ejecuta al darle al botón de cancelar.
		private void btnCancelarFactura_Click(object sender, RoutedEventArgs e)
		{
            // Confirma que la venta de entradas NO se ha realidado.
			MainWindow.facturado = false;
            // Limpia la lista de entradas a vender en MainWindow.
            MainWindow.entradasVender.Clear();
            // Activa MainWindow.
            MainWindow.mainWindow.IsEnabled = true;
			this.Close();
		}
        
        // Método que se ejecuta cuando se cierra la ventana.
		private void Window_Closed(object sender, EventArgs e)
		{
            // Oculta los grids de FacturarEntrada y FacturarProducto y activa MainWindow.
            grFacturaEntrada.IsEnabled = false;
            grFacturaEntrada.Visibility = Visibility.Hidden;
            grFacturarProducto.IsEnabled = false;
            grFacturarProducto.Visibility = Visibility.Hidden;
            // Pone las ventas a falso.
            MainWindow.facturado = false;
            ventaRealizada = false;
            MainWindow.mainWindow.IsEnabled = true;
        }

        // Método que se ejecuta al darle al botón de aceptar.
        private void btnAceptarVenta_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                // Añade la venta static de MainWindow a la base de datos.
                MainWindow.unit.RepositorioVentas.Add(MainWindow.ventaGestionar);
                // Confirma que la venta SÍ se ha realizado.
                MainWindow.vendido = true;
                ventaRealizada = true;
                // Muestra un mensaje.
                if (MessageBox.Show("Desea imprimir ticket?.", "Pregunta", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.No) { }
                else
                {
                    imprimirTicketProducto();
                }
                // Activa MainWindow.
                MainWindow.mainWindow.IsEnabled = true;
                this.Close();
            }
            catch (Exception ex)
            {
                System.Windows.MessageBox.Show("Ha ocurrido un error. Pongase en contacto con el administrador.");
                Console.WriteLine("Error: " + ex.ToString());
            }
        }

        // Método que se ejecuta al darle al botón de cancelar.
        private void btnCancelarVenta_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                // Recorre cada elemento en la lista del objeto Venta static de MainWindow.
                foreach (LineaVenta item in MainWindow.ventaGestionar.LineasVentas)
                {
                    // Obtiene el producto contenido en el item.
                    Producto producto = item.Producto;
                    // Le suma la cantidad obtenida de ese objeto en la compra reestableciendo el stock.
                    producto.stock += item.Cantidad;
                    // Modifica en la base de datos.
                    MainWindow.unit.RepositorioProductos.Update(producto);
                }
                // Confirma que la venta NO se ha realizado.
                MainWindow.vendido = false;
                // Activa MainWindow.
                MainWindow.mainWindow.IsEnabled = true;
                // Muestra un mensaje.
                MessageBox.Show("Venta cancelada");
                this.Close();
            }
            catch (Exception ex)
            {
                System.Windows.MessageBox.Show("Ha ocurrido un error. Pongase en contacto con el administrador.");
                Console.WriteLine("Error: " + ex.ToString());
            }
        }

        // Creamos un fichero txt para el recibo de entradas.
        private void imprimirTicketEntrada()
        {
            try
            {
                string fecha = Convert.ToString(DateTime.Now);
                fecha = fecha.Replace('/', '-');
                fecha = fecha.Replace(':', '-');

                Entrada aux = MainWindow.entradasVender[0];

                string directorio = "ventasEntradas/ventaEntrada " + aux.CodigoFuncion + " " + fecha + ".txt";
                if (!File.Exists(directorio))
                {
                    StreamWriter escribirTicket = new StreamWriter(directorio, true, Encoding.Default);

                    escribirTicket.WriteLine("CINE AUREA");

                    foreach (Entrada item in MainWindow.entradasVender)
                    {
                        escribirTicket.WriteLine("-----------------------------------------------------------------------------------");
                        escribirTicket.WriteLine("Código de función: " + item.CodigoFuncion);
                        escribirTicket.WriteLine("Película: " + item.Funcion.Pelicula.nombre);
                        escribirTicket.WriteLine("Butaca: " + item.butaca);

                        escribirTicket.WriteLine("Código de cliente: " + item.ClienteId);
                        escribirTicket.WriteLine("Cliente: " + item.Cliente.nombre);
                        escribirTicket.WriteLine("Empleado: " + item.nombreEmpleado);

                        escribirTicket.WriteLine("Código de tarifa: " + item.Tarifa.CodigoTarifa);
                        escribirTicket.WriteLine("Precio de entrada: " + item.Tarifa.precio + " €");

                        escribirTicket.WriteLine("Fecha y hora " + item.fecha + " " + item.hora);
                        escribirTicket.WriteLine("-----------------------------------------------------------------------------------");
                    }

                    escribirTicket.Close();
                }

                string directorioCompleto = System.IO.Path.Combine(Environment.CurrentDirectory, directorio);

                // Lanzamos el txt con un proceso.
                abrirTXT = new Process();
                abrirTXT.StartInfo.FileName = directorioCompleto;
                abrirTXT.Start();
			}
            catch (Exception ex)
            {
                System.Windows.MessageBox.Show("Ha ocurrido un error. Pongase en contacto con el administrador.");
                Console.WriteLine("Error: " + ex.ToString());
            }
        }

        // Creamos un fichero txt para el recibo de productos.
        private void imprimirTicketProducto()
        {
            try
            {
                string fecha = Convert.ToString(DateTime.Now);
                fecha = fecha.Replace('/', '-');
                fecha = fecha.Replace(':', '-');

                string directorio = "ventasProductos/ventaProducto " + MainWindow.ventaGestionar.CodigoEmpleado + " " + fecha + ".txt";
                if (!File.Exists(directorio))
                {
                    StreamWriter escribirTicket = new StreamWriter(directorio, true, Encoding.Default);

                    escribirTicket.WriteLine("TIENDA CINE AUREA");

                    escribirTicket.WriteLine("Código de cliente: " + MainWindow.ventaGestionar.Cliente.ClienteId);
                    escribirTicket.WriteLine("Cliente: " + MainWindow.ventaGestionar.Cliente.nombre);

                    escribirTicket.WriteLine("Cantidad | Descrición | Precio");
                    escribirTicket.WriteLine("-----------------------------------------------------------------------------------");

                    double Total = 0;
                    foreach (LineaVenta item in MainWindow.ventaGestionar.LineasVentas)
                    {
                        escribirTicket.WriteLine(item.Cantidad + "\t" + item.Producto.nombre + "\t" + item.Producto.precio);
                        Total += item.Producto.precio * item.Cantidad;
                    }

                    escribirTicket.WriteLine("Total: " + Total);

                    escribirTicket.WriteLine("-----------------------------------------------------------------------------------");

                    escribirTicket.Close();
                }

                string directorioCompleto = System.IO.Path.Combine(Environment.CurrentDirectory, directorio);

                // Lanzamos el txt con un proceso.
                abrirTXT = new Process();
                abrirTXT.StartInfo.FileName = directorioCompleto;
                abrirTXT.Start();
            }
            catch (Exception ex)
            {
                System.Windows.MessageBox.Show("Ha ocurrido un error. Pongase en contacto con el administrador.");
                Console.WriteLine("Error: " + ex.ToString());
            }
        }
    }
}
