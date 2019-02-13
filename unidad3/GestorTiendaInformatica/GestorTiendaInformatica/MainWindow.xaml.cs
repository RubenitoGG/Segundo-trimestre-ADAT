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
using GestorTiendaInformatica.Model;

namespace GestorTiendaInformatica
{
    /// <summary>
    /// Lógica de interacción para MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        PaginaInicio pI;
        Usuario us = new Usuario();
        Color normal = new Color();
        Color actual = new Color();

        public MainWindow()
        {
            InitializeComponent();

            normal.R = 16;
            normal.B = 152;
            normal.G = 174;

            actual.R = 27;
            actual.B = 100;
            actual.G = 141;

            this.pI = new PaginaInicio(frameVentana, this);
            frameVentana.Content = pI;
        }

        private void BotonCerrarSesion_Click(object sender, RoutedEventArgs e)
        {
            frameVentana.Content = pI;
            this.lbl_usuario.Content = "Sin identificar";
            botonCerrarSesion.IsEnabled = false;
        }

        public void RecogerUsuario(Usuario usuario)
        {
            this.us = usuario;
            this.lbl_usuario.Content = "Usuario identificado como " + usuario.TipoCuenta;
            botonCerrarSesion.IsEnabled = true;
        }

        private void QuitarColor()
        {
            menuInicio.Background. = new Color();
        }
    }
}
