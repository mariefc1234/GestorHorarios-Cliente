using MaterialDesignThemes.Wpf;
using Microsoft.VisualBasic;
using Newtonsoft.Json;
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
using static Proyecto_Cliente.Cliente.A_AdministrarArea;

namespace Proyecto_Cliente.Cliente
{
    public partial class A_AdministrarArea : Window
    {
        HttpClient client = new HttpClient();
        String tokenR;

        public A_AdministrarArea(String tokenS)
        {
            tokenR = tokenS;
            client.BaseAddress = new Uri("http://127.0.0.1:5000/api/area");
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Add("auth_token", tokenR);
            client.DefaultRequestHeaders.Accept.Add(
                new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json")
                );

            InitializeComponent();
            GetAreas();
        }

        //bottones
        private void Button_ClickMostrarArea(object sender, RoutedEventArgs e)
        {
            AreaAgregar areaN = dgArea.SelectedItem as AreaAgregar;
            if (areaN == null)
            {
                MessageBox.Show("Debes seleccionar una area primero");
            }
            else
            {
                string detalles = "Nombre Area: " + areaN.nombre;
                MessageBox.Show(detalles);
            }
        }

        private void Button_ClickModificarArea(object sender, RoutedEventArgs e)
        {

            AreaAgregar areaN = dgArea.SelectedItem as AreaAgregar;
            if (areaN == null)
            {
                MessageBox.Show("Debes seleccionar una area primero");
            }
            else
            {
                string nuevoNombre = Interaction.InputBox("Ingresa el nuevo valor?", "Actualizar Area", areaN.nombre);
                areaN.nombre = nuevoNombre;

                MessageBox.Show(nuevoNombre);
                MessageBox.Show(areaN.nombre);
                this.ActualizarArea(areaN);
            }
        }

        private void Button_ClickEliminar(object sender, RoutedEventArgs e)
        {
            AreaAgregar areaN = dgArea.SelectedItem as AreaAgregar;
            if (areaN == null)
            {
                MessageBox.Show("Debes seleccionar una area primero");
            }
            else
            {
                MessageBoxResult messageBoxResult = System.Windows.MessageBox.Show("Estas seguro?", "Confirmacion de eliminacion", System.Windows.MessageBoxButton.YesNo);
                if (messageBoxResult == MessageBoxResult.Yes)
                {
                    this.EliminarArea(areaN.idArea);
                }
            }
        }

        private void Button_ClickAgregarArea(object sender, RoutedEventArgs e)
        {
            if (tbNombreArea.Text.Length == 0 || tbSemestre.Text.Length == 0)
            {
                MessageBox.Show("Ingresa datos");
            }
            else
            {
                var area = new AreaAgregar()
                {
                    idArea = Convert.ToInt32(tbId.Text),
                    nombre = tbNombreArea.Text,
                    semestre = Convert.ToInt32(tbSemestre.Text)
                };
                if (area.idArea == 0)
                {
                    this.GuardarArea(area);
                    MessageBox.Show("Area guardada con exito");
                }
                else
                {
                    this.ActualizarArea(area);
                    MessageBox.Show("Area actualizada con exito");
                }
            }
            GetAreas();
            tbId.Text = 0.ToString();
            tbNombreArea.Text = "";
        }

        private void Button_ClickRegresar(object sender, RoutedEventArgs e)
        {
            PrincipalAdministrador psa = new PrincipalAdministrador(tokenR);
            psa.Show();
            this.Close();
        }


        //clases
        private class AreaAgregar
        {
            public int idArea { get; set; }
            public string nombre { get; set; }

            public int semestre { get; set; }

            public AreaAgregar()
            {

            }
        }


        //Funcione sde ayuda
        private async void GetAreas()
        {
            var response = await client.GetStringAsync("area");
            JObject json = JObject.Parse(response);
            var data = json.SelectToken("data");
            List<AreaAgregar> listaAreas = new List<AreaAgregar>();

            foreach (var datosArea in data)
            {
                int idF = (int)datosArea.SelectToken("id");
                string nombreF = (string)datosArea.SelectToken("nombre");
                listaAreas.Add(new AreaAgregar() { idArea = idF, nombre = nombreF });
            }
            dgArea.ItemsSource = listaAreas;
        }

        private async void GuardarArea(AreaAgregar area)
        {
            try
            {
                await client.PostAsJsonAsync("area", area);
            }
            catch (HttpRequestException he)
            {
                MessageBox.Show("No se pudo conectar con la base de datos");
                Console.WriteLine(he);
            }
        }

        private async void ActualizarArea(AreaAgregar area)
        {
            try
            {
                await client.PutAsJsonAsync("area/" + area.idArea, area);
                GetAreas();
            }
            catch (HttpRequestException he)
            {
                MessageBox.Show("No se pudo conectar con la base de datos");
                Console.WriteLine(he);
            }
        }

        private async void EliminarArea(int areaId)
        {
            try
            {
                var response = await client.DeleteAsync("area/" + areaId);
                if (response.StatusCode.ToString() == "OK")
                {
                    MessageBox.Show("Area eliminada correctamente");
                    GetAreas();
                }
                else
                {
                    MessageBox.Show("Error al eliminar");
                }
            }
            catch (HttpRequestException he)
            {
                MessageBox.Show("No se pudo conectar con la base de datos");
                Console.WriteLine(he);
            }
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
