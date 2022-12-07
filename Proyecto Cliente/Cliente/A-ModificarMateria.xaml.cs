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

namespace Proyecto_Cliente.Cliente
{
    public partial class A_ModificarMateria : Window
    {
        HttpClient client = new HttpClient();
        string tokenR;
        int idMateria;

        //Iniciador de ventana
        public A_ModificarMateria(string tokenS,int idS,string nombreS,int semestreS)
        {
            tokenR = tokenS;
            client.BaseAddress = new Uri("http://127.0.0.1:5000/api/materia");
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Add("authtoken", tokenR);
            client.DefaultRequestHeaders.Accept.Add(
                new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json")
                );
            InitializeComponent();
            idMateria = idS;

            tbNombre.Text = nombreS;
            tbSemestre.Text = semestreS.ToString();
        }

        //Funciones de ayuda
        private async void ActualizarMateria(Materia materia)
        {
            try
            {
                await client.PutAsJsonAsync("materia/" + idMateria, materia);
                MessageBox.Show("Los datos se actualizaron con exito");
                A_AdministrarMateria adm = new A_AdministrarMateria(tokenR);
                adm.Show();
                this.Close();

            }
            catch (HttpRequestException he)
            {
                MessageBox.Show("No se pudo conectar con la base de datos");
            }
        }

        //Clases
        public class Materia
        {
            public int id { get; set; }
            public string nombre { get; set; }
            public int semestre { get; set; }

            public Materia() { }
        }

        //Botones
        private void Button_ClickCancelar(object sender, RoutedEventArgs e)
        {
            A_AdministrarMateria adm = new A_AdministrarMateria(tokenR);
            adm.Show();
            this.Close();
        }

        private void Button_ClickModificar(object sender, RoutedEventArgs e)
        {
            Materia materiaN = new Materia();

            if (tbNombre.Text.Length == 0 || tbSemestre.Text.Length == 0)
            {
                MessageBox.Show("Debes ingresar informacion");
            }
            else
            {
                try
                {
                    materiaN.id = idMateria;
                    materiaN.nombre = tbNombre.Text;
                    materiaN.semestre = Convert.ToInt32(tbSemestre.Text);

                    this.ActualizarMateria(materiaN);
                }
                catch (FormatException FE)
                {
                    MessageBox.Show("Semestre solo admite enteros");
                }
            }
        }
    }
}
