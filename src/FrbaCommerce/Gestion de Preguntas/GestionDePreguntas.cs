using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace FrbaCommerce.Gestion_de_Preguntas
{
    public partial class GestionDePreguntas : Form
    {
        Usuario usuario;

        public GestionDePreguntas(Usuario us)
        {
            InitializeComponent();
            usuario = us;
        }

        private void btnRespPreg_Click(object sender, EventArgs e)
        {
            ResponderPreguntas responderPreguntas = new ResponderPreguntas(this, usuario.Cod_usuario.ToString());
            this.Hide();
            responderPreguntas.Show();
        }

        private void btnVerResp_Click(object sender, EventArgs e)
        {
            VerRespuestas verRespuestas = new VerRespuestas(this, usuario.Cod_usuario.ToString());
            this.Hide();
            verRespuestas.Show();
        }

    }
}
