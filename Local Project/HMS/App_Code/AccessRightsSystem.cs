

/// <summary>
/// Summary description for AccessRights
/// </summary>
public class AccessRightsSystem
{
    #region Private
    // private int accessID;

    private int intAR_AccessRightsID;
    private int intEmployeeID;
    private int intRoleID;
    private string strAR_PageURL;
    private string strAR_PageGroup;
    private string strAR_PageEn;
    private string strAR_PageAR;
    private bool bolAR_View;
    private bool bolAR_Add;
    private bool bolAR_Edit;
    private bool bolAR_Delete;
    private bool bolAR_Approve;
    private string strFullUrl;
    private int intPageID;
    #endregion

    public AccessRightsSystem()
    {
        //
        // TODO: Add constructor logic here
        //
    }

    public int AR_AccessRightsID
    {
        get { return intAR_AccessRightsID; }
        set { intAR_AccessRightsID = value; }

    }

    public int UserID
    {
        get { return intEmployeeID; }
        set { intEmployeeID = value; }

    }

    public int RoleID
    {
        get { return intRoleID; }
        set { intRoleID = value; }

    }

    public string AR_PageURL
    {
        get { return strAR_PageURL; }
        set { strAR_PageURL = value; }

    }

    public string AR_PageGroup
    {
        get { return strAR_PageGroup; }
        set { strAR_PageGroup = value; }

    }

    public string AR_PageEn
    {
        get { return strAR_PageEn; }
        set { strAR_PageEn = value; }

    }

    public string AR_PageAR
    {
        get { return strAR_PageAR; }
        set { strAR_PageAR = value; }

    }

    public bool AR_View
    {
        get { return bolAR_View; }
        set { bolAR_View = value; }

    }

    public bool AR_Add
    {
        get { return bolAR_Add; }
        set { bolAR_Add = value; }

    }

    public bool AR_Edit
    {
        get { return bolAR_Edit; }
        set { bolAR_Edit = value; }

    }

    public bool AR_Delete
    {
        get { return bolAR_Delete; }
        set { bolAR_Delete = value; }

    }

    public bool AR_Approve
    {
        get { return bolAR_Approve; }
        set { bolAR_Approve = value; }

    }
    public string FullUrl
    {
        get { return strFullUrl; }
        set { strFullUrl = value; }

    }

    private bool bolDeleteYNID;
    public bool DeleteYNID
    {
        get { return bolDeleteYNID; }
        set { bolDeleteYNID = value; }

    }

    /// <summary>
    /// Added By Bilal
    /// Dated 15Aug2016
    /// Requested by Mr.Atif
    /// </summary>
    public int PageID
    {
        get { return intPageID; }
        set { intPageID = value; }

    }
}
