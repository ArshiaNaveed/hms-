using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace HMS
{
    public partial class login : Page
    {
        Utilities ui = new Utilities();
        AccessRightsSystemManager accessManager = new AccessRightsSystemManager();

        //UserManager usermanager = new UserManager();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

            }
        }
        public static bool InitializePageSecurityUser(string PageUrl)
        {
            try
            {
                bool status = false;
                string[] arr = PageUrl.Split('/');

                string[] arr2 = arr[arr.Length - 1].Split('?');

                Dictionary<string, AccessRightsSystem> dictForm = (Dictionary<string, AccessRightsSystem>)System.Web.HttpContext.Current.Session["UserAccessDict"];

                if (BaseModel.ApplicationUserID == 1)
                {
                    status = true;
                }
                else
                {
                    if (dictForm != null)
                    {
                        if (dictForm.Count > 0)
                        {
                            if (dictForm.ContainsKey(arr2[0].ToLower()))
                            {
                                status = true;
                            }
                        }
                    }
                }

                return status;

            }
            catch (Exception ex)
            {
                return false;
            }
        }
        protected void btnLogin_Click(object sender, EventArgs e)
        {
            try
            {
                DataTable dt = new DataTable();
                dt = ui.FetchinControldt(@"select u.idx, (u.firstName + ' ' + u.lastName) as appUserName, u.email, u.userType as userTypeIdx, ut.userTypeName, loginid, [password],u.designationIdx, userImage ,isActive, d.designationName from users u
                inner join userType ut on ut.idx = u.userType
                inner join designation d on d.idx = u.designationIdx
                where u.loginId = '" + ui.GetSQLInject(txtLoginId.Text.Trim()) + "' and u.password = '" + ui.GetSQLInject(txtPassword.Text.Trim()) + "' and u.visible = 1");
                if (dt.Rows.Count > 0)
                {
                    if (dt.Rows[0]["isActive"].ToString() == "1")
                    {
                        if (dt.Rows[0]["idx"].ToString() == "1")
                        {

                            lblError.Visible = false;
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "scr", "loginSuccess();", true);
                            Session["appUserId"] = dt.Rows[0]["idx"].ToString();

                           BaseModel.ApplicationUserID =int.Parse( dt.Rows[0]["idx"].ToString());

                            Session["appUserName"] = dt.Rows[0]["appUserName"].ToString();
                            Session["appEmail"] = dt.Rows[0]["email"].ToString();
                            Session["appUserType"] = dt.Rows[0]["userTypeName"].ToString();
                            Session["appDesignation"] = dt.Rows[0]["designationName"].ToString();
                            Session["appUserPassword"] = dt.Rows[0]["password"].ToString();
                            Session["appUserImage"] = dt.Rows[0]["userImage"].ToString();
                            Session["designationIdx"] = dt.Rows[0]["designationIdx"].ToString();
                            //  Session["UserAccessDict"] = accessManager.GetAccessForm(int.Parse(Session["appUserId"].ToString()));
                            InitializePageSecurityUser( Session["appUserId"].ToString());
                            Response.Redirect("dashboard.aspx");
                        }
                        else
                        {
                            lblError.Visible = false;
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "scr", "loginSuccess();", true);
                            Session["appUserId"] = dt.Rows[0]["idx"].ToString();
                            Session["appUserName"] = dt.Rows[0]["appUserName"].ToString();
                            Session["appEmail"] = dt.Rows[0]["email"].ToString();
                            Session["appUserType"] = dt.Rows[0]["userTypeName"].ToString();
                            Session["appDesignation"] = dt.Rows[0]["designationName"].ToString();
                            Session["appUserPassword"] = dt.Rows[0]["password"].ToString();
                            Session["appUserImage"] = dt.Rows[0]["userImage"].ToString();
                            Session["designationIdx"] = dt.Rows[0]["designationIdx"].ToString();
                            Session["UserAccessDict"] = accessManager.GetAccessForm(int.Parse(Session["appUserId"].ToString()));
                            Response.Redirect("dashboard.aspx");
                        }
                    }
                    else
                    {
                        lblError.Visible = true;
                        lblError.Text = "User account is not active.";
                    }
                }
                else
                {
                    lblError.Visible = true;
                    lblError.Text = "Invalid Login ID or Password!";
                }
            }
            catch (Exception ex)
            { }
        }

    }
}