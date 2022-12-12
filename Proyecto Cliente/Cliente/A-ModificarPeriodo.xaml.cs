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
    public partial class A_ModificarPeriodo : Window
    {
        HttpClient client = new HttpClient();
        string tokenR;
        int id;
        public A_ModificarPeriodo(string tokenS,int idPeriodo,string fechaInicio, string fechaOrdinario, string fechaExtra, string fechaFin)
        {
            id = idPeriodo;
            tokenR = tokenS;
            client.BaseAddress = new Uri("http://127.0.0.1:5000/api/periodo");
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Add("authtoken", tokenR);
            client.DefaultRequestHeaders.Accept.Add(
                new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json")
                );
            InitializeComponent();

            var fInicio = DateTime.Parse(fechaInicio);
            var fOdinario = DateTime.Parse(fechaOrdinario);
            var fExtra = DateTime.Parse(fechaExtra);
            var fFin = DateTime.Parse(fechaFin);

            dpFechaInicio.SelectedDate = fInicio;
            dpFechaOrdinario.SelectedDate = fOdinario;
            dpFechaExtra.SelectedDate = fExtra;
            dpFechaFin.SelectedDate = fFin;
        }

        private void Button_clickSalir(object sender, RoutedEventArgs e)
        {
            A_AdministrarPeriodo adp = new A_AdministrarPeriodo(tokenR);
            adp.Show();
            this.Close();
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
                this.ActualizarPeriodo(periodo);
            }
            catch (InvalidOperationException io)
            {
                MessageBox.Show("Seleccione las fechas faltantes");
                Console.WriteLine(io);
            }
        }

        private async void ActualizarPeriodo(Periodo periodo)
        {
            try
            {
                var response = await client.PutAsJsonAsync("periodo/" +id, periodo);

                if(response.StatusCode.ToString() == "OK")
                {
                    MessageBox.Show("Los datos se actualizaron con exito");
                    A_AdministrarPeriodo adp = new A_AdministrarPeriodo(tokenR);
                    adp.Show();
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Error al actualizar");
                }
            }
            catch (HttpRequestException he)
            {
                MessageBox.Show("No se pudo conectar con la base de datos");
            }
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

        public class Periodo
        {
            public string fechaInicio { get; set; }
            public string fechaFin { get; set; }
            public string fechaOrdinario { get; set; }
            public string fechaExtra { get; set; }
            public Periodo() { }
        }
    }
}
