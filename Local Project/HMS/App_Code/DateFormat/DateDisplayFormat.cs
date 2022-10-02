using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Created By Nauman Manzoor
/// Summary description for DateDisplayFormat
/// 22/03/2015
/// </summary>
public class DateDisplayFormat
{
    private bool IsHijri;
    private bool IsGregorian;
    string DefaultFormat = "dd/MM/yyyy";
    DateFormatLibrary DFL = new DateFormatLibrary();
    public DateDisplayFormat()
    {
    }
    public string ReturnDbFormatDate(string date)
    {
        try
        {
            string returnDate = "";
            bool IsHijri = DFL.IsHijri(date);
            if (IsHijri == true)
            {
                returnDate = DFL.HijriToGreg(date, DefaultFormat);
            }
            else
            {
                returnDate = DFL.FormatGreg(date, DefaultFormat);
            }
            return returnDate;
        }
        catch
        {
            return "";
        }
    }

    public string ReturnDatebyCulture(string date)
    {
        try
        {
            string returnDate = "";
            if (BaseModel.IsSiteRTL == true)
            {
                returnDate = GetHijriOnly(date);
            }
            else
            {
                bool IsHijri = DFL.IsHijri(date);
                if (IsHijri == true)
                {
                    returnDate = DFL.HijriToGreg(date, DefaultFormat);
                }
                else
                {
                    returnDate = DFL.FormatGreg(date, DefaultFormat);
                }
            }

            return returnDate;
        }
        catch
        {
            return "";
        }
    }
    public string CheckAndReturnExpectedDate(string date)
    {
        try
        {
            string returnDate = "";
            bool IsHijri = DFL.IsHijri(date);
            if (IsHijri == true)
            {
                returnDate = DFL.HijriToGreg(date, DefaultFormat);
            }
            else
            {
                returnDate = DFL.GregToHijri(date, DefaultFormat);
            }
            return returnDate;
        }
        catch
        {
            return "";
        }
    }


    public string GetHijriOnly(string date)
    {
        try
        {
            string returnDate = "";
            bool IsHijri = DFL.IsHijri(date);
            if (IsHijri == true)
            {
                returnDate = DFL.FormatHijri(date, DefaultFormat);
            }
            else
            {
                returnDate = DFL.GregToHijri(date, DefaultFormat);
            }
            return returnDate;
        }
        catch
        {
            return "";
        }
    }

    public int ReturnDaysDifference(string date, string date2)
    {
        try
        {
            string Date1 = "";
            string Date2 = "";

            bool IsHijri1 = DFL.IsHijri(date);
            bool IsHijri2 = DFL.IsHijri(date2);
            if (IsHijri == true)
            {
                Date1 = DFL.HijriToGreg(date, DefaultFormat);

            }
            else
            {
                Date1 = date;
            }

            if (IsHijri2 == true)
            {

                Date2 = DFL.HijriToGreg(date2, DefaultFormat);
            }
            else
            {
                Date2 = date2;
            }

            int Days = 0;

            Days = DFL.DaysDiffernce(Date1, Date2);
            return Days;
        }
        catch
        {
            return 0;
        }
    }

    public DateTime ReturnDateTime(string Value)
    {
        if(!string.IsNullOrEmpty(Value))
        {
           return  DFL.GetDateTime(Value, DefaultFormat);
        }
        else
        {
            return DateTime.Now;
        }
    }

}