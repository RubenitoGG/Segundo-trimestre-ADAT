﻿using System;
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
using WpfBiblioteca.DAL;
using WpfBiblioteca.Model;

namespace WpfBiblioteca
{
    /// <summary>
    /// Lógica de interacción para MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        UnitOfWork uow = new UnitOfWork();
        Libro l = new Libro();

        public MainWindow()
        {
            InitializeComponent();
            dgEjemplares.ItemsSource = l.Ejemplares.ToList();
        }

        private void Btn_registrar_Click(object sender, RoutedEventArgs e)
        {
            l = new Libro
            {
                Isbn = txt_isbn.Text,
                Titulo = txt_titulo.Text,
                Editorial = txt_editorial.Text,
                Descripcion = txt_descripcion.Text
            };

            uow.LibrosRepositorio.Añadir(l);
        }

        private void Btn_GuardarEjemplar_Click(object sender, RoutedEventArgs e)
        {
            Ejemplar ejem = new Ejemplar
            {
                LibroId = l.LibroId,
                NumeroEjemplar = Convert.ToInt16(txt_numEjemplar.Text),
                FechaPublicacion = dp_FechaPublicacion.SelectedDate,
                Estado = "D"
            };

            l.Ejemplares.Add(ejem);
            uow.LibrosRepositorio.Update(l);
        }
    }
}
