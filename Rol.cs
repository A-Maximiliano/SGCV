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
    public partial class Rol : Form
    {
        clsRol rol = new clsRol();
        string validacion = "No";

        public Rol()
        {
            InitializeComponent();
        }

        #region Botones de acceso a las Categorias
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

        private void Rol_Load(object sender, EventArgs e)
        {
            dgvRoles.DataSource = rol.cargarDatosRoles();
            //cargarCmbRoles();
        }

        public void cargarCmbRoles()
        {
            Conexion.conectar();
            string consulta = "Select distinct (nombre) from TbRol";
            SqlCommand cmd = new SqlCommand(consulta, Conexion.conectar());
            SqlDataAdapter adap = new SqlDataAdapter(cmd);
            SqlDataReader registro = cmd.ExecuteReader();
            while (registro.Read())
            {
                cmbNomRol.Items.Add(registro["Nombre"].ToString());
            }
        }
        #endregion

        public void validarCampos()
        {
            if (cmbNomRol.Text != "" & txtDesRol.Text != "")
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
            txtDesRol.Text = "";
            cmbNomRol.Text = "";
        }

        private void dgvRoles_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            txtIdRol.Text = dgvRoles.CurrentRow.Cells[0].Value.ToString();
            cmbNomRol.Text = dgvRoles.CurrentRow.Cells[1].Value.ToString();
            txtDesRol.Text = dgvRoles.CurrentRow.Cells[2].Value.ToString();
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            validarCampos();
            if (validacion == "Ok")
            {
                // obtener el valor inicial del combo box
                string tipocom = cmbNomRol.SelectedItem.ToString();
                //pasar los datos ingresados en los campos de texto a gurdar en la base de datos
                rol.agregarRoles(new Entidades.Rol(tipocom, txtDesRol.Text));
                dgvRoles.DataSource = rol.cargarDatosRoles();
                limpiarCampos();
                MessageBox.Show("Guardado exitoso!");
            }
        }

        private void btnModificar_Click(object sender, EventArgs e)
        {
            validarCampos();
            if (validacion == "Ok")
            {
                // obtener el valor inicial del combo box
                string tipocom = cmbNomRol.SelectedItem.ToString();
                //pasar los datos ingresados en los campos de texto a gurdar en la base de datos
                rol.modificarRoles(new Entidades.Rol(int.Parse(txtIdRol.Text), tipocom, txtDesRol.Text));
                dgvRoles.DataSource = rol.cargarDatosRoles();
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
                string tipocom = cmbNomRol.SelectedItem.ToString();
                //pasar los datos ingresados en los campos de texto a gurdar en la base de datos
                rol.elimnarRoles(new Entidades.Rol(int.Parse(txtIdRol.Text), tipocom, txtDesRol.Text));
                dgvRoles.DataSource = rol.cargarDatosRoles();
                limpiarCampos();
                MessageBox.Show("Eliminación exitosa!");
            }
        }
    }
}
