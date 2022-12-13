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
    public partial class S_VerHorarioSalon : Window
    {
        string tokenR;
        HttpClient client = new HttpClient();
        public S_VerHorarioSalon(string tokenS)
        {
            tokenR = tokenS;
            client.BaseAddress = new Uri("http://127.0.0.1:5000/api/salon");
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Add("auth_token", tokenR);
            client.DefaultRequestHeaders.Accept.Add(
                new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json")
                );
            InitializeComponent();
            GetSalones();
        }

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

                if (proyectorD == 1)
                {
                    tieneProyectorD = "Si";
                }
                else
                {
                    tieneProyectorD = "No";
                }
                listaSalones.Add(new Salon() { idSalon = idSD, idEdificio = idED, nombre = nombreD, proyector = proyectorD, totalCupo = totalCupoD, tieneProyector = tieneProyectorD });
            }
            dgSalones.ItemsSource = listaSalones;
        }

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

        private void Button_ClickVerHorario(object sender, RoutedEventArgs e)
        {
            Salon salonN = dgSalones.SelectedItem as Salon;
            if (salonN == null)
            {
                MessageBox.Show("Debes seleccionar un salon primero");
            }
            else
            {
                S_VerHorario svh = new S_VerHorario(tokenR,salonN.idSalon);
                svh.Show();
                dgSalones.SelectedItem = null;
            }
        }

        private void Button_ClickRegresar(object sender, RoutedEventArgs e)
        {
            Prinicipal_Secretario ps = new Prinicipal_Secretario(tokenR);
            ps.Show();
            this.Close();
        }
    }
}
