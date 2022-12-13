using MaterialDesignThemes.Wpf;
using Newtonsoft.Json.Linq;
using Proyecto_Cliente.clases;
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
using static Proyecto_Cliente.Cliente.S_AsignarEstudiantesGrupo;

namespace Proyecto_Cliente.Cliente
{
    public partial class S_AsignarEstudiantesGrupo : Window
    {
        string tokenR;
        HttpClient client = new HttpClient();
        int idSemestre;
        int idGrupo;
        List<Estudiante> listaEstudiantes = new List<Estudiante>();
        List<Estudiante> listaEstudiantes2 = new List<Estudiante>();
        List<Estudiante> listaSeleccionados = new List<Estudiante>();

        public S_AsignarEstudiantesGrupo(string tokenS, int idS, int idG)
        {
            tokenR = tokenS;
            idSemestre = idS;
            idGrupo = idG;
            client.BaseAddress = new Uri("http://127.0.0.1:5000/api/grupo/");
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Add("authtoken", tokenR);
            client.DefaultRequestHeaders.Accept.Add(
                new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json")
                );
            InitializeComponent();
            GetEstudiantes();
            listaEstudiantes2 = listaEstudiantes;
        }

        private void Buttlon_ClickAgregar(object sender, RoutedEventArgs e)
        {
            Estudiante estudianteN = dgRecuperarAlumnos.SelectedItem as Estudiante;
            if (estudianteN == null)
            {
                MessageBox.Show("Debes seleccionar un alumno primero");
            }
            else
            {

                listaSeleccionados.Add(estudianteN);
                dgAlumnosSeleccionados.ItemsSource = null;
                dgAlumnosSeleccionados.ItemsSource = listaSeleccionados;

                listaEstudiantes2.Remove(estudianteN);
                dgRecuperarAlumnos.ItemsSource = null;
                dgRecuperarAlumnos.ItemsSource = listaEstudiantes2;

                dgRecuperarAlumnos.SelectedItem = null;
            }
        }

        public async void GetEstudiantes()
        {
            var response = await client.GetStringAsync("estudianteValido/"+ idSemestre);
            JObject json = JObject.Parse(response);
            var data = json.SelectToken("data");
            var estudiantes = data.SelectToken("estudiantes");

            foreach (var datosEstudiantes in estudiantes)
            {
                int idEstudiante = (int)datosEstudiantes.SelectToken("uid");
                string vCorreo = (string)datosEstudiantes.SelectToken("correo");
                string vPrimerNombre = (string)datosEstudiantes.SelectToken("primerNombre");
                string vSegundoNombre = (string)datosEstudiantes.SelectToken("segundoNombre");
                string vPrimerApellido = (string)datosEstudiantes.SelectToken("primerApellido");
                string vSegundoApellido = (string)datosEstudiantes.SelectToken("segundoApellido");
                string vMatricula = (string)datosEstudiantes.SelectToken("matricula");
                int vSemestre = (int)datosEstudiantes.SelectToken("ultimoSemestre");

                listaEstudiantes.Add(new Estudiante() {
                    uid = idEstudiante,
                    correo = vCorreo,
                    primerNombre = vPrimerNombre,
                    segundoNombre = vSegundoNombre,
                    primerApellido = vPrimerApellido,
                    segundoApellido = vSegundoApellido,
                    matricula = vMatricula,
                    ultimoSemestre = vSemestre,
                    nombreCompleto = vPrimerNombre+ " "+vSegundoNombre+" "+vPrimerApellido+" "+ vSegundoApellido
                });
            }
            dgRecuperarAlumnos.ItemsSource = listaEstudiantes;
        }


        public class Estudiante
        {
            public int uid { get; set; }
            public string correo { get; set; }
            public string primerNombre { get; set; }
            public string segundoNombre { get; set; }
            public string primerApellido { get; set; }
            public string segundoApellido { get; set; }
            public string matricula { get; set; }
            public int ultimoSemestre { get; set; }
            public string nombreCompleto { get; set; }

            public Estudiante() { }
        }

        private void Buttlon_ClickRegresar(object sender, RoutedEventArgs e)
        {
            Estudiante estudianteN = dgAlumnosSeleccionados.SelectedItem as Estudiante;
            if (estudianteN == null)
            {
                MessageBox.Show("Debes seleccionar un alumno primero");
            }
            else
            {
                listaEstudiantes2.Add(estudianteN);
                dgRecuperarAlumnos.ItemsSource = null;
                dgRecuperarAlumnos.ItemsSource = listaEstudiantes2;

                listaSeleccionados.Remove(estudianteN);
                dgAlumnosSeleccionados.ItemsSource = null;
                dgAlumnosSeleccionados.ItemsSource = listaSeleccionados;

                dgAlumnosSeleccionados.SelectedItem = null;
            }
        }

        private void Buttlon_ClickGuardar(object sender, RoutedEventArgs e)
        {
            if(listaSeleccionados == null)
            {
                MessageBox.Show("No has añadido ningun alumno");
            }
            else
            {
                IDUsuarios idusuarios = new IDUsuarios();
                idusuarios.idsEstudiantes = "";

                if (listaSeleccionados.Count == 1)
                {
                    idusuarios.idsEstudiantes = listaSeleccionados[0].uid.ToString();
                }
                else
                {
                    List<string> listaAuxiliar = new List<string>();

                    foreach (var estudiantes in listaSeleccionados)
                    {

                        listaAuxiliar.Add(estudiantes.uid.ToString());
                    }
                    idusuarios.idsEstudiantes = string.Join(",", listaAuxiliar);
                }
                this.AsignarEstudiantesGrupo(idusuarios);
            }
        }

        private void Buttlon_ClickSalir(object sender, RoutedEventArgs e)
        {
            S_AdministrarGrupo amg = new S_AdministrarGrupo(tokenR);
            amg.Show();
            this.Close();
        }


        private async void AsignarEstudiantesGrupo(IDUsuarios iDUsuarios)
        {
            try
            {
                var response = await client.PostAsJsonAsync("estudiante/"+idGrupo, iDUsuarios);

                if(response.StatusCode.ToString() == "OK")
                {
                    MessageBox.Show("Se añadieron los alumnos al grupo correctamente");
                }
                else
                {
                    MessageBox.Show("Error al añadir alumnos");
                }
            }
            catch (HttpRequestException he)
            {
                MessageBox.Show("No se pudo conectar con la base de datos");
            }
        }

        public class IDUsuarios {
            public string idsEstudiantes { get; set; }
            public IDUsuarios() { }
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
