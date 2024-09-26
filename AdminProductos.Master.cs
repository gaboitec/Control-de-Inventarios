using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ControlInventario
{
    public partial class AdminProductos : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session.Count == 0)
            {
                Response.Redirect("Login.aspx");
            }

            Usuario.Text = "Usuario: " + Session["Usuario"].ToString();
        }
    }
}