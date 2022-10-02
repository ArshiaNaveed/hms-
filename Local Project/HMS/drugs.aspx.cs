using System;
using System.Data;
using System.Web.UI;

namespace HMS
{
    public partial class drugs : System.Web.UI.Page
    {
        Utilities ui = new Utilities();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["sysAccess"] != null)
                {
                    DataTable dtSysAccess = (DataTable)Session["sysAccess"];
                    if (dtSysAccess.Rows[0]["addMedicneDrugs"].ToString() == "0")
                    {
                        Session["page"] = "Settings / Add Medicne Drugs";
                        Response.Redirect("404.aspx");
                    }
                }

                fillDrugsType();

                if (Session["drugAction"] != null)
                {
                    getDrugs(Convert.ToInt32(Session["drugIdx"].ToString()));
                    Session["drugAction"] = null;
                }
                GetAccessRights();
            }
        }
        private void GetAccessRights()
        {
            DataTable dt = new DataTable();
            dt = ui.FetchinControldtPara(@"SELECT r.userIdx,u.idx,u.pageUrl FROM Roles r inner join Url u On u.idx = r.pageUrl Where r.visible = 1 and r.userIdx =  @param AND u.idx='15'", Session["appUserId"].ToString());
            if (dt.Rows.Count > 0)
            {
                if (dt.Rows[0]["pageUrl"].ToString() == "drugs.aspx")
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
        protected void fillDrugsType()
        {
            try
            {
                DataTable dt = new DataTable();
                dt = ui.FetchinControldt(@"select * from medicneType where visible = 1");
                if (dt.Rows.Count > 0)
                {
                    ddlDrugType.DataSource = dt;
                    ddlDrugType.DataValueField = "idx";
                    ddlDrugType.DataTextField = "medicneType";
                    ddlDrugType.DataBind();
                    ddlDrugType.Items.Insert(0, "Medicine Type");
                }
            }
            catch (Exception ex)
            { }
        }

        protected void getDrugs(int idx)
        {
            try
            {
                DataTable dt = new DataTable();
                dt = ui.FetchinControldt(@"select * from drugs where visible = 1 and idx = " + idx);
                if (dt.Rows.Count > 0)
                {
                    txtDrugs.Text = dt.Rows[0]["drugName"].ToString();
                    ddlDrugType.SelectedItem.Text = dt.Rows[0]["drugType"].ToString();
                    txtDrugs.Enabled = false;
                    ddlDrugType.Enabled = false;
                    btnSubmit.Enabled = false;
                    btnUpdate.Enabled = false;
                    btnEdit.Enabled = true;
                }
            }
            catch (Exception ex)
            { }
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            try
            {
                bool x;
                x = ui.ExecuteNonQuery(@"INSERT INTO [dbo].[drugs]
                                       ([drugName]
                                       ,[drugType]
                                       ,[createdByUserIdx]
                                        )
                                 VALUES
                                       (
                                        '" + ui.GetSQLInject(txtDrugs.Text) + @"',
                                        '" + ui.GetSQLInject(ddlDrugType.SelectedItem.ToString()) + @"',
                                        '" + Session["appUserId"].ToString() + @"'
                )");
                if (x == true)
                {
                    clearAll();
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "", "<script>showSuccessMessage()</script>", false);
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "", "<script>showErrorMessage()</script>", false);
                }
            }
            catch (Exception ex)
            { }
        }
        protected void btnCancel_Click(object sender, EventArgs e)
        {
            clearAll();
        }

        protected void clearAll()
        {
            txtDrugs.Text = "";
            ddlDrugType.ClearSelection();
        }

        protected void btnEdit_Click(object sender, EventArgs e)
        {
            txtDrugs.Enabled = true;
            ddlDrugType.Enabled = true;
            btnSubmit.Enabled = false;
            btnUpdate.Enabled = true;
            btnEdit.Enabled = false;
        }

        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                bool x;
                x = ui.ExecuteNonQuery(@"UPDATE [dbo].[drugs]
                                       SET [drugName] = '" + ui.GetSQLInject(txtDrugs.Text) + @"'
                                          ,[drugType] = '" + ui.GetSQLInject(ddlDrugType.SelectedItem.ToString()) + @"'
                                     WHERE idx = " + Session["drugIdx"].ToString());
                if (x == true)
                {
                    Session["drugIdx"] = null;
                    btnUpdate.Enabled = false;
                    btnSubmit.Enabled = true;
                    clearAll();
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "", "<script>showSuccessMessage()</script>", false);
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "", "<script>showErrorMessage()</script>", false);
                }
            }
            catch (Exception ex)
            { }
        }
    }
}