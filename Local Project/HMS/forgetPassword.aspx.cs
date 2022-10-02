using System;
using System.Data;

namespace HMS
{
    public partial class forgetPassword : System.Web.UI.Page
    {
        Utilities ui = new Utilities();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

            }
        }

        protected void btnConfirmPassword_Click(object sender, EventArgs e)
        {
            DataTable dt = new DataTable();
            dt = ui.FetchinControldt(@"select * from Users where PascodeYNID = 1 and idx = " + Session["userIDxForget"].ToString());
            if (dt.Rows.Count > 0)
            {
                if (dt.Rows[0]["passCode"].ToString() == ui.GetSQLInject(txtPassCode.Text))
                {
                    bool x;
                    x = ui.ExecuteNonQuery(@"update Users set [password] = '" + ui.GetSQLInject(txtNewPassword.Text) + "' where idx = " + Session["userIDxForget"].ToString());
                    if (x == true)
                    {
                        lblErrorMessege.Visible = true;
                        lblErrorMessege.ForeColor = System.Drawing.Color.Green;
                        lblErrorMessege.Text = "Password has been changed successfuly.";

                        emails e1 = new emails();
                        e1.NAME = dt.Rows[0]["firstName"].ToString() + " " + dt.Rows[0]["lastName"].ToString();
                        e1.MAILTO = txtEmail.Text;
                        e1.MAILFROM = "noreply.yiff@gmail.com";
                        e1.BCC = "izhar.supersoft@gmail.com";
                        int k = e1.usersResetPassword(ui.GetSQLInject(txtNewPassword.Text));

                    }
                    else
                    {
                        lblErrorMessege.Visible = true;
                        lblErrorMessege.ForeColor = System.Drawing.Color.Red;
                        lblErrorMessege.Text = "Password could not changed.";
                    }
                }
                else
                {
                    lblErrorMessege.Visible = true;
                    lblErrorMessege.ForeColor = System.Drawing.Color.Red;
                    lblErrorMessege.Text = "Passcode has been expired or invalid.";
                }
            }
            else
            {
                lblErrorMessege.Visible = true;
                lblErrorMessege.ForeColor = System.Drawing.Color.Red;
                lblErrorMessege.Text = "Pass Code has been expired.";
            }
        }

        protected void sendCodeForResetPassword()
        {
            try
            {
                Random random = new Random();
                int randomNumber = random.Next(0, 100000000);
                string passCode = randomNumber.ToString();
                string email = txtEmail.Text.Trim();
                DataTable dt = new DataTable();
                dt = ui.FetchinControldtPara("select * from users where email=@param", email.ToString());
                if (dt.Rows.Count > 0)
                {
                    Session["userIDxForget"] = dt.Rows[0]["idx"].ToString();
                    bool result = ui.ExecuteNonQueryWithParam(@"update users set passCode=@Para , PascodeYNID=1 where idx=" + dt.Rows[0]["idx"].ToString() + " ", passCode);
                    if (result)
                    {
                        emails e = new emails();
                        e.NAME = dt.Rows[0]["firstName"].ToString() + " " + dt.Rows[0]["lastName"].ToString();
                        e.MAILTO = txtEmail.Text;
                        e.MAILFROM = "noreply.yiff@gmail.com";
                        e.BCC = "izhar.supersoft@gmail.com";
                        int k = e.usersResetCode(passCode);
                        if (k > 0)
                        {
                            divEmail.Visible = false;
                            btnSend.Visible = false;
                            divPassCode.Visible = true;
                            divPassword.Visible = true;
                            btnConfirmPassword.Visible = true;

                            lblErrorMessege.Visible = true;
                            lblErrorMessege.ForeColor = System.Drawing.Color.Green;
                            lblErrorMessege.Text = "Email sent, Please check your inbox.";
                        }
                        else
                        {
                            divEmail.Visible = true;
                            btnSend.Visible = true;
                            divPassCode.Visible = false;
                            divPassword.Visible = false;
                            btnConfirmPassword.Visible = false;

                            lblErrorMessege.Visible = true;
                            lblErrorMessege.ForeColor = System.Drawing.Color.Red;
                            lblErrorMessege.Text = "Email could not send.";
                        }
                    }
                    else
                    {
                        divEmail.Visible = true;
                        btnSend.Visible = true;
                        divPassCode.Visible = false;
                        divPassword.Visible = false;
                        btnConfirmPassword.Visible = false;

                        lblErrorMessege.Visible = true;
                        lblErrorMessege.ForeColor = System.Drawing.Color.Red;
                        lblErrorMessege.Text = "System error, Please contact system administrator.";
                    }
                }
                else
                {
                    lblErrorMessege.Visible = true;
                    lblErrorMessege.ForeColor = System.Drawing.Color.Red;
                    lblErrorMessege.Text = "Email is invalid!";
                }
            }
            catch (Exception ex)
            { }
        }

        protected void btnSend_Click(object sender, EventArgs e)
        {
            sendCodeForResetPassword();
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            txtEmail.Text = "";
            txtPassCode.Text = "";
            txtNewPassword.Text = "";
        }

        protected void btnBack_Click(object sender, EventArgs e)
        {
            Response.Redirect("Dashboard.aspx");
        }

    }
}