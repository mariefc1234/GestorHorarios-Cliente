using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RestSharp;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Script.Serialization;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using MessageBox = System.Windows.MessageBox;

namespace Proyecto_Cliente.Cliente
{
    public partial class MostrarEdificios : Window
    {
        List<Edificio> listaEdificios = new List<Edificio>();

        public MostrarEdificios()
        {
            InitializeComponent();
            GetEdificios();
        }


        public void GetEdificios()
        {
            var url = "http://127.0.0.1:5000/api/edificio";
            var client = new RestClient(url);
            var request = new RestRequest();
            request.Method = Method.Get;
            var response = client.Execute(request);
            JObject json = JObject.Parse(response.Content);

            var data = json.SelectToken("data");
            string nombreF;
            int idF;
            int pisosF;

            if (response.StatusCode.ToString().Equals("OK")){

                foreach (var datosEdificio in data)
                {
                    idF = (int)datosEdificio.SelectToken("id");
                    nombreF = (string)datosEdificio.SelectToken("nombre");
                    pisosF = (int)datosEdificio.SelectToken("pisos");
                    listaEdificios.Add(new Edificio() { id = idF, noPisos = pisosF, nombre = nombreF });
                }
                dgEdificios.ItemsSource = listaEdificios;
            }
            else
            {
                MessageBox.Show("No se pudo obtener la informacion");
            }
        }
        public class Edificio
        {
            public int id { get; set; }
            public int noPisos { get; set; }
            public string nombre { get; set; }
            public Edificio(int id, int noPisos, string nombre)
            {
                this.id = id;
                this.noPisos = noPisos;
                this.nombre = nombre;
            }
            public Edificio() { }
        }

        private void Button_ClickMostrarDetalles(object sender, RoutedEventArgs e)
        {
            Edificio edificioN = dgEdificios.SelectedItem as Edificio;
            if (edificioN == null)
            {
                MessageBox.Show("Debes seleccionar un edificio primero");
            }
            else
            {
                string detalles = "Nombre Edificio: " + edificioN.nombre+ " \nNumero de pisos: " + edificioN.noPisos;
                MessageBox.Show(detalles);
            }
        }

        private void Button_ClickModificarEdificio(object sender, RoutedEventArgs e)
        {
            Edificio edificioN = dgEdificios.SelectedItem as Edificio;
            if (edificioN == null)
            {
                MessageBox.Show("Debes seleccionar un edificio primero");
            }
            else
            {
                int id = edificioN.id;
                string nombre = edificioN.nombre;
                int noPisos = edificioN.noPisos;

                A_ModificarEdificio mde = new A_ModificarEdificio(id,nombre,noPisos);
                mde.Show();
                this.Close();
            }
        }

        private void Button_ClickEliminar(object sender, RoutedEventArgs e)
        {
            Edificio edificioN = dgEdificios.SelectedItem as Edificio;
            int idEdificio = edificioN.id;
            if (edificioN == null)
            {
                MessageBox.Show("Debes seleccionar un edificio primero");
            }
            else
            {
                DialogResult dialogResult = System.Windows.Forms.MessageBox.Show("Are you sure?", "Delete Confirmation", MessageBoxButtons.YesNo);
                if (dialogResult.ToString().Equals("Yes"))
                {
                    var url = "http://127.0.0.1:5000/api/edificio/"+idEdificio;
                    var client = new RestClient(url);
                    var request = new RestRequest();
                    request.Method = Method.Delete;
                    var response = client.Execute(request);

                    if (response.StatusCode.ToString().Equals("OK"))
                    {
                        MessageBox.Show("Edificio eliminado con exito");
                        // no se pudo banda ayuda
                    }
                    else
                    {
                        MessageBox.Show("No se pudo conectar con el servidor");
                    }
                }
            }
        }

        private void Button_ClickSalir(object sender, RoutedEventArgs e)
        {
            AdministrarEdificio ade = new AdministrarEdificio();
            ade.Show();
            this.Close();
        }
    }
}
