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
using static Proyecto_Cliente.Cliente.S_RegistrarAlumno;
using static Proyecto_Cliente.Cliente.S_RegistrarMaestro;

namespace Proyecto_Cliente.Cliente
{
    public partial class A_RegistrarSecretario : Window
    {
        HttpClient client = new HttpClient();
        string tokenR;
        public A_RegistrarSecretario(string tokenS)
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

        private void Button_ClicGuardar(object sender, RoutedEventArgs e)
        {
            String vCorreo = tbCorreoElectronico.Text;
            String vNombres = tbNombre.Text;
            String vApellidos = tbApellidos.Text;
            String vClaveEmpleado = tbClaveEmpleado.Text;
            String vContraseña = tbContraseña.Text;

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

                if (vCorreo.Length == 0 || vNombres.Length == 0 || vClaveEmpleado.Length == 0 || vContraseña.Length == 0)
                {
                    MessageBox.Show("Campos vacios");
                }
                else
                {
                    var secretario = new Secretario()
                    {
                        correo = vCorreo,
                        password = vContraseña,
                        primerNombre = primerNombre,
                        segundoNombre = segundoNombre,
                        primerApellido = primerApellido,
                        segundoApellido = segundoApellido,
                        rol = Convert.ToInt32(3),
                        fechaNacimiento = vFechaNacimiento,
                        claveEmpleado = vClaveEmpleado
                    };
                    this.RegistrarSecretario(secretario);
                }
            }
            catch (InvalidOperationException io)
            {
                MessageBox.Show("Ingrese o seleccione una fecha");
            }
            catch (IndexOutOfRangeException iorg)
            {
                MessageBox.Show("favor de ingresar los dos apellidos");
            }
        }

        private void Button_ClicRegresar(object sender, RoutedEventArgs e)
        {
            PrincipalAdministrador pa = new PrincipalAdministrador(tokenR);
            pa.Show();
            this.Close();
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
            fechaFinal = año + "-" + mes + "-" + dia + " 00:00:00";

            return fechaFinal;
        }

        private async void RegistrarSecretario(Secretario secretario)
        {
            try
            {
                await client.PostAsJsonAsync("register", secretario);
                MessageBox.Show("Secretario registrado con exito");

                tbNombre.Clear();
                tbClaveEmpleado.Clear();
                tbCorreoElectronico.Clear();
                tbContraseña.Clear();
                tbApellidos.Clear();
                dpFechaNacimiento.SelectedDate = null;
            }
            catch (HttpRequestException he)
            {
                MessageBox.Show("No se pudo conectar con la base de datos");
            }
        }

        public class Secretario
        {
            public string correo { get; set; }
            public string password { get; set; }
            public string primerNombre { get; set; }
            public string segundoNombre { get; set; }
            public string primerApellido { get; set; }
            public string segundoApellido { get; set; }
            public int rol { get; set; }
            public string fechaNacimiento { get; set; }
            public string claveEmpleado { get; set; }

            public Secretario() { }
        }
    }
}
