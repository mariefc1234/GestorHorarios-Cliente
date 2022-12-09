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
    public partial class A_AdministrarSalon : Window
    {
        //variables
        HttpClient client = new HttpClient();
        string tokenR;
        List<Edificio> listaEdificios = new List<Edificio>();
        String nombreEdificioFinal = null;

        //Iniciador de ventana 
        public A_AdministrarSalon(string tokenS)
        {
            tokenR = tokenS;
            client.BaseAddress = new Uri("http://127.0.0.1:5000/api/salon");
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Add("authtoken", tokenR);
            client.DefaultRequestHeaders.Accept.Add(
                new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json")
                );
            InitializeComponent();
            GetSalones();
        }

        //llamdas de botones
        private void Button_ClickAgregarSalon(object sender, RoutedEventArgs e)
        {
            A_AgregarSalon ags = new A_AgregarSalon(tokenR);
            ags.Show();
            this.Close();
        }
        private void Button_ClickRegresar(object sender, RoutedEventArgs e)
        {
            PrincipalAdministrador psa = new PrincipalAdministrador(tokenR);
            psa.Show();
            this.Close();
        }
        private void Button_ClickMostrar(object sender, RoutedEventArgs e)
        {
            Salon salonN = dgSalones.SelectedItem as Salon;
            if (salonN == null)
            {
                MessageBox.Show("Debes seleccionar un salon primero");
            }
            else
            {
                GetEdificios(salonN.idEdificio);
                string detalles = "Nombre Salon: " + salonN.nombre + " \nProyector: " + salonN.tieneProyector + "  \nCupo Total: " + salonN.totalCupo + "\nEdificio: " + nombreEdificioFinal;
                MessageBox.Show(detalles);
            }
        }
        private void Button_ClickEliminar(object sender, RoutedEventArgs e)
        {
            Salon salonN = dgSalones.SelectedItem as Salon;
            if (salonN == null)
            {
                MessageBox.Show("Debes seleccionar un edificio primero");
            }
            else
            {
                MessageBoxResult messageBoxResult = System.Windows.MessageBox.Show("Estas seguro?", "Confirmacion de eliminacion", System.Windows.MessageBoxButton.YesNo);
                if (messageBoxResult == MessageBoxResult.Yes)
                {
                    this.EliminarSalon(salonN.idSalon);
                    MessageBox.Show("Salon eliminado");
                }
            }
        }
        private void Button_ClickModificar(object sender, RoutedEventArgs e)
        {
            Salon salonN = dgSalones.SelectedItem as Salon;
            if (salonN == null)
            {
                MessageBox.Show("Debes seleccionar un salon primero");
            }
            else
            {
                int idS = salonN.idSalon;
                int idE = salonN.idEdificio;
                string nombreE = salonN.nombre;
                int proyectorR = salonN.proyector;
                int totalCupo = salonN.totalCupo;
                string tieneProyectorR = salonN.tieneProyector;

                A_ModificarSalon mds = new A_ModificarSalon(idS,idE,nombreE, proyectorR, totalCupo, tieneProyectorR, tokenR);
                mds.Show();
                this.Close();
            }
        }

        //funciones de ayuda
        private async void GetSalones()
        {
            var response = await client.GetStringAsync("salon");
            JObject json = JObject.Parse(response);
            var data = json.SelectToken("data");
            List<Salon> listaSalones = new List<Salon>();

            foreach (var datosSalon in data)
            {
                int idSD = (int)datosSalon.SelectToken("idSalon");
                int idED = (int)datosSalon.SelectToken("idEdificio");
                string nombreD = (string)datosSalon.SelectToken("nombre");
                int proyectorD = (int)datosSalon.SelectToken("proyector");
                int totalCupoD = (int)datosSalon.SelectToken("totalCupo");
                String tieneProyectorD;

                if(proyectorD == 1)
                {
                    tieneProyectorD = "Si";
                }
                else
                {
                    tieneProyectorD = "No";
                }
                listaSalones.Add(new Salon() { idSalon = idSD,idEdificio = idED, nombre = nombreD, proyector = proyectorD, totalCupo = totalCupoD, tieneProyector = tieneProyectorD });
            }
            dgSalones.ItemsSource = listaSalones;
        }
        public async void GetEdificios(int idEdificios)
        {
            var response = await client.GetStringAsync("edificio");
            JObject json = JObject.Parse(response);
            var data = json.SelectToken("data");

            foreach (var datosEdificio in data)
            {
                int idEdificio = (int)datosEdificio.SelectToken("id");
                string nombreEdificio = (string)datosEdificio.SelectToken("nombre");
                int pisosEdificio = (int)datosEdificio.SelectToken("pisos");

                if (idEdificios == idEdificio)
                {
                    nombreEdificioFinal = nombreEdificio;
                }
            }
        }
        private async void EliminarSalon(int salonId)
        {
            try
            {
                await client.DeleteAsync("salon/" + salonId);
                GetSalones();
            }
            catch (HttpRequestException he)
            {
                MessageBox.Show("No se pudo conectar con la base de datos");
            }
        }

        //Clases
        public class Salon
        {
            public int idSalon { get; set; }
            public int idEdificio { get; set; }
            public string nombre { get; set; }
            public int proyector { get; set; }
            public int totalCupo { get; set; }
            public string tieneProyector { get; set; }


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
