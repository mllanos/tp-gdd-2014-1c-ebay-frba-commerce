using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace FrbaCommerce.ABM_Rol
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void btnCrear_Click(object sender, EventArgs e)
        {
            ABM_Rol.FormCrear CrearRol = new ABM_Rol.FormCrear();
            CrearRol.ShowDialog();
            this.Hide();
        }

        private void btnMod_Click(object sender, EventArgs e)
        {
            ABM_Rol.FormSeleccion ModRol = new ABM_Rol.FormSeleccion(1);
            ModRol.ShowDialog();
            this.Hide();
        }

        private void btnEli_Click(object sender, EventArgs e)
        {
            ABM_Rol.FormSeleccion ModRol = new ABM_Rol.FormSeleccion(0);
            ModRol.ShowDialog();
            this.Hide();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}
