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
using GestorTiendaInformatica.DAL;
using GestorTiendaInformatica.Model;

namespace GestorTiendaInformatica
{
    /// <summary>
    /// Lógica de interacción para PaginaRegistro.xaml
    /// </summary>
    public partial class PaginaRegistro : Page
    {
        public Frame f;
        UnitOfWork uow = new UnitOfWork();
        public PaginaRegistro(Frame f)
        {
            InitializeComponent();
            this.f = f;
        }

        private void BotonVolver_Click(object sender, RoutedEventArgs e)
        {
            PaginaInicio pI = new PaginaInicio(f);
            f.Content = pI;
        }

        private void BotonAgregar_Click(object sender, RoutedEventArgs e)
        {
            // COMPROBACIONES:

            // CREAR USUARIO:
            Usuario u = new Usuario
            {
                Nombre = textNombre.Text,
                user = textUsuario.Text,
                password = textContraseña.Password,
                TipoCuenta = "Usuario"
            };

            // AÑADIR USUARIO:
            uow.UsuarioRepositorio.Añadir(u);
        }
    }
}