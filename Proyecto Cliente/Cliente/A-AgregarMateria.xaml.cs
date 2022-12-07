﻿using System;
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

namespace Proyecto_Cliente.Cliente
{
    public partial class A_AgregarMateria : Window
    {
        HttpClient client = new HttpClient();
        string tokenR;

        //Iniciador de ventana
        public A_AgregarMateria(string tokenS)
        {
            tokenR = tokenS;
            client.BaseAddress = new Uri("http://127.0.0.1:5000/api/salon");
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Add("authtoken", tokenR);
            client.DefaultRequestHeaders.Accept.Add(
                new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json")
                );
            InitializeComponent();
        }

        //Botones
        private void Button_ClickAgregar(object sender, RoutedEventArgs e)
        {

            if (tbNombre.Text.Length == 0 || tbSemestre.Text.Length == 0)
            {
                MessageBox.Show("Ingresa los datos faltantes");
            }
            else
            {
                try
                {
                    var materia = new Materia()
                    {
                        nombre = tbNombre.Text,
                        semestre = Convert.ToInt32(tbSemestre.Text),
                    };
                    this.GuardarMateria(materia);
                }
                catch (FormatException FE)
                {
                    MessageBox.Show("Semestre solo admite enteros");
                }
            }
        }
        private void Button_ClickCancelar(object sender, RoutedEventArgs e)
        {
            A_AdministrarMateria adm = new A_AdministrarMateria(tokenR);
            adm.Show();
            this.Close();
        }


        //Funciones de apoyo
        private async void GuardarMateria(Materia materia)
        {
            try
            {
                await client.PostAsJsonAsync("materia", materia);
                MessageBox.Show("Se guardo con exito");

                tbNombre.Text = "";
                tbSemestre.Text = "";
            }
            catch (HttpRequestException he)
            {
                MessageBox.Show("No se pudo conectar con la base de datos");
            }
        }


        //Clases
        public class Materia
        {
            public string nombre { get; set; }
            public int semestre { get; set; }

            public Materia() { }
        }

    }
}
