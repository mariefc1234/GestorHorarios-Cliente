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
                MessageBox.Show("Algunos campos estan vacios");
            }
            else
            {
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

                    Console.WriteLine(response.StatusCode.ToString());


                    if (response.StatusCode.ToString().Equals("OK"))
                    {
                        PrincipalAdministrador psa = new PrincipalAdministrador();
                        psa.Show();
                        this.Close();

                        //Prinicipal_Secretario ps = new Prinicipal_Secretario();
                        //7ps.Show();
                        //this.Close();
                    }
                    else if (response.StatusCode.ToString().Equals("0"))
                    {
                        MessageBox.Show("No se pudo establecer conexion con el servidor");
                    }
                    else
                    {
                        MessageBox.Show("Credenciales no validas");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error no se pudo establecer conexion con el servidor");
                }
            }
        }

        private void Button_Cancelar(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
