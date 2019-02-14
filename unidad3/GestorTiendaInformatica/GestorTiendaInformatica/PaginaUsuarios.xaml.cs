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
using GestorTiendaInformatica.Model;
using GestorTiendaInformatica.DAL;

namespace GestorTiendaInformatica
{
    /// <summary>
    /// Lógica de interacción para PaginaUsuarios.xaml
    /// </summary>
    public partial class PaginaUsuarios : Page
    {
        Frame f;
        UnitOfWork uow = new UnitOfWork();
        public PaginaUsuarios(Frame f)
        {
            InitializeComponent();
            this.f = f;
            dg_usuarios.ItemsSource = uow.UsuarioRepositorio.GetAll();

            List<string> s = new List<string>();
            s.Add("Administrador");
            s.Add("Usuario");
            cb_tipo.ItemsSource = s;
        }

        private void Dg_usuarios_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Usuario u = (Usuario)dg_usuarios.SelectedItem;
            txt_id.Text = u.UsuarioId.ToString();
            txt_nombre.Text = u.Nombre;
            txt_usuario.Text = u.user;
            txt_pass.Text = u.password;
            txt_tipo.Text = u.TipoCuenta;

            cb_tipo.SelectedItem = "";
        }

        private void Cb_tipo_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            txt_tipo.Text = cb_tipo.SelectedValue.ToString();
        }

        private void Btn_guardar_Click(object sender, RoutedEventArgs e)
        {
            Usuario u = new Usuario
            {
                UsuarioId = Convert.ToInt32(txt_id.Text),
                Nombre = txt_nombre.Text,
                user = txt_usuario.Text,
                password = txt_pass.Text,
                TipoCuenta = txt_tipo.Text
            };

            uow.UsuarioRepositorio.Update(u);
        }

        private void Btn_eliminar_Click(object sender, RoutedEventArgs e)
        {
            Usuario u = (Usuario)dg_usuarios.SelectedItem;
            uow.UsuarioRepositorio.Delete(u);
            dg_usuarios.ItemsSource = null;
            dg_usuarios.ItemsSource = uow.UsuarioRepositorio.GetAll();
            dg_usuarios.Items.Refresh();
        }
    }
}
