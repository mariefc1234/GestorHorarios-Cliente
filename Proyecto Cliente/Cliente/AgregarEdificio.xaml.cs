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
using static Proyecto_Cliente.Cliente.A_AdministrarEdificio;

namespace Proyecto_Cliente.Cliente
{
    public partial class AgregarEdificio : Window
    {
        HttpClient client = new HttpClient();
        string tokenR;
        public AgregarEdificio(string tokenS)
        {
            client.BaseAddress = new Uri("http://127.0.0.1:5000/api/edificio");
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Add("authtoken", tokenR);
            client.DefaultRequestHeaders.Accept.Add(
                new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json")
                );
            InitializeComponent();
            tokenR = tokenS;
        }

        private async void GuardarEdificio(Edificio edificio)
        {
            try
            {
                await client.PostAsJsonAsync("edificio", edificio);
            }
            catch (HttpRequestException he)
            {
                MessageBox.Show("No se pudo conectar con la base de datos");
            }
        }


        private void Button_Guardar(object sender, RoutedEventArgs e)
        {
            if (tbNombre.Text.Length == 0 || cbPisos.SelectedIndex == -1)
            {
                MessageBox.Show("Ingresa datos");
            }
            else
            {
                var edificio = new Edificio()
                {
                    
                    nombre = tbNombre.Text,
                    pisos = Convert.ToInt32(cbPisos.SelectedIndex+1)
                };


                this.GuardarEdificio(edificio);

                MessageBox.Show("Area guardada con exito");
            }
            tbNombre.Text = "";
            cbPisos.SelectedValue = null;
        }

        public class Edificio
        {
            public int pisos { get; set; }
            public string nombre { get; set; }
            public Edificio() { }
        }

        private void Button_Cancelar(object sender, RoutedEventArgs e)
        {
            A_AdministrarEdificio ade = new A_AdministrarEdificio(tokenR);
            ade.Show();
            this.Close();
        }
    }
}
