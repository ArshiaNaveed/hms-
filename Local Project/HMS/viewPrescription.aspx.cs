using System;
using System.Data;

namespace HMS
{
    public partial class viewPrescription : System.Web.UI.Page
    {
        Utilities ui = new Utilities();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["sysAccess"] != null)
                {
                    DataTable dtSysAccess = (DataTable)Session["sysAccess"];
                    if (dtSysAccess.Rows[0]["pharmacyPresc"].ToString() == "0")
                    {
                        Session["page"] = "Pharmacy";
                        Response.Redirect("404.aspx");
                    }
                }
                GetAccessRights();
            }
        }
        private void GetAccessRights()
        {
            DataTable dt = new DataTable();
            dt = ui.FetchinControldtPara(@"SELECT r.userIdx,u.idx,u.pageUrl FROM Roles r inner join Url u On u.idx = r.pageUrl Where r.visible = 1 and r.userIdx =  @param AND u.idx='14'", Session["appUserId"].ToString());
            if (dt.Rows.Count > 0)
            {
                if (dt.Rows[0]["pageUrl"].ToString() == "viewPrescription.aspx")
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
        protected void btnSearch_Click(object sender, EventArgs e)
        {
            if (txtCardNumber.Text != "")
            {
                fillPatientDetails();
                fillPrescriptionLog();
            }
        }

        protected void fillPatientDetails()
        {
            try
            {
                DataTable dt = new DataTable();
                dt = ui.FetchinControldt(@"select top 1 p.idx as patientIdx, t.idx as tokenIdx, 
                                p.cardNumber, p.patientName, p.age, p.contactNumber1, p.contactNumber2,
                                t.tokenNumber, (u.firstName + ' ' + u.lastName) as doctorName, t.appointmentDate, t.fee
                                from token t
                                inner join patentRegistration p on p.idx = t.patientIdx
                                inner join users u on u.idx = t.physicianIdx
                                where  Convert(date, t.appointmentDate, 103) = Convert(date, getdate(), 103) and t.visible = 1 and p.cardNumber = " + ui.GetSQLInject(txtCardNumber.Text) + @"
                                order by t.tokenNumber asc");
                if (dt.Rows.Count > 0)
                {
                    pnlMain.Visible = true;
                    lblError.Visible = false;
                    lblDate.Text = dt.Rows[0]["appointmentDate"].ToString();
                    lblCardNumber.Text = dt.Rows[0]["cardNumber"].ToString();
                    lblPatientName.Text = dt.Rows[0]["patientName"].ToString();
                    lblAge.Text = dt.Rows[0]["age"].ToString();
                    lblTokenNumber.Text = dt.Rows[0]["tokenNumber"].ToString();
                    lblDoctor.Text = dt.Rows[0]["doctorName"].ToString();
                    lblAppointmentDate.Text = dt.Rows[0]["appointmentDate"].ToString();
                    Session["patientIdx"] = dt.Rows[0]["patientIdx"].ToString();
                    Session["tokenIdx"] = dt.Rows[0]["tokenIdx"].ToString();
                }
                else
                {
                    lblError.Visible = true;
                    pnlMain.Visible = false;
                }
            }
            catch (Exception ex)
            { }
        }

        protected void fillPrescriptionLog()
        {
            try
            {
                DataTable dt = new DataTable();
                dt = ui.FetchinControldt(@"select row_number() over (order by pl.idx) as sn, pl.* from prescriptionLog pl
                inner join treatment tm on tm.idx = pl.treatmentIdx
                inner join token t on t.idx = tm.tokenIdx
                inner join patentRegistration pr on pr.idx = t.patientIdx
                where  t.visible = 1 and pr.cardNumber = " + ui.GetSQLInject(txtCardNumber.Text) + @"
                and pl.treatmentIdx = (
                select top 1 t.idx from treatment t
                inner join patentRegistration pr on pr.idx = t.patientIdx
                where  t.visible = 1 and pr.cardNumber = " + ui.GetSQLInject(txtCardNumber.Text) + @"
                order by t.idx desc)");
                if (dt.Rows.Count > 0)
                {
                    rptPrescription.DataSource = dt;
                    rptPrescription.DataBind();
                }
                else
                {
                    rptPrescription.DataSource = null;
                    rptPrescription.DataBind();
                }

            }
            catch (Exception ex)
            { }
        }
    }
}