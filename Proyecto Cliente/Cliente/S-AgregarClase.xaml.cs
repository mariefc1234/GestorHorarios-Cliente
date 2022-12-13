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
using static Proyecto_Cliente.Cliente.S_AgregarClase;

namespace Proyecto_Cliente.Cliente
{
    public partial class S_AgregarClase : Window
    {
        string tokenR;
        int idGrupo;
        HttpClient client = new HttpClient();
        public S_AgregarClase(string tokenS, int idG)
        {
            tokenR = tokenS;
            idGrupo = idG;
            client.BaseAddress = new Uri("http://127.0.0.1:5000/api/clase");
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Add("authtoken", tokenR);
            client.DefaultRequestHeaders.Accept.Add(
                new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json")
                );
            InitializeComponent();
            GetMaestros();
            GetMateriasValidas();
        }

        //Bottones

        private void Button_ClickGuardar(object sender, RoutedEventArgs e)
        {
            Maestro maestroN = dgMaestros.SelectedItem as Maestro;
            MateriaValida materiaValidaN = dgMaterias.SelectedItem as MateriaValida;

            if (maestroN == null || materiaValidaN == null || tbNrc.Text.Length == 0)
            {
                MessageBox.Show("Campos o selecciones vacias");
            }
            else
            {
                var clase = new Clase()
                {
                    nrc = tbNrc.Text,
                    idGrupo = idGrupo,
                    idProfesor = maestroN.uid,
                    idMateria = materiaValidaN.id
                };

                this.GuardarClase(clase);
            }
        }
        private void Button_ClickSalir(object sender, RoutedEventArgs e)
        {
            S_AdministrarGrupo adg = new S_AdministrarGrupo(tokenR);
            adg.Show();
            this.Close();
        }

        //funciones de ayuda
        public async void GetMaestros()
        {
            var response = await client.GetStringAsync("profesor");
            JObject json = JObject.Parse(response);
            var data = json.SelectToken("data");
            List<Maestro> listaMaestros = new List<Maestro>();

            foreach (var datosMaestros in data)
            {
                int idMaestro = (int)datosMaestros.SelectToken("uid");
                string vPrimerNombre = (string)datosMaestros.SelectToken("primerNombre");
                string vSegundoNombre = (string)datosMaestros.SelectToken("segundoNombre");
                string vPrimerApellido = (string)datosMaestros.SelectToken("primerApellido");
                string vSegundoApellido = (string)datosMaestros.SelectToken("segundoApellido");
                string vClaveEmpleado = (string)datosMaestros.SelectToken("claveEmpleado");

                listaMaestros.Add(new Maestro()
                {
                    uid = idMaestro,
                    primerNombre = vPrimerNombre,
                    segundoNombre = vSegundoNombre,
                    primerApellido = vPrimerNombre,
                    segundoApellido = vSegundoApellido,
                    claveEmpleado = vClaveEmpleado,
                    nombreCompleto = vPrimerNombre + " " + vSegundoNombre + " " + vPrimerApellido + " " + vSegundoApellido
                });
            }
            dgMaestros.ItemsSource = listaMaestros;
        }
        public async void GetMateriasValidas()
        {
            var response = await client.GetStringAsync("clase/materiasvalidas/"+idGrupo);
            JObject json = JObject.Parse(response);
            var data = json.SelectToken("data");
            List<MateriaValida> listaMaterias = new List<MateriaValida>();
            foreach (var datosMateria in data)
            {
                int idMaterias = (int)datosMateria.SelectToken("id");
                string vNombreMateria = (string)datosMateria.SelectToken("nombre");

                listaMaterias.Add(new MateriaValida()
                {
                    id = idMaterias,
                    nombre = vNombreMateria
                });
            }
            dgMaterias.ItemsSource = listaMaterias;
        }
        private async void GuardarClase(Clase clase)
        {
            try
            {
                var response = await client.PostAsJsonAsync("clase", clase);

                if (response.StatusCode.ToString() == "OK")
                {
                    MessageBox.Show("Se guardo con exito");
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

        //Clasess
        public class MateriaValida
        {
            public int id { get; set; }
            public string nombre { get; set; }

            public MateriaValida() { }
        }
        public class Maestro
        {
            public int uid { get; set; }
            public string primerNombre { get; set; }
            public string segundoNombre { get; set; }
            public string primerApellido { get; set; }
            public string segundoApellido { get; set; }
            public string claveEmpleado { get; set; }
            public string nombreCompleto { get; set; }

            public Maestro() { }
        }
        public class Clase
        {
            public string nrc { get; set; }
            public int idProfesor { get; set; }
            public int idMateria { get; set; }
            public int idGrupo { get; set; }

            public Clase() { }
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
