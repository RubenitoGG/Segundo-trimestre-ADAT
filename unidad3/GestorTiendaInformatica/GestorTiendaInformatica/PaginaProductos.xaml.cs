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
using GestorTiendaInformatica.Model;
using GestorTiendaInformatica.DAL;

namespace GestorTiendaInformatica
{
    /// <summary>
    /// Lógica de interacción para PaginaProductos.xaml
    /// </summary>
    public partial class PaginaProductos : Page
    {
        Frame f;
        UnitOfWork uow = new UnitOfWork();
        Producto p;
        bool nuevo = true;
        public PaginaProductos(Frame f)
        {
            InitializeComponent();
            this.f = f;
            dg_productos.ItemsSource = uow.ProductoRepositorio.GetAll();
        }

        private void Btn_guardar_Click(object sender, RoutedEventArgs e)
        {
            if (nuevo) // AÑADIR PRODUCTO:
            {
                p = new Producto();
                p.Nombre = txt_nombre.Text;
                p.Descripcion = txt_descripcion.Text;
                p.Precio = Convert.ToDouble(txt_precio.Text);
                p.Stock = Convert.ToInt32(txt_stock.Text);
                p.Imagen = "imagen";

                uow.ProductoRepositorio.Añadir(p);
            }
            else // ACTUALIZAR PRODUCTO:
            {
                p.ProductoId = Convert.ToInt32(txt_id.Text);
                p.Nombre = txt_nombre.Text;
                p.Descripcion = txt_descripcion.Text;
                p.Precio = Convert.ToDouble(txt_precio.Text);
                p.Stock = Convert.ToInt32(txt_stock.Text);
                p.Imagen = "imagen";

                uow.ProductoRepositorio.Update(p);
            }

            BorrarCampos();
            dg_productos.ItemsSource = "";
            dg_productos.ItemsSource = uow.ProductoRepositorio.GetAll();
            dg_productos.Items.Refresh();

            nuevo = true;
            btn_guardar.Content = "Añadir";
        }

        private void BorrarCampos()
        {
            txt_id.Text = "";
            txt_nombre.Text = "";
            txt_descripcion.Text = "";
            txt_precio.Text = "";
            txt_stock.Text = "";
            p = null;
        }

        private void Dg_productos_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                p = (Producto)dg_productos.SelectedItem;
                txt_id.Text = p.ProductoId.ToString();
                txt_nombre.Text = p.Nombre;
                txt_descripcion.Text = p.Descripcion;
                txt_precio.Text = p.Precio.ToString();
                txt_stock.Text = p.Stock.ToString();

                nuevo = false;
                btn_guardar.Content = "Actualizar";
                btn_eliminar.IsEnabled = true;
            }
            catch { }
        }

        private void Btn_limpiar_Click(object sender, RoutedEventArgs e)
        {
            BorrarCampos();
            btn_eliminar.IsEnabled = false;
            nuevo = true;
            btn_guardar.Content = "Añadir";
        }

        private void Btn_eliminar_Click(object sender, RoutedEventArgs e)
        {
            p = (Producto)dg_productos.SelectedItem;
            uow.ProductoRepositorio.Delete(p);

            BorrarCampos();
            dg_productos.ItemsSource = "";
            dg_productos.ItemsSource = uow.ProductoRepositorio.GetAll();
            dg_productos.Items.Refresh();

        }
    }
}
