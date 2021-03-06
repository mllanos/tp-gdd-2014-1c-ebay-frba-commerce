﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Configuration;

namespace FrbaCommerce.Historial_Cliente
{
    public partial class HistorialCalificaciones : Form
    {
        HistorialCliente hCliente;
        string cod_usuario;

        public HistorialCalificaciones(HistorialCliente h, string cod_us)
        {
            InitializeComponent();
            hCliente = h;
            cod_usuario = cod_us;
        }

        private void HistorialCalificaciones_FormClosing(object sender, FormClosingEventArgs e)
        {
            hCliente.Show();
        }

        private void HistorialCalificaciones_Load(object sender, EventArgs e)
        {
            string connectionString = ConfigurationManager.AppSettings["ConexionBD"];
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string sql = "EBAY.historial_cliente_ver_calificaciones";
                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@cod_usuario", SqlDbType.Decimal).Value = cod_usuario;
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    dataGridView.DataSource = dt;
                }
            }
        }

    }
}
