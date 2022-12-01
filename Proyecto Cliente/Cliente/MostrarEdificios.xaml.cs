using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Script.Serialization;
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
    public partial class MostrarEdificios : Window
    {
        List<Edificio> listaEdificios = new List<Edificio>();
        public MostrarEdificios()
        {
            InitializeComponent();
            GetEdificios();
            dgEdificios.ItemsSource = listaEdificios;
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
    }
}
