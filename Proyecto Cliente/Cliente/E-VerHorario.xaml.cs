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
    public partial class E_VerHorario : Window
    {
        HttpClient client = new HttpClient();
        string tokenR;
        public E_VerHorario(string tokenS)
        {
            tokenR = tokenS;
            client.BaseAddress = new Uri("http://127.0.0.1:5000/api/horario");
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Add("auth_token", tokenS);
            client.DefaultRequestHeaders.Accept.Add(
                new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json")
                );
            InitializeComponent();
            GetHorario();
        }

        public async void GetHorario()
        {
            var response = await client.GetStringAsync("horario/estudiante");
            JObject json = JObject.Parse(response);
            var data = json.SelectToken("data");
            var lunes = data.SelectToken("lunes");
            var martes = data.SelectToken("martes");
            var miercoles = data.SelectToken("miercoles");
            var jueves = data.SelectToken("jueves");
            var viernes = data.SelectToken("viernes");

            int nLunes = lunes.Count();
            string vLunes = GetDias(nLunes, lunes);
            lbLunes.Content = vLunes;

            int nMartes = martes.Count();
            string vMartes = GetDias(nMartes, martes);
            lbMartes.Content = vMartes;

            int nMiercoles = miercoles.Count();
            string vMiercoles = GetDias(nMiercoles, miercoles);
            lbMiercoles.Content = vMiercoles;

            int nJueves = jueves.Count();
            string vJueves = GetDias(nJueves, jueves);
            lbJueves.Content = vJueves;

            int nViernes = viernes.Count();
            string vViernes = GetDias(nViernes, viernes);
            lbViernes.Content = vViernes;
        }

        public class Clase
        {
            public string nrc { get; set; }
            public string horaInicio { get; set; }
            public string horaFin { get; set; }
            public string materia { get; set; }
            public string salon { get; set; }
            public string edificio { get; set; }
        }

        public string GetDias(int totalMateriasDia, dynamic diaLista)
        {
            string textDia = "";
            if (totalMateriasDia > 0)
            {
                List<Clase> listaDia = new List<Clase>();
                int contadorDia = totalMateriasDia;
                int contAuxiliar = 0;

                foreach (var diaV in diaLista)
                {
                    string vNrc = (string)diaV.SelectToken("nrc");
                    string vHoraInicio = (string)diaV.SelectToken("horaInicio");
                    string vHoraFin = (string)diaV.SelectToken("horaFIn");
                    string vMateria = (string)diaV.SelectToken("materia");
                    string vSalon = (string)diaV.SelectToken("salon");
                    string vEdificio = (string)diaV.SelectToken("edificio");

                    listaDia.Add(new Clase()
                    {
                        nrc = vNrc,
                        horaInicio = vHoraInicio,
                        horaFin = vHoraFin,
                        materia = vMateria,
                        salon = vSalon,
                        edificio = vEdificio
                    });

                    if (contAuxiliar < contadorDia)
                    {
                        textDia += string.Join("\n", "NRC: " + listaDia[contAuxiliar].nrc + "\n" +
                            "Nombre: " + listaDia[contAuxiliar].materia + "\n" + "Hora Inicio: " + listaDia[contAuxiliar].horaInicio +
                            "\n" + "Hora Fin: " + listaDia[contAuxiliar].horaFin + "\n" + "Salon: " + listaDia[contAuxiliar].salon +
                            "\n" + "Edificio: " + listaDia[contAuxiliar].edificio + "\n\n");
                        contAuxiliar++;
                    }
                }
            }
            return textDia;
        }

        private void Button_ClickRegresar(object sender, RoutedEventArgs e)
        {
            PrincipalAlumno psa = new PrincipalAlumno(tokenR);
            psa.Show();
            this.Close();
        }
    }
}
