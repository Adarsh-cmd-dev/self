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
    public partial class Registration : System.Web.UI.Page
    {
        Commonfnx fn = new Commonfnx();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                GetRegistraion();

            }
        }

        private void GetRegistraion()
        {
            DataTable dt = fn.Fetch(@"select Row_NUMBER() over(order by (select 1)) as [Sr. No], 
                                  Id, [Name], DOB, Gender, Mobile, Email, [Address], [Password] from Registraion ");
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
                    DataTable dt = fn.Fetch("select * from Registraion where Email = '" + email + "' ");
                    if (dt.Rows.Count == 0)
                    {
                        string query = "Insert into Registraion values('" + txtName.Text.Trim() + "', '" + txtDOB.Text.Trim() + "', '" + ddlGender.SelectedValue + "', '" + txtMobile.Text.Trim() + "', '" + txtEmail.Text.Trim() + "', '" + txtAddress.Text.Trim() + "', '" + txtPassword.Text.Trim() + "')";
                        fn.Query(query);
                        lblMsg.Text = "Inserted Successfully !!";
                        lblMsg.CssClass = "alert alert-success !!";
                        ddlGender.SelectedIndex = 0;
                        txtName.Text = string.Empty;
                        txtDOB.Text = string.Empty;
                        txtMobile.Text = string.Empty;
                        txtEmail.Text = string.Empty;
                        txtAddress.Text = string.Empty;
                        txtPassword.Text = string.Empty;
                        GetRegistraion();
                    }
                    else
                    {
                        lblMsg.Text = "Entered <b>'" + email + "'</b> be already exists !!";
                        lblMsg.CssClass = "alert alert-danger !!";
                    }
                }
                else
                {
                    lblMsg.Text = "Gender is required !!";
                    lblMsg.CssClass = "alert alert-danger !!";
                }
            }
            catch (Exception ex)
            {
                Response.Write("<script>alert('" + ex.Message + "');</script>");
            }
        }

        protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GridView1.PageIndex = e.NewPageIndex;
            GetRegistraion();
        }

        protected void GridView1_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            GridView1.EditIndex = -1;
            GetRegistraion();
        }

        protected void GridView1_RowEditing(object sender, GridViewEditEventArgs e)
        {
            GridView1.EditIndex = e.NewEditIndex;
            GetRegistraion();
        }

        protected void GridView1_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            try
            {
                GridViewRow row = GridView1.Rows[e.RowIndex];
                int id = Convert.ToInt32(GridView1.DataKeys[e.RowIndex].Values[0]);
                string name = (row.FindControl("txtNameEdit") as TextBox).Text;
                string mobile = (row.FindControl("txtMobileEdit") as TextBox).Text;
                string password = (row.FindControl("txtPasswordEdit") as TextBox).Text;
                string address = (row.FindControl("txtAddressEdit") as TextBox).Text;
                fn.Query("update Registraion set Name = '" + name.Trim() + "', Mobile = '" + mobile.Trim() + "', Password = '" + password.Trim() + "', Address = '" + address.Trim() + "' where Id = '" + id + "' ");
                lblMsg.Text = "Registraion Updated Successfully !!";
                lblMsg.CssClass = "alert alert-success !!";
                GridView1.EditIndex = -1;
                GetRegistraion();
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
                fn.Query("Delete from Registraion where Id ='" + id + "' ");
                lblMsg.Text = "Registraion Deleted Successfully !!";
                lblMsg.CssClass = "alert alert-success !!";
                GridView1.EditIndex = -1;
                GetRegistraion();
            }
            catch (Exception ex)
            {
                Response.Write("<script>alert('" + ex.Message + "');</script>");
            }
        }
    }
}