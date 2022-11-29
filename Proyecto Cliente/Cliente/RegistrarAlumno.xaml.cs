using Microsoft.SqlServer.Server;
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
using System.Windows.Media.Media3D;
using System.Windows.Shapes;

namespace Proyecto_Cliente.Cliente
{
    public partial class RegistrarAlumno : Window
    {
        public RegistrarAlumno()
        {
            InitializeComponent();
        }

        private void Button_Cancelar(object sender, RoutedEventArgs e)
        {
            Prinicipal_Secretario ps = new Prinicipal_Secretario();
            ps.Show();
            this.Close();
        }

        private void Button_Registrar(object sender, RoutedEventArgs e)
        {
            //obtencion de datos

            String vCorreo = tbCorreo.Text;
            String vNombres = tbNombres.Text;
            String vApellidos = tbApellidos.Text;
            String vMAtricula = tbMatricula.Text;
            String vContraseña = tbContrasena.Text;


            try
            {
                String vFechaNacimiento = dpFechaNacimiento.SelectedDate.Value.ToString();
                String primerNombre;
                String segundoNombre;
                String primerApellido;
                String segundoApellido;

                char delimitador = ' ';
                string[] valores = vFechaNacimiento.Split(delimitador);
                vFechaNacimiento = valores[0];
                valores[1] = " ";

                vFechaNacimiento = transformarFecha(vFechaNacimiento);


                if (vNombres.Contains(' '))
                {
                    valores = vNombres.Split(delimitador);
                    primerNombre = valores[0];
                    valores[1] = " ";
                    segundoNombre = valores[1];
                }
                else
                {
                    primerNombre = vNombres;
                    segundoNombre = " ";
                }

                valores = vApellidos.Split(delimitador);
                primerApellido = valores[0];
                segundoApellido = valores[1];

                if(vCorreo.Length == 0 || vNombres.Length == 0 || vMAtricula.Length == 0 || vContraseña.Length == 0)
                {
                    MessageBox.Show("Campos vacios");
                }else
                {
                    var url = "http://127.0.0.1:5000/api/register";
                    var json = "{\"correo\": \""+ vCorreo + "\",    \"password\": \""+ vContraseña +"\",    \"primerNombre\": \""+ primerNombre +"\",    \"segundoNombre\": \""+ segundoNombre +"\",    \"primerApellido\": \""+ primerApellido +"\",    \"segundoApellido\": \""+ segundoApellido +"\",    \"rol\": 1,    \"fechaNacimiento\": \""+ vFechaNacimiento+"\",   \"matricula\": \"" + vMAtricula +"\"}";
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

                        tbNombres.Clear();
                        tbMatricula.Clear();
                        tbCorreo.Clear();
                        tbContrasena.Clear();
                        tbApellidos.Clear();
                        dpFechaNacimiento.SelectedDate = null;
                    }
                }
            }
            catch (InvalidOperationException io)
            {
                MessageBox.Show("Ingrese o seleccione una fecha");
            }catch(IndexOutOfRangeException iorg)
            {
                MessageBox.Show("favor de ingresar los dos apellidos");
            }
        }

        public String transformarFecha(String vFechaNacimiento)
        {
            String fechaFinal;
            String dia;
            String mes;
            String año;
            char delimitador = '/';
            string[] valores = vFechaNacimiento.Split(delimitador);
            dia = valores[0];
            mes = valores[1];
            año = valores[2];
            fechaFinal = año +"-"+ mes+"-"+ dia+ " 00:00:00";

            return fechaFinal;
        }
    }
}
