using Newtonsoft.Json;
using Proyecto_Cliente.Cliente;
using RestSharp;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Proyecto_Cliente
{

    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button_IniciarSesion(object sender, RoutedEventArgs e)
        {
            int vMatricula = tbMatricula.Text.Length;
            int vContrasenia = psbContrasenia.Password.Length;

            if (vMatricula == 0 || vContrasenia == 0)
            {
                MessageBox.Show("campos vacios");
            }
            else
            {
                MessageBox.Show("vas bien puto");

                /*
                var url = "http://127.0.0.1:5000/api/login";

                var json = "{\"correo\": \""+tbMatricula.Text+"\",\"password\": \""+psbContrasenia.Password+"\"}"; */

                try
                {
                    var url = "http://127.0.0.1:5000/api/login";
                    var json = "{\"correo\": \"" + tbMatricula.Text + "\",\"password\": \"" + psbContrasenia.Password + "\"}";
                    var client = new RestClient(url);
                    var request = new RestRequest();
                    request.Method = Method.Post;

                    request.AddHeader("content-type", "application/json");
                    request.AddParameter("application/json", json, ParameterType.RequestBody);
                    var response = client.Execute(request);


                    if (response.StatusCode.ToString().Equals("OK"))
                    {
                        MessageBox.Show("ohh si");
                        AgregarArea agregarArea = new AgregarArea();
                        agregarArea.Show();
                        this.Close();
                    }
                    else
                    {
                        MessageBox.Show("ohh no");
                    }

                }
                catch (Exception ex)
                {
                    MessageBox.Show("nel prro");
                }
            }
        }

        private void Button_Cancelar(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
