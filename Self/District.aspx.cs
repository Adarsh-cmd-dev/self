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
    public partial class District : System.Web.UI.Page
    {
        Commonfnx fn = new Commonfnx();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                GetState();
                GetDistrict();
            }
        }

        private void GetDistrict()
        {
            DataTable dt = fn.Fetch(@"select Row_NUMBER() over(order by (select 1)) as [Sr. No], 
                                    d.DistrictId, d.StateId, s.StateName, d.DistrictName from District d inner join State s on s.StateId = d.StateId");
            GridView1.DataSource = dt;
            GridView1.DataBind();
        }

        private void GetState()
        {
            DataTable dt = fn.Fetch("select * from State");
            ddlState.DataSource = dt;
            ddlState.DataTextField = "StateName";
            ddlState.DataValueField = "StateId";
            ddlState.DataBind();
            ddlState.Items.Insert(0, "Select State");
        }

        protected void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                string stateVal = ddlState.SelectedItem.Text;
                DataTable dt = fn.Fetch("select * from District where StatetId = '" + ddlState.SelectedItem.Value + "' and DistrictName = '" + txtDistrict.Text.Trim() + "' ");
                if (dt.Rows.Count == 0)
                {
                    string query = "Insert into District values('" + ddlState.SelectedItem.Value + "','" + txtDistrict.Text.Trim() + "')";
                    fn.Query(query);
                    lblMsg.Text = "Inserted Successfully !!";
                    lblMsg.CssClass = "alert alert-success !!";
                    ddlState.SelectedIndex = 0;
                    txtDistrict.Text = string.Empty;
                    GetDistrict();
                }
                else
                {
                    lblMsg.Text = "Entered District already exists for <b> '" + stateVal + "' </b> !!";
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
            GetDistrict();
        }

        protected void GridView1_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            GridView1.EditIndex = -1;
            GetDistrict();
        }

        protected void GridView1_RowEditing(object sender, GridViewEditEventArgs e)
        {
            GridView1.EditIndex = e.NewEditIndex;
            GetDistrict();
        }

        protected void GridView1_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            try
            {
                GridViewRow row = GridView1.Rows[e.RowIndex];
                int distId = Convert.ToInt32(GridView1.DataKeys[e.RowIndex].Values[0]);
                string ststeId = ((DropDownList)GridView1.Rows[e.RowIndex].Cells[2].FindControl("DropDownList1")).SelectedValue;
                string distName = (row.FindControl("txtDistrictEdit") as TextBox).Text;
                fn.Query("update District set StateId = '" + ststeId + "', DistrictName = '" + distName + "' where DistrictId = '" + distId + "' ");
                lblMsg.Text = "District Updated Successfully !!";
                lblMsg.CssClass = "alert alert-success !!";
                GridView1.EditIndex = -1;
                GetDistrict();
            }
            catch (Exception ex)
            {
                Response.Write("<script>alert('" + ex.Message + "');</script>");
            }
        }
    }
}