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
    public partial class TestStudent : System.Web.UI.Page
    {
        Commonfnx fn = new Commonfnx();
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack)
            {
                GetState();
                GetTestStudent();
            }
        }

        private void GetTestStudent()
        {
            DataTable dt = fn.Fetch(@"select ROW_NUMBER() over(order by(select 1))as [Sr.No.],
                                    t.Id, t.[Name], t.DOB, t.[RollNo], s.StateId, s.StateName from TestStudent as t
                                    inner join State as s on s.StateId = t.StateId");
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
                string roll = txtRoll.Text.Trim();
                DataTable dt = fn.Fetch("select * from TestStudent where StateId = '" + ddlState.SelectedValue + "' and RollNo = '" + roll + "'");
                if (dt.Rows.Count == 0)
                {
                    string query = "Insert into TestStudent values('" + txtName.Text.Trim() + "', '" + txtDOB.Text.Trim() + "', '" + txtRoll.Text.Trim() + "',  '" + ddlState.SelectedValue + "')";
                    fn.Query(query);
                    lblmsg.Text = "Inserted Successfully !!";
                    lblmsg.CssClass = "alert alert-success !!";
                    txtName.Text = string.Empty;
                    txtDOB.Text = string.Empty;
                    txtRoll.Text = string.Empty;
                    ddlState.SelectedIndex = 0;
                    GetTestStudent();
                }
                else
                {
                    lblmsg.Text = "Entered Roll No.<b>'" + roll + "'</b> be already exists for selected class !!";
                    lblmsg.CssClass = "alert alert-danger !!";
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
            GetTestStudent();
        }

        protected void GridView1_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            GridView1.EditIndex = -1;
            GetTestStudent();
        }

        protected void GridView1_RowEditing(object sender, GridViewEditEventArgs e)
        {
            GridView1.EditIndex = e.NewEditIndex;
            GetTestStudent();
        }

        protected void GridView1_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            try
            {
                int id = Convert.ToInt32(GridView1.DataKeys[e.RowIndex].Values[0]);
                fn.Query("Delete from TestStudent where Id ='" + id + "' ");
                lblmsg.Text = "Student Deleted Successfully !!";
                lblmsg.CssClass = "alert alert-success !!";
                GridView1.EditIndex = -1;
                GetTestStudent();
            }
            catch (Exception ex)
            {
                Response.Write("<script>alert('" + ex.Message + "');</script>");
            }
        }

        protected void GridView1_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            try
            {
                GridViewRow row = GridView1.Rows[e.RowIndex];
                int id = Convert.ToInt32(GridView1.DataKeys[e.RowIndex].Values[0]);
                string name = (row.FindControl("txtNameEdit") as TextBox).Text;
                
                string rollNo = (row.FindControl("txtRollEdit") as TextBox).Text;
               
                string stateId = ((DropDownList)GridView1.Rows[e.RowIndex].Cells[4].FindControl("ddlStateEdit")).SelectedValue;
                fn.Query("update TestStudent set Name = '" + name.Trim() + "',  RollNo = '" + rollNo.Trim() + "', StateId = '" + stateId + "'  where Id = '" + id + "' ");
                lblmsg.Text = "Student Updated Successfully !!";
                lblmsg.CssClass = "alert alert-success !!";
                GridView1.EditIndex = -1;
                GetTestStudent();
            }
            catch (Exception ex)
            {
                Response.Write("<script>alert('" + ex.Message + "');</script>");
            }
        }

        protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow && GridView1.EditIndex == e.Row.RowIndex)
            {
                if ((e.Row.RowState & DataControlRowState.Edit) > 0)
                {
                    DropDownList ddlState = (DropDownList)e.Row.FindControl("ddlStateEdit");
                    DataTable dt = fn.Fetch("Select * from State ");
                    ddlState.DataSource = dt;
                    ddlState.DataTextField = "StateName";
                    ddlState.DataValueField = "StateId";
                    ddlState.DataBind();
                    ddlState.Items.Insert(0, "Select State");
                    string selectedState = DataBinder.Eval(e.Row.DataItem, "StateName").ToString();
                    ddlState.Items.FindByText(selectedState).Selected = true;
                }
            }
        }
    }
}