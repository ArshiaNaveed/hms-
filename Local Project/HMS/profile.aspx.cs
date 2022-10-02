using System;
using System.Data;

namespace HMS
{
    public partial class profile : System.Web.UI.Page
    {
        Utilities ui = new Utilities();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
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

                enable(false);
                fillForm();
            }
        }

        protected void fillForm()
        {
            DataTable dt = new DataTable();
            dt = ui.FetchinControldt(@"select * from users where idx = " + Session["appUserId"].ToString());
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
                }
                else
                {
                    imgFaculty.ImageUrl = "assets/images/noImage.jpg";
                }

                DataTable dtSchedule = new DataTable();
                dtSchedule = ui.FetchinControldt(@"select *,(select fromTime from facultySchedule fs where fs.day=da.Day and fs.userIdx=" + Session["appUserId"].ToString() + " and visible=1) as fromTime,(select toTime from facultySchedule fs where fs.day=da.Day and fs.userIdx= " + Session["appUserId"].ToString() + "  and visible=1)  as toTime from Days da ");
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
                dt = ui.FetchinControldt(@"select * from designation where departmentIdx = " + ui.GetSQLInject(ddlDepartment.SelectedValue) + " and visible = 1");
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
                else if (ddlUserType.SelectedValue == "1")
                {
                    divSchedule.Visible = false;
                }

                DataTable dt = new DataTable();
                dt = ui.FetchinControldt(@"select * from specialty where userTypeIdx = " + ui.GetSQLInject(ddlUserType.SelectedValue) + " and visible = 1");
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



    }
}