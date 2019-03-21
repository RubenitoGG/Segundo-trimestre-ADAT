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
using ProyectoCine.MODEL;
using ProyectoCine.DAL;

namespace ProyectoCine
{
    /// <summary>
    /// Lógica de interacción para Recaudación.xaml
    /// </summary>
    public partial class Recaudación : Window
    {
        public Recaudación(string codPelicula)
        {
            InitializeComponent();
            try
            {
                int totalEntradas = 0;
                double totalRecaudacion = 0;

                List<Funcion> funciones = MainWindow.unit.RepositorioFunciones.GetGroup(c => c.CodigoPelicula.Equals(codPelicula));
                foreach (Funcion fun in funciones)
                {
                    List<Entrada> entradas = MainWindow.unit.RepositorioEntradas.GetGroup(c => c.CodigoFuncion.Equals(fun.CodigoFuncion));
                    totalEntradas += entradas.Count;
                    foreach (Entrada ent in entradas)
                    {
                        totalRecaudacion += ent.Tarifa.precio;
                    }
                }

                lblTotalEntradasVendidas.Content = totalEntradas;
                lblTotalRecaudación.Content = Math.Round(totalRecaudacion,2);
            }catch(Exception ex)
            {
                System.Windows.MessageBox.Show("Ha ocurrido un error. Pongase en contacto con el administrador.");
                Console.WriteLine("Error: " + ex.ToString());
            }
        }
    }
}
