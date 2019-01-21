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
using System.Windows.Shapes;
using WpfBiblioteca.DAL;
using WpfBiblioteca.Model;

namespace WpfBiblioteca
{
    /// <summary>
    /// Lógica de interacción para SociosWindow.xaml
    /// </summary>
    public partial class SociosWindow : Window
    {
        UnitOfWork uow = new UnitOfWork();
        Socio s = new Socio();

        public SociosWindow()
        {
            InitializeComponent();
            gridSocio.DataContext = s;
        }
        
        private void Btn_agregar_Click(object sender, RoutedEventArgs e)
        {
            uow.SociosRepository.Añadir(s);
            s = new Socio();
        }

        private void Btn_eliminar_Click(object sender, RoutedEventArgs e)
        {
            uow.SociosRepository.Delete(s);
            s = new Socio();
        }

        private void Btn_actualizar_Click(object sender, RoutedEventArgs e)
        {
            s.Dni = txt_dni.Text;
            s.Nombre = txt_nombre.Text;
            s.Apellidos = txt_apellido.Text;
            s.Direccion = txt_direccion.Text;
            s.Correo = txt_correo.Text;
            s.Telefono = txt_telefono.Text;
            s.Password = txt_password.Text;
        }

        private void Btn_buscarLibro_Click(object sender, RoutedEventArgs e)
        {
            dgSocios.ItemsSource = uow.SociosRepository.GetFiltrado(txt_buscar.Text);
        }

        private void DgSocios_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            s = (Socio)dgSocios.SelectedItem;
            gridSocio.DataContext = s;
        }
    }
}
