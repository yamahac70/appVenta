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
    class ventanaVenta
    {
        logicaNegocio logicadb = new logicaNegocio();
        public void renderProductosdgv(DataGridView dgv)
        {
            dgv.DataSource = "";
            dgv.DataSource = logicadb.traerVentas();
            dgv.ReadOnly = true;
        }
    }
}
