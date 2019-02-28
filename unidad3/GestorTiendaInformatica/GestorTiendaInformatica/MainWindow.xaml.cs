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
        bool moverte = false;

        public MainWindow()
        {
            InitializeComponent();

            normal.R = 16;
            normal.G = 152;
            normal.B = 174;
            normal.A = 100;

            actual.R = 32;
            actual.G = 79;
            actual.B = 112;
            actual.A = 100;

            this.pI = new PaginaInicio(frameVentana, this);
            frameVentana.Content = pI;
            menuInicio.Background = new SolidColorBrush(actual);
        }

        private void BotonCerrarSesion_Click(object sender, RoutedEventArgs e)
        {
            PaginaInicio pI = new PaginaInicio(frameVentana, this);
            frameVentana.Content = pI;
            this.lbl_usuario.Content = "Sin identificar";
            botonCerrarSesion.IsEnabled = false;
            moverte = false;
            QuitarColor();
            menuInicio.Background = new SolidColorBrush(actual);
        }

        public void RecogerUsuario(Usuario usuario)
        {
            this.us = usuario;
            this.lbl_usuario.Content = "Usuario identificado como " + usuario.TipoCuenta;
            botonCerrarSesion.IsEnabled = true;
            moverte = true;
            QuitarColor();
            menuCliente.Background = new SolidColorBrush(actual);
        }

        private void QuitarColor()
        {
            menuInicio.Background = new SolidColorBrush(normal);
            menuUsuario.Background = new SolidColorBrush(normal);
            menuCliente.Background = new SolidColorBrush(normal);
            menuProveedores.Background = new SolidColorBrush(normal);
            menuCategoria.Background = new SolidColorBrush(normal);
            menuProductos.Background = new SolidColorBrush(normal);
            menuTPV.Background = new SolidColorBrush(normal);
            menuInformes.Background = new SolidColorBrush(normal);
        }

        private void MenuUsuario_Click(object sender, RoutedEventArgs e)
        {
            if (moverte)
            {
                PaginaUsuarios pU = new PaginaUsuarios(frameVentana);
                frameVentana.Content = pU;
                QuitarColor();
                menuUsuario.Background = new SolidColorBrush(actual);
            }
            else
                MessageBox.Show("Necesitas iniciar sesión antes de nada", "", MessageBoxButton.OK, MessageBoxImage.Error);
        }

        private void MenuCliente_Click(object sender, RoutedEventArgs e)
        {
            if (moverte)
            {
                PaginaClientes pC = new PaginaClientes(frameVentana);
                frameVentana.Content = pC;
                QuitarColor();
                menuCliente.Background = new SolidColorBrush(actual);
            }
            else
                MessageBox.Show("Necesitas iniciar sesión antes de nada", "", MessageBoxButton.OK, MessageBoxImage.Error);
        }

        private void MenuProveedores_Click(object sender, RoutedEventArgs e)
        {
            if (moverte)
            {
                PaginaProveedores pP = new PaginaProveedores(frameVentana);
                frameVentana.Content = pP;
                QuitarColor();
                menuProveedores.Background = new SolidColorBrush(actual);
            }
            else
                MessageBox.Show("Necesitas iniciar sesión antes de nada", "", MessageBoxButton.OK, MessageBoxImage.Error);
        }

        private void MenuCategoria_Click(object sender, RoutedEventArgs e)
        {
            if (moverte)
            {
                PaginaCategoria pC = new PaginaCategoria(frameVentana);
                frameVentana.Content = pC;
                QuitarColor();
                menuCategoria.Background = new SolidColorBrush(actual);
            }
            else
                MessageBox.Show("Necesitas iniciar sesión antes de nada", "", MessageBoxButton.OK, MessageBoxImage.Error);
        }

        private void MenuProductos_Click(object sender, RoutedEventArgs e)
        {
            if (moverte)
            {
                PaginaProductos pP = new PaginaProductos(frameVentana);
                frameVentana.Content = pP;
                QuitarColor();
                menuProductos.Background = new SolidColorBrush(actual);
            }
            else
                MessageBox.Show("Necesitas iniciar sesión antes de nada", "", MessageBoxButton.OK, MessageBoxImage.Error);
        }

        private void MenuInformes_Click(object sender, RoutedEventArgs e)
        {
            if (moverte)
            {
                PaginaInformes pI = new PaginaInformes(frameVentana);
                frameVentana.Content = pI;
                QuitarColor();
                menuInformes.Background = new SolidColorBrush(actual);
            }
            else
                MessageBox.Show("Necesitas iniciar sesión antes de nada", "", MessageBoxButton.OK, MessageBoxImage.Error);
        }

        private void MenuTPV_Click(object sender, RoutedEventArgs e)
        {
            if (moverte)
            {
                PaginaTPV pT = new PaginaTPV();
                frameVentana.Content = pT;
                QuitarColor();
                menuTPV.Background = new SolidColorBrush(actual);
            }
        }
    }
}
