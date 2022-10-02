using System;
using System.Data;
using System.Web.UI;

namespace HMS
{
    public partial class addFee : System.Web.UI.Page
    {
        Utilities ui = new Utilities();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["sysAccess"] != null)
                {
                    DataTable dtSysAccess = (DataTable)Session["sysAccess"];
                    if (dtSysAccess.Rows[0]["addFee"].ToString() == "0")
                    {
                        Session["page"] = "Settings / Add Fee";
                        Response.Redirect("404.aspx");
                    }
                }

                fillDoctors();

                if (Session["FeeAction"] != null)
                {
                    getFee(Convert.ToInt32(Session["feeIdx"].ToString()));
                    Session["FeeAction"] = null;
                }
                GetAccessRights();
            }
        }
        private void GetAccessRights()
        {
            DataTable dt = new DataTable();
            dt = ui.FetchinControldtPara(@"SELECT r.userIdx,u.idx,u.pageUrl FROM Roles r inner join Url u On u.idx = r.pageUrl Where r.visible = 1 and r.userIdx =  @param AND u.idx='17'", Session["appUserId"].ToString());
            if (dt.Rows.Count > 0)
            {
                if (dt.Rows[0]["pageUrl"].ToString() == "addFee.aspx")
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
        protected void fillDoctors()
        {
            try
            {
                DataTable dt = new DataTable();
                dt = ui.FetchinControldt(@"select idx, (firstName + ' ' + lastName)as fullName from users where userType = 1 and isactive= 1 and visible = 1");
                if (dt.Rows.Count > 0)
                {
                    ddlDoctor.DataSource = dt;
                    ddlDoctor.DataValueField = "idx";
                    ddlDoctor.DataTextField = "fullName";
                    ddlDoctor.DataBind();
                    ddlDoctor.Items.Insert(0, "Select Doctors");
                }
            }
            catch (Exception ex)
            { }
        }

        protected void getFee(int idx)
        {
            try
            {
                DataTable dt = new DataTable();
                dt = ui.FetchinControldt(@"select * from fee where visible = 1 and idx = " + idx);
                if (dt.Rows.Count > 0)
                {
                    txtFee.Text = dt.Rows[0]["fee"].ToString();
                    ddlDoctor.SelectedValue = dt.Rows[0]["doctorIdx"].ToString();
                    txtFee.Enabled = false;
                    ddlDoctor.Enabled = false;
                    btnSubmit.Enabled = false;
                    btnUpdate.Enabled = false;
                    btnEdit.Enabled = true;
                }
            }
            catch (Exception ex)
            { }
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            try
            {
                DataTable dt = new DataTable();
                dt = ui.FetchinControldt(@"select * from fee where doctorIdx = " + ui.GetSQLInject(ddlDoctor.SelectedValue) + " and visible = 1");
                if (dt.Rows.Count > 0)
                {
                    lblError.Text = "Record already exist!";
                    lblError.Visible = true;
                }
                else
                {
                    lblError.Visible = false;
                    bool x;
                    x = ui.ExecuteNonQuery(@"INSERT INTO [dbo].[fee]
                                       ([fee]
                                       ,[doctorIdx]
                                        )
                                 VALUES
                                       (
                                        '" + ui.GetSQLInject(txtFee.Text) + @"',
                                        '" + ui.GetSQLInject(ddlDoctor.SelectedValue.ToString()) + @"'
                                        )");
                    if (x == true)
                    {
                        clearAll();
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "", "<script>showSuccessMessage()</script>", false);
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
        protected void btnCancel_Click(object sender, EventArgs e)
        {
            clearAll();
        }

        protected void clearAll()
        {
            txtFee.Text = "";
            ddlDoctor.ClearSelection();
        }

        protected void btnEdit_Click(object sender, EventArgs e)
        {
            txtFee.Enabled = true;
            ddlDoctor.Enabled = true;
            btnSubmit.Enabled = false;
            btnUpdate.Enabled = true;
            btnEdit.Enabled = false;
        }

        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                bool x;
                x = ui.ExecuteNonQuery(@"UPDATE [dbo].[fee]
                                       SET [fee] = '" + ui.GetSQLInject(txtFee.Text) + @"'
                                          ,[doctorIdx] = '" + ui.GetSQLInject(ddlDoctor.SelectedValue.ToString()) + @"'
                                     WHERE idx = " + Session["feeIdx"].ToString());
                if (x == true)
                {
                    Session["feeIdx"] = null;
                    btnUpdate.Enabled = false;
                    btnSubmit.Enabled = true;
                    clearAll();
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
    }
}