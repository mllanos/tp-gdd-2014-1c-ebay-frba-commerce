using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace FrbaCommerce
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            Login.Form1 formLogin  = new Login.Form1();

            formLogin.ShowDialog();

        }

        private void btnRegistro_Click(object sender, EventArgs e)
        {
            Registro_de_Usuario.Form1 formRegistro = new Registro_de_Usuario.Form1();
            formRegistro.ShowDialog();
        }
    }
}
