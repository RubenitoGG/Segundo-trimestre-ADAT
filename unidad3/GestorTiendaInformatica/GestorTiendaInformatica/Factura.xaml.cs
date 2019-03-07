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
using GestorTiendaInformatica.DAL;
using GestorTiendaInformatica.Model;

namespace GestorTiendaInformatica
{
    /// <summary>
    /// Lógica de interacción para Factura.xaml
    /// </summary>
    public partial class Factura : Window
    {
        double precio;

        public Factura(List<Producto> productos, string nombre)
        {
            InitializeComponent();
            precio = 0;

            dg_productos.ItemsSource = productos;

            // CALCULAR PRECIO:
            foreach (Producto producto in productos)
            {
                precio += producto.Precio;
            }

            lbl_precio.Content = precio + " €";
            lbl_nombre.Content = nombre;
        }
    }
}
