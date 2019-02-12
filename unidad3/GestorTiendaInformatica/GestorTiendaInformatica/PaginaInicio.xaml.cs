using System;
using System.Collections.Generic;
using System.Diagnostics;
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
    /// Lógica de interacción para PaginaInicio.xaml
    /// </summary>
    public partial class PaginaInicio : Page
    {
        Frame f;
        UnitOfWork uow = new UnitOfWork();

        public PaginaInicio(Frame f)
        {
            InitializeComponent();
            this.f = f;
            textUsuario.Text = "";
            textContraseña.Password = "";
        }

        private void BotonRegistrarse_Click(object sender, RoutedEventArgs e)
        {
            PaginaRegistro pG = new PaginaRegistro(f);
            f.Content = pG;
        }

        private void BotonEntrar_Click(object sender, RoutedEventArgs e)
        {
            List<Usuario> usuarios = uow.UsuarioRepositorio.GetAll();
            foreach (Usuario usuario in usuarios)
            {
                if (textUsuario.Text == usuario.user && textContraseña.Password == usuario.password)
                    this.f.Content = new PaginaClientes(f);
                else
                    MessageBox.Show("Usuario o contraseña incorrectos", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
