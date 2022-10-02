using Microsoft.Practices.EnterpriseLibrary.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Text;



/// <summary>
/// Summary description for AccessRightsManager
/// </summary>
public class AccessRightsSystemManager
{
    #region Private
    private Database db = null;
    #endregion
    public AccessRightsSystemManager()
    {
        db = DatabaseFactory.CreateDatabase("connectionstring");
    }

    //Use to add AccessRightsSSSS
    public int AddAccessRights(AccessRightsSystem accessRights)
    {
        try
        {
            string storedproc = "dbo.usp_Add_CR_AccessRights";
            DbCommand command = db.GetStoredProcCommand(storedproc);

            command.Parameters.Add(new SqlParameter("@EmployeeID", accessRights.UserID));
            command.Parameters.Add(new SqlParameter("@RoleID", accessRights.RoleID));
            command.Parameters.Add(new SqlParameter("@AR_PageURL", accessRights.AR_PageURL));
            command.Parameters.Add(new SqlParameter("@AR_PageGroup", accessRights.AR_PageGroup));
            command.Parameters.Add(new SqlParameter("@AR_PageEn", accessRights.AR_PageEn));
            command.Parameters.Add(new SqlParameter("@AR_PageAR", accessRights.AR_PageAR));
            command.Parameters.Add(new SqlParameter("@AR_View", accessRights.AR_View));
            command.Parameters.Add(new SqlParameter("@AR_Add", accessRights.AR_Add));
            command.Parameters.Add(new SqlParameter("@AR_Edit", accessRights.AR_Edit));
            command.Parameters.Add(new SqlParameter("@AR_Delete", accessRights.AR_Delete));
            command.Parameters.Add(new SqlParameter("@AR_Approve", accessRights.AR_Approve));
            command.Parameters.Add(new SqlParameter("@PageID", accessRights.PageID));

            if (accessRights.DeleteYNID != null) {
                command.Parameters.Add(new SqlParameter("@DeleteYNID", accessRights.DeleteYNID));
            }
            else {
                command.Parameters.Add(new SqlParameter("@DeleteYNID", DBNull.Value));
            }

            return int.Parse(db.ExecuteScalar(command).ToString());
        }
        catch (Exception ex)
        {
            string sMsg = ex.Message;
            return 0;
        }
    }


    public int AddAccessRightsEx(AccessRightsSystem accessRights)
    {
        try
        {
            string storedproc = "dbo.usp_Add_CR_AccessRightsEx";
            DbCommand command = db.GetStoredProcCommand(storedproc);

            command.Parameters.Add(new SqlParameter("@EmployeeID", accessRights.UserID));
            command.Parameters.Add(new SqlParameter("@RoleID", accessRights.RoleID));
            command.Parameters.Add(new SqlParameter("@AR_PageURL", accessRights.AR_PageURL));
            command.Parameters.Add(new SqlParameter("@AR_PageGroup", accessRights.AR_PageGroup));
            command.Parameters.Add(new SqlParameter("@AR_PageEn", accessRights.AR_PageEn));
            command.Parameters.Add(new SqlParameter("@AR_PageAR", accessRights.AR_PageAR));
            command.Parameters.Add(new SqlParameter("@AR_View", accessRights.AR_View));
            command.Parameters.Add(new SqlParameter("@AR_Add", accessRights.AR_Add));
            command.Parameters.Add(new SqlParameter("@AR_Edit", accessRights.AR_Edit));
            command.Parameters.Add(new SqlParameter("@AR_Delete", accessRights.AR_Delete));
            command.Parameters.Add(new SqlParameter("@AR_Approve", accessRights.AR_Approve));
            command.Parameters.Add(new SqlParameter("@FullUrl", accessRights.FullUrl));


            return int.Parse(db.ExecuteScalar(command).ToString());
        }
        catch (Exception ex)
        {
            string sMsg = ex.Message;
            return 0;
        }
    }


    public DataSet FetchinGrid(string strQuery, int RoleID)
    {

        DbCommand dbCommand = db.GetSqlStringCommand("Select PL_PageURL, PL_PageGroup, RoleID, PL_PageEn, PL_PageAR, PL_Add, PL_Edit, PL_Delete, PL_Approve, PL_Misc FROM dbo.CR_PageList " + strQuery + ") AND RoleID = @RoleID");
        dbCommand.Parameters.Add(new SqlParameter("@RoleID", RoleID));
        return db.ExecuteDataSet(dbCommand);

    }




    public Dictionary<string, AccessRightsSystem> GetAccessForm(int intEmployeeID)
    {
        try
        {
            Dictionary<string, AccessRightsSystem> dictForm = new Dictionary<string, AccessRightsSystem>();
            StringBuilder builder = new StringBuilder();
            builder.Append(" SELECT AR_PageURL FROM CR_AccessRights Where AR_View = 1 and UserID = @EmployeeID ");
            DbCommand command = db.GetSqlStringCommand(builder.ToString());
            command.Parameters.Add(new SqlParameter("@EmployeeID", intEmployeeID));

            AccessRightsSystem access = null;
            using (IDataReader reader = db.ExecuteReader(command))
            {
                while (reader.Read())
                {
                    access = new AccessRightsSystem();
                    access.AR_PageURL = reader[0].ToString().ToLower();

                    if (!dictForm.ContainsKey(access.AR_PageURL))
                    {
                        dictForm.Add(access.AR_PageURL, access);
                    }
                }
                reader.Close();
            }

            return dictForm;
        }
        catch (Exception ex)
        {
            string sMsg = ex.Message;
            return null;

        }

    }


    public Dictionary<string, AccessRightsSystem> GetAccessFormUserAdd(int intEmployeeID)
    {
        try
        {
            Dictionary<string, AccessRightsSystem> dictForm = new Dictionary<string, AccessRightsSystem>();
            StringBuilder builder = new StringBuilder();
            builder.Append(" SELECT AR_PageURL FROM CR_AccessRights Where AR_Add = 1 and UserID = @EmployeeID ");
            DbCommand command = db.GetSqlStringCommand(builder.ToString());
            command.Parameters.Add(new SqlParameter("@EmployeeID", intEmployeeID));

            AccessRightsSystem access = null;
            using (IDataReader reader = db.ExecuteReader(command))
            {
                while (reader.Read())
                {
                    access = new AccessRightsSystem();
                    access.AR_PageURL = reader[0].ToString().ToLower();

                    if (!dictForm.ContainsKey(access.AR_PageURL))
                    {
                        dictForm.Add(access.AR_PageURL, access);
                    }
                }
                reader.Close();
            }

            return dictForm;
        }
        catch (Exception ex)
        {
            string sMsg = ex.Message;
            return null;
        }

    }

    public Dictionary<string, AccessRightsSystem> GetAccessFormUserEdit(int intEmployeeID)
    {
        try
        {
            Dictionary<string, AccessRightsSystem> dictForm = new Dictionary<string, AccessRightsSystem>();
            StringBuilder builder = new StringBuilder();
            builder.Append(" SELECT AR_PageURL FROM CR_AccessRights Where AR_Edit = 1 and UserID = @EmployeeID ");
            DbCommand command = db.GetSqlStringCommand(builder.ToString());
            command.Parameters.Add(new SqlParameter("@EmployeeID", intEmployeeID));

            AccessRightsSystem access = null;
            using (IDataReader reader = db.ExecuteReader(command))
            {
                while (reader.Read())
                {
                    access = new AccessRightsSystem();
                    access.AR_PageURL = reader[0].ToString().ToLower();

                    if (!dictForm.ContainsKey(access.AR_PageURL))
                    {
                        dictForm.Add(access.AR_PageURL, access);
                    }
                }
                reader.Close();
            }

            return dictForm;
        }
        catch (Exception ex)
        {
            string sMsg = ex.Message;
            return null;
        }

    }

    public Dictionary<string, AccessRightsSystem> GetAccessFormUserDelete(int intEmployeeID)
    {
        try
        {
            Dictionary<string, AccessRightsSystem> dictForm = new Dictionary<string, AccessRightsSystem>();
            StringBuilder builder = new StringBuilder();
            builder.Append(" SELECT AR_PageURL FROM CR_AccessRights Where AR_Delete = 1 and UserID = @EmployeeID ");
            DbCommand command = db.GetSqlStringCommand(builder.ToString());
            command.Parameters.Add(new SqlParameter("@EmployeeID", intEmployeeID));

            AccessRightsSystem access = null;
            using (IDataReader reader = db.ExecuteReader(command))
            {
                while (reader.Read())
                {
                    access = new AccessRightsSystem();
                    access.AR_PageURL = reader[0].ToString().ToLower();

                    if (!dictForm.ContainsKey(access.AR_PageURL))
                    {
                        dictForm.Add(access.AR_PageURL, access);
                    }
                }
                reader.Close();
            }

            return dictForm;
        }
        catch (Exception ex)
        {
            string sMsg = ex.Message;
            return null;
        }

    }


    public Dictionary<string, AccessRightsSystem> GetAccessFormRole(int RoleID)
    {
        try
        {
            Dictionary<string, AccessRightsSystem> dictForm = new Dictionary<string, AccessRightsSystem>();
            StringBuilder builder = new StringBuilder();
            builder.Append(" select P.PL_PageURL as PageURL from RoleDetail RD join CR_PageList P on P.CR_PageListID = RD.CR_PageListID where RD.[View] = 1 and RD.RoleID = @RoleID ");
            DbCommand command = db.GetSqlStringCommand(builder.ToString());
            command.Parameters.Add(new SqlParameter("@RoleID", RoleID));

            AccessRightsSystem access = null;
            using (IDataReader reader = db.ExecuteReader(command))
            {
                while (reader.Read())
                {
                    access = new AccessRightsSystem();
                    access.AR_PageURL = reader[0].ToString().ToLower();

                    if (!dictForm.ContainsKey(access.AR_PageURL))
                    {
                        dictForm.Add(access.AR_PageURL, access);
                    }
                }
                reader.Close();
            }

            return dictForm;
        }
        catch (Exception ex)
        {
            string sMsg = ex.Message;
            return null;
        }

    }



    public Dictionary<string, AccessRightsSystem> GetAccessFormEx(int intEmployeeID)
    {
        try
        {
            Dictionary<string, AccessRightsSystem> dictFormEx = new Dictionary<string, AccessRightsSystem>();

            StringBuilder builder = new StringBuilder();
            builder.Append(" SELECT AR_PageURL FROM CR_AccessRightsEx Where AR_View = 1 and UserID = @EmployeeID ");
            DbCommand command = db.GetSqlStringCommand(builder.ToString());
            command.Parameters.Add(new SqlParameter("@EmployeeID", intEmployeeID));

            AccessRightsSystem access = null;
            using (IDataReader reader = db.ExecuteReader(command))
            {
                while (reader.Read())
                {
                    access = new AccessRightsSystem();
                    access.AR_PageURL = reader[0].ToString();

                    if (!dictFormEx.ContainsKey(access.AR_PageURL))
                    {
                        dictFormEx.Add(access.AR_PageURL, access);
                    }
                }
                reader.Close();
            }

            return dictFormEx;
        }
        catch (Exception ex)
        {
            string sMsg = ex.Message;
            return null;
        }

    }


    /* Use to Get  employee and thier roles by filter*/

    public DataSet GetRoleModuleNameByEmployeeID(int employeeID)
    {
        try
        {
            DbCommand dbCommand = db.GetSqlStringCommand(" SELECT  dbo.AccessPrevilleges.RoleID, dbo.AccessPrevilleges.ModuleID, dbo.lkpt_UserModule.UM_ModuleAR, dbo.lkpt_UserModule.UM_ModuleEn FROM  dbo.AccessPrevilleges INNER JOIN dbo.lkpt_UserModule ON dbo.AccessPrevilleges.ModuleID = dbo.lkpt_UserModule.UM_UserModuleID WHERE (dbo.AccessPrevilleges.EmpID = @EmpID) ");
            dbCommand.Parameters.Add(new SqlParameter("@EmpID", employeeID));
            return db.ExecuteDataSet(dbCommand);
        }
        catch (Exception ex)
        {
            string sMsg = ex.Message;
            return null;
        }

    }

    public DataSet GetMenu(int EmployeeID)
    {

        DbCommand dbCommand = db.GetSqlStringCommand("SELECT DISTINCT AR_PageGroup FROM         dbo.CR_AccessRights WHERE     (UserID = @EmployeeID)");
        dbCommand.Parameters.Add(new SqlParameter("@EmployeeID", EmployeeID));
        return db.ExecuteDataSet(dbCommand);


    }

    public void DeleteEmployeeAR(int EmployeeID)
    {

        DbCommand dbCommand = db.GetSqlStringCommand("Delete FROM         dbo.CR_AccessRights WHERE     (UserID = @EmployeeID)");
        dbCommand.Parameters.Add(new SqlParameter("@EmployeeID", EmployeeID));
        db.ExecuteNonQuery(dbCommand);


    }


    //Abdul -- overloading..
    public DataSet FetchinGrid(bool bUser, int ID, int iModuleID, int iRoleID, bool isAdmin)
    {
        DbCommand dbCommand = null;
        if (bUser)
        {
            dbCommand = db.GetSqlStringCommand(@"SELECT CR_PageList.CR_PageListID, CR_PageList.CR_PageListID as PageListID, CR_PageList.PL_PageURL as PageURL ,
                                                CR_AccessRights.AR_PageGroup,RoleID, CR_PageList.PL_PageEn as PageEn, CR_PageList.PL_PageAR as PageAR,  
                                                ISNULL(CR_AccessRights.AR_Add,0) as PL_Add, ISNULL(CR_AccessRights.AR_Edit, 0) as PL_Edit, 
                                                ISNULL(CR_AccessRights.AR_Delete,0) as PL_Delete, ISNULL(CR_AccessRights.AR_Approve,0) AS AR_Approve, ISNULL(CR_AccessRights.AR_Misc,0) AS AR_Misc , ISNULL(AR_View,0) as PL_View,
                                                UM.UM_ModuleAR AS ModuleName 
                                                FROM CR_PageList 
                                                LEFT JOIN CR_AccessRights ON CR_AccessRights.PageID = CR_PageList.CR_PageListID   AND CR_AccessRights.UserID = " + ID + " AND CR_AccessRights.DeleteYNID = 0"
                                                + " LEFT JOIN lkpt_UserModule UM ON UM.UM_UserModuleID = CR_PageList.UM_UserModuleID ORDER BY UM.UM_UserModuleID DESC");
        }
        //dbCommand = db.GetSqlStringCommand("Select PageID as PageListID, AR_PageURL as PageURL ,AR_PageGroup,RoleID, AR_PageEn as PageEn, AR_PageAR as PageAR, AR_Add as PL_Add, AR_Edit as PL_Edit, AR_Delete as PL_Delete, AR_Approve, AR_Misc,AR_View as PL_View FROM CR_AccessRights Where UserID=" + ID + " and DeleteYNID = 0 ");
        else
        {
            dbCommand = db.GetSqlStringCommand("select count(*) from RoleDetail where RoleID=" + iRoleID);
            object iCount = db.ExecuteScalar(dbCommand);
            if (Convert.ToInt32(iCount) < 1)
            {
                if (isAdmin)
                    dbCommand = db.GetSqlStringCommand("select CR_PageListID as PageListID,PL_PageURL as PageURL,PL_PageEn as PageEn,PL_PageAR as PageAR,convert(bit,'False') as PL_Add, convert(bit,'False') as PL_Edit,convert(bit,'False') as PL_Delete ,convert(bit,'False') as PL_View, UM.UM_ModuleAR AS ModuleName from  CR_PageList INNER JOIN lkpt_UserModule UM ON UM.UM_UserModuleID = CR_PageList.UM_UserModuleID ORDER BY UM.UM_UserModuleID DESC");
                else
                    dbCommand = db.GetSqlStringCommand(" SELECT     CR_PageListID AS PageListID, PL_PageURL AS PageURL, PL_PageEn AS PageEn, PL_PageAR AS PageAR, CONVERT(bit, 'False') AS PL_Add, CONVERT(bit, 'False') AS PL_Edit, CONVERT(bit, 'False') AS PL_Delete, CONVERT(bit, 'False') AS PL_View , UM.UM_ModuleAR AS ModuleName FROM         dbo.CR_PageList AS L INNER JOIN lkpt_UserModule UM ON UM.UM_UserModuleID = L.UM_UserModuleID WHERE     (L.UM_UserModuleID = " + iModuleID + ") ORDER BY UM.UM_UserModuleID DESC");
                //dbCommand = db.GetSqlStringCommand("select L.CR_PageListID as PageListID,L.PL_PageURL as PageURL,L.PL_PageEn as PageEn,L.PL_PageAR as PageAR,convert(bit,'False') as PL_Add,convert(bit,'False') as PL_Edit,convert(bit,'False') as PL_Delete ,convert(bit,'False') as PL_View from  CR_PageList L join CR_PageGroup G  on L.CR_PageListID=G.CR_PageListID where   G.PG_GroupID="+ID+"and L.UM_UserModuleID=" + iModuleID);

                // dbCommand = db.GetSqlStringCommand("select CR_PageListID as PageListID,PL_PageURL as PageURL,PL_PageEn as PageEn,PL_PageAR as PageAR,convert(bit,'False') as PL_Add, convert(bit,'False') as PL_Edit,convert(bit,'False') as PL_Delete ,convert(bit,'False') as PL_View from  CR_PageList where  UM_UserModuleID=" + iModuleID);
            }
            else
            {
                dbCommand = db.GetSqlStringCommand("select RD.CR_PageListID as PageListID , P.PL_PageURL as PageURL,P.PL_PageEn as PageEn, PL_PageAR as PageAR,RD.[Add] as PL_Add,RD.[Update] as PL_Edit,RD.[Delete] as PL_Delete,RD.[View] as PL_View, UM.UM_ModuleAR AS ModuleName from RoleDetail RD join CR_PageList P on P.CR_PageListID = RD.CR_PageListID INNER JOIN lkpt_UserModule UM ON UM.UM_UserModuleID = P.UM_UserModuleID where  RD.RoleID=" + iRoleID + " and P.DeleteYNID = 0 ORDER BY UM.UM_UserModuleID DESC /*and (p.UM_UserModuleID = " + iModuleID + " or p.UM_UserModuleID = 0)*/");
                //dbCommand = db.GetSqlStringCommand("select CR_PageListID as PageListID,PL_PageURL as PageURL,PL_PageEn as PageEn,PL_PageAR as PageAR,convert(bit,'False') as PL_Add, convert(bit,'False') as PL_Edit,convert(bit,'False') as PL_Delete ,convert(bit,'False') as PL_View from  CR_PageList");
            }
        }


        return db.ExecuteDataSet(dbCommand);
    }


    public DataSet FetchinGridEx(int ID)
    {
        DbCommand dbCommand = null;

        dbCommand = db.GetSqlStringCommand("Select UserID as PageListID,AR_PageURL as PageURL ,AR_PageGroup,RoleID, AR_PageEn as PageEn, AR_PageAR as PageAR, AR_Add as PL_Add, AR_Edit as PL_Edit, AR_Delete as PL_Delete, AR_Approve, AR_Misc,AR_View as PL_View, FullUrl FROM CR_AccessRightsEx Where UserID=" + ID);

        return db.ExecuteDataSet(dbCommand);
    }


    //Abdul -- get AllEmployees in the Accessright table..
    public DataSet GetAllEmployees()
    {
        try
        {
            DbCommand dbCommand = null;
            dbCommand = db.GetSqlStringCommand("select distinct(E.UserID),E.EmployeeName from Users E join CR_AccessRights A  on A.UserID=E.UserID");
            return db.ExecuteDataSet(dbCommand);
        }
        catch (Exception ex)
        {
            string sMsg = ex.Message;
            return null;
        }
    }


    public DataSet GetAllEmployeesEx()
    {
        try
        {
            DbCommand dbCommand = null;
            dbCommand = db.GetSqlStringCommand("select distinct(E.UserID),E.EmployeeName from Users E join CR_AccessRightsEx A  on A.UserID=E.UserID");
            return db.ExecuteDataSet(dbCommand);
        }
        catch (Exception ex)
        {
            string sMsg = ex.Message;
            return null;
        }
    }

    //Abdul -- fetch all Modules for the selected user by ID
    public DataSet GetModuleForSeletedUser(int ID)
    {
        try
        {
            DbCommand dbCommand = null;
            DataSet ds = null;
            //dbCommand = db.GetSqlStringCommand("select distinct(AR_PageGroup) from dbo.CR_AccessRights where EmployeeID=" + ID);
            dbCommand = db.GetSqlStringCommand("select distinct(R.RoleName) from Roles R join CR_AccessRights A on R.RoleID=A.RoleID where A.UserID=" + ID);
            ds = db.ExecuteDataSet(dbCommand);
            return ds;
        }
        catch (Exception ex)
        {
            string sMsg = ex.Message;
            return null;
        }
    }

    //Abdul -- count(*) from RoleDetail
    public bool isExistInRoleDetails(int iRoleID)
    {
        try
        {
            DbCommand dbCommand = null;
            dbCommand = db.GetSqlStringCommand("select count(*) from RoleDetail where RoleID=" + iRoleID);
            object iCount = db.ExecuteScalar(dbCommand);
            if (Convert.ToInt32(iCount) < 1)
                return false;
            else
                return true;
        }
        catch (Exception ex)
        {
            string sMsg = ex.Message;
            return false;
        }
    }
    //Abdul -- Delete from AccessRights
    public void DeleteAccessRights(int id, bool isUser)
    {
        DbCommand dbCommand = null;
        if (isUser)
            dbCommand = db.GetSqlStringCommand("delete  from CR_AccessRights where UserID=" + id);
        else
            dbCommand = db.GetSqlStringCommand("delete  from RoleDetail where RoleID=" + id);
        db.ExecuteNonQuery(dbCommand);

    }


    public void DeleteAccessRightsEx(int id, bool isUser)
    {
        DbCommand dbCommand = null;
        dbCommand = db.GetSqlStringCommand("delete  from CR_AccessRightsEx where UserID=" + id);
        db.ExecuteNonQuery(dbCommand);

    }


    public void DeleteAccessRightsex(int id)
    {
        DbCommand dbCommand = null;

        dbCommand = db.GetSqlStringCommand("delete  from CR_AccessRightsEx where UserID=" + id);

        db.ExecuteNonQuery(dbCommand);

    }

    //Abdul -- select * from AccessPrevilleges
    public DataSet getAccessPrevillegesByRoleID(int iRoleID)
    {
        try
        {
            DbCommand dbCommand = null;
            dbCommand = db.GetSqlStringCommand("select * from AccessPrevilleges where RoleID=" + iRoleID);
            return (db.ExecuteDataSet(dbCommand));
        }
        catch (Exception ex)
        {
            string sMsg = ex.Message;
            return null;
        }
    }

    public DataSet getRoleDetailByRoleID(int iRoleID)
    {
        DbCommand dbCommand = null;

        StringBuilder builder = new StringBuilder();
        builder.Append(" SELECT     dbo.CR_PageList.PL_PageURL, dbo.RoleDetail.[Add], dbo.RoleDetail.[Update], dbo.RoleDetail.[Delete], dbo.RoleDetail.[View], dbo.RoleDetail.RoleID,  dbo.CR_PageList.PL_PageAR, dbo.CR_PageList.PL_PageEn, dbo.CR_PageList.UM_UserModuleID  ");
        builder.Append(" FROM  dbo.CR_PageList INNER JOIN dbo.RoleDetail ON dbo.CR_PageList.CR_PageListID = dbo.RoleDetail.CR_PageListID where RoleID = " + iRoleID);

        dbCommand = db.GetSqlStringCommand(builder.ToString());
        return (db.ExecuteDataSet(dbCommand));
    }

    //Abdul -- get GetAllEmployeesByRegion
    public DataSet GetAllEmployeesByRegion(int iRegionID)
    {
        try
        {
            DbCommand dbCommand = null;
            dbCommand = db.GetSqlStringCommand("select distinct(E.UserID),E.EmployeeName from Users E join CR_AccessRights A  on A.UserID=E.UserID where E.RegionID=" + iRegionID);
            return db.ExecuteDataSet(dbCommand);
        }
        catch (Exception ex)
        {
            string sMsg = ex.Message;
            return null;
        }
    }

    public bool UpdateAccessRightsbyRoleID(int RoleID, bool ADD, bool Update, bool Delete, bool View, string PageURl)
    {
        try
        {
            string cmd = @"UPDATE [dbo].[CR_AccessRights]
   SET  
      
      [AR_Add] = @ADD
      ,[AR_Edit] = @Update
      ,[AR_Delete] = @Delete
      ,[AR_View] = @View
      
 WHERE RoleID = @ID and AR_PageURL = @PageURl ";
            DbCommand dbcommand = db.GetSqlStringCommand(cmd);
            dbcommand.Parameters.Add(new SqlParameter("@ID", RoleID));
            dbcommand.Parameters.Add(new SqlParameter("@ADD", ADD));
            dbcommand.Parameters.Add(new SqlParameter("@Update", Update));
            dbcommand.Parameters.Add(new SqlParameter("@Delete", Delete));
            dbcommand.Parameters.Add(new SqlParameter("@View", View));
            dbcommand.Parameters.Add(new SqlParameter("@PageURl", PageURl));
            db.ExecuteNonQuery(dbcommand);
            return true;
        }
        catch
        {
            return false;
        }
    }

    public bool UpdateAccessRightsbyUserID(int UserID, int RoleId, bool ADD, bool Update, bool Delete, bool View)
    {
        try
        {
            string cmd = @"UPDATE [dbo].[CR_AccessRights]
   SET  
      
      [AR_Add] = @ADD
      ,[AR_Edit] = @Update
      ,[AR_Delete] = @Delete
      ,[AR_View] = @View
      ,[RoleID] = @RoleID
      
 WHERE UserID = @ID";
            DbCommand dbcommand = db.GetSqlStringCommand(cmd);
            dbcommand.Parameters.Add(new SqlParameter("@ID", UserID));
            dbcommand.Parameters.Add(new SqlParameter("@ADD", ADD));
            dbcommand.Parameters.Add(new SqlParameter("@Update", Update));
            dbcommand.Parameters.Add(new SqlParameter("@Delete", Delete));
            dbcommand.Parameters.Add(new SqlParameter("@View", View));
            dbcommand.Parameters.Add(new SqlParameter("@RoleID", RoleId));
            db.ExecuteNonQuery(dbcommand);
            return true;
        }
        catch
        {
            return false;
        }
    }

}
