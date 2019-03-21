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

namespace ProyectoCine
{
    /// <summary>
    /// Lógica de interacción para Login.xaml
    /// </summary>
    public partial class Login : Window
    {
        public Login()
        {
            InitializeComponent();
        }

        private void lbEntrar_MouseDown(object sender, RoutedEventArgs e)
        {
            Entrar();
        }

        private void Entrar()
        {
            try
            {
                // Si los campos Usuario y Contraseña no estan vacíos...
                if (!String.IsNullOrEmpty(txtUsuarioEnSesion.Text) && !String.IsNullOrEmpty(pwdPassEnSesion.Password))
                {
                    // Buscamos al empleado dentro de la base de datos...
                    MainWindow.empleadoLogueado = MainWindow.unit.RepositorioEmpleados.GetOne(c => c.usuario.Equals(txtUsuarioEnSesion.Text) && c.contrasena.Equals(pwdPassEnSesion.Password));
                    // Si está...
                    if (MainWindow.empleadoLogueado != null)
                    {
                        MainWindow mainWindow = new MainWindow();
                        mainWindow.Show();
                        // Limpiamos los campos para el próximo inicio de sesión.
                        txtUsuarioEnSesion.Clear();
                        pwdPassEnSesion.Clear();
                        this.Close();
                    }
                    // Mostramos mensaje de que el usuario no se encuentra registrado o que ha fallado la contraseña.
                    else { System.Windows.MessageBox.Show("El usuario no se encuentra registrado o ha cometido un error en la contraseña."); }
                }
                // Mostramos mensaje de que los campos están vacios.
                else { System.Windows.MessageBox.Show("Rellene los campos 'Usuario' y 'Contraseña'."); }
            }
            catch (Exception ex)
            {
                System.Windows.MessageBox.Show("Ha ocurrido un error. Pongase en contacto con el administrador.");
                Console.WriteLine("Error: " + ex.ToString());
            }
}

        private void lbSalir_MouseDown(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

       
    }
}
