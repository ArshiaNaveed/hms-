using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace HMS
{
    public partial class speciality : System.Web.UI.Page
    {
        Utilities ui = new Utilities();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["sysAccess"] != null)
                {
                    DataTable dtSysAccess = (DataTable)Session["sysAccess"];
                    if (dtSysAccess.Rows[0]["manageSpeciality"].ToString() == "0")
                    {
                        Session["page"] = "Settings / Manage Speciality";
                        Response.Redirect("404.aspx");
                    }
                }

                bindspeciality();
                GetAccessRights();
            }
        }
        private void GetAccessRights()
        {
            DataTable dt = new DataTable();
            dt = ui.FetchinControldtPara(@"SELECT r.userIdx,u.idx,u.pageUrl FROM Roles r inner join Url u On u.idx = r.pageUrl Where r.visible = 1 and r.userIdx =  @param AND u.idx='21'", Session["appUserId"].ToString());
            if (dt.Rows.Count > 0)
            {
                if (dt.Rows[0]["pageUrl"].ToString() == "speciality.aspx")
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
        protected void bindspeciality()
        {
            DataTable dt = new DataTable();
            dt = ui.FetchinControldt(@"select row_number() over (order by s.idx) as sn, ut.userTypeName, s.* 
                                    from specialty s
                                    inner join userType ut on ut.idx = s.userTypeIdx
                                    where s.visible = 1");
            if (dt.Rows.Count > 0)
            {
                rptSpeciality.DataSource = dt;
                rptSpeciality.DataBind();
            }
            else
            {
                rptSpeciality.DataSource = "";
                rptSpeciality.DataBind();
            }
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            bool x;
            x = ui.ExecuteNonQuery(@"INSERT INTO [dbo].[specialty]
           ([userTypeIdx]
           ,[specialty]
           ,[createdByUserIdx]
			)
     VALUES
           (
           '" + ui.GetSQLInject(ddlUserType.SelectedValue.ToString()) + @"',
		   '" + ui.GetSQLInject(txtSpeciality.Text) + @"',
            '" + Session["appUserId"].ToString() + @"'
		   )");
            if (x == true)
            {
                clearAll();
                bindspeciality();
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
            x = ui.ExecuteNonQuery(@"update [specialty] set [userTypeIdx] = '" + ui.GetSQLInject(ddlUserType.SelectedValue.ToString()) + @"', [specialty] = '" + ui.GetSQLInject(txtSpeciality.Text) + @"' where idx = " + Session["specialityIdx"].ToString());
            if (x == true)
            {
                clearAll();
                btnSubmit.Enabled = true;
                btnUpdate.Enabled = false;
                bindspeciality();
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
            ddlUserType.ClearSelection();
            txtSpeciality.Text = "";
        }

        protected void lnkView_Click(object sender, EventArgs e)
        {
            LinkButton lnk = (LinkButton)sender;
            string idx = lnk.CommandArgument.ToString();

            Session["specialityIdx"] = idx;
            DataTable dt = new DataTable();
            dt = ui.FetchinControldt(@"select * from [specialty] where visible = 1 and idx = " + idx);
            if (dt.Rows.Count > 0)
            {
                btnSubmit.Enabled = false;
                btnUpdate.Enabled = true;
                ddlUserType.SelectedValue = dt.Rows[0]["userTypeIdx"].ToString();
                txtSpeciality.Text = dt.Rows[0]["specialty"].ToString();
            }

        }

        protected void lnkDelete_Click(object sender, EventArgs e)
        {
            try
            {
                LinkButton lnk = (LinkButton)sender;
                string idx = lnk.CommandArgument.ToString();

                bool x;
                x = ui.ExecuteNonQuery(@"update specialty set visible = 0 where idx = " + idx);
                if (x == true)
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "", "<script>deleteMessage()</script>", false);
                    bindspeciality();
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