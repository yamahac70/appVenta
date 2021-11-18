using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;

namespace CapaDatos
{
    public class datosVentas
    {

        private conexion con = new conexion();
        SqlDataReader leer;
        DataTable tabla ;
        SqlCommand comando = new SqlCommand();
        String tablaName = "venta";


        public DataTable mostrar()
        {
            tabla = new DataTable();
            comando.Connection = con.abrirConexion();
            comando.CommandText = "SELECT * FROM " + tablaName;
            leer = comando.ExecuteReader();
            tabla.Load(leer);
            con.cerrarConexion();
            return tabla;
        }

        public void insertar(String nombreCliente,String producto,int cantidad,Decimal precio)
        {
            comando.Connection = con.abrirConexion();
            comando.CommandText = $"INSERT INTO {tablaName}(nombreCliente,producto,cantidad,precio) VALUES('{nombreCliente}','{producto}','{cantidad}', '{precio}' )";
            comando.ExecuteNonQuery();
            con.cerrarConexion();
        }
        public void actualizarCliente(int id, String nombreCliente, String producto, int cantidad, Decimal precio)
        {
            comando.Connection = con.abrirConexion();
            comando.CommandText = $"UPDATE {tablaName} SET nombreCliente='{nombreCliente}' producto='{producto}' cantidad='{cantidad}' precio='{precio}' WHERE codigo='{id}'";
           
            comando.ExecuteNonQuery();
            con.cerrarConexion();
        }
        public void eliminar(int id)
        {
            comando.Connection = con.abrirConexion();
            comando.CommandText = $"DELETE FROM {tablaName} WHERE id = '{id}'";
            comando.ExecuteNonQuery();
            con.cerrarConexion();
        }


    }
}
