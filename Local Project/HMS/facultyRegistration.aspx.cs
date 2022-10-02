using System;
using System.Data;
using System.IO;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace HMS
{
    public partial class facultyRegistration : System.Web.UI.Page
    {
        Utilities ui = new Utilities();
        [WebMethod]
        public static bool IsUserAvailable(string username)
        {
            Utilities uit = new Utilities();
            DataTable dt = new DataTable();
            dt = uit.FetchinControldt(@"select idx from users where loginId = '" + username + "' and visible = 1");
            if (dt.Rows.Count > 0)
                return false;
            else
                return true;
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.IsPostBack)
            {
                if (Session["sysAccess"] != null)
                {
                    DataTable dtSysAccess = (DataTable)Session["sysAccess"];
                    if (dtSysAccess.Rows[0]["empReg"].ToString() == "0")
                    {
                        Session["page"] = "Employee / Faculty Registration";
                        Response.Redirect("404.aspx");
                    }
                }

                imgFaculty.ImageUrl = "assets/images/noImage.jpg";

                bindGender();
                bindMaritalStatus();
                bindDepartment();
                bindUserType();

                #region Get week days
                System.Data.DataTable dtDaysOfWeek = new System.Data.DataTable("DaysOfWeek");
                dtDaysOfWeek.Columns.Add("ID");
                dtDaysOfWeek.Columns.Add("day");
                dtDaysOfWeek.Columns.Add("fromTime");
                dtDaysOfWeek.Columns.Add("toTime");

                foreach (DayOfWeek val in Enum.GetValues(typeof(DayOfWeek)))
                {
                    dtDaysOfWeek.Rows.Add((int)val, val.ToString());
                }

                grvSchedule.DataSource = dtDaysOfWeek;
                grvSchedule.DataBind();
                #endregion

                if (Session["userAction"] != null)
                {
                    fillForm();
                    Session["userAction"] = null;
                    txtPassword.Enabled = false;
                    btnSubmit.Enabled = false;
                    btnEdit.Enabled = true;
                    enable(false);
                }
                GetAccessRights();
            }
        }
        private void GetAccessRights()
        {
            DataTable dt = new DataTable();
            dt = ui.FetchinControldtPara(@"SELECT r.userIdx,u.idx,u.pageUrl FROM Roles r inner join Url u On u.idx = r.pageUrl Where r.visible = 1 and r.userIdx =  @param AND u.idx='2'", Session["appUserId"].ToString());
            if (dt.Rows.Count > 0)
            {
                if (dt.Rows[0]["pageUrl"].ToString() == "facultyRegistration.aspx")
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
        protected void fillForm()
        {
            DataTable dt = new DataTable();
            dt = ui.FetchinControldt(@"select * from users where idx = " + Session["userIdx"].ToString());
            if (dt.Rows.Count > 0)
            {
                txtFirstName.Text = dt.Rows[0]["firstName"].ToString();
                txtLastName.Text = dt.Rows[0]["lastName"].ToString();
                ddlGender.SelectedValue = dt.Rows[0]["genderIdx"].ToString();
                ddlMaritalStatus.SelectedValue = dt.Rows[0]["maritalStatusIdx"].ToString();
                txtDob.Text = dt.Rows[0]["dob"].ToString();
                txtCnic.Text = dt.Rows[0]["cnic"].ToString();
                txtContactNumber.Text = dt.Rows[0]["contact"].ToString();
                txtEmail.Text = dt.Rows[0]["email"].ToString();
                txtAddress.Text = dt.Rows[0]["residentialAddress"].ToString();
                ddlDepartment.SelectedValue = dt.Rows[0]["departmentIdx"].ToString();
                bindDesignation();
                ddlDesignation.SelectedValue = dt.Rows[0]["designationIdx"].ToString();
                txtHiringDate.Text = dt.Rows[0]["doj"].ToString();
                txtSalary.Text = dt.Rows[0]["salary"].ToString();
                ddlUserType.SelectedValue = dt.Rows[0]["userType"].ToString();
                bindSpeciality();
                ddlSpeciality.SelectedValue = dt.Rows[0]["specialityIdx"].ToString();
                txtLoginID.Text = dt.Rows[0]["loginId"].ToString();

                if (dt.Rows[0]["userImage"].ToString() != "")
                {
                    imgFaculty.ImageUrl = "~/userImages/" + dt.Rows[0]["userImage"].ToString();
                    Session["userImage"] = dt.Rows[0]["userImage"].ToString();
                    btnImageUpload.Enabled = false;
                    fuFaculty.Enabled = false;
                }
                else
                {
                    imgFaculty.ImageUrl = "assets/images/noImage.jpg";
                }

                int x = Convert.ToInt32(dt.Rows[0]["isActive"].ToString());
                if (x == 1)
                {
                    btnActivate.Visible = false;
                    btnDeActivate.Visible = true;
                }
                else if (x == 0)
                {
                    btnActivate.Visible = true;
                    btnDeActivate.Visible = false;
                }




                DataTable dtSchedule = new DataTable();
                dtSchedule = ui.FetchinControldt(@"select *,(select fromTime from facultySchedule fs where fs.day=da.Day and fs.userIdx=" + Session["userIdx"].ToString() + " and visible=1) as fromTime,(select toTime from facultySchedule fs where fs.day=da.Day and fs.userIdx= " + Session["userIdx"].ToString() + "  and visible=1)  as toTime from Days da ");
                if (dtSchedule.Rows.Count > 0)
                {
                    grvSchedule.DataSource = dtSchedule;
                    grvSchedule.DataBind();
                }
                else
                {
                    grvSchedule.DataSource = null;
                    grvSchedule.DataBind();
                }
            }
        }

        protected void enable(bool enable)
        {
            txtFirstName.Enabled = enable;
            txtLastName.Enabled = enable;
            ddlGender.Enabled = enable;
            ddlMaritalStatus.Enabled = enable;
            txtDob.Enabled = enable;
            txtCnic.Enabled = enable;
            txtContactNumber.Enabled = enable;
            txtEmail.Enabled = enable;
            txtAddress.Enabled = enable;
            ddlDepartment.Enabled = enable;
            ddlDesignation.Enabled = enable;
            txtHiringDate.Enabled = enable;
            txtSalary.Enabled = enable;
            ddlUserType.Enabled = enable;
            ddlSpeciality.Enabled = enable;
            txtLoginID.Enabled = enable;
            btnUpdate.Enabled = enable;
          
        }
        
        protected void bindGender()
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
                    ddlGender.Items.Insert(0, "Select Gender");
                }
            }
            catch (Exception ex)
            { }
        }

        protected void bindMaritalStatus()
        {
            try
            {
                DataTable dt = new DataTable();
                dt = ui.FetchinControldt(@"select * from maritalStatus where visible = 1");
                if (dt.Rows.Count > 0)
                {
                    ddlMaritalStatus.DataSource = dt;
                    ddlMaritalStatus.DataValueField = "idx";
                    ddlMaritalStatus.DataTextField = "maritalStatusName";
                    ddlMaritalStatus.DataBind();
                    ddlMaritalStatus.Items.Insert(0, "Select Marital Status");
                }
            }
            catch (Exception ex)
            { }
        }

        protected void bindDepartment()
        {
            try
            {
                DataTable dt = new DataTable();
                dt = ui.FetchinControldt(@"select * from department where visible = 1");
                if (dt.Rows.Count > 0)
                {
                    ddlDepartment.DataSource = dt;
                    ddlDepartment.DataValueField = "idx";
                    ddlDepartment.DataTextField = "departmentName";
                    ddlDepartment.DataBind();
                    ddlDepartment.Items.Insert(0, "Select Department");
                }
            }
            catch (Exception ex)
            { }
        }

        protected void bindDesignation()
        {
            try
            {
                DataTable dt = new DataTable();
                dt = ui.FetchinControldt(@"select * from designation where departmentIdx = " + ddlDepartment.SelectedValue + " and visible = 1");
                if (dt.Rows.Count > 0)
                {
                    ddlDesignation.DataSource = dt;
                    ddlDesignation.DataValueField = "idx";
                    ddlDesignation.DataTextField = "designationName";
                    ddlDesignation.DataBind();
                    ddlDesignation.Items.Insert(0, "Select Designation");
                }
            }
            catch (Exception ex)
            { }
        }

        protected void bindUserType()
        {
            try
            {
                DataTable dt = new DataTable();
                dt = ui.FetchinControldt(@"select * from userType where visible = 1");
                if (dt.Rows.Count > 0)
                {
                    ddlUserType.DataSource = dt;
                    ddlUserType.DataValueField = "idx";
                    ddlUserType.DataTextField = "userTypeName";
                    ddlUserType.DataBind();
                    ddlUserType.Items.Insert(0, "Select User Type");
                }
            }
            catch (Exception ex)
            { }
        }

        protected void bindSpeciality()
        {
            try
            {
                if (ddlUserType.SelectedValue == "1")
                {
                    divSchedule.Visible = true;
                }
                else if (ddlUserType.SelectedValue == "2")
                {
                    divSchedule.Visible = false;
                }

                DataTable dt = new DataTable();
                dt = ui.FetchinControldt(@"select * from specialty where userTypeIdx = " + ddlUserType.SelectedValue + " and visible = 1");
                if (dt.Rows.Count > 0)
                {
                    ddlSpeciality.DataSource = dt;
                    ddlSpeciality.DataValueField = "idx";
                    ddlSpeciality.DataTextField = "specialty";
                    ddlSpeciality.DataBind();
                    ddlSpeciality.Items.Insert(0, "Select Speciality");
                }
            }
            catch (Exception ex)
            { }
        }

        protected void ddlDepartment_SelectedIndexChanged(object sender, EventArgs e)
        {
            bindDesignation();
        }

        protected void ddlUserType_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            bindSpeciality();
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            try
            {

                string imgFile = "noImage.jpg";
                if (Session["userImage"] != null)
                {
                    imgFile = Session["userImage"].ToString();
                    Session["userImage"] = "";
                }

                bool check = ui.ExistorNotNewParam(@"select loginId from Users where loginId = @Value ", txtLoginID.Text);

                if (check == true)
                {
                    string msg = "UserID Existed, Try Another";
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "", "showErrorMessage('" + msg + "');", true);
                }
                else 
                {


                    int x = 0;
                    x = ui.ExecuteScalar(@"INSERT INTO [dbo].[users]
               ([firstName]
               ,[lastName]
               ,[genderIdx]
               ,[maritalStatusIdx]
               ,[dob]
               ,[cnic]
               ,[contact]
               ,[email]
               ,[residentialAddress]
               ,[departmentIdx]
               ,[designationIdx]
               ,[doj]
               ,[salary]
               ,[userType]
               ,[specialityIdx]
               ,[loginId]
               ,[password]
               ,[createdByUserIdx]
               ,userImage

                )
         VALUES
               (
                '" + ui.GetSQLInject(txtFirstName.Text) + @"',
                '" + ui.GetSQLInject(txtLastName.Text) + @"',
                '" + ui.GetSQLInject(ddlGender.SelectedValue) + @"',
                '" + ui.GetSQLInject(ddlMaritalStatus.SelectedValue) + @"',
                '" + ui.GetSQLInject(txtDob.Text) + @"',
                '" + ui.GetSQLInject(txtCnic.Text) + @"',
                '" + ui.GetSQLInject(txtContactNumber.Text) + @"',
                '" + ui.GetSQLInject(txtEmail.Text) + @"',
                '" + ui.GetSQLInject(txtAddress.Text) + @"',
                '" + ui.GetSQLInject(ddlDepartment.SelectedValue) + @"',
                '" + ui.GetSQLInject(ddlDesignation.SelectedValue) + @"',
                '" + ui.GetSQLInject(txtHiringDate.Text) + @"',
                '" + ui.GetSQLInject(txtSalary.Text) + @"',
                '" + ui.GetSQLInject(ddlUserType.SelectedValue) + @"',
                '" + ui.GetSQLInject(ddlSpeciality.SelectedValue) + @"',
                '" + ui.GetSQLInject(txtLoginID.Text) + @"',
                '" + ui.GetSQLInject(txtPassword.Text) + @"',
                '" + Session["appUserId"].ToString() + @"',
                '" + imgFile + @"'
                )" + "SELECT CAST(scope_identity() AS int)");


                    if (x > 0)
                    {
                        foreach (GridViewRow i in grvSchedule.Rows)
                        {
                            Label Day = (Label)i.FindControl("lblMonday");
                            TextBox FromTime = (TextBox)i.FindControl("txtFromTime");
                            TextBox ToTime = (TextBox)i.FindControl("txtToTime");
                            CheckBox chk = (CheckBox)i.FindControl("chkRow");
                            if (chk.Checked == true)
                            {
                                bool result = ui.ExecuteNonQuery(@"INSERT INTO [dbo].[facultySchedule]
                       ([userIdx]
                       ,[day]
                       ,[fromTime]
                       ,[toTime]
                       ,[createdByUserIdx])
                 VALUES
                       (
                        '" + x + @"',
                        '" + ui.GetSQLInject(Day.Text) + @"',
                        '" + ui.GetSQLInject(FromTime.Text) + @"',
                        '" + ui.GetSQLInject(ToTime.Text) + @"',
                        '" + Session["appUserId"].ToString() + @"'
                       )");

                            }
                        }
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "", "<script>showSuccessMessage()</script>", false);

                        emails e1 = new emails();
                        e1.NAME = txtFirstName.Text + " " + txtLastName.Text;
                        e1.MAILTO = txtEmail.Text;
                        e1.MAILFROM = "noreply@yiff.com";
                        e1.BCC = "izhar.supersoft@gmail.com";
                        string url = "hms.jobtalaash.com/login.aspx";

                        int k = e1.usersRegistrationEmail(url, txtLoginID.Text, txtPassword.Text);
                        clearAll();
                       
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "", "<script>showErrorMessage()</script>", false);
                    }
                }
            }
            catch (Exception ex)
            { }
        }

        protected void clearAll()
        {
            txtFirstName.Text = "";
            txtLastName.Text = "";
            ddlGender.SelectedIndex = 0;
            ddlMaritalStatus.SelectedIndex = 0;
            txtDob.Text = "";
            txtCnic.Text = "";
            txtContactNumber.Text = "";
            txtEmail.Text = "";
            txtAddress.Text = "";
            ddlDepartment.SelectedIndex = 0;
            ddlDesignation.SelectedIndex = 0;
            txtHiringDate.Text = "";
            txtSalary.Text = "";
            ddlUserType.SelectedIndex = 0;
            ddlSpeciality.SelectedIndex = 0;
            txtLoginID.Text = "";
            txtPassword.Text = "";
            ViewState["faculty"] = null;
            Session["userImage"] = null;
            imgFaculty.ImageUrl = "assets/images/noImage.jpg";
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            clearAll();
        }

        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                string imgFile = "noImage.jpg";
                if (Session["userImage"] != null)
                {
                    imgFile = Session["userImage"].ToString();
                    Session["userImage"] = "";
                }

                bool check = ui.ExistorNotNewParam2(@"select loginId from Users where loginId = @Value1  AND idx != @Value2", txtLoginID.Text, Session["userIdx"].ToString());

                if (check == true)
                {
                    string msg = "UserID Existed, Try Another";
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "", "showErrorMessage('" + msg + "');", true);
                }
                else
                {
                    bool x;
                    x = ui.ExecuteNonQuery(@"UPDATE [dbo].[users]
                           SET [firstName] = '" + ui.GetSQLInject(txtFirstName.Text) + @"'
                              ,[lastName] = '" + ui.GetSQLInject(txtLastName.Text) + @"'
                              ,[genderIdx] = '" + ui.GetSQLInject(ddlGender.SelectedValue) + @"'
                              ,[maritalStatusIdx] = '" + ui.GetSQLInject(ddlMaritalStatus.SelectedValue) + @"'
                              ,[dob] = '" + ui.GetSQLInject(txtDob.Text) + @"'
                              ,[cnic] = '" + ui.GetSQLInject(txtCnic.Text) + @"'
                              ,[contact] = '" + ui.GetSQLInject(txtContactNumber.Text) + @"'
                              ,[email] = '" + ui.GetSQLInject(txtEmail.Text) + @"'
                              ,[residentialAddress] = '" + ui.GetSQLInject(txtAddress.Text) + @"'
                              ,[departmentIdx] = '" + ui.GetSQLInject(ddlDepartment.SelectedValue) + @"'
                              ,[designationIdx] = '" + ui.GetSQLInject(ddlDesignation.SelectedValue) + @"'
                              ,[doj] = '" + ui.GetSQLInject(txtHiringDate.Text) + @"'
                              ,[salary] = '" + ui.GetSQLInject(txtSalary.Text) + @"'
                              ,[userType] = '" + ui.GetSQLInject(ddlUserType.SelectedValue) + @"'
                              ,[specialityIdx] = '" + ui.GetSQLInject(ddlSpeciality.SelectedValue) + @"'
                              ,[loginId] = '" + ui.GetSQLInject(txtLoginID.Text) + @"'
                              ,[userImage] = '" + imgFile + @"'
                         WHERE idx = " + Session["userIdx"].ToString());

                    Session["userImage"] = "";

                    if (x == true)
                    {

                        ui.ExecuteNonQuery(@"delete from facultySchedule where userIdx = " + Session["userIdx"].ToString());

                        foreach (GridViewRow i in grvSchedule.Rows)
                        {
                            Label Day = (Label)i.FindControl("lblMonday");
                            TextBox FromTime = (TextBox)i.FindControl("txtFromTime");
                            TextBox ToTime = (TextBox)i.FindControl("txtToTime");
                            CheckBox chk = (CheckBox)i.FindControl("chkRow");
                            if (chk.Checked == true)
                            {
                                bool result = ui.ExecuteNonQuery(@"INSERT INTO [dbo].[facultySchedule]
                       ([userIdx]
                       ,[day]
                       ,[fromTime]
                       ,[toTime]
                       ,[createdByUserIdx])
                 VALUES
                       (
                        '" + Session["userIdx"].ToString() + @"',
                        '" + ui.GetSQLInject(Day.Text) + @"',
                        '" + ui.GetSQLInject(FromTime.Text) + @"',
                        '" + ui.GetSQLInject(ToTime.Text) + @"',
                        '" + Session["appUserId"].ToString() + @"'
                       )");

                            }
                        }


                        txtPassword.Enabled = false;
                        btnSubmit.Enabled = false;
                        btnEdit.Enabled = true;
                        enable(false);
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "", "<script>showSuccessMessage()</script>", false);
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "", "<script>showErrorMessage()</script>", false);
                    }

                    }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "", "<script>showErrorMessage()</script>", false);
            }
        }

        protected void btnActivate_Click(object sender, EventArgs e)
        {
            bool x;
            x = ui.ExecuteNonQuery(@"update users set isActive = 1 where idx = " + Session["userIdx"].ToString());
            if (x == true)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "", "<script>showSuccessMessage()</script>", false);
                btnActivate.Visible = false;
                btnDeActivate.Visible = true;
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "", "<script>showErrorMessage()</script>", false);
            }
        }

        protected void btnDeActivate_Click(object sender, EventArgs e)
        {
            bool x;
            x = ui.ExecuteNonQuery(@"update users set isActive = 0 where idx = " + Session["userIdx"].ToString());
            if (x == true)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "", "<script>showSuccessMessage()</script>", false);
                btnActivate.Visible = true;
                btnDeActivate.Visible = false;
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "", "<script>showErrorMessage()</script>", false);
            }
        }

        protected void btnEdit_Click(object sender, EventArgs e)
        {
            enable(true);
            btnEdit.Enabled = false;
            btnImageUpload.Enabled = true;
            fuFaculty.Enabled = true;
        }

        protected void btnImageUpload_Click(object sender, EventArgs e)
        {
            if (fuFaculty.HasFile)
            {
                if (Path.GetExtension(fuFaculty.FileName) == ".jpg" || Path.GetExtension(fuFaculty.FileName) == ".jpeg" || Path.GetExtension(fuFaculty.FileName) == ".png")  // Check for file 
                {
                    Random rnd = new Random();
                    int ran = rnd.Next(52);
                    string fileName = Path.Combine(Server.MapPath("~/userImages"), fuFaculty.FileName + "_" + ran);
                    fuFaculty.SaveAs(fileName);

                    string strname = fuFaculty.FileName.ToString();
                    fuFaculty.PostedFile.SaveAs(Server.MapPath("~/userImages/") + ran + strname);

                    Session["userImage"] = ran + strname;


                    if (Session["userImage"] != null)
                    {
                        imgFaculty.ImageUrl = "~/userImages/" + Session["userImage"].ToString();
                    }
                    else
                    {
                        imgFaculty.ImageUrl = "assets/images/noImage.jpg";
                    }

                }
            }
        }
    }
}