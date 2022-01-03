using Datos;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using LogicaNegocio;

namespace SGCV
{
    public partial class Usuario : Form
    {
        clsUsuario clUsu = new clsUsuario();
        string validacion;

        public Usuario()
        {
            InitializeComponent();
        }

        #region botones de acceso a las categorias principales

        private void btnAlmacen_Click_1(object sender, EventArgs e)
        {
            Menu pantalla = new Menu();
            pantalla.Show();
            this.Hide();
        }

        private void btnClientes_Click(object sender, EventArgs e)
        {
            Cliente pantalla = new Cliente();
            pantalla.Show();
            this.Hide();
        }

        private void btnProveedor_Click(object sender, EventArgs e)
        {
            Proveedor pantalla = new Proveedor();
            pantalla.Show();
            this.Hide();
        }

        private void btnCompras_Click_1(object sender, EventArgs e)
        {
            Compra pantalla = new Compra();
            pantalla.Show();
            this.Hide();
        }

        private void btnVentas_Click_1(object sender, EventArgs e)
        {
            Venta pantalla = new Venta();
            pantalla.Show();
            this.Hide();
        }

        private void btnRoles_Click_1(object sender, EventArgs e)
        {
            Rol pantalla = new Rol();
            pantalla.Show();
            this.Hide();
        }

        #endregion

        private void Usuario_Load(object sender, EventArgs e)
        {
            dgvUsuarios.DataSource = clUsu.cargarDatosUsuarios();
            dgvUsuarios.Refresh();
        }

        private void dgvUsuarios_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

            txtIdUsu.Text = dgvUsuarios.CurrentRow.Cells[0].Value.ToString();
            cmbRol.Text = dgvUsuarios.CurrentRow.Cells[1].Value.ToString();
            txtNombre.Text = dgvUsuarios.CurrentRow.Cells[2].Value.ToString();
            cmbTipoDoc.SelectedItem = dgvUsuarios.CurrentRow.Cells[3].Value.ToString();
            txtNumDoc.Text = dgvUsuarios.CurrentRow.Cells[4].Value.ToString();
            txtDireccion.Text = dgvUsuarios.CurrentRow.Cells[5].Value.ToString();
            txtTel.Text = dgvUsuarios.CurrentRow.Cells[6].Value.ToString();
            txtEmail.Text = dgvUsuarios.CurrentRow.Cells[7].Value.ToString();
            txtClave.Text = dgvUsuarios.CurrentRow.Cells[8].Value.ToString();
        }


        private void btnGuardar_Click(object sender, EventArgs e)
        {
            if (validacion == "Ok")
            {
                // obtener el valor inicial del combo box
                string name = cmbRol.Text;
                char[] charArray = name.ToCharArray();
                char first = charArray[0];
                string valor = first.ToString();
                string valorDocu = cmbTipoDoc.SelectedItem.ToString();
                //pasar los datos ingresados en los campos de texto a gurdar en la base de datos
                clUsu.agregarUsuario(new Entidades.Usuario(int.Parse(valor), txtNombre.Text, valorDocu, txtNumDoc.Text, txtDireccion.Text, txtTel.Text, txtEmail.Text, txtClave.Text));
                dgvUsuarios.DataSource = clUsu.cargarDatosUsuarios();
                limpiarCampos();
                MessageBox.Show("Guardado exitoso!");
            }
            else
            {
                validarCampos();
            }
        }

        private void btnModificar_Click(object sender, EventArgs e)
        {   
            if (validacion == "Ok")
            {
                string name = cmbRol.Text;
                char[] charArray = name.ToCharArray();
                char first = charArray[0];
                string valor = first.ToString();

                clUsu.modificarUsuario(new Entidades.Usuario(int.Parse(txtIdUsu.Text), int.Parse(valor), txtNombre.Text, cmbTipoDoc.Text, txtNumDoc.Text, txtDireccion.Text, txtTel.Text, txtEmail.Text, txtClave.Text));
                dgvUsuarios.DataSource = clUsu.cargarDatosUsuarios();
                limpiarCampos();
                MessageBox.Show("Modificación exitosa!");
            }
            else
            {
                validarCampos();
            }
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            if (validacion == "Ok")
            {
                string name = cmbRol.Text;
                char[] charArray = name.ToCharArray();
                char first = charArray[0];
                string valor = first.ToString();

                clUsu.eliminarUsuario(new Entidades.Usuario(int.Parse(txtIdUsu.Text), int.Parse(valor), txtNombre.Text, cmbTipoDoc.Text, txtNumDoc.Text, txtDireccion.Text, txtTel.Text, txtEmail.Text, txtClave.Text));
                dgvUsuarios.DataSource = clUsu.cargarDatosUsuarios();
                limpiarCampos();
                MessageBox.Show("Eliminación exitosa!");
            }
            else
            {
                validarCampos();
            }
        }


        public void validarCampos()
        {
            if (cmbRol.Text == "" || txtNombre.Text == "" || cmbTipoDoc.Text == "" || txtNumDoc.Text == "" || txtDireccion.Text == "" || txtTel.Text == "" || txtEmail.Text == "" || txtClave.Text == "")
            {
                MessageBox.Show("Debe completar la informacion");
            }
            else
            {
                 validacion = "Ok";
            }
        }

        public void limpiarCampos()
        {
            txtIdUsu.Text = "";
            cmbRol.Text = "";
            txtNombre.Text = "";
            cmbTipoDoc.Text = "";
            txtNumDoc.Text = "";
            txtDireccion.Text = "";
            txtTel.Text = "";
            txtEmail.Text = "";
            txtClave.Text = "";
        }
    }
}
