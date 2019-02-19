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
    /// Lógica de interacción para PaginaCategoria.xaml
    /// </summary>
    public partial class PaginaCategoria : Page
    {
        Frame f;
        UnitOfWork uow = new UnitOfWork();
        Categoria c;
        bool nuevo = true;

        public PaginaCategoria(Frame f)
        {
            InitializeComponent();
            this.f = f;
            dg_categorias.ItemsSource = uow.CategoriaRepositorio.GetAll();
        }

        private void Btn_guardar_Click(object sender, RoutedEventArgs e)
        {
            if (nuevo)
            {
                c = new Categoria()
                {
                    Nombre = txt_nombre.Text,
                    Descripcion = txt_descripcion.Text,
                    Imagen = "foto"
                };
                uow.CategoriaRepositorio.Añadir(c);
            }
            else
            {
                c.Nombre = txt_nombre.Text;
                c.Descripcion = txt_descripcion.Text;

                uow.CategoriaRepositorio.Update(c);
            }

            BorrarCampos();
            btn_guardar.Content = "Añadir";
            nuevo = true;

            dg_categorias.ItemsSource = "";
            dg_categorias.ItemsSource = uow.CategoriaRepositorio.GetAll();
        }

        private void Dg_categorias_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            c = (Categoria)dg_categorias.SelectedItem;
            if (c != null)
            {
                txt_id.Text = c.CategoriaId.ToString();
                txt_nombre.Text = c.Nombre;
                txt_descripcion.Text = c.Descripcion;

                nuevo = false;
                btn_guardar.Content = "Actualizar";
            }
        }

        private void Btn_limpiar_Click(object sender, RoutedEventArgs e)
        {
            BorrarCampos();
            nuevo = true;
            btn_guardar.Content = "Añadir";
        }

        private void BorrarCampos()
        {
            txt_id.Text = "";
            txt_nombre.Text = "";
            txt_descripcion.Text = "";
        }

        private void Btn_eliminar_Click(object sender, RoutedEventArgs e)
        {
            c = (Categoria)dg_categorias.SelectedItem;
            uow.CategoriaRepositorio.Delete(c);
            c = null;

            BorrarCampos();
            dg_categorias.ItemsSource = "";
            dg_categorias.ItemsSource = uow.CategoriaRepositorio.GetAll();

            nuevo = true;
            btn_guardar.Content = "Añadir";
        }
    }
}
