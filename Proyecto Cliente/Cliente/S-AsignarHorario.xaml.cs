using Newtonsoft.Json.Linq;
using Proyecto_Cliente.clases;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Runtime.Remoting.Metadata.W3cXsd2001;
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
    public partial class S_AsignarHorario : Window
    {
        HttpClient client = new HttpClient();
        string tokenR;

        private DateTime? _Time;
        private DateTime? _Time2;
        public DateTime? Time
        {
            get { return _Time; }
            set
            {
                _Time = value;
            }
        }
        public DateTime? Time2
        {
            get { return _Time2; }
            set
            {
                _Time2 = value;
            }
        }


        public S_AsignarHorario(string tokenS)
        {
            tokenR = tokenS;
            client.BaseAddress = new Uri("http://127.0.0.1:5000/api/horario");
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Add("authtoken", tokenR);
            client.DefaultRequestHeaders.Accept.Add(
                new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json")
                );
            InitializeComponent();
            GetClases();
            GetSalones();
        }

        public async void GetClases()
        {
            var response = await client.GetStringAsync("clase");
            JObject json = JObject.Parse(response);
            var data = json.SelectToken("data");
            var clase = data.SelectToken("clases");
            List<Clase> listaClases = new List<Clase>();

            foreach (var datosClases in clase)
            {
                string vNrc = (string)datosClases.SelectToken("nrc");
                string vMateria = (string)datosClases.SelectToken("materia");

                listaClases.Add(new Clase() { nrc = vNrc,materia = vMateria});
            }
            dgClases.ItemsSource = listaClases;
        }

        public async void GetSalones()
        {
            var response = await client.GetStringAsync("salon");
            JObject json = JObject.Parse(response);
            var data = json.SelectToken("data");
            List<Salon> listaSalones = new List<Salon>();

            foreach (var datosSalones in data)
            {
                int vIdSalon = (int)datosSalones.SelectToken("idSalon");
                int vIdEdificio = (int)datosSalones.SelectToken("idEdificio");
                string vNombre = (string)datosSalones.SelectToken("nombre");
                int vTotalCupo = (int)datosSalones.SelectToken("totalCupo");

                listaSalones.Add(new Salon() {
                    idSalon = vIdSalon,
                    idEdificio = vIdEdificio,
                    nombre = vNombre,
                    totalCupo = vTotalCupo
                });
            }
            dgSalones.ItemsSource = listaSalones;
        }

        public class Clase
        {
            public string nrc { get; set; }
            public string materia { get; set; }

            public Clase() { }
        }

        public class Salon
        {
            public int idSalon { get; set; }
            public int idEdificio { get; set; }
            public string nombre { get; set; }
            public int totalCupo { get; set; }
            public Salon()
            {

            }
        }

        public class Horario
        {
            public int idSemana { get; set; }
            public int idSalon { get; set; }
            public string horarioInicio { get; set; }
            public string horarioFin { get; set; }
            public string idClase { get; set; }
            public Horario() { }
        }

        private void Button_ClickGuardarAsignacion(object sender, RoutedEventArgs e)
        {

            try
            {
                string horaInicio = _Time.Value.ToString("HH:mm");
                string horaFin = _Time2.Value.ToString("HH:mm");
                int semana = cbDias.SelectedIndex + 1;

                Clase claseN = dgClases.SelectedItem as Clase;
                Salon salonN = dgSalones.SelectedItem as Salon;

                if (claseN == null || salonN == null || semana == 0)
                {
                    MessageBox.Show("Seleccion faltante");
                }
                else
                {
                    var horario = new Horario()
                    {
                        idSemana = semana,
                        idSalon = salonN.idSalon,
                        horarioInicio = horaInicio,
                        horarioFin = horaFin,
                        idClase = claseN.nrc
                    };
                    this.GuardarHorario(horario);
                }
            }
            catch (InvalidOperationException io)
            {
                MessageBox.Show("Debes seleccionar las horas inico y fin primero");
            }


        }


        private async void GuardarHorario(Horario horario)
        {
            try
            {
                var response = await client.PostAsJsonAsync("horario", horario);

                if (response.StatusCode.ToString() == "OK")
                {
                    MessageBox.Show("Horario guardado con exito");
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
    }
}
