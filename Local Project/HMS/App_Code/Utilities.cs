using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Data.Common;
using System.Collections;
using System.Text;
using System.Net.Mail;


using Microsoft.Practices.EnterpriseLibrary.Data;
using System.Data.SqlClient;

using Microsoft.Practices.EnterpriseLibrary.Common.Configuration;



/// <summary>
/// Atif Ahmed
/// Summary description for Utilities
/// </summary>
/// 

public class Utilities
{
    #region Private
    private Database db = null;
    #endregion

    public Utilities()
    {
        db = DatabaseFactory.CreateDatabase("connectionstring");
    }

    public DataSet FetchinCombo(string strQuery)
    {

        DbCommand dbCommand = db.GetSqlStringCommand(strQuery);

        return db.ExecuteDataSet(dbCommand);
    }

    public DataSet FetchinComboPara(string strQuery, string Para)
    {

        DbCommand dbCommand = db.GetSqlStringCommand(strQuery);
        dbCommand.Parameters.Add(new SqlParameter("@para", Para));
        return db.ExecuteDataSet(dbCommand);
    }
    //Added Faisal dated:08/07/2019
    public DataSet FetchinComboPara3(string strQuery, string Para1, string Para2, string Para3)
    {

        DbCommand dbCommand = db.GetSqlStringCommand(strQuery);
        dbCommand.Parameters.Add(new SqlParameter("@Para1", Para1));
        dbCommand.Parameters.Add(new SqlParameter("@Para2", Para2));
        dbCommand.Parameters.Add(new SqlParameter("@Para3", Para3));
        return db.ExecuteDataSet(dbCommand);
    }

    public DataSet FetchinControl(string strQuery)
    {

        DbCommand dbCommand = db.GetSqlStringCommand(strQuery);
        //dbCommand.CommandTimeout = 600;

        return db.ExecuteDataSet(dbCommand);
    }

    public DataTable FetchinControldt(string strQuery)
    {
        try
        {
            DbCommand dbCommand = db.GetSqlStringCommand(strQuery);
            return db.ExecuteDataSet(dbCommand).Tables[0];
        }
        catch
        {
            return null;
        }
    }


    public DataTable FetchinControldtSP(string strQuery, int Param)
    {
        try
        {
            DbCommand dbCommand = db.GetStoredProcCommand(strQuery);
            dbCommand.Parameters.Add(new SqlParameter("@param", Param));
            return db.ExecuteDataSet(dbCommand).Tables[0];
        }
        catch (Exception ex)
        {
            return null;
        }
    }

    public DataTable FetchinControldtPara(string strQuery, string Param)
    {
        try
        {
            DbCommand dbCommand = db.GetSqlStringCommand(strQuery);
            dbCommand.Parameters.Add(new SqlParameter("@param", Param));
            return db.ExecuteDataSet(dbCommand).Tables[0];
        }
        catch
        {
            return null;
        }
    }

    public DataTable FetchinControldtParams(string strQuery, string Param, string Param2)
    {

        try
        {
            DbCommand dbCommand = db.GetSqlStringCommand(strQuery);
            if (Param != null)
                dbCommand.Parameters.Add(new SqlParameter("@param", Param));
            if (Param2 != null)
                dbCommand.Parameters.Add(new SqlParameter("@param2", Param2));
            return db.ExecuteDataSet(dbCommand).Tables[0];
        }
        catch
        {
            return null;
        }
    }

    public DataTable FetchinControldtParams2(string strQuery, string Param, string Param2)
    {

        try
        {
            DbCommand dbCommand = db.GetSqlStringCommand(strQuery);
            if (Param != null)
                dbCommand.Parameters.Add(new SqlParameter("@param", Param));
            if (Param2 != null)
                dbCommand.Parameters.Add(new SqlParameter("@param2", Param2));
            return db.ExecuteDataSet(dbCommand).Tables[0];
        }
        catch
        {
            return null;
        }
    }
    // change by faisal dated: 08/07/2019
    public DataTable FetchinControldtParams3(string strQuery, string Param1, string Param2, string Param3)
    {

        try
        {
            DbCommand dbCommand = db.GetSqlStringCommand(strQuery);
            if (Param1 != null)
                dbCommand.Parameters.Add(new SqlParameter("@param1", Param1));
            if (Param2 != null)
                dbCommand.Parameters.Add(new SqlParameter("@param2", Param2));
            if (Param3 != null)
                dbCommand.Parameters.Add(new SqlParameter("@param3", Param3));
            return db.ExecuteDataSet(dbCommand).Tables[0];
        }
        catch
        {
            return null;
        }
    }

    public DataTable FetchinControldtWithSetCmndTimeOut(string strQuery)
    {
        try
        {
            DbCommand dbCommand = db.GetSqlStringCommand(strQuery);
            dbCommand.CommandTimeout = 420;
            return db.ExecuteDataSet(dbCommand).Tables[0];
        }
        catch
        {
            return null;
        }
    }

    public DataTable FetchinControldtWithSetCmndTimeOutPara(string strQuery, string Param)
    {
        try
        {
            DbCommand dbCommand = db.GetSqlStringCommand(strQuery);
            dbCommand.Parameters.Add(new SqlParameter("@param", Param));
            dbCommand.CommandTimeout = 420;
            return db.ExecuteDataSet(dbCommand).Tables[0];
        }
        catch
        {
            return null;
        }
    }

    public DataSet FetchinControlWithSetCmndTimeOut(string strQuery)
    {
        DbCommand dbCommand = db.GetSqlStringCommand(strQuery);
        dbCommand.CommandTimeout = 420;
        return db.ExecuteDataSet(dbCommand);
    }

    public static DataColumn AddDataColumn(string columnName, Type type, string header)
    {
        DataColumn dt = new DataColumn();
        dt.ColumnName = columnName;
        dt.DataType = type;
        dt.Caption = header;
        return dt;

    }

    //public int GetMaxPrimaryKey(string strQuery, string strFieldName)
    //{

    //    DataSet dsMaxItem = new DataSet();
    //    int MaxItem;

    //    DbCommand dbCommand = db.GetSqlStringCommand(strQuery);

    //    dsMaxItem = db.ExecuteDataSet(dbCommand);

    //    MaxItem = int.Parse(dsMaxItem.Tables[0].Rows[0][strFieldName].ToString());

    //    return MaxItem + 1;

    //}

    public int GetMaxPrimaryKey(string strQuery, string strFieldName)
    {

        DataSet dsMaxItem = new DataSet();
        int MaxItem;

        DbCommand dbCommand = db.GetSqlStringCommand(strQuery);

        dsMaxItem = db.ExecuteDataSet(dbCommand);

        if (dsMaxItem.Tables[0].Rows[0][strFieldName] != DBNull.Value)
        {
            MaxItem = int.Parse(dsMaxItem.Tables[0].Rows[0][strFieldName].ToString());
            return MaxItem + 1;
        }
        else
            return 1;

    }


    public bool ExistorNot(string strQuery)
    {
        DataSet dsCheck = new DataSet();

        DbCommand dbCommand = db.GetSqlStringCommand(strQuery);

        dsCheck = db.ExecuteDataSet(dbCommand);

        if (dsCheck.Tables[0].Rows.Count > 0) // available
        {
            return false;
        }
        else
        {
            return true;
        }

    }

    /// <summary>
    /// Exist or Not with True if exist
    /// </summary>
    /// <param name="strQuery"></param>
    /// <returns></returns>
    public bool ExistorNotNew(string strQuery)
    {
        DataSet dsCheck = new DataSet();

        DbCommand dbCommand = db.GetSqlStringCommand(strQuery);

        dsCheck = db.ExecuteDataSet(dbCommand);

        if (dsCheck.Tables[0].Rows.Count > 0) // available
        {
            return true;
        }
        else
        {
            return false;
        }

    }

    public bool ExistorNotNewParam(string strQuery, string Value)
    {
        DataSet dsCheck = new DataSet();

        DbCommand dbCommand = db.GetSqlStringCommand(strQuery);
        dbCommand.Parameters.Add(new SqlParameter("@Value", Value));

        dsCheck = db.ExecuteDataSet(dbCommand);

        if (dsCheck.Tables[0].Rows.Count > 0) // available
        {
            return true;
        }
        else
        {
            return false;
        }

    }

    //By Izhar Mehmood
    public bool ExistorNotNewParam2(string strQuery, string Value1, string Value2)
    {
        DataSet dsCheck = new DataSet();

        DbCommand dbCommand = db.GetSqlStringCommand(strQuery);
        dbCommand.Parameters.Add(new SqlParameter("@Value1", Value1));
        dbCommand.Parameters.Add(new SqlParameter("@Value2", Value2));

        dsCheck = db.ExecuteDataSet(dbCommand);

        if (dsCheck.Tables[0].Rows.Count > 0) // available
        {
            return true;
        }
        else
        {
            return false;
        }

    }

    //By Izhar Mehmood
    public bool ExistorNotNewParam3(string strQuery, string Value1, string Value2, string Value3)
    {
        DataSet dsCheck = new DataSet();

        DbCommand dbCommand = db.GetSqlStringCommand(strQuery);
        dbCommand.Parameters.Add(new SqlParameter("@Value1", Value1));
        dbCommand.Parameters.Add(new SqlParameter("@Value2", Value2));
        dbCommand.Parameters.Add(new SqlParameter("@Value3", Value3));

        dsCheck = db.ExecuteDataSet(dbCommand);

        if (dsCheck.Tables[0].Rows.Count > 0) // available
        {
            return true;
        }
        else
        {
            return false;
        }

    }

    public bool ExistorNotPara(string strQuery, string Value)
    {
        DataSet dsCheck = new DataSet();

        DbCommand dbCommand = db.GetSqlStringCommand(strQuery);

        dbCommand.Parameters.Add(new SqlParameter("@Value", Value));


        dsCheck = db.ExecuteDataSet(dbCommand);

        if (dsCheck.Tables[0].Rows.Count > 0)
        {
            return false;
        }
        else
        {
            return true;
        }

    }

    public bool ExistorNotParaD(string strQuery, string Value, string Value2)
    {
        DataSet dsCheck = new DataSet();

        DbCommand dbCommand = db.GetSqlStringCommand(strQuery);

        dbCommand.Parameters.Add(new SqlParameter("@Value", Value));
        dbCommand.Parameters.Add(new SqlParameter("@Value2", Value2));

        dsCheck = db.ExecuteDataSet(dbCommand);

        if (dsCheck.Tables[0].Rows.Count > 0)
        {
            return false;
        }
        else
        {
            return true;
        }

    }


    /// <summary>
    /// Added By Bilal - Dated 18April2017
    /// Exist or Not with True if exist
    /// takes 2 paramters
    /// </summary>
    /// <param name="strQuery"></param>
    /// <returns></returns>
    public bool ExistorNotParams(string strQuery, string Value, string Value2)
    {
        DataSet dsCheck = new DataSet();

        DbCommand dbCommand = db.GetSqlStringCommand(strQuery);

        dbCommand.Parameters.Add(new SqlParameter("@Value", Value));
        dbCommand.Parameters.Add(new SqlParameter("@Value2", Value2));


        dsCheck = db.ExecuteDataSet(dbCommand);

        if (dsCheck.Tables[0].Rows.Count > 0)
        {
            return false;
        }
        else
        {
            return true;
        }

    }
    public bool ExistEmailUserNameorNot(string UserName, string Email)
    {
        StringBuilder builder = new StringBuilder();
        DbCommand command;
        DataSet dsCheck = new DataSet();

        builder.Append("Select Email FROM MemberUser where Email = @Email AND UserName = @UserName");

        command = db.GetSqlStringCommand(builder.ToString());
        command.Parameters.Add(new SqlParameter("@Email", Email));
        command.Parameters.Add(new SqlParameter("@UserName", UserName));

        dsCheck = db.ExecuteDataSet(command);

        if (dsCheck.Tables[0].Rows.Count > 0)
        {
            return true;
        }
        else
        {
            return false;
        }

    }

    public int MemberUserId(string UserName)//, string Email)
    {
        StringBuilder builder = new StringBuilder();
        DbCommand command;
        DataSet dsCheck = new DataSet();

        //builder.Append("Select MemberUserId FROM MemberUser where Email = @Email AND UserName = @UserName");
        builder.Append("Select MemberUserId FROM MemberUser where UserName = @UserName");

        command = db.GetSqlStringCommand(builder.ToString());
        //command.Parameters.Add(new SqlParameter("@Email", Email));
        command.Parameters.Add(new SqlParameter("@UserName", UserName));

        dsCheck = db.ExecuteDataSet(command);

        if (dsCheck.Tables[0].Rows.Count > 0)
        {
            return Convert.ToInt32(dsCheck.Tables[0].Rows[0]["MemberUserID"]);
        }
        else
        {
            return 0;
        }

    }

    public int MemberInfoId(int MemberUserID)//, string Email)
    {
        StringBuilder builder = new StringBuilder();
        DbCommand command;
        DataSet dsCheck = new DataSet();

        //builder.Append("Select MemberUserId FROM MemberUser where Email = @Email AND UserName = @UserName");
        builder.Append("Select MemberInfoID FROM PD_MemberInfo where MemberUserID = @MemberUserID");

        command = db.GetSqlStringCommand(builder.ToString());
        //command.Parameters.Add(new SqlParameter("@Email", Email));
        command.Parameters.Add(new SqlParameter("@MemberUserID", MemberUserID));

        dsCheck = db.ExecuteDataSet(command);

        if (dsCheck.Tables[0].Rows.Count > 0)
        {
            return Convert.ToInt32(dsCheck.Tables[0].Rows[0]["MemberInfoID"]);
        }
        else
        {
            return 0;
        }

    }

    public int DonatorPersonId(int MemberUserID)//, string Email)
    {
        StringBuilder builder = new StringBuilder();
        DbCommand command;
        DataSet dsCheck = new DataSet();

        //builder.Append("Select MemberUserId FROM MemberUser where Email = @Email AND UserName = @UserName");
        builder.Append("Select DonatePersonID FROM PD_DonatePerson where DonationUserID = @MemberUserID");

        command = db.GetSqlStringCommand(builder.ToString());
        //command.Parameters.Add(new SqlParameter("@Email", Email));
        command.Parameters.Add(new SqlParameter("@MemberUserID", MemberUserID));

        dsCheck = db.ExecuteDataSet(command);

        if (dsCheck.Tables[0].Rows.Count > 0)
        {
            return Convert.ToInt32(dsCheck.Tables[0].Rows[0]["DonatePersonID"]);
        }
        else
        {
            return 0;
        }

    }

    //public static bool IsAuthenticated(string url)
    //{
    //    bool status = false;
    //    string[] arr = url.Split('/');

    //    string[] arr2 = arr[arr.Length - 1].Split('?');

    //    Dictionary<string, AccessRightsSystem> dictForm = (Dictionary<string, AccessRightsSystem>)System.Web.HttpContext.Current.Session["Dict"];

    //    Dictionary<string, AccessRightsSystem> dictFormEx = (Dictionary<string, AccessRightsSystem>)System.Web.HttpContext.Current.Session["DictEx"];

    //    if (dictForm != null)
    //    {
    //        if (dictForm.Count > 0)
    //        {
    //            if (dictForm.ContainsKey(arr2[0]) || dictFormEx.ContainsKey(arr2[0]))
    //            {
    //                status = true;
    //            }

    //        }
    //    }




    //    return status;
    //}


    public DataSet CheckButtonStatus(string url, int empid)
    {
        string[] arr = url.Split('/');

        string[] arr2 = arr[arr.Length - 1].Split('?');

        url = arr2[0];

        StringBuilder builder = new StringBuilder();
        builder.Append(" SELECT AR_Add, AR_Edit, AR_Delete, AR_View, AR_Approve FROM CR_AccessRights Where EmployeeID = @EmployeeID and  AR_PageURL = @URL");
        DbCommand command = db.GetSqlStringCommand(builder.ToString());
        command.Parameters.Add(new SqlParameter("@EmployeeID", empid));
        command.Parameters.Add(new SqlParameter("@URL", url));

        return db.ExecuteDataSet(command);


    }

    public string NowDateHijri()
    {
        DateTime dt;
        System.Globalization.DateTimeFormatInfo hijriDate;

        dt = Convert.ToDateTime(DateTime.Now.ToString());
        hijriDate = new System.Globalization.CultureInfo("ar-SA", false).DateTimeFormat;
        hijriDate.Calendar = new System.Globalization.HijriCalendar();
        hijriDate.ShortDatePattern = "dd/MM/yyyy";
        hijriDate.LongDatePattern = "dd/MM/yyyy";
        //string date = dt.ToString("f", hijriDate);

        string date = dt.ToShortDateString();

        string strNowDate;

        //strNowDate = date.Substring(0, 2) + "/" + date.Substring(date.Length - 15, 2) + "/" + date.Substring(date.Length - 12, 4);

        strNowDate = date.Substring(0, 2) + "/" + date.Substring(date.Length - 5, 2) + "/" + "14" + date.Substring(date.Length - 2, 2);

        return strNowDate;

    }

    public DataSet GetRFQ()
    {
        StringBuilder builder = new StringBuilder();
        DbCommand command;

        builder.Append(" SELECT RFQID , 'RFQ-' + CAST(RFQID AS VARCHAR(50)) RFQName  FROM RFQ ");

        try
        {
            command = db.GetSqlStringCommand(builder.ToString());

        }
        catch (Exception ex)
        {
            return null;
        }
        return db.ExecuteDataSet(command);
    }


    public string GetItemPrice(string strIC)
    {
        StringBuilder builder = new StringBuilder();
        DbCommand command;
        builder.Append(" SELECT ItemPrice from Item Where ItemCode = @ItemCode");

        try
        {
            command = db.GetSqlStringCommand(builder.ToString());
            command.Parameters.Add(new SqlParameter("@ItemCode", strIC));

        }
        catch (Exception ex)
        {
            return "";
        }
        return db.ExecuteScalar(command).ToString();
    }

    public bool CheckRFQforComm(int RFQID, int CommTypeID)
    {
        StringBuilder builder = new StringBuilder();
        DbCommand command;
        DataSet dsCheck = new DataSet();

        builder.Append(" SELECT RFQID from IC_CommRFQEmp Where CommissionTypeID = @CTID and RFQID = @RFQID");

        command = db.GetSqlStringCommand(builder.ToString());
        command.Parameters.Add(new SqlParameter("@RFQID", RFQID));
        command.Parameters.Add(new SqlParameter("@CTID", CommTypeID));

        dsCheck = db.ExecuteDataSet(command);

        if (dsCheck.Tables[0].Rows.Count > 0)
        {
            return true;
        }
        else
        {
            return false;
        }

    }


    public void SendEmail(string toemail, string subject, string body)
    {




        SmtpClient smtpClient = new SmtpClient();
        MailMessage message = new MailMessage();


        MailAddress fromAddress = new MailAddress("gic@arabsea.com", "GIC");

        // You can specify the host name or ipaddress of your server
        // Default in IIS will be localhost 
        smtpClient.Host = "localhost";

        //Default port will be 25
        smtpClient.Port = 25;

        //From address will be given as a MailAddress Object
        message.From = fromAddress;

        // To address collection of MailAddress
        message.To.Add("gic@arabsea.com");//gic@arabsea.com");
        message.Subject = "GIC - Error - " + subject;

        // CC and BCC optional
        // MailAddressCollection class is used to send the email to various users
        // You can specify Address as new MailAddress("admin1@yoursite.com")
        //message.CC.Add("gic@arabsea.com");
        //message.CC.Add("webmaster@arabsea.com");

        // You can specify Address directly as string
        //message.Bcc.Add(new MailAddress(""));
        //message.Bcc.Add(new MailAddress(""));

        //Body can be Html or text format
        //Specify true if it is html message
        message.IsBodyHtml = true;


        // Message body content
        message.Body = body;

        System.Net.NetworkCredential SMTPUserInfo = new System.Net.NetworkCredential("admin@gic.cc", "arabsea123");
        //System.Net.NetworkCredential SMTPUserInfo = new System.Net.NetworkCredential("gic@arabsea.com", "g1c");
        smtpClient.UseDefaultCredentials = false;
        smtpClient.Credentials = SMTPUserInfo;

        // Send SMTP mail
        smtpClient.Send(message);
    }


    public int ReturnInt(string strQuery)
    {
        StringBuilder builder = new StringBuilder();
        DbCommand command;
        builder.Append(strQuery);

        try
        {
            command = db.GetSqlStringCommand(builder.ToString());

            return int.Parse(db.ExecuteScalar(command).ToString());
        }
        catch (Exception ex)
        {
            return 0;
        }
    }

    public int ReturnIntMinus(string strQuery)
    {
        StringBuilder builder = new StringBuilder();
        DbCommand command;
        builder.Append(strQuery);

        try
        {
            command = db.GetSqlStringCommand(builder.ToString());

            return int.Parse(db.ExecuteScalar(command).ToString());
        }
        catch (Exception ex)
        {
            return -1;
        }
    }

    public int ReturnIntPara(string strQuery, string Para)
    {
        StringBuilder builder = new StringBuilder();
        DbCommand command;
        builder.Append(strQuery);

        try
        {
            command = db.GetSqlStringCommand(builder.ToString());
            command.Parameters.Add(new SqlParameter("@Para", Para));
            return int.Parse(db.ExecuteScalar(command).ToString());
        }
        catch (Exception ex)
        {
            return 0;
        }

    }


    public float Returnflt(string strQuery)
    {
        StringBuilder builder = new StringBuilder();
        DbCommand command;
        builder.Append(strQuery);

        try
        {
            command = db.GetSqlStringCommand(builder.ToString());
            return float.Parse(db.ExecuteScalar(command).ToString());
        }
        catch (Exception ex)
        {
            return 0.0F;
        }

    }


    public decimal ReturnDec(string strQuery)
    {
        StringBuilder builder = new StringBuilder();
        DbCommand command;
        builder.Append(strQuery);

        try
        {
            command = db.GetSqlStringCommand(builder.ToString());
            return decimal.Parse(db.ExecuteScalar(command).ToString());
        }
        catch (Exception ex)
        {
            return 0.0M;
        }

    }

    public decimal ReturnDecPara(string strQuery, string Para)
    {
        StringBuilder builder = new StringBuilder();
        DbCommand command;
        builder.Append(strQuery);

        try
        {
            command = db.GetSqlStringCommand(builder.ToString());
            command.Parameters.Add(new SqlParameter("@Para", Para));
            return decimal.Parse(db.ExecuteScalar(command).ToString());
        }
        catch (Exception ex)
        {
            return 0.0M;
        }

    }

    public string ReturnStr(string strQuery)
    {
        StringBuilder builder = new StringBuilder();
        DbCommand command;
        builder.Append(strQuery);

        try
        {
            command = db.GetSqlStringCommand(builder.ToString());
            return db.ExecuteScalar(command).ToString();
        }
        catch (Exception ex)
        {
            return "";
        }

    }

    public string TReturnStr(string strQuery, DbTransaction txn)
    {
        StringBuilder builder = new StringBuilder();
        DbCommand command;
        builder.Append(strQuery);

        try
        {
            command = db.GetSqlStringCommand(builder.ToString());
            return db.ExecuteScalar(command, txn).ToString();
        }
        catch (Exception ex)
        {
            return "";
        }

    }

    public string ReturnStrPara(string strQuery, string Para)
    {
        StringBuilder builder = new StringBuilder();
        DbCommand command;
        builder.Append(strQuery);

        try
        {
            command = db.GetSqlStringCommand(builder.ToString());
            command.Parameters.Add(new SqlParameter("@Para", Para));
            return db.ExecuteScalar(command).ToString();
        }
        catch (Exception ex)
        {
            return "";
        }

    }

    //By Izhar Mehmood
    public string ReturnStrPara2(string strQuery, string Para1, string Para2)
    {
        StringBuilder builder = new StringBuilder();
        DbCommand command;
        builder.Append(strQuery);

        try
        {
            command = db.GetSqlStringCommand(builder.ToString());
            command.Parameters.Add(new SqlParameter("@Para1", Para1));
            command.Parameters.Add(new SqlParameter("@Para2", Para2));
            return db.ExecuteScalar(command).ToString();
        }
        catch (Exception ex)
        {
            return "";
        }

    }


    public DateTime Returndt(string strQuery)
    {
        StringBuilder builder = new StringBuilder();
        DbCommand command;
        builder.Append(strQuery);

        try
        {
            command = db.GetSqlStringCommand(builder.ToString());

        }
        catch (Exception ex)
        {
            return DateTime.Parse("0");
        }
        return DateTime.Parse(db.ExecuteScalar(command).ToString());
    }


    public int Check_IssueNumberAndDate(string table, string PKeyField, string pKeyValue)
    {
        DbCommand command;

        string strSP = "Check_IssueNumberAndDate";

        command = db.GetStoredProcCommand(strSP);
        command.Parameters.Add(new SqlParameter("@table", table));
        command.Parameters.Add(new SqlParameter("@PKeyField", PKeyField));
        command.Parameters.Add(new SqlParameter("@PKeyValue", pKeyValue));


        DataSet ds = db.ExecuteDataSet(command);

        if (ds != null)
        {
            if (ds.Tables.Count > 0)
                if (ds.Tables[0].Rows.Count > 0)
                    return (int.Parse(ds.Tables[0].Rows[0]["count"].ToString()));
        }
        return 0;

    }

    public int CheckOverlappingEdit(int conID, DateTime StartDate, DateTime EndDate)
    {

        int nResult = -1;

        try
        {
            string cmd = "Check_OverlappingBetweenDates";
            DbCommand command = db.GetStoredProcCommand(cmd);
            command.Parameters.Add(new SqlParameter("@ContractID", conID));
            command.Parameters.Add(new SqlParameter("@StartDate", StartDate));
            command.Parameters.Add(new SqlParameter("@EndDate", EndDate));

            nResult = int.Parse(db.ExecuteScalar(command).ToString());
            if (nResult >= 0)
                return nResult;
        }
        catch (Exception ex)
        {
            return -1;
        }
        return -1;
    }

    public int CheckOverlappingContEdit(int conID, DateTime StartDate, DateTime EndDate)
    {
        int nResult = -1;

        try
        {
            string cmd = "Check_OverlappingBetweenContDates";
            DbCommand command = db.GetStoredProcCommand(cmd);
            command.Parameters.Add(new SqlParameter("@ContractID", conID));
            command.Parameters.Add(new SqlParameter("@StartDate", StartDate));
            command.Parameters.Add(new SqlParameter("@EndDate", EndDate));

            nResult = int.Parse(db.ExecuteScalar(command).ToString());
            if (nResult >= 0)
                return nResult;
        }
        catch (Exception ex)
        {
            return -1;
        }
        return -1;
    }

    public int CheckOverlappingContEditWithVarchar(int conID, string StartDate, string EndDate, int EmployeeID)
    {
        try
        {
            string cmd = "Check_OverlappingBetweenContDatesWithVarchar";
            DbCommand command = db.GetStoredProcCommand(cmd);
            command.Parameters.Add(new SqlParameter("@ContractID", conID));
            command.Parameters.Add(new SqlParameter("@DateFromParam", StartDate));
            command.Parameters.Add(new SqlParameter("@DateToParam", EndDate));
            command.Parameters.Add(new SqlParameter("@EmployeeID", EmployeeID));

            return int.Parse(db.ExecuteScalar(command).ToString());
        }
        catch (Exception ex)
        {
            return -1;
        }
    }


    public int GetEmployeeStatusByContractID(int ContractID)
    {
        StringBuilder builder = new StringBuilder();
        DbCommand command;
        builder.Append(" SELECT     HR_Employee.SettingsEmployeeStatusTypeID FROM  HR_Contracts INNER JOIN ");
        builder.Append("  HR_Employee ON HR_Contracts.Emp_EmployeeID = HR_Employee.Emp_EmployeeID where ContractID = " + ContractID);
        try
        {
            command = db.GetSqlStringCommand(builder.ToString());
        }
        catch (Exception ex)
        {
            return 0;
        }
        return int.Parse(db.ExecuteScalar(command).ToString());
    }


    //public HRSalarySuspension FetchinGrid(string strFetchforGrid, int ID)
    //{
    //    IDataReader reader = null;
    //    HRSalarySuspension hrsal = new HRSalarySuspension();
    //    DbCommand dbCommand = db.GetSqlStringCommand(strFetchforGrid);
    //    dbCommand.Parameters.Add(new SqlParameter("@ID", ID));

    //    reader = db.ExecuteReader(dbCommand);

    //    while (reader.Read())
    //    {

    //        hrsal.IssueNumber = reader[0].ToString();
    //        hrsal.IssueDate = reader[1].ToString();
    //        hrsal.LetterDate = reader[2].ToString();
    //        hrsal.LetterNo = reader[3].ToString();
    //        hrsal.AbsentStartDate = reader[4].ToString();
    //        hrsal.StopDate = reader[5].ToString();
    //        hrsal.Memo = reader[6].ToString();
    //        hrsal.Reason = reader[7].ToString();

    //        return hrsal;
    //    }
    //    return null;

    //}
    public string CheckDate(string strDate)
    {
        StringBuilder builder = new StringBuilder();
        DbCommand command;
        string[] Date = strDate.Split('/');
        string strDay, strMonth, strYear, DtDate;
        int preday = 0;
        try
        {

            strDay = Date[0].ToString();
            strMonth = Date[1].ToString();
            strYear = Date[2].ToString();
            DtDate = strYear + strMonth + strDay;
            builder.Append(" Select convert(smalldatetime,'" + DtDate + "',131)");


            command = db.GetSqlStringCommand(builder.ToString());
            db.ExecuteScalar(command).ToString();
            return strDate;
        }
        catch (Exception ex)
        {
            preday = int.Parse(Date[0].ToString()) - 1;
            return preday.ToString() + "/" + Date[1].ToString() + "/" + Date[2].ToString();
        }
        //return db.ExecuteScalar(command).ToString();
    }
    public string CheckDateNew(string strDate)
    {
        StringBuilder builder = new StringBuilder();
        DbCommand command;
        string[] Date = strDate.Split('/');
        string strDay, strMonth, strYear, DtDate;
        int preday = 0;
        try
        {

            strDay = Date[0].ToString();
            strMonth = Date[1].ToString();
            strYear = Date[2].ToString();
            DtDate = strYear + strMonth + strDay;
            builder.Append(" Select convert(smalldatetime,'" + DtDate + "',131)");


            command = db.GetSqlStringCommand(builder.ToString());
            db.ExecuteScalar(command).ToString();
            return strDate;
        }
        catch (Exception ex)
        {
            //preday = int.Parse(Date[0].ToString()) - 1;

            //if condition by Izhar Mehmood on dated 161219 it remove 1 day.
            if (Date[0] == "01")
            {
                preday = Convert.ToInt32(Date[0].ToString());
            }
            else
            {
                preday = Convert.ToInt32(Date[0].ToString()) - 1;
            }

            return preday.ToString() + "/" + Date[1].ToString() + "/" + Date[2].ToString();
        }
        //return db.ExecuteScalar(command).ToString();
    }
    public void SetEmployeeStatus(int intID, int intStatus)
    {
        try
        {
            DbCommand command = db.GetSqlStringCommand(" Update HR_Employee set SettingsEmployeeStatusTypeID = @Status where  Emp_EmployeeID = @EmpID ");
            command.Parameters.Add(new SqlParameter("@EmpID", intID));
            command.Parameters.Add(new SqlParameter("@Status", intStatus));
            db.ExecuteNonQuery(command);
        }
        catch (Exception ex)
        {

        }
    }


    public string GetSQLInject(string txtString)
    {
        //    if (txtString.Contains(" OR ")

        //        || txtString.Contains("OR")
        //        || txtString.Contains("or")
        //        || txtString.Contains("Or")
        //        || txtString.Contains("oR")

        //        || txtString.Contains(" -- ")
        //        || txtString.Contains(" --")
        //        || txtString.Contains(" /* ")
        //        || txtString.Contains(" /*")
        //        || txtString.Contains(" ; ")
        //        || txtString.Contains(" ;")
        //        || txtString.Contains(" ;--")
        //        || txtString.Contains(" ;-- ")

        //        )

        //        return "0";
        //    else

        txtString = txtString.ToLower();
        string sqlstr = txtString.Replace("--", "").Replace("'", "").Replace("/*", "").Replace(";", "").Replace(" OR ", "").Replace(" or ", "").Replace(" oR ", "").Replace(" Or ", "").Replace(";--", "").Replace("*/", "").Replace("@@", "");
        sqlstr = sqlstr.Replace("begin", "").Replace("create", "").Replace("cursor", "").Replace("declare", "").Replace("delete", "").Replace("drop", "").Replace("end", "").Replace("exec", "").Replace("execute", "").Replace("fetch", "");
        sqlstr = sqlstr.Replace("insert", "").Replace("kill", "").Replace("sys", "").Replace("sysobjects", "").Replace("syscolumns", "").Replace("alter", "").Replace("1=1", "").Replace("#", "");
        return sqlstr;
    }

    //public static Boolean checkForSQLInjection(string userInput)
    //{
    //    bool isSQLInjection = false;

    //    string[] sqlCheckList = { "--",
    //                                   ";--",
    //                                   ";",
    //                                   "/*",
    //                                   "*/",
    //                                    "@@",
    //                                    "@",
    //                                    "char",
    //                                   "nchar",
    //                                   "varchar",
    //                                   "nvarchar",
    //                                   "alter",
    //                                   "begin",
    //                                   "cast",
    //                                   "create",
    //                                   "cursor",
    //                                   "declare",

    //                                   "delete",
    //                                   "drop",
    //                                   "end",
    //                                   "exec",
    //                                   "execute",

    //                                   "fetch",
    //                                    "insert",
    //                                    "kill",
    //                                    "select",
    //                                    "sys",
    //                                    "sysobjects",
    //                                    "syscolumns",
    //                                     "table",
    //                                     "update"
    //                                   };

    //    string CheckString = userInput.Replace("'", "''").Replace("'", "").Replace(" OR ", "").Replace(" or ", "");

    //    for (int i = 0; i <= sqlCheckList.Length - 1; i++)
    //    {
    //        if ((CheckString.IndexOf(sqlCheckList[i], StringComparison.OrdinalIgnoreCase) >= 0))
    //        {
    //            isSQLInjection = true;
    //        }
    //    }
    //    return isSQLInjection;
    //}

    //public byte[] GetImageByte(FileUpload fileUpload)
    //{

    //    if (fileUpload.HasFile)
    //    {

    //        byte[] MainImage = new byte[fileUpload.PostedFile.InputStream.Length];
    //        ////store the currently selected file in memeory.
    //        //HttpPostedFile img = fileUpload.PostedFile;
    //        ////set the binary data .
    //        fileUpload.PostedFile.InputStream.Read(MainImage, 0, MainImage.Length);
    //        return MainImage;
    //    }
    //    else
    //    {
    //        return new byte[] { 0 };
    //    }
    //}

    //public byte[] GetImageByte(AsyncFileUpload fileUpload)
    //{

    //    if (fileUpload.HasFile)
    //    {

    //        byte[] MainImage = new byte[fileUpload.PostedFile.InputStream.Length];
    //        ////store the currently selected file in memeory.
    //        //HttpPostedFile img = fileUpload.PostedFile;
    //        ////set the binary data .
    //        fileUpload.PostedFile.InputStream.Read(MainImage, 0, MainImage.Length);
    //        return MainImage;
    //    }
    //    else
    //    {
    //        return new byte[] { 0 };
    //    }
    //}

    public bool ExecuteNonQuery(string strQuery)
    {
        try
        {
            DbCommand dbCommand = db.GetSqlStringCommand(strQuery);

            db.ExecuteNonQuery(dbCommand);
            return true;
        }
        catch
        {
            return false;
        }
    }

    /*
	  public int ReturnIntPara(string strQuery, string Para)
	{
		StringBuilder builder = new StringBuilder();
		DbCommand command;
		builder.Append(strQuery);

		try
		{
			command = db.GetSqlStringCommand(builder.ToString());
			command.Parameters.Add(new SqlParameter("@Para", Para));
			return int.Parse(db.ExecuteScalar(command).ToString());
		}
	 * */
    public bool ExecuteNonQueryWithParam(string strQuery, string Param)
    {
        StringBuilder builder = new StringBuilder();
        DbCommand command;
        builder.Append(strQuery);

        try
        {
            command = db.GetSqlStringCommand(builder.ToString());
            command.Parameters.Add(new SqlParameter("@Para", Param));

            db.ExecuteNonQuery(command);
            return true;
        }
        catch
        {
            return false;
        }
    }

    //By Izhar Mehmood
    public bool ExecuteNonQueryWithParam2(string strQuery, string Param1, string Param2)
    {
        StringBuilder builder = new StringBuilder();
        DbCommand command;
        builder.Append(strQuery);

        try
        {
            command = db.GetSqlStringCommand(builder.ToString());
            command.Parameters.Add(new SqlParameter("@Param1", Param1));
            command.Parameters.Add(new SqlParameter("@Param2", Param2));

            db.ExecuteNonQuery(command);
            return true;
        }
        catch
        {
            return false;
        }
    }
    public bool ExecuteNonQueryWithParam3(string strQuery, string Param1, string Param2, string Param3)
    {
        StringBuilder builder = new StringBuilder();
        DbCommand command;
        builder.Append(strQuery);

        try
        {
            command = db.GetSqlStringCommand(builder.ToString());
            command.Parameters.Add(new SqlParameter("@Param1", Param1));
            command.Parameters.Add(new SqlParameter("@Param2", Param2));
            command.Parameters.Add(new SqlParameter("@Param3", Param3));
            db.ExecuteNonQuery(command);
            return true;
        }
        catch(Exception ex)
        {
            return false;
        }
    }
    public bool ExecuteNonQueryWithParam4(string strQuery, string Param1, string Param2, string Param3, string Param4)
    {
        StringBuilder builder = new StringBuilder();
        DbCommand command;
        builder.Append(strQuery);

        try
        {
            command = db.GetSqlStringCommand(builder.ToString());
            command.Parameters.Add(new SqlParameter("@Param1", Param1));
            command.Parameters.Add(new SqlParameter("@Param2", Param2));
            command.Parameters.Add(new SqlParameter("@Param3", Param3));
            command.Parameters.Add(new SqlParameter("@Param4", Param4));

            db.ExecuteNonQuery(command);
            return true;
        }
        catch
        {
            return false;
        }
    }

    public int ExecuteSpRint(string strQuery)
    {
        try
        {
            DbCommand dbCommand = db.GetStoredProcCommand(strQuery);

            return int.Parse(db.ExecuteScalar(dbCommand).ToString());
        }
        catch
        {
            return 0;
        }
    }


    public int ExecuteScalar(string strQuery)
    {
        try
        {
            DbCommand dbCommand = db.GetSqlStringCommand(strQuery);

            return int.Parse(db.ExecuteScalar(dbCommand).ToString());
        }
        catch
        {
            return 0;
        }
    }

    public string ExecuteScalarWithParam(string strQuery, string Param)
    {
        try
        {
            DbCommand dbCommand = db.GetSqlStringCommand(strQuery);
            dbCommand.Parameters.Add(new SqlParameter("@Para", Param));

            object oo = db.ExecuteScalar(dbCommand);
            return oo.ToString();
        }
        catch
        {
            return null;
        }
    }

    public int GetEmpbyHR(string ID)
    {
        StringBuilder builder = new StringBuilder();
        DbCommand command;

        builder.Append(" SELECT     dbo.HR_Employee.Emp_EmployeeID FROM         dbo.Users INNER JOIN ");
        builder.Append(" dbo.HR_Employee ON dbo.Users.EmployeeGlobalID = dbo.HR_Employee.IDDocumentNumber WHERE     (dbo.HR_Employee.IDDocumentNumber = '" + ID + "')");

        try
        {
            command = db.GetSqlStringCommand(builder.ToString());
            return int.Parse(db.ExecuteScalar(command).ToString());
        }
        catch (Exception ex)
        {
            return 0;
        }

    }

    public bool EexecuteNonQuery(string strQuery)
    {
        try
        {
            DbCommand dbCommand = db.GetSqlStringCommand(strQuery);

            db.ExecuteNonQuery(dbCommand);
            return true;
        }
        catch
        {
            return false;
        }
    }


    public static bool IsAuthenticated(string url)
    {
        //bool status = false;
        //string[] arr = url.Split('/');

        //string[] arr2 = arr[arr.Length - 1].Split('?');

        //Dictionary<string, AccessRightsSystem> dictForm = (Dictionary<string, AccessRightsSystem>)System.Web.HttpContext.Current.Session["Dict"];

        //Dictionary<string, AccessRightsSystem> dictFormEx = (Dictionary<string, AccessRightsSystem>)System.Web.HttpContext.Current.Session["DictEx"];

        //if (dictForm != null)
        //{
        //    if (dictForm.Count > 0)
        //    {
        //        if (dictForm.ContainsKey(arr2[0]) || dictFormEx.ContainsKey(arr2[0]))
        //        {
        //            status = true;
        //        }

        //    }
        //}




        //return status;

        return true;
    }


    public DataTable ImageUploadRecord(int TypeID, int DocumentTypeID)
    {
        try
        {
            DbCommand dbCommand = db.GetSqlStringCommand(@"SELECT     dbo.AC_ArchiveMedia.ArchiveMediaID AS ID, dbo.AC_ArchiveMedia.Doc, dbo.AC_ArchiveMedia.FileType AS ContentType, dbo.AC_ArchiveMedia.Size, dbo.AC_ArchiveMedia.FileName AS Name FROM         dbo.AC_ArchiveMedia INNER JOIN dbo.PD_Family ON dbo.AC_ArchiveMedia.UserID = dbo.PD_Family.FamilyUserID where dbo.AC_ArchiveMedia.ProcessTypeID = " + TypeID + " and dbo.AC_ArchiveMedia.SettingsArchiveTypeID = " + DocumentTypeID + " ");
            return db.ExecuteDataSet(dbCommand).Tables[0];
        }
        catch
        {
            return null;
        }
    }

    public DataTable ImageUploadMainServiceRecord(int TypeID, int DocumentTypeID, int serviceTypeID)
    {
        try
        {
            DbCommand dbCommand = db.GetSqlStringCommand(@"SELECT        dbo.AC_ArchiveMedia.ArchiveMediaID AS ID, dbo.AC_ArchiveMedia.Doc, dbo.AC_ArchiveMedia.FileType AS ContentType, dbo.AC_ArchiveMedia.Size, dbo.AC_ArchiveMedia.FileName AS Name FROM            dbo.AC_ArchiveMedia INNER JOIN dbo.PD_ServiceRequest ON dbo.AC_ArchiveMedia.ProcessID = dbo.PD_ServiceRequest.ServiceRequestID where dbo.PD_ServiceRequest.ServiceID = " + serviceTypeID + " and dbo.AC_ArchiveMedia.ProcessTypeID = " + TypeID + " and dbo.AC_ArchiveMedia.SettingsArchiveTypeID = " + DocumentTypeID + "");
            return db.ExecuteDataSet(dbCommand).Tables[0];
        }
        catch
        {
            return null;
        }
    }

    public DataTable ImageUploadSubServiceRecord(int TypeID, int DocumentTypeID, int MainserviceTypeID)
    {
        try
        {
            DbCommand dbCommand = db.GetSqlStringCommand(@"SELECT        dbo.AC_ArchiveMedia.ArchiveMediaID AS ID, dbo.AC_ArchiveMedia.Doc, dbo.AC_ArchiveMedia.FileType AS ContentType, dbo.AC_ArchiveMedia.Size, dbo.AC_ArchiveMedia.FileName AS Name FROM            dbo.AC_ArchiveMedia INNER JOIN dbo.PD_ServiceRequest ON dbo.AC_ArchiveMedia.ProcessID = dbo.PD_ServiceRequest.ServiceRequestID where dbo.PD_ServiceRequest.MainServiceID = " + MainserviceTypeID + " and dbo.AC_ArchiveMedia.ProcessTypeID = " + TypeID + " and dbo.AC_ArchiveMedia.SettingsArchiveTypeID = " + DocumentTypeID + "");
            return db.ExecuteDataSet(dbCommand).Tables[0];
        }
        catch
        {
            return null;
        }
    }

    public DataTable GetImageUploadRecord(int ID)
    {
        try
        {
            DbCommand dbCommand = db.GetSqlStringCommand("SELECT [ArchiveMediaID] as ID,[Doc] ,[FileType] as ContentType ,[Size] as Size   , [Doc] as Data,   [FileName] as Name FROM [dbo].[AC_ArchiveMedia] where dbo.AC_ArchiveMedia.ArchiveMediaID = @ID");
            dbCommand.Parameters.Add(new SqlParameter("@ID", ID));
            return db.ExecuteDataSet(dbCommand).Tables[0];
        }
        catch
        {
            return null;
        }
    }

    public DataTable ImageUploadFatherRecord(int TypeID, int DocumentTypeID, int FamilyID)
    {
        try
        {
            DbCommand dbCommand = db.GetSqlStringCommand(@"SELECT        dbo.AC_ArchiveMedia.ArchiveMediaID AS ID, dbo.AC_ArchiveMedia.Doc, dbo.AC_ArchiveMedia.FileType AS ContentType, dbo.AC_ArchiveMedia.Size, dbo.AC_ArchiveMedia.FileName AS Name FROM            dbo.AC_ArchiveMedia INNER JOIN dbo.PD_FamilyFather ON dbo.AC_ArchiveMedia.ProcessID = dbo.PD_FamilyFather.FamilyFatherID where dbo.AC_ArchiveMedia.ProcessTypeID = " + TypeID + " and dbo.AC_ArchiveMedia.SettingsArchiveTypeID = " + DocumentTypeID + " and dbo.AC_ArchiveMedia.FamilyID = " + FamilyID + " ");
            return db.ExecuteDataSet(dbCommand).Tables[0];
        }
        catch
        {
            return null;
        }
    }

    public DataTable ImageUploadMotherRecord(int TypeID, int DocumentTypeID, int FamilyID)
    {
        try
        {
            DbCommand dbCommand = db.GetSqlStringCommand(@"SELECT        dbo.AC_ArchiveMedia.ArchiveMediaID AS ID, dbo.AC_ArchiveMedia.Doc, dbo.AC_ArchiveMedia.FileType AS ContentType, dbo.AC_ArchiveMedia.Size, dbo.AC_ArchiveMedia.FileName AS Name FROM            dbo.AC_ArchiveMedia INNER JOIN dbo.PD_FamilyMother ON dbo.AC_ArchiveMedia.ProcessID = dbo.PD_FamilyMother.FamilyMotherID where dbo.AC_ArchiveMedia.ProcessTypeID = " + TypeID + " and dbo.AC_ArchiveMedia.SettingsArchiveTypeID = " + DocumentTypeID + " and dbo.AC_ArchiveMedia.FamilyID = " + FamilyID + "  ");
            return db.ExecuteDataSet(dbCommand).Tables[0];
        }
        catch
        {
            return null;
        }
    }

    public DataTable ImageUploadChildRecord(int TypeID, int DocumentTypeID, int FamilyID)
    {
        try
        {
            DbCommand dbCommand = db.GetSqlStringCommand(@"SELECT        dbo.AC_ArchiveMedia.ArchiveMediaID AS ID, dbo.AC_ArchiveMedia.Doc, dbo.AC_ArchiveMedia.FileType AS ContentType, dbo.AC_ArchiveMedia.Size, dbo.AC_ArchiveMedia.FileName AS Name FROM            dbo.AC_ArchiveMedia INNER JOIN dbo.PD_FamilyChild ON dbo.AC_ArchiveMedia.ProcessID = dbo.PD_FamilyChild.FamilyChildID where dbo.AC_ArchiveMedia.ProcessTypeID = " + TypeID + " and dbo.AC_ArchiveMedia.SettingsArchiveTypeID = " + DocumentTypeID + " and dbo.AC_ArchiveMedia.FamilyID = " + FamilyID + "  ");
            return db.ExecuteDataSet(dbCommand).Tables[0];
        }
        catch
        {
            return null;
        }
    }

    public DataTable ImageUploadChildRecordwithChildID(int TypeID, int ChildID, int DocumentTypeID, int FamilyID)
    {
        try
        {
            DbCommand dbCommand = db.GetSqlStringCommand(@"SELECT        dbo.AC_ArchiveMedia.ArchiveMediaID AS ID, dbo.AC_ArchiveMedia.Doc, dbo.AC_ArchiveMedia.FileType AS ContentType, dbo.AC_ArchiveMedia.Size, dbo.AC_ArchiveMedia.FileName AS Name FROM            dbo.AC_ArchiveMedia INNER JOIN dbo.PD_FamilyChild ON dbo.AC_ArchiveMedia.ProcessID = dbo.PD_FamilyChild.FamilyChildID where dbo.AC_ArchiveMedia.ProcessID = " + ChildID + " and  dbo.AC_ArchiveMedia.ProcessTypeID = " + TypeID + " and dbo.AC_ArchiveMedia.SettingsArchiveTypeID = " + DocumentTypeID + " and dbo.AC_ArchiveMedia.FamilyID = " + FamilyID + "  ");
            return db.ExecuteDataSet(dbCommand).Tables[0];
        }
        catch
        {
            return null;
        }
    }

    public DataTable ImageUploadfamilyRecord(int TypeID, int DocumentTypeID, int FamilyID)
    {
        try
        {
            DbCommand dbCommand = db.GetSqlStringCommand(@"SELECT        dbo.AC_ArchiveMedia.ArchiveMediaID AS ID, dbo.AC_ArchiveMedia.Doc, dbo.AC_ArchiveMedia.FileType AS ContentType, dbo.AC_ArchiveMedia.Size, dbo.AC_ArchiveMedia.FileName AS Name FROM            dbo.AC_ArchiveMedia INNER JOIN dbo.PD_Family ON dbo.AC_ArchiveMedia.ProcessID = dbo.PD_Family.FamilyID where dbo.AC_ArchiveMedia.ProcessTypeID = " + TypeID + " and dbo.AC_ArchiveMedia.SettingsArchiveTypeID = " + DocumentTypeID + " and dbo.AC_ArchiveMedia.FamilyID = " + FamilyID + "  ");
            return db.ExecuteDataSet(dbCommand).Tables[0];
        }
        catch
        {
            return null;
        }
    }


    public DataTable GetImageUploadRecordbyFamilyID(int ProcessTypeID, int ProcessID, int FamilyID, int SettingArchiveTypeID)
    {
        try
        {
            DbCommand dbCommand = db.GetSqlStringCommand(@"SELECT        dbo.AC_ArchiveMedia.ArchiveMediaID AS ID, dbo.AC_ArchiveMedia.Doc, dbo.AC_ArchiveMedia.FileType AS ContentType, dbo.AC_ArchiveMedia.Size, dbo.AC_ArchiveMedia.FileName AS Name, dbo.AC_ArchiveMedia.Description FROM            dbo.AC_ArchiveMedia where  dbo.AC_ArchiveMedia.ProcessTypeID = " + ProcessTypeID + " and dbo.AC_ArchiveMedia.ProcessID = @ID and dbo.AC_ArchiveMedia.FamilyID = " + FamilyID + " and dbo.AC_ArchiveMedia.SettingsArchiveTypeID = " + SettingArchiveTypeID + "");
            dbCommand.Parameters.Add(new SqlParameter("@ID", ProcessID));
            return db.ExecuteDataSet(dbCommand).Tables[0];
        }
        catch
        {
            return null;
        }
    }

    //------------------Installment and Transfer
    public DataTable GetImageUploadRecordbyInstallmentandTransfer(int ProcessTypeID, int ProcessID, int FamilyID, int SettingArchiveTypeID)
    {
        try
        {
            DbCommand dbCommand = db.GetSqlStringCommand(@"SELECT        dbo.AC_ArchiveMedia.ArchiveMediaID AS ID, dbo.AC_ArchiveMedia.Doc, dbo.AC_ArchiveMedia.FileType AS ContentType, dbo.AC_ArchiveMedia.Size, dbo.AC_ArchiveMedia.FileName AS Name, dbo.AC_ArchiveMedia.Description
FROM            dbo.AC_ArchiveMedia where 
dbo.AC_ArchiveMedia.ProcessTypeID = " + ProcessTypeID + " and dbo.AC_ArchiveMedia.ProcessID = " + ProcessID + " and dbo.AC_ArchiveMedia.FamilyID = " + FamilyID + " and dbo.AC_ArchiveMedia.SettingsArchiveTypeID = " + SettingArchiveTypeID + "");
            return db.ExecuteDataSet(dbCommand).Tables[0];
        }
        catch
        {
            return null;
        }
    }

    //Munawar

    public DataTable GetTransportImageUploadRecordbyFamilyID(int ProcessTypeID, int ProcessID, int FamilyID, int SettingArchiveTypeID, int FamilyPersonId) // 81,82,83 for Doc Uploaded by Daughter in Transport
    {
        try
        {
            DbCommand dbCommand = db.GetSqlStringCommand(@"SELECT        dbo.AC_ArchiveMedia.ArchiveMediaID AS ID, dbo.AC_ArchiveMedia.Doc, dbo.AC_ArchiveMedia.FileType AS ContentType, dbo.AC_ArchiveMedia.Size, dbo.AC_ArchiveMedia.FileName AS Name, dbo.AC_ArchiveMedia.Description FROM            dbo.AC_ArchiveMedia where  dbo.AC_ArchiveMedia.ProcessTypeID = " + ProcessTypeID + " and dbo.AC_ArchiveMedia.ProcessID = " + ProcessID + " and dbo.AC_ArchiveMedia.FamilyID = " + FamilyID + " and FamilyPersonID = " + FamilyPersonId + " and dbo.AC_ArchiveMedia.SettingsArchiveTypeID in (81,82,83)");
            return db.ExecuteDataSet(dbCommand).Tables[0];
        }
        catch
        {
            return null;
        }
    }

    public DataTable GetTransportFileImageUploadRecordsbyServiceID(int ProcessTypeID, int ProcessID, int FamilyID, int SettingArchiveTypeID)
    {
        try
        {
            DbCommand dbCommand = db.GetSqlStringCommand(@"SELECT        dbo.AC_ArchiveMedia.ArchiveMediaID AS ID, dbo.AC_ArchiveMedia.Doc, dbo.AC_ArchiveMedia.FileType AS ContentType, dbo.AC_ArchiveMedia.Size, dbo.AC_ArchiveMedia.FileName AS Name, dbo.AC_ArchiveMedia.Description FROM            dbo.AC_ArchiveMedia where  dbo.AC_ArchiveMedia.ProcessTypeID = " + ProcessTypeID + " and dbo.AC_ArchiveMedia.ProcessID = " + ProcessID + " and dbo.AC_ArchiveMedia.FamilyID = " + FamilyID + " and dbo.AC_ArchiveMedia.SettingsArchiveTypeID = 87 ");
            return db.ExecuteDataSet(dbCommand).Tables[0];
        }
        catch
        {
            return null;
        }
    }


    //Munawar

    public DataTable GetCancelBDImageUploadRecordbyCancelID(int ProcessTypeID, int ProcessID, int FamilyID, int SettingArchiveTypeID, int FamilyPersonId) // 81,82,83 for Doc Uploaded by Cancel BD TransList
    {
        try
        {
            DbCommand dbCommand = db.GetSqlStringCommand(@"SELECT        dbo.AC_ArchiveMedia.ArchiveMediaID AS ID, dbo.AC_ArchiveMedia.Doc, dbo.AC_ArchiveMedia.FileType AS ContentType, dbo.AC_ArchiveMedia.Size, dbo.AC_ArchiveMedia.FileName AS Name, dbo.AC_ArchiveMedia.Description
FROM            dbo.AC_ArchiveMedia where 
dbo.AC_ArchiveMedia.ProcessTypeID = " + ProcessTypeID + " and dbo.AC_ArchiveMedia.ProcessID = " + ProcessID + " and dbo.AC_ArchiveMedia.FamilyID = " + FamilyID + " and FamilyPersonID = " + FamilyPersonId + " and dbo.AC_ArchiveMedia.SettingsArchiveTypeID = " + SettingArchiveTypeID);
            return db.ExecuteDataSet(dbCommand).Tables[0];
        }
        catch
        {
            return null;
        }
    }

    public DataTable GetBranchDonationITImageUploadRecord(int ProcessTypeID, int ProcessID, int SettingArchiveTypeID) // 81,82,83 for Doc Uploaded by Cancel BD TransList
    {
        try
        {
            DbCommand dbCommand = db.GetSqlStringCommand(@"SELECT        dbo.AC_ArchiveMedia.ArchiveMediaID AS ID, dbo.AC_ArchiveMedia.Doc, dbo.AC_ArchiveMedia.FileType AS ContentType, dbo.AC_ArchiveMedia.Size, dbo.AC_ArchiveMedia.FileName AS Name, dbo.AC_ArchiveMedia.Description
FROM            dbo.AC_ArchiveMedia where 
dbo.AC_ArchiveMedia.ProcessTypeID = " + ProcessTypeID + " and dbo.AC_ArchiveMedia.ProcessID = " + ProcessID + " and dbo.AC_ArchiveMedia.SettingsArchiveTypeID = " + SettingArchiveTypeID);
            return db.ExecuteDataSet(dbCommand).Tables[0];
        }
        catch
        {
            return null;
        }
    }

    //Bilal
    public DataTable GetGaraunteeStoreItemRecordImageUploadRecord(int ProcessTypeID, int ProcessID) // 89,90,91,92 for Doc Uploaded by StoreItemRecord
    {
        try
        {
            DbCommand dbCommand = db.GetSqlStringCommand(@"SELECT        dbo.AC_ArchiveMedia.ArchiveMediaID AS ID, dbo.AC_ArchiveMedia.Doc, dbo.AC_ArchiveMedia.FileType AS ContentType, dbo.AC_ArchiveMedia.Size, dbo.AC_ArchiveMedia.FileName AS Name, dbo.AC_ArchiveMedia.Description
FROM            dbo.AC_ArchiveMedia where 
dbo.AC_ArchiveMedia.ProcessTypeID = " + ProcessTypeID + " and dbo.AC_ArchiveMedia.ProcessID = " + ProcessID + " and dbo.AC_ArchiveMedia.SettingsArchiveTypeID IN (89,90)");
            return db.ExecuteDataSet(dbCommand).Tables[0];
        }
        catch
        {
            return null;
        }
    }
    public DataTable GetInvoiceStoreItemRecordImageUploadRecord(int ProcessTypeID, int ProcessID) // 89,90,91,92 for Doc Uploaded by StoreItemRecord
    {
        try
        {
            DbCommand dbCommand = db.GetSqlStringCommand(@"SELECT        dbo.AC_ArchiveMedia.ArchiveMediaID AS ID, dbo.AC_ArchiveMedia.Doc, dbo.AC_ArchiveMedia.FileType AS ContentType, dbo.AC_ArchiveMedia.Size, dbo.AC_ArchiveMedia.FileName AS Name, dbo.AC_ArchiveMedia.Description
FROM            dbo.AC_ArchiveMedia where 
dbo.AC_ArchiveMedia.ProcessTypeID = " + ProcessTypeID + " and dbo.AC_ArchiveMedia.ProcessID = " + ProcessID + " and dbo.AC_ArchiveMedia.SettingsArchiveTypeID IN (91,92)");
            return db.ExecuteDataSet(dbCommand).Tables[0];
        }
        catch
        {
            return null;
        }
    }


    //Bilal
    public DataTable GetFileUploadRecordbySupplierTenderID(int SupplierTenderID)
    {
        try
        {
            DbCommand dbCommand = db.GetSqlStringCommand(@"SELECT RFQID As ID, Ten_UploadSpecs As Name, Ten_UploadSpecs As FileName, SpecsType As ContentType, Specs as Data FROM RFQTender WHERE RFQID =@ID");
            dbCommand.Parameters.Add(new SqlParameter("@ID", SupplierTenderID));
            return db.ExecuteDataSet(dbCommand).Tables[0];
        }
        catch
        {
            return null;
        }
    }
    //Bilal
    public DataTable GetEmployeeTechCQFilesByQuotationID(int QuotationID)
    {
        try
        {
            DbCommand dbCommand = db.GetSqlStringCommand(@"SELECT dbo.AC_ArchiveMedia.ArchiveMediaID AS ID, dbo.AC_ArchiveMedia.Doc, dbo.AC_ArchiveMedia.FileType AS ContentType, dbo.AC_ArchiveMedia.Size, dbo.AC_ArchiveMedia.FileName AS Name, dbo.AC_ArchiveMedia.Description from AC_ArchiveMedia WHERE ProcessID = " + QuotationID + " AND ProcessTypeID = 12 AND SettingsArchiveTypeID = 93");
            return db.ExecuteDataSet(dbCommand).Tables[0];
        }
        catch
        {
            return null;
        }
    }
    public int GetActiveFamilyMembers(int FamilyID, int MotherStatusID)
    {
        int FamilyCount = 0;
        if (MotherStatusID == 3 || MotherStatusID == 5 || MotherStatusID == 7 || MotherStatusID == 9 || MotherStatusID == 10)
        {
            FamilyCount = 0;
        }
        FamilyCount = ReturnInt("Select Count(FamilyMotherID) from PD_FamilyMother where MotherAliveYNID  = 1 and FamilyID = " + FamilyID + "");
        int ChildCount = ReturnInt("Select Count(FamilyChildID) from PD_FamilyChild where IsActive  = 1 and FamilyID = " + FamilyID + "");
        return int.Parse((float.Parse(FamilyCount.ToString()) + float.Parse(ChildCount.ToString())).ToString());
    }


    public bool UpdateActiveMemberstoTeamProject(int FamilyID, int MotherStatusID)
    {
        try
        {
            int FamilyMember = GetActiveFamilyMembers(FamilyID, MotherStatusID);
            DbCommand dbCommand = db.GetSqlStringCommand("Update PD_TeamProject Set FamilyMembers = @FamilyMembers where FamilyID = @FamilyID ");
            dbCommand.Parameters.Add(new SqlParameter("@FamilyMembers", FamilyMember));
            dbCommand.Parameters.Add(new SqlParameter("@FamilyID", FamilyID));
            db.ExecuteNonQuery(dbCommand);
            return true;
        }
        catch
        {
            return false;
        }
    }


    public bool UpdateEmployeePassword(string Password, int ID)
    {
        try
        {
            DbCommand dbCommand = db.GetSqlStringCommand("Update Login Set Password = @Password where UserID = @ID ");
            dbCommand.Parameters.Add(new SqlParameter("@Password", Password));
            dbCommand.Parameters.Add(new SqlParameter("@ID", ID));
            db.ExecuteNonQuery(dbCommand);
            return true;
        }
        catch
        {
            return false;
        }
    }

    public bool UpdateFamilyUserPassword(string Password, int ID)
    {
        try
        {
            DbCommand dbCommand = db.GetSqlStringCommand("Update FamilyUser Set Password = @Password where FamilyUserID = @ID ");
            dbCommand.Parameters.Add(new SqlParameter("@Password", Password));
            dbCommand.Parameters.Add(new SqlParameter("@ID", ID));
            db.ExecuteNonQuery(dbCommand);
            return true;
        }
        catch
        {
            return false;
        }
    }

    public bool UpdateMemberUserPassword(string Password, int ID)
    {
        try
        {
            DbCommand dbCommand = db.GetSqlStringCommand("Update MemberUser Set Password = @Password where MemberUserID = @ID ");
            dbCommand.Parameters.Add(new SqlParameter("@Password", Password));
            dbCommand.Parameters.Add(new SqlParameter("@ID", ID));
            db.ExecuteNonQuery(dbCommand);
            return true;
        }
        catch
        {
            return false;
        }
    }

    public bool UpdateEmployeePortalPassword(string Password, int ID)
    {
        try
        {
            DbCommand dbCommand = db.GetSqlStringCommand("Update HR_Employee Set Password = @Password where Emp_EmployeeID = @ID ");
            dbCommand.Parameters.Add(new SqlParameter("@Password", Password));
            dbCommand.Parameters.Add(new SqlParameter("@ID", ID));
            db.ExecuteNonQuery(dbCommand);
            return true;
        }
        catch
        {
            return false;
        }
    }

    public bool Returnbol(string strQuery)
    {
        StringBuilder builder = new StringBuilder();
        DbCommand command;
        builder.Append(strQuery);

        try
        {
            command = db.GetSqlStringCommand(builder.ToString());
            return bool.Parse(db.ExecuteScalar(command).ToString());
        }
        catch (Exception ex)
        {
            return false;
        }

    }

    public DataSet ReturnBank(bool Status)
    {
        string strQuery = " ";
        if (Status == false)
        {
            strQuery = "SELECT [SettingsBankID] ,[BankName_Eng] as BankName ,[BankCode]   ,[TimeStamp] FROM [dbo].[SettingsBanks]";
            DbCommand dbCommand = db.GetSqlStringCommand(strQuery);

            return db.ExecuteDataSet(dbCommand);
        }
        else
        {
            strQuery = "SELECT [SettingsBankID] ,[BankName_t] as BankName, [BankCode]   ,[TimeStamp] FROM [dbo].[SettingsBanks]";
            DbCommand dbCommand = db.GetSqlStringCommand(strQuery);

            return db.ExecuteDataSet(dbCommand);
        }


    }

    public DataTable GetImageUploadRecordAppointmentVisit(int ID)
    {
        try
        {
            DbCommand dbCommand = db.GetSqlStringCommand("SELECT [AppointmentVisitImageID] as ID,[Doc] ,[FileType] as ContentType ,[Size] as Size   , [Doc] as Data,   [FileName] as Name FROM [dbo].[PD_AppointmentVisitImage] where [AppointmentVisitImageID] = @ID");
            dbCommand.Parameters.Add(new SqlParameter("@ID", ID));
            return db.ExecuteDataSet(dbCommand).Tables[0];
        }
        catch
        {
            return null;
        }
    }

    public DataTable GetImageUploadRecordEmpEducation(int ID)
    {
        try
        {
            DbCommand dbCommand = db.GetSqlStringCommand("SELECT EmpEducationID as ID,[CertImageContentType] as ContentType , [CertImage] as Data,   [CertImageName] as Name, [CertImageExt] as Ext FROM [dbo].[HR_EmpEducationDoc] where [EmpEducationID] = @ID");
            dbCommand.Parameters.Add(new SqlParameter("@ID", ID));
            return db.ExecuteDataSet(dbCommand).Tables[0];
        }
        catch
        {
            return null;
        }
    }

    public DataTable GetImageUploadRecordEmpExperience(int ID)
    {
        try
        {
            DbCommand dbCommand = db.GetSqlStringCommand("SELECT EmpExperienceID as ID,[CertImageContentType] as ContentType , [CertImage] as Data,   [CertImageName] as Name, [CertImageExt] as Ext FROM [dbo].[HR_EmpExperienceDoc] where [ExperienceID] = @ID");
            dbCommand.Parameters.Add(new SqlParameter("@ID", ID));
            return db.ExecuteDataSet(dbCommand).Tables[0];
        }
        catch
        {
            return null;
        }
    }


    public DataTable GetImageUploadRecordbyAppointmentID(int AppointmentID)
    {
        try
        {
            DbCommand dbCommand = db.GetSqlStringCommand("Select AppointmentVisitImageID AS ID,FileName AS NAME,FileType AS ContentType,Doc,Size,Description from PD_AppointmentVisitImage Where AppointmentID =" + AppointmentID.ToString());
            return db.ExecuteDataSet(dbCommand).Tables[0];
        }
        catch
        {
            return null;
        }
    }

    public DataTable GetNotesbyAppointmentID(int AppointmentVisitID)
    {
        try
        {
            DbCommand dbCommand = db.GetSqlStringCommand("Select  Notes from PD_AppointmentVisit Where AppointmentVisitID= " + AppointmentVisitID.ToString());
            return db.ExecuteDataSet(dbCommand).Tables[0];
        }
        catch
        {
            return null;
        }
    }

    //public HRSalarySuspension FetchinGrid(string strFetchforGrid, int ID)
    //{
    //    IDataReader reader = null;
    //    HRSalarySuspension hrsal = new HRSalarySuspension();
    //    DbCommand dbCommand = db.GetSqlStringCommand(strFetchforGrid);
    //    dbCommand.Parameters.Add(new SqlParameter("@ID", ID));

    //    reader = db.ExecuteReader(dbCommand);

    //    while (reader.Read())
    //    {

    //        hrsal.IssueNumber = reader[0].ToString();
    //        hrsal.IssueDate = reader[1].ToString();
    //        hrsal.LetterDate = reader[2].ToString();
    //        hrsal.LetterNo = reader[3].ToString();
    //        hrsal.AbsentStartDate = reader[4].ToString();
    //        hrsal.StopDate = reader[5].ToString();
    //        hrsal.Memo = reader[6].ToString();
    //        hrsal.Reason = reader[7].ToString();

    //        return hrsal;
    //    }
    //    return null;

    //}

    //public byte[] GetImageByte(FileUpload fileUpload)
    //{

    //    if (fileUpload.HasFile)
    //    {

    //        byte[] MainImage = new byte[fileUpload.PostedFile.InputStream.Length];
    //        ////store the currently selected file in memeory.
    //        //HttpPostedFile img = fileUpload.PostedFile;
    //        ////set the binary data .
    //        fileUpload.PostedFile.InputStream.Read(MainImage, 0, MainImage.Length);
    //        return MainImage;
    //    }
    //    else
    //    {
    //        return new byte[] { 0 };
    //    }
    //}

    //public byte[] GetImageByte(AsyncFileUpload fileUpload)
    //{

    //    if (fileUpload.HasFile)
    //    {

    //        byte[] MainImage = new byte[fileUpload.PostedFile.InputStream.Length];
    //        ////store the currently selected file in memeory.
    //        //HttpPostedFile img = fileUpload.PostedFile;
    //        ////set the binary data .
    //        fileUpload.PostedFile.InputStream.Read(MainImage, 0, MainImage.Length);
    //        return MainImage;
    //    }
    //    else
    //    {
    //        return new byte[] { 0 };
    //    }
    //}

    public bool UpdateCode(int Num, string VCode)
    {
        try
        {

            string cmd = "Update NextVNo set NextCode=@Code where VoucherCode = @VCode";
            DbCommand dbcommand = db.GetSqlStringCommand(cmd);
            dbcommand.Parameters.Add(new SqlParameter("@Code", Num));
            dbcommand.Parameters.Add(new SqlParameter("@VCode", VCode));
            db.ExecuteNonQuery(dbcommand);
            return true;
        }
        catch
        {
            return false;
        }
    }

    //Added By Bilal - Dated 22OCT2017
    //OrgMemberList uploaded files
    public DataTable GetOrgMemberFiles(int ProcessTypeID, int ProcessID, int SettingArchiveTypeID)
    {
        try
        {
            DbCommand dbCommand = db.GetSqlStringCommand(@"SELECT    dbo.AC_ArchiveMedia.ArchiveMediaID AS ID, dbo.AC_ArchiveMedia.Doc, dbo.AC_ArchiveMedia.FileType AS ContentType, dbo.AC_ArchiveMedia.Size, dbo.AC_ArchiveMedia.FileName AS Name 
														   FROM  dbo.AC_ArchiveMedia
														   WHERE dbo.AC_ArchiveMedia.ProcessID = " + ProcessID + " AND dbo.AC_ArchiveMedia.ProcessTypeID = " + ProcessTypeID + " and dbo.AC_ArchiveMedia.SettingsArchiveTypeID = " + SettingArchiveTypeID + "");
            return db.ExecuteDataSet(dbCommand).Tables[0];
        }
        catch
        {
            return null;
        }
    }

    public bool ExecuteNonQueryWithParam5(string strQuery, string Param1, string Param2, string Param3, string Param4, string Param5)
    {
        StringBuilder builder = new StringBuilder();
        DbCommand command;
        builder.Append(strQuery);

        try
        {
            command = db.GetSqlStringCommand(builder.ToString());
            command.Parameters.Add(new SqlParameter("@Param1", Param1));
            command.Parameters.Add(new SqlParameter("@Param2", Param2));
            command.Parameters.Add(new SqlParameter("@Param3", Param3));
            command.Parameters.Add(new SqlParameter("@Param4", Param4));
            command.Parameters.Add(new SqlParameter("@Param5", Param5));

            db.ExecuteNonQuery(command);
            return true;
        }
        catch
        {
            return false;
        }
    }

    #region Old Commented
    ////public class Utilities
    ////{
    ////    #region Private
    ////    private Database db = null;
    ////    #endregion

    ////    public Utilities()
    ////    {
    ////        db = DatabaseFactory.CreateDatabase("connectionstring");
    ////    }

    ////    public DataSet FetchinCombo(string strQuery)
    ////    {

    ////        DbCommand dbCommand = db.GetSqlStringCommand(strQuery);

    ////        return db.ExecuteDataSet(dbCommand);
    ////    }

    ////    public DataSet FetchinControl(string strQuery)
    ////    {

    ////        DbCommand dbCommand = db.GetSqlStringCommand(strQuery);

    ////        return db.ExecuteDataSet(dbCommand);
    ////    }

    ////    public DataTable FetchinControldt(string strQuery)
    ////    {
    ////        try
    ////        {
    ////            DbCommand dbCommand = db.GetSqlStringCommand(strQuery);

    ////            return db.ExecuteDataSet(dbCommand).Tables[0];
    ////        }
    ////        catch
    ////        {
    ////            return null;
    ////        }
    ////    }

    public DataTable FetchinControldtWithParam(string strQuery, string Param)
    {
        try
        {
            DbCommand dbCommand = db.GetSqlStringCommand(strQuery);
            dbCommand.Parameters.Add(new SqlParameter("@param", Param));
            return db.ExecuteDataSet(dbCommand).Tables[0];
        }
        catch
        {
            return null;
        }
    }


    ////    public static DataColumn AddDataColumn(string columnName, Type type, string header)
    ////    {
    ////        DataColumn dt = new DataColumn();
    ////        dt.ColumnName = columnName;
    ////        dt.DataType = type;
    ////        dt.Caption = header;
    ////        return dt;

    ////    }

    ////    //public int GetMaxPrimaryKey(string strQuery, string strFieldName)
    ////    //{

    ////    //    DataSet dsMaxItem = new DataSet();
    ////    //    int MaxItem;

    ////    //    DbCommand dbCommand = db.GetSqlStringCommand(strQuery);

    ////    //    dsMaxItem = db.ExecuteDataSet(dbCommand);

    ////    //    MaxItem = int.Parse(dsMaxItem.Tables[0].Rows[0][strFieldName].ToString());

    ////    //    return MaxItem + 1;

    ////    //}

    ////    public int GetMaxPrimaryKey(string strQuery, string strFieldName)
    ////    {

    ////        DataSet dsMaxItem = new DataSet();
    ////        int MaxItem;

    ////        DbCommand dbCommand = db.GetSqlStringCommand(strQuery);

    ////        dsMaxItem = db.ExecuteDataSet(dbCommand);

    ////        if (dsMaxItem.Tables[0].Rows[0][strFieldName] != DBNull.Value)
    ////        {
    ////            MaxItem = int.Parse(dsMaxItem.Tables[0].Rows[0][strFieldName].ToString());
    ////            return MaxItem + 1;
    ////        }
    ////        else
    ////            return 1;

    ////    }


    ////    public bool ExistorNot(string strQuery)
    ////    {
    ////        DataSet dsCheck = new DataSet();

    ////        DbCommand dbCommand = db.GetSqlStringCommand(strQuery);

    ////        dsCheck = db.ExecuteDataSet(dbCommand);

    ////        if (dsCheck.Tables[0].Rows.Count > 0)
    ////        {
    ////            return false;
    ////        }
    ////        else
    ////        {
    ////            return true;
    ////        }

    ////    }

    ////    public bool ExistorNotPara(string strQuery, string Value)
    ////    {
    ////        DataSet dsCheck = new DataSet();

    ////        DbCommand dbCommand = db.GetSqlStringCommand(strQuery);

    ////        dbCommand.Parameters.Add(new SqlParameter("@Value", Value));


    ////        dsCheck = db.ExecuteDataSet(dbCommand);

    ////        if (dsCheck.Tables[0].Rows.Count > 0)
    ////        {
    ////            return false;
    ////        }
    ////        else
    ////        {
    ////            return true;
    ////        }

    ////    }

    ////    public bool ExistEmailUserNameorNot(string UserName, string Email)
    ////    {
    ////        StringBuilder builder = new StringBuilder();
    ////        DbCommand command;
    ////        DataSet dsCheck = new DataSet();

    ////        builder.Append("Select Email FROM MemberUser where Email = @Email AND UserName = @UserName");

    ////        command = db.GetSqlStringCommand(builder.ToString());
    ////        command.Parameters.Add(new SqlParameter("@Email", Email));
    ////        command.Parameters.Add(new SqlParameter("@UserName", UserName));

    ////        dsCheck = db.ExecuteDataSet(command);

    ////        if (dsCheck.Tables[0].Rows.Count > 0)
    ////        {
    ////            return true;
    ////        }
    ////        else
    ////        {
    ////            return false;
    ////        }

    ////    }

    ////    public int MemberUserId(string UserName)//, string Email)
    ////    {
    ////        StringBuilder builder = new StringBuilder();
    ////        DbCommand command;
    ////        DataSet dsCheck = new DataSet();

    ////        //builder.Append("Select MemberUserId FROM MemberUser where Email = @Email AND UserName = @UserName");
    ////        builder.Append("Select MemberUserId FROM MemberUser where UserName = @UserName");

    ////        command = db.GetSqlStringCommand(builder.ToString());
    ////        //command.Parameters.Add(new SqlParameter("@Email", Email));
    ////        command.Parameters.Add(new SqlParameter("@UserName", UserName));

    ////        dsCheck = db.ExecuteDataSet(command);

    ////        if (dsCheck.Tables[0].Rows.Count > 0)
    ////        {
    ////            return Convert.ToInt32(dsCheck.Tables[0].Rows[0]["MemberUserID"]);
    ////        }
    ////        else
    ////        {
    ////            return 0;
    ////        }

    ////    }

    ////    public int MemberInfoId(int MemberUserID)//, string Email)
    ////    {
    ////        StringBuilder builder = new StringBuilder();
    ////        DbCommand command;
    ////        DataSet dsCheck = new DataSet();

    ////        //builder.Append("Select MemberUserId FROM MemberUser where Email = @Email AND UserName = @UserName");
    ////        builder.Append("Select MemberInfoID FROM PD_MemberInfo where MemberUserID = @MemberUserID");

    ////        command = db.GetSqlStringCommand(builder.ToString());
    ////        //command.Parameters.Add(new SqlParameter("@Email", Email));
    ////        command.Parameters.Add(new SqlParameter("@MemberUserID", MemberUserID));

    ////        dsCheck = db.ExecuteDataSet(command);

    ////        if (dsCheck.Tables[0].Rows.Count > 0)
    ////        {
    ////            return Convert.ToInt32(dsCheck.Tables[0].Rows[0]["MemberInfoID"]);
    ////        }
    ////        else
    ////        {
    ////            return 0;
    ////        }

    ////    }

    ////    public int DonatorPersonId(int MemberUserID)//, string Email)
    ////    {
    ////        StringBuilder builder = new StringBuilder();
    ////        DbCommand command;
    ////        DataSet dsCheck = new DataSet();

    ////        //builder.Append("Select MemberUserId FROM MemberUser where Email = @Email AND UserName = @UserName");
    ////        builder.Append("Select DonatePersonID FROM PD_DonatePerson where DonationUserID = @MemberUserID");

    ////        command = db.GetSqlStringCommand(builder.ToString());
    ////        //command.Parameters.Add(new SqlParameter("@Email", Email));
    ////        command.Parameters.Add(new SqlParameter("@MemberUserID", MemberUserID));

    ////        dsCheck = db.ExecuteDataSet(command);

    ////        if (dsCheck.Tables[0].Rows.Count > 0)
    ////        {
    ////            return Convert.ToInt32(dsCheck.Tables[0].Rows[0]["DonatePersonID"]);
    ////        }
    ////        else
    ////        {
    ////            return 0;
    ////        }

    ////    }

    ////    //public static bool IsAuthenticated(string url)
    ////    //{
    ////    //    bool status = false;
    ////    //    string[] arr = url.Split('/');

    ////    //    string[] arr2 = arr[arr.Length - 1].Split('?');

    ////    //    Dictionary<string, AccessRightsSystem> dictForm = (Dictionary<string, AccessRightsSystem>)System.Web.HttpContext.Current.Session["Dict"];

    ////    //    Dictionary<string, AccessRightsSystem> dictFormEx = (Dictionary<string, AccessRightsSystem>)System.Web.HttpContext.Current.Session["DictEx"];

    ////    //    if (dictForm != null)
    ////    //    {
    ////    //        if (dictForm.Count > 0)
    ////    //        {
    ////    //            if (dictForm.ContainsKey(arr2[0]) || dictFormEx.ContainsKey(arr2[0]))
    ////    //            {
    ////    //                status = true;
    ////    //            }

    ////    //        }
    ////    //    }




    ////    //    return status;
    ////    //}


    ////    public DataSet CheckButtonStatus(string url, int empid)
    ////    {
    ////        string[] arr = url.Split('/');

    ////        string[] arr2 = arr[arr.Length - 1].Split('?');

    ////        url = arr2[0];

    ////        StringBuilder builder = new StringBuilder();
    ////        builder.Append(" SELECT AR_Add, AR_Edit, AR_Delete, AR_View, AR_Approve FROM CR_AccessRights Where EmployeeID = @EmployeeID and  AR_PageURL = @URL");
    ////        DbCommand command = db.GetSqlStringCommand(builder.ToString());
    ////        command.Parameters.Add(new SqlParameter("@EmployeeID", empid));
    ////        command.Parameters.Add(new SqlParameter("@URL", url));

    ////        return db.ExecuteDataSet(command);


    ////    }

    ////    public string NowDateHijri()
    ////    {
    ////        DateTime dt;
    ////        System.Globalization.DateTimeFormatInfo hijriDate;

    ////        dt = Convert.ToDateTime(DateTime.Now.ToString());
    ////        hijriDate = new System.Globalization.CultureInfo("ar-SA", false).DateTimeFormat;
    ////        hijriDate.Calendar = new System.Globalization.HijriCalendar();
    ////        hijriDate.ShortDatePattern = "dd/MM/yyyy";
    ////        hijriDate.LongDatePattern = "dd/MM/yyyy";
    ////        //string date = dt.ToString("f", hijriDate);

    ////        string date = dt.ToShortDateString();

    ////        string strNowDate;

    ////        //strNowDate = date.Substring(0, 2) + "/" + date.Substring(date.Length - 15, 2) + "/" + date.Substring(date.Length - 12, 4);

    ////        strNowDate = date.Substring(0, 2) + "/" + date.Substring(date.Length - 5, 2) + "/" + "14" + date.Substring(date.Length - 2, 2);

    ////        return strNowDate;

    ////    }

    ////    public DataSet GetRFQ()
    ////    {
    ////        StringBuilder builder = new StringBuilder();
    ////        DbCommand command;

    ////        builder.Append(" SELECT RFQID , 'RFQ-' + CAST(RFQID AS VARCHAR(50)) RFQName  FROM RFQ ");

    ////        try
    ////        {
    ////            command = db.GetSqlStringCommand(builder.ToString());

    ////        }
    ////        catch (Exception ex)
    ////        {
    ////            return null;
    ////        }
    ////        return db.ExecuteDataSet(command);
    ////    }


    ////    public string GetItemPrice(string strIC)
    ////    {
    ////        StringBuilder builder = new StringBuilder();
    ////        DbCommand command;
    ////        builder.Append(" SELECT ItemPrice from Item Where ItemCode = @ItemCode");

    ////        try
    ////        {
    ////            command = db.GetSqlStringCommand(builder.ToString());
    ////            command.Parameters.Add(new SqlParameter("@ItemCode", strIC));

    ////        }
    ////        catch (Exception ex)
    ////        {
    ////            return "";
    ////        }
    ////        return db.ExecuteScalar(command).ToString();
    ////    }

    ////    public bool CheckRFQforComm(int RFQID, int CommTypeID)
    ////    {
    ////        StringBuilder builder = new StringBuilder();
    ////        DbCommand command;
    ////        DataSet dsCheck = new DataSet();

    ////        builder.Append(" SELECT RFQID from IC_CommRFQEmp Where CommissionTypeID = @CTID and RFQID = @RFQID");

    ////        command = db.GetSqlStringCommand(builder.ToString());
    ////        command.Parameters.Add(new SqlParameter("@RFQID", RFQID));
    ////        command.Parameters.Add(new SqlParameter("@CTID", CommTypeID));

    ////        dsCheck = db.ExecuteDataSet(command);

    ////        if (dsCheck.Tables[0].Rows.Count > 0)
    ////        {
    ////            return true;
    ////        }
    ////        else
    ////        {
    ////            return false;
    ////        }

    ////    }


    ////    public void SendEmail(string toemail, string subject, string body)
    ////    {




    ////        SmtpClient smtpClient = new SmtpClient();
    ////        MailMessage message = new MailMessage();


    ////        MailAddress fromAddress = new MailAddress("gic@arabsea.com", "GIC");

    ////        // You can specify the host name or ipaddress of your server
    ////        // Default in IIS will be localhost 
    ////        smtpClient.Host = "localhost";

    ////        //Default port will be 25
    ////        smtpClient.Port = 25;

    ////        //From address will be given as a MailAddress Object
    ////        message.From = fromAddress;

    ////        // To address collection of MailAddress
    ////        message.To.Add("gic@arabsea.com");//gic@arabsea.com");
    ////        message.Subject = "GIC - Error - " + subject;

    ////        // CC and BCC optional
    ////        // MailAddressCollection class is used to send the email to various users
    ////        // You can specify Address as new MailAddress("admin1@yoursite.com")
    ////        //message.CC.Add("gic@arabsea.com");
    ////        //message.CC.Add("webmaster@arabsea.com");

    ////        // You can specify Address directly as string
    ////        //message.Bcc.Add(new MailAddress(""));
    ////        //message.Bcc.Add(new MailAddress(""));

    ////        //Body can be Html or text format
    ////        //Specify true if it is html message
    ////        message.IsBodyHtml = true;


    ////        // Message body content
    ////        message.Body = body;

    ////        System.Net.NetworkCredential SMTPUserInfo = new System.Net.NetworkCredential("admin@gic.cc", "arabsea123");
    ////        //System.Net.NetworkCredential SMTPUserInfo = new System.Net.NetworkCredential("gic@arabsea.com", "g1c");
    ////        smtpClient.UseDefaultCredentials = false;
    ////        smtpClient.Credentials = SMTPUserInfo;

    ////        // Send SMTP mail
    ////        smtpClient.Send(message);





    ////    }


    ////    public int ReturnInt(string strQuery)
    ////    {
    ////        StringBuilder builder = new StringBuilder();
    ////        DbCommand command;
    ////        builder.Append(strQuery);

    ////        try
    ////        {
    ////            command = db.GetSqlStringCommand(builder.ToString());
    ////            return int.Parse(db.ExecuteScalar(command).ToString());
    ////        }
    ////        catch (Exception ex)
    ////        {
    ////            return 0;
    ////        }

    ////    }

    ////    public float Returnflt(string strQuery)
    ////    {
    ////        StringBuilder builder = new StringBuilder();
    ////        DbCommand command;
    ////        builder.Append(strQuery);

    ////        try
    ////        {
    ////            command = db.GetSqlStringCommand(builder.ToString());
    ////            return float.Parse(db.ExecuteScalar(command).ToString());
    ////        }
    ////        catch (Exception ex)
    ////        {
    ////            return 0.0F;
    ////        }

    ////    }


    ////    public decimal ReturnDec(string strQuery)
    ////    {
    ////        StringBuilder builder = new StringBuilder();
    ////        DbCommand command;
    ////        builder.Append(strQuery);

    ////        try
    ////        {
    ////            command = db.GetSqlStringCommand(builder.ToString());
    ////            return decimal.Parse(db.ExecuteScalar(command).ToString());
    ////        }
    ////        catch (Exception ex)
    ////        {
    ////            return 0.0M;
    ////        }

    ////    }

    ////    public string ReturnStr(string strQuery)
    ////    {
    ////        StringBuilder builder = new StringBuilder();
    ////        DbCommand command;
    ////        builder.Append(strQuery);

    ////        try
    ////        {
    ////            command = db.GetSqlStringCommand(builder.ToString());
    ////            return db.ExecuteScalar(command).ToString();
    ////        }
    ////        catch (Exception ex)
    ////        {
    ////            return "";
    ////        }

    ////    }


    ////    public DateTime Returndt(string strQuery)
    ////    {
    ////        StringBuilder builder = new StringBuilder();
    ////        DbCommand command;
    ////        builder.Append(strQuery);

    ////        try
    ////        {
    ////            command = db.GetSqlStringCommand(builder.ToString());

    ////        }
    ////        catch (Exception ex)
    ////        {
    ////            return DateTime.Parse("0");
    ////        }
    ////        return DateTime.Parse(db.ExecuteScalar(command).ToString());
    ////    }


    ////    public int Check_IssueNumberAndDate(string table, string PKeyField, string pKeyValue)
    ////    {
    ////        DbCommand command;

    ////        string strSP = "Check_IssueNumberAndDate";

    ////        command = db.GetStoredProcCommand(strSP);
    ////        command.Parameters.Add(new SqlParameter("@table", table));
    ////        command.Parameters.Add(new SqlParameter("@PKeyField", PKeyField));
    ////        command.Parameters.Add(new SqlParameter("@PKeyValue", pKeyValue));


    ////        DataSet ds = db.ExecuteDataSet(command);

    ////        if (ds != null)
    ////        {
    ////            if (ds.Tables.Count > 0)
    ////                if (ds.Tables[0].Rows.Count > 0)
    ////                    return (int.Parse(ds.Tables[0].Rows[0]["count"].ToString()));
    ////        }
    ////        return 0;

    ////    }

    ////    public int CheckOverlappingEdit(int conID, DateTime StartDate, DateTime EndDate)
    ////    {

    ////        int nResult = -1;

    ////        try
    ////        {
    ////            string cmd = "Check_OverlappingBetweenDates";
    ////            DbCommand command = db.GetStoredProcCommand(cmd);
    ////            command.Parameters.Add(new SqlParameter("@ContractID", conID));
    ////            command.Parameters.Add(new SqlParameter("@StartDate", StartDate));
    ////            command.Parameters.Add(new SqlParameter("@EndDate", EndDate));

    ////            nResult = int.Parse(db.ExecuteScalar(command).ToString());
    ////            if (nResult >= 0)
    ////                return nResult;
    ////        }
    ////        catch (Exception ex)
    ////        {
    ////            return -1;
    ////        }
    ////        return -1;
    ////    }

    ////    public int CheckOverlappingContEdit(int conID, DateTime StartDate, DateTime EndDate)
    ////    {

    ////        int nResult = -1;

    ////        try
    ////        {
    ////            string cmd = "Check_OverlappingBetweenContDates";
    ////            DbCommand command = db.GetStoredProcCommand(cmd);
    ////            command.Parameters.Add(new SqlParameter("@ContractID", conID));
    ////            command.Parameters.Add(new SqlParameter("@StartDate", StartDate));
    ////            command.Parameters.Add(new SqlParameter("@EndDate", EndDate));

    ////            nResult = int.Parse(db.ExecuteScalar(command).ToString());
    ////            if (nResult >= 0)
    ////                return nResult;
    ////        }
    ////        catch (Exception ex)
    ////        {
    ////            return -1;
    ////        }
    ////        return -1;
    ////    }

    ////    public int GetEmployeeStatusByContractID(int ContractID)
    ////    {
    ////        StringBuilder builder = new StringBuilder();
    ////        DbCommand command;
    ////        builder.Append(" SELECT     HR_Employee.SettingsEmployeeStatusTypeID FROM  HR_Contracts INNER JOIN ");
    ////        builder.Append("  HR_Employee ON HR_Contracts.Emp_EmployeeID = HR_Employee.Emp_EmployeeID where ContractID = " + ContractID);
    ////        try
    ////        {
    ////            command = db.GetSqlStringCommand(builder.ToString());
    ////        }
    ////        catch (Exception ex)
    ////        {
    ////            return 0;
    ////        }
    ////        return int.Parse(db.ExecuteScalar(command).ToString());
    ////    }


    ////    //public HRSalarySuspension FetchinGrid(string strFetchforGrid, int ID)
    ////    //{
    ////    //    IDataReader reader = null;
    ////    //    HRSalarySuspension hrsal = new HRSalarySuspension();
    ////    //    DbCommand dbCommand = db.GetSqlStringCommand(strFetchforGrid);
    ////    //    dbCommand.Parameters.Add(new SqlParameter("@ID", ID));

    ////    //    reader = db.ExecuteReader(dbCommand);

    ////    //    while (reader.Read())
    ////    //    {

    ////    //        hrsal.IssueNumber = reader[0].ToString();
    ////    //        hrsal.IssueDate = reader[1].ToString();
    ////    //        hrsal.LetterDate = reader[2].ToString();
    ////    //        hrsal.LetterNo = reader[3].ToString();
    ////    //        hrsal.AbsentStartDate = reader[4].ToString();
    ////    //        hrsal.StopDate = reader[5].ToString();
    ////    //        hrsal.Memo = reader[6].ToString();
    ////    //        hrsal.Reason = reader[7].ToString();

    ////    //        return hrsal;
    ////    //    }
    ////    //    return null;

    ////    //}
    ////    public string CheckDate(string strDate)
    ////    {
    ////        StringBuilder builder = new StringBuilder();
    ////        DbCommand command;
    ////        string[] Date = strDate.Split('/');
    ////        string strDay, strMonth, strYear, DtDate;
    ////        int preday = 0;
    ////        try
    ////        {

    ////            strDay = Date[0].ToString();
    ////            strMonth = Date[1].ToString();
    ////            strYear = Date[2].ToString();
    ////            DtDate = strYear + strMonth + strDay;
    ////            builder.Append(" Select convert(smalldatetime,'" + DtDate + "',131)");


    ////            command = db.GetSqlStringCommand(builder.ToString());
    ////            db.ExecuteScalar(command).ToString();
    ////            return strDate;
    ////        }
    ////        catch (Exception ex)
    ////        {
    ////            preday = int.Parse(Date[0].ToString()) - 1;
    ////            return preday.ToString() + "/" + Date[1].ToString() + "/" + Date[2].ToString();
    ////        }
    ////        //return db.ExecuteScalar(command).ToString();
    ////    }
    ////    public void SetEmployeeStatus(int intID, int intStatus)
    ////    {
    ////        try
    ////        {
    ////            DbCommand command = db.GetSqlStringCommand(" Update HR_Employee set SettingsEmployeeStatusTypeID = @Status where  Emp_EmployeeID = @EmpID ");
    ////            command.Parameters.Add(new SqlParameter("@EmpID", intID));
    ////            command.Parameters.Add(new SqlParameter("@Status", intStatus));
    ////            db.ExecuteNonQuery(command);
    ////        }
    ////        catch (Exception ex)
    ////        {

    ////        }
    ////    }


    ////    public string GetSQLInject(string txtString)
    ////    {


    ////        if (txtString.Contains(" OR ")

    ////            || txtString.Contains("OR")
    ////            || txtString.Contains("or")
    ////            || txtString.Contains("Or")
    ////            || txtString.Contains("oR")

    ////            || txtString.Contains(" -- ")
    ////            || txtString.Contains(" --")
    ////            || txtString.Contains(" /* ")
    ////            || txtString.Contains(" /*")
    ////            || txtString.Contains(" ; ")
    ////            || txtString.Contains(" ;")
    ////            || txtString.Contains(" ;--")
    ////            || txtString.Contains(" ;-- ")

    ////            )

    ////            return "0";
    ////        else
    ////            return txtString;

    ////    }

    ////    //public byte[] GetImageByte(FileUpload fileUpload)
    ////    //{

    ////    //    if (fileUpload.HasFile)
    ////    //    {

    ////    //        byte[] MainImage = new byte[fileUpload.PostedFile.InputStream.Length];
    ////    //        ////store the currently selected file in memeory.
    ////    //        //HttpPostedFile img = fileUpload.PostedFile;
    ////    //        ////set the binary data .
    ////    //        fileUpload.PostedFile.InputStream.Read(MainImage, 0, MainImage.Length);
    ////    //        return MainImage;
    ////    //    }
    ////    //    else
    ////    //    {
    ////    //        return new byte[] { 0 };
    ////    //    }
    ////    //}

    ////    //public byte[] GetImageByte(AsyncFileUpload fileUpload)
    ////    //{

    ////    //    if (fileUpload.HasFile)
    ////    //    {

    ////    //        byte[] MainImage = new byte[fileUpload.PostedFile.InputStream.Length];
    ////    //        ////store the currently selected file in memeory.
    ////    //        //HttpPostedFile img = fileUpload.PostedFile;
    ////    //        ////set the binary data .
    ////    //        fileUpload.PostedFile.InputStream.Read(MainImage, 0, MainImage.Length);
    ////    //        return MainImage;
    ////    //    }
    ////    //    else
    ////    //    {
    ////    //        return new byte[] { 0 };
    ////    //    }
    ////    //}

    ////    public bool ExecuteNonQuery(string strQuery)
    ////    {
    ////        try
    ////        {
    ////            DbCommand dbCommand = db.GetSqlStringCommand(strQuery);

    ////            db.ExecuteNonQuery(dbCommand);
    ////            return true;
    ////        }
    ////        catch
    ////        {
    ////            return false;
    ////        }
    ////    }


    ////    public int GetEmpbyHR(string ID)
    ////    {
    ////        StringBuilder builder = new StringBuilder();
    ////        DbCommand command;

    ////        builder.Append(" SELECT     dbo.HR_Employee.Emp_EmployeeID FROM         dbo.Users INNER JOIN ");
    ////        builder.Append(" dbo.HR_Employee ON dbo.Users.EmployeeGlobalID = dbo.HR_Employee.IDDocumentNumber WHERE     (dbo.HR_Employee.IDDocumentNumber = '" + ID + "')");

    ////        try
    ////        {
    ////            command = db.GetSqlStringCommand(builder.ToString());
    ////            return int.Parse(db.ExecuteScalar(command).ToString());
    ////        }
    ////        catch (Exception ex)
    ////        {
    ////            return 0;
    ////        }

    ////    }

    ////    public bool EexecuteNonQuery(string strQuery)
    ////    {
    ////        try
    ////        {
    ////            DbCommand dbCommand = db.GetSqlStringCommand(strQuery);

    ////            db.ExecuteNonQuery(dbCommand);
    ////            return true;
    ////        }
    ////        catch
    ////        {
    ////            return false;
    ////        }
    ////    }


    ////    public static bool IsAuthenticated(string url)
    ////    {
    ////        //bool status = false;
    ////        //string[] arr = url.Split('/');

    ////        //string[] arr2 = arr[arr.Length - 1].Split('?');

    ////        //Dictionary<string, AccessRightsSystem> dictForm = (Dictionary<string, AccessRightsSystem>)System.Web.HttpContext.Current.Session["Dict"];

    ////        //Dictionary<string, AccessRightsSystem> dictFormEx = (Dictionary<string, AccessRightsSystem>)System.Web.HttpContext.Current.Session["DictEx"];

    ////        //if (dictForm != null)
    ////        //{
    ////        //    if (dictForm.Count > 0)
    ////        //    {
    ////        //        if (dictForm.ContainsKey(arr2[0]) || dictFormEx.ContainsKey(arr2[0]))
    ////        //        {
    ////        //            status = true;
    ////        //        }

    ////        //    }
    ////        //}




    ////        //return status;

    ////        return true;
    ////    }


    ////    public DataTable ImageUploadRecord(int TypeID, int DocumentTypeID)
    ////    {
    ////        try
    ////        {
    ////            DbCommand dbCommand = db.GetSqlStringCommand(@"SELECT     dbo.AC_ArchiveMedia.ArchiveMediaID AS ID, dbo.AC_ArchiveMedia.Doc, dbo.AC_ArchiveMedia.FileType AS ContentType, dbo.AC_ArchiveMedia.Size, dbo.AC_ArchiveMedia.FileName AS Name
    ////FROM         dbo.AC_ArchiveMedia INNER JOIN
    ////                      dbo.PD_Family ON dbo.AC_ArchiveMedia.UserID = dbo.PD_Family.FamilyUserID where dbo.AC_ArchiveMedia.ProcessTypeID = " + TypeID + " and dbo.AC_ArchiveMedia.SettingsArchiveTypeID = " + DocumentTypeID + " ");
    ////            return db.ExecuteDataSet(dbCommand).Tables[0];
    ////        }
    ////        catch
    ////        {
    ////            return null;
    ////        }
    ////    }

    ////    public DataTable ImageUploadMainServiceRecord(int TypeID, int DocumentTypeID, int serviceTypeID)
    ////    {
    ////        try
    ////        {
    ////            DbCommand dbCommand = db.GetSqlStringCommand(@"SELECT        dbo.AC_ArchiveMedia.ArchiveMediaID AS ID, dbo.AC_ArchiveMedia.Doc, dbo.AC_ArchiveMedia.FileType AS ContentType, dbo.AC_ArchiveMedia.Size, dbo.AC_ArchiveMedia.FileName AS Name
    ////FROM            dbo.AC_ArchiveMedia INNER JOIN
    ////                         dbo.PD_ServiceRequest ON dbo.AC_ArchiveMedia.ProcessID = dbo.PD_ServiceRequest.ServiceRequestID
    ////where dbo.PD_ServiceRequest.ServiceID = " + serviceTypeID + " and dbo.AC_ArchiveMedia.ProcessTypeID = " + TypeID + " and dbo.AC_ArchiveMedia.SettingsArchiveTypeID = " + DocumentTypeID + "");
    ////            return db.ExecuteDataSet(dbCommand).Tables[0];
    ////        }
    ////        catch
    ////        {
    ////            return null;
    ////        }
    ////    }

    ////    public DataTable ImageUploadSubServiceRecord(int TypeID, int DocumentTypeID, int MainserviceTypeID)
    ////    {
    ////        try
    ////        {
    ////            DbCommand dbCommand = db.GetSqlStringCommand(@"SELECT        dbo.AC_ArchiveMedia.ArchiveMediaID AS ID, dbo.AC_ArchiveMedia.Doc, dbo.AC_ArchiveMedia.FileType AS ContentType, dbo.AC_ArchiveMedia.Size, dbo.AC_ArchiveMedia.FileName AS Name
    ////FROM            dbo.AC_ArchiveMedia INNER JOIN
    ////                         dbo.PD_ServiceRequest ON dbo.AC_ArchiveMedia.ProcessID = dbo.PD_ServiceRequest.ServiceRequestID
    ////						 where dbo.PD_ServiceRequest.MainServiceID = " + MainserviceTypeID + " and dbo.AC_ArchiveMedia.ProcessTypeID = " + TypeID + " and dbo.AC_ArchiveMedia.SettingsArchiveTypeID = " + DocumentTypeID + "");
    ////            return db.ExecuteDataSet(dbCommand).Tables[0];
    ////        }
    ////        catch
    ////        {
    ////            return null;
    ////        }
    ////    }

    ////    public DataTable GetImageUploadRecord(int ID)
    ////    {
    ////        try
    ////        {
    ////            DbCommand dbCommand = db.GetSqlStringCommand("SELECT [ArchiveMediaID] as ID,[Doc] ,[FileType] as ContentType ,[Size] as Size   , [Doc] as Data,   [FileName] as Name FROM [dbo].[AC_ArchiveMedia] where dbo.AC_ArchiveMedia.ArchiveMediaID = " + ID + "");
    ////            return db.ExecuteDataSet(dbCommand).Tables[0];
    ////        }
    ////        catch
    ////        {
    ////            return null;
    ////        }
    ////    }

    ////    public DataTable ImageUploadFatherRecord(int TypeID, int DocumentTypeID, int FamilyID)
    ////    {
    ////        try
    ////        {
    ////            DbCommand dbCommand = db.GetSqlStringCommand(@"SELECT        dbo.AC_ArchiveMedia.ArchiveMediaID AS ID, dbo.AC_ArchiveMedia.Doc, dbo.AC_ArchiveMedia.FileType AS ContentType, dbo.AC_ArchiveMedia.Size, dbo.AC_ArchiveMedia.FileName AS Name
    ////FROM            dbo.AC_ArchiveMedia INNER JOIN
    ////                         dbo.PD_FamilyFather ON dbo.AC_ArchiveMedia.ProcessID = dbo.PD_FamilyFather.FamilyFatherID where dbo.AC_ArchiveMedia.ProcessTypeID = " + TypeID + " and dbo.AC_ArchiveMedia.SettingsArchiveTypeID = " + DocumentTypeID + " and dbo.AC_ArchiveMedia.FamilyID = " + FamilyID + " ");
    ////            return db.ExecuteDataSet(dbCommand).Tables[0];
    ////        }
    ////        catch
    ////        {
    ////            return null;
    ////        }
    ////    }

    ////    public DataTable ImageUploadMotherRecord(int TypeID, int DocumentTypeID, int FamilyID)
    ////    {
    ////        try
    ////        {
    ////            DbCommand dbCommand = db.GetSqlStringCommand(@"SELECT        dbo.AC_ArchiveMedia.ArchiveMediaID AS ID, dbo.AC_ArchiveMedia.Doc, dbo.AC_ArchiveMedia.FileType AS ContentType, dbo.AC_ArchiveMedia.Size, dbo.AC_ArchiveMedia.FileName AS Name
    ////FROM            dbo.AC_ArchiveMedia INNER JOIN
    ////                         dbo.PD_FamilyMother ON dbo.AC_ArchiveMedia.ProcessID = dbo.PD_FamilyMother.FamilyMotherID where dbo.AC_ArchiveMedia.ProcessTypeID = " + TypeID + " and dbo.AC_ArchiveMedia.SettingsArchiveTypeID = " + DocumentTypeID + " and dbo.AC_ArchiveMedia.FamilyID = " + FamilyID + "  ");
    ////            return db.ExecuteDataSet(dbCommand).Tables[0];
    ////        }
    ////        catch
    ////        {
    ////            return null;
    ////        }
    ////    }

    ////    public DataTable ImageUploadChildRecord(int TypeID, int DocumentTypeID, int FamilyID)
    ////    {
    ////        try
    ////        {
    ////            DbCommand dbCommand = db.GetSqlStringCommand(@"SELECT        dbo.AC_ArchiveMedia.ArchiveMediaID AS ID, dbo.AC_ArchiveMedia.Doc, dbo.AC_ArchiveMedia.FileType AS ContentType, dbo.AC_ArchiveMedia.Size, dbo.AC_ArchiveMedia.FileName AS Name
    ////FROM            dbo.AC_ArchiveMedia INNER JOIN
    ////                         dbo.PD_FamilyChild ON dbo.AC_ArchiveMedia.ProcessID = dbo.PD_FamilyChild.FamilyChildID where dbo.AC_ArchiveMedia.ProcessTypeID = " + TypeID + " and dbo.AC_ArchiveMedia.SettingsArchiveTypeID = " + DocumentTypeID + " and dbo.AC_ArchiveMedia.FamilyID = " + FamilyID + "  ");
    ////            return db.ExecuteDataSet(dbCommand).Tables[0];
    ////        }
    ////        catch
    ////        {
    ////            return null;
    ////        }
    ////    }

    ////    public DataTable ImageUploadChildRecordwithChildID(int TypeID, int ChildID, int DocumentTypeID, int FamilyID)
    ////    {
    ////        try
    ////        {
    ////            DbCommand dbCommand = db.GetSqlStringCommand(@"SELECT        dbo.AC_ArchiveMedia.ArchiveMediaID AS ID, dbo.AC_ArchiveMedia.Doc, dbo.AC_ArchiveMedia.FileType AS ContentType, dbo.AC_ArchiveMedia.Size, dbo.AC_ArchiveMedia.FileName AS Name
    ////FROM            dbo.AC_ArchiveMedia INNER JOIN
    ////                         dbo.PD_FamilyChild ON dbo.AC_ArchiveMedia.ProcessID = dbo.PD_FamilyChild.FamilyChildID where dbo.AC_ArchiveMedia.ProcessID = " + ChildID + " and  dbo.AC_ArchiveMedia.ProcessTypeID = " + TypeID + " and dbo.AC_ArchiveMedia.SettingsArchiveTypeID = " + DocumentTypeID + " and dbo.AC_ArchiveMedia.FamilyID = " + FamilyID + "  ");
    ////            return db.ExecuteDataSet(dbCommand).Tables[0];
    ////        }
    ////        catch
    ////        {
    ////            return null;
    ////        }
    ////    }

    ////    public DataTable ImageUploadfamilyRecord(int TypeID, int DocumentTypeID, int FamilyID)
    ////    {
    ////        try
    ////        {
    ////            DbCommand dbCommand = db.GetSqlStringCommand(@"SELECT        dbo.AC_ArchiveMedia.ArchiveMediaID AS ID, dbo.AC_ArchiveMedia.Doc, dbo.AC_ArchiveMedia.FileType AS ContentType, dbo.AC_ArchiveMedia.Size, dbo.AC_ArchiveMedia.FileName AS Name
    ////FROM            dbo.AC_ArchiveMedia INNER JOIN
    ////                         dbo.PD_Family ON dbo.AC_ArchiveMedia.ProcessID = dbo.PD_Family.FamilyID where dbo.AC_ArchiveMedia.ProcessTypeID = " + TypeID + " and dbo.AC_ArchiveMedia.SettingsArchiveTypeID = " + DocumentTypeID + " and dbo.AC_ArchiveMedia.FamilyID = " + FamilyID + "  ");
    ////            return db.ExecuteDataSet(dbCommand).Tables[0];
    ////        }
    ////        catch
    ////        {
    ////            return null;
    ////        }
    ////    }


    ////    public DataTable GetImageUploadRecordbyFamilyID(int ProcessTypeID, int ProcessID, int FamilyID, int SettingArchiveTypeID)
    ////    {
    ////        try
    ////        {
    ////            DbCommand dbCommand = db.GetSqlStringCommand(@"SELECT        dbo.AC_ArchiveMedia.ArchiveMediaID AS ID, dbo.AC_ArchiveMedia.Doc, dbo.AC_ArchiveMedia.FileType AS ContentType, dbo.AC_ArchiveMedia.Size, dbo.AC_ArchiveMedia.FileName AS Name, dbo.AC_ArchiveMedia.Description
    ////FROM            dbo.AC_ArchiveMedia where 
    ////dbo.AC_ArchiveMedia.ProcessTypeID = " + ProcessTypeID + " and dbo.AC_ArchiveMedia.ProcessID = " + ProcessID + " and dbo.AC_ArchiveMedia.FamilyID = " + FamilyID + " and dbo.AC_ArchiveMedia.SettingsArchiveTypeID = " + SettingArchiveTypeID + "");
    ////            return db.ExecuteDataSet(dbCommand).Tables[0];
    ////        }
    ////        catch
    ////        {
    ////            return null;
    ////        }
    ////    }

    ////    public DataTable GetTransportImageUploadRecordbyFamilyID(int ProcessTypeID, int ProcessID, int FamilyID, int SettingArchiveTypeID, int FamilyPersonId) // 81,82,83 for Doc Uploaded by Daughter in Transport
    ////    {
    ////        try
    ////        {
    ////            DbCommand dbCommand = db.GetSqlStringCommand(@"SELECT        dbo.AC_ArchiveMedia.ArchiveMediaID AS ID, dbo.AC_ArchiveMedia.Doc, dbo.AC_ArchiveMedia.FileType AS ContentType, dbo.AC_ArchiveMedia.Size, dbo.AC_ArchiveMedia.FileName AS Name, dbo.AC_ArchiveMedia.Description
    ////FROM            dbo.AC_ArchiveMedia where 
    ////dbo.AC_ArchiveMedia.ProcessTypeID = " + ProcessTypeID + " and dbo.AC_ArchiveMedia.ProcessID = " + ProcessID + " and dbo.AC_ArchiveMedia.FamilyID = " + FamilyID + " and FamilyPersonID = " + FamilyPersonId + " and dbo.AC_ArchiveMedia.SettingsArchiveTypeID in (81,82,83)");
    ////            return db.ExecuteDataSet(dbCommand).Tables[0];
    ////        }
    ////        catch
    ////        {
    ////            return null;
    ////        }
    ////    }

    ////    public int GetActiveFamilyMembers(int FamilyID, int MotherStatusID)
    ////    {
    ////        int FamilyCount = 0;
    ////        if(MotherStatusID == 3 || MotherStatusID == 5 || MotherStatusID == 7 || MotherStatusID == 9 || MotherStatusID == 10)
    ////        {
    ////            FamilyCount = 0;
    ////        }
    ////        FamilyCount = ReturnInt("Select Count(FamilyMotherID) from PD_FamilyMother where MotherAliveYNID  = 1 and FamilyID = " + FamilyID + "");
    ////        int ChildCount = ReturnInt("Select Count(FamilyChildID) from PD_FamilyChild where IsActive  = 1 and FamilyID = " + FamilyID + "");
    ////        return int.Parse((float.Parse(FamilyCount.ToString()) + float.Parse(ChildCount.ToString())).ToString());
    ////    }


    ////    public bool UpdateActiveMemberstoTeamProject(int FamilyID, int MotherStatusID)
    ////    {
    ////        try
    ////        {
    ////            int FamilyMember = GetActiveFamilyMembers(FamilyID, MotherStatusID);
    ////            DbCommand dbCommand = db.GetSqlStringCommand("Update PD_TeamProject Set FamilyMembers = @FamilyMembers where FamilyID = @FamilyID ");
    ////            dbCommand.Parameters.Add(new SqlParameter("@FamilyMembers", FamilyMember));
    ////            dbCommand.Parameters.Add(new SqlParameter("@FamilyID", FamilyID));
    ////            db.ExecuteNonQuery(dbCommand);
    ////            return true;
    ////        }
    ////        catch
    ////        {
    ////            return false;
    ////        }
    ////    }


    ////    public bool UpdateEmployeePassword(string Password, int ID)
    ////    {
    ////        try
    ////        {
    ////            DbCommand dbCommand = db.GetSqlStringCommand("Update Login Set Password = @Password where UserID = @ID ");
    ////            dbCommand.Parameters.Add(new SqlParameter("@Password", Password));
    ////            dbCommand.Parameters.Add(new SqlParameter("@ID", ID));
    ////            db.ExecuteNonQuery(dbCommand);
    ////            return true;
    ////        }
    ////        catch
    ////        {
    ////            return false;
    ////        }
    ////    }

    ////    public bool UpdateFamilyUserPassword(string Password, int ID)
    ////    {
    ////        try
    ////        {
    ////            DbCommand dbCommand = db.GetSqlStringCommand("Update FamilyUser Set Password = @Password where FamilyUserID = @ID ");
    ////            dbCommand.Parameters.Add(new SqlParameter("@Password", Password));
    ////            dbCommand.Parameters.Add(new SqlParameter("@ID", ID));
    ////            db.ExecuteNonQuery(dbCommand);
    ////            return true;
    ////        }
    ////        catch
    ////        {
    ////            return false;
    ////        }
    ////    }

    ////    public bool UpdateMemberUserPassword(string Password, int ID)
    ////    {
    ////        try
    ////        {
    ////            DbCommand dbCommand = db.GetSqlStringCommand("Update MemberUser Set Password = @Password where MemberUserID = @ID ");
    ////            dbCommand.Parameters.Add(new SqlParameter("@Password", Password));
    ////            dbCommand.Parameters.Add(new SqlParameter("@ID", ID));
    ////            db.ExecuteNonQuery(dbCommand);
    ////            return true;
    ////        }
    ////        catch
    ////        {
    ////            return false;
    ////        }
    ////    }


    ////    public bool Returnbol(string strQuery)
    ////    {
    ////        StringBuilder builder = new StringBuilder();
    ////        DbCommand command;
    ////        builder.Append(strQuery);

    ////        try
    ////        {
    ////            command = db.GetSqlStringCommand(builder.ToString());
    ////            return bool.Parse(db.ExecuteScalar(command).ToString());
    ////        }
    ////        catch (Exception ex)
    ////        {
    ////            return false;
    ////        }

    ////    }

    ////    public DataSet ReturnBank(bool Status)
    ////    {
    ////        string strQuery = " ";
    ////        if (Status == false)
    ////        {
    ////            strQuery = "SELECT [SettingsBankID] ,[BankName_Eng] as BankName ,[BankCode]   ,[TimeStamp] FROM [dbo].[SettingsBanks]";
    ////            DbCommand dbCommand = db.GetSqlStringCommand(strQuery);

    ////            return db.ExecuteDataSet(dbCommand);
    ////        }
    ////        else
    ////        {
    ////            strQuery = "SELECT [SettingsBankID] ,[BankName_t] as BankName, [BankCode]   ,[TimeStamp] FROM [dbo].[SettingsBanks]";
    ////            DbCommand dbCommand = db.GetSqlStringCommand(strQuery);

    ////            return db.ExecuteDataSet(dbCommand);
    ////        }


    ////    }

    ////    public DataTable GetImageUploadRecordAppointmentVisit(int ID)
    ////    {
    ////        try
    ////        {
    ////            DbCommand dbCommand = db.GetSqlStringCommand("SELECT [AppointmentVisitImageID] as ID,[Doc] ,[FileType] as ContentType ,[Size] as Size   , [Doc] as Data,   [FileName] as Name FROM [dbo].[PD_AppointmentVisitImage] where [AppointmentVisitImageID] = " + ID + "");
    ////            return db.ExecuteDataSet(dbCommand).Tables[0];
    ////        }
    ////        catch
    ////        {
    ////            return null;
    ////        }
    ////    }


    ////    public DataTable GetImageUploadRecordbyAppointmentID(int AppointmentID)
    ////    {
    ////        try
    ////        {
    ////            DbCommand dbCommand = db.GetSqlStringCommand("Select AppointmentVisitImageID AS ID,FileName AS NAME,FileType AS ContentType,Doc,Size,Description from PD_AppointmentVisitImage Where AppointmentID =" + AppointmentID.ToString());
    ////            return db.ExecuteDataSet(dbCommand).Tables[0];
    ////        }
    ////        catch
    ////        {
    ////            return null;
    ////        }
    ////    }

    ////    public DataTable GetNotesbyAppointmentID(int AppointmentVisitID)
    ////    {
    ////        try
    ////        {
    ////            DbCommand dbCommand = db.GetSqlStringCommand("Select  Notes from PD_AppointmentVisit Where AppointmentVisitID= " + AppointmentVisitID.ToString());
    ////            return db.ExecuteDataSet(dbCommand).Tables[0];
    ////        }
    ////        catch
    ////        {
    ////            return null;
    ////        }
    ////    }

    ////    public HRSalarySuspension FetchinGrid(string strFetchforGrid, int ID)
    ////    {
    ////        IDataReader reader = null;
    ////        HRSalarySuspension hrsal = new HRSalarySuspension();
    ////        DbCommand dbCommand = db.GetSqlStringCommand(strFetchforGrid);
    ////        dbCommand.Parameters.Add(new SqlParameter("@ID", ID));

    ////        reader = db.ExecuteReader(dbCommand);

    ////        while (reader.Read())
    ////        {

    ////            hrsal.IssueNumber = reader[0].ToString();
    ////            hrsal.IssueDate = reader[1].ToString();
    ////            hrsal.LetterDate = reader[2].ToString();
    ////            hrsal.LetterNo = reader[3].ToString();
    ////            hrsal.AbsentStartDate = reader[4].ToString();
    ////            hrsal.StopDate = reader[5].ToString();
    ////            hrsal.Memo = reader[6].ToString();
    ////            hrsal.Reason = reader[7].ToString();

    ////            return hrsal;
    ////        }
    ////        return null;

    ////    }

    ////    //public byte[] GetImageByte(FileUpload fileUpload)
    ////    //{

    ////    //    if (fileUpload.HasFile)
    ////    //    {

    ////    //        byte[] MainImage = new byte[fileUpload.PostedFile.InputStream.Length];
    ////    //        ////store the currently selected file in memeory.
    ////    //        //HttpPostedFile img = fileUpload.PostedFile;
    ////    //        ////set the binary data .
    ////    //        fileUpload.PostedFile.InputStream.Read(MainImage, 0, MainImage.Length);
    ////    //        return MainImage;
    ////    //    }
    ////    //    else
    ////    //    {
    ////    //        return new byte[] { 0 };
    ////    //    }
    ////    //}

    ////    //public byte[] GetImageByte(AsyncFileUpload fileUpload)
    ////    //{

    ////    //    if (fileUpload.HasFile)
    ////    //    {

    ////    //        byte[] MainImage = new byte[fileUpload.PostedFile.InputStream.Length];
    ////    //        ////store the currently selected file in memeory.
    ////    //        //HttpPostedFile img = fileUpload.PostedFile;
    ////    //        ////set the binary data .
    ////    //        fileUpload.PostedFile.InputStream.Read(MainImage, 0, MainImage.Length);
    ////    //        return MainImage;
    ////    //    }
    ////    //    else
    ////    //    {
    ////    //        return new byte[] { 0 };
    ////    //    }
    ////    //}

    ////    public bool UpdateCode(int Num, string VCode)
    ////    {
    ////        try
    ////        {

    ////            string cmd = "Update NextVNo set NextCode=@Code where VoucherCode = @VCode";
    ////            DbCommand dbcommand = db.GetSqlStringCommand(cmd);
    ////            dbcommand.Parameters.Add(new SqlParameter("@Code", Num));
    ////            dbcommand.Parameters.Add(new SqlParameter("@VCode", VCode));
    ////            db.ExecuteNonQuery(dbcommand);
    ////            return true;
    ////        }
    ////        catch
    ////        {
    ////            return false;
    ////        }
    ////    }

    #endregion

    //By Izhar Mehmood on dated 30082021
    public DataTable FetchinControldtProcWithParam1(string strQuery, string param)
    {
        try
        {
            DbCommand dbCommand = db.GetStoredProcCommand(strQuery);
            dbCommand.Parameters.Add(new SqlParameter("@param", param));
            return db.ExecuteDataSet(dbCommand).Tables[0];
        }
        catch
        {
            return null;
        }
    }

    public DataTable FetchinControldtProcWithParam3(string strQuery, string param, string dateFrom, string dateTo)
    {
        try
        {
            DbCommand dbCommand = db.GetStoredProcCommand(strQuery);
            dbCommand.Parameters.Add(new SqlParameter("@param", param));
            dbCommand.Parameters.Add(new SqlParameter("@dateFrom", dateFrom));
            dbCommand.Parameters.Add(new SqlParameter("@dateTo", dateTo));
            return db.ExecuteDataSet(dbCommand).Tables[0];
        }
        catch
        {
            return null;
        }
    }
}
