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
        Usuario usuario;
        bool nuevo = true;
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
            this.usuario = (Usuario)dg_usuarios.SelectedItem;
            this.nuevo = false;
            btn_guardar.Content = "Actualizar";

            if (this.usuario != null)
            {
                txt_id.Text = this.usuario.UsuarioId.ToString();
                txt_nombre.Text = this.usuario.Nombre;
                txt_usuario.Text = this.usuario.user;
                txt_pass.Text = this.usuario.password;
                txt_tipo.Text = this.usuario.TipoCuenta;

                cb_tipo.SelectedItem = "";
            }

        }

        private void Cb_tipo_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            txt_tipo.Text = cb_tipo.SelectedValue.ToString();
        }

        private void Btn_guardar_Click(object sender, RoutedEventArgs e)
        {
            if (!nuevo) // ACTUALIZAR USUARIO:
            {

                this.usuario.UsuarioId = Convert.ToInt32(txt_id.Text);
                this.usuario.Nombre = txt_nombre.Text;
                this.usuario.user = txt_usuario.Text;
                this.usuario.password = txt_pass.Text;
                this.usuario.TipoCuenta = txt_tipo.Text;
                this.usuario.Ventas = this.usuario.Ventas;

                uow.UsuarioRepositorio.Update(usuario);
            }
            else // AÑADIR NUEVO USUARIO:
            {
                // COMPROBACIONES:

                Usuario u = new Usuario
                {
                    Nombre = txt_nombre.Text,
                    user = txt_usuario.Text,
                    password = txt_pass.Text,
                    TipoCuenta = txt_tipo.Text
                };
                uow.UsuarioRepositorio.Añadir(u);
            }

            // ACTUALIZAR DATAGRID:
            BorrarCampos();
            dg_usuarios.ItemsSource = "";
            dg_usuarios.ItemsSource = uow.UsuarioRepositorio.GetAll();
            dg_usuarios.Items.Refresh();
        }

        private void Btn_eliminar_Click(object sender, RoutedEventArgs e)
        {
            this.usuario = (Usuario)dg_usuarios.SelectedItem;
            uow.UsuarioRepositorio.Delete(this.usuario);
            this.usuario = null;
            BorrarCampos();
            dg_usuarios.ItemsSource = "";
            dg_usuarios.ItemsSource = uow.UsuarioRepositorio.GetAll();
        }

        private void BorrarCampos()
        {
            txt_id.Text = "";
            txt_nombre.Text = "";
            txt_pass.Text = "";
            txt_tipo.Text = "";
            txt_usuario.Text = "";
        }

        private void Btn_añadir_Click(object sender, RoutedEventArgs e)
        {
            nuevo = true;
            btn_guardar.Content = "Añadir";
            BorrarCampos();
        }

        private void Btn_buscar_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
