using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace HMS
{
    public partial class designation : System.Web.UI.Page
    {
        Utilities ui = new Utilities();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["sysAccess"] != null)
                {
                    DataTable dtSysAccess = (DataTable)Session["sysAccess"];
                    if (dtSysAccess.Rows[0]["manageDesignation"].ToString() == "0")
                    {
                        Session["page"] = "Settings / Manage Designation";
                        Response.Redirect("404.aspx");
                    }
                }

                fillDepartment();
                bindDesignation();
                GetAccessRights();
            }
        }
        private void GetAccessRights()
        {
            DataTable dt = new DataTable();
            dt = ui.FetchinControldtPara(@"SELECT r.userIdx,u.idx,u.pageUrl FROM Roles r inner join Url u On u.idx = r.pageUrl Where r.visible = 1 and r.userIdx =  @param AND u.idx='20'", Session["appUserId"].ToString());
            if (dt.Rows.Count > 0)
            {
                if (dt.Rows[0]["pageUrl"].ToString() == "designation.aspx")
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
        protected void fillDepartment()
        {
            DataTable dt = new DataTable();
            dt = ui.FetchinControldt(@"select * from department where visible = 1");
            if (dt.Rows.Count > 0)
            {
                ddlDepartment.DataSource = dt;
                ddlDepartment.DataBind();
            }
            else
            {
                ddlDepartment.DataSource = "";
                ddlDepartment.DataBind();
            }
        }

        protected void bindDesignation()
        {
            DataTable dt = new DataTable();
            dt = ui.FetchinControldt(@"select row_number() over (order by ds.idx) as sn, ds.idx, d.departmentName, ds.designationName from designation ds
                                        inner join department d on d.idx = ds.departmentIdx
                                        where ds.visible = 1 ");
            if (dt.Rows.Count > 0)
            {
                rptDesignation.DataSource = dt;
                rptDesignation.DataBind();
            }
            else
            {
                rptDesignation.DataSource = "";
                rptDesignation.DataBind();
            }
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            bool x;
            x = ui.ExecuteNonQuery(@"INSERT INTO [dbo].[designation]
           ([departmentIdx]
           ,[designationName]
           ,[createdByUserIdx]
			)
     VALUES
           (
		   '" + ui.GetSQLInject(ddlDepartment.SelectedValue.ToString()) + @"',
		   '" + ui.GetSQLInject(txtDesignation.Text) + @"',
            '" + Session["appUserId"].ToString() + @"'
		   )");
            if (x == true)
            {
                clearAll();
                bindDesignation();
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
            x = ui.ExecuteNonQuery(@"update [designation] set [departmentIdx] = '" + ui.GetSQLInject(ddlDepartment.SelectedValue.ToString()) + @"', [designationName] = '" + ui.GetSQLInject(txtDesignation.Text) + @"' where idx = " + Session["designationIdx"].ToString());
            if (x == true)
            {
                clearAll();
                btnSubmit.Enabled = true;
                btnUpdate.Enabled = false;
                bindDesignation();
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
            ddlDepartment.ClearSelection();
            txtDesignation.Text = "";
        }

        protected void lnkView_Click(object sender, EventArgs e)
        {
            LinkButton lnk = (LinkButton)sender;
            string idx = lnk.CommandArgument.ToString();

            Session["designationIdx"] = idx;
            DataTable dt = new DataTable();
            dt = ui.FetchinControldt(@"select * from designation where visible = 1 and idx = " + idx);
            if (dt.Rows.Count > 0)
            {
                btnSubmit.Enabled = false;
                btnUpdate.Enabled = true;
                ddlDepartment.SelectedValue = dt.Rows[0]["departmentIdx"].ToString();
                txtDesignation.Text = dt.Rows[0]["designationName"].ToString();
            }

        }
        protected void lnkDelete_Click(object sender, EventArgs e)
        {
            try
            {
                LinkButton lnk = (LinkButton)sender;
                string idx = lnk.CommandArgument.ToString();

                bool x;
                x = ui.ExecuteNonQuery(@"update designation set visible = 0 where idx = " + idx);
                if (x == true)
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "", "<script>deleteMessage()</script>", false);
                    bindDesignation();
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