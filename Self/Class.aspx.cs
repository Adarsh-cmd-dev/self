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
    public partial class Class : System.Web.UI.Page
    {
        Commonfnx fn = new Commonfnx();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                GetClass();
            }
        }

        private void GetClass()
        {
            DataTable dt = fn.Fetch("select Row_NUMBER() over(order by (select 1)) as [Sr. No], ClassId, ClassName from Class");
            GridView1.DataSource = dt;
            GridView1.DataBind();
        }

        protected void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                DataTable dt = fn.Fetch("select * from Class where ClassName = '" + txtClass.Text.Trim() + "' ");
                if (dt.Rows.Count == 0)
                {
                    string query = "Insert into Class values('" + txtClass.Text.Trim() + "')";
                    fn.Query(query);
                    lblMsg.Text = "Inserted Successfully !!";
                    lblMsg.CssClass = "alert alert-success !!";
                    txtClass.Text = string.Empty;
                    GetClass();
                }
                else
                {
                    lblMsg.Text = "Entered Class already exists !!";
                    lblMsg.CssClass = "alert alert-danger";
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
            GetClass();
        }

        protected void GridView1_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            GridView1.EditIndex = -1;
            GetClass();
        }

        protected void GridView1_RowEditing(object sender, GridViewEditEventArgs e)
        {
            GridView1.EditIndex = e.NewEditIndex;
        }

        protected void GridView1_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            try
            {
                GridViewRow row = GridView1.Rows[e.RowIndex];
                int cId = Convert.ToInt32(GridView1.DataKeys[e.RowIndex].Values[0]);
                string ClassName = (row.FindControl("txtClassEdit") as TextBox).Text;
                fn.Query("update Class set ClassName ='" + ClassName + "' where ClassId='" + cId + "' ");
                lblMsg.Text = "Class Updated Successfully !!";
                lblMsg.CssClass = "alert alert-success !!";
                //txtClass.Text = string.Empty;
                GridView1.EditIndex = -1;
                GetClass();

            }
            catch (Exception ex)
            {
                Response.Write("<script>alert('" + ex.Message + "');</script>");
            }
        }
    }
}