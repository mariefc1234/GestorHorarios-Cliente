using Microsoft.VisualBasic;
using Newtonsoft.Json;
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
using static Proyecto_Cliente.Cliente.A_AdministrarArea;

namespace Proyecto_Cliente.Cliente
{
    public partial class A_AdministrarArea : Window
    {
        HttpClient client = new HttpClient();
        String tokenR;

        public A_AdministrarArea(String tokenS)
        {
            client.BaseAddress = new Uri("http://127.0.0.1:5000/api/area");
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Add("authtoken", tokenR);
            client.DefaultRequestHeaders.Accept.Add(
                new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json")
                );

            InitializeComponent();
            GetAreas();
            tokenR = tokenS;
        }

        private void Button_ClickMostrarArea(object sender, RoutedEventArgs e)
        {
            Area areaN = dgArea.SelectedItem as Area;
            if (areaN == null)
            {
                MessageBox.Show("Debes seleccionar una area primero");
            }
            else
            {
                string detalles = "Nombre Area: " + areaN.nombre;
                MessageBox.Show(detalles);
            }
        }

        private void Button_ClickModificarArea(object sender, RoutedEventArgs e)
        {

            Area areaN = dgArea.SelectedItem as Area;
            if (areaN == null)
            {
                MessageBox.Show("Debes seleccionar una area primero");
            }
            else
            {
                string nuevoNombre = Interaction.InputBox("Ingresa el nuevo valor?", "Actualizar Area", areaN.nombre);
                areaN.nombre = nuevoNombre;

                MessageBox.Show(nuevoNombre);
                MessageBox.Show(areaN.nombre);
                this.ActualizarArea(areaN);
            }
        }

        private void Button_ClickEliminar(object sender, RoutedEventArgs e)
        {
            Area areaN = dgArea.SelectedItem as Area;
            if (areaN == null)
            {
                MessageBox.Show("Debes seleccionar una area primero");
            }
            else
            {
                int id = areaN.idArea;
                this.EliminarArea(areaN.idArea);
            }
        }

        private void Button_ClickAgregarArea(object sender, RoutedEventArgs e)
        {
            if (tbNombreArea.Text.Length == 0)
            {
                MessageBox.Show("Ingresa datos");
            }
            else
            {
                var area = new Area()
                {
                    idArea = Convert.ToInt32(tbId.Text),
                    nombre = tbNombreArea.Text
                };
                if (area.idArea == 0)
                {
                    this.GuardarArea(area);
                    MessageBox.Show("Area guardada con exito");
                }
                else
                {
                    this.ActualizarArea(area);
                    MessageBox.Show("Area actualizada con exito");
                }
            }
            GetAreas();
            tbId.Text = 0.ToString();
            tbNombreArea.Text = "";
        }

        private void Button_ClickCancelar(object sender, RoutedEventArgs e)
        {
            PrincipalAdministrador psa = new PrincipalAdministrador(tokenR);
            psa.Show();
            this.Close();
        }

        private class Area
        {
            public int idArea { get; set; }
            public string nombre { get; set; }

            public Area()
            {

            }
        }

        private async void GetAreas()
        {
            var response = await client.GetStringAsync("area");
            JObject json = JObject.Parse(response);
            var data = json.SelectToken("data");
            List<Area> listaAreas = new List<Area>();

            foreach (var datosArea in data)
            {
                int idF = (int)datosArea.SelectToken("id");
                string nombreF = (string)datosArea.SelectToken("nombre");
                listaAreas.Add(new Area() { idArea = idF, nombre = nombreF });
            }
            dgArea.ItemsSource = listaAreas;
        }

        private async void GuardarArea(Area area)
        {
            try
            {
                await client.PostAsJsonAsync("area", area);
            }
            catch (HttpRequestException he)
            {
                MessageBox.Show("No se pudo conectar con la base de datos");
            }
        }

        private async void ActualizarArea(Area area)
        {
            try
            {
                await client.PutAsJsonAsync("area/" + area.idArea, area);
                GetAreas();
            }
            catch (HttpRequestException he)
            {
                MessageBox.Show("No se pudo conectar con la base de datos");
            }
        }

        private async void EliminarArea(int areaId)
        {
            try
            {
                await client.DeleteAsync("area/" + areaId);
                GetAreas();
            }
            catch (HttpRequestException he)
            {
                MessageBox.Show("No se pudo conectar con la base de datos");
            }
        }

    }
}
