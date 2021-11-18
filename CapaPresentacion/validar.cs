using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
namespace CapaPresentacion
{
    class validar
    {
        public static void numeros(KeyPressEventArgs n){

            if (!(char.IsNumber(n.KeyChar)) && (n.KeyChar != (char)Keys.Back))
            {
                MessageBox.Show("Solo se permiten numeros enteros", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                n.Handled = true;
                return;
            }
        }
        public static void decimales(KeyPressEventArgs e){

            if (!(char.IsNumber(e.KeyChar)) && (e.KeyChar != (char)Keys.Back) && !(e.KeyChar == ('.')))
            {
                MessageBox.Show("Solo se permiten numeros ", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                e.Handled = true;
                return;
            }
        }
    }
}
