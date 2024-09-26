using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing.Drawing2D;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ControlInventario
{
    public partial class Pedidos : System.Web.UI.Page
    {
        DataTable tabla_pedidos = new DataTable();
        DataTable tabla_clientes = new DataTable();
        DataTable tabla_productos = new DataTable();
        DataTable tabla_estados = new DataTable();
        DataTable tabla_cantidades = new DataTable();

        public void limpiar()
        {
            TbCodigo.Text = "";
            TbFecha.Text = "";
            DdCliente.SelectedValue = "0";
            TbNomCliente.Text = "";
            TbApCliente.Text = "";
            TbCorreo.Text = "";
            DdProducto.SelectedValue = "0";
            TbProd.Text = "";
            TbPropProd.Text = "";
            TbDetProd.Text = "";
            DdCantidad.SelectedValue = "0";
            TbTotal.Text = "";
            DdEstado.SelectedValue = "0";

            TextBoxBuscar.Text = "";

            Guardar.Enabled = true;
            Editar.Enabled = false;
            Eliminar.Enabled = false;
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            StreamWriter escribir = new StreamWriter(Server.MapPath("Archivos/Cantidades.txt"));
            for(int i = 0; i <= 100; i++)
            {
                escribir.WriteLine(i.ToString() + ',' + i.ToString());
            }
            escribir.Close();

            tabla_pedidos.Columns.Add("Codigo del pedido");//0
            tabla_pedidos.Columns.Add("Fecha");//1
            tabla_pedidos.Columns.Add("Codigo de Cliente");//2
            tabla_pedidos.Columns.Add("Nombre de Cliente");//3
            tabla_pedidos.Columns.Add("Apellido de cliente");//4
            tabla_pedidos.Columns.Add("Correo de cliente");//5
            tabla_pedidos.Columns.Add("Codigo del Producto");//6
            tabla_pedidos.Columns.Add("Producto");//7
            tabla_pedidos.Columns.Add("Propiedades del Producto");//8
            tabla_pedidos.Columns.Add("Detalles del Producto");//9
            tabla_pedidos.Columns.Add("Cantidad");//10
            tabla_pedidos.Columns.Add("Total");//11
            tabla_pedidos.Columns.Add("Codigo del Estado");//12
            tabla_pedidos.Columns.Add("Estado");//13

            cargarPedidos();

            if (!IsPostBack)
            {
                cargarClientes();

                cargarProductos();

                cargarEstados();

                cargarCantidades();
            }

            TbCodigo.Enabled = false;
            TbNomCliente.Enabled = false;
            TbApCliente.Enabled = false;
            TbCorreo.Enabled = false;
            TbProd.Enabled = false;
            TbDetProd.Enabled = false;
            TbPropProd.Enabled = false;
            TbTotal.Enabled = false;

            Guardar.Enabled = true;
            Editar.Enabled = false;
            Eliminar.Enabled = false;
        }

        protected void DdCliente_SelectedIndexChanged(object sender, EventArgs e)
        {
            StreamReader leerClientes = new StreamReader(Server.MapPath("Archivos/Clientes.txt"));
            while (!leerClientes.EndOfStream)
            {
                string linea = leerClientes.ReadLine();
                string[] datC = linea.Split(',');
                if (datC[0] == DdCliente.SelectedValue)
                {
                    TbNomCliente.Text = datC[1];
                    TbApCliente.Text = datC[4];
                    TbCorreo.Text = datC[11];
                    break;
                }
            }
            leerClientes.Close();
        }

        public void cargarClientes()
        {
            tabla_clientes.Columns.Add("Codigo");
            tabla_clientes.Columns.Add("Datos");

            tabla_clientes.Rows.Add("0", "- Seleccione cliente -");

            string cliente = "";
            StreamReader leer = new StreamReader(Server.MapPath("Archivos/Clientes.txt"));
            while (!leer.EndOfStream)
            {
                string linea = leer.ReadLine();
                string[] auxcliente = linea.Split(',');

                cliente = auxcliente[1] + " " + auxcliente[4] + " " + auxcliente[11];
                tabla_clientes.Rows.Add(auxcliente[0], cliente);
            }
            leer.Close();

            DdCliente.SelectedIndexChanged -= DdCliente_SelectedIndexChanged;
            DdCliente.DataTextField = "Datos";
            DdCliente.DataValueField = "Codigo";
            DdCliente.DataSource = tabla_clientes;
            DdCliente.DataBind();
            DdCliente.SelectedIndexChanged += DdCliente_SelectedIndexChanged;
        }

        protected void DdProducto_SelectedIndexChanged(object sender, EventArgs e)
        {
            StreamReader leerProductos = new StreamReader(Server.MapPath("Archivos/Productos.txt"));
            while (!leerProductos.EndOfStream)
            {
                string linea = leerProductos.ReadLine();
                string[] datP = linea.Split(',');
                if (datP[0] == DdProducto.SelectedValue)
                {
                    TbProd.Text = datP[1];
                    TbPropProd.Text = datP[2];
                    TbDetProd.Text = datP[3];
                    break;
                }
            }
            leerProductos.Close();

            cargarTotal();
        }

        public void cargarProductos()
        {
            tabla_productos.Columns.Add("Codigo");
            tabla_productos.Columns.Add("Datos");

            tabla_productos.Rows.Add("0", "- Seleccione producto -");

            string producto = "";
            StreamReader leer = new StreamReader(Server.MapPath("Archivos/Productos.txt"));
            while (!leer.EndOfStream)
            {
                string linea = leer.ReadLine();
                string[] datos = linea.Split(',');

                producto = datos[1] + " " + datos[2] + " " + datos[3];
                tabla_productos.Rows.Add(datos[0], producto);
            }
            leer.Close();

            DdProducto.SelectedIndexChanged -= DdProducto_SelectedIndexChanged;
            DdProducto.DataTextField = "Datos";
            DdProducto.DataValueField = "Codigo";
            DdProducto.DataSource = tabla_productos;
            DdProducto.DataBind();
            DdProducto.SelectedIndexChanged += DdProducto_SelectedIndexChanged;
        }

        protected void DdCantidad_SelectedIndexChanged(object sender, EventArgs e) 
        {
            cargarTotal();
        }

        public void cargarTotal()
        {
            if (DdProducto.SelectedValue != "0" && DdCantidad.SelectedValue != "0")
            {
                StreamReader leer = new StreamReader(Server.MapPath("Archivos/Productos.txt"));
                while (!leer.EndOfStream)
                {
                    string linea = leer.ReadLine();
                    string[] datos = linea.Split(',');
                    int total = Convert.ToInt32(datos[6]);
                    total *= Convert.ToInt32(DdCantidad.Text);
                    TbTotal.Text = total.ToString();
                }
                leer.Close();
            }
        }

        public void cargarCantidades()
        {
            tabla_cantidades.Columns.Add("Codigo");
            tabla_cantidades.Columns.Add("Cantidad");

            StreamReader leer = new StreamReader(Server.MapPath("Archivos/Cantidades.txt"));
            while (!leer.EndOfStream)
            {
                string linea = leer.ReadLine();
                string[] datos = linea.Split(',');

                tabla_cantidades.Rows.Add(datos);
            }
            leer.Close();

            DdCantidad.SelectedIndexChanged -= DdCantidad_SelectedIndexChanged;
            DdCantidad.DataTextField = "Cantidad";
            DdCantidad.DataValueField = "Codigo";
            DdCantidad.DataSource = tabla_cantidades;
            DdCantidad.DataBind();
            DdCantidad.SelectedIndexChanged += DdCantidad_SelectedIndexChanged;
        }

        protected void DdEstado_SelectedIndexChanged(object sender, EventArgs e) { }

        public void cargarEstados()
        {
            tabla_estados.Columns.Add("Codigo");
            tabla_estados.Columns.Add("Estado");

            StreamReader leer = new StreamReader(Server.MapPath("Archivos/Estados.txt"));
            while (!leer.EndOfStream)
            {
                string linea = leer.ReadLine();
                string[] datos = linea.Split(',');

                tabla_estados.Rows.Add(datos);
            }
            leer.Close();

            DdEstado.SelectedIndexChanged -= DdEstado_SelectedIndexChanged;
            DdEstado.DataTextField = "Estado";
            DdEstado.DataValueField = "Codigo";
            DdEstado.DataSource = tabla_estados;
            DdEstado.DataBind();
            DdEstado.SelectedIndexChanged += DdEstado_SelectedIndexChanged;
        }

        public void cargarPedidos()
        {
            string nomCliente = "", apCliente = "", corrCliente = "";
            string prod = "", propProd = "", detProd = "";
            string estado = "";

            StreamReader leer = new StreamReader(Server.MapPath("Archivos/Pedidos.txt"));
            while (!leer.EndOfStream)
            {
                string linea = leer.ReadLine();
                string[] datos = linea.Split(',');

                StreamReader leerClientes = new StreamReader(Server.MapPath("Archivos/Clientes.txt"));
                while (!leerClientes.EndOfStream)
                {
                    linea = leerClientes.ReadLine();
                    string[] datC = linea.Split(',');
                    if (datC[0] == datos[1])
                    {
                        nomCliente = datC[1];
                        apCliente = datC[4];
                        corrCliente = datC[11];
                        break;
                    }
                }
                leerClientes.Close();

                StreamReader leerProductos = new StreamReader(Server.MapPath("Archivos/Productos.txt"));
                while (!leerProductos.EndOfStream)
                {
                    linea = leerProductos.ReadLine();
                    string[] datP = linea.Split(',');
                    if (datP[0] == datos[2])
                    {
                        prod = datP[1];
                        propProd = datP[2];
                        detProd = datP[3];
                        break;
                    }
                }
                leerProductos.Close();

                StreamReader leerEstados = new StreamReader(Server.MapPath("Archivos/Estados.txt"));
                while (!leerEstados.EndOfStream)
                {
                    linea = leerEstados.ReadLine();
                    string[] datE = linea.Split(',');
                    if (datE[0] == datos[6])
                    {
                        estado = datE[1];
                        break;
                    }
                }
                leerProductos.Close();

                tabla_pedidos.Rows.Add(datos[0], datos[3], datos[1], nomCliente, apCliente, corrCliente, datos[2], prod, propProd, detProd, datos[4], datos[5], datos[6], estado);
            }
            leer.Close();

            GridView1.DataSource = tabla_pedidos;
            GridView1.DataBind();

        }

        protected void Guardar_Click(object sender, EventArgs e)
        {
            if (TbNomCliente.Text != "" && TbProd.Text != "" && TbTotal.Text != "" && DdCliente.SelectedValue == "0" && DdProducto.Text == "0" && DdCantidad.SelectedValue == "0")
            {
                int cont = tabla_pedidos.Rows.Count + 1;

                tabla_productos.Rows.Add(cont, TbFecha.Text, DdCliente.SelectedValue, TbNomCliente.Text, TbApCliente.Text, TbCorreo.Text,
                                        DdProducto, TbProd.Text, TbPropProd.Text, TbDetProd.Text, DdCantidad.Text, TbTotal.Text, DdEstado.SelectedValue, DdEstado.Text);
                GridView1.DataBind();

                string linea = cont.ToString() + ',' + DdCliente.SelectedValue + ',' + DdProducto.SelectedValue + ',' +
                            TbFecha.Text + ',' + DdCantidad.Text + ',' + TbTotal.Text + ',' + DdEstado.SelectedValue;

                StreamWriter escribir = new StreamWriter(Server.MapPath("Archivos/Pedidos.txt"), true);
                escribir.WriteLine(linea);
                escribir.Close();

                Response.Write("<script>alert('El pedido se ha realizado con éxito')</script>");

                tabla_pedidos.Rows.Clear();
                cargarPedidos();

                limpiar();
            }
            else Response.Write("<script>alert('Uno o más campos están vacíos, llene todos los campos')</script>");

        }

        protected void Buscar_Click(object sender, EventArgs e)
        {
            string aux = TextBoxBuscar.Text;
            foreach (DataRow fila in tabla_pedidos.Rows)
            {
                if (aux == fila[0].ToString())
                {
                    Guardar.Enabled = false;
                    Editar.Enabled = true;
                    Eliminar.Enabled = true;

                    TbCodigo.Text = fila[0].ToString();
                    TbFecha.Text = fila[1].ToString();
                    DdCliente.SelectedValue = fila[2].ToString();
                    DdProducto.SelectedValue = fila[6].ToString();
                    DdCantidad.Text = fila[10].ToString();
                    TbTotal.Text = fila[11].ToString();
                    DdEstado.SelectedValue = fila[12].ToString();
                }
            }
        }

        protected void Editar_Click(object sender, EventArgs e)
        {

        }

        protected void Eliminar_Click(object sender, EventArgs e)
        {

        }



        /*


        protected void Guardar_Click(object sender, EventArgs e)
        {

        }

        protected void Buscar_Click(object sender, EventArgs e)
        {
            
        }

        protected void Editar_Click(object sender, EventArgs e)
        {
            foreach (DataRow fila in tabla_productos.Rows)
            {
                if (fila[0].ToString() == TbCodigo.Text)
                {
                    fila[1] = TbProducto.Text;
                    fila[2] = TbPropiedades.Text;
                    fila[3] = TbDetalles.Text;
                    fila[4] = TbFecha.Text;
                    fila[5] = TbPcosto.Text;
                    fila[6] = TbPventa.Text;
                    fila[7] = TbExistencia.Text;

                    actualizar();

                    break;
                }
            }

        }

        protected void Eliminar_Click(object sender, EventArgs e)
        {
            foreach (DataRow fila in tabla_productos.Rows)
            {
                if (fila[0].ToString() == TbCodigo.Text)
                {
                    tabla_productos.Rows.Remove(fila);
                    actualizar();
                    break;
                }
            }

        }

        public void actualizar()
        {
            StreamWriter escribir = new StreamWriter(Server.MapPath("Archivos/Productos.txt"));
            foreach (DataRow fil in tabla_productos.Rows)
            {
                string linea = fil[0].ToString() + ',' + fil[1].ToString() + ',' + fil[2].ToString() + ',' + fil[3].ToString()
                    + ',' + fil[4].ToString() + ',' + fil[5].ToString() + ',' + fil[6].ToString() + ',' + fil[7].ToString();

                escribir.WriteLine(linea);
            }
            escribir.Close();

            GridView1.DataBind();

            limpiar();
        }
         */
    }
}