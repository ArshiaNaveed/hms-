using System;
using System.Data;
using System.Web.UI;

namespace HMS
{
    public partial class token : System.Web.UI.Page
    {
        Utilities ui = new Utilities();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["sysAccess"] != null)
                {
                    DataTable dtSysAccess = (DataTable)Session["sysAccess"];
                    if (dtSysAccess.Rows[0]["appointment"].ToString() == "0")
                    {
                        Session["page"] = "Appointment";
                        Response.Redirect("404.aspx");
                    }
                }

                bindPhysicianForRegPatient();

                if (Session["isSubmitted"] != null)
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "", "<script>showSuccessMessage()</script>", false);
                    string url = "tokenPrint.aspx";
                    string s = "window.open('" + url + "', 'popup_window', 'width=800,height=600,resizable=no');";
                    ClientScript.RegisterStartupScript(this.GetType(), "script", s, true);
                    Session["isSubmitted"] = null;
                }
                GetAccessRights();
            }
        }
        private void GetAccessRights()
        {
            DataTable dt = new DataTable();
            dt = ui.FetchinControldtPara(@"SELECT r.userIdx,u.idx,u.pageUrl FROM Roles r inner join Url u On u.idx = r.pageUrl Where r.visible = 1 and r.userIdx =  @param AND u.idx='8'", Session["appUserId"].ToString());
            if (dt.Rows.Count > 0)
            {
                if (dt.Rows[0]["pageUrl"].ToString() == "token.aspx")
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

        #region Registered Patient Token

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            DataTable dt = new DataTable();
            dt = ui.FetchinControldt(@"select * from patentRegistration where cardNumber = '" + ui.GetSQLInject(txtRegCardNumber.Text) + "' and visible = 1");
            if (dt.Rows.Count > 0)
            {
                txtRegPatientName.Text = dt.Rows[0]["patientName"].ToString();
                txtRegAge.Text = dt.Rows[0]["age"].ToString();
                txtRegContactNumber1.Text = dt.Rows[0]["contactNumber1"].ToString();
                txtRegContactNumber2.Text = dt.Rows[0]["contactNumber2"].ToString();
                Session["patientIdx"] = dt.Rows[0]["idx"].ToString();
                //getToken();
            }
            else
            {

                ScriptManager.RegisterStartupScript(this, this.GetType(), "", "<script>notFoundMessage()</script>", false);
            }
        }

        protected void bindPhysicianForRegPatient()
        {
            try
            {
                DataTable dt = new DataTable();
                dt = ui.FetchinControldt(@"select row_number() over (order by u.idx) as sn, u.idx, (u.firstName + ' ' + u.lastName) as doctorName
                from users u
                inner join facultySchedule f on f.userIdx = u.idx
                where u.visible = 1 and u.userType = 1 and f.[day] = DATENAME(dw, GETDATE())");
                if (dt.Rows.Count > 0)
                {
                    ddlRegPhysision.DataSource = dt;
                    ddlRegPhysision.DataValueField = "idx";
                    ddlRegPhysision.DataTextField = "doctorName";
                    ddlRegPhysision.DataBind();
                    ddlRegPhysision.Items.Insert(0, "Select Doctors");
                }
            }
            catch (Exception ex)
            { }
        }

        protected void btnSubmitRegPatient_Click(object sender, EventArgs e)
        {
            try
            {

                int y = 0;
                y = ui.ExecuteScalar(@"INSERT INTO [dbo].[token]
                               ([patientIdx]
                               ,[tokenNumber]
                               ,[physicianIdx]
                               ,[appointmentDate]
                               ,[fee]
                               ,[specialityIdx]
                               ,[createdByUserIdx]
                                )
                         VALUES
                               (
                                '" + Session["patientIdx"].ToString() + @"',
                                '" + ui.GetSQLInject(txtRegToken.Text) + @"',
                                '" + ui.GetSQLInject(ddlRegPhysision.SelectedValue) + @"',
                                '" + ui.GetSQLInject(txtRegAppointmentDate.Text) + @"',
                                '" + ui.GetSQLInject(txtRegFee.Text) + @"',
                                '" + ui.GetSQLInject(ddlDisess.SelectedValue.ToString()) + @"',
                                '" + Session["appUserId"].ToString() + @"'
                                )" + "SELECT CAST(scope_identity() AS int)");

                if (y > 0)
                {
                    clearRegPatients();
                    getToken();

                    Session["tokenIdx"] = y;

                    Session["isSubmitted"] = "yes";
                    Response.Redirect("token.aspx");
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "", "<script>showErrorMessage()</script>", false);
                }

            }
            catch (Exception ex)
            {

            }
        }

        protected void btnCancelRegPatient_Click(object sender, EventArgs e)
        {
            clearRegPatients();
            getToken();
        }

        protected void clearRegPatients()
        {
            txtRegCardNumber.Text = "";
            txtRegPatientName.Text = "";
            txtRegAge.Text = "";
            txtRegContactNumber1.Text = "";
            txtRegContactNumber2.Text = "";
            txtRegToken.Text = "";
            ddlRegPhysision.SelectedIndex = 0;
            txtRegAppointmentDate.Text = "";
            txtRegFee.Text = "";
        }

        protected void getToken()
        {
            try
            {
                int x = 0;
                DataTable dt = new DataTable();
                dt = ui.FetchinControldt(@"select top 1 t.tokenNumber from token t
                where  Convert(date, t.creationDate, 103) = Convert(date, '" + ui.GetSQLInject(txtRegAppointmentDate.Text) + "', 103) and t.physicianIdx = " + ui.GetSQLInject(ddlRegPhysision.SelectedValue) + @" and t.visible = 1
                order by tokenNumber desc");

                if (dt.Rows.Count > 0)
                {
                    x = Convert.ToInt32(dt.Rows[0]["tokenNumber"]) + 1;
                }
                else
                {
                    x = 1;
                }

                txtRegToken.Text = x.ToString();
            }
            catch (Exception ex)
            { }
        }

        protected void getDoctorFee()
        {
            try
            {
                DataTable dt = new DataTable();
                dt = ui.FetchinControldt(@"select * from fee where doctorIdx = " + ui.GetSQLInject(ddlRegPhysision.SelectedValue) + " and visible =1");

                if (dt.Rows.Count > 0)
                {
                    txtRegFee.Text = dt.Rows[0]["fee"].ToString();
                }
                else
                {
                    txtRegFee.Text = "0";
                }
            }
            catch (Exception ex)
            { }
        }

        protected void ddlRegPhysision_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtRegAppointmentDate.Text = DateTime.Now.ToString("dd/MM/yyyy");
            getToken();
            getDoctorFee();
            getDoctorSpesiality();
            ddlDisess.SelectedIndex = 1;
        }

        protected void getDoctorSpesiality()
        {
            try
            {
                DataTable dt = new DataTable();
                dt = ui.FetchinControldt(@"select s.idx, s.specialty from specialty s
                                        inner join users u on s.idx = u.specialityIdx
                                        where u.idx = " + ui.GetSQLInject(ddlRegPhysision.SelectedValue.ToString()));
                if (dt.Rows.Count > 0)
                {
                    ddlDisess.DataSource = dt;
                    ddlDisess.DataValueField = "idx";
                    ddlDisess.DataTextField = "specialty";
                    ddlDisess.DataBind();
                    ddlDisess.Items.Insert(0, "Select Disess");
                }
            }
            catch (Exception ex)
            { }
        }
        
        #endregion


    }
}