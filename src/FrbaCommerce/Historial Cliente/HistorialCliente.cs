using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace FrbaCommerce.Historial_Cliente
{
    public partial class HistorialCliente : Form
    {
        Usuario usuario;

        public HistorialCliente(Usuario us)
        {
            InitializeComponent();
            usuario = us;
        }

        private void btnHCompras_Click(object sender, EventArgs e)
        {
            HistorialCompras hCompras = new HistorialCompras(this, usuario.Cod_usuario.ToString());
            this.Hide();
            hCompras.Show();

        }

        private void btnHOfertas_Click(object sender, EventArgs e)
        {
            HistorialOfertas hOfertas = new HistorialOfertas(this, usuario.Cod_usuario.ToString());
            this.Hide();
            hOfertas.Show();
        }

        private void btnHCalificaciones_Click(object sender, EventArgs e)
        {
            HistorialCalificaciones hCalificaciones = new HistorialCalificaciones(this, usuario.Cod_usuario.ToString());
            this.Hide();
            hCalificaciones.Show();
        }

    }
}
