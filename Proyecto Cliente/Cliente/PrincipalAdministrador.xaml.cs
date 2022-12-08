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
    public partial class PrincipalAdministrador : Window
    {
        string tokenR;
        public PrincipalAdministrador(string tokenS)
        {
            InitializeComponent();
            tokenR = tokenS;
        }

        // ofjhdlafj

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

        private void administrarSalones_Click(object sender, RoutedEventArgs e)
        {
            A_AdministrarSalon ads = new A_AdministrarSalon(tokenR);
            ads.Show();
            this.Close();
        }

        private void administrarMateria_Click(object sender, RoutedEventArgs e)
        {
            A_AdministrarMateria adm = new A_AdministrarMateria(tokenR);
            adm.Show();
            this.Close();
        }

        private void administrarEdificio_Click(object sender, RoutedEventArgs e)
        {
            A_AdministrarEdificio ade = new A_AdministrarEdificio(tokenR);
            ade.Show();
            this.Close();
        }

        private void administrarArea_Click(object sender, RoutedEventArgs e)
        {
            A_AdministrarArea ata = new A_AdministrarArea(tokenR);
            ata.Show();
            this.Close();
        }

        private void registrarSecretarios_Click(object sender, RoutedEventArgs e)
        {

        }

        private void cerrarSesion_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mw = new MainWindow();
            mw.Show();
            this.Close();
        }
    }
}
