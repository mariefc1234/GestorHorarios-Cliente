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
    /// <summary>
    /// Lógica de interacción para AdministrarEdificio.xaml
    /// </summary>
    public partial class AdministrarEdificio : Window
    {
        public AdministrarEdificio()
        {
            InitializeComponent();
        }

        private void Button_AgregarEdificio(object sender, RoutedEventArgs e)
        {
            AgregarEdificio age = new AgregarEdificio();
            age.Show();
            this.Close();
        }

        private void Button_ClickSalir(object sender, RoutedEventArgs e)
        {
            PrincipalAdministrador psa = new PrincipalAdministrador();
            psa.Show();
            this.Close();
        }

        private void Button_MostrarEdificios(object sender, RoutedEventArgs e)
        {
            MostrarEdificios mse = new  MostrarEdificios();
            mse.Show();
            this.Close();
        }
    }
}
