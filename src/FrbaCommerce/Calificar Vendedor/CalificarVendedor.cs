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

namespace FrbaCommerce.Calificar_Vendedor
{
    public partial class CalificarVendedor : Form
    {
        Usuario usuario;

        public CalificarVendedor(Usuario us)
        {
            InitializeComponent();
            usuario = us;
        }

        private void CalificarVendedor_Load(object sender, EventArgs e)
        {
            loadDataGridView();
        }

        private void dataGridView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            var senderGrid = (DataGridView)sender;

            if (senderGrid.Columns[e.ColumnIndex] is DataGridViewButtonColumn && e.RowIndex >= 0)
            {
                string cod_publicacion = dataGridView.Rows[e.RowIndex].Cells["Codigo"].Value.ToString();
                string descripcion = dataGridView.Rows[e.RowIndex].Cells["Descripcion"].Value.ToString();
                AltaCalificacion altaCalificacion = new AltaCalificacion(cod_publicacion, descripcion, usuario.Cod_usuario.ToString(), this);
                Enabled = false;
                altaCalificacion.ShowDialog(this);
                Enabled = true;
                loadDataGridView();
            }
        }

        private void loadDataGridView()
        {
            string connectionString = ConfigurationManager.AppSettings["ConexionBD"];
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string sql = "EBAY.calificar_vendedor_obtener_compras_subastas";
                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@cod_usuario", SqlDbType.Decimal).Value = usuario.Cod_usuario;
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
