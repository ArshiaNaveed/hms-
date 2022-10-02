using System;
using System.Collections.Generic;
using System.Configuration;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading;
using System.Web;
using System.Web.UI;


/// <summary>
/// Create by Nauman Manzoor
/// Summary description for BaseModel
/// </summary>
/// 

public delegate void CultureChanged(object Sender, string CurrentCulture);
public class BaseModel : Page
{

    private static string stDefaultDateFormat = "dd/MM/yyyy";
    private static string stDefaultTimeFormat = "HH:mm:ss";
    private string DefaultDateFormat = "dd/MM/yyyy";
    private string DefaultTimeFormat = "HH:mm:ss";
    public BaseModel()
    {
    }
    // get db connection
    private string GetDbConnection()
    {
        ConnectionStringSettings s = System.Configuration.ConfigurationManager.ConnectionStrings["connectionstring"];
        return s.ConnectionString;
    }
    public static int ApplicationUserID
    {
        set { HttpContext.Current.Session["ApplicationUserID"] = value; }
        get
        {
            if (HttpContext.Current.Session["ApplicationUserID"] != null)
            {
                return (int)HttpContext.Current.Session["ApplicationUserID"];
            }
            return 0;
        }
    }

    public static int FamilyID
    {
        set { HttpContext.Current.Session["BaseFamilyID"] = value; }
        get
        {
            if (HttpContext.Current.Session["BaseFamilyID"] != null)
            {
                return (int)HttpContext.Current.Session["BaseFamilyID"];
            }
            return 0;
        }
    }


    public static int FamilyUserID
    {
        set { HttpContext.Current.Session["FamilyUserID"] = value; }
        get
        {
            if (HttpContext.Current.Session["FamilyUserID"] != null)
            {
                return (int)HttpContext.Current.Session["FamilyUserID"];
            }
            return 0;
        }
    }
    public static int DonatorTypeID
    {
        set { HttpContext.Current.Session["DonatorTypeID"] = value; }
        get
        {
            if (HttpContext.Current.Session["DonatorTypeID"] != null)
            {
                return (int)HttpContext.Current.Session["DonatorTypeID"];
            }
            return 0;
        }
    }
    public static int MemberUserID
    {
        set { HttpContext.Current.Session["MemberUserID"] = value; }
        get
        {
            if (HttpContext.Current.Session["MemberUserID"] != null)
            {
                return (int)HttpContext.Current.Session["MemberUserID"];
            }
            return 0;
            //return 1;
        }
    }
    public static int DonatorUserID
    {
        set { HttpContext.Current.Session["DonatorUserID"] = value; }
        get
        {
            if (HttpContext.Current.Session["DonatorUserID"] != null)
            {
                return (int)HttpContext.Current.Session["DonatorUserID"];
            }
            return 0;
            //return 1;
        }
    }
    public static int HREmployeeUserID
    {
        set { HttpContext.Current.Session["HREmployeeID"] = value; }
        get
        {
            if (HttpContext.Current.Session["HREmployeeID"] != null)
            {
                return int.Parse(HttpContext.Current.Session["HREmployeeID"].ToString());
            }
            return 0;
            //return 1;
        }
    }
    public static int SupplierUserID
    {
        set { HttpContext.Current.Session["SupplierUserID"] = value; }
        get
        {
            if (HttpContext.Current.Session["SupplierUserID"] != null)
            {
                return int.Parse(HttpContext.Current.Session["SupplierUserID"].ToString());
            }
            return 0;
            //return 1;
        }
    }
    public static int UserRoleID
    {
        set { HttpContext.Current.Session["UserRoleID"] = value; }
        get
        {
            if (HttpContext.Current.Session["UserRoleID"] != null)
            {
                return (int)HttpContext.Current.Session["UserRoleID"];
            }
            return 0;
        }
    }

    private int intOnlineBranch;
    public int OnlineBranch
    {
        set { intOnlineBranch = value; }
        get
        {
            return 21;
        }
    }

    public enum UserRoles
    {
        BranchManager = 2,
        HRManager = 9,
        FinanceManager = 5,
        GeneralManager = 7
    };
    public static int UserBranchID
    {
        set { HttpContext.Current.Session["UserBranchID"] = value; }
        get
        {
            if (HttpContext.Current.Session["UserBranchID"] != null)
            {
                return (int)HttpContext.Current.Session["UserBranchID"];
            }
            return 0;
        }
    }
    public static int IsLoginUserID
    {
        set { HttpContext.Current.Session["IsCheckLoginUserID"] = value; }
        get
        {
            if (HttpContext.Current.Session["IsCheckLoginUserID"] != null)
            {
                return (int)HttpContext.Current.Session["IsCheckLoginUserID"];
            }
            return 0;
        }
    }
    public static int UserModuleID
    {
        set { HttpContext.Current.Session["UserModuleID"] = value; }
        get
        {
            if (HttpContext.Current.Session["UserModuleID"] != null)
            {
                return (int)HttpContext.Current.Session["UserModuleID"];
            }
            return 0;
        }
    }
    public static string ModuleName
    {
        set { HttpContext.Current.Session["ModuleHead"] = value; }
        get
        {
            if (HttpContext.Current.Session["ModuleHead"] != null)
            {
                return (string)HttpContext.Current.Session["ModuleHead"];
            }
            return "";
        }
    }
    public static string EmployeePassword
    {
        set { HttpContext.Current.Session["EmployeePwd"] = value; }
        get
        {
            if (HttpContext.Current.Session["EmployeePwd"] != null)
            {
                return (string)HttpContext.Current.Session["EmployeePwd"];
            }
            return "";
        }
    }
    public static string SiteLanguageCode
    {
        set { HttpContext.Current.Session["SiteLanguageCode"] = value; }
        get
        {
            if (HttpContext.Current.Session["SiteLanguageCode"] != null)
            {
                return (string)HttpContext.Current.Session["SiteLanguageCode"];
            }
            else
            {
                return "ar-SA";
            }
        }
    }

    public static string EmployeeName
    {
        set { HttpContext.Current.Session["EmployeeName"] = value; }
        get
        {
            if (HttpContext.Current.Session["EmployeeName"] != null)
            {
                return (string)HttpContext.Current.Session["EmployeeName"];
            }
            return "";
        }
    }

    public static string ApplicationUserName
    {
        set { HttpContext.Current.Session["ApplicationUserName"] = value; }
        get
        {
            if (HttpContext.Current.Session["ApplicationUserName"] != null)
            {
                return (string)HttpContext.Current.Session["ApplicationUserName"];
            }
            return "";
        }
    }
    public static bool IsActiveUser
    {
        set { HttpContext.Current.Session["IsActiveUser"] = value; }
        get
        {
            if (HttpContext.Current.Session["IsActiveUser"] != null)
            {
                return (bool)HttpContext.Current.Session["IsActiveUser"];
            }
            return false;
        }
    }
    public static bool IsSiteRTL
    {
        set
        {
            HttpContext.Current.Session["IsSiteRTL"] = value;
        }
        get
        {
            if (HttpContext.Current.Session["IsSiteRTL"] != null)
            {
                return bool.Parse(HttpContext.Current.Session["IsSiteRTL"].ToString());
            }
            else
            {
                return false;
            }
        }
    }
    public static bool IsModuleActive
    {
        set
        {
            HttpContext.Current.Session["IsModuleActive"] = value;
        }
        get
        {
            if (HttpContext.Current.Session["IsModuleActive"] != null)
            {
                return (bool)HttpContext.Current.Session["IsModuleActive"];
            }
            return false;
        }
    }
    public static bool IsMenuActive
    {
        set
        {
            HttpContext.Current.Session["IsMenuActive"] = value;
        }
        get
        {
            if (HttpContext.Current.Session["IsMenuActive"] != null)
            {
                return (bool)HttpContext.Current.Session["IsMenuActive"];
            }
            return false;
        }
    }

    public static int SettingRoundDecimal
    {
        set
        {
            HttpContext.Current.Session["SettingRoundDecimal"] = value;
        }
        get
        {
            if (HttpContext.Current.Session["SettingRoundDecimal"] != null)
            {
                return (int)HttpContext.Current.Session["SettingRoundDecimal"];
            }
            // By Default the the round decimal will be 2
            return 2;
        }
    }

    public static int ResearchTeamID
    {
        set
        {
            HttpContext.Current.Session["ResearchTeamID"] = value;
        }
        get
        {
            if (HttpContext.Current.Session["ResearchTeamID"] != null)
            {
                return (int)HttpContext.Current.Session["ResearchTeamID"];
            }
            // this is Default Value
            //return 0;
            // this is ChnageAble Value
            return 0;
        }
    }

    public static bool Scribes
    {
        set
        {
            HttpContext.Current.Session["Scribes"] = value;
        }
        get
        {
            if (HttpContext.Current.Session["Scribes"] != null)
            {
                return (bool)HttpContext.Current.Session["Scribes"];
            }
            return false;
        }
    }

    public static string CurrentDate
    {
        get
        {
            DateDisplayFormat dt = new DateDisplayFormat();
            return dt.ReturnDbFormatDate(DateTime.Now.ToString(stDefaultDateFormat));
        }
    }

    public static string CurrentTime
    {
        get
        {

            return DateTime.Now.ToString(stDefaultTimeFormat);
        }
    }

    protected void Page_Init(object sender, EventArgs e)
    {
        // if Only Family Login
        if (ApplicationUserID == 0 && FamilyUserID != 0 && MemberUserID == 0 && DonatorUserID == 0 && HREmployeeUserID == 0 && SupplierUserID == 0)
        {

        }
        // if Only Organization Login
        else if (ApplicationUserID != 0 && FamilyUserID == 0 && MemberUserID == 0 && DonatorUserID == 0 && HREmployeeUserID == 0 && SupplierUserID == 0)
        {

        }
        // if Only Donator and Member Login
        else if (ApplicationUserID == 0 && FamilyUserID == 0 && MemberUserID != 0 && DonatorUserID != 0 && HREmployeeUserID == 0 && SupplierUserID == 0)
        {

        }
        // if Only Donator and Member Login
        else if (ApplicationUserID == 0 && FamilyUserID == 0 && MemberUserID != 0 && DonatorUserID != 0 && HREmployeeUserID == 0 && SupplierUserID == 0)
        {

        }
        // if Only HREmployee Login
        else if (ApplicationUserID == 0 && FamilyUserID == 0 && MemberUserID == 0 && DonatorUserID == 0 && HREmployeeUserID != 0 && SupplierUserID == 0)
        {

        }
        // if only Supplier Login
        else if (ApplicationUserID == 0 && FamilyUserID == 0 && MemberUserID == 0 && DonatorUserID == 0 && HREmployeeUserID == 0 && SupplierUserID != 0)
        {

        }
        else
        {
            doLogout();
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

            //if (dictForm != null)
            //{
            //    if (dictForm.Count > 0)
            //    {
            //        if (dictForm.ContainsKey(arr2[0].ToLower()))
            //        {
            //            status = true;
            //        }
            //    }
            //}
            if (BaseModel.ApplicationUserID == 1)
            {
                status = true;
            }
            else
            {
                status = true;
                //if (dictForm != null)
                //{
                //    if (dictForm.Count > 0)
                //    {
                //        if (dictForm.ContainsKey(arr2[0].ToLower()))
                //        {
                //            status = true;
                //        }
                //    }
                //}
            }
            return status;

        }
        catch (Exception ex)
        {
            return false;
        }
    }

    public static bool InitializePageSecurityRole(string PageUrl)
    {
        try
        {
            bool status = false;
            string[] arr = PageUrl.Split('/');

            string[] arr2 = arr[arr.Length - 1].Split('?');

            Dictionary<string, AccessRightsSystem> dictForm = (Dictionary<string, AccessRightsSystem>)System.Web.HttpContext.Current.Session["RoleAccessDict"];

            if (dictForm != null)
            {
                if (dictForm.Count > 0)
                {
                    if (dictForm.ContainsKey(arr2[0].ToLower()) || dictForm.ContainsKey(arr2[0]))
                    {
                        status = true;
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


    public static bool InitializePageSecuritybyUserasAdd(string PageUrl)
    {
        try
        {
            bool status = false;
            string[] arr = PageUrl.Split('/');

            string[] arr2 = arr[arr.Length - 1].Split('?');

            Dictionary<string, AccessRightsSystem> dictForm = (Dictionary<string, AccessRightsSystem>)System.Web.HttpContext.Current.Session["UserAddAccessDict"];

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
            return status;

        }
        catch (Exception ex)
        {
            return false;
        }
    }

    public static bool InitializePageSecuritybyUserasEdit(string PageUrl)
    {
        try
        {
            bool status = false;
            string[] arr = PageUrl.Split('/');

            string[] arr2 = arr[arr.Length - 1].Split('?');

            Dictionary<string, AccessRightsSystem> dictForm = (Dictionary<string, AccessRightsSystem>)System.Web.HttpContext.Current.Session["UserEditAccessDict"];

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
            return status;

        }
        catch (Exception ex)
        {
            return false;
        }
    }


    public static bool InitializePageSecuritybyUserasDelete(string PageUrl)
    {
        try
        {
            bool status = false;
            string[] arr = PageUrl.Split('/');

            string[] arr2 = arr[arr.Length - 1].Split('?');

            Dictionary<string, AccessRightsSystem> dictForm = (Dictionary<string, AccessRightsSystem>)System.Web.HttpContext.Current.Session["UserDeleteAccessDict"];

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
            return status;

        }
        catch (Exception ex)
        {
            return false;
        }
    }

    public static void doLogout()
    {

        if (IsLoginUserID == 1)
        {
            HttpContext.Current.Session.Abandon();
            HttpContext.Current.Session.Clear();
            HttpContext.Current.Response.Redirect("~/ApplicationLogin.aspx");
        }
        else if (IsLoginUserID == 2)
        {
            HttpContext.Current.Session.Abandon();
            HttpContext.Current.Session.Clear();
            HttpContext.Current.Response.Redirect("~/ApplicationFamilyLogin.aspx");
        }
        else if (IsLoginUserID == 3)
        {
            HttpContext.Current.Session.Abandon();
            HttpContext.Current.Session.Clear();
            HttpContext.Current.Response.Redirect("~/ApplicationMemberLogin.aspx");
        }
        else if (IsLoginUserID == 4)
        {
            HttpContext.Current.Session.Abandon();
            HttpContext.Current.Session.Clear();
            HttpContext.Current.Response.Redirect("~/DonatorLogin.aspx");
        }
        else if (IsLoginUserID == 5)
        {
            HttpContext.Current.Session.Abandon();
            HttpContext.Current.Session.Clear();
            HttpContext.Current.Response.Redirect("~/EmpServiceLoginAR.aspx");
        }
        else
        {
            HttpContext.Current.Session.Abandon();
            HttpContext.Current.Session.Clear();
            HttpContext.Current.Response.Redirect("~/Default.aspx");

        }
    }
    public static void doAuthorizeProcess()
    {
        HttpContext.Current.Response.Redirect("~/Forms/EPortal/Autorization.aspx");
    }

    /// Language Initilaize
    /// 

    public string LastCultureName
    {
        get
        {
            string value = BaseModel.SiteLanguageCode as string;
            if (string.IsNullOrEmpty(value))
            {
                doLogout();
            }
            else
            {

            }
            return value;
        }
        set
        {
            BaseModel.SiteLanguageCode = value;
        }
    }


    protected override void InitializeCulture()
    {
        try
        {
            string LastCulure = LastCultureName;
            string language = Request["__EventTarget"];
            string languageId = LastCultureName.ToString();

            if (!string.IsNullOrEmpty(language))
            {
                if (BaseModel.IsSiteRTL == true)
                {
                    languageId = "ar-SA";
                    BaseModel.SiteLanguageCode = "ar-SA";
                    BaseModel.IsSiteRTL = true;
                }
                else if (BaseModel.IsSiteRTL == false)
                {
                    languageId = "en-US";
                    BaseModel.SiteLanguageCode = "en-US";
                    BaseModel.IsSiteRTL = false;
                }
                else
                {
                    languageId = "ar-SA";
                    BaseModel.SiteLanguageCode = "ar-SA";
                    BaseModel.IsSiteRTL = true;
                }

                //Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture(languageId);
                Thread.CurrentThread.CurrentUICulture = new CultureInfo(languageId);
            }
            else
            {
                //Thread.CurrentThread.CurrentCulture = new CultureInfo(languageId);
                Thread.CurrentThread.CurrentUICulture = CultureInfo.CreateSpecificCulture(languageId);
            }
            base.InitializeCulture();
        }
        catch (Exception)
        {
            throw;
        }

        ////string LastCulure = LastCultureName;
        ////string language = Request["__EventTarget"];
        ////string languageId = LastCultureName.ToString();
        ////Thread.CurrentThread.CurrentCulture = new CultureInfo(languageId);
        ////Thread.CurrentThread.CurrentUICulture = CultureInfo.CreateSpecificCulture(languageId);
        ////if (!string.IsNullOrEmpty(language))
        ////{
        ////    if (BaseModel.IsSiteRTL == true)
        ////    {
        ////        languageId = "ar-SA";
        ////        BaseModel.SiteLanguageCode = "ar-SA";
        ////        BaseModel.IsSiteRTL = true;
        ////    }
        ////    else if (BaseModel.IsSiteRTL == false)
        ////    {
        ////        languageId = "en-US";
        ////        BaseModel.SiteLanguageCode = "en-US";
        ////        BaseModel.IsSiteRTL = false;
        ////    }
        ////    else
        ////    {
        ////        languageId = "ar-SA";
        ////        BaseModel.SiteLanguageCode = "ar-SA";
        ////        BaseModel.IsSiteRTL = true;
        ////    }

        ////    Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture(languageId);
        ////    Thread.CurrentThread.CurrentUICulture = new CultureInfo(languageId);
        ////    //Session["lang"] = languageId;


        ////    //if (LastCulure != Thread.CurrentThread.CurrentCulture.Name)
        ////    //{
        ////    //    Session["PageCheckID"] = 0;
        ////    //}
        ////    //else
        ////    //{
        ////    //    Session["PageCheckID"] = 1;
        ////    //}
        ////}
        ////base.InitializeCulture();
    }

    /// Language Initilaize
    /// 


    // Setting for return values

    public static int ReturnInt(string Value)
    {
        return int.Parse(Value);
    }

    public static string ReturnInt(int Value)
    {
        return Value.ToString();
    }

    public static double Returndouble(string Value)
    {
        return double.Parse(Value);
    }

    public static float ReturnFloat(string Value)
    {
        return float.Parse(Value);
    }

    public static decimal Returndecimal(string Value)
    {
        return decimal.Parse(Value);
    }

    public static decimal returnRounddecimal(decimal Value)
    {
        return decimal.Round(Value, SettingRoundDecimal);
    }


    // Setting for return values 

    // Email Settings


    public static string EmailBody(string Title, string subject, string Name, string Content, string ExtraContent)
    {
        StringBuilder sb = new StringBuilder(2000, 8000);
        sb.AppendLine(" <!DOCTYPE html PUBLIC '-//W3C//DTD XHTML 1.0 Transitional//EN' 'http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd'><html xmlns='http://www.w3.org/1999/xhtml'><head> <meta http-equiv='Content-Type' content='text/html; charset=UTF-8'/> <title>" + Title + "</title> </head> ");
        //sb.AppendLine(" <style media=\"all\" type=\"text/css\"> ");
        //sb.AppendLine(" </style> ");
        sb.AppendLine("  <body leftmargin='0' marginwidth='0' topmargin='0' marginheight='0' offset='0' dir='rtl'> <center> <table bgcolor='#eaeaea' align='center' border='0' cellpadding='0' cellspacing='0' height='100%' width='100%' > <tr> <td align='center' valign='top'> <div ><br/><br/> </div><div ><table bgcolor='#ffffff' style='border:1px solid #ccc; max-width:600px;' border='0' cellpadding='0' cellspacing='0' dir='rtl'> <tr> <td align='center' valign='top'> </td></tr><tr> <td align='center' valign='top'> ");
        sb.AppendLine("  <table style='border-bottom:5px solid #005A30;' border='0' cellpadding='0' cellspacing='0' width='100%' id='templateHeader' dir='rtl'> <tr> <td valign='top' ><table border='0' cellpadding='20' cellspacing='0' width='100%'> <tr> <td align='right' ><img src='http://hr.arabsea.com/Images/logo.png' width='300' height='60'/></td></tr></table></td><td valign='top' ><table border='0' cellpadding='20' cellspacing='0' width='100%'> <tr> <td align='left'><font face='Arial, Helvetica, sans-serif'> ");
        sb.AppendLine("  " + subject + "</font></td></tr></table></td></tr></table> </td></tr><tr> <td align='center' valign='top'> <table border='0' cellpadding='15' cellspacing='0' width='100%' dir='rtl'> <tr mc:repeatable> <td align='right' valign='top'><font face='helvetica' size='+1'>مرحبا بكم " + Name + ",</font><br/> <br/> <font size='-1' face='arial' > " + Content + " </font></td></tr> ");
        if (!string.IsNullOrEmpty(ExtraContent))
        {
            sb.AppendLine("  <tr><td align='center' valign='top'> <table border='0' cellpadding='15' cellspacing='0' width='100%' > <tr mc:repeatable> <td align='left' valign='top' > <table bgcolor='#e8f0f7' width='100%' border='0' cellpadding='14' dir='rtl'> <tr> <td> " + ExtraContent + " </td></tr></table> </tr></table> </td></tr>  ");
        }
        sb.AppendLine("  </table></td></tr><tr> <td align='center' valign='top'> <table border='0' cellpadding='15' cellspacing='0' width='100%' dir='rtl'> <tr mc:repeatable> <td align='center' valign='top' > <table bgcolor='#005A30' width='100%' border='0' cellpadding='14'> <tr> <td valign='top'><table width='100%' border='0'> <tr> <td width='200'><font size='-1' color='#FFFFFF' face='Arial, Helvetica, sans-serif'>شركة بحر العرب لأنظمة المعلومات</font></td><td width='165' align='right'><table border='0' cellpadding='2'> </table></td></tr></table> </td></tr></table> ");
        sb.AppendLine("  </td></tr></table> </td></tr></table></div><br/><br/> </td></tr></table> </center> </body></html> ");
        return sb.ToString();
    }

    //By Izhar Mehmood on dated 24102021
    public static string EmailBodyLEavePermission(string Title, string subject, string Name, string Content, string ExtraContent)
    {
        StringBuilder sb = new StringBuilder(2000, 8000);
        sb.AppendLine(" <!DOCTYPE html PUBLIC '-//W3C//DTD XHTML 1.0 Transitional//EN' 'http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd'><html xmlns='http://www.w3.org/1999/xhtml'><head> <meta http-equiv='Content-Type' content='text/html; charset=UTF-8'/> <title></title> </head> ");
        //sb.AppendLine(" <style media=\"all\" type=\"text/css\"> ");
        //sb.AppendLine(" </style> ");
        sb.AppendLine("  <body leftmargin='0' marginwidth='0' topmargin='0' marginheight='0' offset='0' dir='rtl'> <center> <table bgcolor='#eaeaea' align='center' border='0' cellpadding='0' cellspacing='0' height='100%' width='100%' > <tr> <td align='center' valign='top'> <div ><br/><br/> </div><div ><table bgcolor='#ffffff' style='border:1px solid #ccc; max-width:600px;' border='0' cellpadding='0' cellspacing='0' dir='rtl'> <tr> <td align='center' valign='top'> </td></tr><tr> <td align='center' valign='top'> ");
        sb.AppendLine("  <table style='border-bottom:5px solid #005A30;' border='0' cellpadding='0' cellspacing='0' width='100%' id='templateHeader' dir='rtl'> <tr> <td valign='top' ><table border='0' cellpadding='20' cellspacing='0' width='100%'> <tr> <td align='right' ><img src='https://services.smactive.net/asishrtest/images/logo.png' width='300' height='60'/></td></tr></table></td><td valign='top' ><table border='0' cellpadding='20' cellspacing='0' width='100%'> <tr> <td align='left'><font face='Arial, Helvetica, sans-serif'> ");
        sb.AppendLine("  </font></td></tr></table></td></tr></table> </td></tr><tr> <td align='center' valign='top'> <table border='0' cellpadding='15' cellspacing='0' width='100%' dir='rtl'> <tr mc:repeatable> <td align='right' valign='top'><br/> <font face='helvetica' size='+1'> " + Title + "  <br/></font><font face='helvetica' size='+1'>مرحبا بكم " + Name + ",</font><br/> <br/> <font size='-1' face='arial' > " + Content + " </font></td></tr> ");
        if (!string.IsNullOrEmpty(ExtraContent))
        {
            sb.AppendLine("  <tr><td align='center' valign='top'> <table border='0' cellpadding='15' cellspacing='0' width='100%' > <tr mc:repeatable> <td align='left' valign='top' > <table bgcolor='#e8f0f7' width='100%' border='0' cellpadding='14' dir='rtl'> <tr> <td> " + ExtraContent + " </td></tr></table> </tr></table> </td></tr>  ");
        }
        //Content changed by Bilal- Dated 24OCT2017
        sb.AppendLine("  </table></td></tr><tr> <td align='center' valign='top'> <table border='0' cellpadding='15' cellspacing='0' width='100%' dir='rtl'> <tr mc:repeatable> <td align='center' valign='top' > <table bgcolor='#005A30' width='100%' border='0' cellpadding='14'> <tr> <td valign='top'><table width='100%' border='0'> <tr> <td width='200'><font size='-1' color='#FFFFFF' face='Arial, Helvetica, sans-serif'>شركة بحر العرب لأنظمة المعلومات</font></td><td width='165' align='right'><table border='0' cellpadding='2'> </table></td></tr></table> </td></tr></table> ");
        sb.AppendLine("  </td></tr></table> </td></tr></table></div><br/><br/> </td></tr></table> </center> </body></html> ");
        return sb.ToString();
    }

    //By Izhar Mehmood on dated 24102021
    public static bool SendEmailWithHTML(string Email, string Subject, string Title, string Name, string Content, string ExtraContent)
    {
        try
        {
            if (ConfigurationManager.AppSettings["IsEmailEnable"] == "true")
            {
                MailAddress from = new MailAddress(ConfigurationManager.AppSettings["SendEmailFrom"]);
                MailAddress to = new MailAddress(Email);
                MailMessage message = new MailMessage(from, to);
                if (ConfigurationManager.AppSettings["IsAdministratorEmailRequired"] == "true")
                {
                    MailAddress addressBCC = new MailAddress(ConfigurationManager.AppSettings["AdministratorEmail"]);
                    message.Bcc.Add(addressBCC);
                }
                else
                { }
                message.Subject = Subject;
                message.IsBodyHtml = true;
                message.Body = Content;

                SmtpClient client = new SmtpClient(ConfigurationManager.AppSettings["MailAddress"]);
                client.Port = int.Parse(ConfigurationManager.AppSettings["Port"].ToString());
                client.Credentials = new NetworkCredential(ConfigurationManager.AppSettings["AuthorizeEmail"], ConfigurationManager.AppSettings["AuthorizeEmailPassword"]);
                client.EnableSsl = true;
                client.Send(message);
                return true;
            }
            else
            {
                return false;
            }

        }
        catch
        {
            return false;
        }
    }

    public static bool SendEmail(string Email, string Subject, string Title, string Name, string Content, string ExtraContent)
    {
        try
        {
            if (ConfigurationManager.AppSettings["IsEmailEnable"] == "true")
            {
                MailAddress from = new MailAddress(ConfigurationManager.AppSettings["SendEmailFrom"]);
                MailAddress to = new MailAddress(Email);
                MailMessage message = new MailMessage(from, to);
                if (ConfigurationManager.AppSettings["IsAdministratorEmailRequired"] == "true")
                {
                    MailAddress addressBCC = new MailAddress(ConfigurationManager.AppSettings["AdministratorEmail"]);
                    message.Bcc.Add(addressBCC);
                }
                else
                { }
                message.Subject = Subject;
                message.IsBodyHtml = true;
                message.Body = EmailBody(Title, Subject, Name, Content, ExtraContent);
                //            Thread email = new Thread(delegate ()
                //{
                SmtpClient client = new SmtpClient(ConfigurationManager.AppSettings["MailAddress"]);
                client.Port = 587;
                client.Credentials = new NetworkCredential(ConfigurationManager.AppSettings["AuthorizeEmail"], ConfigurationManager.AppSettings["AuthorizeEmailPassword"]);
                client.EnableSsl = false;
                client.Send(message);
                //});
                //email.IsBackground = true;
                //email.Start();
                return true;
            }
            else
            {
                return true;
            }
        }
        catch (Exception ex)
        {
            return false ;
        }
    }

    public static string strSendEmail(string Email, string Subject, string Title, string Name, string Content, string ExtraContent)
    {
        try
        {
            if (ConfigurationManager.AppSettings["IsEmailEnable"] == "true")
            {
                MailAddress from = new MailAddress(ConfigurationManager.AppSettings["SendEmailFrom"]);
                MailAddress to = new MailAddress(Email);
                MailMessage message = new MailMessage(from, to);
                if (ConfigurationManager.AppSettings["IsAdministratorEmailRequired"] == "true")
                {
                    MailAddress addressBCC = new MailAddress(ConfigurationManager.AppSettings["AdministratorEmail"]);
                    message.Bcc.Add(addressBCC);
                }
                else
                { }
                message.Subject = Subject;
                message.IsBodyHtml = true;
                message.Body = EmailBody(Title, Subject, Name, Content, ExtraContent);
                //            Thread email = new Thread(delegate ()
                //{
                SmtpClient client = new SmtpClient(ConfigurationManager.AppSettings["MailAddress"]);
                client.Port = 587;
                client.UseDefaultCredentials = false;
                client.Credentials = new NetworkCredential(ConfigurationManager.AppSettings["AuthorizeEmail"], ConfigurationManager.AppSettings["AuthorizeEmailPassword"]);
                client.EnableSsl = false;
                client.Send(message);
                //});
                //email.IsBackground = true;
                //email.Start();
                return "True";
            }
            else
            {
                return "False";
            }
        }
        catch (Exception ex)
        {
            return ex.ToString();
        }
    }


    public static bool SendEmail(string Email, string Subject, string Title, string Name, string Content, string ExtraContent, List<Attachment> lstAttachment)
    {
        try
        {
            if (ConfigurationManager.AppSettings["IsEmailEnable"] == "true")
            {
                MailAddress from = new MailAddress(ConfigurationManager.AppSettings["SendEmailFrom"]);
                MailAddress to = new MailAddress(Email);
                MailMessage message = new MailMessage(from, to);
                if (ConfigurationManager.AppSettings["IsAdministratorEmailRequired"] == "true")
                {
                    MailAddress addressBCC = new MailAddress(ConfigurationManager.AppSettings["AdministratorEmail"]);
                    message.Bcc.Add(addressBCC);
                }
                else
                { }
                message.Subject = Subject;
                message.IsBodyHtml = true;
                message.Body = EmailBody(Title, Subject, Name, Content, ExtraContent);
                foreach (Attachment item in lstAttachment)
                {
                    message.Attachments.Add(item);
                }
                SmtpClient client = new SmtpClient(ConfigurationManager.AppSettings["MailAddress"]);
                client.Credentials = new NetworkCredential(ConfigurationManager.AppSettings["AuthorizeEmail"], ConfigurationManager.AppSettings["AuthorizeEmailPassword"]);
                client.EnableSsl = false;
                client.Send(message);
                return true;
            }
            else
            {
                return false;
            }
        }
        catch
        {
            return false;
        }
    }


    // Image Upload Properties


    public static bool CheckPaymentStatus()
    {

        if (ConfigurationManager.AppSettings["IsAutoPayment"] == "true")
        {
            return true;
        }
        else
        {
            return false;
        }
    }




    public static int ProcessTypeID
    {
        set
        {
            HttpContext.Current.Session["ProcessTypeID"] = value;
        }
        get
        {
            if (HttpContext.Current.Session["ProcessTypeID"] != null)
            {
                return (int)HttpContext.Current.Session["ProcessTypeID"];
            }
            // By Default the the round decimal will be 2
            return 0;
        }
    }
    public static int ProcessID
    {
        set
        {
            HttpContext.Current.Session["ProcessID"] = value;
        }
        get
        {
            if (HttpContext.Current.Session["ProcessID"] != null)
            {
                return (int)HttpContext.Current.Session["ProcessID"];
            }
            // By Default the the round decimal will be 2
            return 0;
        }
    }
    public static int SettingArchiveTypeID
    {
        set
        {
            HttpContext.Current.Session["SettingArchiveTypeID"] = value;
        }
        get
        {
            if (HttpContext.Current.Session["SettingArchiveTypeID"] != null)
            {
                return (int)HttpContext.Current.Session["SettingArchiveTypeID"];
            }
            // By Default the the round decimal will be 2
            return 0;
        }
    }
    public static string TableName
    {
        set
        {
            HttpContext.Current.Session["TableName"] = value;
        }
        get
        {
            if (HttpContext.Current.Session["TableName"] != null)
            {
                return (string)HttpContext.Current.Session["TableName"];
            }
            // By Default the the round decimal will be 2
            return "";
        }
    }
    public static string TableNameAR
    {
        set
        {
            HttpContext.Current.Session["TableNameAR"] = value;
        }
        get
        {
            if (HttpContext.Current.Session["TableNameAR"] != null)
            {
                return (string)HttpContext.Current.Session["TableNameAR"];
            }
            // By Default the the round decimal will be 2
            return "";
        }
    }

    public static string FieldName
    {
        set
        {
            HttpContext.Current.Session["FieldName"] = value;
        }
        get
        {
            if (HttpContext.Current.Session["FieldName"] != null)
            {
                return (string)HttpContext.Current.Session["FieldName"];
            }
            // By Default the the round decimal will be 2
            return "";
        }
    }

    public void RetunErrorMessage(int ID)
    {
        if (ID == 1)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "scr", "showSuccessToast('يرجى إدخال التفاصيل أولاً');", true);
        }
    }


    public static int SetFamilyID
    {
        set { HttpContext.Current.Session["SetFamilyID"] = value; }
        get
        {
            if (HttpContext.Current.Session["SetFamilyID"] != null)
            {
                return (int)HttpContext.Current.Session["SetFamilyID"];
            }
            return 0;
        }
    }

    public static int SetProcessTypeID
    {
        set { HttpContext.Current.Session["SetProcessTypeID"] = value; }
        get
        {
            if (HttpContext.Current.Session["SetProcessTypeID"] != null)
            {
                return (int)HttpContext.Current.Session["SetProcessTypeID"];
            }
            return 0;
        }
    }

    public static int SetProcessID
    {
        set { HttpContext.Current.Session["SetProcessID"] = value; }
        get
        {
            if (HttpContext.Current.Session["SetProcessID"] != null)
            {
                return (int)HttpContext.Current.Session["SetProcessID"];
            }
            return 0;
        }
    }

    public static int SetSettingArchiveTypeID
    {
        set { HttpContext.Current.Session["SetSettingArchiveTypeID"] = value; }
        get
        {
            if (HttpContext.Current.Session["SetSettingArchiveTypeID"] != null)
            {
                return (int)HttpContext.Current.Session["SetSettingArchiveTypeID"];
            }
            return 0;
        }
    }

    //By Izhar Mehmood on dated 24102021
    public static bool SendVacationRequestStatus(string Email, string Subject, string Title, string Name, string Content, string ExtraContent)
    {
        try
        {
            if (ConfigurationManager.AppSettings["IsEmailEnable"] == "true")
            {
                MailAddress from = new MailAddress(ConfigurationManager.AppSettings["SendEmailFrom"]);
                MailAddress to = new MailAddress(Email);
                MailMessage message = new MailMessage(from, to);
                if (ConfigurationManager.AppSettings["IsAdministratorEmailRequired"] == "true")
                {
                    MailAddress addressBCC = new MailAddress(ConfigurationManager.AppSettings["AdministratorEmail"]);
                    message.Bcc.Add(addressBCC);
                }
                else
                { }
                message.Subject = Subject;
                message.IsBodyHtml = true;
                message.Body = EmailBodyLEavePermission(Title, Subject, Name, Content, ExtraContent);
                Thread email = new Thread(delegate ()
                {
                    SmtpClient client = new SmtpClient(ConfigurationManager.AppSettings["MailAddress"]);
                    client.Port = int.Parse(ConfigurationManager.AppSettings["Port"].ToString());
                    client.Credentials = new NetworkCredential(ConfigurationManager.AppSettings["AuthorizeEmail"], ConfigurationManager.AppSettings["AuthorizeEmailPassword"]);
                    client.EnableSsl = true;
                    client.Send(message);
                });
                email.IsBackground = true;
                email.Start();
                return true;
            }
            else
            {
                return false;
            }
        }
        catch
        {
            return false;
        }
    }


    public static string CallEncrypt(string str)
    {
        QueryCipherT qc = new QueryCipherT();
        HttpContext.Current.Session["EncID"] = qc.Encrypt(str).ToString();
        return HttpContext.Current.Session["EncID"].ToString();
    }

    public static string CallDecrypt(string str)
    {
        QueryCipherT qc = new QueryCipherT();
        HttpContext.Current.Session["DecrID"] = qc.Decrypt(str).ToString();
        return HttpContext.Current.Session["DecrID"].ToString();
    }

}