using MaterialDesignThemes.Wpf;
using Newtonsoft.Json.Linq;
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
using System.Windows.Shapes;

namespace Proyecto_Cliente.Cliente
{
    public partial class Prinicipal_Secretario : Window
    {
        string tokenR;
        public Prinicipal_Secretario(string tokenS)
        {
            InitializeComponent();
            tokenR = tokenS;
        }

        private void Button_RegistarAlumno(object sender, RoutedEventArgs e)
        {
            S_RegistrarAlumno rgA = new S_RegistrarAlumno(tokenR);
            rgA.Show();
            this.Close();
        }

        private void Button_CerrarSesion(object sender, RoutedEventArgs e)
        {
            MainWindow mw = new MainWindow();
            mw.Show();
            this.Close();
        }

        private void Button_ClickRegistarMaestro(object sender, RoutedEventArgs e)
        {
            S_RegistrarMaestro srm = new S_RegistrarMaestro(tokenR);
            srm.Show();
            this.Close();
        }

        private void Button_AdministrarGrupos(object sender, RoutedEventArgs e)
        {
            S_AdministrarGrupo adg = new S_AdministrarGrupo(tokenR);
            adg.Show();
            this.Close();
        }

        private void Button_ClickAsignarHorario(object sender, RoutedEventArgs e)
        {
            S_AsignarHorario ash = new S_AsignarHorario(tokenR);
            ash.Show();
            this.Close();
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
