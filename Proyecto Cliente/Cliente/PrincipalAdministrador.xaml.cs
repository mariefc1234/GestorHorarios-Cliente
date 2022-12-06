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

        private void Button_AdministrarEdificio(object sender, RoutedEventArgs e)
        {
            A_AdministrarEdificio ade = new A_AdministrarEdificio(tokenR);
            ade.Show();
            this.Close();
        }

        private void Button_CerrarSesion(object sender, RoutedEventArgs e)
        {
            MainWindow mw = new MainWindow();
            mw.Show();
            this.Close();
        }

        private void Button_ClickAdministrarArea(object sender, RoutedEventArgs e)
        {
            A_AdministrarArea ata = new A_AdministrarArea(tokenR);
            ata.Show();
            this.Close();
        }

        private void Button_ClickAdministrarSalones(object sender, RoutedEventArgs e)
        {
            A_AdministrarSalon ads = new A_AdministrarSalon(tokenR);
            ads.Show();
            this.Close();
        }
    }
}
