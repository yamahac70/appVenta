using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaDatos
{
   public class DatosUsuarios
    {

        private conexion con = new conexion();
        SqlDataReader leer;
        DataTable tabla = new DataTable();
        SqlCommand comando = new SqlCommand();
        String tablaName = "usuarios";


        public DataTable mostrar()
        {
            comando.Connection = con.abrirConexion();
            comando.CommandText = "SELECT * FROM " + tablaName;
            leer = comando.ExecuteReader();
            tabla.Load(leer);
            con.cerrarConexion();
            return tabla;
        }
        public Boolean mostrar(String usuario, String contrasenia)
        {
            comando.Connection = con.abrirConexion();
            comando.CommandText = $"SELECT * FROM {tablaName} WHERE nombre='{usuario}' AND contrasenia='{contrasenia}'";
           
            leer = comando.ExecuteReader();
            if (leer.Read())
            {
                con.cerrarConexion();
                return true;
            }
            else
            {
                con.cerrarConexion();
                return false;
            }
           
           
        }

        public void insertar(String nombre, String contrasenia)
        {
            comando.Connection = con.abrirConexion();
            comando.CommandText = $"INSERT INTO {tablaName}(nombre,contrasenia) VALUES('{nombre}','{contrasenia}' )";
            comando.ExecuteNonQuery();
            con.cerrarConexion();
        }
        public void actualizarUsuario(int id, String nombre, String contrasenia)
        {
            comando.Connection = con.abrirConexion();
            comando.CommandText = $"UPDATE {tablaName} SET nombre='{nombre}' contrasenia='{contrasenia}'  WHERE id='{id}'";
            comando.ExecuteNonQuery();
            con.cerrarConexion();
        }
        public void eliminar(int id)
        {
            comando.Connection = con.abrirConexion();
            comando.CommandText = $"DELETE FROM {tabla} WHERE id = '{id}'";
            comando.ExecuteNonQuery();
            con.cerrarConexion();
        }




    }
}
