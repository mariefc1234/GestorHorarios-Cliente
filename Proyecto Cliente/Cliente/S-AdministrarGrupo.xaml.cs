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

                foreach(var area in listaArea)
                {
                    if(vIdArea == area.id)
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
                    id=idArea,
                    nombre =vNombreArea
                });
            }
        }

    }
}
