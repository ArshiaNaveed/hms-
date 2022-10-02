using System;
using System.Data;
using System.Web.UI;

namespace HMS
{
    public partial class accountReport : System.Web.UI.Page
    {
        Utilities ui = new Utilities();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["sysAccess"] != null)
                {
                    DataTable dtSysAccess = (DataTable)Session["sysAccess"];
                    if (dtSysAccess.Rows[0]["accountsReport"].ToString() == "0")
                    {
                        Session["page"] = "Accounts Report";
                        Response.Redirect("404.aspx");
                    }
                }

                bindPhysicianForRegPatient();
                GetAccessRights();
            }
        }
        private void GetAccessRights()
        {
            DataTable dt = new DataTable();
            dt = ui.FetchinControldtPara(@"SELECT r.userIdx,u.idx,u.pageUrl FROM Roles r inner join Url u On u.idx = r.pageUrl Where r.visible = 1 and r.userIdx =  @param AND u.idx='22'", Session["appUserId"].ToString());
            if (dt.Rows.Count > 0)
            {
                if (dt.Rows[0]["pageUrl"].ToString() == "accountreport.aspx")
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
        protected void btnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                DataTable dt = new DataTable();
                if (txtFromDate.Text != "" && txtToDate.Text != "")
                {
                    if (ddlRegPhysision.SelectedIndex == 0)
                    {
                        dt = ui.FetchinControldt(@"select distinct row_number() over (order by t.appointmentDate) as sn,  t.appointmentDate, 
                        DATENAME(WEEKDAY,Convert(date,t.appointmentDate,103)) as appointmentDay, 
                        sum(cast(t.fee as int)) as fee,
                         (u.firstName + ' ' + u.lastName) as doctorName,
                         (select count(t2.idx) from token t2 where t2.physicianIdx = t.physicianIdx and t2.appointmentDate = t.appointmentDate) as totalPatients
                        from token t
                        inner join patentRegistration p on p.idx = t.patientIdx
                        inner join gender g on g.idx = p.gender
                        inner join users u on u.idx = t.physicianIdx 
                        where Convert(date, t.appointmentDate,103) between  Convert(date, '" + ui.GetSQLInject(txtFromDate.Text) + "',103) and Convert(date, '" + ui.GetSQLInject(txtToDate.Text) + @"',103) and t.visible = 1 
                        group by t.appointmentDate, u.firstName, u.lastName, t.physicianIdx
                        order by t.appointmentDate asc  ");
                    }
                    else
                    {
                        dt = ui.FetchinControldt(@"select row_number() over (order by t.appointmentDate) as sn, p.cardNumber, t.appointmentDate, DATENAME(WEEKDAY,Convert(date,t.appointmentDate,103)) as appointmentDay, 
                                                          t.fee, p.patientName,g.genderName,p.age, (u.firstName + ' ' + u.lastName) as doctorName
                                                          from token t
                                                          inner join patentRegistration p on p.idx = t.patientIdx
                                                          inner join gender g on g.idx = p.gender
                                                          inner join users u on u.idx = t.physicianIdx
                          where Convert(date, t.appointmentDate,103) between Convert(date, '" + ui.GetSQLInject(txtFromDate.Text) + "',103) and Convert(date, '" + ui.GetSQLInject(txtToDate.Text) + "' ,103) and t.visible = 1 and t.physicianIdx = " + ui.GetSQLInject(ddlRegPhysision.SelectedValue.ToString()) + @" order by Convert(date, t.appointmentDate,103) asc");
                    }
                }
                else
                {
                    if (ddlRegPhysision.SelectedIndex == 0)
                    {
                        dt = ui.FetchinControldt(@"select distinct row_number() over (order by t.appointmentDate) as sn,  t.appointmentDate, 
                        DATENAME(WEEKDAY,Convert(date,t.appointmentDate,103)) as appointmentDay, 
                        sum(cast(t.fee as int)) as fee,
                         (u.firstName + ' ' + u.lastName) as doctorName,
                         (select count(t2.idx) from token t2 where t2.physicianIdx = t.physicianIdx and t2.appointmentDate = t.appointmentDate) as totalPatients
                        from token t
                        inner join patentRegistration p on p.idx = t.patientIdx
                        inner join gender g on g.idx = p.gender
                        inner join users u on u.idx = t.physicianIdx 
                        where t.visible = 1 
                        group by t.appointmentDate, u.firstName, u.lastName, t.physicianIdx
                        order by t.appointmentDate asc  ");
                    }
                    else
                    {
                        dt = ui.FetchinControldt(@"select row_number() over (order by t.appointmentDate) as sn, p.cardNumber, t.appointmentDate, DATENAME(WEEKDAY,Convert(date,t.appointmentDate,103)) as appointmentDay, 
                                                          t.fee, p.patientName,g.genderName,p.age, (u.firstName + ' ' + u.lastName) as doctorName
                                                          from token t
                                                          inner join patentRegistration p on p.idx = t.patientIdx
                                                          inner join gender g on g.idx = p.gender
                                                          inner join users u on u.idx = t.physicianIdx
                          where  t.visible = 1 and t.physicianIdx = " + ui.GetSQLInject(ddlRegPhysision.SelectedValue.ToString()) + @" order by Convert(date, t.appointmentDate,103) asc");

                    }
                }


                if (dt.Rows.Count > 0)
                {
                    if (ddlRegPhysision.SelectedIndex == 0)
                    {
                        physitionList.Visible = false;
                        allDoctorsList.Visible = true;

                        rptAllDoctors.DataSource = dt;
                        rptAllDoctors.DataBind();

                        Session["allDoctorsList"] = "true";
                        Session["physitionsList"] = null;
                    }
                    else
                    {
                        physitionList.Visible = true;
                        allDoctorsList.Visible = false;

                        rptAccount.DataSource = dt;
                        rptAccount.DataBind();

                        Session["physitionsList"] = "true";
                        Session["allDoctorsList"] = null;
                    }


                    Session["dt"] = dt;
                }
                else
                {
                    rptAccount.DataSource = null;
                    rptAccount.DataBind();

                    rptAllDoctors.DataSource = null;
                    rptAllDoctors.DataBind();
                }

            }
            catch (Exception ex)
            { }
        }

        public string totalsal()
        {
            DataTable dt = new DataTable();
            if (ddlRegPhysision.SelectedIndex == 0 && txtFromDate.Text != "" && txtToDate.Text != "")
            {
                dt = ui.FetchinControldt(@"select sum(CAST(CAST (t.fee AS NUMERIC) AS INT)) as total from token t
                 where convert(date, t.appointmentDate,103) between convert(date, '" + ui.GetSQLInject(txtFromDate.Text) + "',103) and convert(date, '" + ui.GetSQLInject(txtToDate.Text) + "',103) and t.visible = 1");
            }
            else if (ddlRegPhysision.SelectedIndex != 0 && txtFromDate.Text == "" && txtToDate.Text == "")
            {
                dt = ui.FetchinControldt(@"select sum(CAST(CAST (t.fee AS NUMERIC) AS INT)) as total from token t
                where  t.visible = 1 and t.physicianIdx = " + ui.GetSQLInject(ddlRegPhysision.SelectedValue.ToString()) + @"");
            }


            if (ddlRegPhysision.SelectedIndex == 0 && txtFromDate.Text == "" && txtToDate.Text == "")
            {
                dt = ui.FetchinControldt(@"select sum(CAST(CAST (t.fee AS NUMERIC) AS INT)) as total from token t");

            }
            else if (ddlRegPhysision.SelectedIndex != 0 && txtFromDate.Text != "" && txtToDate.Text != "")
            {
                dt = ui.FetchinControldt(@"select sum(CAST(CAST (t.fee AS NUMERIC) AS INT)) as total from token t
                where convert(date, t.appointmentDate,103) between convert(date, '" + ui.GetSQLInject(txtFromDate.Text) + "',103) and convert(date, '" + ui.GetSQLInject(txtToDate.Text) + "',103) and t.visible = 1 and t.physicianIdx = " + ui.GetSQLInject(ddlRegPhysision.SelectedValue.ToString()) + @"");
            }



            decimal Totalsalary = Convert.ToDecimal(dt.Rows[0]["total"].ToString());
            Session["totalSalary"] = Totalsalary;
            return Convert.ToString(Totalsalary);
        }

        public string totalsalinWords()
        {
            //DataTable dt = new DataTable();
            //if (ddlRegPhysision.SelectedIndex == 0 && txtFromDate.Text != "" && txtToDate.Text != "")
            //{
            //    dt = ui.FetchinControldt(@"select sum(CAST(CAST (t.fee AS NUMERIC) AS INT)) as total from token t
            //    where convert(date, t.appointmentDate,103) between convert(date, '" + ui.GetSQLInject(txtFromDate.Text) + "',103) and convert(date, '" + ui.GetSQLInject(txtToDate.Text) + "',103) and t.visible = 1");
            //}
            //else if(ddlRegPhysision.SelectedIndex != 0 && txtFromDate.Text == "" && txtToDate.Text == "")
            //{
            //    dt = ui.FetchinControldt(@"select sum(CAST(CAST (t.fee AS NUMERIC) AS INT)) as total from token t
            //    where convert(date, t.appointmentDate,103) between  convert(date,'" + ui.GetSQLInject(txtFromDate.Text) + "',103) and convert(date, '" + ui.GetSQLInject(txtToDate.Text) + "',103) and t.visible = 1 and t.physicianIdx = " + ui.GetSQLInject(ddlRegPhysision.SelectedValue.ToString()) + @"");
            //}

            //if (ddlRegPhysision.SelectedIndex == 0 && txtFromDate.Text == "" && txtToDate.Text == "")
            //{
            //    dt = ui.FetchinControldt(@"select sum(CAST(CAST (t.fee AS NUMERIC) AS INT)) as total from token t
            //    where convert(date, t.appointmentDate,103) between convert(date, '" + ui.GetSQLInject(txtFromDate.Text) + "',103) and convert(date, '" + ui.GetSQLInject(txtToDate.Text) + "',103) and t.visible = 1");
            //}
            //else if (ddlRegPhysision.SelectedIndex != 0 && txtFromDate.Text == "" && txtToDate.Text == "")
            //{
            //    dt = ui.FetchinControldt(@"select sum(CAST(CAST (t.fee AS NUMERIC) AS INT)) as total from token t
            //    where convert(date, t.appointmentDate,103) between  convert(date,'" + ui.GetSQLInject(txtFromDate.Text) + "',103) and convert(date, '" + ui.GetSQLInject(txtToDate.Text) + "',103) and t.visible = 1 and t.physicianIdx = " + ui.GetSQLInject(ddlRegPhysision.SelectedValue.ToString()) + @"");
            //}

            //string word = ConvertNumbertoWords(Convert.ToInt32(dt.Rows[0]["total"].ToString()));
            int word = int.Parse(Session["totalSalary"].ToString());
            //Session["word"] = 
            Session["total"] = word;
            string totalInWords = ConvertNumbertoWords(word);
            Session["totalInWords"] = totalInWords;
            return totalInWords;

        }

        protected void bindPhysicianForRegPatient()
        {
            try
            {
                DataTable dt = new DataTable();
                dt = ui.FetchinControldt(@"select idx, (firstName + ' ' + lastName)as fullName from users where userType = 1 and isactive= 1 and visible = 1");
                if (dt.Rows.Count > 0)
                {
                    ddlRegPhysision.DataSource = dt;
                    ddlRegPhysision.DataValueField = "idx";
                    ddlRegPhysision.DataTextField = "fullName";
                    ddlRegPhysision.DataBind();
                    ddlRegPhysision.Items.Insert(0, "All Doctors");
                }
            }
            catch (Exception ex)
            { }
        }

        public static string ConvertNumbertoWords(int number)
        {

            if (number == 0)
                return "ZERO";
            if (number < 0)
                return "minus " + ConvertNumbertoWords(Math.Abs(number));
            string words = "";

            if ((number / 1000000000) > 0)
            {
                words += ConvertNumbertoWords(number / 1000000000) + " Billion ";
                number %= 1000000000;
            }

            if ((number / 10000000) > 0)
            {
                words += ConvertNumbertoWords(number / 10000000) + " Crore ";
                number %= 10000000;
            }

            if ((number / 1000000) > 0)
            {
                words += ConvertNumbertoWords(number / 1000000) + " MILLION ";
                number %= 1000000;
            }
            if ((number / 1000) > 0)
            {
                words += ConvertNumbertoWords(number / 1000) + " THOUSAND ";
                number %= 1000;
            }
            if ((number / 100) > 0)
            {
                words += ConvertNumbertoWords(number / 100) + " HUNDRED ";
                number %= 100;
            }
            if (number > 0)
            {
                if (words != "")
                    words += "AND ";
                var unitsMap = new[] { "ZERO", "ONE", "TWO", "THREE", "FOUR", "FIVE", "SIX", "SEVEN", "EIGHT", "NINE", "TEN", "ELEVEN", "TWELVE", "THIRTEEN", "FOURTEEN", "FIFTEEN", "SIXTEEN", "SEVENTEEN", "EIGHTEEN", "NINETEEN" };
                var tensMap = new[] { "ZERO", "TEN", "TWENTY", "THIRTY", "FORTY", "FIFTY", "SIXTY", "SEVENTY", "EIGHTY", "NINETY" };

                if (number < 20)
                    words += unitsMap[number];
                else
                {
                    words += tensMap[number / 10];
                    if ((number % 10) > 0)
                        words += " " + unitsMap[number % 10];
                }
            }
            return words;
        }

        protected void btnPrint_Click(object sender, EventArgs e)
        {
            if (Session["dt"] != null)
            {
                if (Session["physitionsList"] != null && Session["physitionsList"].ToString() == "true")
                {
                    OpenNewBrowserWindow("AccountsReportPrint.aspx", this);
                }
                else if (Session["allDoctorsList"] != null && Session["allDoctorsList"].ToString() == "true")
                {
                    OpenNewBrowserWindow("allDrAccountReportPrint.aspx", this);
                }
            }
        }
        public static void OpenNewBrowserWindow(string Url, Control control)
        {
            ScriptManager.RegisterStartupScript(control, control.GetType(), "Open", "window.open('" + Url + "','','top=100px,bottom=100px,right=100px,left=100px, directories=no,menubar=no,toolbar=no,location=no,resizable=no,height=1000px,width=1500,status=no,scrollbars=yes,maximize=null,resizable=0,titlebar=no');", true);
        }
    }
}