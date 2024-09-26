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
    public partial class Clientes : System.Web.UI.Page
    {

        DataTable tabla_cliente = new DataTable();
        DataTable tabla_depa = new DataTable();
        DataTable tabla_muni = new DataTable();
        
        public void limpiar()
        {
            TbCodigo.Text = "";
            TbNIT.Text = "";
            TbNombre1.Text = "";
            TbNombre2.Text = "";
            TbOtrosNombres.Text = "";
            TbApellido1.Text = "";
            TbApellido2.Text = "";
            TbApellidoCas.Text = "";
            TbDireccion.Text = "";
            DropDownDepa.SelectedValue = "0";
            tabla_muni.Rows.Clear();
            DropDownMuni.DataSource = tabla_muni;
            DropDownMuni.DataBind();
            TbTelefono.Text = "";
            TbCorreo.Text = "";
            TextBoxBuscar.Text = "";

            TbNIT.Focus();
            Guardar.Enabled = true;
            Editar.Enabled = false;
            Eliminar.Enabled = false;
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            tabla_cliente.Columns.Add("Codigo");//0
            tabla_cliente.Columns.Add("NIT");//1
            tabla_cliente.Columns.Add("1er Nombre");//2
            tabla_cliente.Columns.Add("2do Nombre");//3
            tabla_cliente.Columns.Add("Otros Nombres");//4
            tabla_cliente.Columns.Add("1er Apellido");//5
            tabla_cliente.Columns.Add("2do Apellido");//6
            tabla_cliente.Columns.Add("Apellido Casada");//7
            tabla_cliente.Columns.Add("Direccion");//8
            tabla_cliente.Columns.Add("Codigo departamento");//9
            tabla_cliente.Columns.Add("Departamento");//10
            tabla_cliente.Columns.Add("Codigo de municipio");//11
            tabla_cliente.Columns.Add("Municipio");//12
            tabla_cliente.Columns.Add("Telefono");//13
            tabla_cliente.Columns.Add("Correo");//14

            cargarCliente();

            if (!IsPostBack)
            {
                tabla_depa.Columns.Add("Codigo");
                tabla_depa.Columns.Add("Departamento");
                cargarDepas();
            }

            tabla_muni.Columns.Add("Codigo");
            tabla_muni.Columns.Add("Municipio");

            TbCodigo.Enabled = false;

            Guardar.Enabled = true;
            Editar.Enabled = false;
            Eliminar.Enabled = false;
        }

        public void cargarDepas()
        {

            StreamReader leer = new StreamReader(Server.MapPath("Archivos/Departamentos.txt"));
            while (!leer.EndOfStream)
            {
                string linea = leer.ReadLine();
                string[] auxdepa = linea.Split(',');
                tabla_depa.Rows.Add(auxdepa);
            }
            leer.Close();

            DropDownDepa.SelectedIndexChanged -= DropDownDepa_SelectedIndexChanged;
            DropDownDepa.DataTextField = "Departamento";
            DropDownDepa.DataValueField = "Codigo";
            DropDownDepa.DataSource = tabla_depa;
            DropDownDepa.DataBind();
            DropDownDepa.SelectedIndexChanged += DropDownDepa_SelectedIndexChanged;
        }

        protected void DropDownDepa_SelectedIndexChanged(object sender, EventArgs e)
        {
            cargarMunis();
        }

        public void cargarMunis()
        {
            tabla_muni.Rows.Clear();
            string cod_depa = DropDownDepa.SelectedValue.ToString();

            StreamReader leer = new StreamReader(Server.MapPath("Archivos/Municipios.txt"));
            while (!leer.EndOfStream)
            {
                string linea = leer.ReadLine();
                string[] auxmuni = linea.Split(',');
                if (cod_depa == auxmuni[2].ToString())
                {
                    tabla_muni.Rows.Add(auxmuni[0], auxmuni[1]);
                }
            }
            leer.Close();
            DropDownMuni.DataTextField = "Municipio";
            DropDownMuni.DataValueField = "Codigo";
            DropDownMuni.DataSource = tabla_muni;
            DropDownMuni.DataBind();
        }

        public void cargarCliente()
        {   
            string nomMuni = "";
            string nomDepa = "";
            string codDepa = "";

            StreamReader leer = new StreamReader(Server.MapPath("Archivos/Clientes.txt"));
            while (!leer.EndOfStream)
            {
                string linea = leer.ReadLine();
                string[] auxcliente = linea.Split(',');

                StreamReader leermunis = new StreamReader(Server.MapPath("Archivos/Municipios.txt"));
                while (!leermunis.EndOfStream)
                {
                    linea = leermunis.ReadLine();
                    string[] auxmuni = linea.Split(',');
                    if (auxcliente[9] == auxmuni[0])
                    {
                        nomMuni = auxmuni[1];
                        codDepa = auxmuni[2];
                    }
                }
                leermunis.Close();

                StreamReader leerdepas = new StreamReader(Server.MapPath("Archivos/Departamentos.txt"));
                while (!leerdepas.EndOfStream)
                {
                    linea = leerdepas.ReadLine();
                    string[] auxdepa = linea.Split(',');
                    if (codDepa == auxdepa[0])
                    {
                        nomDepa = auxdepa[1];
                    }
                }
                leerdepas.Close();

                tabla_cliente.Rows.Add(auxcliente[0], auxcliente[7], auxcliente[1], auxcliente[2], auxcliente[3], auxcliente[4], auxcliente[5], auxcliente[6], auxcliente[8], codDepa, nomDepa, auxcliente[9], nomMuni, auxcliente[10], auxcliente[11]);
            }
            leer.Close();

            GridView1.DataSource = tabla_cliente;
            GridView1.DataBind();
        }

        protected void Guardar_Click(object sender, EventArgs e)
        {
            if (TbNIT.Text != "" && TbNombre1.Text != "" && TbApellido1.Text != "" && TbDireccion.Text != ""
                   && DropDownMuni.SelectedValue != "0" && TbTelefono.Text != "" && TbCorreo.Text != "")
            {
                string Aux_correo = TbCorreo.Text;
                bool error = false;

                foreach (DataRow fila in tabla_cliente.Rows)
                {
                    if (fila[14].ToString() == Aux_correo)
                    {
                        error = true;
                        break;
                    }

                }

                if (!error)
                {
                    int cont = tabla_cliente.Rows.Count + 1;

                    tabla_cliente.Rows.Add(cont, TbNIT.Text, TbNombre1.Text, TbNombre2.Text, TbOtrosNombres.Text, TbApellido1.Text, 
                                            TbApellido2.Text, TbApellidoCas.Text, TbDireccion.Text, DropDownDepa.SelectedValue, 
                                            DropDownDepa.DataTextField, DropDownMuni.SelectedValue, DropDownMuni.DataTextField, TbTelefono.Text, TbCorreo.Text);
                    GridView1.DataBind();

                    string linea = cont.ToString() + ',' + TbNombre1.Text + ',' + TbNombre2.Text + ',' + TbOtrosNombres.Text 
                                    + ',' + TbApellido1.Text + ',' + TbApellido2.Text + ',' + TbApellidoCas.Text + ',' + TbNIT.Text 
                                    + ',' + TbDireccion.Text + ',' + DropDownMuni.SelectedValue + ',' + TbTelefono.Text + ',' + TbCorreo.Text;

                    StreamWriter escribir = new StreamWriter(Server.MapPath("Archivos/Clientes.txt"), true);
                    escribir.WriteLine(linea);
                    escribir.Close();

                    Response.Write("<script>alert('El cliente fue ingresado correctamente')</script>");

                    tabla_cliente.Rows.Clear();
                    cargarCliente();

                    limpiar();
                }
                else
                {
                    Response.Write("<script>alert('El correo ingresado ya está en uso')</script>");
                    TbCorreo.Text = "";
                }
            }
            else Response.Write("<script>alert('Uno o más campos están vacíos, llene todos los campos')</script>");
        }

        protected void BotonBuscar_Click(object sender, EventArgs e)
        {
            string aux = TextBoxBuscar.Text;
            foreach (DataRow fila in tabla_cliente.Rows)
            {
                if (aux == fila[0].ToString())
                {
                    Guardar.Enabled = false;
                    Editar.Enabled = true;
                    Eliminar.Enabled = true;

                    TbCodigo.Text = fila[0].ToString();
                    TbNIT.Text = fila[1].ToString();
                    TbNombre1.Text = fila[2].ToString();
                    TbNombre2.Text = fila[3].ToString();
                    TbOtrosNombres.Text = fila[4].ToString();
                    TbApellido1.Text = fila[5].ToString();
                    TbApellido2.Text = fila[6].ToString();
                    TbApellidoCas.Text = fila[7].ToString();
                    TbDireccion.Text = fila[8].ToString();
                    StreamReader leerMuni = new StreamReader(Server.MapPath("Archivos/Municipios.txt"));
                    while (!leerMuni.EndOfStream)
                    {
                        string linea = leerMuni.ReadLine();
                        string[] dato = linea.Split(',');
                        if (dato[0] == fila[11].ToString())
                        {
                            DropDownDepa.SelectedValue = dato[2].ToString();
                            cargarMunis();
                            DropDownMuni.SelectedValue = dato[0].ToString();
                            break;
                        }
                    }
                    leerMuni.Close();
                    TbTelefono.Text = fila[13].ToString();
                    TbCorreo.Text = fila[14].ToString();
                }
            }
        }

        protected void Editar_Click(object sender, EventArgs e)
        {
            foreach (DataRow fila in tabla_cliente.Rows)
            {
                if (fila[0].ToString() == TbCodigo.Text)
                {
                    fila[1] = TbNIT.Text;
                    fila[2] = TbNombre1.Text;
                    fila[3] = TbNombre2.Text;
                    fila[4] = TbOtrosNombres.Text;
                    fila[5] = TbApellido1.Text;
                    fila[6] = TbApellido2.Text;
                    fila[7] = TbApellidoCas.Text;
                    fila[8] = TbDireccion.Text;
                    StreamReader leerMuni = new StreamReader(Server.MapPath("Archivos/Municipios.txt"));
                    while (!leerMuni.EndOfStream)
                    {
                        string linea = leerMuni.ReadLine();
                        string[] dato = linea.Split(',');
                        if (dato[0] == DropDownMuni.SelectedValue)
                        {
                            StreamReader leerDepa = new StreamReader(Server.MapPath("Archivos/Departamentos.txt"));
                            while (!leerDepa.EndOfStream)
                            {
                                linea = leerDepa.ReadLine();
                                string[] aux = linea.Split(',');
                                if (aux[0] == DropDownDepa.SelectedValue)
                                {
                                    fila[10] = aux[0];
                                    fila[9] = aux[1];
                                    fila[11] = dato[0];
                                    fila[12] = dato[1];
                                    break;
                                }
                            }
                            leerDepa.Close();
                            break;
                        }
                    }
                    leerMuni.Close();
                    fila[13] = TbTelefono.Text;
                    fila[14] = TbCorreo.Text;

                    actualizar();

                    break;
                }
            }

        }

        protected void Eliminar_Click(object sender, EventArgs e)
        {
            foreach (DataRow fila in tabla_cliente.Rows)
            {
                if (fila[0].ToString() == TbCodigo.Text)
                {
                    tabla_cliente.Rows.Remove(fila);
                    actualizar();
                    break;
                }
            }

        }

        public void actualizar()
        {
            StreamWriter escribir = new StreamWriter(Server.MapPath("Archivos/Clientes.txt"));
            foreach (DataRow fil in tabla_cliente.Rows)
            {
                string linea = fil[0].ToString() + ',' + fil[2].ToString() + ',' + fil[3].ToString() + ',' + fil[4].ToString()
                    + ',' + fil[5].ToString() + ',' + fil[6].ToString() + ',' + fil[7].ToString() + ',' + fil[1].ToString()
                    + ',' + fil[8].ToString() + ',' + fil[11].ToString() + ',' + fil[13].ToString() + ',' + fil[14].ToString();

                escribir.WriteLine(linea);
            }
            escribir.Close();

            GridView1.DataBind();

            limpiar();
        }
    }
}