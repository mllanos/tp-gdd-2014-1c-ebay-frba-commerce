using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Security.Cryptography;

namespace FrbaCommerce
{
    public class Usuario
    {
        private int iCod_usuario;
        private string sRol;
        private int iIdRol;

        public int Cod_usuario
        {
            get { return iCod_usuario; }
            set { iCod_usuario = value; }
        }

        public string Rol
        {
            get { return sRol; }
            set { sRol = value; }
        }

        public int IdRol
        {
            get { return iIdRol; }
            set { iIdRol = value; }

        }
        public void validarNum(object sender, KeyPressEventArgs e)
        {
            if (Char.IsDigit(e.KeyChar))
            {
                e.Handled = false;
            }
            else
                if (Char.IsControl(e.KeyChar)) //permitir teclas de control como retroceso 
                {
                    e.Handled = false;
                }
                else
                {
                    //el resto de teclas pulsadas se desactivan 
                    e.Handled = true;
                    MessageBox.Show("solo numeros", "aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }

        }
        public void validarLetra(object sender, KeyPressEventArgs e)
        {
            if (Char.IsLetter(e.KeyChar))
            {
                e.Handled = false;
            }
            else
                if (Char.IsControl(e.KeyChar)) //permitir teclas de control como retroceso 
                {
                    e.Handled = false;
                }
                else
                {
                    //el resto de teclas pulsadas se desactivan 
                    e.Handled = true;
                    MessageBox.Show("solo letras", "aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }

        }
    }
}
