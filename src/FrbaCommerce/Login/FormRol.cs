using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace FrbaCommerce.Login
{
    public partial class FormRol : Form
    {
        System.Windows.Forms.ComboBox comboRol;
        Usuario usuario;
        DataTable tablaRol;

        public FormRol(DataTable tabla, Usuario user)
        {
            usuario = user;
            tablaRol = tabla;
            InitializeComponent();
            InitilizeComboRol(tabla);
           
        }

        public void InitilizeComboRol(DataTable tabla)
        {
            comboRol = new System.Windows.Forms.ComboBox();
            this.comboRol.Location = new System.Drawing.Point(20, 60);
            this.comboRol.Name = "comboBoxRol";
            this.comboRol.Size = new System.Drawing.Size(245, 25);
            this.comboRol.BackColor = System.Drawing.Color.Orange;
            this.comboRol.ForeColor = System.Drawing.Color.Black;
            foreach (DataRow row in tabla.Rows)
            {
                comboRol.Items.Add(Convert.ToString(row[0]));
            }
            this.Controls.Add(this.comboRol);
        }
        private void comboRol_selectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void FormRol_Load(object sender, EventArgs e)
        {

        }

        private void btnSig_Click(object sender, EventArgs e)
        {
            usuario.Rol = Convert.ToString(comboRol.SelectedItem);
           
            Login.FormFuncionalidades FormFunc = new Login.FormFuncionalidades(Convert.ToString(comboRol.SelectedItem), usuario);
            FormFunc.ShowDialog();
            this.Hide();
        }
    }
}
