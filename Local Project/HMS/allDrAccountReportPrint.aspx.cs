using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace HMS
{
    public partial class allDrAccountReportPrint : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["dt"] != null)
                {
                    getReportData();
                }
            }
        }

        protected void getReportData()
        {
            DataTable dt = (DataTable)Session["dt"];
            if (dt.Rows.Count > 0)
            {
                rptAllDoctors.DataSource = dt;
                rptAllDoctors.DataBind();
                lblDate.Text = DateTime.Now.ToString("dd/MM/yyyy");
                lblReportBy.Text = Session["appUserName"].ToString();
                lblTotalAmount.Text = Session["totalSalary"].ToString();
                lblAmountInWords.Text = Session["totalInWords"].ToString();
                lblTotalPatients.Text = Convert.ToString(dt.Compute("Sum(totalPatients)", string.Empty));
            }
        }
    }
}