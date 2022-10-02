using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace HMS
{
    public partial class viewPatients : System.Web.UI.Page
    {
        Utilities ui = new Utilities();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["sysAccess"] != null)
                {
                    DataTable dtSysAccess = (DataTable)Session["sysAccess"];
                    if (dtSysAccess.Rows[0]["patientList"].ToString() == "0")
                    {
                        Session["page"] = "Patients List";
                        Response.Redirect("404.aspx");
                    }
                }

                bindPatients();
                GetAccessRights();
            }
        }
        private void GetAccessRights()
        {
            DataTable dt = new DataTable();
            dt = ui.FetchinControldtPara(@"SELECT r.userIdx,u.idx,u.pageUrl FROM Roles r inner join Url u On u.idx = r.pageUrl Where r.visible = 1 and r.userIdx =  @param AND u.idx='9'", Session["appUserId"].ToString());
            if (dt.Rows.Count > 0)
            {
                if (dt.Rows[0]["pageUrl"].ToString() == "viewPatients.aspx")
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
        protected void bindPatients()
        {
            try
            {
                DataTable dt = new DataTable();
                dt = ui.FetchinControldt(@"select row_number() over (order by p.idx) as sn, p.*, g.genderName from patentRegistration p 
                                        inner join gender g on g.idx = p.gender
                                        where p.visible = 1");

                if (dt.Rows.Count > 0)
                {
                    rptPatient.DataSource = dt;
                    rptPatient.DataBind();
                }
                else
                {
                    rptPatient.DataSource = "";
                    rptPatient.DataBind();
                }

            }
            catch (Exception ex)
            { }
        }

        protected void lnkView_Click(object sender, EventArgs e)
        {
            LinkButton lnk = (LinkButton)sender;
            string idx = lnk.CommandArgument.ToString();

            Session["patientIdx"] = idx;
            Session["patientAction"] = "view";
            Response.Redirect("patientRegistration.aspx");
        }

        protected void lnkDelete_Click(object sender, EventArgs e)
        {
            try
            {
                LinkButton lnk = (LinkButton)sender;
                string idx = lnk.CommandArgument.ToString();

                bool x;
                x = ui.ExecuteNonQuery(@"update patentRegistration set visible = 0 where idx = " + idx);
                if (x == true)
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "", "<script>deleteMessage()</script>", false);
                    bindPatients();
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
            OpenNewBrowserWindow("patientsReport.aspx", this);
        }
        public static void OpenNewBrowserWindow(string Url, Control control)
        {
            ScriptManager.RegisterStartupScript(control, control.GetType(), "Open", "window.open('" + Url + "','','top=100px,bottom=100px,right=100px,left=100px, directories=no,menubar=no,toolbar=no,location=no,resizable=no,height=1000px,width=1500,status=no,scrollbars=yes,maximize=null,resizable=0,titlebar=no');", true);
        }
    }
}