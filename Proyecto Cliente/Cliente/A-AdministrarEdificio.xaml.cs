using MaterialDesignThemes.Wpf;
using Microsoft.VisualBasic;
using Newtonsoft.Json;
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
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using MessageBox = System.Windows.MessageBox;

namespace Proyecto_Cliente.Cliente
{
    public partial class A_AdministrarEdificio : Window
    {
        List<Edificio> listaEdificios = new List<Edificio>();
        HttpClient client = new HttpClient();
        string tokenR;
        public A_AdministrarEdificio(string tokenS)
        {
            tokenR = tokenS;
            client.BaseAddress = new Uri("http://127.0.0.1:5000/api/edificio");
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Add("authtoken", tokenR);
            client.DefaultRequestHeaders.Accept.Add(
                new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json")
                );

            InitializeComponent();
            GetEdificios();
        }

        //Bottones
        private void Button_ClickMostrarDetalles(object sender, RoutedEventArgs e)
        {
            Edificio edificioN = dgEdificios.SelectedItem as Edificio;
            if (edificioN == null)
            {
                MessageBox.Show("Debes seleccionar un edificio primero");
            }
            else
            {
                string detalles = "Nombre Edificio: " + edificioN.nombre + " \nNumero de pisos: " + edificioN.pisos;
                MessageBox.Show(detalles);
            }
        }

        private void Button_ClickModificarEdificio(object sender, RoutedEventArgs e)
        {
            Edificio edificioN = dgEdificios.SelectedItem as Edificio;
            if (edificioN == null)
            {
                MessageBox.Show("Debes seleccionar un edificio primero");
            }
            else
            {
                int id = edificioN.id;
                string nombre = edificioN.nombre;
                int noPisos = edificioN.pisos;

                A_ModificarEdificio mde = new A_ModificarEdificio(id, nombre, noPisos, tokenR);
                mde.Show();
                this.Close();
            }
        }

        private void Button_ClickEliminar(object sender, RoutedEventArgs e)
        {
            Edificio edificioN = dgEdificios.SelectedItem as Edificio;
            if (edificioN == null)
            {
                MessageBox.Show("Debes seleccionar un edificio primero");
            }
            else
            {
                MessageBoxResult messageBoxResult = System.Windows.MessageBox.Show("Estas seguro?", "Confirmacion de eliminacion", System.Windows.MessageBoxButton.YesNo);
                if (messageBoxResult == MessageBoxResult.Yes)
                {
                    this.EliminarEdificio(edificioN.id);
                    MessageBox.Show("Edificio eliminado correctamente");
                }
            }
        }

        private void Button_ClickRegresar(object sender, RoutedEventArgs e)
        {
            PrincipalAdministrador psa = new PrincipalAdministrador(tokenR);
            psa.Show();
            this.Close();
        }

        private void Button_ClickAgregarEdificio(object sender, RoutedEventArgs e)
        {
            AgregarEdificio age = new AgregarEdificio(tokenR);
            age.Show();
            this.Close();
        }


        //Funciones de ayuda
        private async void EliminarEdificio(int edificioId)
        {
            try
            {
                var response = await client.DeleteAsync("edificio/" + edificioId);

                if (response.StatusCode.ToString() == "OK")
                {
                    MessageBox.Show("Edificio eliminado con exito");
                    GetEdificios();
                }
                else
                {
                    MessageBox.Show("Error al eliminar");
                }
            }
            catch (HttpRequestException he)
            {
                Console.WriteLine(he);
                MessageBox.Show("No se pudo conectar con la base de datos");
            }
        }

        public async void GetEdificios()
        {
            var response = await client.GetStringAsync("edificio");
            JObject json = JObject.Parse(response);
            var data = json.SelectToken("data");
            List<Edificio> listaEdificios = new List<Edificio>();

            foreach (var datosEdificio in data)
            {
                int idEdificio = (int)datosEdificio.SelectToken("id");
                string nombreEdificio = (string)datosEdificio.SelectToken("nombre");
                int pisosEdificio = (int)datosEdificio.SelectToken("pisos");

                listaEdificios.Add(new Edificio() { id = idEdificio, nombre = nombreEdificio, pisos = pisosEdificio });
            }
            dgEdificios.ItemsSource = listaEdificios;
        }


        //clases
        public class Edificio
        {
            public int id { get; set; }
            public string nombre { get; set; }
            public int pisos { get; set; }
            public Edificio() { }
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
