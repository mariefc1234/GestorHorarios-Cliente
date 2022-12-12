using MaterialDesignThemes.Wpf;
using Microsoft.VisualBasic;
using Newtonsoft.Json;
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
using static Proyecto_Cliente.Cliente.A_AdministrarEdificio;

namespace Proyecto_Cliente.Cliente
{
    public partial class AgregarEdificio : Window
    {
        HttpClient client = new HttpClient();
        string tokenR;
        public AgregarEdificio(string tokenS)
        {
            tokenR = tokenS;
            client.BaseAddress = new Uri("http://127.0.0.1:5000/api/edificio");
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Add("authtoken", tokenR);
            client.DefaultRequestHeaders.Accept.Add(
                new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json")
                );
            InitializeComponent();
        }

        private async void GuardarEdificio(Edificio edificio)
        {
            try
            {
                await client.PostAsJsonAsync("edificio", edificio);
            }
            catch (HttpRequestException he)
            {
                MessageBox.Show("No se pudo conectar con la base de datos");
            }
        }


        private void Button_ClickGuardar(object sender, RoutedEventArgs e)
        {
            if (tbNombre.Text.Length == 0 || cbPisos.SelectedIndex == -1)
            {
                MessageBox.Show("Ingresa datos");
            }
            else
            {
                var edificio = new Edificio()
                {
                    
                    nombre = tbNombre.Text,
                    pisos = Convert.ToInt32(cbPisos.SelectedIndex+1)
                };


                this.GuardarEdificio(edificio);

                MessageBox.Show("Edificio registrado con exito");
            }
            tbNombre.Text = "";
            cbPisos.SelectedValue = null;
        }

        private void Button_ClickRegresar(object sender, RoutedEventArgs e)
        {
            A_AdministrarEdificio ade = new A_AdministrarEdificio(tokenR);
            ade.Show();
            this.Close();
        }

        public class Edificio
        {
            public int pisos { get; set; }
            public string nombre { get; set; }
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
