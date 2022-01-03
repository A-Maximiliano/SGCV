using Datos;
using LogicaNegocio;
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

namespace SGCV
{
    public partial class Proveedor : Form
    {
        clsPersona clsPer = new clsPersona();
        string validacion = "No";
        string estado = "true";

        public Proveedor()
        {
            InitializeComponent();
        }

        #region Botones para acceder a los botones
        private void btnAlmacen_Click(object sender, EventArgs e)
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

        private void btnCompras_Click(object sender, EventArgs e)
        {
            Compra pantalla = new Compra();
            pantalla.Show();
            this.Hide();
        }

        private void btnVentas_Click(object sender, EventArgs e)
        {
            Venta pantalla = new Venta();
            pantalla.Show();
            this.Hide();
        }

        private void btnUsuarios_Click(object sender, EventArgs e)
        {
            Usuario pantalla = new Usuario();
            pantalla.Show();
            this.Hide();
        }

        private void btnRoles_Click(object sender, EventArgs e)
        {
            Rol pantalla = new Rol();
            pantalla.Show();
            this.Hide();
        }

        private void Proveedor_Load(object sender, EventArgs e)
        {
            dgvProveedores.DataSource = clsPer.cargarDatosProveedores();
        }
        #endregion


        private void dgvProveedores_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            txtIdPer.Text = dgvProveedores.CurrentRow.Cells[0].Value.ToString();
            cmbTipoPers.Text = dgvProveedores.CurrentRow.Cells[1].Value.ToString();
            txtNombre.Text = dgvProveedores.CurrentRow.Cells[2].Value.ToString();
            cmbTipoDocumento.SelectedItem = dgvProveedores.CurrentRow.Cells[3].Value.ToString();
            txtNumDoc.Text = dgvProveedores.CurrentRow.Cells[4].Value.ToString();
            txtDireccion.Text = dgvProveedores.CurrentRow.Cells[5].Value.ToString();
            txtTel.Text = dgvProveedores.CurrentRow.Cells[6].Value.ToString();
            txtEmail.Text = dgvProveedores.CurrentRow.Cells[7].Value.ToString();
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            validarCampos();
            if (validacion == "Ok")
            {
                // obtener el valor inicial del combo box
                string name = cmbTipoPers.SelectedItem.ToString();
                string valorDocu = cmbTipoDocumento.SelectedItem.ToString();
                //pasar los datos ingresados en los campos de texto a gurdar en la base de datos
                clsPer.agregarPersona(new Entidades.Persona(name, txtNombre.Text, valorDocu, txtNumDoc.Text, txtDireccion.Text, txtTel.Text, txtEmail.Text, estado));
                dgvProveedores.DataSource = clsPer.cargarDatosProveedores();
                limpiarCampos();
                MessageBox.Show("Guardado exitoso!");
            }
        }

        public void validarCampos()
        {
            if (cmbTipoPers.Text != "" & txtNombre.Text != "" & cmbTipoDocumento.Text != "" & txtNumDoc.Text != "" & txtDireccion.Text != "" & txtTel.Text != "" & txtEmail.Text != "")
            {
                validacion = "Ok";
            }
            else
            {
                MessageBox.Show("Debe completar la informacion");
            }
        }

        public void limpiarCampos()
        {
            txtIdPer.Text = "";
            cmbTipoPers.Text = "";
            txtNombre.Text = "";
            cmbTipoDocumento.Text = "Proveedor";
            txtNumDoc.Text = "";
            txtDireccion.Text = "";
            txtTel.Text = "";
            txtEmail.Text = "";
        }

        private void btnModificar_Click(object sender, EventArgs e)
        {
            validarCampos();
            if (validacion == "Ok")
            {
                // obtener el valor inicial del combo box
                string name = cmbTipoPers.SelectedItem.ToString();
                string valorDocu = cmbTipoDocumento.SelectedItem.ToString();
                //pasar los datos ingresados en los campos de texto a gurdar en la base de datos
                clsPer.modificarPersona(new Entidades.Persona(int.Parse(txtIdPer.Text), name, txtNombre.Text, valorDocu, txtNumDoc.Text, txtDireccion.Text, txtTel.Text, txtEmail.Text, estado));
                dgvProveedores.DataSource = clsPer.cargarDatosProveedores();
                limpiarCampos();
                MessageBox.Show("Modificación exitosa!");
            }
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            validarCampos();
            if (validacion == "Ok")
            {
                // obtener el valor inicial del combo box
                string name = cmbTipoPers.SelectedItem.ToString();
                string valorDocu = cmbTipoDocumento.SelectedItem.ToString();
                //pasar los datos ingresados en los campos de texto a gurdar en la base de datos
                clsPer.eliminarPersona(new Entidades.Persona(int.Parse(txtIdPer.Text), name, txtNombre.Text, valorDocu, txtNumDoc.Text, txtDireccion.Text, txtTel.Text, txtEmail.Text, estado));
                dgvProveedores.DataSource = clsPer.cargarDatosProveedores();
                limpiarCampos();
                MessageBox.Show("Eliminación exitosa!");
            }
        }
    }
}
