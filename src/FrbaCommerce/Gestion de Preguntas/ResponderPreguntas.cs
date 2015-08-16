using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Configuration;

namespace FrbaCommerce.Gestion_de_Preguntas
{
    public partial class ResponderPreguntas : Form
    {
        GestionDePreguntas selector;
        string cod_usuario;

        public ResponderPreguntas(GestionDePreguntas s, string cod_us)
        {
            InitializeComponent();
            selector = s;
            cod_usuario = cod_us;
        }

        private void ResponderPreguntas_Closing(object sender, FormClosingEventArgs e)
        {
            selector.Show();
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            fillDataGridView();
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            tbxLikeFilter.Text = String.Empty;
            tbxEqualsFilter.Text = String.Empty;
            tbxDateFrom.Text = String.Empty;
            tbxDateTo.Text = String.Empty;
            dataGridView.Columns.Clear();
            dataGridView.DataSource = null;
        }

        private void btnSelectFrom_Click(object sender, EventArgs e)
        {
            mcalFrom.Visible = mcalFrom.Visible ? false : true;
        }

        private void btnSelectTo_Click(object sender, EventArgs e)
        {
            mcalTo.Visible = mcalTo.Visible ? false : true;
        }

        private void mcalFrom_DateSelected(object sender, DateRangeEventArgs e)
        {
            tbxDateFrom.Text = e.Start.ToString().Substring(0, e.Start.ToString().Length - 5);
        }

        private void mcalTo_DateSelected(object sender, DateRangeEventArgs e)
        {
            tbxDateTo.Text = e.Start.ToString().Substring(0, e.Start.ToString().Length - 5);
        }

        private void dataGridView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            var senderGrid = (DataGridView)sender;

            if (senderGrid.Columns[e.ColumnIndex] is DataGridViewButtonColumn && e.RowIndex >= 0)
            {
                string pregunta = dataGridView.Rows[e.RowIndex].Cells["Pregunta"].Value.ToString();
                string cod_publicacion = dataGridView.Rows[e.RowIndex].Cells["Codigo"].Value.ToString();
                string descripcion = dataGridView.Rows[e.RowIndex].Cells["Descripcion"].Value.ToString();
                Responder responder = new Responder(cod_publicacion, pregunta, descripcion, cod_usuario);
                Enabled = false;
                responder.ShowDialog(this);
                Enabled = true;
                fillDataGridView();
            }
        }

        private void fillDataGridView()
        {
            string connectionString = ConfigurationManager.AppSettings["ConexionBD"];
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string sql = "EBAY.gestion_de_preguntas_ver_preguntas";
                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@cod_usuario", SqlDbType.Decimal).Value = cod_usuario;
                    cmd.Parameters.Add("@filtro1", SqlDbType.VarChar).Value = tbxLikeFilter.Text;
                    cmd.Parameters.Add("@filtro2", SqlDbType.VarChar).Value = tbxEqualsFilter.Text;
                    cmd.Parameters.Add("@filtro3", SqlDbType.VarChar).Value = tbxDateFrom.Text;
                    cmd.Parameters.Add("@filtro4", SqlDbType.VarChar).Value = tbxDateTo.Text;
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    dataGridView.Columns.Clear(); // para evitar duplicaciones de columna Seleccionar
                    dataGridView.DataSource = dt;
                    DataGridViewButtonColumn btnColumn = new DataGridViewButtonColumn();
                    btnColumn.HeaderText = "Seleccionar";
                    btnColumn.Text = "Seleccionar";
                    btnColumn.UseColumnTextForButtonValue = true;
                    dataGridView.Columns.Add(btnColumn);
                }
            }
        }

    }
}