using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace HMS
{
    public partial class viewFeeList : System.Web.UI.Page
    {
        Utilities ui = new Utilities();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["sysAccess"] != null)
                {
                    DataTable dtSysAccess = (DataTable)Session["sysAccess"];
                    if (dtSysAccess.Rows[0]["feeList"].ToString() == "0")
                    {
                        Session["page"] = "Settings / Fee List";
                        Response.Redirect("404.aspx");
                    }
                }

                bindFee();
                GetAccessRights();
            }
        }
        private void GetAccessRights()
        {
            DataTable dt = new DataTable();
            dt = ui.FetchinControldtPara(@"SELECT r.userIdx,u.idx,u.pageUrl FROM Roles r inner join Url u On u.idx = r.pageUrl Where r.visible = 1 and r.userIdx =  @param AND u.idx='18'", Session["appUserId"].ToString());
            if (dt.Rows.Count > 0)
            {
                if (dt.Rows[0]["pageUrl"].ToString() == "viewFeeList.aspx")
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
        protected void bindFee()
        {
            try
            {
                DataTable dt = new DataTable();
                dt = ui.FetchinControldt(@"select row_number() over (order by f.idx) as sn, f.*, (u.firstName + ' ' + u.lastName) as doctorName from fee f
                                            inner join users u on u.idx = f.doctorIdx
                                            where f.visible = 1");
                if (dt.Rows.Count > 0)
                {
                    rptFee.DataSource = dt;
                    rptFee.DataBind();
                }
                else
                {
                    rptFee.DataSource = null;
                    rptFee.DataBind();
                }
            }
            catch (Exception ex)
            { }
        }

        protected void lnkView_Click(object sender, EventArgs e)
        {
            LinkButton lnk = (LinkButton)sender;
            string idx = lnk.CommandArgument.ToString();

            Session["feeIdx"] = idx;
            Session["feeAction"] = "view";
            Response.Redirect("addFee.aspx");
        }

        protected void lnkDelete_Click(object sender, EventArgs e)
        {
            try
            {
                LinkButton lnk = (LinkButton)sender;
                string idx = lnk.CommandArgument.ToString();

                bool x;
                x = ui.ExecuteNonQuery(@"update fee set visible = 0 where idx = " + idx);
                if (x == true)
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "", "<script>deleteMessage()</script>", false);
                    bindFee();
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