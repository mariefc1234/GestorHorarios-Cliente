using MaterialDesignThemes.Wpf;
using Microsoft.VisualBasic;
using Newtonsoft.Json.Linq;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
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
using static Proyecto_Cliente.Cliente.S_RegistrarAlumno;

namespace Proyecto_Cliente.Cliente
{

    public partial class S_RegistrarMaestro : Window
    {
        HttpClient client = new HttpClient();
        string tokenR;
        public S_RegistrarMaestro(string tokenS)
        {
            tokenR = tokenS;
            client.BaseAddress = new Uri("http://127.0.0.1:5000/api/register");
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Add("auth_token", tokenR);

            client.DefaultRequestHeaders.Accept.Add(
                new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json")
                );
            InitializeComponent();
        }

        private void Button_ClickRegresar(object sender, RoutedEventArgs e)
        {
            Prinicipal_Secretario ps = new Prinicipal_Secretario(tokenR);
            ps.Show();
            this.Close();
        }

        private void Button_ClickGuardar(object sender, RoutedEventArgs e)
        {
            String vCorreo = tbCorreoElectronico.Text;
            String vNombres = tbNombre.Text;
            String vApellidos = tbApellidos.Text;
            String vClaveEmpleado = tbClaveEmpleado.Text;
            String vContraseña = tbContraseña.Text;


            try
            {
                String vFechaNacimiento = dpFechaNacimiento.SelectedDate.Value.ToString();
                String primerApellido;
                String segundoApellido;

                char delimitador = ' ';
                string[] valores = vFechaNacimiento.Split(delimitador);
                vFechaNacimiento = valores[0];
                valores[1] = " ";

                vFechaNacimiento = transformarFecha(vFechaNacimiento);

                valores = vApellidos.Split(delimitador);
                primerApellido = valores[0];
                segundoApellido = valores[1];

                if (vCorreo.Length == 0 || vNombres.Length == 0 || vClaveEmpleado.Length == 0 || vContraseña.Length == 0)
                {
                    MessageBox.Show("Campos vacios");
                }
                else
                {
                    var maestro = new Maestro()
                    {
                        correo = vCorreo,
                        password = vContraseña,
                        primerNombre = vNombres,
                        primerApellido = primerApellido,
                        segundoApellido = segundoApellido,
                        rol = Convert.ToInt32(2),
                        fechaNacimiento = vFechaNacimiento,
                        claveEmpleado = vClaveEmpleado
                    };
                    this.RegistrarMaestros(maestro);
                }
            }
            catch (InvalidOperationException io)
            {
                MessageBox.Show("Ingrese o seleccione una fecha");
            }
            catch (IndexOutOfRangeException iorg)
            {
                MessageBox.Show("favor de ingresar los dos apellidos");
            }
        }
        public String transformarFecha(String vFechaNacimiento)
        {
            String fechaFinal;
            String dia;
            String mes;
            String año;
            char delimitador = '/';
            string[] valores = vFechaNacimiento.Split(delimitador);
            dia = valores[0];
            mes = valores[1];
            año = valores[2];
            fechaFinal = año + "-" + mes + "-" + dia + " 00:00:00";

            return fechaFinal;
        }

        private async void RegistrarMaestros(Maestro maestro)
        {
            try
            {
                await client.PostAsJsonAsync("register", maestro);
                MessageBox.Show("Maestro registrado con exito");

                tbNombre.Clear();
                tbClaveEmpleado.Clear();
                tbCorreoElectronico.Clear();
                tbContraseña.Clear();
                tbApellidos.Clear();
                dpFechaNacimiento.SelectedDate = null;
            }
            catch (HttpRequestException he)
            {
                MessageBox.Show("No se pudo conectar con la base de datos");
            }
        }

        public class Maestro
        {
            public string correo { get; set; }
            public string password { get; set; }
            public string primerNombre { get; set; }
            public string primerApellido { get; set; }
            public string segundoApellido { get; set; }
            public int rol { get; set; }
            public string fechaNacimiento { get; set; }
            public string claveEmpleado { get; set; }

            public Maestro() { }
        }

        // Funciones de la ventana
        public bool IsDarkTheme { get; set; }
        private readonly PaletteHelper paletteHelper = new PaletteHelper();

        private void themeToggle_Click(object sender, RoutedEventArgs e)
        {
            ITheme theme = paletteHelper.GetTheme();
            if (IsDarkTheme = theme.GetBaseTheme() == BaseTheme.Dark)
            {
                IsDarkTheme = false;
                theme.SetBaseTheme(Theme.Light);
            }
            else
            {
                IsDarkTheme = true;
                theme.SetBaseTheme(Theme.Dark);
            }
            paletteHelper.SetTheme(theme);
        }
        protected override void OnMouseLeftButtonDown(MouseButtonEventArgs e)
        {
            base.OnMouseLeftButtonDown(e);
            DragMove();
        }

        private void btnCloseWindow_Click(object sender, MouseButtonEventArgs e)
        {
            try { this.Close(); } catch (Exception ex) { MessageBox.Show(ex.Message); }
        }

        private void minimizeWindow(object sender, MouseButtonEventArgs e)
        {
            try { this.WindowState = WindowState.Minimized; } catch (Exception ex) { MessageBox.Show(ex.Message); }
        }
    }
}
