using MaterialDesignThemes.Wpf;
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
    public partial class PrincipalMaestro : Window
    {
        string tokenR;
        public PrincipalMaestro(string tokenS)
        {
            tokenR = tokenS;
            InitializeComponent();
        }

        //Bottones

        private void Button_ClickSalir(object sender, RoutedEventArgs e)
        {
            MainWindow mw = new MainWindow();
            mw.Show();
            this.Close();
        }
        private void Button_ClickVerMaterias(object sender, RoutedEventArgs e)
        {
            M_VerMaterias vmt = new M_VerMaterias(tokenR);
            vmt.Show();
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
