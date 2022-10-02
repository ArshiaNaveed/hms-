using System;
using System.Data;

namespace HMS
{
    public partial class patientsReport : System.Web.UI.Page
    {
        Utilities ui = new Utilities();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                lblDate.Text = DateTime.Now.ToShortDateString();
                lblTime.Text = DateTime.Now.ToShortTimeString();
                lblUserName.Text = Session["appUserName"].ToString();
                bindPatients();
            }
        }

        protected void bindPatients()
        {
            try
            {
                DataTable dt = new DataTable();
                dt = ui.FetchinControldt(@"select row_number() over (order by p.idx) as sn, p.*, g.genderName from patentRegistration p 
                                        inner join gender g on g.idx = p.gender
                                        where p.visible = 1");

                if (dt.Rows.Count > 0)
                {
                    rptPatient.DataSource = dt;
                    rptPatient.DataBind();

                    lblError.Visible = false;
                    divResult.Visible = true;
                }
                else
                {
                    rptPatient.DataSource = "";
                    rptPatient.DataBind();
                    lblError.Visible = true;
                    divResult.Visible = false;
                }

            }
            catch (Exception ex)
            { }
        }
    }
}