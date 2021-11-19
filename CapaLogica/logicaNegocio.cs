using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using CapaDatos;


namespace CapaLogica
{
    public class logicaNegocio
    {
        public struct categoria
        {
            public string nombre;
            public Decimal precio;
            public int stock;
        }

        private DatosNegocio datosNegocio = new DatosNegocio();
        private datosVentas datosVenta = new datosVentas();
        private DatosUsuarios usuariosdb = new DatosUsuarios();
        private int cantidad;
        private Decimal precioTotal;
        private String NombreCliente;
        public String NombreUsuario="";
        private DataTable dtVentas;
        DataTable dtProducto;
        public List<categoria> productos;
        public categoria objProducto;

        public logicaNegocio()
        {
           /// datosNegocio.stock("");
           // NombreUsuario = "usuario";
          
        }
        public Boolean verificarUsuario(String usuario, String contrasenia)
        {
            NombreUsuario = usuario;
            return usuariosdb.mostrar(usuario, contrasenia);

        }
        //#######################################################################
        // Ventas
        public DataTable traerVentas()
        {
            dtVentas = new DataTable();
            DataColumn columna;
            DataRow fila;

            //id
            columna = new DataColumn();
            columna.DataType = System.Type.GetType("System.Int32");
            columna.ColumnName = "Id";
            dtVentas.Columns.Add(columna);
            //Nombre de cliente 
            columna = new DataColumn();
            columna.DataType = System.Type.GetType("System.String");
            columna.ColumnName = "Nombre de Cliente";
            dtVentas.Columns.Add(columna);
            //producto
            columna = new DataColumn();
            columna.DataType = System.Type.GetType("System.String");
            columna.ColumnName = "Producto";
            dtVentas.Columns.Add(columna);
            //cantidad
            columna = new DataColumn();
            columna.DataType = System.Type.GetType("System.String");
            columna.ColumnName = "Cantidad";
            dtVentas.Columns.Add(columna);
            //precio
            columna = new DataColumn();
            columna.DataType = System.Type.GetType("System.Decimal");
            columna.ColumnName = "Precio total";
            dtVentas.Columns.Add(columna);
            int i = 0;
            int dt = datosVenta.mostrar().Rows.Count;
            for (i=0;i< dt;i++)
            {
                fila = dtVentas.NewRow();
                fila["Id"] = datosVenta.mostrar().Rows[i]["id"];
                fila["Nombre de Cliente"] = datosVenta.mostrar().Rows[i]["nombreCliente"];
                fila["Producto"] = datosVenta.mostrar().Rows[i]["producto"];
                fila["Cantidad"] = datosVenta.mostrar().Rows[i]["cantidad"];
                fila["Precio total"] = datosVenta.mostrar().Rows[i]["precio"];
                dtVentas.Rows.Add(fila);
            }



            return dtVentas;
        }
        public void vender(String nombreCliente, String producto, int cantidad, Decimal precio,int stockActual)
        {
           ;
            //actualizo el stock
            stockDisminuir(producto,cantidad);
            //realizo la carga
            datosVenta.insertar(nombreCliente,producto,cantidad,precio);

        }
        public  void stockDisminuir(String producto, int cantidad)
        {
           int stockActual= datosNegocio.stock(producto)[0],
               id=datosNegocio.stock(producto)[1];
            datosNegocio.actualizarStock(id, stockActual-cantidad);
        }
        public List<categoria> categoriasTraer(String categoria)
        {
            objProducto = new categoria();
            productos = new List<categoria>();
            int cta = datosNegocio.mostrar(categoria).Rows.Count;
            for (int i = 0; i < cta; i++)
            {
                objProducto.nombre = Convert.ToString(datosNegocio.mostrar(categoria).Rows[i]["nombre"]);
                objProducto.precio = Convert.ToDecimal(datosNegocio.mostrar(categoria).Rows[i]["precio"]);
                objProducto.stock = Convert.ToInt32(datosNegocio.mostrar(categoria).Rows[i]["stock"]);
                productos.Add(objProducto);

            }
            return productos;
            //return datosNegocio.mostrar(categoria);
            // Console.WriteLine($"{datosNegocio.mostrar(categoria)[0].nombre}");
        }
        //eliminar
        public void eliminarVenta(int id)
        {
            datosVenta.eliminar(id);
        }
        //#########################################################################
        //Traer producto
        public void aniadirProducto(String cuit,String nombre,int stock,Decimal precio,String categoria)
        {
            //añadiremos productos
            datosNegocio.insertar(cuit,nombre,stock,precio,categoria);

        }
        public DataTable renderProductos()
        {
            dtProducto = new DataTable();
            DataColumn columna;
            DataRow fila;
            //reconstruimos una tabla con la informacion que nos interesa
            //id
            columna = new DataColumn();
            columna.DataType = System.Type.GetType("System.Int32");
            columna.ColumnName = "Id";
            dtProducto.Columns.Add(columna);
            //nombre
            columna = new DataColumn();
            columna.DataType = System.Type.GetType("System.String");
            columna.ColumnName = "Nombre";
            dtProducto.Columns.Add(columna);
            //categoria
            columna = new DataColumn();
            columna.DataType = System.Type.GetType("System.String");
            columna.ColumnName = "Categoria";
            dtProducto.Columns.Add(columna);
            //precio
            columna = new DataColumn();
            columna.DataType = System.Type.GetType("System.Decimal");
            columna.ColumnName = "Precio";
            dtProducto.Columns.Add(columna);
            //stock
            columna = new DataColumn();
            columna.DataType = System.Type.GetType("System.Int32");
            columna.ColumnName = "Stock";
            dtProducto.Columns.Add(columna);
            //cuit
            columna = new DataColumn();
            columna.DataType = System.Type.GetType("System.String");
            columna.ColumnName = "Cuit";
            dtProducto.Columns.Add(columna);

            int dt=datosNegocio.mostrar().Rows.Count;
            int i = 0;
            for (i=0; i < dt; i++) {

                fila = dtProducto.NewRow();
                fila["Id"] = datosNegocio.mostrar().Rows[i]["id"];
                fila["Nombre"] = datosNegocio.mostrar().Rows[i]["nombre"];
                fila["Categoria"] = datosNegocio.mostrar().Rows[i]["categoria"];
                fila["Precio"] = datosNegocio.mostrar().Rows[i]["precio"];
                fila["Stock"] = datosNegocio.mostrar().Rows[i]["stock"];
                fila["Cuit"] = datosNegocio.mostrar().Rows[i]["cuit"];
                dtProducto.Rows.Add(fila);

            }

            return dtProducto;
        }
        //en la siguiente capa se vera la logica tanto de restar stock y aumentar stock
        
        public void elimininarProducto(int id)
        {
            datosNegocio.eliminar(id);
        }
        public void editarProducto(int id, string cuit, string nombre, int stock, Decimal precio, String categoria)
        {
            datosNegocio.editar(id, cuit, nombre, stock, precio, categoria);
        }



    }
}
