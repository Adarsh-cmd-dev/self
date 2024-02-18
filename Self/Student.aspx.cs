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
    public partial class Student : System.Web.UI.Page
    {
        Commonfnx fn = new Commonfnx();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                GetStudent();
                GetClass();
            }
        }

        private void GetClass()
        {
            DataTable dt = fn.Fetch("select * from Class");
            ddlClass.DataSource = dt;
            ddlClass.DataTextField = "ClassName";
            ddlClass.DataValueField = "ClassId";
            ddlClass.DataBind();
            ddlClass.Items.Insert(0, "Select Class");
        }

        private void GetStudent()
        {
            DataTable dt = fn.Fetch(@"select Row_NUMBER() over(order by (select 1)) as [Sr. No], 
                                    s.StudentId, s.[Name], s.DOB, s.Gender, s.Mobile, s.[Address], s.[RollNo], c.ClassId, c.ClassName from Student s
                                    inner join Class c on c.ClassId = s.ClassId");
            GridView1.DataSource = dt;
            GridView1.DataBind();
        }

        protected void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                if (ddlGender.SelectedValue != "0")
                {
                    string rollNo = txtRoll.Text.Trim();
                    DataTable dt = fn.Fetch("select * from Student where ClassId = '" + ddlClass.SelectedValue + "' and RollNo = '" + rollNo + "' ");
                    if (dt.Rows.Count == 0)
                    {
                        string query = "Insert into Student values('" + txtName.Text.Trim() + "', '" + txtDOB.Text.Trim() + "', '" + ddlGender.SelectedValue + "', '" + txtMobile.Text.Trim() + "', '" + txtRoll.Text.Trim() + "', '" + txtAddress.Text.Trim() + "', '" + ddlClass.SelectedValue + "')";
                        fn.Query(query);
                        lblMsg.Text = "Inserted Successfully !!";
                        lblMsg.CssClass = "alert alert-success !!";
                        ddlGender.SelectedIndex = 0;
                        txtName.Text = string.Empty;
                        txtDOB.Text = string.Empty;
                        txtMobile.Text = string.Empty;
                        txtRoll.Text = string.Empty;
                        txtAddress.Text = string.Empty;
                        txtAddress.Text = string.Empty;
                        ddlClass.SelectedIndex = 0;
                        GetStudent();
                    }
                    else
                    {
                        lblMsg.Text = "Entered Roll No.<b>'" + rollNo + "'</b> be already exists for selected class !!";
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
            GetStudent();
        }

        protected void GridView1_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            GridView1.EditIndex = -1;
            GetStudent();
        }

        protected void GridView1_RowEditing(object sender, GridViewEditEventArgs e)
        {
            GridView1.EditIndex = e.NewEditIndex;
            GetStudent();
        }

        protected void GridView1_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            try
            {
                GridViewRow row = GridView1.Rows[e.RowIndex];
                int studentId = Convert.ToInt32(GridView1.DataKeys[e.RowIndex].Values[0]);
                string name = (row.FindControl("txtNameEdit") as TextBox).Text;
                string mobile = (row.FindControl("txtMobileEdit") as TextBox).Text;
                string rollNo = (row.FindControl("txtRollNoEdit") as TextBox).Text;
                string address = (row.FindControl("txtAddressEdit") as TextBox).Text;
                string classId = ((DropDownList)GridView1.Rows[e.RowIndex].Cells[4].FindControl("ddlClassEdit")).SelectedValue;
                fn.Query("update Student set Name = '" + name.Trim() + "', Mobile = '" + mobile.Trim() + "', RollNo = '" + rollNo.Trim() + "', ClassId = '" + classId + "' ,Address = '" + address.Trim() + "' where StudentId = '" + studentId + "' ");
                lblMsg.Text = "Student Updated Successfully !!";
                lblMsg.CssClass = "alert alert-success !!";
                GridView1.EditIndex = -1;
                GetStudent();
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
                    DropDownList ddlClass = (DropDownList)e.Row.FindControl("ddlClassEdit");
                    DataTable dt = fn.Fetch("Select * from Class ");
                    ddlClass.DataSource = dt;
                    ddlClass.DataTextField = "ClassName";
                    ddlClass.DataValueField = "ClassId";
                    ddlClass.DataBind();
                    ddlClass.Items.Insert(0, "Select Class");
                    string selectedClass = DataBinder.Eval(e.Row.DataItem, "ClassName").ToString();
                    ddlClass.Items.FindByText(selectedClass).Selected = true;
                }
            }
        }

        protected void GridView1_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            try
            {
                int studentId = Convert.ToInt32(GridView1.DataKeys[e.RowIndex].Values[0]);
                fn.Query("Delete from Student where StudentId ='" + studentId + "' ");
                lblMsg.Text = "Student Deleted Successfully !!";
                lblMsg.CssClass = "alert alert-success !!";
                GridView1.EditIndex = -1;
                GetStudent();
            }
            catch (Exception ex)
            {
                Response.Write("<script>alert('" + ex.Message + "');</script>");
            }
        }
    }
}