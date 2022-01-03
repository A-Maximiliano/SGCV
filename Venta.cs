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
    public partial class Venta : Form
    {
        clsVenta clsven = new clsVenta();
        string validacion = "No";
        

        public Venta()
        {
            InitializeComponent();
        }

        #region Botones para accesar a las diferentes Categorias
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

        private void btnCompras_Click(object sender, EventArgs e)
        {
            Compra pantalla = new Compra();
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

        private void Venta_Load(object sender, EventArgs e)
        {
            dgvVentas.DataSource = clsven.cargarDatosVentas();
            cargarCmbclientes();
            cargarCmbUsu();
            cargarCmbComprob();
        }

        #endregion

        private void dgvVentas_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            txtIdVenta.Text = dgvVentas.CurrentRow.Cells[0].Value.ToString();
            cmbIdCliente.Text = dgvVentas.CurrentRow.Cells[1].Value.ToString();
            cmbIdUsu.Text = dgvVentas.CurrentRow.Cells[2].Value.ToString();
            cmbTipoComprobante.Text = dgvVentas.CurrentRow.Cells[3].Value.ToString();
            txtNumComp.Text = dgvVentas.CurrentRow.Cells[4].Value.ToString();
            dtFechaRegistro.Text = dgvVentas.CurrentRow.Cells[5].Value.ToString();
            txtImpuesto.Text = dgvVentas.CurrentRow.Cells[6].Value.ToString();
            txtTotal.Text = dgvVentas.CurrentRow.Cells[7].Value.ToString();
        }

        public void cargarCmbclientes()
        {
            Conexion.conectar();
            string consulta = "Select idpersona from TbVenta";
            SqlCommand cmd = new SqlCommand(consulta, Conexion.conectar());
            SqlDataAdapter adap = new SqlDataAdapter(cmd);
            SqlDataReader registro = cmd.ExecuteReader();
            while (registro.Read())
            {
                cmbIdCliente.Items.Add(registro["idpersona"].ToString());
            }

        }

        public void cargarCmbUsu()
        {
            Conexion.conectar();
            string consulta = "select distinct (IdUsuario) from TbVenta";
            SqlCommand cmd = new SqlCommand(consulta, Conexion.conectar());
            SqlDataAdapter adap = new SqlDataAdapter(cmd);
            SqlDataReader registro = cmd.ExecuteReader();
            while (registro.Read())
            {
                cmbIdUsu.Items.Add(registro["IdUsuario"].ToString());
            }

        }

        public void cargarCmbComprob()
        {
            Conexion.conectar();
            string consulta = "Select distinct (tipo_comprobante) from TbVenta";
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
            if (cmbIdCliente.Text != "" & cmbIdUsu.Text != "" & cmbTipoComprobante.Text != "" & txtNumComp.Text != "" & txtImpuesto.Text != "" & txtTotal.Text != "")
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
                string idcli = cmbIdCliente.SelectedItem.ToString();
                string idus = cmbIdUsu.SelectedItem.ToString();
                string tipocom = cmbTipoComprobante.SelectedItem.ToString();
                //pasar los datos ingresados en los campos de texto a gurdar en la base de datos
                clsven.agregarVentas(new Entidades.Venta(int.Parse(idcli), int.Parse(idus), tipocom, txtNumComp.Text, DateTime.Parse(dtFechaRegistro.Text),
                decimal.Parse(txtImpuesto.Text), decimal.Parse(txtTotal.Text)));
                dgvVentas.DataSource = clsven.cargarDatosVentas();
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
                string idcli = cmbIdCliente.SelectedItem.ToString();
                string idus = cmbIdUsu.SelectedItem.ToString();
                string tipocom = cmbTipoComprobante.SelectedItem.ToString();
                //pasar los datos ingresados en los campos de texto a gurdar en la base de datos
                clsven.modificarVentas(new Entidades.Venta(int.Parse(txtIdVenta.Text), int.Parse(idcli), int.Parse(idus), tipocom, txtNumComp.Text, DateTime.Parse(dtFechaRegistro.Text),
                decimal.Parse(txtImpuesto.Text), decimal.Parse(txtTotal.Text)));
                dgvVentas.DataSource = clsven.cargarDatosVentas();
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
                string idcli = cmbIdCliente.SelectedItem.ToString();
                string idus = cmbIdUsu.SelectedItem.ToString();
                string tipocom = cmbTipoComprobante.SelectedItem.ToString();
                //pasar los datos ingresados en los campos de texto a gurdar en la base de datos
                clsven.eliminarVentas(new Entidades.Venta(int.Parse(txtIdVenta.Text), int.Parse(idcli), int.Parse(idus), tipocom, txtNumComp.Text, DateTime.Parse(dtFechaRegistro.Text),
                decimal.Parse(txtImpuesto.Text), decimal.Parse(txtTotal.Text)));
                dgvVentas.DataSource = clsven.cargarDatosVentas();
                limpiarCampos();
                MessageBox.Show("Eliminación exitosa!");
            }
        }
    }
    
}
