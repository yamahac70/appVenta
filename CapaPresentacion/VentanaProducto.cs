using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data;
using CapaLogica;
namespace CapaPresentacion
{
    
    public class VentanaProducto
    {
        logicaNegocio logicadb = new logicaNegocio();
        public void renderProductosdgv(DataGridView dgvProductos)
        {
            dgvProductos.DataSource = "";
            dgvProductos.DataSource = logicadb.renderProductos();
            dgvProductos.ReadOnly = true;
        }
       
    }
}
