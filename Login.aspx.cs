using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ControlInventario
{
    public partial class WebForm1 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Session.Clear();
            Usuario.Focus();
        }

        protected void btnIngresar_Click(object sender, EventArgs e)
        {
            if(Usuario.Text != "" && Password.Text != "")
            {
                StreamReader leerUsuarios = new StreamReader(Server.MapPath("Archivos/Usuarios.txt"));
                while (!leerUsuarios.EndOfStream)
                {
                    string linea = leerUsuarios.ReadLine();
                    string[] datos = linea.Split(',');
                    if (datos[3] == Usuario.Text && datos[4] == Password.Text)
                    {
                        Session.Add("Usuario", datos[1] + " " + datos[2]);
                        Response.Redirect("Clientes.aspx");
                    }   
                }
                leerUsuarios.Close();
                Response.Write("<script>alert('Usuario o contraseña incorrectos, inténtelo de nuevo.')</script>");
            }
            else
            {
                Response.Write("<script>alert('Campo vacio, Por favor llene todos los campos.')</script>");
            }
        }

        protected void btnCrear_Click(object sender, EventArgs e)
        {
            if(CrearNombre.Text != "" && CrearApellido.Text != "" && CrearPass.Text != "" && NomUser.Text != "")
            {
                bool error = false;

                StreamReader leerUser = new StreamReader(Server.MapPath("Archivos/Usuarios.txt"));
                while (!leerUser.EndOfStream)
                {
                    string linUser = leerUser.ReadLine();
                    string[] datos = linUser.Split(',');
                    if (datos[3] == NomUser.Text)
                    {
                        error = true;
                        Response.Write("<script>alert('El Nombre de Usuario ya está siendo utilizado')</script>");
                        break;
                    }
                }
                leerUser.Close();

                if (!error)
                {
                    if (CrearPass.Text == PassConfirm.Text)
                    {
                        int cont = 0;
                        StreamReader leer = new StreamReader(Server.MapPath("Archivos/Usuarios.txt"));
                        while (!leer.EndOfStream)
                        {
                            string linea = leer.ReadLine();
                            string[] datos = linea.Split(',');
                            cont = Convert.ToInt32(datos[0]) + 1;
                        }
                        leer.Close();

                        StreamWriter escribir = new StreamWriter(Server.MapPath("Archivos/Usuarios.txt"), true);
                        string lin = cont.ToString() + "," + CrearNombre.Text + "," + CrearApellido.Text + "," + NomUser.Text + "," + PassConfirm.Text;
                        escribir.WriteLine(lin);
                        escribir.Close();

                        Response.Write("<script>alert('Usuario creado con exito')</script>");
                    }
                    else
                    {
                        Response.Write("<script>alert('Las contraseñas no coinciden')</script>");
                    }
                }
                
            }
            else
            {
                Response.Write("<script>alert('Uno o más campos están vacios, llene todos los campos')</script>");
            }
            
        }
    }
}