using Microsoft.VisualBasic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.SqlServer.Server;
using RestSharp;
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
using System.Net.Http;
using System.Net.Http.Headers;

namespace Proyecto_Cliente.Cliente
{
    public partial class RegistrarAlumno : Window
    {
        HttpClient client = new HttpClient();
        string tokenR;
        public RegistrarAlumno(string tokenS)
        {
            tokenR = tokenS;
            client.BaseAddress = new Uri("http://127.0.0.1:5000/api/register");
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Add("auth_token", tokenR);

            client.DefaultRequestHeaders.Accept.Add(
                new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json")
                );
            InitializeComponent();
        }

        private void Button_Cancelar(object sender, RoutedEventArgs e)
        {
            Prinicipal_Secretario ps = new Prinicipal_Secretario(tokenR);
            ps.Show();
            this.Close();
        }

        private void Button_Registrar(object sender, RoutedEventArgs e)
        {
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
                    var alumno = new Alumno()
                    {
                        correo = vCorreo,
                        password = vContraseña,
                        primerNombre =primerNombre,
                        segundoNombre = segundoNombre,
                        primerApellido = primerApellido,
                        segundoApellido = segundoApellido,
                        rol = Convert.ToInt32(1),
                        fechaNacimiento = vFechaNacimiento,
                        matricula = vMAtricula
                    };
                    this.RegistrarAlumnos(alumno);
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

        private async void RegistrarAlumnos(Alumno alumno)
        {
            try
            {
                await client.PostAsJsonAsync("register", alumno);
                MessageBox.Show("Usuario registrado con exito");
                tbNombres.Clear();
                tbMatricula.Clear();
                tbCorreo.Clear();
                tbContrasena.Clear();
                tbApellidos.Clear();
                dpFechaNacimiento.SelectedDate = null;
            }
            catch (HttpRequestException he)
            {
                MessageBox.Show("No se pudo conectar con la base de datos");
            }
        }

        public class Alumno
        {
            public string correo { get; set; }
            public string password { get; set; }
            public string primerNombre { get; set; }
            public string segundoNombre { get; set; }
            public string primerApellido { get; set; }
            public string segundoApellido { get; set; }
            public int rol { get; set; }
            public string fechaNacimiento { get; set; }
            public string matricula { get; set; }

            public Alumno() { }
        }

    }
}
