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
    /// Lógica de interacción para PaginaCategoria.xaml
    /// </summary>
    public partial class PaginaCategoria : Page
    {
        Frame f;
        public PaginaCategoria(Frame f)
        {
            InitializeComponent();
            this.f = f;
        }
    }
}
