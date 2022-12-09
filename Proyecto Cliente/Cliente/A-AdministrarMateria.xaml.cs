using MaterialDesignThemes.Wpf;
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
    public partial class A_AdministrarMateria : Window
    {
        HttpClient client = new HttpClient();
        string tokenR;
        
        //Iniciador de Ventana
        public A_AdministrarMateria(string tokenS)
        {
            tokenR = tokenS;
            client.BaseAddress = new Uri("http://127.0.0.1:5000/api/area");
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Add("authtoken", tokenR);
            client.DefaultRequestHeaders.Accept.Add(
                new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json")
                );
            InitializeComponent();
            GetMaterias();
        }


        //Botones
        private void Button_ClickAgregar(object sender, RoutedEventArgs e)
        {
            A_AgregarMateria agm = new A_AgregarMateria(tokenR);
            agm.Show();
            this.Close();

        }
        private void Button_ClickEliminar(object sender, RoutedEventArgs e)
        {
            Materia materiaN = dgMaterias.SelectedItem as Materia;
            if (materiaN == null)
            {
                MessageBox.Show("Debes seleccionar una area primero");
            }
            else
            {
                MessageBoxResult messageBoxResult = System.Windows.MessageBox.Show("Estas seguro?", "Confirmacion de eliminacion", System.Windows.MessageBoxButton.YesNo);
                if (messageBoxResult == MessageBoxResult.Yes)
                {
                    this.EliminarMateria(materiaN.id);
                    MessageBox.Show("Materia eliminada correctamente");
                }
            }
        }
        private void Button_ClickDetalles(object sender, RoutedEventArgs e)
        {
            Materia materiaN = dgMaterias.SelectedItem as Materia;
            if (materiaN == null)
            {
                MessageBox.Show("Debes seleccionar una materia primero");
            }
            else
            {
                string detalles = "Nombre Area: " + materiaN.nombre + "\nSemestre: "+materiaN.semestre;
                MessageBox.Show(detalles);
            }
        }
        private void Button_ClickModificar(object sender, RoutedEventArgs e)
        {
            Materia materiaN = dgMaterias.SelectedItem as Materia;
            if (materiaN == null)
            {
                MessageBox.Show("Debes seleccionar una materia primero");
            }
            else
            {
                int idS = materiaN.id;
                string nombreS = materiaN.nombre;
                int semestreS = materiaN.semestre;

                A_ModificarMateria mdm = new A_ModificarMateria(tokenR,idS,nombreS,semestreS);
                mdm.Show();
                this.Close();
            }
        }
        private void Button_ClickRegresar(object sender, RoutedEventArgs e)
        {
            PrincipalAdministrador psa = new PrincipalAdministrador(tokenR);
            psa.Show();
            this.Close();
        }


        //Funciones de ayuda
        public async void GetMaterias()
        {
            var response = await client.GetStringAsync("materia");
            JObject json = JObject.Parse(response);
            var data = json.SelectToken("data");
            List<Materia> listaMaterias = new List<Materia>();

            foreach (var datosMateria in data)
            {
                int idAD = (int)datosMateria.SelectToken("id");
                string nombreD = (string)datosMateria.SelectToken("nombre");
                int semestreD = (int)datosMateria.SelectToken("semestre");

                listaMaterias.Add(new Materia() {id = idAD, nombre = nombreD, semestre = semestreD });
            }
            dgMaterias.ItemsSource = listaMaterias;
        }
        private async void EliminarMateria(int idMateria)
        {
            try
            {
                await client.DeleteAsync("materia/" + idMateria);
                GetMaterias();
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

            public Materia(){}
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
