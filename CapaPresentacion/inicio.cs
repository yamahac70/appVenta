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
        private int cantidadActual;
        private Decimal cantidadAnterior = 0;
        private int stock;
        int mEvento = 0,ex,ey;
        public inicio()
        {
            InitializeComponent();
        }

        private void inicio_Load(object sender, EventArgs e)
        {
            lbUusuario.Text = $"Bienvenido";
            ventanaProductos.renderProductosdgv(dgvProductos);
            ventanaVenta.render(dgvVenta);
            tbInicio.SelectedIndex = 2;
            cbCategoriaProducto.Items.Add("Motor");
            cbCategoriaProducto.Items.Add("Carroceria");
            cbCategoriaProducto.Items.Add("Tren delantero");
            cbCategoriaProducto.Items.Add("Electricidad");

            cbCategoriaVentas.Items.Add("Motor");
            cbCategoriaVentas.Items.Add("Carroceria");
            cbCategoriaVentas.Items.Add("Tren delantero");
            cbCategoriaVentas.Items.Add("Electricidad");
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
        //pagina de venta 
        private void btnVentas_Click(object sender, EventArgs e)
        {
            tbInicio.SelectedIndex = 1;
            ventanaVenta.render(dgvVenta);
        }

        private void btnProductos_Click(object sender, EventArgs e)
        {
            tbInicio.SelectedIndex = 0;
            ventanaProductos.renderProductosdgv(dgvProductos);
        }

        private void btnCargarEditarVenta_Click(object sender, EventArgs e)
        {
            
            //cargar items
            logicadb.vender(tbNombreCliente.Text,cbProducto.Text,Convert.ToInt32(nudCantidad.Value),Convert.ToDecimal(tbPrecio.Text), stock);
            cbProducto.Text = "";
            tbNombreCliente.Text = "";
            tbPrecio.Text = "";
            cbCategoriaVentas.Text = "";
            ventanaVenta.render(dgvVenta);
        }

        private void cbCategoriaVentas_SelectedIndexChanged(object sender, EventArgs e)
        {
           var cat= logicadb.categoriasTraer(Convert.ToString(cbCategoriaVentas.SelectedItem));
            if (logicadb.categoriasTraer(Convert.ToString(cbCategoriaVentas.SelectedItem)).Count <= 0 || logicadb.categoriasTraer(Convert.ToString(cbCategoriaVentas.SelectedItem))[0].stock <= 0) {
                Console.WriteLine("No stock");
                cbProducto.Items.Clear();
            }
            else
            {
                cbProducto.Text = "";
                cbProducto.Items.Clear();
                for (int i=0;i<cat.Count;i++)
                {
                    cbProducto.Items.Add(cat[i].nombre);
                }
            }
        }
       
        private void nudCantidad_ValueChanged(object sender, EventArgs e)
        {
             cantidadActual = Convert.ToInt32(nudCantidad.Value);
                String cat = Convert.ToString(cbCategoriaVentas.SelectedItem);
                stock = Convert.ToInt32(logicadb.categoriasTraer(cat)[cbProducto.SelectedIndex].precio);
                tbPrecio.Text = Convert.ToString(Convert.ToDecimal(logicadb.categoriasTraer(cat)[cbProducto.SelectedIndex].precio) * Convert.ToInt32(nudCantidad.Value));
            cantidadAnterior = Convert.ToDecimal(cantidadActual);
        }

        private void btnEliminar_Click_1(object sender, EventArgs e)
        {
            indice = dgvVenta.CurrentRow.Index;
            Int32 id = Convert.ToInt32(dgvVenta[0, indice].Value);
            logicadb.eliminarVenta(id);
            ventanaVenta.render(dgvVenta);
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void panel1_MouseDown(object sender, MouseEventArgs e)
        {
            mEvento= 1;
            ex = e.X;
            ey = e.Y;
        }

        private void panel1_MouseMove(object sender, MouseEventArgs e)
        {
            if (mEvento==1)
            {
                this.SetDesktopLocation(MousePosition.X-ex, MousePosition.Y-ey);
            }
          
        }

        private void panel1_MouseUp(object sender, MouseEventArgs e)
        {
            mEvento = 0;
        }


     






    }
}
