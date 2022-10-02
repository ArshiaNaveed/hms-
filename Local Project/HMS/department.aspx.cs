using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace HMS
{
    public partial class department : System.Web.UI.Page
    {
        Utilities ui = new Utilities();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["sysAccess"] != null)
                {
                    DataTable dtSysAccess = (DataTable)Session["sysAccess"];
                    if (dtSysAccess.Rows[0]["manageDepartment"].ToString() == "0")
                    {
                        Session["page"] = "Settings / Manage Department";
                        Response.Redirect("404.aspx");
                    }
                }

                bindDepartment();
                GetAccessRights();
            }
        }
        private void GetAccessRights()
        {
            DataTable dt = new DataTable();
            dt = ui.FetchinControldtPara(@"SELECT r.userIdx,u.idx,u.pageUrl FROM Roles r inner join Url u On u.idx = r.pageUrl Where r.visible = 1 and r.userIdx =  @param AND u.idx='19'", Session["appUserId"].ToString());
            if (dt.Rows.Count > 0)
            {
                if (dt.Rows[0]["pageUrl"].ToString() == "department.aspx")
                {

                }
                else
                {
                    Response.Redirect("404.aspx");
                }
            }
            else
            {
                Response.Redirect("404.aspx");
            }

        }
        protected void bindDepartment()
        {
            DataTable dt = new DataTable();
            dt = ui.FetchinControldt(@"select row_number() over (order by idx) as sn, * from department where visible = 1");
            if (dt.Rows.Count > 0)
            {
                rptDepartment.DataSource = dt;
                rptDepartment.DataBind();
            }
            else
            {
                rptDepartment.DataSource = "";
                rptDepartment.DataBind();
            }
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            bool x;
            x = ui.ExecuteNonQuery(@"INSERT INTO [dbo].[department]
           ([departmentName]
           ,[createdByUserIdx]
			)
     VALUES
           (
		   '" + ui.GetSQLInject(txtDepartment.Text) + @"',
            '" + Session["appUserId"].ToString() + @"'
		   )");
            if (x == true)
            {
                clearAll();
                bindDepartment();
                ScriptManager.RegisterStartupScript(this, this.GetType(), "", "<script>showSuccessMessage()</script>", false);
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "", "<script>showErrorMessage()</script>", false);
            }
        }

        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            bool x;
            x = ui.ExecuteNonQuery(@"update [department] set [departmentName] = '" + ui.GetSQLInject(txtDepartment.Text) + @"' where idx = " + Session["departmentIdx"].ToString());
            if (x == true)
            {
                clearAll();
                btnSubmit.Enabled = true;
                btnUpdate.Enabled = false;
                bindDepartment();
                ScriptManager.RegisterStartupScript(this, this.GetType(), "", "<script>showSuccessMessage()</script>", false);
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "", "<script>showErrorMessage()</script>", false);
            }
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            clearAll();
        }

        protected void clearAll()
        {
            txtDepartment.Text = "";
        }

        protected void lnkView_Click(object sender, EventArgs e)
        {
            LinkButton lnk = (LinkButton)sender;
            string idx = lnk.CommandArgument.ToString();

            Session["departmentIdx"] = idx;
            DataTable dt = new DataTable();
            dt = ui.FetchinControldt(@"select * from department where visible = 1 and idx = " + idx);
            if (dt.Rows.Count > 0)
            {
                btnSubmit.Enabled = false;
                btnUpdate.Enabled = true;
                txtDepartment.Text = dt.Rows[0]["departmentName"].ToString();
            }

        }

        protected void lnkDelete_Click(object sender, EventArgs e)
        {
            try
            {
                LinkButton lnk = (LinkButton)sender;
                string idx = lnk.CommandArgument.ToString();

                bool x;
                x = ui.ExecuteNonQuery(@"update department set visible = 0 where idx = " + idx);
                if (x == true)
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "", "<script>deleteMessage()</script>", false);
                    bindDepartment();
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "", "<script>showErrorMessage()</script>", false);
                }
            }
            catch (Exception ex)
            { }
        }

    }
}