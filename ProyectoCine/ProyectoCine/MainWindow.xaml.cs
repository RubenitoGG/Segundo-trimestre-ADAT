using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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
using ProyectoCine.MODEL;
using ProyectoCine.DAL;
using System.Windows.Forms;
using System.IO;
using System.Globalization;

namespace ProyectoCine
{
    /// <summary>
    /// Lógica de interacción para MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        #region comVariables
        // La variable mainWindow se usa para activar o desactivar la misma ventana.
        public static Window mainWindow;
        // Las variables facturado y vendido permiten controlar si la venta se ha realizado o no en la ventana Facturar.
        public static bool facturado;
        public static bool vendido;
        public static UnitOfWork unit = new UnitOfWork();
        // Empleado que inicia sesión, determinará a que permisos accederá el terminal.
        public static Empleado empleadoLogueado = new Empleado();
        // Objetos creados especificamente para gestión.
        Empleado empleadoGestionar = new Empleado();
        Cliente clienteGestionar = new Cliente();
        Tarifa tarifaGestionar = new Tarifa();
        Funcion funcionGestionar = new Funcion();
        Sala salaGestionar = new Sala();
        Horario horarioGestionar = new Horario();
        Pelicula peliculaGestionar = new Pelicula();
        Productora productoraGestionar = new Productora();
        CategoriaPelicula categoriaPeliculaGestionar = new CategoriaPelicula();
        public static List<Entrada> entradasVender = new List<Entrada>();
        public static Tarifa tarifaVenta;
        Entrada entradaGestionar = new Entrada();

        Proveedor proveedorGestionar = new Proveedor();
        CategoriaProducto categoriaProductoGestionar = new CategoriaProducto();
        Producto productoGestionar = new Producto();
        Producto productoTPV = new Producto();
        public static Venta ventaGestionar;
        LineaVenta lineaVentaGestionar = new LineaVenta();
        // Variable que determinará en que tabItem nos encontramos.
        // Inicializado en -1 dado que no se encuentra en ningun tabItem.
        int index = -1;
        // Bool para la imagen.
        bool copiado;
        // Directorio imagen
        string directorioCompleto;
        // Esta lista nos permitirá guardar las peliculas que hay en cartelera para despues operar con ellas.
        List<Pelicula> peliculasCartelera;
        // Esta variable es la que nos permite definir cual es la pelicula que vamos a seleccionar en el TPV
        int peliculaSeleccionadaIndex;
        Pelicula peliculaSeleccionadaTPV;
        Funcion funcionTPV;
        public static string tipoListaLeer;
        public static string tipoFactura;

        CultureInfo ci = new CultureInfo("Es-Es");
        #endregion
        public MainWindow()
        {
            InitializeComponent();
            mainWindow = this;

            crearDirectorios();

            // Inicializamos el indice del tabControll
            index = -1;
            tbcPrincipal.SelectedIndex = index;

            // Limpiamos el status bar y creamos un nuevo txtblock, le asignamos el nombre del empleado logueado y se lo añadimos.
            statbarEmpleado.Items.Clear();
            TextBlock txtbEmpleado = new TextBlock();
            txtbEmpleado.Text = "El empleado logueado es: " + empleadoLogueado.nombre;
            statbarEmpleado.Items.Add(txtbEmpleado);

            // Depende de los permisos activamos unos tabs u otros.
            switch (empleadoLogueado.rango)
            {
                case "Super Administrador":

                    break;
                case "Administrador departamento comestibles":
                    tbcPrincipal.Items.Remove(tbFunciones);
                    tbcPrincipal.Items.Remove(tbHorarios);
                    tbcPrincipal.Items.Remove(tbSalas);
                    tbcPrincipal.Items.Remove(tbTarifas);
                    tbcPrincipal.Items.Remove(tbProductoras);
                    tbcPrincipal.Items.Remove(tbCategorias);
                    tbcPrincipal.Items.Remove(tbPeliculas);
                    tbcPrincipal.Items.Remove(tbTPV);
                    break;
                case "Administrador departamento cine":
                    tbcPrincipal.Items.Remove(tbProveedores);
                    tbcPrincipal.Items.Remove(tbCategoriasProductos);
                    tbcPrincipal.Items.Remove(tbProductos);
                    tbcPrincipal.Items.Remove(tbTPVProducto);
                    break;
                case "Empleado tienda comestibles":
                    tbcPrincipal.Items.Remove(tbEmpleados);
                    tbcPrincipal.Items.Remove(tbClientes);
                    tbcPrincipal.Items.Remove(tbFunciones);
                    tbcPrincipal.Items.Remove(tbHorarios);
                    tbcPrincipal.Items.Remove(tbSalas);
                    tbcPrincipal.Items.Remove(tbTarifas);
                    tbcPrincipal.Items.Remove(tbProductoras);
                    tbcPrincipal.Items.Remove(tbCategorias);
                    tbcPrincipal.Items.Remove(tbPeliculas);
                    tbcPrincipal.Items.Remove(tbProveedores);
                    tbcPrincipal.Items.Remove(tbCategoriasProductos);
                    tbcPrincipal.Items.Remove(tbProductos);
                    tbcPrincipal.Items.Remove(tbTPV);
                    break;
                case "Empleado tienda cine":
                    tbcPrincipal.Items.Remove(tbEmpleados);
                    tbcPrincipal.Items.Remove(tbFunciones);
                    tbcPrincipal.Items.Remove(tbHorarios);
                    tbcPrincipal.Items.Remove(tbSalas);
                    tbcPrincipal.Items.Remove(tbTarifas);
                    tbcPrincipal.Items.Remove(tbProductoras);
                    tbcPrincipal.Items.Remove(tbCategorias);
                    tbcPrincipal.Items.Remove(tbPeliculas);
                    tbcPrincipal.Items.Remove(tbProveedores);
                    tbcPrincipal.Items.Remove(tbCategoriasProductos);
                    tbcPrincipal.Items.Remove(tbProductos);
                    tbcPrincipal.Items.Remove(tbTPVProducto);
                    break;

            }
        }

        private Boolean validado(Object obj)
        {
            ValidationContext validationContext = new ValidationContext(obj, null, null);
            List<System.ComponentModel.DataAnnotations.ValidationResult> errors = new List<System.ComponentModel.DataAnnotations.ValidationResult>();
            Validator.TryValidateObject(obj, validationContext, errors, true);

            if (errors.Count() > 0)
            {

                string mensageErrores = string.Empty;
                foreach (var error in errors)
                {
                    error.MemberNames.First();

                    mensageErrores += error.ErrorMessage + Environment.NewLine;
                }
                System.Windows.MessageBox.Show(mensageErrores); return false;
            }
            else
            {
                return true;
            }
        }

        // Región para Tabs. Aquí se limpiaran los campos de cada uno y se inicializaran variables para su gestión.
        #region comTabs
        private void tbcPrincipal_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (tbcPrincipal.SelectedIndex < 0 || tbcPrincipal.SelectedIndex == index)
            {
                return;
            }
            if (tbEmpleados.IsSelected && tbcPrincipal.SelectedIndex != index)
            {
                LimpiarCamposEmpleado();

                cbxBuscarEmpleado.SelectedIndex = -1;
                txtBuscarEmpleado.Text = "";
                index = tbcPrincipal.SelectedIndex;
            }
            if (tbClientes.IsSelected && tbcPrincipal.SelectedIndex != index)
            {
                LimpiarCamposCliente();

                cbxBuscarCliente.SelectedIndex = -1;
                txtBuscarClientes.Text = "";
                index = tbcPrincipal.SelectedIndex;
            }
            if (tbTarifas.IsSelected && tbcPrincipal.SelectedIndex != index)
            {
                LimpiarCamposTarifa();

                txtBuscarTarifa.Text = "";
                index = tbcPrincipal.SelectedIndex;
            }
            if (tbFunciones.IsSelected && tbcPrincipal.SelectedIndex != index)
            {
                LimpiarCamposFuncion();

                dtgrSalasFuncion.ItemsSource = "";
                dtgrSalasFuncion.ItemsSource = unit.RepositorioSalas.GetAll();
                dtgrPelisFuncion.ItemsSource = "";
                dtgrPelisFuncion.ItemsSource = unit.RepositorioPeliculas.GetAll();

                dtpkFechaInicioFuncion.DisplayDateStart = DateTime.Today;
                dtpkFechaFinFuncion.DisplayDateStart = DateTime.Today;

                txtBuscarFuncion.Text = "";
                index = tbcPrincipal.SelectedIndex;
            }
            if (tbSalas.IsSelected && tbcPrincipal.SelectedIndex != index)
            {
                LimpiarCamposSala();

                cbxBuscarSala.Text = "";
                txtBuscarSala.Text = "";
                index = tbcPrincipal.SelectedIndex;
            }
            if (tbHorarios.IsSelected && tbcPrincipal.SelectedIndex != index)
            {
                LimpiarCamposHorario();

                dtgrSalasHorarios.ItemsSource = "";
                dtgrSalasHorarios.ItemsSource = unit.RepositorioFunciones.GetAll();

                cbxBuscarHorario.SelectedIndex = -1;
                txtBuscarHorario.Text = "";
                index = tbcPrincipal.SelectedIndex;
            }
            if (tbPeliculas.IsSelected && tbcPrincipal.SelectedIndex != index)
            {
                LimpiarCamposPelicula();

                dtpkFechaEstrenoPelicula.DisplayDateStart = DateTime.Today;

                cbxBuscarPelicula.SelectedIndex = -1;
                txtBuscarPelicula.Text = "";
                index = tbcPrincipal.SelectedIndex;
            }
            if (tbProductoras.IsSelected && tbcPrincipal.SelectedIndex != index)
            {
                LimpiarCamposProductora();

                cbxBuscarProductora.SelectedIndex = -1;
                txtBuscarProductora.Text = "";
                index = tbcPrincipal.SelectedIndex;
            }
            if (tbCategorias.IsSelected && tbcPrincipal.SelectedIndex != index)
            {
                LimpiarCamposCategoríaPelícula();

                cbxBuscarCategorias.SelectedIndex = -1;
                txtBuscarCategoria.Text = "";
                index = tbcPrincipal.SelectedIndex;
            }
            if (tbTPV.IsSelected && tbcPrincipal.SelectedIndex != index)
            {
				mainWindow = this;
				entradasVender = new List<Entrada>();
                facturado = false;

                peliculaSeleccionadaIndex = 0;
                GestionarPeliculaTPV();
                cbxFuncionesTPV.DisplayMemberPath = "CodigoFuncion";
                cbxFuncionesTPV.SelectedValuePath = "CodigoFuncion";
                cbxTarifasFuncionTPV.DisplayMemberPath = "CodigoTarifa";
                cbxTarifasFuncionTPV.SelectedValuePath = "CodigoTarifa";
                index = tbcPrincipal.SelectedIndex;
            }
            if (tbProveedores.IsSelected && tbcPrincipal.SelectedIndex != index)
            {
                LimpiarCamposProveedor();

                cbxBuscarProveedor.SelectedIndex = -1;
                txtBuscarProveedores.Text = "";
                index = tbcPrincipal.SelectedIndex;
            }
            if (tbCategoriasProductos.IsSelected && tbcPrincipal.SelectedIndex != index)
            {
                LimpiarCamposCategoríaProducto();

                cbxBuscarCategoriasProductos.SelectedIndex = -1;
                txtBuscarCategoriaProductos.Text = "";
                index = tbcPrincipal.SelectedIndex;
            }
            if (tbProductos.IsSelected && tbcPrincipal.SelectedIndex != index)
            {
                LimpiarCamposProducto();

                txtBuscarProductos.Text = "";
                index = tbcPrincipal.SelectedIndex;
            }
            if (tbTPVProducto.IsSelected && tbcPrincipal.SelectedIndex != index)
            {
				mainWindow = this;
				GestionarTPVenta();
                index = tbcPrincipal.SelectedIndex;
            }

            if (ventaGestionar != null && ventaGestionar.LineasVentas.Count != 0)
                LimpiarVentaGestionar();
        }
        #endregion
        // Región para la gestión de los empleados. Añadir, modificar, eliminar, buscar, limpiar campos, abrir listado.
        #region comEmpleados
        // Método que se ejecuta al darle al botón de buscar (Lupa). 
        private void btnBuscarEmpleado_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                // Si los campos de busqueda no estan vacíos...
                if (!String.IsNullOrEmpty(cbxBuscarEmpleado.Text) && !String.IsNullOrEmpty(txtBuscarEmpleado.Text))
                {
                    // Buscamos por el parametro seleccionado en el comboBox.
                    switch (cbxBuscarEmpleado.Text)
                    {
                        case "Código":
                            empleadoGestionar = unit.RepositorioEmpleados.GetOne(c => c.CodigoEmpleado.Equals(txtBuscarEmpleado.Text));
                            break;
                        case "Usuario":
                            empleadoGestionar = unit.RepositorioEmpleados.GetOne(c => c.usuario.Equals(txtBuscarEmpleado.Text));
                            break;
                        case "NIF":
                            empleadoGestionar = unit.RepositorioEmpleados.GetOne(c => c.nif.Equals(txtBuscarEmpleado.Text));
                            break;
                        case "Teléfono":
                            empleadoGestionar = unit.RepositorioEmpleados.GetOne(c => c.telefono.Equals(txtBuscarEmpleado.Text));
                            break;
                        case "Email":
                            empleadoGestionar = unit.RepositorioEmpleados.GetOne(c => c.correo.Equals(txtBuscarEmpleado.Text));
                            break;

                    }
                    // Si devuelve un empleado...
                    if (empleadoGestionar != null)
                    {
                        // Le pasamos el contexto al dockPanel y establecemos de forma directa la contraseña en el passwordBox.
                        dckpEmpleados.DataContext = empleadoGestionar;
                        pswContrasenaEmpleado.Password = empleadoGestionar.contrasena;
                    }
                    // Mensaje de que el empleado no se encuentra.
                    else
                    {
                        System.Windows.MessageBox.Show("El empleado no se encuentra registrado.");
                        LimpiarCamposEmpleado();
                    }
                }
                // Mensaje de que no se han rellenado los campos.
                else
                {
                    System.Windows.MessageBox.Show("Debe señalar el código/usuario/NIF de quien desea buscar.");
                }
            }
            catch (Exception ex)
            {
                System.Windows.MessageBox.Show("Ha ocurrido un error. Pongase en contacto con el administrador.");
                Console.WriteLine("Error: " + ex.ToString());
            }
        }

        // Método que se ejecuta al darle al boton de añadir(disquete).
        private void btnAñadirEmpleado_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                // Establecemos el codigo y el password directamente.
                empleadoGestionar.CodigoEmpleado = txtCodigoEmpleado.Text;
                empleadoGestionar.contrasena = pswContrasenaEmpleado.Password;
                // Buscamos al empleado en la base de datos.
                Empleado aux = unit.RepositorioEmpleados.GetOne(c => c.CodigoEmpleado.Equals(empleadoGestionar.CodigoEmpleado) || c.usuario.Equals(empleadoGestionar.usuario)
                    || c.telefono.Equals(empleadoGestionar.telefono) || c.nif.Equals(empleadoGestionar.nif) ||
                    c.correo.Equals(empleadoGestionar.correo));
                // Si no lo encuentra...
                if (aux == null)
                {
                    // Si el objeto pasa las validaciones...
                    if (validado(empleadoGestionar))
                    {
                        // Lo añadimos a la base de datos.
                        unit.RepositorioEmpleados.Add(empleadoGestionar);
                        System.Windows.MessageBox.Show("Empleado registrado.");
                        // Limpiamos los campos.
                        LimpiarCamposEmpleado();
                    }
                }
                // Si lo encuentra, manda mensaje de que se ha creado un usuario con los mismos campos.
                else
                {
                    String error = "El empleado ya ha sido registrado con el mismo:";
                    if (aux.CodigoEmpleado == empleadoGestionar.CodigoEmpleado)
                        error += " código";
                    if (aux.usuario == empleadoGestionar.usuario)
                        error += " usuario ";
                    if (aux.telefono == empleadoGestionar.telefono)
                        error += " teléfono ";
                    if (aux.nif == empleadoGestionar.nif)
                        error += " NIF ";
                    if (aux.correo == empleadoGestionar.correo)
                        error += " correo ";
                    System.Windows.MessageBox.Show(error);
                }
            }
            catch (Exception ex)
            {
                System.Windows.MessageBox.Show("Ha ocurrido un error. Pongase en contacto con el administrador.");
                Console.WriteLine("Error: " + ex.ToString());
            }
        }

        // Método que se ejecuta cuando le das al botón de editar(lápiz).
        private void btnModificarEmpleado_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (!String.IsNullOrEmpty(txtCodigoEmpleado.Text))
                {
                    Empleado aux = unit.RepositorioEmpleados.GetOne(c => c.CodigoEmpleado.Equals(txtCodigoEmpleado.Text));
                    // Establecemos el password directamente.
                    if (aux != null)
                    {
                        empleadoGestionar.contrasena = pswContrasenaEmpleado.Password;
                        // Si pasa las validaciones...
                        if (validado(empleadoGestionar))
                        {
                            // Modificamos los datos en la base de datos.
                            unit.RepositorioEmpleados.Update(empleadoGestionar);
                            System.Windows.MessageBox.Show("Empleado modificado.");
                            LimpiarCamposEmpleado();
                        }
                    }
                    else { System.Windows.MessageBox.Show("El empleado no se encuentra registrado. No se puede modificar."); }
                }
                else { System.Windows.MessageBox.Show("Establezca un código en el campo correspondiente."); }
            }
            catch (Exception ex)
            {
                System.Windows.MessageBox.Show("Ha ocurrido un error. Pongase en contacto con el administrador.");
                Console.WriteLine("Error: " + ex.ToString());
            }
        }

        // Método que se ejecuta cuando le das al boton de eliminar(basura).
        private void btnEliminarEmpleado_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (!String.IsNullOrEmpty(txtCodigoEmpleado.Text))
                {
                    // Buscamos si existe el empleado
                    Empleado aux = unit.RepositorioEmpleados.GetOne(c => c.CodigoEmpleado.Equals(txtCodigoEmpleado.Text));
                    // Si el empleado se encuentra registrado...
                    if (aux != null)
                    {
                        // Si el objeto pasa las validaciones...
                        if (validado(empleadoGestionar))
                        {
                            // Eliminamos los datos de la base de datos.
                            unit.RepositorioEmpleados.Delete(empleadoGestionar);
                            System.Windows.MessageBox.Show("Empleado eliminado.");
                            // Limpiamos los campos.
                            LimpiarCamposEmpleado();
                        }
                    }
                    else { System.Windows.MessageBox.Show("El empleado no se encuentra registrado con ese código. No se puede eliminar."); }
                }
                else { System.Windows.MessageBox.Show("Establezca un código en el campo correspondiente."); }
            }
            catch (Exception ex)
            {
                System.Windows.MessageBox.Show("Ha ocurrido un error. Pongase en contacto con el administrador.");
                Console.WriteLine("Error: " + ex.ToString());
            }
        }

        // Método que se ejecuta al darle al botón de limpiar(papelera).
        private void btnLimpiarEmpleado_Click(object sender, RoutedEventArgs e)
        {
            // Limpiamos los campos.
            LimpiarCamposEmpleado();
        }

        // Método para limpiar los campos de empleado.
        private void LimpiarCamposEmpleado()
        {
            empleadoGestionar = new Empleado();
            dckpEmpleados.DataContext = empleadoGestionar;
            pswContrasenaEmpleado.Clear();
        }

        // Método que se ejecuta al darle al botón de abrir lista(listado).
        private void btnListaEmpleados_Click(object sender, RoutedEventArgs e)
        {
            tipoListaLeer = "Empleados";
            Listas listas = new Listas();
            listas.Show();
        }
        #endregion
        // Región para la gestión de los clientes. Añadir, modificar, eliminar, buscar, limpiar campos, abrir listado.
        #region comClientes
        // Método que se ejecuta al darle al botón de buscar (Lupa). 
        private void btnBuscarClientes_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                // Comprobamos que los campos no estan vacios...
                if (!String.IsNullOrEmpty(cbxBuscarCliente.Text) && !String.IsNullOrEmpty(txtBuscarClientes.Text))
                {
                    // Buscamos por el campo correspondiente.
                    switch (cbxBuscarCliente.Text)
                    {
                        case "Id":
                            int id;
                            if (Int32.TryParse(txtBuscarClientes.Text, out id))
                            {
                                id = Convert.ToInt32(txtBuscarClientes.Text);
                                clienteGestionar = unit.RepositorioClientes.GetOne(c => c.ClienteId == id);
                            }
                            else
                            {
                                System.Windows.MessageBox.Show("El id es numerico.");
                                return;
                            }
                            break;
                        case "NIF":
                            clienteGestionar = unit.RepositorioClientes.GetOne(c => c.nif.Equals(txtBuscarClientes.Text));
                            break;
                        case "Teléfono":
                            clienteGestionar = unit.RepositorioClientes.GetOne(c => c.telefono.Equals(txtBuscarClientes.Text));
                            break;
                        case "Email":
                            clienteGestionar = unit.RepositorioClientes.GetOne(c => c.correo.Equals(txtBuscarClientes.Text));
                            break;
                    }

                    // Si lo encuentra...
                    if (clienteGestionar != null)
                    {
                        // Establecemos el contexto del dockPanel.
                        dckpClientes.DataContext = clienteGestionar;
                    }
                    // Mensaje si no encuentra al cliente.
                    else
                    {
                        System.Windows.MessageBox.Show("El cliente no se encuentra registrado.");
                        LimpiarCamposCliente();
                    }
                }
                // Mensaje si no introduce los campos de busqueda.
                else
                {
                    System.Windows.MessageBox.Show("Debe señalar el Id/NIF/teléfono/email de quien desea buscar.");
                }
            }
            catch (Exception ex)
            {
                System.Windows.MessageBox.Show("Ha ocurrido un error. Pongase en contacto con el administrador.");
                Console.WriteLine("Error: " + ex.ToString());
            }
        }
        // Método que se ejecuta al darle al boton de añadir(disquete).
        private void btnAñadirCliente_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                // Buscamos al cliente...
                Cliente aux = unit.RepositorioClientes.GetOne(c => c.nif.Equals(clienteGestionar.nif) || c.correo.Equals(clienteGestionar.correo)
                        || c.telefono.Equals(clienteGestionar.telefono));

                // Si no encuentra...
                if (aux == null)
                {
                    // Si pasa las validaciones.
                    if (validado(clienteGestionar))
                    {
                        // Añadimos los datos a la base de datos.
                        unit.RepositorioClientes.Add(clienteGestionar);
                        System.Windows.MessageBox.Show("Cliente registrado.");
                        // Limpiamos los campos
                        LimpiarCamposCliente();
                    }
                }
                // Mensaje de que un cliente ha sido registrado ya con los mismos datos.
                else
                {
                    String error = "El cliente ya ha sido registrado con el mismo:";
                    if (aux.telefono == clienteGestionar.telefono)
                        error += " teléfono ";
                    if (aux.nif == clienteGestionar.nif)
                        error += " NIF ";
                    if (aux.correo == clienteGestionar.correo)
                        error += " correo ";
                    System.Windows.MessageBox.Show(error);
                }
            }
            catch (Exception ex)
            {
                System.Windows.MessageBox.Show("Ha ocurrido un error. Pongase en contacto con el administrador.");
                Console.WriteLine("Error: " + ex.ToString());
            }
        }
        // Método que se ejecuta cuando le das al botón de editar(lápiz).
        private void btnModificarCliente_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                // Buscamos al cliente...
                Cliente aux = unit.RepositorioClientes.GetOne(c => c.nif.Equals(clienteGestionar.nif));

                if (aux != null)
                {
                    // Si pasa las validaciones...
                    if (validado(clienteGestionar))
                    {
                        // Modifica los datos en la base de datos.
                        unit.RepositorioClientes.Update(clienteGestionar);
                        System.Windows.MessageBox.Show("Cliente modificado.");
                        LimpiarCamposCliente();
                    }
                }
                else { System.Windows.MessageBox.Show("El cliente no se encuentra registrado con ese nif no se puede modificar."); }
            }
            catch (Exception ex)
            {
                System.Windows.MessageBox.Show("Ha ocurrido un error. Pongase en contacto con el administrador.");
                Console.WriteLine("Error: " + ex.ToString());
            }
        }
        // Método que se ejecuta cuando le das al boton de eliminar(basura).
        private void btnEliminarCliente_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                // Buscamos al cliente por el nif
                Cliente aux = unit.RepositorioClientes.GetOne(c => c.nif.Equals(clienteGestionar.nif));
                // Si se encuentra el cliente registrado...
                if (aux != null)
                {
                    if (aux.ClienteId != 1)
                    {
                        // Si pasa las validaciones.
                        if (validado(clienteGestionar))
                        {
                            // Eliminamos los datos de la base de datos.
                            unit.RepositorioClientes.Delete(clienteGestionar);
                            System.Windows.MessageBox.Show("Cliente eliminado.");
                            // Limpiamos los campos
                            LimpiarCamposCliente();
                        }
                    }
                    else { System.Windows.MessageBox.Show("No se puede eliminar el cliente genérico."); }
                }
                else { System.Windows.MessageBox.Show("El cliente no se encuentra registrado con ese NIF. No se puede eliminar."); }
            }
            catch (Exception ex)
            {
                System.Windows.MessageBox.Show("Ha ocurrido un error. Pongase en contacto con el administrador.");
                Console.WriteLine("Error: " + ex.ToString());
            }
        }
        // Método que se ejecuta al darle al botón de limpiar(papelera).
        private void btnLimpiarClientes_Click(object sender, RoutedEventArgs e)
        {
            LimpiarCamposCliente();
        }
        // Método para limpiar los campos de empleado.
        private void LimpiarCamposCliente()
        {
            clienteGestionar = new Cliente();
            dckpClientes.DataContext = clienteGestionar;
        }
        // Método que se ejecuta al darle al boton de abrir lista(listado).
        private void btnListaClientes_Click(object sender, RoutedEventArgs e)
        {
            tipoListaLeer = "Clientes";
            Listas listas = new Listas();
            listas.Show();
        }
        #endregion
        // Región para la gestión de las tarifas. Añadir, modificar, eliminar, buscar, limpiar campos.
        #region comTarifas
        // Método que se ejecuta al darle al botón de buscar (Lupa). 
        private void btnBuscarTarifa_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                // Comprovamos si el campo esta vacío.
                if (!String.IsNullOrEmpty(txtBuscarTarifa.Text))
                {
                    // Buscamos por el campo.
                    tarifaGestionar = unit.RepositorioTarifas.GetOne(c => c.CodigoTarifa.Equals(txtBuscarTarifa.Text));
                    // Si encuentra...
                    if (tarifaGestionar != null)
                    {
                        // Establecemos el context del dockPanel.
                        dckpTarifas.DataContext = tarifaGestionar;
                    }
                    // Mensaje de que no encontro la tarifa.
                    else
                    {
                        System.Windows.MessageBox.Show("La tarifa no se encuentra registrada.");
                        LimpiarCamposTarifa();
                    }
                }
                // Mensaje de que no se introdujo los datos de busqueda.
                else
                {
                    System.Windows.MessageBox.Show("Debe señalar el código/formato de la tarifa desea buscar.");

                }
            }
            catch (Exception ex)
            {
                System.Windows.MessageBox.Show("Ha ocurrido un error. Pongase en contacto con el administrador.");
                Console.WriteLine("Error: " + ex.ToString());
            }
        }
        // Método que se ejecuta al darle al boton de añadir(disquete).
        private void btnAñadirTarifa_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                tarifaGestionar.CodigoTarifa = txtCodigoTarifa.Text;
                // Buscamos la tarifa.
                Tarifa aux = unit.RepositorioTarifas.GetOne(c => c.CodigoTarifa.Equals(tarifaGestionar.CodigoTarifa));
                // Si no encuentra...
                if (aux == null)
                {
                    double precio;
                    // Comprobamos que el campo se puede parsear a doble...
                    if (Double.TryParse(txtPrecioTarifa.Text, out precio))
                    {
                        // Si pasa las validaciones...
                        if (validado(tarifaGestionar))
                        {
                            // Añadimos los datos a la base de datos.
                            unit.RepositorioTarifas.Add(tarifaGestionar);
                            System.Windows.MessageBox.Show("Tarifa registrada.");
                            // Limpiamos los campos.
                            LimpiarCamposTarifa();
                        }
                    }
                    // Mensaje de que el campo de precio no tiene el formato correcto.
                    else
                    {
                        System.Windows.MessageBox.Show("El precio debe ser decimal.");
                        return;
                    }
                }
                // Mensaje de que la tarifa ya esta registrada.
                else { System.Windows.MessageBox.Show("La tarifa ya está registrada."); }
            }
            catch (Exception ex)
            {
                System.Windows.MessageBox.Show("Ha ocurrido un error. Pongase en contacto con el administrador.");
                Console.WriteLine("Error: " + ex.ToString());
            }
        }

        // Método que se ejecuta cuando le das al botón de editar(lápiz).
        private void btnModificarTarifa_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (!String.IsNullOrEmpty(txtCodigoTarifa.Text))
                {
                    // Buscamos la tarifa.
                    Tarifa aux = unit.RepositorioTarifas.GetOne(c => c.CodigoTarifa.Equals(txtCodigoTarifa.Text));
                    if (aux != null)
                    {
                        // Si pasa las validaciones...
                        if (validado(tarifaGestionar))
                        {
                            // Modificamos los datos en la base de datos.
                            unit.RepositorioTarifas.Update(tarifaGestionar);
                            // Recargamos el dataGrid.
                            dtgrTarifasActuales.ItemsSource = "";
                            dtgrTarifasActuales.ItemsSource = unit.RepositorioTarifas.GetAll();
                            System.Windows.MessageBox.Show("Tarifa modificada.");
                            LimpiarCamposTarifa();
                        }
                    }
                    else { System.Windows.MessageBox.Show("La tarifa no se encuentra registrada. No se puede modificar."); }
                }
                else { System.Windows.MessageBox.Show("Establezca un código en el campo correspondiente."); }
            }
            catch (Exception ex)
            {
                System.Windows.MessageBox.Show("Ha ocurrido un error. Pongase en contacto con el administrador.");
                Console.WriteLine("Error: " + ex.ToString());
            }
        }
        // Método que se ejecuta cuando le das al boton de eliminar(basura).
        private void btnEliminarTarifa_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (!String.IsNullOrEmpty(txtCodigoTarifa.Text))
                {
                    // Buscamos la tarifa.
                    Tarifa aux = unit.RepositorioTarifas.GetOne(c => c.CodigoTarifa.Equals(txtCodigoTarifa.Text));
                    // Si se encuentra...
                    if (aux != null)
                    {
                        // Si pasa las validaciones...
                        if (validado(tarifaGestionar))
                        {
                            // Eliminamos los datos de la base de datos.
                            unit.RepositorioTarifas.Delete(tarifaGestionar);
                            System.Windows.MessageBox.Show("Tarifa eliminada.");
                            LimpiarCamposTarifa();
                        }
                    }
                    else { System.Windows.MessageBox.Show("La tarifa no se encuentra registrada con ese código. No se puede eliminar."); }
                }
                else { System.Windows.MessageBox.Show("Establezca un código en el campo correspondiente."); }
            }
            catch (Exception ex)
            {
                System.Windows.MessageBox.Show("Ha ocurrido un error. Pongase en contacto con el administrador.");
                Console.WriteLine("Error: " + ex.ToString());
            }
        }
        // Método que se ejecuta al darle al botón de limpiar(papelera).
        private void btnLimpiarTarifa_Click(object sender, RoutedEventArgs e)
        {
            // Limpiamos los campos
            LimpiarCamposTarifa();
        }

        // Método que sirve para limpiar los campos de tarifa.
        private void LimpiarCamposTarifa()
        {
            tarifaGestionar = new Tarifa();
            dckpTarifas.DataContext = tarifaGestionar;
            dtgrTarifasActuales.ItemsSource = "";
            dtgrTarifasActuales.ItemsSource = unit.RepositorioTarifas.GetAll();
        }
        #endregion
        // Región para la gestión de los funciones. Añadir, modificar, eliminar, buscar, limpiar campos, abrir listado, comprobar CheckBox.
        #region comFunciones
        // Método que se ejecuta al darle al botón de buscar (Lupa). 
        private void btnBuscarFuncion_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                // Comprovamos si el campo no está vacio.
                if (!String.IsNullOrEmpty(txtBuscarFuncion.Text))
                {
                    // Buscamos por el campo correspondiente.
                    funcionGestionar = unit.RepositorioFunciones.GetOne(c => c.CodigoFuncion.Equals(txtBuscarFuncion.Text));

                    // Si encuentra...
                    if (funcionGestionar != null)
                    {
                        // Establecemos el contexto del dockPanel.
                        dckpFunciones.DataContext = funcionGestionar;
                    }
                    // Mensaje de que no se encuentra registrado.
                    else
                    {
                        System.Windows.MessageBox.Show("La función no se encuentra registrada.");
                        LimpiarCamposFuncion();
                    }
                }
                // Mensaje de que el campo de busqueda no esta completo.
                else
                {
                    System.Windows.MessageBox.Show("Debe señalar el Id de la función desea buscar.");
                }
            }
            catch (Exception ex)
            {
                System.Windows.MessageBox.Show("Ha ocurrido un error. Pongase en contacto con el administrador.");
                Console.WriteLine("Error: " + ex.ToString());
            }
        }
        // Método que se ejecuta al darle al boton de añadir(disquete).
        private void btnAñadirFuncion_Click(object sender, RoutedEventArgs e)
        {
            try
            {

                funcionGestionar.CodigoFuncion = txtCodigoFuncion.Text;
                // Buscamos la función.
                Funcion aux = unit.RepositorioFunciones.GetOne(c => c.CodigoPelicula.Equals(funcionGestionar.CodigoPelicula) &&
                c.CodigoSala.Equals(funcionGestionar.CodigoSala) || c.CodigoFuncion.Equals(funcionGestionar.CodigoFuncion));

                // Si no encuentra...
                if (aux == null)
                {
                    // Si pasa las validaciones...
                    if (validado(funcionGestionar))
                    {
                        // Buscamos la sala y la película por el código.
                        Sala auxSal = unit.RepositorioSalas.GetOne(c => c.CodigoSala.Equals(txtCodSalaFuncion.Text));
                        Pelicula auxPel = unit.RepositorioPeliculas.GetOne(c => c.CodigoPelicula.Equals(txtCodPeliFuncion.Text));
                        // Si las encuentra...
                        if (auxSal != null && auxPel != null)
                        {
                            // Añade los datos a la base de datos.
                            unit.RepositorioFunciones.Add(funcionGestionar);
                            System.Windows.MessageBox.Show("Función programada.");
                            // Limpiamos los campos.
                            LimpiarCamposFuncion();
                        }
                        // Mensaje de que no se encontraron la sala o la película.
                        else { System.Windows.MessageBox.Show("El código de sala/película es erroneo."); }
                    }
                }
                // Mensaje de que ya se encuentra registrada la función.
                else { System.Windows.MessageBox.Show("La función ya está registrada."); }
            }
            catch (Exception ex)
            {
                System.Windows.MessageBox.Show("Ha ocurrido un error. Pongase en contacto con el administrador.");
                Console.WriteLine("Error: " + ex.ToString());
            }
        }
        // Método que se ejecuta cuando le das al botón de editar(lápiz).
        private void btnModificarFuncion_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (!String.IsNullOrEmpty(txtCodigoFuncion.Text))
                {
                    Funcion aux = unit.RepositorioFunciones.GetOne(c => c.CodigoPelicula.Equals(funcionGestionar.CodigoPelicula) &&
                    c.CodigoSala.Equals(funcionGestionar.CodigoSala) || c.CodigoFuncion.Equals(txtCodigoFuncion.Text));

                    if (aux != null)
                    {
                        // Si pasa la validación...
                        if (validado(funcionGestionar))
                        {
                            // Buscamos la sala y la película.
                            Sala auxSal = unit.RepositorioSalas.GetOne(c => c.CodigoSala.Equals(txtCodSalaFuncion.Text));
                            Pelicula auxPel = unit.RepositorioPeliculas.GetOne(c => c.CodigoPelicula.Equals(txtCodPeliFuncion.Text));
                            // Si los encuentra...
                            if (auxSal != null && auxPel != null)
                            {
                                if (ComprobarCheckFuncion())
                                {
                                    // Modifica los datos en la base de datos.
                                    unit.RepositorioFunciones.Update(funcionGestionar);
                                    System.Windows.MessageBox.Show("Función modificada.");
                                    LimpiarCamposFuncion();
                                }
                            }
                            // Mensaje de que no se encontraron la sala o la película.
                            else { System.Windows.MessageBox.Show("El código de sala/película es erroneo."); }
                        }
                    }
                    else { System.Windows.MessageBox.Show("La función no se encuentra registrada por lo tanto no se puede modificar."); }
                }
                else { System.Windows.MessageBox.Show("El código de la función debe estar cubierto."); }
            }
            catch (Exception ex)
            {
                System.Windows.MessageBox.Show("Ha ocurrido un error. Pongase en contacto con el administrador.");
                Console.WriteLine("Error: " + ex.ToString());
            }
        }
        // Método que se ejecuta cuando le das al boton de eliminar(basura).
        private void btnEliminarFuncion_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (!String.IsNullOrEmpty(txtCodigoFuncion.Text))
                {
                    // Buscamos la función.
                    Funcion aux = unit.RepositorioFunciones.GetOne(c => c.CodigoFuncion.Equals(txtCodigoFuncion.Text));

                    // Si se encuentra...
                    if (aux != null)
                    {
                        // Si pasa la validación...
                        if (validado(funcionGestionar))
                        {
                            // Elimina los datos de la base de datos.
                            unit.RepositorioFunciones.Delete(funcionGestionar);
                            System.Windows.MessageBox.Show("Función eliminada.");
                            // Limpiamos los campos.
                            LimpiarCamposFuncion();
                        }
                    }
                    else { System.Windows.MessageBox.Show("La función no se encuentra registrada con ese código. No se puede eliminar."); }
                }
                else { System.Windows.MessageBox.Show("Establezca un código en el campo correspondiente."); }
            }
            catch (Exception ex)
            {
                System.Windows.MessageBox.Show("Ha ocurrido un error. Pongase en contacto con el administrador.");
                Console.WriteLine("Error: " + ex.ToString());
            }
        }
        // Método que se ejecuta al darle al botón de limpiar(papelera).
        private void btnLimpiarFuncion_Click(object sender, RoutedEventArgs e)
        {
            // Limpiamos los campos.
            LimpiarCamposFuncion();
        }

        // Método que limpia los campos de función.
        private void LimpiarCamposFuncion()
        {
            funcionGestionar = new Funcion();
            dckpFunciones.DataContext = funcionGestionar;
        }
        // Método que se ejecuta al darle al boton de abrir lista(listado).
        private void btnListaFunciones_Click(object sender, RoutedEventArgs e)
        {
            tipoListaLeer = "Funciones";
            Listas listas = new Listas();
            listas.Show();
        }

        // Método que comprueba que se pueda activar una función o no por la fecha de estreno de la película. (revisar)
        private void chckActivoFuncion_Checked(object sender, RoutedEventArgs e)
        {
            ComprobarCheckFuncion();
        }

        private bool ComprobarCheckFuncion()
        {
            bool log = false;
            bool nice = true;
            try
            {
                string mensaje = "";

                Pelicula auxPel = unit.RepositorioPeliculas.GetOne(c => c.CodigoPelicula.Equals(txtCodPeliFuncion.Text));

                if (auxPel != null)
                {
                    DateTime fechaIni;
                    DateTime fechaFin;

                    if (!String.IsNullOrEmpty(dtpkFechaInicioFuncion.Text) && !String.IsNullOrEmpty(dtpkFechaInicioFuncion.Text))
                    {
                        if (DateTime.TryParse(dtpkFechaInicioFuncion.Text, out fechaIni) && DateTime.TryParse(dtpkFechaFinFuncion.Text, out fechaFin))
                        {
                            if (Convert.ToDateTime(dtpkFechaInicioFuncion.Text) < Convert.ToDateTime(auxPel.fechaEstreno))
                            {
                                nice = false;
                                mensaje += "La fecha de inicio de funcion debe ser mayor o igual a la fecha de estreno de la película. \n";
                            }
                            if (Convert.ToDateTime(dtpkFechaInicioFuncion.Text) > Convert.ToDateTime(dtpkFechaFinFuncion.Text))
                            {
                                nice = false;
                                mensaje += "La fecha de inicio de funcion debe ser menor o igual a la de fecha fin. \n";
                            }
                            if (Convert.ToDateTime(dtpkFechaInicioFuncion.Text) > DateTime.Today)
                            {
                                nice = false;
                                mensaje += "La fecha de inicio de función aun no ha llegado, no puede activarla.";
                            }
                            if (DateTime.Today > Convert.ToDateTime(dtpkFechaFinFuncion.Text))
                            {
                                nice = false;
                                mensaje += "La fecha fin de la función ya ha pasado, modifique el estado.";

                            }

                            if (nice)
                            {
                                log = true;
                                chckActivoFuncion.IsChecked = true;
                                return log;
                            }
                            else
                            {
                                chckActivoFuncion.IsChecked = false;
                                System.Windows.MessageBox.Show(mensaje);
                                return log;
                            }
                        }
                        else
                        {
                            chckActivoFuncion.IsChecked = false;
                            System.Windows.MessageBox.Show("El formato de la fecha de inicio o la fecha de fin de la función son incorrectas.");
                        }
                    }
                    else
                    {
                        chckActivoFuncion.IsChecked = false;
                        System.Windows.MessageBox.Show("Debe de seleccionar la fecha de inicio y la fecha de fin de la función.");
                    }
                }
                else
                {
                    chckActivoFuncion.IsChecked = false;
                    System.Windows.MessageBox.Show("El código de la película es incorrecto.");
                }
                return log;
            }
            catch (Exception ex)
            {
                System.Windows.MessageBox.Show("Ha ocurrido un error. Pongase en contacto con el administrador.");
                Console.WriteLine("Error: " + ex.ToString());
                return log;
            }
        }
        #endregion
        // Región para la gestión de los salas. Añadir, modificar, eliminar, buscar, limpiar campos, abrir listado.
        #region comSalas
        // Método que se ejecuta al darle al botón de buscar (Lupa). 
        private void btnBuscarSala_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                // Comprobamos que los campos de busqueda estan cubiertos.
                if (!String.IsNullOrEmpty(cbxBuscarSala.Text) && !String.IsNullOrEmpty(txtBuscarSala.Text))
                {
                    // Buscamos por el campo correspondiente.
                    switch (cbxBuscarSala.Text)
                    {
                        case "Código":
                            salaGestionar = unit.RepositorioSalas.GetOne(c => c.CodigoSala.Equals(txtBuscarSala.Text));
                            break;
                        case "Número":
                            salaGestionar = unit.RepositorioSalas.GetOne(c => c.numero.Equals(txtBuscarSala.Text));
                            break;
                    }
                    // Si encuentra...
                    if (salaGestionar != null)
                    {
                        // Establecemos el contexto del dockpanel.
                        dckpSalas.DataContext = salaGestionar;
                    }
                    // Mensaje de que no encuentra la sala.
                    else
                    {
                        System.Windows.MessageBox.Show("La sala no se encuentra registrada.");
                        LimpiarCamposSala();
                    }
                }
                // Mensaje de que no estan rellenados los campos.
                else
                {
                    System.Windows.MessageBox.Show("Debe señalar el código/número de la sala desea buscar.");
                }
            }
            catch (Exception ex)
            {
                System.Windows.MessageBox.Show("Ha ocurrido un error. Pongase en contacto con el administrador.");
                Console.WriteLine("Error: " + ex.ToString());
            }
        }
        // Método que se ejecuta al darle al boton de añadir(disquete).
        private void btnAñadirSala_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                salaGestionar.CodigoSala = txtCodigoSala.Text;
                // Buscamos la sala.
                Sala aux = unit.RepositorioSalas.GetOne(c => c.CodigoSala.Equals(salaGestionar.CodigoSala) || c.numero.Equals(salaGestionar.numero));
                // Si no encuentra...
                if (aux == null)
                {
                    int num;
                    int numFilas;
                    int numColumnas;
                    // Comprobamos que los campos de filas y columnas se pueden parsear a Int.
                    if (Int32.TryParse(txtNumeroFilas.Text, out numFilas) && Int32.TryParse(txtNumeroColumnas.Text, out numColumnas))
                    {
                        if (Int32.TryParse(txtNumeroSala.Text, out num))
                        {
                            // Si está validado...
                            if (validado(salaGestionar))
                            {
                                // Añade los datos a la base de datos.
                                unit.RepositorioSalas.Add(salaGestionar);
                                System.Windows.MessageBox.Show("Sala registrada.");
                                // Limpiamos los campos.
                                LimpiarCamposSala();
                            }
                        }
                        else { System.Windows.MessageBox.Show("El número de sala es un número entero."); }
                    }
                    // Mensaje de que el tipo introducido en los campos no es el correcto.
                    else
                    {
                        System.Windows.MessageBox.Show("El número de columnas y filas es un número entero.");
                        return;
                    }
                }
                // Mensaje de que la sala está registrada ya.
                else { System.Windows.MessageBox.Show("La sala ya está registrada."); }
            }
            catch (Exception ex)
            {
                System.Windows.MessageBox.Show("Ha ocurrido un error. Pongase en contacto con el administrador.");
                Console.WriteLine("Error: " + ex.ToString());
            }
        }
        // Método que se ejecuta cuando le das al botón de editar(lápiz).
        private void btnModificarSala_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (!String.IsNullOrEmpty(txtCodigoSala.Text))
                {
                    Sala aux1 = unit.RepositorioSalas.GetOne(c => c.CodigoSala.Equals(txtCodigoSala.Text));
                    if (aux1 != null)
                    {
                        // Buscamos la sala.
                        Sala aux = unit.RepositorioSalas.GetOne(c => c.numero.Equals(salaGestionar.numero));
                        // Si no encuentra...
                        if (aux == null || aux.numero == salaGestionar.numero)
                        {
                            int num;
                            int numFilas;
                            int numColumnas;
                            // Comprobamos que los campos de filas y columnas se puedan parsear a Int.
                            if (Int32.TryParse(txtNumeroFilas.Text, out numFilas) && Int32.TryParse(txtNumeroColumnas.Text, out numColumnas) && Int32.TryParse(txtNumeroSala.Text, out num))
                            {
                                if (Int32.TryParse(txtNumeroSala.Text, out num))
                                {
                                    // Si pasa la validación...
                                    if (validado(salaGestionar))
                                    {
                                        // Modifica los datos en la base de datos.
                                        unit.RepositorioSalas.Update(salaGestionar);
                                        System.Windows.MessageBox.Show("Sala modificada.");
                                        LimpiarCamposSala();
                                    }
                                }
                                else { System.Windows.MessageBox.Show("El número de sala es un número entero."); }
                            }
                            // Mensaje de que el tipo introducido en los campos no es el correcto.
                            else
                            {
                                System.Windows.MessageBox.Show("El número de columnas y filas es entero.");
                                return;
                            }
                        }
                        else { System.Windows.MessageBox.Show("La sala ya está registrada con ese número."); }
                    }
                    else { System.Windows.MessageBox.Show("No hay una sala registrada con ese código. No se puede modificar."); }
                }
                else { System.Windows.MessageBox.Show("Establezca un código en el campo correspondiente."); }
            }
            catch (Exception ex)
            {
                System.Windows.MessageBox.Show("Ha ocurrido un error. Pongase en contacto con el administrador.");
                Console.WriteLine("Error: " + ex.ToString());
            }
        }
        // Método que se ejecuta cuando le das al boton de eliminar(basura).
        private void btnEliminarSala_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (!String.IsNullOrEmpty(txtCodigoSala.Text))
                {
                    // Buscamos la sala.
                    Sala aux = unit.RepositorioSalas.GetOne(c => c.CodigoSala.Equals(txtCodigoSala.Text));
                    // Si se encuentra...
                    if (aux != null)
                    {
                        // Si pasa la validación...
                        if (validado(salaGestionar))
                        {
                            // Eliminamos los datos de la base de datos.
                            unit.RepositorioSalas.Delete(salaGestionar);
                            System.Windows.MessageBox.Show("Sala eliminada.");
                            //Limpiamos los campos.
                            LimpiarCamposSala();
                        }
                    }
                    else { System.Windows.MessageBox.Show("La sala no se encuentra registrada con ese código. No se puede eliminar."); }
                }
                else { System.Windows.MessageBox.Show("Establezca un codigo en la sala correspondiente."); }
            }
            catch (Exception ex)
            {
                System.Windows.MessageBox.Show("Ha ocurrido un error. Pongase en contacto con el administrador.");
                Console.WriteLine("Error: " + ex.ToString());
            }
        }
        // Método que se ejecuta al darle al botón de limpiar(papelera).
        private void btnLimpiarSala_Click(object sender, RoutedEventArgs e)
        {
            // Limpiamos los campos.
            LimpiarCamposSala();
        }

        // Método para limpiar los campos de sala.
        private void LimpiarCamposSala()
        {
            salaGestionar = new Sala();
            dckpSalas.DataContext = salaGestionar;
        }
        // Método que se ejecuta al darle al boton de abrir lista(listado).
        private void btnListaSalas_Click(object sender, RoutedEventArgs e)
        {
            tipoListaLeer = "Salas";
            Listas listas = new Listas();
            listas.Show();
        }
        #endregion
        // Región para la gestión de los horarios. Añadir, modificar, eliminar, buscar, limpiar campos, abrir listado.
        #region comHorarios
        // Método que se ejecuta al darle al botón de buscar (Lupa). 
        private void btnBuscarHorario_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                // Comprobamos si los campos de busqueda no están vacios.
                if (!String.IsNullOrEmpty(cbxBuscarHorario.Text) && !String.IsNullOrEmpty(txtBuscarHorario.Text))
                {
                    string dia = cbxBuscarHorario.Text;
                    // Buscamos por el campo correspondiente.
                    horarioGestionar = unit.RepositorioHorarios.GetOne(c => c.DiaSemana.Equals(dia)
                        && c.CodigoFuncion.Equals(txtBuscarHorario.Text));
                    // Si encuentra...
                    if (horarioGestionar != null)
                    {
                        // Establecemos el contexto del dockPanel.
                        dckpHorarios.DataContext = horarioGestionar;
                    }
                    // Mensaje de que no se encuentra el horario.
                    else
                    {
                        LimpiarCamposHorario();
                        System.Windows.MessageBox.Show("El horario no se encuentra registrado.");
                    }
                }
                // Mensaje de que los campos no estan cubiertos.
                else
                {
                    System.Windows.MessageBox.Show("Debe señalar el código de la funcion del horario que desea buscar.");
                }
            }
            catch (Exception ex)
            {
                System.Windows.MessageBox.Show("Ha ocurrido un error. Pongase en contacto con el administrador.");
                Console.WriteLine("Error: " + ex.ToString());
            }
        }
        // Método que se ejecuta al darle al boton de añadir(disquete).
        private void btnAñadirHorario_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                // Establecemos el día y el código directamente.
                horarioGestionar.DiaSemana = cbxDiasSemanaHorario.Text;
                horarioGestionar.CodigoFuncion = txtCodigoFuncionHorario.Text;
                // Buscamos el horario.
                Horario aux = unit.RepositorioHorarios.GetOne(c => c.DiaSemana.Equals(horarioGestionar.DiaSemana) && c.CodigoFuncion.Equals(horarioGestionar.CodigoFuncion));
                // Si no encuentra...
                if (aux == null)
                {
                    // Si pasa las validaciones...
                    if (validado(horarioGestionar))
                    {
                        Funcion auxFun = unit.RepositorioFunciones.GetOne(c => c.CodigoFuncion.Equals(txtCodigoFuncionHorario.Text));
                        DateTime horaInicio;
                        DateTime horaFin;
                        // Comprobamos que los datos de las horas se pueden parsear correctamente
                        if (DateTime.TryParse(txtHoraInicioHorario.Text, out horaInicio) && DateTime.TryParse(txtHoraFinHorario.Text, out horaFin))
                        {
                            // Comprobamos si la funcion que se asigna es correcta.
                            if (auxFun != null)
                            {
                                // Añade los datos a la base de datos.
                                unit.RepositorioHorarios.Add(horarioGestionar);
                                System.Windows.MessageBox.Show("Horario registrado.");

                                //Limpiamos los campos.
                                LimpiarCamposHorario();
                            }
                            // Mensaje de que la funcion no se encontró.
                            else { System.Windows.MessageBox.Show("El código de función no es correcto."); }
                        }
                        // Mensaje de que el formato de las horas no es correcto.
                        else { System.Windows.MessageBox.Show("El formato de hora inicio/fin no es correcto. Debe ser HH:mm."); }
                    }
                }
                // Mensaje de que el horario ya está registrado.
                else { System.Windows.MessageBox.Show("El horario ya está registrado."); }
            }
            catch (Exception ex)
            {
                System.Windows.MessageBox.Show("Ha ocurrido un error. Pongase en contacto con el administrador.");
                Console.WriteLine("Error: " + ex.ToString());
            }
        }
        // Método que se ejecuta cuando le das al botón de editar(lápiz).
        private void btnModificarHorario_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (!String.IsNullOrEmpty(cbxDiasSemanaHorario.Text) || !String.IsNullOrEmpty(txtCodigoFuncionHorario.Text))
                {
                    // Buscamos el horario.
                    Horario aux = unit.RepositorioHorarios.GetOne(c => c.DiaSemana.Equals(cbxDiasSemanaHorario.Text) && c.CodigoFuncion.Equals(txtCodigoFuncionHorario.Text));
                    // Si se encuentra...
                    if (aux != null)
                    {
                        // Si pasa la validación...
                        if (validado(horarioGestionar))
                        {
                            // Modificamos los datos en la base de datos.
                            unit.RepositorioHorarios.Update(horarioGestionar);
                            System.Windows.MessageBox.Show("Horario modificado.");
                            LimpiarCamposHorario();
                        }
                    }
                    else { System.Windows.MessageBox.Show("El horario no se encuentra registrado con ese día y esa función. No se puede modificar."); }
                }
                else { System.Windows.MessageBox.Show("Establezca un día y un código de función en los campos correspondientes."); }
            }
            catch (Exception ex)
            {
                System.Windows.MessageBox.Show("Ha ocurrido un error. Pongase en contacto con el administrador.");
                Console.WriteLine("Error: " + ex.ToString());
            }
        }
        // Método que se ejecuta cuando le das al boton de eliminar(basura).
        private void btnEliminarHorario_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (!String.IsNullOrEmpty(cbxDiasSemanaHorario.Text) || !String.IsNullOrEmpty(txtCodigoFuncionHorario.Text))
                {
                    // Buscamos el horario.
                    Horario aux = unit.RepositorioHorarios.GetOne(c => c.DiaSemana.Equals(cbxDiasSemanaHorario.Text) && c.CodigoFuncion.Equals(txtCodigoFuncionHorario.Text));
                    // Si se encuentra...
                    if (aux != null)
                    {
                        // Si pasa la validación...
                        if (validado(horarioGestionar))
                        {
                            // Eliminamos los datos de la base de datos.
                            unit.RepositorioHorarios.Delete(horarioGestionar);
                            System.Windows.MessageBox.Show("Horario eliminado.");

                            //Limpiamos los campos.
                            LimpiarCamposHorario();
                        }
                    }
                    else { System.Windows.MessageBox.Show("El horario no se encuentra registrado con ese día y esa función. No se puede eliminar."); }
                }
                else { System.Windows.MessageBox.Show("Establezca un día y un código de función en los campos correspondientes."); }
            }
            catch (Exception ex)
            {
                System.Windows.MessageBox.Show("Ha ocurrido un error. Pongase en contacto con el administrador.");
                Console.WriteLine("Error: " + ex.ToString());
            }
        }
        // Método que se ejecuta al darle al botón de limpiar(papelera).
        private void btnLimpiarHorario_Click(object sender, RoutedEventArgs e)
        {
            //Limpiamos los campos.
            LimpiarCamposHorario();
        }
        // Método para limpiar los campos de horario.
        private void LimpiarCamposHorario()
        {
            horarioGestionar = new Horario();
            dckpHorarios.DataContext = horarioGestionar;
        }

        // Método que se ejecuta al darle al boton de abrir lista(listado).
        private void btnListaHorarios_Click(object sender, RoutedEventArgs e)
        {
            tipoListaLeer = "Horarios";
            Listas listas = new Listas();
            listas.Show();
        }
        #endregion
        // Región para la gestión de los películas. Añadir, añadir imagen, modificar, eliminar, buscar, limpiar campos, abrir listado, comprobar CheckBox.
        #region comPeliculas
        // Método que se ejecuta al darle al botón de buscar (Lupa). 
        private void btnBuscarPelicula_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                // Comprobamos que los campos de busqueda no están vacios.
                if (!String.IsNullOrEmpty(cbxBuscarPelicula.Text) && !String.IsNullOrEmpty(txtBuscarPelicula.Text))
                {
                    // Buscamos por el campo correspondiente.
                    switch (cbxBuscarPelicula.Text)
                    {
                        case "Código":
                            peliculaGestionar = unit.RepositorioPeliculas.GetOne(c => c.CodigoPelicula.Equals(txtBuscarPelicula.Text));
                            break;
                        case "Nombre":
                            peliculaGestionar = unit.RepositorioPeliculas.GetOne(c => c.nombre.Equals(txtBuscarPelicula.Text));
                            break;
                    }
                    // Si encuetra...
                    if (peliculaGestionar != null)
                    {
                        //Establecemos el contexto del dockPanel.
                        dckpPeliculas.DataContext = peliculaGestionar;
                    }
                    // Mensaje de que no se encuentra la película
                    else
                    {
                        System.Windows.MessageBox.Show("La película no se encuentra registrada.");
                        LimpiarCamposPelicula();
                    }
                }
                // Mensaje de que los campos no estan cubiertos.
                else
                {
                    System.Windows.MessageBox.Show("Debe señalar el código/nombre de la película desea buscar.");
                }
            }
            catch (Exception ex)
            {
                System.Windows.MessageBox.Show("Ha ocurrido un error. Pongase en contacto con el administrador.");
                Console.WriteLine("Error: " + ex.ToString());
            }
        }
        // Método que añade una imagen al darle al botón de imagen (picture).
        private void btnImagenPelicula_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                // Variable para comprobar si copiamos la imagen a la carpeta que necesitamos.
                copiado = false;
                // Abrimos el buscador.
                OpenFileDialog browser = new OpenFileDialog();
                // Instanciamos el directorio de inicio y los archivos que busca.
                browser.InitialDirectory = "c:\\";
                browser.Filter = "txt files (*.txt)|*.txt|All files (*.*)|*.*";
                browser.FilterIndex = 2;
                browser.RestoreDirectory = true;
                // Lo mostramos.
                DialogResult res = browser.ShowDialog();
                // Directorio de la imagen seleccionada.
                string directorioImagen = browser.FileName;
                // Directorio donde se van a guardar las imagenes (bin)
                string direccionImagenes = System.IO.Path.Combine(Environment.CurrentDirectory);

                direccionImagenes = System.IO.Path.Combine(direccionImagenes, "imagenesPeliculas");

                FileInfo f = new FileInfo(directorioImagen);
                // Directorio completo de la imagen al ser copiada.
                directorioCompleto = System.IO.Path.Combine(direccionImagenes, f.Name);
                try
                {
                    // Si no existe la copiamos.
                    if (!File.Exists(directorioCompleto))
                        File.Copy(f.FullName, directorioCompleto);
                    copiado = true;

                    // Establecemos la imagen en el pictureBox
                    peliculaGestionar.imagen = directorioCompleto;
                    imgPelicula.Source = new BitmapImage(new Uri(directorioCompleto));
                }
                catch (Exception ex)
                {
                    //Mensaje de que la imagen ya existe.
                    System.Windows.MessageBox.Show("El fichero " + f.Name + " ya existe");
                    Console.WriteLine("Warning: " + ex.ToString());
                }
            }
            catch (Exception ex) { Console.WriteLine("Warning: " + ex.ToString()); }
        }
        // Método que se ejecuta al darle al boton de añadir(disquete).
        private void btnAñadirPelicula_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                // Establecemos directamente el codigo de la película
                peliculaGestionar.CodigoPelicula = txtCodigoPelicula.Text;
                // Buscamos la película.
                Pelicula aux = unit.RepositorioPeliculas.GetOne(c => c.CodigoPelicula.Equals(peliculaGestionar.CodigoPelicula));
                // Si  no la encuentra...
                if (aux == null)
                {
                    // Si pasa las validaciones...
                    if (validado(peliculaGestionar))
                    {
                        // Buscamos si existe la productora y la categoria de la película.
                        Productora auxPro = unit.RepositorioProductoras.GetOne(c => c.CodigoProductora.Equals(txtCodigoProductoraPelicula.Text));
                        CategoriaPelicula auxCat = unit.RepositorioCategorias.GetOne(c => c.CodigoCategoriaPelicula.Equals(txtCodigoCategoríaPelicula.Text));
                        DateTime duracion;
                        // Comprobamos si la duracion se puede parsear a datetime.
                        if (DateTime.TryParse(txtDuracionPelicula.Text, out duracion))
                        {
                            // Si se encuentran la productora y la categoría...
                            if (auxPro != null && auxCat != null)
                            {
                                // Añadimos los datos a la base de datos.
                                unit.RepositorioPeliculas.Add(peliculaGestionar);
                                System.Windows.MessageBox.Show("Película registrada.");
                                //Limpiamos los campos.
                                LimpiarCamposPelicula();
                            }
                            // Mensaje de que la productora y la categoría no se encontraron.
                            else { System.Windows.MessageBox.Show("El código de productora/categoría no es el correcto."); }
                        }
                        // Mensaje de que el formato de la hora no es el correcto.
                        else { System.Windows.MessageBox.Show("El formato de la duración no es correcto.Debe ser HH:mm."); }
                    }
                }
                // Mensaje de que la película ya esta registrada.
                else { System.Windows.MessageBox.Show("La película ya está registrada."); }
            }
            catch (Exception ex)
            {
                System.Windows.MessageBox.Show("Ha ocurrido un error. Pongase en contacto con el administrador.");
                Console.WriteLine("Error: " + ex.ToString());
            }
        }
        // Método que se ejecuta cuando le das al botón de editar(lápiz).
        private void btnModificarPelicula_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (!String.IsNullOrEmpty(txtCodigoPelicula.Text))
                {
                    // Buscamos la película.
                    Pelicula aux = unit.RepositorioPeliculas.GetOne(c => c.CodigoPelicula.Equals(txtCodigoPelicula.Text));
                    // Si  no la encuentra...
                    if (aux != null)
                    {
                        // Si pasa las validaciones...
                        if (validado(peliculaGestionar))
                        {
                            // Buscamos la productora y la categoría de la película.
                            Productora auxPro = unit.RepositorioProductoras.GetOne(c => c.CodigoProductora.Equals(txtCodigoProductoraPelicula.Text));
                            CategoriaPelicula auxCat = unit.RepositorioCategorias.GetOne(c => c.CodigoCategoriaPelicula.Equals(txtCodigoCategoríaPelicula.Text));
                            // Si las encuentra...
                            if (auxPro != null && auxPro != null)
                            {
                                if (ComprobarCheckPelicula())
                                {
                                    // Modificamos los datos en la base de datos.
                                    unit.RepositorioPeliculas.Update(peliculaGestionar);
                                    System.Windows.MessageBox.Show("Película modificada.");
                                    LimpiarCamposPelicula();
                                }
                            }
                            // Mensaje de que la productora y la categoría no se encontraron.
                            else { System.Windows.MessageBox.Show("El código de productora/categoría no es el correcto."); }
                        }
                    }
                    else { System.Windows.MessageBox.Show("La película no se encuentra registrada con ese código. No se puede modificar."); }
                }
                else { System.Windows.MessageBox.Show("Establezca un código en el campo correspondiente."); }
            }
            catch (Exception ex)
            {
                System.Windows.MessageBox.Show("Ha ocurrido un error. Pongase en contacto con el administrador.");
                Console.WriteLine("Error: " + ex.ToString());
            }
        }
        // Método que se ejecuta cuando le das al boton de eliminar(basura).
        private void btnEliminarPelicula_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (!String.IsNullOrEmpty(txtCodigoPelicula.Text))
                {
                    // Buscamos la película.
                    Pelicula aux = unit.RepositorioPeliculas.GetOne(c => c.CodigoPelicula.Equals(txtCodigoPelicula.Text));
                    // Si  no la encuentra...
                    if (aux != null)
                    {
                        // Si pasa las validaciones...
                        if (validado(peliculaGestionar))
                        {
                            // Eliminamos los datos de la base de datos.
                            unit.RepositorioPeliculas.Delete(peliculaGestionar);
                            System.Windows.MessageBox.Show("Película eliminada.");
                            //Limpiamos los campos.
                            LimpiarCamposPelicula();
                        }
                    }
                    else { System.Windows.MessageBox.Show("La película no se encuentra registrada con ese código. No se puede eliminar."); }
                }
                else { System.Windows.MessageBox.Show("Establezca un código en el campo correspondiente."); }
            }
            catch (Exception ex)
            {
                System.Windows.MessageBox.Show("Ha ocurrido un error. Pongase en contacto con el administrador.");
                Console.WriteLine("Error: " + ex.ToString());
            }
        }
        // Método que se ejecuta al darle al boton de abrir lista(listado).
        private void btnListaPelicula_Click(object sender, RoutedEventArgs e)
        {
            tipoListaLeer = "Peliculas";
            Listas listas = new Listas();
            listas.Show();
        }
        // Método que se ejecuta al darle al botón de limpiar(papelera).
        private void btnLimpiarPelicula_Click(object sender, RoutedEventArgs e)
        {
            //Limpiamos los campos.
            LimpiarCamposPelicula();
        }
        // Método para limpiar los campos de película.
        private void LimpiarCamposPelicula()
        {
            peliculaGestionar = new Pelicula();
            dckpPeliculas.DataContext = peliculaGestionar;
            dtpkFechaEstrenoPelicula.Text = "";
        }

        // Método que comprueba si la película puede estar en cartelera o no. (revisar)
        private void chckEnCarteleraPelicula_Checked(object sender, RoutedEventArgs e)
        {
            ComprobarCheckPelicula();
        }

        private bool ComprobarCheckPelicula()
        {
            bool log = false;

            try
            {
                if (!String.IsNullOrEmpty(dtpkFechaEstrenoPelicula.Text))
                {
                    DateTime fechaEstreno;
                    if (DateTime.TryParse(dtpkFechaEstrenoPelicula.Text, out fechaEstreno))
                    {
                        if (Convert.ToDateTime(dtpkFechaEstrenoPelicula.Text) <= DateTime.Today)
                        {
                            chckEnCarteleraPelicula.IsChecked = true;
                            return log = true;
                        }
                        else
                        {
                            System.Windows.MessageBox.Show("La fecha de inicio de estreno debe ser menor o igual a la de hoy para poder estar en cartelera.");
                            chckEnCarteleraPelicula.IsChecked = false;
                            return log = false;
                        }
                    }
                    else
                    {
                        System.Windows.MessageBox.Show("La fecha de inicio de estreno debe tener el formato correcto.");
                        chckEnCarteleraPelicula.IsChecked = false;
                    }
                }
                else
                {
                    System.Windows.MessageBox.Show("Debe tener una fecha seleccionada.");
                    chckEnCarteleraPelicula.IsChecked = false;
                }

                return log;
            }
            catch (Exception ex)
            {
                System.Windows.MessageBox.Show("Ha ocurrido un error. Pongase en contacto con el administrador.");
                Console.WriteLine("Error: " + ex.ToString());
                return log;
            }
        }

        // Método que se ejecuta al darle al boton de recaudación(moneda)
        private void btnRecaudaciónPelicula_Click(object sender, RoutedEventArgs e)
        {
            if (validado(peliculaGestionar))
            {
                Recaudación recaudación = new Recaudación(peliculaGestionar.CodigoPelicula);
                recaudación.Show();
            }
        }

        #endregion
        // Región para la gestión de los productoras. Añadir, modificar, eliminar, buscar, limpiar campos, abrir listado.
        #region comProducturas
        // Método que se ejecuta al darle al botón de buscar (Lupa). 
        private void btnBuscarProductora_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                // Comprobamos que los campos no esten vacios.
                if (!String.IsNullOrEmpty(cbxBuscarProductora.Text) && !String.IsNullOrEmpty(txtBuscarProductora.Text))
                {
                    // Buscamos por el campo correspondiente.
                    switch (cbxBuscarProductora.Text)
                    {
                        case "Código":
                            productoraGestionar = unit.RepositorioProductoras.GetOne(c => c.CodigoProductora.Equals(txtBuscarProductora.Text));
                            break;
                        case "Nombre":
                            productoraGestionar = unit.RepositorioProductoras.GetOne(c => c.nombre.Equals(txtBuscarProductora.Text));
                            break;
                    }
                    // Si lo encuentra...
                    if (peliculaGestionar != null)
                    {
                        // Establecemos el contexto del dockPanel.
                        dckpProductoras.DataContext = productoraGestionar;
                    }
                    // Mensaje de que no la encuentra.
                    else
                    {
                        System.Windows.MessageBox.Show("La productora no se encuentra registrada.");
                        LimpiarCamposProductora();
                    }
                }
                // Mensaje de que los campos no estan cubiertos.
                else
                {
                    System.Windows.MessageBox.Show("Debe señalar el código/nombre de la productora desea buscar.");
                }
            }
            catch (Exception ex)
            {
                System.Windows.MessageBox.Show("Ha ocurrido un error. Pongase en contacto con el administrador.");
                Console.WriteLine("Error: " + ex.ToString());
            }
        }
        // Método que se ejecuta al darle al boton de añadir(disquete).
        private void btnAñadirProductora_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                //Establecemos directamente el código.
                productoraGestionar.CodigoProductora = txtCodigoProductora.Text;
                // Buscamos si la productora esta guardada.
                Productora aux = unit.RepositorioProductoras.GetOne(c => c.CodigoProductora.Equals(productoraGestionar.CodigoProductora) || c.nombre.Equals(productoraGestionar.nombre));
                // Si no lo está...
                if (aux == null)
                {
                    // Si pasa la validación...
                    if (validado(productoraGestionar))
                    {
                        // Añadimos los datos a la base de datos.
                        unit.RepositorioProductoras.Add(productoraGestionar);
                        System.Windows.MessageBox.Show("Productora registrada.");
                        // Limpiamos los campos.
                        LimpiarCamposProductora();
                    }
                }
                // Mensaje de que la productora ya esta registrada.
                else { System.Windows.MessageBox.Show("La productora ya está registrada."); }
            }
            catch (Exception ex)
            {
                System.Windows.MessageBox.Show("Ha ocurrido un error. Pongase en contacto con el administrador.");
                Console.WriteLine("Error: " + ex.ToString());
            }
        }
        // Método que se ejecuta cuando le das al botón de editar(lápiz).
        private void btnModificarProductora_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (!String.IsNullOrEmpty(txtCodigoProductora.Text))
                {
                    // Buscamos si la productora esta guardada.
                    Productora aux = unit.RepositorioProductoras.GetOne(c => c.CodigoProductora.Equals(txtCodigoProductora.Text));
                    // Si se encuentra...
                    if (aux != null)
                    {
                        // Si pasa la validación...
                        if (validado(productoraGestionar))
                        {
                            // Modificamos los datos de la base de datos.
                            unit.RepositorioProductoras.Update(productoraGestionar);
                            System.Windows.MessageBox.Show("Productora modificada.");
                            LimpiarCamposProductora();
                        }
                    }
                    else { System.Windows.MessageBox.Show("La productora no se encuentra registrada con ese código. No se puede modificar."); }
                }
                else { System.Windows.MessageBox.Show("Establezca un código en el campo correspondiente."); }
            }
            catch (Exception ex)
            {
                System.Windows.MessageBox.Show("Ha ocurrido un error. Pongase en contacto con el administrador.");
                Console.WriteLine("Error: " + ex.ToString());
            }
        }
        // Método que se ejecuta cuando le das al boton de eliminar(basura).
        private void btnEliminarProductora_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (!String.IsNullOrEmpty(txtCodigoProductora.Text))
                {
                    // Buscamos si la productora esta guardada.
                    Productora aux = unit.RepositorioProductoras.GetOne(c => c.CodigoProductora.Equals(txtCodigoProductora.Text));
                    // Si se encuentra...
                    if (aux != null)
                    {
                        // Si pasa la validación...
                        if (validado(productoraGestionar))
                        {
                            // Eliminamos los datos de la base de datos.
                            unit.RepositorioProductoras.Delete(productoraGestionar);
                            System.Windows.MessageBox.Show("Productora eliminada.");
                            // Limpiamos los campos.
                            LimpiarCamposProductora();
                        }
                    }
                    else { System.Windows.MessageBox.Show("La productora no se encuentra registrada con ese código. No se puede eliminar."); }
                }
                else { System.Windows.MessageBox.Show("Establezca un código en el campo correspondiente."); }
            }
            catch (Exception ex)
            {
                System.Windows.MessageBox.Show("Ha ocurrido un error. Pongase en contacto con el administrador.");
                Console.WriteLine("Error: " + ex.ToString());
            }
        }
        // Método que se ejecuta al darle al boton de abrir lista(listado).
        private void btnListaProductoras_Click(object sender, RoutedEventArgs e)
        {
            tipoListaLeer = "Productoras";
            Listas listas = new Listas();
            listas.Show();
        }
        // Método que se ejecuta al darle al botón de limpiar(papelera).
        private void btnLimpiarProductora_Click(object sender, RoutedEventArgs e)
        {
            // Limpiamos los campos.
            LimpiarCamposProductora();
        }
        // Método para limpiar los campos de productora.
        private void LimpiarCamposProductora()
        {
            productoraGestionar = new Productora();
            dckpProductoras.DataContext = productoraGestionar;
        }
        #endregion
        // Región para la gestión de las categorías de película. Añadir, modificar, eliminar, buscar, limpiar campos, abrir listado.
        #region comCategoriasPeliculas
        // Método que se ejecuta al darle al botón de buscar (Lupa). 
        private void btnBuscarCategoria_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                // Comprobamos si los campos no están vacios.
                if (!String.IsNullOrEmpty(cbxBuscarCategorias.Text) && !String.IsNullOrEmpty(txtBuscarCategoria.Text))
                {
                    // Buscamos por el campo correspondiente.
                    switch (cbxBuscarCategorias.Text)
                    {
                        case "Código":
                            categoriaPeliculaGestionar = unit.RepositorioCategorias.GetOne(c => c.CodigoCategoriaPelicula.Equals(txtBuscarCategoria.Text));
                            break;
                        case "Nombre":
                            categoriaPeliculaGestionar = unit.RepositorioCategorias.GetOne(c => c.nombre.Equals(txtBuscarCategoria.Text));
                            break;
                    }
                    // Si encuentra...
                    if (categoriaPeliculaGestionar != null)
                    {
                        // Establecemos el contexto del dockPanel.
                        dckpCategorias.DataContext = categoriaPeliculaGestionar;
                    }
                    // Mensaje de que no se ha encontrado la categoría.
                    else
                    {
                        System.Windows.MessageBox.Show("La categoría no se encuentra registrada.");
                        LimpiarCamposCategoríaPelícula();
                    }
                }
                // Mensaje de que los campos de busqueda no estan cubiertos.
                else
                {
                    System.Windows.MessageBox.Show("Debe señalar el código/nombre de la categoría desea buscar.");
                }
            }
            catch (Exception ex)
            {
                System.Windows.MessageBox.Show("Ha ocurrido un error. Pongase en contacto con el administrador.");
                Console.WriteLine("Error: " + ex.ToString());
            }
        }
        // Método que se ejecuta al darle al boton de añadir(disquete).
        private void btnAñadirCategoria_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                // Establecemos el código directamente.
                categoriaPeliculaGestionar.CodigoCategoriaPelicula = txtCodigoCategoria.Text;
                // Buscamos si ya está registrada.
                CategoriaPelicula aux = unit.RepositorioCategorias.GetOne(c => c.CodigoCategoriaPelicula.Equals(categoriaPeliculaGestionar.CodigoCategoriaPelicula) || c.nombre.Equals(categoriaPeliculaGestionar.nombre));
                // Si no lo está...
                if (aux == null)
                {
                    // Si pasa la validación...
                    if (validado(categoriaPeliculaGestionar))
                    {
                        // Añadimos los datos a la base de datos.
                        unit.RepositorioCategorias.Add(categoriaPeliculaGestionar);
                        System.Windows.MessageBox.Show("Categoría registrada.");
                        // Limpiamos los campos.
                        LimpiarCamposCategoríaPelícula();
                    }
                }
                // Mensaje de que la categoría ya está registrada.
                else { System.Windows.MessageBox.Show("La categoría ya está registrada."); }
            }
            catch (Exception ex)
            {
                System.Windows.MessageBox.Show("Ha ocurrido un error. Pongase en contacto con el administrador.");
                Console.WriteLine("Error: " + ex.ToString());
            }
        }
        // Método que se ejecuta cuando le das al botón de editar(lápiz).
        private void btnModificarCategoria_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (!String.IsNullOrEmpty(txtCodigoCategoria.Text))
                {
                    // Buscamos si ya está registrada.
                    CategoriaPelicula aux = unit.RepositorioCategorias.GetOne(c => c.CodigoCategoriaPelicula.Equals(txtCodigoCategoria.Text));
                    // Si lo está...
                    if (aux != null)
                    {
                        // Si pasa la validación...
                        if (validado(categoriaPeliculaGestionar))
                        {
                            // Modificamos los datos en la base de datos...
                            unit.RepositorioCategorias.Update(categoriaPeliculaGestionar);
                            System.Windows.MessageBox.Show("Categoría modificada.");
                            LimpiarCamposCategoríaPelícula();
                        }

                    }
                    else { System.Windows.MessageBox.Show("La categoría no se encuentra registrada con ese código. No se puede modificar."); }
                }
                else { System.Windows.MessageBox.Show("Establezca un código en el campo correspondiente."); }
            }
            catch (Exception ex)
            {
                System.Windows.MessageBox.Show("Ha ocurrido un error. Pongase en contacto con el administrador.");
                Console.WriteLine("Error: " + ex.ToString());
            }
        }
        // Método que se ejecuta cuando le das al boton de eliminar(basura).
        private void btnEliminarCategoria_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (!String.IsNullOrEmpty(txtCodigoCategoria.Text))
                {
                    // Buscamos si ya está registrada.
                    CategoriaPelicula aux = unit.RepositorioCategorias.GetOne(c => c.CodigoCategoriaPelicula.Equals(txtCodigoCategoria.Text));
                    // Si lo está...
                    if (aux != null)
                    {
                        // Si pasa la validación...
                        if (validado(categoriaPeliculaGestionar))
                        {
                            // Eliminamos los datos de la base de datos.
                            unit.RepositorioCategorias.Delete(categoriaPeliculaGestionar);
                            System.Windows.MessageBox.Show("Categoría eliminada.");
                            // Limpiamos los campos.
                            LimpiarCamposCategoríaPelícula();
                        }
                    }
                    else { System.Windows.MessageBox.Show("La categoría no se encuentra registrada con ese código. No se puede eliminar."); }
                }
                else { System.Windows.MessageBox.Show("Establezca un código en el campo correspondiente."); }
            }
            catch (Exception ex)
            {
                System.Windows.MessageBox.Show("Ha ocurrido un error. Pongase en contacto con el administrador.");
                Console.WriteLine("Error: " + ex.ToString());
            }
        }
        // Método que se ejecuta al darle al boton de abrir lista(listado).
        private void btnListaCategorias_Click(object sender, RoutedEventArgs e)
        {
            tipoListaLeer = "Categorias";
            Listas listas = new Listas();
            listas.Show();
        }
        // Método que se ejecuta al darle al botón de limpiar(papelera).
        private void btnLimpiarCategoria_Click(object sender, RoutedEventArgs e)
        {
            // Limpiamos los campos.
            LimpiarCamposCategoríaPelícula();
        }

        //Método que limpia los campos de categoría película
        private void LimpiarCamposCategoríaPelícula()
        {
            categoriaPeliculaGestionar = new CategoriaPelicula();
            dckpCategorias.DataContext = categoriaPeliculaGestionar;
        }
        #endregion
        // Región para la gestión del TPVEntradas.
        #region comTPVEntradas
        // Método que mueve una foto anterior (flecha izquierda)
        private void btnAnteriorPeliculaTPV_Click(object sender, RoutedEventArgs e)
        {
			try
			{
				if (peliculaSeleccionadaIndex > 0)
				{
					peliculaSeleccionadaIndex--;
					GestionarPeliculaTPV();
				}
			}
			catch (Exception ex)
			{
				System.Windows.MessageBox.Show("Ha ocurrido un error. Pongase en contacto con el administrador.");
				Console.WriteLine("Error: " + ex.ToString());
			}
		}
        // Método que mueve una foto posterior (flecha izquierda)
        private void btnSiguientePeliculaTPV_Click(object sender, RoutedEventArgs e)
        {
			try
			{
				if (peliculaSeleccionadaIndex < peliculasCartelera.Count - 1)
				{
					peliculaSeleccionadaIndex++;
					GestionarPeliculaTPV();
				}
			}
			catch (Exception ex)
			{
				System.Windows.MessageBox.Show("Ha ocurrido un error. Pongase en contacto con el administrador.");
				Console.WriteLine("Error: " + ex.ToString());
			}
		}
        // Método que permite seleccionar la funcion. 
        private void cbxFuncionesTPV_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                // Si se ha seleccionado un campo...
                if (cbxFuncionesTPV.SelectedItem != null)
                {
                    // Busca la función...
                    funcionTPV = unit.RepositorioFunciones.GetOne(c => c.CodigoFuncion.Equals(cbxFuncionesTPV.SelectedValue.ToString()));
                    // Recoge la fecha de inicio de final en el datePiker.
                    dtpkFechaFuncionTPV.DisplayDateStart = Convert.ToDateTime(funcionTPV.fechaInicio);
                    dtpkFechaFuncionTPV.DisplayDateEnd = Convert.ToDateTime(funcionTPV.fechaFin);
                    // Establece el nombre de la sala asignada a la funcion en el label.
                    lblSalaFuncionTPV.Text = funcionTPV.CodigoSala;
                    // Busca la sala.
                    Sala auxSal = unit.RepositorioSalas.GetOne(c => c.CodigoSala.Equals(funcionTPV.CodigoSala));
                    // Añade las tarifas disponibles con esa función y esa sala al comboBox de tarifas.
                    cbxTarifasFuncionTPV.ItemsSource = unit.RepositorioTarifas.GetGroup(c => c.formatoFuncion.Equals(funcionTPV.formato)
                                                            && c.formatoSala.Equals(auxSal.formato));
                    // Anulamos las fechas.
                    AnularFechas();
                }
            }
            catch (Exception ex)
            {
                System.Windows.MessageBox.Show("Ha ocurrido un error. Pongase en contacto con el administrador.");
                Console.WriteLine("Error: " + ex.ToString());
            }
        }
        // Método al seleccionar una butaca.
        private void butacas_click(object sender, RoutedEventArgs e)
        {
            try
            {
                // Recoge el botón.
                var aux = e.OriginalSource;
                // Comprueba si es un boton.
                if (aux.GetType() == typeof(System.Windows.Controls.Button))
                {
                    // Variable que controla si ya está en la lista.
                    bool enLista = false;
                    System.Windows.Controls.Button b = (System.Windows.Controls.Button)aux;
                    // Recogemos el nombre del botón.
                    String btname = b.Content.ToString();
                    // Comprobamos si está en la lista.
                    foreach (Entrada item in entradasVender)
                    {
                        // Si está la eliminamos.
                        if (item.butaca.Equals(btname))
                        {
                            // Lo eliminamos de la lista.
                            entradasVender.Remove(item);
                            // (Revisar, cambiar color)
                            b.Style = this.FindResource("MaterialDesignRaisedButton") as Style;
                            // b.Background = Brushes.LightGray;
                            // Establecemos que estaba en la lista.
                            enLista = true;
                            break;
                        }
                    }
                    // Si no está en la lista...
                    if (!enLista)
                    {
                        // Creamos el objeto...
                        entradaGestionar = new Entrada();
                        entradaGestionar.butaca = btname;
                        entradasVender.Add(entradaGestionar);
                        // (Revisar, cambiar color)
                        b.Style = this.FindResource("MaterialDesignRaisedAccentButton") as Style;
                        //b.Background = Brushes.Green;
                    }
                }
            }
            catch (Exception ex)
            {
                System.Windows.MessageBox.Show("Ha ocurrido un error. Pongase en contacto con el administrador.");
                Console.WriteLine("Error: " + ex.ToString());
            }
        }

        // Método que se ejecuta cuando cambiamos el valor del datePiker.
        private void dtpkFechaFuncionTPV_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                // Si una fecha está seleccionada.
                if (dtpkFechaFuncionTPV.SelectedDate != null)
                {
                    // Recoge el día
                    string diaSemana = ci.DateTimeFormat.GetDayName(dtpkFechaFuncionTPV.SelectedDate.Value.DayOfWeek).ToLower();
                    // Recoge un horario que tenga ese día asignado y esa función asignada.
                    Horario horario = unit.RepositorioHorarios.GetOne(c => c.Funcion.CodigoFuncion.Equals(funcionTPV.CodigoFuncion) && c.DiaSemana.Equals(diaSemana));
                    // Recoge la hora de inicio y la hora de fin.
                    DateTime calcHoraIn = Convert.ToDateTime(horario.horaInicio);
                    DateTime calcHoraOut = Convert.ToDateTime(horario.horaFin);
                    // Si la hora de inicio es mayor a la de salida, añade un día.
                    if (calcHoraIn > calcHoraOut)
                    {
                        calcHoraOut = calcHoraOut.AddDays(1);
                    }

                    cbxHorasFuncionTPV.ItemsSource = "";

                    DateTime duracionPelicula = Convert.ToDateTime(funcionTPV.Pelicula.duracion).AddMinutes(30);
                    double duracionTotal = duracionPelicula.Subtract(DateTime.Today).TotalMinutes;

                    List<string> horas = new List<string>();
                    // Mientras la hora de inicio sea menor a la de salida.
                    while (calcHoraIn < calcHoraOut)
                    {
                        // Añade la hora.
                        horas.Add(calcHoraIn.ToShortTimeString());
                        // Establece 3 horas más a la hora de inicio.
                        calcHoraIn = calcHoraIn.AddMinutes(duracionTotal);
                    }
                    // Cargamos las horas en el comboBox.
                    cbxHorasFuncionTPV.ItemsSource = horas;
                }
                else
                {
                    cbxHorasFuncionTPV.ItemsSource = "";
                }
            }
            catch (Exception ex)
            {
                System.Windows.MessageBox.Show("Ha ocurrido un error. Pongase en contacto con el administrador.");
                Console.WriteLine("Error: " + ex.ToString());
            }
        }

        // Método que se ejecuta al cambiar la hora, bloquea las butacas.
        private void cbxHorasFuncionTPV_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cbxHorasFuncionTPV.SelectedValue != null)
            {
                BloquearButacas();
            }
        }
        // Método que limpia campos y carga nuevos valores en el TPV de película.
        private void GestionarPeliculaTPV()
        {
            try
            {
                unfgButacas.Children.Clear();
                unfgButacas.Columns = 0;
                unfgButacas.Rows = 0;
                dtpkFechaFuncionTPV.Text = "";
                cbxHorasFuncionTPV.ItemsSource = "";
                cbxTarifasFuncionTPV.ItemsSource = "";
                txtClienteFuncionTPV.Text = "1";
                peliculasCartelera = unit.RepositorioPeliculas.GetGroup(c => c.enCartelera == true);
                if (peliculasCartelera.Count != 0)
                {
                    peliculaSeleccionadaTPV = peliculasCartelera[peliculaSeleccionadaIndex];

                    imgPeliculaSeleccionadaTPV.Source = new BitmapImage(new Uri(peliculaSeleccionadaTPV.imagen));
                    List<Funcion> aux = unit.RepositorioFunciones.GetGroup(c => c.activo == true &&
                                                        c.CodigoPelicula.Equals(peliculaSeleccionadaTPV.CodigoPelicula));
                    cbxFuncionesTPV.ItemsSource = "";
                    cbxFuncionesTPV.ItemsSource = aux;
                }
                entradasVender = new List<Entrada>();
            }
            catch (Exception ex)
            {
                System.Windows.MessageBox.Show("Ha ocurrido un error. No se ha encontrado la ruta de la imagen. Pongase en contacto con el administrador.");
                Console.WriteLine("Error: " + ex.ToString());
            }
        }
        // Método que anula las fechas del datePicker.
        private void AnularFechas()
        {
            try
            {
                // Limpiamos las marcadas.
                dtpkFechaFuncionTPV.BlackoutDates.Clear();

                List<string> lista = unit.RepositorioHorarios.GetGroup(c => c.CodigoFuncion.Equals(funcionTPV.CodigoFuncion)).Select(c => c.DiaSemana).ToList<string>();

                for (DateTime i = dtpkFechaFuncionTPV.DisplayDateStart.Value; i < dtpkFechaFuncionTPV.DisplayDateEnd.Value.AddDays(1); i = i.AddDays(1))
                {
                    // Si tiene el mismo nombre que el dia, lo marca.
                    if (!lista.Contains(ci.DateTimeFormat.GetDayName(i.DayOfWeek).ToLower()))
                    {
                        dtpkFechaFuncionTPV.BlackoutDates.Add(new CalendarDateRange(i));
                    }

                }
            }
            catch (Exception ex)
            {
                System.Windows.MessageBox.Show("Ha ocurrido un error. Pongase en contacto con el administrador.");
                Console.WriteLine("Error: " + ex.ToString());
            }
        }
        // Método que limpia los campos para hacer una nueva venta de entradas.
        public void NuevaEntrada()
        {
            entradasVender = new List<Entrada>();
            Entrada entradaGestionar = new Entrada();
            dckpTPV.DataContext = entradaGestionar;
        }
        // Metodo que permite hacer una nueva venta de entradas pulsando el botón(Entrada).
        private void btnFacturarEntradaTPV_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                int num;
                // Si el id del cliente es correcto...
                if (Int32.TryParse(txtClienteFuncionTPV.Text, out num))
                {
                    // Lo buscamos...
                    Cliente auxCli = unit.RepositorioClientes.GetOne(c => c.ClienteId == num);
                    // Si lo encuentra y esta marcada la tarifa...
                    if (auxCli != null && cbxTarifasFuncionTPV.SelectedItem != null)
                    {
                        if (entradasVender.Count != 0)
                        {
                            // Reocorre la lista de entradas y establece los campos.
                            foreach (Entrada item in entradasVender)
                            {
                                item.CodigoEmpleado = empleadoLogueado.CodigoEmpleado;
                                item.nombreEmpleado = empleadoLogueado.nombre;
                                item.ClienteId = Convert.ToInt32(txtClienteFuncionTPV.Text);
                                item.CodigoTarifa = cbxTarifasFuncionTPV.SelectedValue.ToString();
                                item.CodigoFuncion = cbxFuncionesTPV.SelectedValue.ToString();
                                item.hora = cbxHorasFuncionTPV.SelectedValue.ToString();
                                item.fecha = dtpkFechaFuncionTPV.Text;
                            }
                            tipoFactura = "Entradas";
                            // Abre la ventana de facturar.
                            Facturar facturar = new Facturar();
                            facturar.Show();
                            facturado = true;
                            this.IsEnabled = false;
                        }
                        // Mensaje de que no ha seleccionado butacas.
                        else { System.Windows.MessageBox.Show("Debe seleccionar alguna butaca para poder facturar."); }
                    }
                    // Mensaje de que no ha seleccionado un id o tarifa.
                    else { System.Windows.MessageBox.Show("El id de cliente es incorrecto o no ha seleccionado tarifa."); }
                }
                // Mensaje de que no ha puesto el formato correcto en id cliente.
                else { System.Windows.MessageBox.Show("El id (numérico) de cliente es incorrecto."); }
            }
            catch (Exception ex)
            {
                System.Windows.MessageBox.Show("Ha ocurrido un error. Pongase en contacto con el administrador.");
                Console.WriteLine("Error: " + ex.ToString());
            }
        }
        // Método que hace una nueva venta al pulsar el botón(new)
        private void btnNuevaFacturaEntradaTPV_Click(object sender, RoutedEventArgs e)
        {
            if (System.Windows.MessageBox.Show("Desea crear una nueva venta? Se eliminaran los datos actuales.", "Pregunta", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.No)
            {
                // do no stuff
            }
            else
            {
                NuevaEntrada();
                GestionarPeliculaTPV();
            }
        }
        // Método que guarda la tarifa seleccionada.
        private void cbxTarifasFuncionTPV_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cbxTarifasFuncionTPV.SelectedValue != null)
            {
                string codigoTarifa = cbxTarifasFuncionTPV.SelectedValue.ToString();
                tarifaVenta = unit.RepositorioTarifas.GetOne(c => c.CodigoTarifa.Equals(codigoTarifa));
            }
        }
        // Método que bloquea las butacas.
        public void BloquearButacas()
        {
            try
            {
                // Busca la sala.
                Sala salaTPV = unit.RepositorioSalas.GetOne(c => c.CodigoSala.Equals(funcionTPV.CodigoSala));
                // Busca las entradas que ya están cubiertas.
                List<Entrada> entradasOcupadas = unit.RepositorioEntradas.GetGroup(c => c.CodigoFuncion.Equals(funcionTPV.CodigoFuncion)
                                                    && c.fecha.Equals(dtpkFechaFuncionTPV.Text) && c.hora.Equals(cbxHorasFuncionTPV.SelectedValue.ToString()));
                // Establece las butacas en el UniformGrid.
                unfgButacas.Children.Clear();
                unfgButacas.Columns = salaTPV.numColumnas;
                unfgButacas.Rows = salaTPV.numFilas;
                // Recorremos filas y columnas.
                for (int i = 0; i < salaTPV.numFilas; i++)
                {
                    for (int j = 0; j < salaTPV.numColumnas; j++)
                    {
                        // Creamos las butacas.
                        System.Windows.Controls.Button btn = new System.Windows.Controls.Button();
                        btn.Name = "b" + i + "b" + j;
                        btn.Content = i + "/" + j;
                        btn.Click += butacas_click;
                        btn.MinHeight = 23;
                        btn.MinWidth = 50;
                        btn.Width = double.NaN;
                        btn.Height = double.NaN;
                        btn.Margin = new Thickness(1);
                        // Comprobamos si esta ocupada.
                        foreach (Entrada item in entradasOcupadas)
                        {
                            if (item.butaca.Equals(btn.Content))
                            {
                                btn.IsEnabled = false;
                                //(Revisar cambiar color)
                                //btn.Background = Brushes.DarkRed;
                            }
                        }
                        // La añadimos al grid.
                        unfgButacas.Children.Add(btn);
                    }
                }
            }
            catch (Exception ex)
            {
                System.Windows.MessageBox.Show("Ha ocurrido un error. Pongase en contacto con el administrador.");
                Console.WriteLine("Error: " + ex.ToString());
            }
        }
        #endregion
        // Región para la gestión de los proveedores. Añadir, modificar, eliminar, buscar, limpiar campos, abrir listado.
        #region comProveedores
        // Método que se ejecuta al darle al botón de buscar (Lupa). 
        private void btnBuscarProveedores_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                // Comprobamos si los campos estan cubiertos.
                if (!String.IsNullOrEmpty(cbxBuscarProveedor.Text) && !String.IsNullOrEmpty(txtBuscarProveedores.Text))
                {
                    // Buscamos por el campo correspondiente.
                    switch (cbxBuscarProveedor.Text)
                    {
                        case "Código":
                            proveedorGestionar = unit.RepositorioProveedores.GetOne(c => c.CodigoProveedor.Equals(txtBuscarProveedores.Text));
                            break;
                        case "NIF":
                            proveedorGestionar = unit.RepositorioProveedores.GetOne(c => c.nif.Equals(txtBuscarProveedores.Text));
                            break;
                        case "Teléfono":
                            proveedorGestionar = unit.RepositorioProveedores.GetOne(c => c.telefono.Equals(txtBuscarProveedores.Text));
                            break;
                        case "Email":
                            proveedorGestionar = unit.RepositorioProveedores.GetOne(c => c.correo.Equals(txtBuscarProveedores.Text));
                            break;

                    }
                    // Si encuentra...
                    if (proveedorGestionar != null)
                    {
                        // Cargamos el contexto del dockPanel.
                        dckpProveedores.DataContext = proveedorGestionar;
                    }
                    // Mensaje de que no ha encontrado al proveedor.
                    else
                    {
                        System.Windows.MessageBox.Show("El proveedor no se encuentra registrado.");
                        LimpiarCamposProveedor();
                    }
                }
                // Mensaje de que no estan los campos cubiertos.
                else
                {
                    System.Windows.MessageBox.Show("Debe señalar el código/NIF/teléfono/email de quien desea buscar.");
                }
            }
            catch (Exception ex)
            {
                System.Windows.MessageBox.Show("Ha ocurrido un error. Pongase en contacto con el administrador.");
                Console.WriteLine("Error: " + ex.ToString());
            }
        }
        // Método que se ejecuta al darle al boton de añadir(disquete).
        private void btnAñadirProveedor_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                // Añadimos el código directamente.
                proveedorGestionar.CodigoProveedor = txtCodigoProveedor.Text;
                // Buscamos al proveedor
                Proveedor aux = unit.RepositorioProveedores.GetOne(c => c.CodigoProveedor.Equals(proveedorGestionar.CodigoProveedor)
                    || c.telefono.Equals(proveedorGestionar.telefono) || c.nif.Equals(proveedorGestionar.nif) ||
                    c.correo.Equals(proveedorGestionar.correo));
                // Si no se encuentra...
                if (aux == null)
                {
                    // Si pasa la validación...
                    if (validado(proveedorGestionar))
                    {
                        // Añadimos los datos a la base de datos.
                        unit.RepositorioProveedores.Add(proveedorGestionar);
                        System.Windows.MessageBox.Show("Proveedor registrado.");
                        // Limpiar campos.
                        LimpiarCamposProveedor();
                    }
                }
                // Mensaje si el proveedor ya ha sido registrado con los mismos campos.
                else
                {
                    String error = "El proveedor ya ha sido registrado con el mismo:";
                    if (aux.CodigoProveedor == proveedorGestionar.CodigoProveedor)
                        error += " código";
                    if (aux.telefono == proveedorGestionar.telefono)
                        error += " teléfono ";
                    if (aux.nif == proveedorGestionar.nif)
                        error += " NIF ";
                    if (aux.correo == proveedorGestionar.correo)
                        error += " correo ";
                    System.Windows.MessageBox.Show(error);
                }
            }
            catch (Exception ex)
            {
                System.Windows.MessageBox.Show("Ha ocurrido un error. Pongase en contacto con el administrador.");
                Console.WriteLine("Error: " + ex.ToString());
            }
        }
        // Método que se ejecuta cuando le das al botón de editar(lápiz).
        private void btnModificarProveedor_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (!String.IsNullOrEmpty(txtCodigoProveedor.Text))
                {
                    // Buscamos al proveedor
                    Proveedor aux = unit.RepositorioProveedores.GetOne(c => c.CodigoProveedor.Equals(txtCodigoProveedor.Text));
                    // Si encuentra...
                    if (aux != null)
                    {
                        // Si pasa la validación...
                        if (validado(proveedorGestionar))
                        {
                            // Modificamos los datos en la base de datos.
                            unit.RepositorioProveedores.Update(proveedorGestionar);
                            System.Windows.MessageBox.Show("Proveedor modificado.");
                            LimpiarCamposProveedor();
                        }
                    }
                    else { System.Windows.MessageBox.Show("El proveedor no se encuentra registrado con ese código. No se puede modificar."); }
                }
                else { System.Windows.MessageBox.Show("Establezca un código en el campo correspondiente."); }
            }
            catch (Exception ex)
            {
                System.Windows.MessageBox.Show("Ha ocurrido un error. Pongase en contacto con el administrador.");
                Console.WriteLine("Error: " + ex.ToString());
            }
        }
        // Método que se ejecuta cuando le das al boton de eliminar(basura).
        private void btnEliminarProveedor_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (!String.IsNullOrEmpty(txtCodigoProveedor.Text))
                {
                    // Buscamos al proveedor
                    Proveedor aux = unit.RepositorioProveedores.GetOne(c => c.CodigoProveedor.Equals(txtCodigoProveedor.Text));
                    // Si encuentra...
                    if (aux != null)
                    {
                        // Si pasa la validación...
                        if (validado(proveedorGestionar))
                        {
                            // Eliminamos los datos de la base de datos.
                            unit.RepositorioProveedores.Delete(proveedorGestionar);
                            System.Windows.MessageBox.Show("Proveedor eliminado.");
                            // Limpiar campos.
                            LimpiarCamposProveedor();
                        }
                    }
                    else { System.Windows.MessageBox.Show("El proveedor no se encuentra registrado con ese código. No se puede eliminar."); }
                }
                else { System.Windows.MessageBox.Show("Establezca un código en el campo correspondiente."); }
            }
            catch (Exception ex)
            {
                System.Windows.MessageBox.Show("Ha ocurrido un error. Pongase en contacto con el administrador.");
                Console.WriteLine("Error: " + ex.ToString());
            }
        }
        // Método que se ejecuta al darle al boton de abrir lista(listado).
        private void btnListaProveedores_Click(object sender, RoutedEventArgs e)
        {
            tipoListaLeer = "Proveedores";
            Listas listas = new Listas();
            listas.Show();
        }
        // Método que se ejecuta al darle al botón de limpiar(papelera).
        private void btnLimpiarProveedores_Click(object sender, RoutedEventArgs e)
        {
            // Limpiar campos.
            LimpiarCamposProveedor();
        }

        // Método que permite limpiar los campos de Proveedor
        private void LimpiarCamposProveedor()
        {
            proveedorGestionar = new Proveedor();
            dckpProveedores.DataContext = proveedorGestionar;
        }
        #endregion
        // Región para la gestión de los categorías de producto. Añadir, añadir imagen, modificar, eliminar, buscar, limpiar campos, abrir listado.
        #region comCategoriasProductos
        // Método que se ejecuta al darle al botón de buscar (Lupa). 
        private void btnBuscarCategoriaProducto_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                // Comprueba si los campos están vacios
                if (!String.IsNullOrEmpty(cbxBuscarCategoriasProductos.Text) && !String.IsNullOrEmpty(txtBuscarCategoriaProductos.Text))
                {
                    // Busca por el campo correspondiente.
                    switch (cbxBuscarCategoriasProductos.Text)
                    {
                        case "Código":
                            categoriaProductoGestionar = unit.RepositorioCategoriasProductos.GetOne(c => c.CodigoCategoriaProducto.Equals(txtBuscarCategoriaProductos.Text));
                            break;
                        case "Nombre":
                            categoriaProductoGestionar = unit.RepositorioCategoriasProductos.GetOne(c => c.nombre.Equals(txtBuscarCategoriaProductos.Text));
                            break;
                    }
                    // Si lo encuentra
                    if (categoriaProductoGestionar != null)
                    {
                        // Establece el contexto del dockPanel.
                        dckpCategoriasProductos.DataContext = categoriaProductoGestionar;
                    }
                    // Mensaje de que no se encuentra en la base de datos.
                    else
                    {
                        System.Windows.MessageBox.Show("La categoría no se encuentra registrada.");
                        LimpiarCamposCategoríaProducto();
                    }
                }
                // Mensaje de que los campos no estan cubiertos.
                else
                {
                    System.Windows.MessageBox.Show("Debe señalar el código/nombre de la categoría desea buscar.");
                }
            }
            catch (Exception ex)
            {
                System.Windows.MessageBox.Show("Ha ocurrido un error. Pongase en contacto con el administrador.");
                Console.WriteLine("Error: " + ex.ToString());
            }
        }
        // Método que permite seleccionar una imagen dandole al botón(picture).
        private void btnImagenCategoriaProducto_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                //Variable que nos permite saber si copiamos la imagen a la carpeta que necesitamos
                copiado = false;
                // Instanciamos el buscador
                OpenFileDialog browser = new OpenFileDialog();
                // Seleccionamos el directorio de origen.
                browser.InitialDirectory = "c:\\";
                browser.Filter = "txt files (*.txt)|*.txt|All files (*.*)|*.*";
                browser.FilterIndex = 2;
                browser.RestoreDirectory = true;
                // Lanzamos el buscador
                DialogResult res = browser.ShowDialog();
                // Directorio seleccionado.
                string directorioImagen = browser.FileName;
                // Directorio del proyecto.
                string direccionImagenes = System.IO.Path.Combine(Environment.CurrentDirectory);

                direccionImagenes = System.IO.Path.Combine(direccionImagenes, "imagenesCategoriasProductos");

                FileInfo f = new FileInfo(directorioImagen);
                // Directorio donde se va a guardar la imagen
                directorioCompleto = System.IO.Path.Combine(direccionImagenes, f.Name);
                try
                {
                    // Si no esta copiada la copia.
                    if (!File.Exists(directorioCompleto))
                        File.Copy(f.FullName, directorioCompleto);
                    copiado = true;
                    // La mostramos en el pictureBox.
                    categoriaProductoGestionar.imagen = directorioCompleto;
                    imgCategoriaProducto.Source = new BitmapImage(new Uri(directorioCompleto));
                }
                catch (Exception ex)
                {
                    // Mensaje si ya está copiada.
                    System.Windows.MessageBox.Show("El fichero " + f.Name + " ya existe");
                    Console.WriteLine("Warning: " + ex.ToString());
                }
            }
            catch (Exception ex) { Console.WriteLine("Warning: " + ex.ToString()); }
        }
        // Método que se ejecuta al darle al boton de añadir(disquete).
        private void btnAñadirCategoriaProducto_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                // Establecemos el codigo directamente.
                categoriaProductoGestionar.CodigoCategoriaProducto = txtCodigoCategoriaProducto.Text;
                // Buscamos la categoría.
                CategoriaProducto aux = unit.RepositorioCategoriasProductos.GetOne(c => c.CodigoCategoriaProducto.Equals(categoriaProductoGestionar.CodigoCategoriaProducto) || c.nombre.Equals(categoriaProductoGestionar.nombre));
                // Si no la encuentra...
                if (aux == null)
                {
                    // Si pasa la validación...
                    if (validado(categoriaProductoGestionar))
                    {
                        // Añadimos los datos a la base de datos.
                        unit.RepositorioCategoriasProductos.Add(categoriaProductoGestionar);
                        System.Windows.MessageBox.Show("Categoría registrada.");
                        // Limpiar campos.
                        LimpiarCamposCategoríaProducto();
                    }
                }
                // Mensaje de que la categoría ya esta registrada.
                else { System.Windows.MessageBox.Show("La categoría ya está registrada."); }
            }
            catch (Exception ex)
            {
                System.Windows.MessageBox.Show("Ha ocurrido un error. Pongase en contacto con el administrador.");
                Console.WriteLine("Error: " + ex.ToString());
            }
        }
        // Método que se ejecuta cuando le das al botón de editar(lápiz).
        private void btnModificarCategoriaProducto_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (!String.IsNullOrEmpty(txtCodigoCategoriaProducto.Text))
                {
                    // Buscamos la categoria
                    CategoriaProducto aux = unit.RepositorioCategoriasProductos.GetOne(c => c.CodigoCategoriaProducto.Equals(txtCodigoCategoriaProducto.Text));
                    // Si la encuentra...
                    if (aux != null)
                    {
                        // Si pasa la validación...
                        if (validado(categoriaProductoGestionar))
                        {
                            // Modificamos los datos de la base de datos.
                            unit.RepositorioCategoriasProductos.Update(categoriaProductoGestionar);
                            System.Windows.MessageBox.Show("Categoría modificada.");
                            LimpiarCamposCategoríaProducto();
                        }
                    }
                    else { System.Windows.MessageBox.Show("La categoría no se encuentra registrada con ese código. No se puede modificar."); }
                }
                else { System.Windows.MessageBox.Show("Establezca un código en el campo correspondiente."); }
            }
            catch (Exception ex)
            {
                System.Windows.MessageBox.Show("Ha ocurrido un error. Pongase en contacto con el administrador.");
                Console.WriteLine("Error: " + ex.ToString());
            }
        }
        // Método que se ejecuta cuando le das al boton de eliminar(basura).
        private void btnEliminarCategoriaProducto_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (!String.IsNullOrEmpty(txtCodigoCategoriaProducto.Text))
                {
                    // Buscamos la categoria
                    CategoriaProducto aux = unit.RepositorioCategoriasProductos.GetOne(c => c.CodigoCategoriaProducto.Equals(txtCodigoCategoriaProducto.Text));
                    // Si la encuentra...
                    if (aux != null)
                    {
                        // Si pasa la validación...
                        if (validado(categoriaProductoGestionar))
                        {
                            // Eliminamos los datos de la base de datos.
                            unit.RepositorioCategoriasProductos.Delete(categoriaProductoGestionar);
                            System.Windows.MessageBox.Show("Categoría eliminada.");
                            // Limpiar campos.
                            LimpiarCamposCategoríaProducto();
                        }
                    }
                    else { System.Windows.MessageBox.Show("La categoría no se encuentra registrada con ese código. No se puede eliminar."); }
                }
                else { System.Windows.MessageBox.Show("Establezca un código en el campo correspondiente."); }
            }
            catch (Exception ex)
            {
                System.Windows.MessageBox.Show("Ha ocurrido un error. Pongase en contacto con el administrador.");
                Console.WriteLine("Error: " + ex.ToString());
            }
        }
        // Método que se ejecuta al darle al boton de abrir lista(listado).
        private void btnListaCategoriasProducto_Click(object sender, RoutedEventArgs e)
        {
            tipoListaLeer = "CategoriasProductos";
            Listas listas = new Listas();
            listas.Show();
        }
        // Método que se ejecuta al darle al botón de limpiar(papelera).
        private void btnLimpiarCategoriaProducto_Click(object sender, RoutedEventArgs e)
        {
            // Limpiar campos.
            LimpiarCamposCategoríaProducto();
        }

        // Método para limpiar los campos de categoría producto.
        private void LimpiarCamposCategoríaProducto()
        {
            categoriaProductoGestionar = new CategoriaProducto();
            dckpCategoriasProductos.DataContext = categoriaProductoGestionar;
        }
        #endregion
        // Región para la gestión de los productos. Añadir, añadir imagen, modificar, eliminar, buscar, limpiar campos, abrir listado.
        #region comProductos
        // Método que se ejecuta al darle al botón de buscar (Lupa). 
        private void btnBuscarProductos_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                // Comprobamos si el campo esta vacío.
                if (!String.IsNullOrEmpty(txtBuscarProductos.Text))
                {
                    // Buscamos por el campo correspondiente.
                    productoGestionar = unit.RepositorioProductos.GetOne(c => c.CodigoProducto.Equals(txtBuscarProductos.Text));
                    // Si encuentra...
                    if (productoGestionar != null)
                    {
                        // Establecemos el contexto del dockPanel.
                        dckpProductos.DataContext = productoGestionar;
                    }
                    // Mensaje de que el producto no existe.
                    else
                    {
                        System.Windows.MessageBox.Show("El producto no se encuentra registrado.");
                        LimpiarCamposProducto();
                    }
                }
                // Mensaje de que el campo no esta cubierto.
                else
                {
                    System.Windows.MessageBox.Show("Debe señalar el código del producto que desea buscar.");
                }
            }
            catch (Exception ex)
            {
                System.Windows.MessageBox.Show("Ha ocurrido un error. Pongase en contacto con el administrador.");
                Console.WriteLine("Error: " + ex.ToString());
            }
        }

        private void btnImagenProducto_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                //Copiamos la imagen a la carpeta que necesitamos
                copiado = false;
                // Instanciamos el buscador.
                OpenFileDialog browser = new OpenFileDialog();
                // Le asignamos la carpeta de inicio.
                browser.InitialDirectory = "c:\\";
                browser.Filter = "txt files (*.txt)|*.txt|All files (*.*)|*.*";
                browser.FilterIndex = 2;
                browser.RestoreDirectory = true;
                // Mostramos el buscador.
                DialogResult res = browser.ShowDialog();
                // Directorio de la imagen seleccionada.
                string directorioImagen = browser.FileName;
                // Directorio de la carpeta del proyecto.
                string direccionImagenes = System.IO.Path.Combine(Environment.CurrentDirectory);

                direccionImagenes = System.IO.Path.Combine(direccionImagenes, "imagenesProductos");

                FileInfo f = new FileInfo(directorioImagen);
                // Directorio de donde se guardará la imagen.
                directorioCompleto = System.IO.Path.Combine(direccionImagenes, f.Name);
                try
                {
                    // Si no existe la copiamos.
                    if (!File.Exists(directorioCompleto))
                        File.Copy(f.FullName, directorioCompleto);
                    copiado = true;
                    // Mostramos la imagen en un pictureBox.
                    productoGestionar.imagen = directorioCompleto;
                    imgProducto.Source = new BitmapImage(new Uri(directorioCompleto));
                }
                catch (Exception ex)
                {
                    // Mensaje si ya existe la imagen en el directorio.
                    System.Windows.MessageBox.Show("El fichero " + f.Name + " ya existe");
                    Console.WriteLine("Warning: " + ex.ToString());
                }
            }
            catch (Exception ex) { Console.WriteLine("Warning: " + ex.ToString()); }
        }
        // Método que se ejecuta al darle al boton de añadir(disquete).
        private void btnAñadirProducto_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                // Establecemos el código directamente.
                productoGestionar.CodigoProducto = txtCodigoProducto.Text;
                // Buscamos el producto.
                Producto aux = unit.RepositorioProductos.GetOne(c => c.CodigoProducto.Equals(productoGestionar.CodigoProducto));
                // Si no lo encuentra...
                if (aux == null)
                {
                    // Si pasa las validaciones...
                    if (validado(productoGestionar))
                    {
                        // Buscamos el proveedor y la categoría de producto.
                        Proveedor auxPro = unit.RepositorioProveedores.GetOne(c => c.CodigoProveedor.Equals(txtProveedorProducto.Text));
                        CategoriaProducto auxCat = unit.RepositorioCategoriasProductos.GetOne(c => c.CodigoCategoriaProducto.Equals(txtCodigoCategoriaProductoProducto.Text));
                        double precio;
                        int cantidad;
                        // Si existen...
                        if (auxPro != null && auxCat != null)
                        {
                            if (Double.TryParse(txtPrecioProducto.Text, out precio) && Int32.TryParse(txtStockProducto.Text, out cantidad))
                            {
                                // Añadimos los datos a la base de datos...
                                unit.RepositorioProductos.Add(productoGestionar);
                                System.Windows.MessageBox.Show("Producto registrada.");
                                // Limpiar campos.
                                LimpiarCamposProducto();
                            }
                            else { System.Windows.MessageBox.Show("El precio debe ser decimal y el stock entero."); }
                        }
                        // Mensaje si no encuentra al proveedor o la categoría.
                        else { System.Windows.MessageBox.Show("El código de proveedor/categoría no es el correcto."); }
                    }
                }
                // Mensaje si el producto ya esta registrado.
                else { System.Windows.MessageBox.Show("El producto ya está registrado."); }
            }
            catch (Exception ex)
            {
                System.Windows.MessageBox.Show("Ha ocurrido un error. Pongase en contacto con el administrador.");
                Console.WriteLine("Error: " + ex.ToString());
            }
        }
        // Método que se ejecuta cuando le das al botón de editar(lápiz).
        private void btnModificarProducto_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (!String.IsNullOrEmpty(txtCodigoProducto.Text))
                {
                    // Buscamos el producto.
                    Producto aux = unit.RepositorioProductos.GetOne(c => c.CodigoProducto.Equals(txtCodigoProducto.Text));
                    // Si lo encuentra...
                    if (aux != null)
                    {
                        // Si pasa la validación...
                        if (validado(productoGestionar))
                        {
                            // Buscamos el proveedor y la categoría de producto.
                            Proveedor auxPro = unit.RepositorioProveedores.GetOne(c => c.CodigoProveedor.Equals(txtProveedorProducto.Text));
                            CategoriaProducto auxCat = unit.RepositorioCategoriasProductos.GetOne(c => c.CodigoCategoriaProducto.Equals(txtCodigoCategoriaProductoProducto.Text));
                            double precio;
                            int cantidad;
                            // Si existen...
                            if (auxPro != null && auxPro != null)
                            {
                                if (Double.TryParse(txtPrecioProducto.Text, out precio) && Int32.TryParse(txtStockProducto.Text, out cantidad))
                                {
                                    // Modificamos los datos en la base de datos...
                                    unit.RepositorioProductos.Update(productoGestionar);
                                    System.Windows.MessageBox.Show("Producto modificado.");
                                    LimpiarCamposProducto();
                                }
                                else { System.Windows.MessageBox.Show("El precio debe ser decimal y el stock entero."); }
                            }
                            // Mensaje si el proveedor y la categoria no se encontraron.
                            else { System.Windows.MessageBox.Show("El código de proveedor/categoría no es el correcto."); }
                        }
                    }
                    else { System.Windows.MessageBox.Show("El producto no se encuentra registrado con ese código. No se puede modificar."); }
                }
                else { System.Windows.MessageBox.Show("Establezca un código en el campo correspondiente."); }
            }
            catch (Exception ex)
            {
                System.Windows.MessageBox.Show("Ha ocurrido un error. Pongase en contacto con el administrador.");
                Console.WriteLine("Error: " + ex.ToString());
            }
        }
        // Método que se ejecuta cuando le das al boton de eliminar(basura).
        private void btnEliminarProducto_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (!String.IsNullOrEmpty(txtCodigoProducto.Text))
                {
                    // Buscamos el producto.
                    Producto aux = unit.RepositorioProductos.GetOne(c => c.CodigoProducto.Equals(txtCodigoProducto.Text));
                    // Si lo encuentra...
                    if (aux != null)
                    {
                        // Si pasa las validaciones...
                        if (validado(productoGestionar))
                        {
                            // Eliminamos los datos de la base de datos.
                            unit.RepositorioProductos.Delete(productoGestionar);
                            System.Windows.MessageBox.Show("Producto eliminado.");
                            // Limpiar campos.
                            LimpiarCamposProducto();
                        }
                    }
                    else { System.Windows.MessageBox.Show("El producto no se encuentra registrado con ese código. No se puede eliminar."); }
                }
                else { System.Windows.MessageBox.Show("Establezca un código en el campo correspondiente."); }
            }
            catch (Exception ex)
            {
                System.Windows.MessageBox.Show("Ha ocurrido un error. Pongase en contacto con el administrador.");
                Console.WriteLine("Error: " + ex.ToString());
            }
        }
        // Método que se ejecuta al darle al boton de abrir lista(listado).
        private void btnListaProductos_Click(object sender, RoutedEventArgs e)
        {
            tipoListaLeer = "Productos";
            Listas listas = new Listas();
            listas.Show();
        }
        // Método que se ejecuta al darle al botón de limpiar(papelera).
        private void btnLimpiarProductos_Click(object sender, RoutedEventArgs e)
        {
            // Limpiar campos.
            LimpiarCamposProducto();
        }

        //Método para limpiar los campos de producto.
        private void LimpiarCamposProducto()
        {
            productoGestionar = new Producto();
            dckpProductos.DataContext = productoGestionar;
        }
        #endregion
        // Región para la gestión del TPVProductos.
        #region comTPVProductos
        // Método que permite carga el stackPanel de categorías producto.
        private void cargarTPVcategorias()
        {
            try
            {
                List<CategoriaProducto> catList = unit.RepositorioCategoriasProductos.GetAll();

                unfgCategoriasProducto.Children.Clear();
                // Por cada elemento de la lista
                foreach (CategoriaProducto item in catList)
                {
                    // Instanciamos un stackPanel.
                    StackPanel stackp = new StackPanel();
                    stackp.Orientation = System.Windows.Controls.Orientation.Vertical;
                    stackp.VerticalAlignment = VerticalAlignment.Center;
                    stackp.HorizontalAlignment = System.Windows.HorizontalAlignment.Center;
                    // Añadimos la imagen
                    if (item.imagen != null)
                    {
                        Image img = new Image();
                        img.Source = new BitmapImage(new Uri(item.imagen));
                        img.Stretch = Stretch.UniformToFill;
                        stackp.Children.Add(img);
                    }
                    // Añadimos un boton.
                    System.Windows.Controls.Button btn = new System.Windows.Controls.Button();
                    btn.Name = "c_" + item.CodigoCategoriaProducto.ToString();
                    btn.Content = stackp;
                    btn.Width = 125;
                    btn.Height = 125;
                    btn.Margin = new Thickness(2);

                    btn.Style = this.FindResource("MaterialDesignRaisedButton") as Style;

                    btn.Click += categorias_click;
                    // Añadimos otro stackpanel exterior.
                    StackPanel stackpExterior = new StackPanel();
                    stackpExterior.Orientation = System.Windows.Controls.Orientation.Vertical;
                    stackpExterior.VerticalAlignment = VerticalAlignment.Center;
                    stackpExterior.HorizontalAlignment = System.Windows.HorizontalAlignment.Center;
                    // Añadimos un label
                    System.Windows.Controls.Label l = new System.Windows.Controls.Label();
                    l.HorizontalAlignment = System.Windows.HorizontalAlignment.Center;
                    l.MaxWidth = 125;
                    l.Content = item.nombre;
                    // Añadimos el botón con la imagen y el label al stackpanel exterior.
                    stackpExterior.Children.Add(btn);
                    stackpExterior.Children.Add(l);
                    // Añadimos el extacpanel exterior al StackPanel Contenedor.
                    unfgCategoriasProducto.Children.Add(stackpExterior);
                }
            }
            catch (Exception ex)
            {
                System.Windows.MessageBox.Show("Ha ocurrido un error. Pongase en contacto con el administrador.");
                Console.WriteLine("Error: " + ex.ToString());
            }
        }
        // Método que salta al pulsar una categoría.
        private void categorias_click(object sender, RoutedEventArgs e)
        {
            try
            {
                unfgProductos.Children.Clear();

                var aux = e.OriginalSource;
                if (aux.GetType() == typeof(System.Windows.Controls.Button))
                {
                    System.Windows.Controls.Button b = (System.Windows.Controls.Button)aux;
                    string btname = b.Name;

                    String name = btname.Split('_')[1];

                    cargarTPVproductos(unit.RepositorioCategoriasProductos.GetOne(c => c.CodigoCategoriaProducto == name));
                }
            }
            catch (Exception ex)
            {
                System.Windows.MessageBox.Show("Ha ocurrido un error. Pongase en contacto con el administrador.");
                Console.WriteLine("Error: " + ex.ToString());
            }
        }
        // Cargamos el wrapPanel que contiene los productos
        private void cargarTPVproductos(CategoriaProducto cat)
        {
            try
            {
                List<Producto> proList = unit.RepositorioProductos.GetGroup(c => c.CodigoCategoriaProducto == cat.CodigoCategoriaProducto).ToList();

                unfgProductos.Children.Clear();
                // Recorremos la lista.
                foreach (Producto item in proList)
                {
                    // Añadimos un stackpanel
                    StackPanel stackp = new StackPanel();
                    stackp.Orientation = System.Windows.Controls.Orientation.Horizontal;
                    stackp.VerticalAlignment = VerticalAlignment.Center;
                    stackp.HorizontalAlignment = System.Windows.HorizontalAlignment.Center;
                    // Añadimos una imagen.
                    if (item.imagen != null)
                    {
                        Image img = new Image();
                        img.Source = new BitmapImage(new Uri(item.imagen));
                        img.Stretch = Stretch.UniformToFill;
                        stackp.Children.Add(img);
                    }
                    // Creamos un boton.
                    System.Windows.Controls.Button btn = new System.Windows.Controls.Button();
                    btn.Name = "c_" + item.CodigoProducto.ToString();

                    if (item.stock > 0)
                    {
                        // (Revisar color)
                        btn.Style = this.FindResource("MaterialDesignRaisedButton") as Style;
                        //btn.Background = Brushes.LightBlue ;
                    }
                    else
                    {
                        // (Revisar color)
                        //btn.Style = this.FindResource("MaterialDesignRaisedDarkButton") as Style;
                        btn.Background = Brushes.DarkRed;
                    }

                    btn.Content = stackp;
                    btn.Width = 125;
                    btn.Height = 125;
                    btn.Margin = new Thickness(2);

                    btn.Click += productos_click;
                    // Añadimos un stackPanel Exterior.
                    StackPanel stackpExterior = new StackPanel();
                    stackpExterior.Orientation = System.Windows.Controls.Orientation.Vertical;
                    stackpExterior.VerticalAlignment = VerticalAlignment.Center;
                    stackpExterior.HorizontalAlignment = System.Windows.HorizontalAlignment.Center;
                    // Añadimos un label.
                    System.Windows.Controls.Label l = new System.Windows.Controls.Label();
                    l.Content = item.nombre;
                    l.HorizontalAlignment = System.Windows.HorizontalAlignment.Center;
                    l.MaxWidth = 125;
                    // Añadimos el botón con la imagen y el label al stackpanel exterior.
                    stackpExterior.Children.Add(btn);
                    stackpExterior.Children.Add(l);
                    // Añadimos el stackpanel exterior al wrapPanel contenedor.
                    unfgProductos.Children.Add(stackpExterior);
                }
            }
            catch (Exception ex)
            {
                System.Windows.MessageBox.Show("Ha ocurrido un error. No se ha encontrado la ruta de la imagen. Pongase en contacto con el administrador.");
                Console.WriteLine("Error: " + ex.ToString());
            }
        }
        // Metodo que se ejecuta al pulsar un producto para añadirlo a la venta.
        private void productos_click(object sender, RoutedEventArgs e)
        {
            try
            {
                int tryParse;
                // Si el campo id Cliente no esta vacío y tiene el formato correcto...
                if (!String.IsNullOrEmpty(txtClienteVentaTPVProducto.Text) && Int32.TryParse(txtClienteVentaTPVProducto.Text, out tryParse) == true)
                {
                    tryParse = Convert.ToInt32(txtClienteVentaTPVProducto.Text);
                    // Si el id es correcto.
                    if (unit.RepositorioClientes.GetOne(c => c.ClienteId == tryParse) != null)
                    {
                        var aux = e.OriginalSource;
                        if (aux.GetType() == typeof(System.Windows.Controls.Button))
                        {
                            System.Windows.Controls.Button b = (System.Windows.Controls.Button)aux;
                            String btname = b.Name;

                            string name = btname.Split('_')[1];
                            // El nombre del producto es el nombre del boton.
                            productoTPV = unit.RepositorioProductos.GetOne(c => c.CodigoProducto == name);
                            // Si el stok es 0
                            if (productoTPV.stock < 1)
                            {
                                //(Revisar color)
                                //b.Style = this.FindResource("MaterialDesignRaisedDarkButton") as Style;
                                b.Background = Brushes.DarkRed;
                                // Mensaje de que no hay stock.
                                System.Windows.MessageBox.Show("El producto no tiene stock", "ERROR", MessageBoxButton.OK, MessageBoxImage.Stop);
                            }
                            else
                            {
                                // Si aun no hay venta...
                                if (ventaGestionar == null)
                                {
                                    // La creamos.
                                    ventaGestionar = new Venta();
                                    ventaGestionar.Empleado = empleadoLogueado;
                                    ventaGestionar.CodigoEmpleado = ventaGestionar.Empleado.CodigoEmpleado;
                                    ventaGestionar.ClienteId = Convert.ToInt32(txtClienteVentaTPVProducto.Text);
                                    ventaGestionar.fecha = DateTime.Now;
                                }
                                // Si ya ha vienta.
                                if (ventaGestionar.LineasVentas.Where(c => c.Producto.CodigoProducto.Equals(productoTPV.CodigoProducto)).FirstOrDefault() == null)
                                {
                                    // La venta no tiene actualmente ese producto, se añade como linea
                                    lineaVentaGestionar = new LineaVenta();
                                    lineaVentaGestionar.Producto = productoTPV;
                                    lineaVentaGestionar.CodigoProducto = productoTPV.CodigoProducto;
                                    LineaVenta auxCodLineaVenta = ventaGestionar.LineasVentas.LastOrDefault();

                                    if (auxCodLineaVenta != null)
                                        lineaVentaGestionar.CodigoLineaVenta = auxCodLineaVenta.CodigoLineaVenta + 1;
                                    else
                                        lineaVentaGestionar.CodigoLineaVenta = 1;

                                    lineaVentaGestionar.Cantidad = 1;
                                    lineaVentaGestionar.Venta = ventaGestionar;

                                    ventaGestionar.LineasVentas.Add(lineaVentaGestionar);
                                    // Le resta stock al producto.
                                    productoTPV.stock--;
                                    unit.RepositorioProductos.Update(productoTPV);
                                }
                                else
                                {
                                    // la venta ya contiene ese producto, se incrementa la cantidad en la linea
                                    LineaVenta linea = ventaGestionar.LineasVentas.Where(c => c.Producto.CodigoProducto.Equals(productoTPV.CodigoProducto)).FirstOrDefault();
                                    // Incrementa la cantidad a comprar de ese producto.
                                    linea.Cantidad++;
                                    // Le resta stock al producto.
                                    productoTPV.stock--;
                                    unit.RepositorioProductos.Update(productoTPV);
                                }
                                // Le pasamos el contexto al grid de datos del producto.
                                grdProducto.DataContext = productoTPV;
                                // Recargamos la lista de la compra.
                                dtgrListaVentaProductos.ItemsSource = "";
                                dtgrListaVentaProductos.ItemsSource = ventaGestionar.LineasVentas.ToList();
                            }
                        }
                    }
                    // Mensaje si el cliente no existe.
                    else { System.Windows.MessageBox.Show("El cliente no existe, introduzca un Id (numérico) correcto"); }
                }
                // Mensaje si el tipo de dato no es el correcto.
                else { System.Windows.MessageBox.Show("Introduzca el Id (numérico) del cliente"); }
            }
            catch (Exception ex)
            {
                System.Windows.MessageBox.Show("Ha ocurrido un error. Pongase en contacto con el administrador.");
                Console.WriteLine("Error: " + ex.ToString());
            }
        }
        // Método que se ejecuta al borrar una fila en la lista con el botón(X).
        private void btnDeleteFila_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                // La venta ya contiene ese producto, se incrementa la cantidad en la linea
                LineaVenta linea = (LineaVenta)dtgrListaVentaProductos.SelectedItem;
                productoTPV = unit.RepositorioProductos.GetOne(c => c.CodigoProducto.Equals(linea.CodigoProducto));
                // Le pasamos el contexto al grid de datos del producto.
                grdProducto.DataContext = productoTPV;
                // Si el stock del producto es mayor a 0 y la cantidad a 1.
                if (linea.Cantidad > 1)
                    //Resta 1 a la cantidad.
                    linea.Cantidad--;
                else
                    // Sino lo elimina directamente.
                    ventaGestionar.LineasVentas.Remove(linea);
                // Aumenta el stock del producto y lo actualiza en la base de datos.
                productoTPV.stock++;
                unit.RepositorioProductos.Update(productoTPV);
                // Recarga la lista de la compra.
                dtgrListaVentaProductos.ItemsSource = "";
                dtgrListaVentaProductos.ItemsSource = ventaGestionar.LineasVentas.ToList();
            }
            catch (Exception ex)
            {
                System.Windows.MessageBox.Show("Ha ocurrido un error. Pongase en contacto con el administrador.");
                Console.WriteLine("Error: " + ex.ToString());
            }
        }
        // Método que lanza la ventana de facturar con el boton de comprar(ticket).
        private void btnFacturarVentaTPVProducto_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (ventaGestionar != null && ventaGestionar.LineasVentas.Count != 0)
                {
                    tipoFactura = "Productos";
                    vendido = true;
                    this.IsEnabled = false;
                    Facturar fac = new Facturar();
                    fac.Show();
                }
                else System.Windows.MessageBox.Show("No hay venta que realizar");
            }
            catch (Exception ex)
            {
                System.Windows.MessageBox.Show("Ha ocurrido un error. Pongase en contacto con el administrador.");
                Console.WriteLine("Error: " + ex.ToString());
            }
        }
        // Método que permite hacer una nueva venta limpiando los campos con el botón(new).
        private void btnNuevaFacturaVentaTPVProducto_Click(object sender, RoutedEventArgs e)
        {
            if (System.Windows.MessageBox.Show("Desea crear una nueva venta? Se eliminaran los datos actuales.", "Pregunta", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.No) { }
            else
            {
                if (ventaGestionar != null && ventaGestionar.LineasVentas.Count != 0)
                    LimpiarVentaGestionar();

                GestionarTPVenta();
            }
        }
        // Método que limpia los campos del TPVenta.
        public void GestionarTPVenta()
        {
            ventaGestionar = null;
            lineaVentaGestionar = new LineaVenta();

            unfgCategoriasProducto.Children.Clear();
            unfgProductos.Children.Clear();

            txtClienteVentaTPVProducto.Text = "1";

            cargarTPVcategorias();

            grdProducto.DataContext = new Producto();

            dtgrListaVentaProductos.ItemsSource = "";
        }

        private void LimpiarVentaGestionar()
        {
            // Recorre cada elemento en la lista del objeto Venta static de MainWindow.
            foreach (LineaVenta item in ventaGestionar.LineasVentas)
            {
                // Obtiene el producto contenido en el item.
                Producto producto = item.Producto;
                // Le suma la cantidad obtenida de ese objeto en la compra reestableciendo el stock.
                producto.stock += item.Cantidad;
                // Modifica en la base de datos.
                MainWindow.unit.RepositorioProductos.Update(producto);
            }
        }
        #endregion

        private void Window_IsEnabledChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            switch (tipoFactura)
            {
                case "Entradas":
                    if (facturado)
                    {
                        BloquearButacas();
                        facturado = false;
                    }
                    else
                    {
                        NuevaEntrada();
                        GestionarPeliculaTPV();
                    }
                    break;
                case "Productos":
                    if (vendido)
                    {
                        if (Facturar.ventaRealizada)
                        {
                            GestionarTPVenta();
                            Facturar.ventaRealizada = false;
                        }
                        vendido = false;
                    }
                    else
                        GestionarTPVenta();
                    break;
            }

        }

        private void CerrarSesion_Click(object sender, RoutedEventArgs e)
        {
            if (ventaGestionar != null && ventaGestionar.LineasVentas.Count != 0)
                LimpiarVentaGestionar();

            empleadoLogueado = new Empleado();
            statbarEmpleado.Items.Clear();
            Login login = new Login();
            login.Show();
            this.Close();
        }

        private void CerrarAplicacion_Click(object sender, RoutedEventArgs e)
        {
            if (System.Windows.MessageBox.Show("Seguro que desea cerrar la aplicación?.", "Pregunta", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.No)
            { }
            else
            {
                if (ventaGestionar != null && ventaGestionar.LineasVentas.Count != 0)
                    LimpiarVentaGestionar();

                this.Close();
            }
        }
        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (ventaGestionar != null && ventaGestionar.LineasVentas.Count != 0)
                LimpiarVentaGestionar();
        }

        // Nos aseguramos de que estos directorios existan al iniciar la ventana.
        private void crearDirectorios()
        {
            try
            {
                DirectoryInfo imagenesPeliculas = new DirectoryInfo("imagenesPeliculas");
                DirectoryInfo imagenesCatProductos = new DirectoryInfo("imagenesCategoriasProductos");
                DirectoryInfo imagenesProductos = new DirectoryInfo("imagenesProductos");

                DirectoryInfo ventasEntradas = new DirectoryInfo("ventasEntradas");
                DirectoryInfo ventasProductos = new DirectoryInfo("ventasProductos");

                if (!imagenesPeliculas.Exists) imagenesPeliculas.Create();
                if (!imagenesCatProductos.Exists) imagenesCatProductos.Create();
                if (!imagenesProductos.Exists) imagenesProductos.Create();

                if (!ventasEntradas.Exists) ventasEntradas.Create();
                if (!ventasProductos.Exists) ventasProductos.Create();
            }
            catch (Exception ex)
            {
                System.Windows.MessageBox.Show("No se han podido crear la estructura de carpetas en el directorio de la aplicación. Pongase en contacto con el administrador.");
                Console.WriteLine("Error: " + ex.ToString());
            }
        }
    }
}
