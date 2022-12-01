using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
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
    /// <summary>
    /// Lógica de interacción para A_ModificarEdificio.xaml
    /// </summary>
    public partial class A_ModificarEdificio : Window
    {
        int idEdificio;
        string nombreEdificio;
        int noPisos;
        public A_ModificarEdificio( int id, string nombre, int pisos)
        {
            InitializeComponent();
            tbNombre.Text = nombre;
            cbPisos.SelectedIndex = pisos-1;

            idEdificio = id;
            nombreEdificio = nombre;
            noPisos = pisos;
        }

        private void Button_ClickCancelar(object sender, RoutedEventArgs e)
        {
            MostrarEdificios mse = new MostrarEdificios();
            mse.Show();
            this.Close();

        }

        private void Button_ClickGuardar(object sender, RoutedEventArgs e)
        {
            int varPisos= cbPisos.SelectedIndex - 1;
            string varNombre = tbNombre.Text;

            var url = "http://127.0.0.1:5000/api/edificio/"+idEdificio;
            var json = "{    \"nombre\": \""+ varNombre+"\",   \"pisos\": "+ varPisos + "}";
            var client = new RestClient(url);
            var request = new RestRequest();
            request.Method = Method.Patch;

            request.AddHeader("content-type", "application/json");
            request.AddParameter("application/json", json, ParameterType.RequestBody);
            var response = client.Execute(request);

            if (response.StatusCode.ToString().Equals("OK"))
            {
                MessageBox.Show("Modificado con exito");
            }
        }
    }
}
