using System;
using System.Data;
using System.Web.UI.WebControls;

namespace HMS
{
    public partial class treatmentHistory : System.Web.UI.Page
    {
        Utilities ui = new Utilities();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["sysAccess"] != null)
                {
                    DataTable dtSysAccess = (DataTable)Session["sysAccess"];
                    if (dtSysAccess.Rows[0]["treatmentHistory"].ToString() == "0")
                    {
                        Session["page"] = "Medical Treatment History";
                        Response.Redirect("404.aspx");
                    }
                }
                GetAccessRights();
            }
        }
        private void GetAccessRights()
        {
            DataTable dt = new DataTable();
            dt = ui.FetchinControldtPara(@"SELECT r.userIdx,u.idx,u.pageUrl FROM Roles r inner join Url u On u.idx = r.pageUrl Where r.visible = 1 and r.userIdx =  @param AND u.idx='12'", Session["appUserId"].ToString());
            if (dt.Rows.Count > 0)
            {
                if (dt.Rows[0]["pageUrl"].ToString() == "treatmentHistory.aspx")
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
            fillPatientDetails();
        }

        protected void fillPatientDetails()
        {
            try
            {
                DataTable dt = new DataTable();
                dt = ui.FetchinControldt(@"select top 1 t.idx as tokenIdx, p.idx as patientIdx, 
                    p.cardNumber, p.patientName, p.age, p.contactNumber1, p.contactNumber2
                    from token t
                    inner join patentRegistration p on p.idx = t.patientIdx
                    inner join users u on u.idx = t.physicianIdx
                    where p.cardNumber = " + txtCardNumber.Text);
                if (dt.Rows.Count > 0)
                {
                    divPatientDetails.Visible = true;
                    lblCardNumber.Text = dt.Rows[0]["cardNumber"].ToString();
                    lblPatientName.Text = dt.Rows[0]["patientName"].ToString();
                    lblAge.Text = dt.Rows[0]["age"].ToString();
                    lblContactNumber1.Text = dt.Rows[0]["contactNumber1"].ToString();
                    lblContactNumber2.Text = dt.Rows[0]["contactNumber2"].ToString();
                    Session["patientIdx"] = dt.Rows[0]["patientIdx"].ToString();
                    Session["tokenIdx"] = dt.Rows[0]["tokenIdx"].ToString();
                    bindHistory();
                }
                else
                {
                    divPatientDetails.Visible = false;
                }
            }
            catch (Exception ex)
            { }
        }

        protected void bindHistory()
        {
            DataTable dt = new DataTable();
            dt = ui.FetchinControldt(@"select t.idx as tokenIdx, 
                    t.tokenNumber, (u.firstName + ' ' + u.lastName) as doctorName, t.appointmentDate, t.fee, 
                    tm.idx as treatmentIdx, tm.fever, tm.bp, tm.sugar, tm.otherDaisies, tm.diagnostics
                    from token t
                    inner join users u on u.idx = t.physicianIdx
                    inner join treatment tm on tm.tokenIdx = t.idx
                    where t.idx = " + Session["tokenIdx"].ToString());
            if (dt.Rows.Count > 0)
            {
                rptHistory.DataSource = dt;
                rptHistory.DataBind();
            }
        }

        protected void rptHistory_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            #region Medical Log
            Repeater rptMedicalLog = e.Item.FindControl("rptMedicalLog") as Repeater;
            Repeater rptPrescriptionLog = e.Item.FindControl("rptPrescriptionLog") as Repeater;
            Label lblTreatmentIdx = e.Item.FindControl("lblTreatmentIdx") as Label;
            
            DataTable dtMedicalLog = new DataTable();
            dtMedicalLog = ui.FetchinControldt(@"select row_number() over (order by ml.idx) as sn, ml.* from medicineLog ml
                inner join treatment tm on tm.idx = ml.treatmentIdx
                inner join token t on t.idx = tm.tokenIdx
                inner join patentRegistration pr on pr.idx = t.patientIdx
                where t.visible = 1 and pr.cardNumber = " + ui.GetSQLInject(txtCardNumber.Text) + @"
                order by t.tokenNumber asc");
            if (dtMedicalLog.Rows.Count > 0)
            {
                rptMedicalLog.DataSource = dtMedicalLog;
                rptMedicalLog.DataBind();
            }
            else
            {
                rptMedicalLog.DataSource = null;
                rptMedicalLog.DataBind();
            }

            #endregion

            #region Prescription Log

            DataTable dtPrescription = new DataTable();
            dtPrescription = ui.FetchinControldt(@"select row_number() over (order by pl.idx) as sn, pl.* from prescriptionLog pl
                inner join treatment tm on tm.idx = pl.treatmentIdx
                inner join token t on t.idx = tm.tokenIdx
                inner join patentRegistration pr on pr.idx = t.patientIdx
                where  t.visible = 1 and pr.cardNumber = " + ui.GetSQLInject(txtCardNumber.Text) + @"
                and pl.treatmentIdx = " + ui.GetSQLInject(lblTreatmentIdx.Text));
            if (dtPrescription.Rows.Count > 0)
            {
                rptPrescriptionLog.DataSource = dtPrescription;
                rptPrescriptionLog.DataBind();
            }
            else
            {
                rptPrescriptionLog.DataSource = null;
                rptPrescriptionLog.DataBind();
            }

            #endregion

        }
    }
}