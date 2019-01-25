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
    /// Lógica de interacción para DevolverPrestamoWindow.xaml
    /// </summary>
    public partial class DevolverPrestamoWindow : Window
    {
        UnitOfWork uow = new UnitOfWork();
        Socio s = new Socio();
        Prestamo p = new Prestamo();
        Libro l = new Libro();

        public DevolverPrestamoWindow()
        {
            InitializeComponent();
        }

        private void Btn_buscar_socios_Click(object sender, RoutedEventArgs e)
        {
            if (txt_socios.Text.Trim() == "")
                return;

            dgSocios.ItemsSource = uow.SociosRepository.GetFiltrado(txt_socios.Text.Trim());
        }

        private void DgSocios_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            s = (Socio)dgSocios.SelectedItem;
            dgPrestamos.ItemsSource = s.Prestamos;
        }

        private void DgEjemplares_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            p = (Prestamo)dgPrestamos.SelectedItem;
        }

        private void Btn_devolver_Click(object sender, RoutedEventArgs e)
        {
            p.FechaDevolucion = DateTime.Today;
            p.Ejemplar.Estado = "D";
            uow.PrestamosRepositorio.Update(p);
        }
    }
}
