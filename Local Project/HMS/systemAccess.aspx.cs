using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace HMS
{
    public partial class systemAccess : System.Web.UI.Page
    {
        Utilities ui = new Utilities();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                //if (Session["sysAccess"] != null)
                //{
                //    DataTable dtSysAccess = (DataTable)Session["sysAccess"];
                //    if (dtSysAccess.Rows[0]["sysAccess"].ToString() == "0")
                //    {
                //        Session["page"] = "System Access";
                //        Response.Redirect("404.aspx");
                //    }
                //}

                fillEmployee();
                fillddl();
                GetAccessRights();
                //fillNavLabel();
            }
        }
        private void GetAccessRights()
        {
            DataTable dt = new DataTable();
            dt = ui.FetchinControldtPara(@"SELECT r.userIdx,u.idx,u.pageUrl FROM Roles r inner join Url u On u.idx = r.pageUrl Where r.visible = 1 and r.userIdx =  @param AND u.idx='5'", Session["appUserId"].ToString());
            if (dt.Rows.Count > 0)
            {
                if (dt.Rows[0]["pageUrl"].ToString() == "systemAccess.aspx")
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
        protected void fillEmployee()
        {
            try
            {
                DataTable dt = new DataTable();
                dt = ui.FetchinControldt(@"select u.idx, (u.firstName + ' ' + u.lastName) as employeeName from users u where u.visible = 1 ");
                if (dt.Rows.Count > 0)
                {
                    ddlEmployees.DataSource = dt;
                    ddlEmployees.DataValueField = "idx";
                    ddlEmployees.DataTextField = "employeeName";
                    ddlEmployees.DataBind();
                    ddlEmployees.Items.Insert(0, "Select Employee");
                }
            }
            catch (Exception ex)
            { }
        }
        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            try
            {
                //Check existing access record.
                DataTable dtCheck = new DataTable();
                dtCheck = ui.FetchinControldtPara("select idx from roles where userIdx=@param", ddlEmployees.SelectedValue.ToString());
                if (dtCheck.Rows.Count > 0)
                {
                    //Delete existing record if avaleble.
                    bool x;
                    x = ui.ExecuteNonQueryWithParam("delete from roles where userIdx = @Para", ddlEmployees.SelectedValue.ToString());
                }
                int i = 0;
                DataTable dt = new DataTable();
                dt = ui.FetchinControldt("select idx from Url where visible = 1 order By pageName asc");
                bool y;


                foreach (RepeaterItem item in rptUserRole.Items)
                {
                    CheckBox chk = item.FindControl("chkPageUrl1") as CheckBox;
                    //CheckBox chk = li.FindControl("chkPageUrl1") as CheckBox;
                    Label lblIdx = item.FindControl("lblIdx") as Label;
                    for (int j = 0; j < dt.Rows.Count; j++)
                    {
                        if (i == j)
                        {
                            if (chk.Checked)
                            {
                                y = ui.ExecuteNonQuery(@"INSERT INTO [dbo].[Roles]
                                                   ([userIdx]
                                                   ,[pageUrl]
                                                   ,[module]
                                                   ,[createdByUserIdx]
		                                           )
                                             VALUES
                                                   (
                                                        '" + ddlEmployees.SelectedValue.ToString() + @"',
                                                        '" + lblIdx.Text + @"',
                                                         '" + lblIdx.Text + @"',
                                                        '" + Session["appUserId"].ToString() + @"'
		                                           )");
                            }
                        }
                    }
                    i = i + 1;
                }
                fillddl();
                clearrAll();
                ScriptManager.RegisterStartupScript(this, this.GetType(), "", "<script>showSuccessMessage()</script>", false);
            }
            catch (Exception ex)
            {
            }
        }
        protected void btnCancel_Click(object sender, EventArgs e)
        {
            clearrAll();
        }
        protected void clearrAll()
        {
            ddlEmployees.SelectedIndex = 0;
            chkAll.Checked = false;
        }
        protected void fillddl()
        {
            DataTable dt = new DataTable();
            dt = ui.FetchinControldt("select idx,pageName,navigationName, pageUrl from url where visible = 1 order By navigationName asc");
            if (dt.Rows.Count > 0)
            {

                rptUserRole.DataSource = dt;
                rptUserRole.DataBind();

            }
            else
            {
                rptUserRole.DataSource = "";
                rptUserRole.DataBind();

            }
        }
        protected void ddlEmployees_SelectedIndexChanged1(object sender, EventArgs e)
        {
            DataTable dt = new DataTable();
            DataTable dtCheck = new DataTable();

            chkAll.Checked = false;
         
            fillddl();
            if (ddlEmployees.SelectedValue != "0")
            {

                dtCheck = ui.FetchinControldtPara("select r.userIdx,u.pageUrl as PageName from Roles r  inner join url u on u.idx=r.pageUrl where userIdx=@param", ddlEmployees.SelectedValue.ToString());
                if (dtCheck.Rows.Count > 0)
                {
                    int i = 0;
                    foreach (RepeaterItem li in rptUserRole.Items)
                    {
                        CheckBox chk = li.FindControl("chkPageUrl1") as CheckBox;
                        Label lburl = li.FindControl("lbpgurl") as Label;
                        for (int j = 0; j < dtCheck.Rows.Count; j++)
                        {
                            if (lburl.Text == dtCheck.Rows[j]["PageName"].ToString())
                            {
                                chk.Checked = true;
                            }
                        }
                        i = i + 1;
                    }
                }
            }
            else
            {
                rptUserRole.DataSource = "";
                rptUserRole.DataBind();
            }
        }
    }
}