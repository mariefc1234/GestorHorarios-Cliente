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

    public partial class A_AdministrarPeriodo : Window
    {
        HttpClient client = new HttpClient();
        string tokenR;
        public A_AdministrarPeriodo(string tokenS)
        {
            tokenR = tokenS;
            client.BaseAddress = new Uri("http://127.0.0.1:5000/api/periodo");
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Add("authtoken", tokenR);
            client.DefaultRequestHeaders.Accept.Add(
                new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json")
                );
            InitializeComponent();
            GetPeriodos();
        }

        //Botones
        private void Button_clickCrear(object sender, RoutedEventArgs e)
        {
            A_AgregarPeriodo agp = new A_AgregarPeriodo(tokenR);
            agp.Show();
            this.Close();
        }
        private void Button_clickBorrar(object sender, RoutedEventArgs e)
        {
            Periodo periodoN = dgPeriodos.SelectedItem as Periodo;
            if (periodoN == null)
            {
                MessageBox.Show("Debes seleccionar un edificio primero");
            }
            else
            {
                MessageBoxResult messageBoxResult = System.Windows.MessageBox.Show("Estas seguro de realizar esta accion? \nEsto eliminara toda informacion de periodo, \ngrupos, calificaciones , clases y todo lo relacionado. ", "Confirmacion de eliminacion", System.Windows.MessageBoxButton.YesNo);
                if (messageBoxResult == MessageBoxResult.Yes)
                {
                    this.EliminarPeriodo(periodoN.id);
                }
            }
        }
        private void Button_clickActivar(object sender, RoutedEventArgs e)
        {
            Periodo periodoN = dgPeriodos.SelectedItem as Periodo;
            if (periodoN == null)
            {
                MessageBox.Show("Debes seleccionar un periodo primero");
            }
            else
            {
                MessageBoxResult messageBoxResult = System.Windows.MessageBox.Show("Estas seguro de realizar esta accion? \nSolo un periodo puede estar activo. ", "Confirmacion de eliminacion", System.Windows.MessageBoxButton.YesNo);
                if (messageBoxResult == MessageBoxResult.Yes)
                {
                    this.ActivarPeriodo(periodoN.id, periodoN);
                }
            }
        }
        private void Button_clickModificar(object sender, RoutedEventArgs e)
        {
            Periodo periodoN = dgPeriodos.SelectedItem as Periodo;
            if (periodoN == null)
            {
                MessageBox.Show("Debes seleccionar un periodo primero");
            }
            else
            {
                A_ModificarPeriodo mdp = new A_ModificarPeriodo(tokenR,periodoN.id,periodoN.fechaInicio,periodoN.fechaOrdinario,periodoN.fechaExtra,periodoN.fechaFin);
                mdp.Show();
                this.Close();
            }
        }

        //Funcione de ayuda
        public async void GetPeriodos()
        {
            var response = await client.GetStringAsync("periodo");
            JObject json = JObject.Parse(response);
            var data = json.SelectToken("data");
            List<Periodo> listaperiodos = new List<Periodo>();

            foreach (var datosPeriodo in data)
            {
                int idPeriodo = (int)datosPeriodo.SelectToken("id");
                string vFechaInicio = (string)datosPeriodo.SelectToken("fechaInicio");
                string vFechaFin = (string)datosPeriodo.SelectToken("fechaFin");
                string vFechaOrdinario = (string)datosPeriodo.SelectToken("fechaOrdinario");
                string vFechaExtra = (string)datosPeriodo.SelectToken("fechaExtra");
                int vActivo = (int)datosPeriodo.SelectToken("activo");
                string vActivotext;
                if (vActivo.ToString() == "0")
                {
                    vActivotext = "No";
                }
                else
                {
                    vActivotext = "Si";
                }

                listaperiodos.Add(new Periodo()
                {
                    id = idPeriodo,
                    fechaInicio = vFechaInicio,
                    fechaFin = vFechaFin,
                    fechaOrdinario = vFechaOrdinario,
                    fechaExtra = vFechaExtra,
                    activo = vActivo,
                    activo2 = vActivotext
                });
            }
            dgPeriodos.ItemsSource = listaperiodos;
        }
        private async void EliminarPeriodo(int periodoId)
        {
            try
            {
                var response = await client.DeleteAsync("periodo/" + periodoId);
                if(response.StatusCode.ToString() == "OK")
                {
                    MessageBox.Show("Periodo eliminado correctamente");
                    GetPeriodos();
                }
                else
                {
                    MessageBox.Show("No se puedo eliminar porfavor intente mas tarde");
                }
            }
            catch (HttpRequestException he)
            {
                MessageBox.Show("No se pudo conectar con la base de datos");
            }
        }
        private async void ActivarPeriodo(int periodoId,Periodo periodo)
        {
            try
            {
                var response = await client.PutAsJsonAsync("periodo/activar/"+periodoId,periodo);
                if (response.StatusCode.ToString() == "OK")
                {
                    MessageBox.Show("Periodo activado correctamente");
                    GetPeriodos();
                }
                else
                {
                    MessageBox.Show("No se puede eliminar porfavor intente mas tarde");
                }
            }
            catch (HttpRequestException he)
            {
                MessageBox.Show("No se pudo conectar con la base de datos");
            }
        }


        //clases
        public class Periodo
        {
            public int id { get; set; }
            public string fechaInicio { get; set; }
            public string fechaFin { get; set; }
            public string fechaOrdinario { get; set; }
            public string fechaExtra { get; set; }
            public int activo { get; set; }
            public string activo2 { get; set; }
            public Periodo() { }
        }
    }
}
