using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;

namespace CapaDatos
{
    
    class conexion
    {
        private string cadenaConexion;

        public SqlConnection conectarDB;


        public conexion()
        {
            //string donde contiene la conexion
           // cadenaConexion = "Data Source = DESKTOP-30DTIQQ\\MISQLEXPRESS;Initial Catalog = VentasC4; User ID = sa; Password=654321";
            cadenaConexion = "Data Source=DESKTOP-OCQK3I5;Initial Catalog=ventaRepuestos; Integrated Security=true";
            //
            conectarDB = new SqlConnection();
            conectarDB.ConnectionString = cadenaConexion;
        }
        public SqlConnection abrirConexion()
        {
            
            conectarDB.Open();
            return conectarDB;
           
        }
        public SqlConnection cerrarConexion()
        {
            conectarDB.Close();
            return conectarDB;
            //MessageBox.Show("La conexión se cerró");
        }
        public SqlConnection getConexion()
        {
            return conectarDB;
        }
    }
}
