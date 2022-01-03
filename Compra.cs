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
    public partial class Compra : Form
    {
        clsCompra clsCom = new clsCompra();
        string validacion = "No";
        string estado = "true";

        public Compra()
        {
            InitializeComponent();
        }

        #region Botones de acceso a Categorias
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

        private void Compra_Load(object sender, EventArgs e)
        {
            cargarCmbProve();
            cargarCmbUsu();
            cargarCmbComprob();
            dgvCompras.DataSource = clsCom.cargarDatosCompras();

        }
        #endregion

        public void cargarCmbProve()
        {
            Conexion.conectar();
            string consulta = "Select idpersona from TbIngreso_compra";
            SqlCommand cmd = new SqlCommand(consulta, Conexion.conectar());
            SqlDataAdapter adap = new SqlDataAdapter(cmd);
            SqlDataReader registro = cmd.ExecuteReader();
            while (registro.Read())
            {
                cmbIdProveedor.Items.Add(registro["idpersona"].ToString());
            }

        }

        public void cargarCmbUsu()
        {
            Conexion.conectar();
            string consulta = "select distinct (IdUsuario) from TbIngreso_compra";
            SqlCommand cmd = new SqlCommand(consulta, Conexion.conectar());
            SqlDataAdapter adap = new SqlDataAdapter(cmd);
            SqlDataReader registro = cmd.ExecuteReader();
            while (registro.Read())
            {
                cmbIdUsu.Items.Add(registro["IdUsuario"].ToString());
            }

        }

        private void dgvCompras_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            txtIdCompra.Text = dgvCompras.CurrentRow.Cells[0].Value.ToString();
            cmbIdProveedor.Text = dgvCompras.CurrentRow.Cells[1].Value.ToString();
            cmbIdUsu.Text = dgvCompras.CurrentRow.Cells[2].Value.ToString();
            cmbTipoComprobante.SelectedItem = dgvCompras.CurrentRow.Cells[3].Value.ToString();
            txtNumComp.Text = dgvCompras.CurrentRow.Cells[4].Value.ToString();
            dtFechaRegistro.Text = dgvCompras.CurrentRow.Cells[5].Value.ToString();
            txtImpuesto.Text = dgvCompras.CurrentRow.Cells[6].Value.ToString();
            txtTotal.Text = dgvCompras.CurrentRow.Cells[7].Value.ToString();
        }

        public void cargarCmbComprob()
        {
            Conexion.conectar();
            string consulta = "Select distinct (tipo_comprobante) from TbIngreso_compra";
            SqlCommand cmd = new SqlCommand(consulta, Conexion.conectar());
            SqlDataAdapter adap = new SqlDataAdapter(cmd);
            SqlDataReader registro = cmd.ExecuteReader();
            while (registro.Read())
            {
                cmbTipoComprobante.Items.Add(registro["tipo_comprobante"].ToString());
            }
        }

        public void validarCampos()
        {
            if (cmbIdProveedor.Text != "" & cmbIdUsu.Text != "" & cmbTipoComprobante.Text != "" & txtNumComp.Text != "" & txtImpuesto.Text != "" & txtTotal.Text != "")
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
            txtNumComp.Text = "";
            txtImpuesto.Text = "";
            txtTotal.Text = "";
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            validarCampos();
            if (validacion == "Ok")
            {
                // obtener el valor inicial del combo box
                string idpro = cmbIdProveedor.SelectedItem.ToString();
                string idus = cmbIdUsu.SelectedItem.ToString();
                string tipocom = cmbTipoComprobante.SelectedItem.ToString();
                //pasar los datos ingresados en los campos de texto a gurdar en la base de datos
                clsCom.agregarCompras(new Entidades.Compra(int.Parse(idpro), int.Parse(idus), tipocom, txtNumComp.Text, DateTime.Parse(dtFechaRegistro.Text), 
                decimal.Parse(txtImpuesto.Text), decimal.Parse(txtTotal.Text)));
                dgvCompras.DataSource = clsCom.cargarDatosCompras();
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
                string idpro = cmbIdProveedor.SelectedItem.ToString();
                string idus = cmbIdUsu.SelectedItem.ToString();
                string tipocom = cmbTipoComprobante.SelectedItem.ToString();
                //pasar los datos ingresados en los campos de texto a gurdar en la base de datos
                clsCom.modificarCompras(new Entidades.Compra(int.Parse(txtIdCompra.Text), int.Parse(idpro), int.Parse(idus), tipocom, txtNumComp.Text, DateTime.Parse(dtFechaRegistro.Text),
                decimal.Parse(txtImpuesto.Text), decimal.Parse(txtTotal.Text)));
                dgvCompras.DataSource = clsCom.cargarDatosCompras();
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
                string idpro = cmbIdProveedor.SelectedItem.ToString();
                string idus = cmbIdUsu.SelectedItem.ToString();
                string tipocom = cmbTipoComprobante.SelectedItem.ToString();
                //pasar los datos ingresados en los campos de texto a gurdar en la base de datos
                clsCom.eliminarCompras(new Entidades.Compra(int.Parse(txtIdCompra.Text), int.Parse(idpro), int.Parse(idus), tipocom, txtNumComp.Text, DateTime.Parse(dtFechaRegistro.Text),
                decimal.Parse(txtImpuesto.Text), decimal.Parse(txtTotal.Text)));
                dgvCompras.DataSource = clsCom.cargarDatosCompras();
                limpiarCampos();
                MessageBox.Show("Eliminación exitosa!");
            }
        }
    }
}
