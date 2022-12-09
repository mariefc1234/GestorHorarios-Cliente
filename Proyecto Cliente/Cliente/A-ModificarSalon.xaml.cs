using MaterialDesignThemes.Wpf;
using Newtonsoft.Json.Linq;
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

namespace Proyecto_Cliente.Cliente
{
    public partial class A_ModificarSalon : Window
    {
        HttpClient client = new HttpClient();
        string tokenR;
        int idEdificio;
        int idSalon;

        public A_ModificarSalon(int idS, int idE, string nombreE, int proyectorR,int totalCupo,string tieneProyectorR, string tokenS)
        {
            tokenR = tokenS;
            client.BaseAddress = new Uri("http://127.0.0.1:5000/api/salon");
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Add("authtoken", tokenR);
            client.DefaultRequestHeaders.Accept.Add(
                new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json")
                );
            InitializeComponent();
            tbCupo.Text = totalCupo.ToString();
            tbNombre.Text = nombreE;
            idEdificio = idE;
            idSalon = idS;

            if (tieneProyectorR.Contains("Si"))
            {
                cbProyector.SelectedIndex = 0;
            }
            else
            {
                cbProyector.SelectedIndex = 1;
            }
            GetEdificios();
        }

        private void Button_ClickGuardar(object sender, RoutedEventArgs e)
        {
            Salon salonN = new Salon();

            if (cbProyector.SelectedIndex == -1 || tbNombre.Text.Length == 0 || tbCupo.Text.Length == 0)
            {
                MessageBox.Show("Debes ingresar informacion");
            }
            else
            {
                try
                {
                    salonN.nombre = tbNombre.Text;
                    salonN.totalCupo = Convert.ToInt32(tbCupo.Text.ToString());
                    if (cbProyector.SelectedIndex == 0)
                    {
                        salonN.proyector = 1;
                    }
                    else
                    {
                        salonN.proyector = 0;
                    }

                    Edificio edificioN = dgEdificios.SelectedItem as Edificio;
                    if (edificioN == null)
                    {
                        salonN.idEdificio = idEdificio;
                        this.ActualizarSalon(salonN);
                    }
                    else
                    {
                        salonN.idEdificio = edificioN.id;
                        this.ActualizarSalon(salonN);
                    }

                }
                catch (FormatException FE)
                {
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

        private async void ActualizarSalon(Salon salon)
        {
            try
            {
                await client.PutAsJsonAsync("salon/" + idSalon, salon);
                MessageBox.Show("Los datos se actualizaron con exito");
                A_AdministrarSalon ads = new A_AdministrarSalon(tokenR);
                ads.Show();
                this.Close();

            }
            catch (HttpRequestException he)
            {
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

        public class Salon
        {
            public string nombre { get; set; }
            public int proyector { get; set; }
            public int idEdificio { get; set; }
            public int totalCupo { get; set; }

            public Salon() { }
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
