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
    public partial class E_VerCalificaciones : Window
    {
        string tokenR;
        HttpClient client = new HttpClient();
        public E_VerCalificaciones(string tokenS)
        {
            tokenR = tokenS;
            client.BaseAddress = new Uri("http://127.0.0.1:5000/api/calificacion");
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Add("auth_token", tokenR);
            client.DefaultRequestHeaders.Accept.Add(
                new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json")
                );
            InitializeComponent();
            GetCalificaciones();
        }

        public async void GetCalificaciones()
        {
            var response = await client.GetStringAsync("calificacion");
            JObject json = JObject.Parse(response);
            var data = json.SelectToken("data");
            var calificaciones = data.SelectToken("calificaiones");
            List<Calificacion> listaCalificaciones = new List<Calificacion>();

            foreach (var datosCalificacion in calificaciones)
            {
                string vNrc = (string)datosCalificacion.SelectToken("nrc");
                int vCalificacion = (int)datosCalificacion.SelectToken("calificacion");
                string vMateria = (string)datosCalificacion.SelectToken("materia");

                listaCalificaciones.Add(new Calificacion() { 
                    nrc = vNrc,
                    calificacion = vCalificacion,
                    materia = vMateria
                    
                });
            }
            dgCalificaciones.ItemsSource = listaCalificaciones;
        }

        public class Calificacion
        {
            public string nrc { get; set; }
            public int calificacion { get; set; }

            public string materia { get; set; }

            public Calificacion() { }
        }

        private void Button_ClickRegresar(object sender, RoutedEventArgs e)
        {
            PrincipalAlumno psa = new PrincipalAlumno(tokenR);
            psa.Show();
            this.Close();
        }
    }
}
