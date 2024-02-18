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
    public partial class WebForm3 : System.Web.UI.Page
    {
        Commonfnx fn = new Commonfnx();
        protected void Page_Load(object sender, EventArgs e)
        {
           if(!IsPostBack)
            {
                GetTest();
            }
        }

        private void GetTest()
        {
            DataTable dt = fn.Fetch(@"select ROW_NUMBER() over(order by(select 1)) as [Sr.No.],
                                    Id,[Name],Email, DOB,Gender,[Address] from Test ");
            GridView1.DataSource = dt;
            GridView1.DataBind();
        }

        protected void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                if (ddlGender.SelectedValue != "0")
                {
                    string email = txtEmail.Text.Trim();
                    DataTable dt = fn.Fetch("select * from Test where Email='" + email + "' ");
                    if(dt.Rows.Count==0)
                    {
                        string query = "insert into Test values ('"+txtName.Text.Trim()+"','"+txtEmail.Text.Trim()+"','"+txtDOB.Text.Trim()+"','"+ddlGender.SelectedValue+"','"+txtAddress.Text.Trim()+"') ";
                        fn.Query(query);
                        lblmsg.Text = "Insert Successfully!!";
                        lblmsg.CssClass = "alert alert-success";
                        txtName.Text = txtEmail.Text = txtDOB.Text = txtAddress.Text = string.Empty;
                        ddlGender.SelectedIndex = 0;
                        GetTest();
                    }
                    else
                    {
                        lblmsg.Text = "Enter <b> '"+email+"' </b> be already exist!!";
                        lblmsg.CssClass = "alert alert-danger";
                    }
                }
                else
                {
                    lblmsg.Text = "Gender is required!!";
                    lblmsg.CssClass = "alert alert-danger";
                }
            }
            catch(Exception ex)
            {
                Response.Write("<script>alert('"+ex.Message+"');</script>");
            }
           
        }

        protected void GridView1_RowEditing(object sender, GridViewEditEventArgs e)
        {
            GridView1.EditIndex = e.NewEditIndex;
            GetTest();
        }

        protected void GridView1_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            try
            {
                GridViewRow row = GridView1.Rows[e.RowIndex];
                int id = Convert.ToInt32(GridView1.DataKeys[e.RowIndex].Values[0]);
                string name = (row.FindControl("txtNameEdit") as TextBox).Text;
                string email = (row.FindControl("txtEmailEdit") as TextBox).Text;
                string dob = (row.FindControl("txtDOBEdit") as TextBox).Text;
                string address = (row.FindControl("txtAddressEdit") as TextBox).Text;
                fn.Query("update Test set Name='"+name.Trim()+"',Email='"+email.Trim()+"',DOB='"+dob.Trim()+"',Address='"+address.Trim()+"' where Id='"+id+"' ");
                lblmsg.Text = "Updated Successfully!!";
                lblmsg.CssClass = "alert alert-success";
                GridView1.EditIndex = -1;
                GetTest();
            }
            catch (Exception ex)
            {
                Response.Write("<script>alert('" + ex.Message + "');</script>");
            }
        }

        protected void GridView1_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            try
            {
                int id = Convert.ToInt32(GridView1.DataKeys[e.RowIndex].Values[0]);
                fn.Query("Delete from Test where Id='"+id+"' ");
                lblmsg.Text = "Deleted Successfully!!";
                lblmsg.CssClass = "alert alert-success";
                GridView1.EditIndex = -1;
                GetTest();

            }
            catch(Exception ex)
            {
                Response.Write("<script>alert('" + ex.Message + "');</script>");
            }
        }

        protected void GridView1_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            GridView1.EditIndex = -1;
            GetTest();
        }

        protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GridView1.PageIndex = e.NewPageIndex;
            GetTest();
        }
    }
}