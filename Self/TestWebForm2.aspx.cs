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
    public partial class TestWebForm2 : System.Web.UI.Page
    {
        Commonfnx fn = new Commonfnx();
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack)
            {
                GetState();
                GetTestWebForm();
            }
        }

        private void GetTestWebForm()
        {
            DataTable dt = fn.Fetch(@"select ROW_NUMBER() over(order by(select 1)) as [Sr.No.],
                                    t.Id, t.[Name], t.Mobile, t.StateId, t.DistrictId, s.StateName, d.DistrictName from TestWebForm as t
                                    inner join State as s on s.StateId = t.StateId inner join District as d on d.DistrictId = t.DistrictId ");
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
                string stateid = ddlState.SelectedValue;
                string distid = ddlDistrict.SelectedValue;
                DataTable dt = fn.Fetch("select * from TestWebForm where Name ='" +txtName.Text.Trim()+ "' ");
                if(dt.Rows.Count==0)
                {
                    string query = "Insert into TestWebForm values('"+txtName.Text.Trim()+"','" +txtMobile.Text.Trim()+ "','" +stateid+ "','" +distid+ "')";
                    fn.Query(query);
                    lblmsg.Text = "Insert Successfully !!";
                    lblmsg.CssClass = "alert alert-success !!";
                    txtName.Text = string.Empty;
                    txtMobile.Text = string.Empty;
                    ddlState.SelectedIndex = 0;
                    ddlDistrict.SelectedIndex = 0;
                    GetTestWebForm();
                }
                else
                {
                    lblmsg.Text = "Enter Name <b>'" +txtName.Text.Trim()+ "'</b> be already exist !!";
                    lblmsg.CssClass = "alert alert-danger !!";
                }
            }
            catch(Exception ex)
            {
                Response.Write("<script>alert('" +ex.Message+ "');</script>");
            }
        }

        protected void ddlState_SelectedIndexChanged(object sender, EventArgs e)
        {
            string stateid = ddlState.SelectedValue;
            DataTable dt = fn.Fetch("select * from District where StateId ='" +stateid+ "' ");
            ddlDistrict.DataSource = dt;
            ddlDistrict.DataTextField = "DistrictName";
            ddlDistrict.DataValueField = "DistrictId";
            ddlDistrict.DataBind();
            ddlDistrict.Items.Insert(0, "Select District");
        }

        protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GridView1.PageIndex = e.NewPageIndex;
            GetTestWebForm();
        }

        protected void GridView1_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            GridView1.EditIndex = -1;
            GetTestWebForm();
        }

        protected void GridView1_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            try
            {
                int id = Convert.ToInt32(GridView1.DataKeys[e.RowIndex].Values[0]);
                fn.Query("Delete from TestWebForm where Id='" +id+ "' ");
                lblmsg.Text = "Record deleted !!";
                lblmsg.Text = "alert alert-success !!";
                GridView1.EditIndex = -1;
                GetTestWebForm();
            }
            catch(Exception ex)
            {
                Response.Write("<script>alert('"+ex.Message+"');</script>");
            }
        }

        protected void GridView1_RowEditing(object sender, GridViewEditEventArgs e)
        {
            GridView1.EditIndex = e.NewEditIndex;
            GetTestWebForm();
        }

        protected void GridView1_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            try
            {
                GridViewRow row = GridView1.Rows[e.RowIndex];
                int id = Convert.ToInt32(GridView1.DataKeys[e.RowIndex].Values[0]);
                string name = (row.FindControl("txtNameEdit") as TextBox).Text.Trim();
                string mobile = (row.FindControl("txtMobileEdit") as TextBox).Text.Trim();
               
                string stateid = ((DropDownList)GridView1.Rows[e.RowIndex].Cells[3].FindControl("ddlStateGv")).SelectedValue;
                string districtid = ((DropDownList)GridView1.Rows[e.RowIndex].Cells[4].FindControl("ddlDistrictGv")).SelectedValue;
                
                fn.Query("update TestWebForm set Name='" + name + "', Mobile='" + mobile + "', StateId ='" + stateid + "',DistrictId ='" + districtid + "' where Id='" + id + "' ");
                lblmsg.Text = "Record Updated Successfully !!";
                lblmsg.CssClass = "alert alert-success !!";
                GridView1.EditIndex = -1;
                GetTestWebForm();
            }
            catch (Exception ex)
            {
                Response.Write("<script>alert('" + ex.Message + "');</script>");
            }
        }

        protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                if ((e.Row.RowState & DataControlRowState.Edit) > 0)
                {
                    DropDownList ddlState = (DropDownList)e.Row.FindControl("ddlStateGv");
                    DropDownList ddlDistrict = (DropDownList)e.Row.FindControl("ddlDistrictGv");
                    DataTable dt = fn.Fetch("Select * from District where StateId ='" + ddlState.SelectedValue + "' ");
                    ddlDistrict.DataSource = dt;
                    ddlDistrict.DataTextField = "DistrictName";
                    ddlDistrict.DataValueField = "DistrictId";
                    ddlDistrict.DataBind();
                    ddlDistrict.Items.Insert(0, "Select District");
                    string selectedDistrict = DataBinder.Eval(e.Row.DataItem, "DistrictName").ToString();
                    ddlDistrict.Items.FindByText(selectedDistrict).Selected = true;
                }
            }
        }

        protected void ddlStateGv_SelectedIndexChanged(object sender, EventArgs e)
        {
            DropDownList ddlStateSelected = (DropDownList)sender;
            GridViewRow row = (GridViewRow)ddlStateSelected.NamingContainer;
            if (row != null)
            {
                if ((row.RowState & DataControlRowState.Edit) > 0)
                {
                    DropDownList ddlDistrictGv = (DropDownList)row.FindControl("ddlDistrictGv");
                    DataTable dt = fn.Fetch("Select * from District where StateId ='" + ddlStateSelected.SelectedValue + "' ");
                    ddlDistrictGv.DataSource = dt;
                    ddlDistrictGv.DataTextField = "DistrictName";
                    ddlDistrictGv.DataValueField = "DistrictId";
                    ddlDistrictGv.DataBind();
                }
            }
        }

        
    }
}