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

namespace FrbaCommerce.Comprar_Ofertar
{
    public partial class ComprarOfertar : Form
    {
        SqlDataAdapter pagingAdapter;
        DataTable pagingDT;
        Usuario usuario;
        int currentPage;
        int maxPages;
        int remainder;
        int maxRecords = 10;

        public ComprarOfertar(Usuario us)
        {
            InitializeComponent();
            usuario = us;
            btnPrevPage.Text += " (-" + maxRecords.ToString() + ")";
            btnNextPage.Text += " (+" + maxRecords.ToString() + ")";
        }

        private void ComprarOfertar_Load(object sender, EventArgs e)
        {
            fillFiltrosRubro();
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            fillDataGridView();
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            tbxLikeDescFilter.Text = String.Empty;
            tbxEqualsDescFilter.Text = String.Empty;
            tbxCounter.Text = String.Empty;
            cmbFieldFilter1.Text = String.Empty;
            cmbFieldFilter2.Text = String.Empty;
            dataGridView.Columns.Clear();
            dataGridView.DataSource = null;
        }

        private void dataGridView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            var senderGrid = (DataGridView)sender;

            if (senderGrid.Columns[e.ColumnIndex] is DataGridViewButtonColumn && e.RowIndex >= 0)
            {
                string cod_publicacion = dataGridView.Rows[e.RowIndex].Cells["Codigo"].Value.ToString();
                InfoPublicacion infoProducto = new InfoPublicacion(cod_publicacion, usuario.Cod_usuario.ToString());
                Enabled = false;
                infoProducto.ShowDialog(this);
                Enabled = true;
            }
        }

        private void btnFirstPage_Click(object sender, EventArgs e)
        {
            currentPage = 0;
            showPage(); 
        }

        private void btnPrevPage_Click(object sender, EventArgs e)
        {
            if (currentPage > 0)
            {
                currentPage -= 1;
                showPage(); 
            }
        }

        private void btnNextPage_Click(object sender, EventArgs e)
        {
            if (currentPage < maxPages)
            {
                currentPage += 1;
                showPage();
            }
            
        }

        private void btnLastPage_Click(object sender, EventArgs e)
        {
            currentPage = maxPages;
            showPage(); 
        }

        private void showPage()
        {
            tbxCounter.Text = (currentPage + 1).ToString() + " / " + (maxPages + 1).ToString();

            DataTable tempDT = new DataTable();
            tempDT = pagingDT.Clone();
            if (maxPages > 0)
            {
                for (int i = currentPage * maxRecords; i < currentPage * maxRecords + maxRecords; i++)
                {
                    if (i < maxPages*maxRecords + remainder)
                    {
                        tempDT.ImportRow(pagingDT.Rows[i]);
                    }
                }
            }
            else if (maxPages == 0)
            {
                for (int i = 0; i < remainder; i++)
                {
                    tempDT.ImportRow(pagingDT.Rows[i]);
                }
            }
            dataGridView.DataSource = tempDT;
        }

        private void fillFiltrosRubro()
        {
            string connectionString = ConfigurationManager.AppSettings["ConexionBD"];
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string sql = "SELECT descripcion FROM EBAY.rubro";
                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    conn.Open();
                    SqlDataReader dr = cmd.ExecuteReader();
                    while (dr.Read())
                    {
                        cmbFieldFilter1.Items.Add(dr["descripcion"].ToString());
                        cmbFieldFilter2.Items.Add(dr["descripcion"].ToString());
                    }
                    dr.Close();
                }
            }
        }

        private void fillDataGridView()
        {
            string connectionString = ConfigurationManager.AppSettings["ConexionBD"];
            SqlConnection conn = new SqlConnection(connectionString);
            string sql = "EBAY.comprar_ofertar_publicaciones_activas";
            pagingAdapter = new SqlDataAdapter(sql, conn);
            pagingAdapter.SelectCommand.CommandType = CommandType.StoredProcedure;
            pagingAdapter.SelectCommand.Parameters.Add("@filtroRubro1", SqlDbType.VarChar).Value = cmbFieldFilter1.Text;
            pagingAdapter.SelectCommand.Parameters.Add("@filtroRubro2", SqlDbType.VarChar).Value = cmbFieldFilter2.Text;
            pagingAdapter.SelectCommand.Parameters.Add("@filtroDesc1", SqlDbType.VarChar).Value = tbxLikeDescFilter.Text;
            pagingAdapter.SelectCommand.Parameters.Add("@filtroDesc2", SqlDbType.VarChar).Value = tbxEqualsDescFilter.Text;
            pagingAdapter.SelectCommand.Parameters.Add("@currentDate", SqlDbType.Date).Value = ConfigurationManager.AppSettings["fecha"];
            pagingDT = new DataTable();
            conn.Open();
            pagingAdapter.Fill(pagingDT);
            conn.Close();
            dataGridView.Columns.Clear(); // para evitar duplicaciones de columna Seleccionar
            maxPages = pagingDT.Rows.Count / maxRecords; // maximo nro. de páginas en el catalogo, según maximo nro. de publicaciones
            remainder = pagingDT.Rows.Count % maxRecords; // remanente para la última página
            currentPage = 0;
            showPage();
            DataGridViewButtonColumn btnColumn = new DataGridViewButtonColumn();
            btnColumn.HeaderText = "Seleccionar";
            btnColumn.Text = "Seleccionar";
            btnColumn.UseColumnTextForButtonValue = true;
            dataGridView.Columns.Add(btnColumn);
        }

    }
}
