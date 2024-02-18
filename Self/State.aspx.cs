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
    public partial class State : System.Web.UI.Page
    {
        Commonfnx fn = new Commonfnx();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                GetState();
            }
        }

        private void GetState()
        {
            DataTable dt = fn.Fetch("select Row_NUMBER() over(order by (select 1)) as [Sr. No], StateId, StateName from State");
            GridView1.DataSource = dt;
            GridView1.DataBind();
        }

        protected void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                DataTable dt = fn.Fetch("select * from State where StateName = '" + txtState.Text.Trim() + "' ");
                if (dt.Rows.Count == 0)
                {
                    string query = "Insert into State values('" + txtState.Text.Trim() + "')";
                    fn.Query(query);
                    lblMsg.Text = "Inserted Successfully !!";
                    lblMsg.CssClass = "alert alert-success !!";
                    txtState.Text = string.Empty;
                    GetState();
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
            GetState();
        }

        protected void GridView1_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            GridView1.EditIndex = -1;
            GetState();
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
                int sId = Convert.ToInt32(GridView1.DataKeys[e.RowIndex].Values[0]);
                string stateName = (row.FindControl("txtStateEdit") as TextBox).Text;
                fn.Query("update State set StateName ='" + stateName + "' where StateId='" + sId + "' ");
                lblMsg.Text = "State Updated Successfully !!";
                lblMsg.CssClass = "alert alert-success !!";
                //txtClass.Text = string.Empty;
                GridView1.EditIndex = -1;
                GetState();

            }
            catch (Exception ex)
            {
                Response.Write("<script>alert('" + ex.Message + "');</script>");
            }
        }
    }
}