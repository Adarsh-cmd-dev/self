using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Self
{
    public partial class Changepassword : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        string strConnString = ConfigurationManager.ConnectionStrings["RegistrationCS"].ConnectionString;
        string query = null;
        SqlCommand cmd;
        byte up;

        protected void btn_update_Click(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection(strConnString);
            con.Open();
            query = "select * from tblLogin ";
            cmd = new SqlCommand(query, con);
            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                if (txt_cpassword.Text == reader["PassWord"].ToString())
                {
                    up = 1;
                }
            }
            reader.Close();
            con.Close();
            if (up == 1)
            {
                con.Open();
                query = "update tblLogin set PassWord=@Password where UserName='" + Session["staff"].ToString() + "'";
                cmd = new SqlCommand(query, con);
                cmd.Parameters.Add(new SqlParameter("@Password", SqlDbType.VarChar, 50));
                cmd.Parameters["@Password"].Value = txt_npassword.Text;
                cmd.ExecuteNonQuery();
                con.Close();
                lbl_msg.Text = "Password changed Successfully";
            }
            else
            {
                lbl_msg.Text = "Please enter correct Current password";
            }
        }
    }
}