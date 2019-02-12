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

namespace GestorTiendaInformatica
{
    /// <summary>
    /// Lógica de interacción para PaginaClientes.xaml
    /// </summary>
    public partial class PaginaClientes : Page
    {
        Frame f;
        public PaginaClientes(Frame f)
        {
            InitializeComponent();
            this.f = f;
        }
    }
}
