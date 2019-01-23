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
using WpfBiblioteca.DAL;
using WpfBiblioteca.Model;

namespace WpfBiblioteca
{
    /// <summary>
    /// Lógica de interacción para RegistrarPrestamoWindow.xaml
    /// </summary>
    public partial class RegistrarPrestamoWindow : Window
    {
        UnitOfWork uow = new UnitOfWork();
        Libro l = new Libro();
        Socio s =  new Socio();
        Prestamo p = new Prestamo();
        Ejemplar e = new Ejemplar();

        public RegistrarPrestamoWindow()
        {
            InitializeComponent();
        }

        private void Btn_buscar_socios_Click(object sender, RoutedEventArgs e)
        {
            if (txt_socios.Text.Trim() == "")
                return;

            dgSocios.ItemsSource = uow.SociosRepository.GetFiltrado(txt_socios.Text.Trim());
        }

        private void Btn_buscar_ejemplares_Click(object sender, RoutedEventArgs e)
        {
            l = new Libro();
            l = uow.LibrosRepositorio.GetFiltrado(txt_libro.Text.Trim());

            if (l == null)
                return;

            dgEjemplares.ItemsSource = l.Ejemplares;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (this.e.Estado == "D")
            {
                p = new Prestamo()
                {
                    LibroId = this.e.LibroId,
                    SocioId = this.s.SocioId,
                    FechaPrestamo = DateTime.Today,
                    NumeroEjemp = Convert.ToInt16(this.e.NumeroEjemplar)
                };

                uow.PrestamosRepositorio.Añadir(p);
                this.e.Estado = "P";

                uow.LibrosRepositorio.Update(l);
            }
            else
            {
                // LIBRO NO DISPONIBLE
            }
        }

        private void DgSocios_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            s = (Socio)dgSocios.SelectedItem;
        }

        private void DgEjemplares_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            this.e = (Ejemplar)dgEjemplares.SelectedItem;
        }
    }
}
