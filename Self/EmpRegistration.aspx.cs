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
    public partial class WebForm2 : System.Web.UI.Page
    {
        Commonfnx fn = new Commonfnx();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                GetState();
                GetEmpRegistraion();

            }
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

        private void GetEmpRegistraion()
        {
            DataTable dt = fn.Fetch(@"select Row_NUMBER() over(order by (select 1)) as [Sr. No], 
                                    e.EmployeeId, e.Email, e.Address, e.StateId, s.StateName, e.DistrictId, d.DistrictName, e.Name, e.DOB, e.Mobile, e.Password, e.Gender from EmpRegistraion e 
                                    inner join State s on e.StateId = s.StateId inner join District d on e.DistrictId = d.DistrictId ");
            GridView1.DataSource = dt;
            GridView1.DataBind();
        }

        protected void btnAdd_Click(object sender, EventArgs e)
        {           
            try
            {
                if (ddlGender.SelectedValue != "0")
                {
                    string stateId = ddlState.SelectedValue;
                    string districtId = ddlDistrict.SelectedValue;
                    DataTable dt = fn.Fetch("select * from EmpRegistraion where Email='"+txtEmail.Text.Trim()+ "'  ");
                    if(dt.Rows.Count==0)
                    {
                        string query = "Insert into EmpRegistraion ( Name, DOB, Gender, Mobile, Email,Password, StateId, DistrictId, Address) values('" + txtName.Text.Trim() + "', '" + txtDOB.Text.Trim() + "', '" + ddlGender.SelectedValue + "', '" + txtMobile.Text.Trim() + "', '" + txtEmail.Text.Trim() + "','" + txtPassword.Text.Trim() + "','" + stateId + "','" + districtId + "', '" + txtAddress.Text.Trim() + "')";                       
                        fn.Query(query);
                        lblMsg.Text = "Inserted Successfully !!";
                        lblMsg.CssClass = "alert alert-success !!";                       
                        txtName.Text = string.Empty;
                        txtDOB.Text = string.Empty;
                        ddlGender.SelectedIndex = 0;
                        txtMobile.Text = string.Empty;
                        txtEmail.Text = string.Empty;              
                        txtPassword.Text = string.Empty;
                        ddlState.SelectedIndex = 0;
                        ddlDistrict.SelectedIndex = 0;
                        txtAddress.Text = string.Empty;
                        GetEmpRegistraion();
                    }
                }
                else
                {
                    lblMsg.Text = "Gender is required !!";
                    lblMsg.CssClass = "alert alert-danger !!";
                }
               
            }
            catch(Exception ex)
            {
                Response.Write("<script>alert('" + ex.Message + "');</script>");
            }
        }

        protected void ddlState_SelectedIndexChanged(object sender, EventArgs e)
        {
            string stateId = ddlState.SelectedValue;
            DataTable dt = fn.Fetch("select * from District where StateId = '" + stateId + "' ");
            ddlDistrict.DataSource = dt;
            ddlDistrict.DataTextField = "DistrictName";
            ddlDistrict.DataValueField = "DistrictId";
            ddlDistrict.DataBind();
            ddlDistrict.Items.Insert(0, "Select District");
        }

        protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GridView1.PageIndex = e.NewPageIndex;
            GetEmpRegistraion();
        }

        protected void GridView1_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            GridView1.EditIndex = -1;
            GetEmpRegistraion();
        }

        protected void GridView1_RowEditing(object sender, GridViewEditEventArgs e)
        {
            GridView1.EditIndex = e.NewEditIndex;
            GetEmpRegistraion();
        }

        protected void GridView1_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            try
            {
                GridViewRow row = GridView1.Rows[e.RowIndex];
                int empId = Convert.ToInt32(GridView1.DataKeys[e.RowIndex].Values[0]);              
                string name = (row.FindControl("txtNameEdit") as TextBox).Text.Trim();
                string mobile = (row.FindControl("txtMobileEdit") as TextBox).Text.Trim();
                string email = (row.FindControl("txtEmailEdit") as TextBox).Text.Trim();
                string password = (row.FindControl("txtPasswordEdit") as TextBox).Text.Trim();
                string stateid = ((DropDownList)GridView1.Rows[e.RowIndex].Cells[5].FindControl("ddlStateGv")).SelectedValue;
                string districtid = ((DropDownList)GridView1.Rows[e.RowIndex].Cells[5].FindControl("ddlDistrictGv")).SelectedValue;
                string address = (row.FindControl("txtAddressEdit") as TextBox).Text.Trim();
                fn.Query("update EmpRegistraion set Name='" + name+ "', Mobile='"+mobile+ "', Email='"+email+ "', Password='"+password+ "', StateId ='" + stateid+ "',DistrictId ='" + districtid+ "',Address ='" + address+ "' where EmployeeId='"+empId+"' ");
                lblMsg.Text = "Record Updated Successfully !!";
                lblMsg.CssClass = "alert alert-success !!";
                GridView1.EditIndex = -1;
                GetEmpRegistraion();
            }
            catch(Exception ex)
            {
                Response.Write("<script>alert('"+ex.Message+"');</script>");
            }
        }

        protected void GridView1_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            try
            {
                int employeeId = Convert.ToInt32(GridView1.DataKeys[e.RowIndex].Values[0]);
                fn.Query("Delete from EmpRegistraion where EmployeeId ='" + employeeId + "' ");
                lblMsg.Text = "Record Deleted Successfully !!";
                lblMsg.CssClass = "alert alert-success !!";
                GridView1.EditIndex = -1;
                GetEmpRegistraion();
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

        protected void btnLogOut_Click(object sender, EventArgs e)
        {
            Session.Abandon();
            Response.Redirect("Login.aspx");
        }
    }
}