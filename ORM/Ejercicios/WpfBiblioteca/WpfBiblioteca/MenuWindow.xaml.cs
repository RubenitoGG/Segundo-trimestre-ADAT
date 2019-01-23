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

namespace WpfBiblioteca
{
    /// <summary>
    /// Lógica de interacción para MenuWindow.xaml
    /// </summary>
    public partial class MenuWindow : Window
    {
        public MenuWindow()
        {
            InitializeComponent();
        }

        private void Btn_mantLibros_Click(object sender, RoutedEventArgs e)
        {
            MainWindow nuevaVentana = new MainWindow();
            nuevaVentana.Show();
        }

        private void Btn_mantSocios_Click(object sender, RoutedEventArgs e)
        {
            SociosWindow nuevaVentana = new SociosWindow();
            nuevaVentana.Show();
        }

        private void Btn_registrarPrest_Click(object sender, RoutedEventArgs e)
        {
            RegistrarPrestamoWindow nuevaVentana = new RegistrarPrestamoWindow();
            nuevaVentana.Show();
        }

        private void Btn_devolverPrest_Click(object sender, RoutedEventArgs e)
        {
            DevolverPrestamoWindow nuevaVentana = new DevolverPrestamoWindow();
            nuevaVentana.Show();
        }
    }
}
