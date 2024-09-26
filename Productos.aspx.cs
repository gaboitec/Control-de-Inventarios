using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ControlInventario
{
    public partial class Productos : System.Web.UI.Page
    {
        DataTable tabla_productos = new DataTable();

        public void limpiar()
        {
            TbCodigo.Text = "";
            TbProducto.Text = "";
            TbPropiedades.Text = "";
            TbDetalles.Text = "";
            TbFecha.Text = "";
            TbPcosto.Text = "";
            TbPventa.Text = "";
            TbExistencia.Text = "";
            TextBoxBuscar.Text = "";

            Guardar.Enabled = true;
            Editar.Enabled = false;
            Eliminar.Enabled = false;
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            cargarProductos();

            TbCodigo.Enabled = false;

            Guardar.Enabled = true;
            Editar.Enabled = false;
            Eliminar.Enabled = false;
        }

        public void cargarProductos()
        {
            tabla_productos.Columns.Add("Codigo");//0
            tabla_productos.Columns.Add("Producto");//1
            tabla_productos.Columns.Add("Propiedades");//2
            tabla_productos.Columns.Add("Detalles");//3
            tabla_productos.Columns.Add("Fecha de Ingreso");//4
            tabla_productos.Columns.Add("Precio Costo");//5
            tabla_productos.Columns.Add("Preci de Venta");//6
            tabla_productos.Columns.Add("Existencias");//7

            StreamReader leer = new StreamReader(Server.MapPath("Archivos/Productos.txt"));
            while (!leer.EndOfStream)
            {
                string linea = leer.ReadLine();
                string[] datos = linea.Split(',');

                tabla_productos.Rows.Add(datos);
            }
            leer.Close();

            GridView1.DataSource = tabla_productos;
            GridView1.DataBind();

        }

        protected void Guardar_Click(object sender, EventArgs e)
        {
            if (TbProducto.Text != "" && TbPropiedades.Text != "" && TbDetalles.Text != ""
                   && TbPcosto.Text != "" && TbPventa.Text != "" && TbExistencia.Text != "")
            {
                string auxProd = TbProducto.Text, auxPropiedades = TbPropiedades.Text, auxDetalles = TbDetalles.Text;
                bool error = false;

                foreach (DataRow fila in tabla_productos.Rows)
                {
                    if (fila[1].ToString() == auxProd && fila[2].ToString() == auxPropiedades && fila[3].ToString() == auxDetalles)
                    {
                        error = true;
                        break;
                    }

                }

                if (!error)
                {
                    int cont = tabla_productos.Rows.Count + 1;

                    tabla_productos.Rows.Add(cont, TbProducto.Text, TbPropiedades.Text, TbDetalles.Text, 
                                            TbFecha.Text, TbPcosto.Text, TbPventa.Text, TbExistencia.Text);
                    GridView1.DataBind();

                    string linea = cont.ToString() + ',' + TbProducto.Text + ',' + TbPropiedades.Text + ',' + TbDetalles.Text
                                    + ',' + TbFecha.Text + ',' + TbPcosto.Text + ',' + TbPventa.Text + ',' + TbExistencia.Text;

                    StreamWriter escribir = new StreamWriter(Server.MapPath("Archivos/Productos.txt"), true);
                    escribir.WriteLine(linea);
                    escribir.Close();

                    Response.Write("<script>alert('El producto fue ingresado correctamente')</script>");

                    tabla_productos.Rows.Clear();
                    cargarProductos();

                    limpiar();
                }
                else
                {
                    Response.Write("<script>alert('El producto ingresado ya se encuentra en la base de datos')</script>");
                    limpiar();
                }
            }
            else Response.Write("<script>alert('Uno o más campos están vacíos, llene todos los campos')</script>");

        }

        protected void Buscar_Click(object sender, EventArgs e)
        {
            string aux = TextBoxBuscar.Text;
            foreach (DataRow fila in tabla_productos.Rows)
            {
                if (aux == fila[0].ToString())
                {
                    Guardar.Enabled = false;
                    Editar.Enabled = true;
                    Eliminar.Enabled = true;

                    TbCodigo.Text = fila[0].ToString();
                    TbProducto.Text = fila[1].ToString();
                    TbPropiedades.Text = fila[2].ToString();
                    TbDetalles.Text = fila[3].ToString();
                    TbFecha.Text = fila[4].ToString();
                    TbPcosto.Text = fila[5].ToString();
                    TbPventa.Text = fila[6].ToString();
                    TbExistencia.Text = fila[7].ToString();
                }
            }
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
    }
}