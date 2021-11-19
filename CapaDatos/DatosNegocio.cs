using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;

namespace CapaDatos
{

    public class DatosNegocio
    {
        
       

        private conexion con = new conexion();
        SqlDataReader leer;
        DataTable tabla ;
        SqlCommand comando = new SqlCommand();
        String tablaName = "productos";
     
        public DataTable mostrar()
        {
            tabla = new DataTable();
            comando.Connection = con.abrirConexion();
            comando.CommandText = "SELECT * FROM "+tablaName;
            leer = comando.ExecuteReader();
            tabla.Load(leer);
            con.cerrarConexion();
            return tabla;
        }
        public DataTable mostrar(String categoria)
        {
            DataColumn colum ;
            tabla = new DataTable();
            
            comando.Connection = con.abrirConexion();
            comando.CommandText = $"SELECT nombre, precio, stock FROM  {tablaName} WHERE categoria = '{categoria}'";
            leer = comando.ExecuteReader();
            
            tabla.Load(leer);
            con.cerrarConexion();
            return tabla;
/*
            if (leer.Read())
            {
                //con.cerrarConexion();
                tabla.Load(leer);
                return tabla;
            }
            else
            {
                con.cerrarConexion();
                colum = new DataColumn();
                DataRow fila;
                colum.DataType = System.Type.GetType("System.String");
                colum.ColumnName = "nombre";
                tabla.Columns.Add(colum);
                colum = new DataColumn();
                colum.DataType = System.Type.GetType("System.Decimal");
                colum.ColumnName = "precio";
                tabla.Columns.Add(colum);
                fila = tabla.NewRow();
                fila["nombre"] = "No existe stock";
                fila["precio"] = 0;
                return tabla;
            }*/
                
        }
        public void insertar(  string cuit, string nombre, int stock,Decimal precio,String categoria)
        {
            comando.Connection = con.abrirConexion();
            comando.CommandText = $"INSERT INTO {tablaName}(cuit,nombre,stock,precio,categoria) VALUES('{cuit}','{nombre}', '{stock}','{precio}','{categoria}' )";
            
            comando.ExecuteNonQuery();
            con.cerrarConexion();
        }
        public void editar(int id, string cuit, string nombre, int stock, Decimal precio, String categoria) {
            comando.Connection = con.abrirConexion();
            comando.CommandText = $"UPDATE {tablaName} SET stock='{stock}', cuit='{cuit}', nombre='{nombre}', precio='{precio}', categoria='{categoria}' WHERE id='{id}'";
            comando.ExecuteNonQuery();
            con.cerrarConexion();
        }
        public List<int> stock(String producto)
        {
            List<int> prod = new List<int>();
            tabla = new DataTable();
            comando.Connection = con.abrirConexion();
            comando.CommandText = $"SELECT * FROM {tablaName} WHERE producto='{producto}'";
            leer = comando.ExecuteReader();
            tabla.Load(leer);
            con.cerrarConexion();
            prod.Add(Convert.ToInt32(tabla.Rows[0]["id"]));
            prod.Add(Convert.ToInt32(tabla.Rows[0]["stock"]));

            return prod;

        }
        public void actualizarStock(int id , int stock)
        {
            comando.Connection = con.abrirConexion();
            comando.CommandText = $"UPDATE {tablaName} SET stock='{stock}' WHERE id='{id}'";
            
            comando.ExecuteNonQuery();
            con.cerrarConexion();
        }
        public void eliminar(int id)
        {
            comando.Connection = con.abrirConexion();
            comando.CommandText = $"DELETE FROM {tablaName} WHERE id= '{id}'";
            comando.ExecuteNonQuery();
            con.cerrarConexion();
        }

        


    }


}
