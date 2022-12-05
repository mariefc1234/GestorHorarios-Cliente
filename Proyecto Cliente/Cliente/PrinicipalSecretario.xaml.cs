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
            RegistrarAlumno rgA = new RegistrarAlumno(tokenR);
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
    }
}
