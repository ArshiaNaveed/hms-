using System;
using System.Data;
using System.Web.Services;

namespace HMS
{
    public partial class HMS : System.Web.UI.MasterPage
    {
        Utilities ui = new Utilities();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["theme"] != null)
            {
                MainBody.Attributes.Remove("class");
                string theme = Session["theme"].ToString();
                MainBody.Attributes.Add("class", theme);
            }

            if (!IsPostBack)
            {
               
                if (Session["appUserId"] != null)
                {
                    lblUserName.Text = Session["appUserName"].ToString();
                    lblDesignation.Text = Session["appDesignation"].ToString();
                    lblEmailId.Text = Session["appEmail"].ToString();
                    if (Session["appUserImage"] != null && Session["appUserImage"].ToString() != "")
                    {
                        imgFaculty.ImageUrl = "~/userImages/" + Session["appUserImage"].ToString();
                    }
                    else
                    {
                        imgFaculty.ImageUrl = "assets/images/noImage.jpg";
                    }


                }
                else
                {
                    Response.Redirect("logout.aspx");
                }

                checkModuleAccess();
                getAppointmentForRep();

            }

            

            if (Session["tretment"] != null)
            {
                if (Session["tretment"].ToString() == "1")
                {
                    appointmentsNumber.Visible = true;
                    totalAppointments.Visible = true;
                    GetTokenNumbers();
                    getTotalTokens();
                }
                else
                {
                    appointmentsNumber.Visible = false;
                    totalAppointments.Visible = false;
                }
            }

            

        }

        protected void checkModuleAccess()
        {
            try
            {
                int tretment = 0;
                int ms = 0;
                if (Session["appUserId"].ToString() == "1")
                {
                    tretment = 1;
                    ms = 1;
                    liAppointmentDetail.Visible = true;
                }
                else
                {
                    DataTable dt1 = new DataTable();
                    dt1 = ui.FetchinControldt(@"select idx from Roles where useridx = " + Session["appUserId"].ToString() + " and pageUrl = 10");
                    if (dt1.Rows.Count > 0)
                    {
                        tretment = 1;
                    }
                    else
                    {
                        tretment = 0;
                    }

                    DataTable dt2 = new DataTable();
                    dt2 = ui.FetchinControldt(@"select idx from Roles where useridx = " + Session["appUserId"].ToString() + " and pageUrl = 8 or pageUrl = 10");
                    if (dt2.Rows.Count > 0)
                    {
                        ms = 1;
                        liAppointmentDetail.Visible = true;
                    }
                    else
                    {
                        ms = 0;
                        liAppointmentDetail.Visible = false;
                    }
                    Session["tretment"] = tretment;
                    Session["ms"] = ms;
                }
            }
            catch (Exception ex)
            { }
        }

        protected void getTotalTokens()
        {
            try
            {
                DataTable dt = new DataTable();
                dt = ui.FetchinControldt(@"select COUNT(t.idx) as totalTokens
                from token t
                where  Convert(date, t.appointmentDate, 103) = Convert(date, getdate(), 103) and physicianIdx = " + Session["appUserId"].ToString() + @" 
                and t.visible = 1 and t.status = 0");
                if (dt.Rows.Count > 0)
                {
                    lblTotalTokens.Text = dt.Rows[0]["totalTokens"].ToString();
                }
                else
                {
                    lblTotalTokens.Text = "0";
                }
            }
            catch (Exception ex)
            { }
        }

        protected void GetTokenNumbers()
        {
            try
            {
                DataTable dt = new DataTable();
                dt = ui.FetchinControldt(@"select top 1 t.idx as tokenIdx, t.tokenNumber
                from token t
                where  Convert(date, t.appointmentDate, 103) = Convert(date, getdate(), 103) and physicianIdx = " + Session["appUserId"].ToString() + @" and t.visible = 1 and t.status = 0
                order by t.tokenNumber asc");
                if (dt.Rows.Count > 0)
                {
                    lblTokenNumber.Text = dt.Rows[0]["tokenNumber"].ToString();
                }
                else
                {
                    lblTokenNumber.Text = "0";
                }
            }
            catch (Exception ex)
            { }
        }

        protected void getAppointmentForRep()
        {
            try
            {
                DataTable dt = new DataTable();

                if (Session["appUserId"].ToString() == "1")
                {
                    dt = ui.FetchinControldt(@"select COUNT(t.idx) as totalTokens, (u.firstName + ' ' + u.lastName) as doctorName
                        from token t
                        inner join users u on u.idx = t.physicianIdx
                        where  Convert(date, t.appointmentDate, 103) = Convert(date, getdate(), 103) and 
                        t.visible = 1 and t.status = 0 
                        group by firstName, u.lastName");
                }
                else
                {
                    DataTable dt1 = new DataTable();
                    dt1 = ui.FetchinControldt(@"select idx from Roles where useridx = " + Session["appUserId"].ToString() + " and pageUrl = 8");
                    if (dt1.Rows.Count > 0)
                    {
                        dt = ui.FetchinControldt(@"select COUNT(t.idx) as totalTokens, (u.firstName + ' ' + u.lastName) as doctorName
                        from token t
                        inner join users u on u.idx = t.physicianIdx
                        where  Convert(date, t.appointmentDate, 103) = Convert(date, getdate(), 103) and 
                        t.visible = 1 and t.status = 0 
                        group by firstName, u.lastName");
                    }
                    else
                    {
                        dt = ui.FetchinControldt(@"select COUNT(t.idx) as totalTokens, (u.firstName + ' ' + u.lastName) as doctorName
                        from token t
                        inner join users u on u.idx = t.physicianIdx
                        where  Convert(date, t.appointmentDate, 103) = Convert(date, getdate(), 103) and 
                        t.visible = 1 and t.status = 0 and t.physicianIdx = " + Session["appUserId"].ToString() + @"
                        group by firstName, u.lastName");
                    }
                }

                if (dt.Rows.Count > 0)
                {
                    rptAppointment.DataSource = dt;
                    rptAppointment.DataBind();
                }
                else
                {
                    rptAppointment.DataSource = "";
                    rptAppointment.DataBind();
                }
            }
            catch (Exception ex)
            { }
        }

        //protected void Timer1_Tick(object sender, EventArgs e)
        //{
        //    if (Session["tretment"] != null)
        //    {
        //        if (Session["tretment"].ToString() == "1")
        //        {
        //            appointmentsNumber.Visible = true;
        //            totalAppointments.Visible = true;
        //            GetTokenNumbers();
        //            getTotalTokens();
        //        }
        //        else
        //        {
        //            appointmentsNumber.Visible = false;
        //            totalAppointments.Visible = false;
        //        }
        //    }
        //}

        //protected void Timer2_Tick(object sender, EventArgs e)
        //{
        //    getAppointmentForRep();
        //}

        protected void applyTheme()
        {
            MainBody.Attributes.Remove("class");
            string theme = Session["theme"].ToString();
            MainBody.Attributes.Add("class", theme);
        }

        protected void ThemeChangePink(object sender, EventArgs e)
        {
            Session["theme"] = null;
            Session["theme"] = "theme-pink";
            applyTheme();
        }
        protected void ThemeChangepu(object sender, EventArgs e)
        {
            Session["theme"] = null;
            Session["theme"] = "theme-purple";
            applyTheme();
        }
        protected void ThemeChangedeeppu(object sender, EventArgs e)
        {
            Session["theme"] = null;
            Session["theme"] = "theme-deep-purple";
            applyTheme();
        }
        protected void ThemeChangered(object sender, EventArgs e)
        {
            Session["theme"] = null;
            Session["theme"] = "theme-red";
            applyTheme();
        }
        protected void ThemeChangeindigo(object sender, EventArgs e)
        {
            Session["theme"] = null;
            Session["theme"] = "theme-indigo";
            applyTheme();
        }
        protected void ThemeChangeblue(object sender, EventArgs e)
        {
            Session["theme"] = null;
            Session["theme"] = "theme-blue";
            applyTheme();
        }
        protected void ThemeChangelb(object sender, EventArgs e)
        {
            Session["theme"] = null;
            Session["theme"] = "theme-light-blue";
            applyTheme();
        }
        protected void ThemeChangecyan(object sender, EventArgs e)
        {
            Session["theme"] = null;
            Session["theme"] = "theme-cyan";
            applyTheme();
        }
        protected void ThemeChangeteal(object sender, EventArgs e)
        {
            Session["theme"] = null;
            Session["theme"] = "theme-teal";
            applyTheme();
        }
        protected void ThemeChangegreen(object sender, EventArgs e)
        {
            Session["theme"] = null;
            Session["theme"] = "theme-green";
            applyTheme();
        }
        protected void ThemeChangelg(object sender, EventArgs e)
        {
            Session["theme"] = null;
            Session["theme"] = "theme-light-green";
            applyTheme();
        }
        protected void ThemeChangelime(object sender, EventArgs e)
        {
            Session["theme"] = null;
            Session["theme"] = "theme-lime";
            applyTheme();
        }
        protected void ThemeChangeyellow(object sender, EventArgs e)
        {
            Session["theme"] = null;
            Session["theme"] = "theme-yellow";
            applyTheme();
        }
        protected void ThemeChangeamber(object sender, EventArgs e)
        {
            Session["theme"] = null;
            Session["theme"] = "theme-amber";
            applyTheme();
        }
        protected void ThemeChangeorange(object sender, EventArgs e)
        {
            Session["theme"] = null;
            Session["theme"] = "theme-orange";
            applyTheme();
        }
        protected void ThemeChangedo(object sender, EventArgs e)
        {
            Session["theme"] = null;
            Session["theme"] = "theme-deep-orange";
            applyTheme();
        }
        protected void ThemeChangebrown(object sender, EventArgs e)
        {
            Session["theme"] = null;
            Session["theme"] = "theme-brown";
            applyTheme();
        }
        protected void ThemeChangegrey(object sender, EventArgs e)
        {
            Session["theme"] = null;
            Session["theme"] = "theme-grey";
            applyTheme();
        }
        protected void ThemeChangebg(object sender, EventArgs e)
        {
            Session["theme"] = null;
            Session["theme"] = "theme-blu-grey";
            applyTheme();
        }
        protected void ThemeChangeblack(object sender, EventArgs e)
        {
            Session["theme"] = null;
            Session["theme"] = "theme-black";
            applyTheme();
        }
        [WebMethod]
        public static bool IsUserAvailable(string username)
        {
            Utilities uit = new Utilities();
            DataTable dt = new DataTable();
            dt = uit.FetchinControldt(@"select idx from users where loginId = '" + username + "' and visible = 1");
            if (dt.Rows.Count > 0)
                return false;
            else
                return true;
        }

    }
}