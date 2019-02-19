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
    /// Lógica de interacción para PaginaClientes.xaml
    /// </summary>
    public partial class PaginaClientes : Page
    {
        Frame f;
        UnitOfWork uow =  new UnitOfWork();
        List<Cliente> listaClientes;
        bool nuevo = true;

        public PaginaClientes(Frame f)
        {
            InitializeComponent();
            this.f = f;
            listaClientes = uow.ClienteRepositorio.GetAll();
            cb_clientes.ItemsSource = listaClientes;
        }

        private void Btn_limpiar_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
