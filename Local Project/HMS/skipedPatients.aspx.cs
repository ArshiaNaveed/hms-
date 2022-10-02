using System;
using System.Data;
using System.Web.UI.WebControls;

namespace HMS
{
    public partial class skipedPatients : System.Web.UI.Page
    {
        Utilities ui = new Utilities();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["sysAccess"] != null)
                {
                    DataTable dtSysAccess = (DataTable)Session["sysAccess"];
                    if (dtSysAccess.Rows[0]["skipedPatients"].ToString() == "0")
                    {
                        Session["page"] = "Skipped Patients";
                        Response.Redirect("404.aspx");
                    }
                }

                fillSkippedPatients();
                GetAccessRights();
            }
        }
        private void GetAccessRights()
        {
            DataTable dt = new DataTable();
            dt = ui.FetchinControldtPara(@"SELECT r.userIdx,u.idx,u.pageUrl FROM Roles r inner join Url u On u.idx = r.pageUrl Where r.visible = 1 and r.userIdx =  @param AND u.idx='11'", Session["appUserId"].ToString());
            if (dt.Rows.Count > 0)
            {
                if (dt.Rows[0]["pageUrl"].ToString() == "skipedPatients.aspx")
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
        protected void fillSkippedPatients()
        {
            try
            {
                //2 status = skipped treatment
                DataTable dt = new DataTable();
                dt = ui.FetchinControldt(@"select row_number() over (order by t.idx) as sn, p.idx as patientIdx, t.idx as tokenIdx, 
                                p.cardNumber, p.patientName, p.age, p.contactNumber1, p.contactNumber2,
                                t.tokenNumber, (u.firstName + ' ' + u.lastName) as doctorName, t.appointmentDate, t.fee
                                from token t
                                inner join patentRegistration p on p.idx = t.patientIdx
                                inner join users u on u.idx = t.physicianIdx
                                where  Convert(date, t.appointmentDate, 103) = Convert(date, getdate(), 103) and physicianIdx = " + Session["appUserId"].ToString() + @" and t.visible = 1 and t.status = 2
                                order by t.tokenNumber asc");
                if (dt.Rows.Count > 0)
                {
                    rptSkippedPatients.DataSource = dt;
                    rptSkippedPatients.DataBind();

                    Session["patientIdx"] = dt.Rows[0]["patientIdx"].ToString();
                    Session["tokenIdx"] = dt.Rows[0]["tokenIdx"].ToString();
                }
            }
            catch (Exception ex)
            { }
        }

        protected void lnkTreatment_Click(object sender, EventArgs e)
        {
            LinkButton lnk = (LinkButton)sender;
            string idx = lnk.CommandArgument.ToString();

            Session["skippedTokenIdx"] = idx;
            Session["skippedToken"] = "view";
            Response.Redirect("treatment.aspx");
        }

    }
}