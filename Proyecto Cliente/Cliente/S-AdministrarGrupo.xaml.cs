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
using System.Xml.Serialization;


namespace Proyecto_Cliente.Cliente
{
    public partial class S_AdministrarGrupo : Window
    {
        HttpClient client = new HttpClient();
        string tokenR;
        List<Area> listaArea = new List<Area>();
        public S_AdministrarGrupo(string tokenS)
        {
            tokenR = tokenS;
            client.BaseAddress = new Uri("http://127.0.0.1:5000/api/grupo");
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Add("authtoken", tokenR);
            client.DefaultRequestHeaders.Accept.Add(
                new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json")
                );
            InitializeComponent();
            GetAreas();
            GetGrupos();
        }


        //Bottones
        private void Button_clicCrear(object sender, RoutedEventArgs e)
        {
            S_AgregarGrupo agg = new S_AgregarGrupo(tokenR);
            agg.Show();
            this.Close();
        }
        private void Button_clicEliminar(object sender, RoutedEventArgs e)
        {

            Grupo grupoN = dgGrupos.SelectedItem as Grupo;
            if (grupoN == null)
            {
                MessageBox.Show("Debes seleccionar un grupo primero");
            }
            else
            {
                MessageBoxResult messageBoxResult = System.Windows.MessageBox.Show("Estas seguro?", "Confirmacion de eliminacion", System.Windows.MessageBoxButton.YesNo);
                if (messageBoxResult == MessageBoxResult.Yes)
                {
                    this.EliminarGrupo(grupoN.id);
                }
            }
        }
        private void Button_clicSalir(object sender, RoutedEventArgs e)
        {
            Prinicipal_Secretario pcs = new Prinicipal_Secretario(tokenR);
            pcs.Show();
            this.Close();
        }
        private void Button_clicAsignar(object sender, RoutedEventArgs e)
        {
            Grupo grupoN = dgGrupos.SelectedItem as Grupo;
            if (grupoN == null)
            {
                MessageBox.Show("Debes seleccionar un grupo primero");
            }
            else
            {
                S_AsignarEstudiantesGrupo aseg = new S_AsignarEstudiantesGrupo(tokenR, grupoN.semestre, grupoN.id);
                aseg.Show();
                this.Close();
            }
        }
        private void Button_clicCrearClase(object sender, RoutedEventArgs e)
        {
            Grupo grupoN = dgGrupos.SelectedItem as Grupo;
            if (grupoN == null)
            {
                MessageBox.Show("Debes seleccionar un grupo primero");
            }
            else
            {
                S_AgregarClase agc = new S_AgregarClase(tokenR, grupoN.id);
                agc.Show();
                this.Close();
            }
        }


        //Funciones de ayuda
        public async void GetGrupos()
        {
            var response = await client.GetStringAsync("grupo");
            JObject json = JObject.Parse(response);
            var data = json.SelectToken("data");
            List<Grupo> listaGrupos = new List<Grupo>();

            foreach (var datosGrupo in data)
            {
                int idGrupo = (int)datosGrupo.SelectToken("id");
                int vIdArea = (int)datosGrupo.SelectToken("idArea");
                int vSemestre = (int)datosGrupo.SelectToken("semestre");
                string vBloque = (string)datosGrupo.SelectToken("bloque");
                string vNombreArea = "";

                foreach (var area in listaArea)
                {
                    if (vIdArea == area.id)
                    {
                        vNombreArea = area.nombre;
                    }
                }

                listaGrupos.Add(new Grupo()
                {
                    id = idGrupo,
                    idArea = vIdArea,
                    semestre = vSemestre,
                    bloque = vBloque,
                    vNombreArea = vNombreArea
                });
            }
            dgGrupos.ItemsSource = listaGrupos;
        }
        public async void GetAreas()
        {
            var response = await client.GetStringAsync("area");
            JObject json = JObject.Parse(response);
            var data = json.SelectToken("data");

            foreach (var datosArea in data)
            {
                int idArea = (int)datosArea.SelectToken("id");
                string vNombreArea = (string)datosArea.SelectToken("nombre");

                listaArea.Add(new Area()
                {
                    id = idArea,
                    nombre = vNombreArea
                });
            }
        }
        private async void EliminarGrupo(int grupoId)
        {
            try
            {
                var response = await client.DeleteAsync("grupo/" + grupoId);

                if (response.StatusCode.ToString() == "OK")
                {
                    MessageBox.Show("Grupo eliminado correctamente");
                    GetGrupos();
                }
                else
                {
                    MessageBox.Show("Error al eliminar");
                }
            }
            catch (HttpRequestException he)
            {
                Console.WriteLine(he);
                MessageBox.Show("No se pudo conectar con la base de datos");
            }
        }

    }
}
