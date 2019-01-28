using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
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
using WpfBiblioteca.DAL;
using WpfBiblioteca.Model;

namespace WpfBiblioteca
{
    /// <summary>
    /// Lógica de interacción para MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        UnitOfWork uow = new UnitOfWork();
        Libro l = new Libro();
        PropertyValidateModel validador = new PropertyValidateModel();

        public MainWindow()
        {
            InitializeComponent();
            GridLibro.DataContext = l;
            dgEjemplares.ItemsSource = l.Ejemplares.ToList();
            getPropiedades();
        }

        private void Btn_registrar_Click(object sender, RoutedEventArgs e)
        {
            if (validador.errores(l) == "")
                uow.LibrosRepositorio.Añadir(l);
            else MessageBox.Show(validador.errores(l));
        }

        private void Btn_GuardarEjemplar_Click(object sender, RoutedEventArgs e)
        {
            dgEjemplares.ItemsSource = "";
            l = uow.LibrosRepositorio.Get(libro => libro.Isbn.Equals(txt_isbn.Text)).FirstOrDefault();

            Ejemplar ejem = new Ejemplar
            {
                LibroId = l.LibroId,
                NumeroEjemplar = Convert.ToInt16(txt_numEjemplar.Text),
                FechaPublicacion = dp_FechaPublicacion.SelectedDate,
                Estado = "D"
            };
            l.Ejemplares.Add(ejem);

            if (validador.errores(l) == "")
            {
                if (uow.LibrosRepositorio.Get(libro => libro.Isbn.Equals(txt_isbn.Text)).FirstOrDefault() != null)
                    uow.LibrosRepositorio.Update(l);
                dgEjemplares.ItemsSource = l.Ejemplares;
            }
            else MessageBox.Show(validador.errores(l));
            
        }

        private void BtNuevo_Click(object sender, RoutedEventArgs e)
        {
            l = new Libro();
            GridLibro.DataContext = l;
        }

        public void getPropiedades()
        {
            PropertyInfo[] lst = typeof(Libro).GetProperties();

            foreach (PropertyInfo oProperty in lst)
            {
                System.Type type = oProperty.GetType();
                cbCampos.Items.Add(oProperty.Name);
            }
        }

        private void Btn_buscarLibro_Click(object sender, RoutedEventArgs e)
        {
            l = new Libro();
            l = uow.LibrosRepositorio.GetFiltrado(tbBusqueda.Text);
            GridLibro.DataContext = l;
            dgEjemplares.ItemsSource = l.Ejemplares;
        }

        private void Btn_modificar_Click(object sender, RoutedEventArgs e)
        {
            l.Isbn = txt_isbn.Text;
            l.Titulo = txt_titulo.Text;
            l.Editorial = txt_editorial.Text;
            l.Descripcion = txt_descripcion.Text;

            uow.LibrosRepositorio.Update(l);
        }

        private void Btn_eliminar_Click(object sender, RoutedEventArgs e)
        {
            uow.LibrosRepositorio.Delete(l);
        }
    }
}
