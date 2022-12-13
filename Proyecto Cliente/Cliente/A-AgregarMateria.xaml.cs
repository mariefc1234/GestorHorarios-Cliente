using MaterialDesignThemes.Wpf;
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
    public partial class A_AgregarMateria : Window
    {
        HttpClient client = new HttpClient();
        string tokenR;

        //Iniciador de ventana
        public A_AgregarMateria(string tokenS)
        {
            tokenR = tokenS;
            client.BaseAddress = new Uri("http://127.0.0.1:5000/api/salon");
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Add("authtoken", tokenR);
            client.DefaultRequestHeaders.Accept.Add(
                new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json")
                );
            InitializeComponent();
        }

        //Botones
        private void Button_ClickGuardar(object sender, RoutedEventArgs e)
        {

            if (tbNombre.Text.Length == 0 || tbSemestre.Text.Length == 0)
            {
                MessageBox.Show("Ingresa los datos faltantes");
            }
            else
            {
                try
                {
                    int semestreVar = Convert.ToInt32(tbSemestre.Text);

                    if (semestreVar > 6)
                    {
                        MessageBox.Show("El semestre solo puede estar entre los rangos 1 y 6 ");
                    }
                    else
                    {
                        var materia = new Materia()
                        {
                            nombre = tbNombre.Text,
                            semestre = Convert.ToInt32(tbSemestre.Text),
                        };
                        this.GuardarMateria(materia);
                    }
                }
                catch (FormatException fe)
                {
                    Console.WriteLine(fe);
                    MessageBox.Show("Semestre solo puede recibir enteros");
                }
            }
        }
        private void Button_ClickRegresar(object sender, RoutedEventArgs e)
        {
            A_AdministrarMateria adm = new A_AdministrarMateria(tokenR);
            adm.Show();
            this.Close();
        }


        //Funciones de apoyo
        private async void GuardarMateria(Materia materia)
        {
            try
            {
                var response = await client.PostAsJsonAsync("materia", materia);

                if (response.StatusCode.ToString() == "OK")
                {
                    MessageBox.Show("Se guardo con exito");
                    tbNombre.Text = "";
                    tbSemestre.Text = "";
                }
                else
                {
                    MessageBox.Show("Error al guardar");
                }
            }
            catch (HttpRequestException he)
            {
                Console.WriteLine(he);
                MessageBox.Show("No se pudo conectar con la base de datos");
            }
        }


        //Clases
        public class Materia
        {
            public string nombre { get; set; }
            public int semestre { get; set; }

            public Materia() { }
        }

        // Funciones de la ventana
        public bool IsDarkTheme { get; set; }
        private readonly PaletteHelper paletteHelper = new PaletteHelper();

        private void themeToggle_Click(object sender, RoutedEventArgs e)
        {
            ITheme theme = paletteHelper.GetTheme();
            if (IsDarkTheme = theme.GetBaseTheme() == BaseTheme.Dark)
            {
                IsDarkTheme = false;
                theme.SetBaseTheme(Theme.Light);
            }
            else
            {
                IsDarkTheme = true;
                theme.SetBaseTheme(Theme.Dark);
            }
            paletteHelper.SetTheme(theme);
        }
        protected override void OnMouseLeftButtonDown(MouseButtonEventArgs e)
        {
            base.OnMouseLeftButtonDown(e);
            DragMove();
        }

        private void btnCloseWindow_Click(object sender, MouseButtonEventArgs e)
        {
            try { this.Close(); } catch (Exception ex) { MessageBox.Show(ex.Message); }
        }

        private void minimizeWindow(object sender, MouseButtonEventArgs e)
        {
            try { this.WindowState = WindowState.Minimized; } catch (Exception ex) { MessageBox.Show(ex.Message); }
        }
    }
}
