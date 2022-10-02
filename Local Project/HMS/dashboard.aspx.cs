using System;
using System.Data;

namespace HMS
{
    public partial class dashboard : System.Web.UI.Page
    {
        Utilities ui = new Utilities();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                fillDashboardCounter();
                getDoctors();
                getPatientSAndType();
                //BarChart1.CategoriesAxis = "";
                //getDTreatment();
                getDoctorPerformance();
            }
        }

        protected void fillDashboardCounter()
        {
            try
            {
                DataTable dt = new DataTable();
                dt = ui.FetchinControldt(@" 
                                            --For Doctors Count
                                            select count(u.idx) as countNumber from users u inner join facultySchedule f on f.userIdx = u.idx where u.visible = 1 and u.userType = 1 and f.[day] = DATENAME(dw, GETDATE())
                                            --For Patients Count
                                            union all
                                            select count(p.idx) as countNumber from patentRegistration p where p.visible = 1 and Convert(date, p.creationDate, 103) = Convert(date, getdate(), 103)
                                            -- For Today's Total Appointment
                                            union all
                                            select count(t.tokenNumber) as countNumber from token t
                                            where  Convert(date, t.creationDate, 103) = Convert(date, getdate(), 103) and t.visible = 1
                                            -- For Today's Total Pending Appointment
                                            union all
                                            select count(t.tokenNumber) as countNumber from token t
                                            where  Convert(date, t.creationDate, 103) = Convert(date, getdate(), 103) and t.visible = 1 and status = 0");
                if (dt.Rows.Count > 0)
                {
                    lblNewPatients.Text = dt.Rows[1]["countNumber"].ToString();
                    lblOurDoctors.Text = dt.Rows[0]["countNumber"].ToString();
                    lblTodaysAppointment.Text = dt.Rows[2]["countNumber"].ToString();
                    lblPendingAppointment.Text = dt.Rows[3]["countNumber"].ToString();
                }
            }
            catch (Exception ex)
            { }
        }

        protected void getDoctors()
        {
            try
            {
                DataTable dt = new DataTable();
                dt = ui.FetchinControldt(@"select row_number() over (order by u.idx) as sn, u.idx, (u.firstName + ' ' + u.lastName) as doctorName, 
                (f.fromTime + ' - ' + f.toTime) as doctorTime, s.specialty, isnull(fe.fee,0) as fee,
                case when f.[day] = DATENAME(dw, GETDATE()) then 'Available' else 'Not Available' end as DoctorStatus
                from users u
                inner join facultySchedule f on f.userIdx = u.idx
                left join specialty s on s.idx = u.specialityIdx
                left join fee fe on fe.doctorIdx = u.idx
                where u.visible = 1 and u.userType = 1 and f.[day] = DATENAME(dw, GETDATE())");
                if (dt.Rows.Count > 0)
                {
                    rptDoctors.DataSource = dt;
                    rptDoctors.DataBind();
                }
                else
                {
                    rptDoctors.DataSource = null;
                    rptDoctors.DataBind();
                }

            }
            catch (Exception ex)
            { }
        }

        protected void getDoctorPerformance()
        {
            try
            {
                DataTable dt = new DataTable();
                if (Session["appUserId"].ToString() == "1")
                {
                    dt = ui.FetchinControldt(@"select 
                    convert(varchar(50),((select Count(*)  from token tn where tn.visible = 1 and tn.status = 1   and 
                    tn.appointmentDate = convert(nvarchar(10),DateAdd(day, 0, GetDate()),103) and tn.physicianIdx=s.idx))) as day7,
                    s.firstName from users s where s.visible = 1 and s.idx <> 1 and s.userType = 1");
                }
                else
                {
                    dt = ui.FetchinControldt(@"select 
                    convert(varchar(50),((select Count(*)  from token tn where tn.visible = 1 and tn.status = 1   and 
                    tn.appointmentDate = convert(nvarchar(10),DateAdd(day, 0, GetDate()),103) and tn.physicianIdx=s.idx))) as day7,
                    s.firstName from users s where s.visible = 1 and s.idx = " + Session["appUserId"].ToString());
                }
                    
                if (dt.Rows.Count > 0)
                {
                    string category = "";
                    decimal[] values = new decimal[dt.Rows.Count];
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        category = category + "," + dt.Rows[i]["firstName"].ToString();
                        values[i] = Convert.ToDecimal(dt.Rows[i]["day7"]);
                    }
                    AreaChart1.CategoriesAxis = category.Remove(0, 1);
                    AreaChart1.Series.Add(new AjaxControlToolkit.AreaChartSeries { Data = values, AreaColor = "#3dc0f4", Name = "Doctor(s) Name" });
                }
            }
            catch (Exception ex)
            { }
        }

        protected void getPatientSAndType()
        {
            try
            {
                DataTable dt = new DataTable();
                dt = ui.FetchinControldt(@"select count(p.idx) as countVal, 'Male Kid' as Category from patentRegistration p where p.visible = 1 and p.age < 18 and p.gender = 1
                union all
                select count(p.idx) as countVal, 'Female Kid' as Category from patentRegistration p where p.visible = 1 and p.age < 18 and p.gender = 2
                union all
                select count(p.idx) as countVal, 'Male' as Category from patentRegistration p where p.visible = 1 and p.age >= 18 and p.gender = 1
                union all
                select count(p.idx) as countVal, 'Female' as Category from patentRegistration p where p.visible = 1 and p.age >= 18 and p.gender = 2");
                if (dt.Rows.Count > 0)
                {
                    foreach (DataRow row in dt.Rows)
                    {
                        patientCountType.PieChartValues.Add(new AjaxControlToolkit.PieChartValue
                        {
                            Category = row["Category"].ToString(),
                            Data = Convert.ToDecimal(row["countVal"])
                        });
                    }
                }
            }
            catch (Exception ex)
            { }
        }

        //protected void getDTreatment()
        //{
        //    try
        //    {
        //        DataTable dt = new DataTable();
        //        dt = ui.FetchinControldt(@"select 
        //        convert(varchar(50),((select Count(*)  from token tn where tn.visible = 1 and tn.status = 1   and tn.appointmentDate = convert(nvarchar(10),DateAdd(day, 0, GetDate()),103) and tn.specialityIdx=s.idx))) as day7,
        //        s.specialty from specialty s");

        //        if (dt.Rows.Count > 0)
        //        {
        //            string[] x = new string[dt.Rows.Count];
        //            decimal[] y = new decimal[dt.Rows.Count];
        //            for (int i = 0; i < dt.Rows.Count; i++)
        //            {
        //                x[i] = dt.Rows[i]["specialty"].ToString();
        //                y[i] = Convert.ToInt32(dt.Rows[i]["day1"]);


        //            }
        //            BarChart1.Series.Add(new AjaxControlToolkit.BarChartSeries { Data = y });

        //            BarChart1.CategoriesAxis = string.Join(",", x);
                   
        //        }
        //    }
        //    catch (Exception ex)
        //    { }
        //}

    }
}