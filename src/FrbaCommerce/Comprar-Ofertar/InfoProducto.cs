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

    public partial class InfoPublicacion : Form
    {
        string cod_publicacion;
        string cod_usuario;
        DataTable dtInfo;

        public InfoPublicacion(string cod_publ, string cod_us)
        {
            InitializeComponent();
            cod_publicacion = cod_publ;
            cod_usuario = cod_us;
        }

        private void InfoVendedor_Load(object sender, EventArgs e)
        {
            fillDataGridView();
            initializeButtons();
        }

        private void btnBuyOffer_Click(object sender, EventArgs e)
        {
            if(btnBuyOffer.Text.Equals("Comprar"))
            {
                Comprar comprar = new Comprar(tbxCode.Text, cod_usuario);
                Enabled = false;
                comprar.ShowDialog(this);
                Enabled = true;
            }
            else if(btnBuyOffer.Text.Equals("Ofertar"))
            {
                Ofertar ofertar = new Ofertar(tbxCode.Text, cod_usuario);
                Enabled = false;
                ofertar.ShowDialog(this);
                Enabled = true;
            }
        }

        private void btnAskQuestion_Click(object sender, EventArgs e)
        {
            Preguntar pregunta = new Preguntar(tbxCode.Text);
            Enabled = false;
            pregunta.ShowDialog(this);
            Enabled = true;
        }

        private void fillDataGridView()
        {
            string connectionString = ConfigurationManager.AppSettings["ConexionBD"];
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string sql = "EBAY.comprar_ofertar_mostrar_info_publicacion";
                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@cod_publicacion", SqlDbType.Int).Value = cod_publicacion;
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    dtInfo = new DataTable();
                    da.Fill(dtInfo);

                    tbxCode.Text = cod_publicacion;
                    tbxDescription.Text = Convert.ToString(dtInfo.Rows[0]["descripcion"]);
                    tbxPrice.Text = Convert.ToString(dtInfo.Rows[0]["precio"]);
                }
            }
        }

        private void initializeButtons()
        {
            int tipoPubl = Convert.ToInt32(dtInfo.Rows[0]["tipo"]);
            int permisoPreg = Convert.ToInt32(dtInfo.Rows[0]["permiso_preg"]);
            string codUsuarioV = dtInfo.Rows[0]["cod_usuario"].ToString();

            if (tipoPubl == 0) // setear boton a comprar
            {
                btnBuyOffer.Text = "Comprar";
                tbxType.Text = "Compra inmediata";
            }

            if (tipoPubl == 1) // setear boton a subastar
            {
                btnBuyOffer.Text = "Ofertar";
                tbxType.Text = "Subasta";
            }

            if (permisoPreg == 0) // desabilitar preguntar
            {
                btnAskQuestion.Enabled = false;
            }

            if (codUsuarioV.Equals(cod_usuario)) // desabilitar comprar/ofertar/preguntar
            {
                btnAskQuestion.Enabled = false;
                btnBuyOffer.Enabled = false;
            }
        }

        private void tbxPrice_TextChanged(object sender, EventArgs e)
        {

        }

    }
}
