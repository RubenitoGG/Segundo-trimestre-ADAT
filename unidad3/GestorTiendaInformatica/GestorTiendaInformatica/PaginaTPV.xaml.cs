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
using System.Windows.Navigation;
using System.Windows.Shapes;
using GestorTiendaInformatica.DAL;
using GestorTiendaInformatica.Model;

namespace GestorTiendaInformatica
{
    /// <summary>
    /// Lógica de interacción para PaginaTPV.xaml
    /// </summary>
    public partial class PaginaTPV : Page
    {
        UnitOfWork uow = new UnitOfWork();
        List<Categoria> categorias;
        public PaginaTPV()
        {
            InitializeComponent();
            // AÑADIR UN BOTÓN POR CADA CATEGORÍA:
            categorias = uow.CategoriaRepositorio.GetAll();
            foreach (Categoria categoria in categorias)
            {
                Color c = new Color();
                c.R = 22;
                c.B = 107;
                c.G = 255;
                c.A = 100;

                Button button = new Button
                {
                    Content = categoria.Nombre,
                    FontSize = 20,
                    Height = 50,
                    Width = lb_categorias.Width - 50,
                    Margin = new Thickness(0, 5, 0, 5),
                    Background = new SolidColorBrush(c),
                };

                button.Click += (sender, args) =>
                {
                    PonerProductos(button.Content.ToString());
                };

                lb_categorias.Items.Add(button);
            }

        }

        private void PonerProductos(string categoria)
        {
            List<Producto> todo = uow.ProductoRepositorio.GetAll();
            List<string> elegidos = new List<string>();

            foreach (Producto producto in todo)
            {
                if (producto.Categoria.Nombre == categoria)
                    elegidos.Add(producto.Nombre + " - " + producto.Descripcion);
            }

            lb_productos.ItemsSource = elegidos;
            txt_nombre.Text = "--";
            txt_precio.Text = "--";
            txt_stock.Text = "--";
        }

        private void Lb_productos_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            List<Producto> productos = uow.ProductoRepositorio.GetAll();
            foreach (Producto p in productos)
            {
                try
                {

                    if (p.Nombre == lb_productos.SelectedItem.ToString().Split('-')[0].Trim())
                    {
                        txt_nombre.Text = p.Nombre;
                        txt_precio.Text = p.Precio.ToString() + " €";
                        txt_stock.Text = p.Stock.ToString();
                    }
                }
                catch
                {
                    lb_productos.SelectedItem = null;
                }
            }
        }

        List<Producto> p = new List<Producto>();
        double cantidad = 0;

        private void Btn_añadir_Click(object sender, RoutedEventArgs e)
        {
            List<Producto> productos = uow.ProductoRepositorio.GetAll();
            foreach (Producto p in productos)
            {
                if (p.Nombre == lb_productos.SelectedItem.ToString().Split('-')[0].Trim())
                {
                    this.p.Add(p);
                    cantidad += p.Precio;
                }
            }

            txt_total.Text = cantidad + "€";
            dg_ticket.ItemsSource = p;
            dg_ticket.Items.Refresh();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            p.Remove((Producto)dg_ticket.SelectedItem);

            List<Producto> productos = uow.ProductoRepositorio.GetAll();
            foreach (Producto p in productos)
            {
                if (p.Nombre == lb_productos.SelectedItem.ToString().Split('-')[0].Trim())
                {
                    cantidad -= p.Precio;
                }
            }

            txt_total.Text = cantidad + "€";
            dg_ticket.ItemsSource = p;
            dg_ticket.Items.Refresh();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            p = new List<Producto>();
            dg_ticket.ItemsSource = "";
            cantidad = 0;
            txt_total.Text = cantidad.ToString();
        }
    }
}
