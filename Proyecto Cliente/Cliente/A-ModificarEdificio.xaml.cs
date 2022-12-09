using MaterialDesignThemes.Wpf;
using Microsoft.VisualBasic;
using Newtonsoft.Json.Linq;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Security.Cryptography.X509Certificates;
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
    public partial class A_ModificarEdificio : Window
    {
        int idEdificio;
        string nombreEdificio;
        int noPisos;
        HttpClient client = new HttpClient();
        string tokenR;
        public A_ModificarEdificio(int id, string nombre, int pisos, string tokenS)
        {
            client.BaseAddress = new Uri("http://127.0.0.1:5000/api/area");
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Add("authtoken", tokenR);
            client.DefaultRequestHeaders.Accept.Add(
                new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json")
                );
            InitializeComponent();
            tbNombre.Text = nombre;
            cbPisos.SelectedIndex = pisos-1;
            idEdificio = id;
            nombreEdificio = nombre;
            noPisos = pisos;
            tokenR = tokenS;
        }

        private void Button_ClickRegresar(object sender, RoutedEventArgs e)
        {
            A_AdministrarEdificio mse = new A_AdministrarEdificio(tokenR);
            mse.Show();
            this.Close();

        }

        private void Button_ClickGuardar(object sender, RoutedEventArgs e)
        {
            Edificio edificioN = new Edificio();

            if(cbPisos.SelectedIndex + 1 == 0 || tbNombre.Text.Length == 0)
            {
                MessageBox.Show("Debes ingresar informacion");
            }
            else
            {
                edificioN.id = idEdificio;
                edificioN.pisos = cbPisos.SelectedIndex + 1;
                edificioN.nombre = tbNombre.Text;
                this.ActualizarEdificio(edificioN);
            }
        }

        private async void ActualizarEdificio(Edificio edificio)
        {
            try
            {
                await client.PutAsJsonAsync("edificio/" + edificio.id, edificio);
                MessageBox.Show("Los datos se actualizaron con exito");
                A_AdministrarEdificio mse = new A_AdministrarEdificio(tokenR);
                mse.Show();
                this.Close();

            }
            catch (HttpRequestException he)
            {
                MessageBox.Show("No se pudo conectar con la base de datos");
            }
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
