using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using static Self.Models.Commonfn;

namespace Self
{
    public partial class Login : System.Web.UI.Page
    {
        Commonfnx fn = new Commonfnx();
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnLogin_Click(object sender, EventArgs e)
        {
            string email = inputUser.Value.Trim();
            
            DataTable dt = fn.Fetch("spLogin '"+inputUser.Value+"', '"+inputPassword.Value+"'");

             if (dt.Rows.Count > 0)
            {
                Session["staff"] = email;
                Response.Redirect("Teacher.aspx");
            }
            else
            {
                lblMsg.Text = "Login Failed !!";
                lblMsg.ForeColor = System.Drawing.Color.Red;
            }
        }
    }
}