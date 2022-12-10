using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
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
using MaterialDesignThemes.Wpf;

namespace Proyecto_Cliente
{

    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        public bool IsDarkTheme { get; set; }
        private readonly PaletteHelper paletteHelper = new PaletteHelper();

        private void themeToggle_Click(object sender, RoutedEventArgs e)
        {
            ITheme theme = paletteHelper.GetTheme();
            if(IsDarkTheme = theme.GetBaseTheme() == BaseTheme.Dark)
            {
                IsDarkTheme = false;
                theme.SetBaseTheme(Theme.Light);
            }else
            {
                IsDarkTheme = true;
                theme.SetBaseTheme(Theme.Dark);
            }
            paletteHelper.SetTheme(theme);
        }

        private void exitApp(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
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

        private void Button_IniciarSesion(object sender, RoutedEventArgs e)
        {
            if (tbMatricula.Text.Length == 0 || psbContrasenia.Password.Length == 0)
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
                    JObject jsonHelp = JObject.Parse(response.Content);

                    if (response.StatusCode.ToString().Equals("OK"))
                    {
                        var data = jsonHelp.SelectToken("data");
                        int rol = (int)data.SelectToken("rol");
                        string tokenS = (string)data.SelectToken("token");
                        switch (rol)
                        {
                            case 1:
                                PrincipalAlumno pa = new PrincipalAlumno(tokenS);
                                pa.Show();
                                this.Close();
                                break;
                            case 2:
                                PrincipalMaestro pm = new PrincipalMaestro(tokenS);
                                pm.Show();
                                this.Close();
                                break;
                            case 3:
                                Prinicipal_Secretario ps = new Prinicipal_Secretario(tokenS);
                                ps.Show();
                                this.Close();
                                break;
                            case 4:
                                PrincipalAdministrador psa = new PrincipalAdministrador(tokenS);
                                psa.Show();
                                this.Close();
                                break;
                        }
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
                    Console.Write(ex.Message);
                    MessageBox.Show("Error no se pudo establecer conexion con el servidor");
                }
            }
        }
    }
}
