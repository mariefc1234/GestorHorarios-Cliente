using Newtonsoft.Json.Linq;
using Proyecto_Cliente.clases;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Reflection;
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
    public partial class S_AgregarGrupo : Window
    {
        HttpClient client = new HttpClient();
        string tokenR;
        public S_AgregarGrupo(string tokenS)
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
        }

        public async void GetAreas()
        {
            var response = await client.GetStringAsync("area");
            JObject json = JObject.Parse(response);
            var data = json.SelectToken("data");
            List<Area> listaArea = new List<Area>();

            foreach (var datosArea in data)
            {
                int idArea = (int)datosArea.SelectToken("id");
                string nombreArea = (string)datosArea.SelectToken("nombre");

                listaArea.Add(new Area() { id = idArea, nombre = nombreArea });
            }
            dgArea.ItemsSource = listaArea;
        }

        private void Button_clickRegresar(object sender, RoutedEventArgs e)
        {
            S_AdministrarGrupo adg = new S_AdministrarGrupo(tokenR);
            adg.Show();
            this.Close();
        }

        private void Button_ClickGuardar(object sender, RoutedEventArgs e)
        {

            Area areaN = dgArea.SelectedItem as Area;

            if (tbBloque.Text.Length == 0 || areaN == null)
            {
                MessageBox.Show("Campos incompletos");
            }
            else
            {
                var grupoAgregar = new GrupoAgregar()
                {

                    idArea = Convert.ToInt32(areaN.id),
                    bloque = tbBloque.Text
                };
                this.GuardarGrupo(grupoAgregar);
            }
            tbBloque.Text = "";
            dgArea.SelectedValue = null;
        }

        private async void GuardarGrupo(GrupoAgregar grupoAgregar)
        {
            try
            {
               var response =  await client.PostAsJsonAsync("grupo", grupoAgregar);

                if (response.StatusCode.ToString() == "OK")
                {
                    MessageBox.Show("Grupo guardado con exito");
                }
                else
                {
                    MessageBox.Show("Error al guardar "+response.ToString());
                }
            }
            catch (HttpRequestException he)
            {
                MessageBox.Show("No se pudo conectar con la base de datos");
            }
        }

        public class GrupoAgregar
        {
            public int idArea { get; set; }
            public string bloque { get; set; }

            public GrupoAgregar() { }
        }
    }
}
