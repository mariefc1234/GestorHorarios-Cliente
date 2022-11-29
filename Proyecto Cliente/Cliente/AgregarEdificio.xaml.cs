using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
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
    public partial class AgregarEdificio : Window
    {
        public AgregarEdificio()
        {
            InitializeComponent();
        }

        private void Button_Cancelar(object sender, RoutedEventArgs e)
        {
            PrincipalAdministrador psa = new PrincipalAdministrador();
            psa.Show();
            this.Close();
        }

        private void Button_Guardar(object sender, RoutedEventArgs e)
        {


            String vNombre = tbNombre.Text;
            int vPisos = cbPisos.SelectedIndex + 1;

            if(vNombre.Length == 0 || vPisos == 0)
            {
                MessageBox.Show("Campos vacios");
            }
            else
            {
                var url = "http://127.0.0.1:5000/api/edificio";
                var json = "{    \"nombre\": \""+ vNombre +"\",    \"pisos\": "+vPisos+"}";
                var client = new RestClient(url);
                var request = new RestRequest();
                request.Method = Method.Post;

                request.AddHeader("content-type", "application/json");
                request.AddParameter("application/json", json, ParameterType.RequestBody);
                var response = client.Execute(request);
                MessageBox.Show(response.StatusCode.ToString());

                if (response.StatusCode.ToString().Equals("Created"))
                {
                    MessageBox.Show("SEXOOOO");
                    tbNombre.Clear();
                    cbPisos.SelectedIndex = -1;
                }
            }
        }
    }
}
