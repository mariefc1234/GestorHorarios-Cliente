using MaterialDesignThemes.Wpf;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Proyecto_Cliente.Cliente
{
    public partial class A_AgregarSalon : Window
    {
        String tokenR;
        HttpClient client = new HttpClient();
        HttpClient clientEdificios = new HttpClient();
        public A_AgregarSalon(string tokenS)
        {
            tokenR = tokenS;
            client.BaseAddress = new Uri("http://127.0.0.1:5000/api/salon");
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Add("authtoken", tokenR);
            client.DefaultRequestHeaders.Accept.Add(
                new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json")
                );
            InitializeComponent();
            GetEdificios();
        }


        //Bottones
        private void Button_ClickGuardar(object sender, RoutedEventArgs e)
        {
            Edificio edificioN = dgEdificios.SelectedItem as Edificio;

            if (edificioN == null || tbNombre.Text.Length == 0 || tbCupo.Text.Length == 0 || cbProyector.SelectedIndex == -1)
            {
                MessageBox.Show("Ingresa los datos faltantes o selecciona un edificio");
            }
            else
            {
                try
                {
                    int proyector = 0;
                    if (cbProyector.SelectedIndex == 0)
                    {
                        proyector = 1;
                    }
                    else
                    {
                        proyector = 0;
                    }

                    var salon = new Salon()
                    {
                        nombre = tbNombre.Text,
                        proyector = Convert.ToInt32(proyector),
                        idEdificio = Convert.ToInt32(edificioN.id),
                        totalCupo = Convert.ToInt32(tbCupo.Text),
                    };
                    this.GuardarSalon(salon);
                }
                catch (FormatException fe)
                {
                    Console.WriteLine(fe);
                    MessageBox.Show("Cupo total solo adminite enteros");
                }
            }
        }
        private void Button_ClickRegresar(object sender, RoutedEventArgs e)
        {
            A_AdministrarSalon ads = new A_AdministrarSalon(tokenR);
            ads.Show();
            this.Close();
        }


        //Funciones de ayuda
        private async void GuardarSalon(Salon salon)
        {
            try
            {
                var response = await client.PostAsJsonAsync("salon", salon);
                if(response.StatusCode.ToString() == "OK")
                {
                    MessageBox.Show("Se guardo con exito");
                    dgEdificios.SelectedItem = null;
                    tbCupo.Text = "";
                    tbNombre.Text = "";
                    cbProyector.SelectedItem = null;
                }
                else
                {
                    MessageBox.Show("Error al guardar");
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
            clientEdificios.BaseAddress = new Uri("http://127.0.0.1:5000/api/edificio");
            clientEdificios.DefaultRequestHeaders.Accept.Clear();
            clientEdificios.DefaultRequestHeaders.Add("authtoken", tokenR);
            clientEdificios.DefaultRequestHeaders.Accept.Add(
                new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json")
                );

            var response = await clientEdificios.GetStringAsync("edificio");
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


        //Clases
        public class Salon
        {
            public string nombre { get; set; }
            public int proyector { get; set; }
            public int idEdificio { get; set; }
            public int totalCupo { get; set; }

            public Salon(){ }
        }
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
