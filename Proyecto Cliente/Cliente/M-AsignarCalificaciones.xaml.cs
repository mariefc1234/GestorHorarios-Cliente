using Newtonsoft.Json.Linq;
using Proyecto_Cliente.clases;
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
    public partial class M_AsignarCalificaciones : Window
    {
        HttpClient client = new HttpClient();
        string tokenR;
        public M_AsignarCalificaciones(string tokenS)
        {
            tokenR = tokenS;
            client.BaseAddress = new Uri("http://127.0.0.1:5000/api/clase");
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Add("auth_token", tokenR);
            client.DefaultRequestHeaders.Accept.Add(
                new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json")
                );
            InitializeComponent();
            GetMaterias();
        }

        private async void GetMaterias()
        {
            var response = await client.GetStringAsync("clase/maestro");
            JObject json = JObject.Parse(response);
            var data = json.SelectToken("data");
            List<ClaseMaestro> listaClasesMaestro = new List<ClaseMaestro>();

            foreach (var datosClases in data)
            {
                string vNrc = (string)datosClases.SelectToken("nrc");
                string vMateria = (string)datosClases.SelectToken("Materia");
                int vIdGrupo = (int)datosClases.SelectToken("idGrupo");
                int vSemestre = (int)datosClases.SelectToken("Semestre");
                string vBloque = (string)datosClases.SelectToken("Bloque");


                listaClasesMaestro.Add(new ClaseMaestro() {
                    nrc = vNrc,
                    Materia = vMateria,
                    idGrupo = vIdGrupo,
                    Semestre = vSemestre,
                    Bloque = vBloque
                });
            }
            dgClases.ItemsSource = listaClasesMaestro;
        }

        public class ClaseMaestro
        {
            public string nrc { get; set; }
            public string Materia { get; set; }
            public int idGrupo { get; set; }
            public int Semestre { get; set; }
            public string Bloque { get; set; }

            public ClaseMaestro() { }
        }

        private void Button_ClickVerAlumnos(object sender, RoutedEventArgs e)
        {

            ClaseMaestro claseN = dgClases.SelectedItem as ClaseMaestro;
            if (claseN == null)
            {
                MessageBox.Show("Debes seleccionar una clase primero");
            }
            else
            {
                M_AsignarCalificacionesAlumnos maca = new M_AsignarCalificacionesAlumnos(tokenR,claseN.idGrupo,claseN.nrc);
                maca.Show();
                this.Close();
            }
        }

        private void Button_ClickSalir(object sender, RoutedEventArgs e)
        {
            PrincipalMaestro psm = new PrincipalMaestro(tokenR);
            psm.Show();
            this.Close();
        }
    }
}
