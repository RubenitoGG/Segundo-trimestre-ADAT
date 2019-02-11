﻿using System;
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

namespace GestorTiendaInformatica
{
    /// <summary>
    /// Lógica de interacción para PaginaInicio.xaml
    /// </summary>
    public partial class PaginaInicio : Page
    {
        Frame f;
        public PaginaInicio(Frame f)
        {
            InitializeComponent();
            this.f = f;
        }

        private void BotonRegistrarse_Click(object sender, RoutedEventArgs e)
        {
            PaginaRegistro pG = new PaginaRegistro(f);
            f.Content = pG;
        }
    }
}
