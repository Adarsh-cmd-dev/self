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
    public partial class WebForm1 : System.Web.UI.Page
    {
        Commonfnx fn = new Commonfnx();
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack)
            {
                GetTestRegistraion();
            }
        }

        private void GetTestRegistraion()
        {
            DataTable dt = fn.Fetch(@"select ROW_NUMBER() over(order by (select 1)) as [Sr. No.],
                                 Id, [Name], DOB, Gender, Mobile, Email, [Address], [Password] from TestRegistraion");
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
                    DataTable dt = fn.Fetch("select * from TestRegistraion where Email = '" + email + "' ");
                    if (dt.Rows.Count == 0)
                    {
                        string query = "Insert into TestRegistraion values('" + txtName.Text.Trim() + "', '" + txtDOB.Text.Trim() + "', '" + ddlGender.SelectedValue + "', '" + txtMobile.Text.Trim() + "', '" + txtEmail.Text.Trim() + "', '" + txtAddress.Text.Trim() + "', '" + txtPassword.Text.Trim() + "')";
                        fn.Query(query);
                        lblMSG.Text = "Inserted Successfully !!";
                        lblMSG.CssClass = "alert alert-success !!";
                        ddlGender.SelectedIndex = 0;
                        txtName.Text = string.Empty;
                        txtDOB.Text = string.Empty;
                        txtMobile.Text = string.Empty;
                        txtEmail.Text = string.Empty;
                        txtAddress.Text = string.Empty;
                        txtPassword.Text = string.Empty;
                        GetTestRegistraion();
                    }
                    else
                    {
                        lblMSG.Text = "Entered <b>'" + email + "'</b> be already exists !!";
                        lblMSG.CssClass = "alert alert-danger !!";
                    }
                }
                else
                {
                    lblMSG.Text = "Gender is required !!";
                    lblMSG.CssClass = "alert alert-danger !!";
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
            GetTestRegistraion();
        }

        protected void GridView1_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            GridView1.EditIndex = -1;
            GetTestRegistraion();
        }

        protected void GridView1_RowEditing(object sender, GridViewEditEventArgs e)
        {
            GridView1.EditIndex = e.NewEditIndex;
            GetTestRegistraion();
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
                fn.Query("update TestRegistraion set Name = '" + name.Trim() + "', Mobile = '" + mobile.Trim() + "', Password = '" + password.Trim() + "', Address = '" + address.Trim() + "' where Id = '" + id + "' ");
                lblMSG.Text = "Registraion Updated Successfully !!";
                lblMSG.CssClass = "alert alert-success !!";
                GridView1.EditIndex = -1;
                GetTestRegistraion();
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
                fn.Query("Delete from TestRegistraion where Id ='" + id + "' ");
                lblMSG.Text = "Registraion Deleted Successfully !!";
                lblMSG.CssClass = "alert alert-success !!";
                GridView1.EditIndex = -1;
                GetTestRegistraion();
            }
            catch (Exception ex)
            {
                Response.Write("<script>alert('" + ex.Message + "');</script>");
            }
        }
    }
}