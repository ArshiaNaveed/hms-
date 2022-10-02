using System;
using System.Data;

namespace HMS
{
    public partial class facultyReport : System.Web.UI.Page
    {
        Utilities ui = new Utilities();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                lblDate.Text = DateTime.Now.ToShortDateString();
                lblTime.Text = DateTime.Now.ToShortTimeString();
                lblUserName.Text = Session["appUserName"].ToString();
                bindUsers();
            }
        }

        protected void bindUsers()
        {
            try
            {
                DataTable dt = new DataTable();
                dt = ui.FetchinControldt(@"select row_number() over (order by u.idx) as sn,u.idx, (u.firstName + ' ' + u.lastName) as fullName, dt.departmentName, dn.designationName, ut.userTypeName, sy.specialty, 
                                        case 
                                        when u.isactive = 0 then 'De-Active'
                                        when u.isactive = 1 then 'Active'
                                        end status
                                        from users u
                                        inner join department dt on dt.idx = u.departmentIdx
                                        inner join designation dn on dn.idx = u.designationIdx
                                        inner join userType ut on ut.idx = u.userType
                                        inner join specialty sy on sy.idx = u.specialityIdx
                                        where u.visible = 1 and u.idx <> 1 order by u.idx desc");

                if (dt.Rows.Count > 0)
                {
                    rptUsers.DataSource = dt;
                    rptUsers.DataBind();
                    lblError.Visible = false;
                    divResult.Visible = true;
                }
                else
                {
                    rptUsers.DataSource = "";
                    rptUsers.DataBind();
                    lblError.Visible = true;
                    divResult.Visible = false;
                }

            }
            catch (Exception ex)
            { }
        }
    }
}