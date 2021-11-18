using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CapaLogica;

namespace CapaPresentacion
{
    public partial class inicio : Form
    {
        VentanaProducto ventanaProductos = new VentanaProducto();
        ventanaVenta ventanaVenta = new ventanaVenta();
         logicaNegocio logicadb = new logicaNegocio();
        private int indice = 0;
        public inicio()
        {
            InitializeComponent();
        }

        private void inicio_Load(object sender, EventArgs e)
        {
            lbUusuario.Text = $"Bienvenido u {logicadb.NombreUsuario}";
            ventanaProductos.renderProductosdgv(dgvProductos);
            
            tbInicio.SelectedIndex = 2;
            cbCategoriaProducto.Items.Add("Motor");
            cbCategoriaProducto.Items.Add("Carroceria");
            cbCategoriaProducto.Items.Add("Tren delantero");
            cbCategoriaProducto.Items.Add("Electricidad");
        }
        
        private void tbPrecio_KeyPress(object sender, KeyPressEventArgs e)
        {
            validar.decimales(e);
        }

        private void tbStock_KeyPress(object sender, KeyPressEventArgs e)
        {
            validar.numeros(e);
        }

        private void btnCargaEdita_Click(object sender, EventArgs e)
        {
            switch (Convert.ToInt32(btnCargaEditaProducto.Tag))
            {
                case 1:
                    logicadb.aniadirProducto(tbCuitProducto.Text,tbNombreProducto.Text,Convert.ToInt32(tbStockProducto.Text),Convert.ToDecimal(tbPrecioProducto.Text), cbCategoriaProducto.Text);
                    MessageBox.Show("Cargado");
                    limpiarCamposProductos();
                    ventanaProductos.renderProductosdgv(dgvProductos);
                    break;
                case 2:
                    logicadb.editarProducto(Convert.ToInt32(lbId.Text), tbCuitProducto.Text, tbNombreProducto.Text, Convert.ToInt32(tbStockProducto.Text), Convert.ToDecimal(tbPrecioProducto.Text), cbCategoriaProducto.Text);
                    MessageBox.Show("Editado");
                    btnCargaEditaProducto.Tag = 1;
                    btnCargaEditaProducto.Text = "Cargar";
                    limpiarCamposProductos();
                    ventanaProductos.renderProductosdgv(dgvProductos);
                    break;
                
            }
        }
        private void btnEditar_Click(object sender, EventArgs e)
        {
            //capturamos lo del dgv
            //capturo el index
            indice = dgvProductos.CurrentRow.Index;
            //capturo el id del dgv
            Int32 id = Convert.ToInt32(dgvProductos[0, indice].Value);
            Console.WriteLine(dgvProductos[1, indice].Value);
            //tbCuit.Text = Convert.ToString(dgvProductos[]);
            tbNombreProducto.Text = Convert.ToString(dgvProductos[1,indice].Value);
            cbCategoriaProducto.Text = Convert.ToString(dgvProductos[2, indice].Value);
            tbPrecioProducto.Text = Convert.ToString(dgvProductos[3, indice].Value);
            tbStockProducto.Text = Convert.ToString(dgvProductos[4, indice].Value);
            lbId.Text = Convert.ToString(id);
            tbCuitProducto.Text = Convert.ToString(dgvProductos[5, indice].Value);
            btnCargaEditaProducto.Tag = 2;
            btnCargaEditaProducto.Text = "Editar";
        }
        private void btnEliminar_Click(object sender, EventArgs e)
        {
            indice = dgvProductos.CurrentRow.Index;
            Int32 id = Convert.ToInt32(dgvProductos[0, indice].Value);
            logicadb.elimininarProducto(id);
            ventanaProductos.renderProductosdgv(dgvProductos);
        }
        private void limpiarCamposProductos()
        {
            tbNombreProducto.Text = "";
            cbCategoriaProducto.Text = "";
            tbPrecioProducto.Text = "";
            tbStockProducto.Text = "";
            lbId.Text ="";
            tbCuitProducto.Text = "";
        }

        private void btnVentas_Click(object sender, EventArgs e)
        {
            tbInicio.SelectedIndex = 1;
            ventanaVenta.renderProductosdgv(dgvVenta);
        }

        private void btnProductos_Click(object sender, EventArgs e)
        {
            tbInicio.SelectedIndex = 0;
        }

        private void btnCargarEditarVenta_Click(object sender, EventArgs e)
        {
            MessageBox.Show(tbNombreCliente.Text);
        }


        //pagina de venta 






    }
}
