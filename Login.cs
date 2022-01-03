using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using Datos;

namespace SGCV
{
    public partial class Login : Form
    {
        public Login()
        {
            InitializeComponent();
        }

        private void txtUsuario_Enter(object sender, EventArgs e)
        {
            if (txtUsuario.Text == "Usuario")
            {
                txtUsuario.Text = "";
                txtUsuario.ForeColor = Color.White;
            }
        }


        private void txtUsuario_Leave_1(object sender, EventArgs e)
        {
            if (txtUsuario.Text == "")
            {
                txtUsuario.Text = "Usuario";
                txtUsuario.ForeColor = Color.OrangeRed;
            }
        }

        private void txtUsuario_TextChanged(object sender, EventArgs e) {}

        private void txtPassword_Enter(object sender, EventArgs e)
        {
            if (txtPassword.Text == "Contraseña")
            {
                txtPassword.Text = "";
                txtPassword.ForeColor = Color.White;
                txtPassword.UseSystemPasswordChar = true;
            }
        }

        private void txtPassword_Leave(object sender, EventArgs e)
        {
            if (txtPassword.Text == "")
            {
                txtPassword.Text = "Contraseña";
                txtPassword.ForeColor = Color.OrangeRed;
                txtPassword.UseSystemPasswordChar = false;
            }
        }

        private void btnEntrar_Click(object sender, EventArgs e)
        {
            //instanciar al metodo conectar de la clase conexion de la capa datos.
            Conexion.conectar();
            // MessageBox.Show("Conexion exitosa");

            SqlCommand consulta = new SqlCommand("select usuario, clave from TLogin where usuario = @vusuario and clave = @vclave", Conexion.conectar());
            consulta.Parameters.AddWithValue("@vusuario", txtUsuario.Text);
            consulta.Parameters.AddWithValue("@vclave", txtPassword.Text);

            SqlDataReader lea = consulta.ExecuteReader();

            if (lea.Read())
            {
                Conexion.conectar().Close();

                Menu pantalla = new Menu();
                pantalla.Show();
                this.Hide();
            }
            else
            {
                MessageBox.Show("Usuario o contraseña incorecto.");

            }
        }
    }
    
}
