using MaterialDesignThemes.Wpf;
using Newtonsoft.Json.Linq;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading;
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
    public partial class A_AgregarPeriodo : Window
    {
        HttpClient client = new HttpClient();
        string tokenR;
        public A_AgregarPeriodo(string tokenS)
        {
            tokenR = tokenS;
            client.BaseAddress = new Uri("http://127.0.0.1:5000/api/periodo");
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Add("authtoken", tokenR);
            client.DefaultRequestHeaders.Accept.Add(
                new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json")
                );
            InitializeComponent();
        }

        private void Button_clickGuardar(object sender, RoutedEventArgs e)
        {
            try
            {
                String vFechaInicio = transformarFecha(dpFechaInicio.SelectedDate.Value.ToShortDateString());
                String vFechaFin = transformarFecha(dpFechaFin.SelectedDate.Value.ToShortDateString());
                String vFechaOrdinario = transformarFecha(dpFechaOrdinario.SelectedDate.Value.ToShortDateString());
                String vFechaExtra = transformarFecha(dpFechaExtra.SelectedDate.Value.ToShortDateString());
                var periodo = new Periodo()
                {
                    fechaInicio = vFechaInicio,
                    fechaFin = vFechaFin,
                    fechaOrdinario = vFechaOrdinario,
                    fechaExtra = vFechaExtra
                };
                this.GuardarPeriodo(periodo);
            }
            catch (InvalidOperationException io)
            {
                MessageBox.Show("seleccione una fecha");
                Console.WriteLine(io);
            }
        }

        private async void GuardarPeriodo(Periodo periodo)
        {
            try
            {
                var response = await client.PostAsJsonAsync("periodo", periodo);
                if (response.StatusCode.ToString() == "OK")
                {
                    MessageBox.Show("Periodo guardado con exito");
                    dpFechaExtra.SelectedDate = null;
                    dpFechaFin.SelectedDate = null;
                    dpFechaOrdinario.SelectedDate = null;
                    dpFechaInicio.SelectedDate = null;
                }
                else
                {
                    MessageBox.Show("Error al tratar de guardar, intentelo mas tarde");
                }
            }
            catch (HttpRequestException he)
            {
                MessageBox.Show("No se pudo conectar con la base de datos");
                Console.WriteLine(he);
            }
        }

        private void Button_clickSalir(object sender, RoutedEventArgs e)
        {
            A_AdministrarPeriodo adp = new A_AdministrarPeriodo(tokenR);
            adp.Show();
            this.Close();
        }

        public class Periodo
        {
            public string fechaInicio { get; set; }
            public string fechaFin { get; set; }
            public string fechaOrdinario { get; set; }
            public string fechaExtra { get; set; }
            public Periodo() { }
        }

        public String transformarFecha(String fecha)
        {
            String fechaFinal;
            String dia;
            String mes;
            String año;
            char delimitador = '/';
            string[] valores = fecha.Split(delimitador);
            dia = valores[0];
            mes = valores[1];
            año = valores[2];
            fechaFinal = año + "/" + mes + "/" + dia;
            return fechaFinal;
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
