using System;
using System.Data;
using System.Web.UI;

namespace HMS
{
    public partial class changePassword : System.Web.UI.Page
    {
        Utilities ui = new Utilities();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                GetAccessRights();
            }
        }
        private void GetAccessRights()
        {
            DataTable dt = new DataTable();
            dt = ui.FetchinControldtPara(@"SELECT r.userIdx,u.idx,u.pageUrl FROM Roles r inner join Url u On u.idx = r.pageUrl Where r.visible = 1 and r.userIdx =  @param AND u.idx='23'", Session["appUserId"].ToString());
            if (dt.Rows.Count > 0)
            {
                if (dt.Rows[0]["pageUrl"].ToString() == "changePassword.aspx")
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
        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            try
            {
                if (Session["appUserPassword"] != null)
                {
                    if (txtOldPassword.Text == Session["appUserPassword"].ToString())
                    {
                        if (txtNewPassword.Text == txtConfirmNewPassword.Text)
                        {
                            bool x;
                            x = ui.ExecuteNonQuery(@"update users set password = '" + ui.GetSQLInject(txtNewPassword.Text) + "' where idx = " + Session["appUserId"].ToString());

                            if (x == true)
                            {
                                Session["appUserPassword"] = txtNewPassword.Text;
                                ScriptManager.RegisterStartupScript(this, this.GetType(), "", "<script>showSuccessMessage()</script>", false);
                                
                                //DataTable dt = new DataTable();
                                //dt = ui.FetchinControldt(@"select idx, firstName, lastName, email from users where visible = 1 and idx = " + Session["appUserId"].ToString());
                                //if (dt.Rows.Count > 0)
                                //{
                                //    emails e1 = new emails();
                                //    e1.NAME = dt.Rows[0]["firstName"].ToString() + " " + dt.Rows[0]["lastName"].ToString();
                                //    e1.MAILTO = dt.Rows[0]["email"].ToString();
                                //    e1.MAILFROM = "noreply.yiff@gmail.com";
                                //    e1.BCC = "izhar.supersoft@gmail.com";

                                //    int k = e1.usersResetPassword(ui.GetSQLInject(txtNewPassword.Text));
                                //}
                            }
                        }
                        else
                        {
                            lblError.Visible = true;
                            lblError.Text = "New Password and Confirm New Password Not Matched.";
                        }
                    }
                    else
                    {
                        lblError.Visible = true;
                        lblError.Text = "Old Password is invalid.";
                    }
                }
            }
            catch (Exception ex)
            { }
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {

        }

    }
}