using System;
using System.Data;
using System.Web.UI;

namespace HMS
{
    public partial class patientRegistration : System.Web.UI.Page
    {
        Utilities ui = new Utilities();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["sysAccess"] != null)
                {
                    DataTable dtSysAccess = (DataTable)Session["sysAccess"];
                    if (dtSysAccess.Rows[0]["patientReg"].ToString() == "0")
                    {
                        Session["page"] = "Patients Registration";
                        Response.Redirect("404.aspx");
                    }
                }



                fillGender();
                bindPhysicianForNewPatient();

                if (Session["isSubmitted"] != null)
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "", "<script>showSuccessMessage()</script>", false);
                    string url = "tokenPrint.aspx";
                    string s = "window.open('" + url + "', 'popup_window', 'width=800,height=600,resizable=no');";
                    ClientScript.RegisterStartupScript(this.GetType(), "script", s, true);
                    Session["isSubmitted"] = null;
                }

                if (Session["patientAction"] != null)
                {
                    fillForm();
                    Session["patientAction"] = null;
                    btnSubmitNewPatient.Enabled = false;
                    btnEdit.Enabled = true;
                    enable(false);
                    btnCancelNewPatient.Enabled = false;

                    tokenDiv.Visible = false;
                    txtTokenNumber.Text = "0";
                    ddlPhysician.SelectedIndex = 0;
                    txtApointmentDate.Text = DateTime.Now.ToString("dd/MM/yyyy");
                    txtFee.Text = "0";
                }
                else
                {
                    //getToken();
                    getNewCardNumber();
                }
                GetAccessRights();
            }
        }
        private void GetAccessRights()
        {
            DataTable dt = new DataTable();
            dt = ui.FetchinControldtPara(@"SELECT r.userIdx,u.idx,u.pageUrl FROM Roles r inner join Url u On u.idx = r.pageUrl Where r.visible = 1 and r.userIdx =  @param AND u.idx='6'", Session["appUserId"].ToString());
            if (dt.Rows.Count > 0)
            {
                if (dt.Rows[0]["pageUrl"].ToString() == "patientRegistration.aspx")
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

        #region New Patient

        protected void getNewCardNumber()
        {
            try
            {
                int x = 0;
                DataTable dt = new DataTable();
                dt = ui.FetchinControldt(@"select top 1 cardNumber from patentRegistration where convert(Date,creationDate) = convert(Date,getdate()) and visible = 1 order by idx desc");
                if (dt.Rows.Count > 0)
                {
                    x = Convert.ToInt32(dt.Rows[0]["cardNumber"]) + 1;
                }
                else
                {
                    int d = Convert.ToInt32(DateTime.Now.Day.ToString());
                    int m = Convert.ToInt32(DateTime.Now.Month.ToString());
                    int y = Convert.ToInt32(DateTime.Now.Year.ToString());
                    string t = Convert.ToString(d) + Convert.ToString(m) + Convert.ToString(y);
                    x = Convert.ToInt32(t + "1");
                }

                txtCardNumber.Text = x.ToString();
            }
            catch (Exception ex)
            { }
        }
        protected void bindPhysicianForNewPatient()
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
                    ddlPhysician.DataSource = dt;
                    ddlPhysician.DataValueField = "idx";
                    ddlPhysician.DataTextField = "doctorName";
                    ddlPhysician.DataBind();
                    ddlPhysician.Items.Insert(0, "Select Doctors");
                }
            }
            catch (Exception ex)
            { }
        }

        protected void btnSubmitNewPatient_Click(object sender, EventArgs e)
        {
            try
            {
                int x = 0;
                x = ui.ExecuteScalar(@"INSERT INTO [dbo].[patentRegistration]
                               ([cardNumber]
                               ,[patientName]
                               ,[age]
                               ,[contactNumber1]
                               ,[contactNumber2]
                               ,[referenceName]
                               ,[referenceCnic]
                               ,[gender] 
                               ,[createdByUserIdx]
                                )
                         VALUES
                               (
                                '" + ui.GetSQLInject(txtCardNumber.Text) + @"',
                                '" + ui.GetSQLInject(txtPatientName.Text) + @"',
                                '" + ui.GetSQLInject(txtAge.Text) + @"',
                                '" + ui.GetSQLInject(txtContactNumber1.Text) + @"',
                                '" + ui.GetSQLInject(txtContactNumber2.Text) + @"',
                                '" + ui.GetSQLInject(txtReferenceName.Text) + @"',
                                '" + ui.GetSQLInject(txtReferenceCNIC.Text) + @"',
                                '" + ui.GetSQLInject(ddlGender.SelectedValue.ToString()) + @"',
                                '" + Session["appUserId"].ToString() + @"'
                                 )" + "SELECT CAST(scope_identity() AS int)");
                if (x > 0)
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
                                '" + x + @"',
                                '" + ui.GetSQLInject(txtTokenNumber.Text) + @"',
                                '" + ui.GetSQLInject(ddlPhysician.SelectedValue) + @"',
                                '" + ui.GetSQLInject(txtApointmentDate.Text) + @"',
                                '" + ui.GetSQLInject(txtFee.Text) + @"',
                                '" + ui.GetSQLInject(ddlDisess.SelectedValue.ToString()) + @"',
                                '" + Session["appUserId"].ToString() + @"'
                                )" + "SELECT CAST(scope_identity() AS int)");

                    if (y > 0)
                    {
                        clearNewPatients();

                        Session["isSubmitted"] = "yes";
                        Session["tokenIdx"] = y;
                        Response.Redirect("patientRegistration.aspx");
                        getNewCardNumber();
                        getToken();
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "", "<script>showErrorMessage()</script>", false);
                    }
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

        protected void btnCancelNewPatient_Click(object sender, EventArgs e)
        {
            clearNewPatients();
            getToken();
        }

        protected void clearNewPatients()
        {
            txtCardNumber.Text = "";
            txtPatientName.Text = "";
            txtAge.Text = "";
            txtContactNumber1.Text = "";
            txtContactNumber2.Text = "";
            txtTokenNumber.Text = "";
            ddlPhysician.SelectedIndex = 0;
            txtApointmentDate.Text = "";
            txtFee.Text = "";
            txtReferenceName.Text = "";
            txtReferenceCNIC.Text = "";
        }

        protected void getToken()
        {
            try
            {
                int x = 0;
                DataTable dt = new DataTable();
                dt = ui.FetchinControldt(@"select top 1 t.tokenNumber from token t
                where  Convert(date, t.creationDate, 103) = Convert(date, getdate(), 103) and t.physicianIdx = " + ui.GetSQLInject(ddlPhysician.SelectedValue) + @" and t.visible = 1
                order by tokenNumber desc");

                if (dt.Rows.Count > 0)
                {
                    x = Convert.ToInt32(dt.Rows[0]["tokenNumber"]) + 1;
                }
                else
                {
                    x = 1;
                }

                txtTokenNumber.Text = x.ToString();
            }
            catch (Exception ex)
            { }
        }

        protected void getDoctorFee()
        {
            try
            {
                DataTable dt = new DataTable();
                dt = ui.FetchinControldt(@"select * from fee where doctorIdx = " + ui.GetSQLInject(ddlPhysician.SelectedValue) + " and visible =1");

                if (dt.Rows.Count > 0)
                {
                    txtFee.Text = dt.Rows[0]["fee"].ToString();
                }
                else
                {
                    txtFee.Text = "0";
                }
            }
            catch (Exception ex)
            { }
        }

        protected void fillGender()
        {
            try
            {
                DataTable dt = new DataTable();
                dt = ui.FetchinControldt(@"select * from gender where visible = 1");

                if (dt.Rows.Count > 0)
                {
                    ddlGender.DataSource = dt;
                    ddlGender.DataValueField = "idx";
                    ddlGender.DataTextField = "genderName";
                    ddlGender.DataBind();
                    ddlGender.Items.Insert(0, "Gender");
                }
                else
                {
                    ddlGender.DataSource = null;
                    ddlGender.DataBind();
                }
            }
            catch (Exception ex)
            { }
        }

        protected void ddlPhysician_SelectedIndexChanged(object sender, EventArgs e)
        {
            getToken();
            getDoctorFee();
            txtApointmentDate.Text = DateTime.Now.ToString("dd/MM/yyyy");
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
                                        where u.idx = " + ui.GetSQLInject(ddlPhysician.SelectedValue.ToString()));
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

        #region Update Patient

        protected void fillForm()
        {
            DataTable dt = new DataTable();
            dt = ui.FetchinControldt(@"select * from patentRegistration where visible = 1 and idx = " + Session["patientIdx"].ToString());
            if (dt.Rows.Count > 0)
            {
                txtCardNumber.Text = dt.Rows[0]["cardNumber"].ToString();
                txtPatientName.Text = dt.Rows[0]["patientName"].ToString();
                txtAge.Text = dt.Rows[0]["age"].ToString();
                txtContactNumber1.Text = dt.Rows[0]["contactNUmber1"].ToString();
                txtContactNumber2.Text = dt.Rows[0]["contactNUmber2"].ToString();
                txtReferenceName.Text = dt.Rows[0]["referenceName"].ToString();
                txtReferenceCNIC.Text = dt.Rows[0]["referenceCnic"].ToString();
                ddlGender.SelectedValue = dt.Rows[0]["gender"].ToString();
            }
        }
        protected void enable(bool enable)
        {
            txtPatientName.Enabled = enable;
            txtAge.Enabled = enable;
            txtContactNumber1.Enabled = enable;
            txtContactNumber2.Enabled = enable;
            ddlGender.Enabled = enable;
            btnUpdate.Enabled = enable;
        }
        protected void btnEdit_Click(object sender, EventArgs e)
        {
            enable(true);
            btnEdit.Enabled = false;
        }
        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                bool x;
                x = ui.ExecuteNonQuery(@"UPDATE [dbo].[patentRegistration]
                           SET 
                               [patientName] = '" + ui.GetSQLInject(txtPatientName.Text) + @"'
                              ,[age] = '" + ui.GetSQLInject(txtAge.Text) + @"'
                              ,[contactNumber1] = '" + ui.GetSQLInject(txtContactNumber1.Text) + @"'
                              ,[contactNumber2] = '" + ui.GetSQLInject(txtContactNumber2.Text) + @"'
                              ,[referenceName] = '" + ui.GetSQLInject(txtReferenceName.Text) + @"'
                              ,[referenceCnic] = '" + ui.GetSQLInject(txtReferenceCNIC.Text) + @"'
                              ,[gender] = '" + ui.GetSQLInject(ddlGender.SelectedValue.ToString()) + @"'
                         WHERE idx = " + Session["patientIdx"].ToString());

                if (x == true)
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "", "<script>showSuccessMessage()</script>", false);
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "", "<script>showErrorMessage()</script>", false);
                }

            }
            catch (Exception ex)
            { }
        }


        #endregion
    }
}