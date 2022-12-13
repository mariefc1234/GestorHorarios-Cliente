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
    public partial class M_AsignarCalificacionesAlumnos : Window
    {
        HttpClient client = new HttpClient();
        int idGrupo;
        string tokenR;
        string Nrc;
        public M_AsignarCalificacionesAlumnos(string tokenS, int idG, string vNrc)
        {
            tokenR = tokenS;
            idGrupo = idG;
            Nrc = vNrc;

            client.BaseAddress = new Uri("http://127.0.0.1:5000/api/estudiante");
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Add("auth_token", tokenR);
            client.DefaultRequestHeaders.Accept.Add(
                new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json")
                );
            InitializeComponent();
            GetEstudiantes();
        }

        public async void GetEstudiantes()
        {
            var response = await client.GetStringAsync("estudiante/nocalificacion/" + Nrc);
            JObject json = JObject.Parse(response);
            var data = json.SelectToken("data");
            var estudiantes = data.SelectToken("estudiantes");

            List<Estudiante> listaEstudiantes = new List<Estudiante>();

            foreach (var datosEstudiantes in estudiantes)
            {
                int vUid = (int)datosEstudiantes.SelectToken("uid");
                string vNombre = (string)datosEstudiantes.SelectToken("nombre");
                int vIdEstGru = (int)datosEstudiantes.SelectToken("idEstGru");

                listaEstudiantes.Add(new Estudiante() { 
                    uid = vUid,
                    nombre = vNombre,
                    idEstGru = vIdEstGru,
                });
            }
            dgEstudiantes.ItemsSource = listaEstudiantes;
        }

        public class Estudiante
        {
            public int uid { get; set; }
            public string nombre { get; set; }
            public int idEstGru { get; set; }
            public Estudiante() { }
        }

        public class Calificacion
        {
            public int idEstudiante { get; set; }
            public int calificacion { get; set; }
            public Calificacion() { }
        }


        private void Button_ClickRegresar(object sender, RoutedEventArgs e)
        {
            M_AsignarCalificaciones maca = new M_AsignarCalificaciones(tokenR);
            maca.Show();
            this.Close();
        }

        private void Button_ClickGuardar(object sender, RoutedEventArgs e)
        {
            Estudiante estudianteN = dgEstudiantes.SelectedItem as Estudiante;

            if (tbCalificacion.Text.Length == 0 || estudianteN == null)
            {
                MessageBox.Show("Ingresa datos");
            }
            else
            {
                try
                {
                    var calificacion = new Calificacion()
                    {
                        idEstudiante = estudianteN.uid,
                        calificacion = Convert.ToInt32(tbCalificacion.Text)
                    };
                    this.GuardarCalificacion(calificacion);
                }
                catch (FormatException fe)
                {
                    Console.WriteLine(fe);
                    MessageBox.Show("Calificacion solo adminite enteros");
                }
            }
        }

        private async void GuardarCalificacion(Calificacion calificacion)
        {
            try
            {
                var response = await client.PostAsJsonAsync("calificacion/clase/"+Nrc, calificacion);

                if (response.StatusCode.ToString() == "OK")
                {
                    MessageBox.Show("Se agrego la calificacion correctamente");
                    GetEstudiantes();
                }
                else
                {
                    MessageBox.Show("error al calificar");
                }
            }
            catch (HttpRequestException he)
            {
                MessageBox.Show("No se pudo conectar con la base de datos");
            }
        }
    }
}
