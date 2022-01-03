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
using LogicaNegocio;

namespace SGCV
{
    public partial class Menu : Form
    {

        clsArt_Cat art_Cat = new clsArt_Cat();
        string validacion = "No";
        string bandera = "ban";

        public Menu()
        {
            InitializeComponent();
        }

        #region Habilitar o deshabilitar campos  de texto y etiquetas
        private void Menu_Load(object sender, EventArgs e)
        {
            cmbSelectdArtCat.Text = "Articulos";           
        }

       public void deshabilitarOpcArt()
        {
            cmbIdCateg.Enabled = false;
            cmbIdCateg.Visible = false;
            txtCodigo.Enabled = false;
            txtCodigo.BorderStyle = BorderStyle.None;
            txtStock.Enabled = false;
            txtStock.BorderStyle = BorderStyle.None;
            txtPrecio.Enabled = false;
            txtPrecio.BorderStyle = BorderStyle.None;
            lblcat.Text = "";
            lblcod.Text = "";
            lblprecio.Text = "";
            lblstock.Text = "";

        }

        public void habilitarOpcArt()
        {
            cmbIdCateg.Enabled = true;
            cmbIdCateg.Visible = true;
            txtCodigo.Enabled = true;
            txtCodigo.BorderStyle = BorderStyle.Fixed3D;           
            txtStock.Enabled = true;
            txtStock.BorderStyle = BorderStyle.Fixed3D;
            txtPrecio.Enabled = true;
            txtPrecio.BorderStyle = BorderStyle.Fixed3D;
            lblcat.Text = "Id Categoría";
            lblcod.Text = "Código";
            lblprecio.Text = "Precio";
            lblstock.Text = "Stock";

        }

        private void cmbSelectdArtCat_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbSelectdArtCat.Text == "Articulos")
            {
                bandera = "art";
                dgvArticulo.DataSource = art_Cat.cargarArticulos();
                habilitarOpcArt();
            }
            else
            {
                bandera = "cat";
                dgvArticulo.DataSource = art_Cat.cargarDatosCateg();
                deshabilitarOpcArt();

            }
        }

        #endregion

        #region Botones de acceso a las categorias del Menu
        private void btnClientes_Click(object sender, EventArgs e)
        {
            
            Cliente pantalla = new Cliente();
            pantalla.Show();
            this.Hide();
        }

        private void btnAlmacen_Click(object sender, EventArgs e)
        {

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

        #endregion

        /* public void validarCampos()
         {
             if (cmbIdCateg.Text != "" & cmbIdUsu.Text != "" & cmbTipoComprobante.Text != "" & txtNumComp.Text != "" & txtImpuesto.Text != "" & txtTotal.Text != "")
             {
                 validacion = "Ok";
             }
             else
             {
                 MessageBox.Show("Debe completar la informacion");
             }
         }*/

        public void limpiarCampos()
         {                      
                txtIdArticulo_Catg.Text = "";
                cmbIdCateg.Text = "";
                txtCodigo.Text = "";
                txtNombre.Text = "";
                txtPrecio.Text = "";
                txtStock.Text = "";
                txtDescripcion.Text = "";           
        }

        private void dgvArticulo_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (bandera == "art")
            {
                txtIdArticulo_Catg.Text = dgvArticulo.CurrentRow.Cells[0].Value.ToString();
                cmbIdCateg.Text = dgvArticulo.CurrentRow.Cells[1].Value.ToString();
                txtCodigo.Text = dgvArticulo.CurrentRow.Cells[2].Value.ToString();
                txtNombre.Text = dgvArticulo.CurrentRow.Cells[3].Value.ToString();
                txtPrecio.Text = dgvArticulo.CurrentRow.Cells[4].Value.ToString();
                txtStock.Text = dgvArticulo.CurrentRow.Cells[5].Value.ToString();
                txtDescripcion.Text = dgvArticulo.CurrentRow.Cells[6].Value.ToString();
            }
            else
            {
                txtIdArticulo_Catg.Text = dgvArticulo.CurrentRow.Cells[0].Value.ToString();
                txtNombre.Text = dgvArticulo.CurrentRow.Cells[1].Value.ToString();
                txtDescripcion.Text = dgvArticulo.CurrentRow.Cells[2].Value.ToString();
            }
           
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            if (bandera == "art")
            {
                // obtener el valor inicial del combo box
                string idcat = cmbIdCateg.Text;
                //pasar los datos ingresados en los campos de texto a gurdar en la base de datos
                art_Cat.agregarArticulos(new Entidades.Articulo(int.Parse(idcat),  txtCodigo.Text, txtNombre.Text, decimal.Parse(txtPrecio.Text),
                int.Parse(txtStock.Text), txtDescripcion.Text));
                dgvArticulo.DataSource = art_Cat.cargarArticulos();
                limpiarCampos();
                MessageBox.Show("Guardado exitoso!");
            }
            else
            {
                //pasar los datos ingresados en los campos de texto a gurdar en la base de datos
                art_Cat.agregarCategorias(new Entidades.Categoria(txtNombre.Text, txtDescripcion.Text));
                dgvArticulo.DataSource = art_Cat.cargarDatosCateg();
                limpiarCampos();
                MessageBox.Show("Guardado exitoso!");
            }
        }

        private void btnModificar_Click(object sender, EventArgs e)
        {
            if (bandera == "art")
            {
                // obtener el valor inicial del combo box
                string idcat = cmbIdCateg.Text;
                //pasar los datos ingresados en los campos de texto a gurdar en la base de datos
                art_Cat.modificarArticulos(new Entidades.Articulo(int.Parse(txtIdArticulo_Catg.Text), int.Parse(idcat), txtCodigo.Text, txtNombre.Text, 
                decimal.Parse(txtPrecio.Text), int.Parse(txtStock.Text), txtDescripcion.Text));
                dgvArticulo.DataSource = art_Cat.cargarArticulos();
                limpiarCampos();
                MessageBox.Show("Modificaión exitosa!");
            }
            else
            {
                //pasar los datos ingresados en los campos de texto a gurdar en la base de datos
                art_Cat.modificarCategorias(new Entidades.Categoria(int.Parse(txtIdArticulo_Catg.Text), txtNombre.Text, txtDescripcion.Text));
                dgvArticulo.DataSource = art_Cat.cargarDatosCateg();
                limpiarCampos();
                MessageBox.Show("Modificaión exitosa!");
            }
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            if (bandera == "art")
            {
                // obtener el valor inicial del combo box
                string idcat = cmbIdCateg.Text;
                //pasar los datos ingresados en los campos de texto a gurdar en la base de datos
                art_Cat.eliminarArticulos(new Entidades.Articulo(int.Parse(txtIdArticulo_Catg.Text), int.Parse(idcat), txtCodigo.Text, txtNombre.Text,
                decimal.Parse(txtPrecio.Text), int.Parse(txtStock.Text), txtDescripcion.Text));
                dgvArticulo.DataSource = art_Cat.cargarArticulos();
                limpiarCampos();
                MessageBox.Show("Eliminación exitosa!");
            }
            
            else
            {
                //pasar los datos ingresados en los campos de texto a gurdar en la base de datos
                art_Cat.eliminarCategorias(new Entidades.Categoria(int.Parse(txtIdArticulo_Catg.Text), txtNombre.Text, txtDescripcion.Text));
                dgvArticulo.DataSource = art_Cat.cargarDatosCateg();
                limpiarCampos();
                MessageBox.Show("Eliminación exitosa!");
            }
        }

      
    }
}
