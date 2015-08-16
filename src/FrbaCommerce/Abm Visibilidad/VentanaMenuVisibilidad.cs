using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace FrbaCommerce.Abm_Visibilidad
{
    public partial class VentanaMenuVisibilidad : Form
    {
        public VentanaMenuVisibilidad()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Abm_Visibilidad.VentanaCrearVisibilidad winCrearVis = new Abm_Visibilidad.VentanaCrearVisibilidad();
            winCrearVis.ShowDialog();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Abm_Visibilidad.VentanaFiltroVisibilidad winModVis = new Abm_Visibilidad.VentanaFiltroVisibilidad(0);
            winModVis.ShowDialog();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Abm_Visibilidad.VentanaFiltroVisibilidad winModVis = new Abm_Visibilidad.VentanaFiltroVisibilidad(1);
            winModVis.ShowDialog();
        }

    }
}
