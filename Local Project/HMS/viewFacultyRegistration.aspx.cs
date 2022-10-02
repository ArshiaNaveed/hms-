using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace HMS
{
    public partial class viewFacultyRegistration : System.Web.UI.Page
    {
        Utilities ui = new Utilities();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["sysAccess"] != null)
                {
                    DataTable dtSysAccess = (DataTable)Session["sysAccess"];
                    if (dtSysAccess.Rows[0]["empList"].ToString() == "0")
                    {
                        Session["page"] = "Employee / Faculty List";
                        Response.Redirect("404.aspx");
                    }
                }

                bindUsers();
                GetAccessRights();
            }
        }
        private void GetAccessRights()
        {
            DataTable dt = new DataTable();
            dt = ui.FetchinControldtPara(@"SELECT r.userIdx,u.idx,u.pageUrl FROM Roles r inner join Url u On u.idx = r.pageUrl Where r.visible = 1 and r.userIdx =  @param AND u.idx='3'", Session["appUserId"].ToString());
            if (dt.Rows.Count > 0)
            {
                if (dt.Rows[0]["pageUrl"].ToString() == "viewFacultyRegistration.aspx")
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

        protected void bindUsers()
        {
            try
            {
                DataTable dt = new DataTable();
                dt = ui.FetchinControldt(@"select row_number() over (order by u.idx) as sn,u.idx, (u.firstName + ' ' + u.lastName) as fullName, dt.departmentName, dn.designationName, ut.userTypeName, sy.specialty, 
                                        case 
                                        when u.isactive = 0 then 'De-Active'
                                        when u.isactive = 1 then 'Active'
                                        end status
                                        from users u
                                        inner join department dt on dt.idx = u.departmentIdx
                                        inner join designation dn on dn.idx = u.designationIdx
                                        inner join userType ut on ut.idx = u.userType
                                        inner join specialty sy on sy.idx = u.specialityIdx
                                        where u.visible = 1 and u.idx <> 1 order by u.idx desc");

                if (dt.Rows.Count > 0)
                {
                    rptUsers.DataSource = dt;
                    rptUsers.DataBind();
                }
                else
                {
                    rptUsers.DataSource = "";
                    rptUsers.DataBind();
                }

            }
            catch (Exception ex)
            { }
        }

        protected void lnkView_Click(object sender, EventArgs e)
        {
            LinkButton lnk = (LinkButton)sender;
            string idx = lnk.CommandArgument.ToString();

            Session["userIdx"] = idx;
            Session["userAction"] = "view";
            Response.Redirect("facultyRegistration.aspx");
        }

        protected void lnkDelete_Click(object sender, EventArgs e)
        {
            try
            {
                LinkButton lnk = (LinkButton)sender;
                string idx = lnk.CommandArgument.ToString();

                bool x;
                x = ui.ExecuteNonQuery(@"update users set visible = 0 where idx = " + idx);
                if (x == true)
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "", "<script>deleteMessage()</script>", false);
                    bindUsers();
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "", "<script>showErrorMessage()</script>", false);
                }
            }
            catch (Exception ex)
            { }
        }

        protected void btnPrint_Click(object sender, EventArgs e)
        {
            OpenNewBrowserWindow("facultyReport.aspx", this);
        }
        public static void OpenNewBrowserWindow(string Url, Control control)
        {
            ScriptManager.RegisterStartupScript(control, control.GetType(), "Open", "window.open('" + Url + "','','top=100px,bottom=100px,right=100px,left=100px, directories=no,menubar=no,toolbar=no,location=no,resizable=no,height=1000px,width=1500,status=no,scrollbars=yes,maximize=null,resizable=0,titlebar=no');", true);
        }
    }
}