using System;
using System.Data;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace HMS
{
    public partial class treatment : System.Web.UI.Page
    {
        Utilities ui = new Utilities();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.IsPostBack)
            {
                if (Session["sysAccess"] != null)
                {
                    DataTable dtSysAccess = (DataTable)Session["sysAccess"];
                    if (dtSysAccess.Rows[0]["treatment"].ToString() == "0")
                    {
                        Session["page"] = "Medical Treatment";
                        Response.Redirect("404.aspx");
                    }
                }

                if (Session["skippedToken"] != null)
                {
                    fillpatientDetails(Convert.ToInt32(Session["skippedTokenIdx"].ToString()));
                    Session["skippedToken"] = null;
                }

                DataTable dtMdicalDrug = new DataTable();
                dtMdicalDrug.Columns.AddRange(new DataColumn[4] { new DataColumn("MedicalDrugName"), new DataColumn("MedicalDrugQty"), new DataColumn("MedicalMedUsage"), new DataColumn("MedicalMedDays") });
                ViewState["internalMedicine"] = dtMdicalDrug;
                this.bindInternalMedicine();

                DataTable dtPrescription = new DataTable();
                dtPrescription.Columns.AddRange(new DataColumn[4] { new DataColumn("PrescriptionDrugName"), new DataColumn("PrescriptionQty"), new DataColumn("PrescriptionMedUsage"), new DataColumn("PrescriptionMedDays") });
                ViewState["pharmacyMedicine"] = dtPrescription;
                this.bindPharmacyMedicine();

                bindMedicalUsage();
                bindDrugs();
                GetAccessRights();
            }

        }
        private void GetAccessRights()
        {
            DataTable dt = new DataTable();
            dt = ui.FetchinControldtPara(@"SELECT r.userIdx,u.idx,u.pageUrl FROM Roles r inner join Url u On u.idx = r.pageUrl Where r.visible = 1 and r.userIdx =  @param AND u.idx='10'", Session["appUserId"].ToString());
            if (dt.Rows.Count > 0)
            {
                if (dt.Rows[0]["pageUrl"].ToString() == "treatment.aspx")
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
        protected void bindMedicalUsage()
        {
            try
            {
                DataTable dt = new DataTable();
                dt = ui.FetchinControldt(@"select * from medicineUsage where visible = 1");
                if (dt.Rows.Count > 0)
                {
                    ddlPrescriptionMedUsage.DataSource = dt;
                    ddlPrescriptionMedUsage.DataValueField = "idx";
                    ddlPrescriptionMedUsage.DataTextField = "medicineUsage";
                    ddlPrescriptionMedUsage.DataBind();
                    ddlPrescriptionMedUsage.Items.Insert(0, "Usage Timings");

                    ddlMedicalMedUsage.DataSource = dt;
                    ddlMedicalMedUsage.DataValueField = "idx";
                    ddlMedicalMedUsage.DataTextField = "medicineUsage";
                    ddlMedicalMedUsage.DataBind();
                    ddlMedicalMedUsage.Items.Insert(0, "Usage Timings");
                }
            }
            catch (Exception ex)
            { }
        }

        protected void bindDrugs()
        {
            try
            {
                DataTable dt = new DataTable();
                dt = ui.FetchinControldt(@"select idx, (drugName + ' ' + '('+drugType+')') as drugName from drugs where visible = 1");
                if (dt.Rows.Count > 0)
                {
                    ddlMedicalDrugName.DataSource = dt;
                    ddlMedicalDrugName.DataValueField = "idx";
                    ddlMedicalDrugName.DataTextField = "drugName";
                    ddlMedicalDrugName.DataBind();
                    ddlMedicalDrugName.Items.Insert(0, "Medicine");

                    ddlPrescriptionDrugName.DataSource = dt;
                    ddlPrescriptionDrugName.DataValueField = "idx";
                    ddlPrescriptionDrugName.DataTextField = "drugName";
                    ddlPrescriptionDrugName.DataBind();
                    ddlPrescriptionDrugName.Items.Insert(0, "Medicine");

                }
            }
            catch (Exception ex)
            { }
        }

        protected void fillpatientDetails(int tokenIdx)
        {
            try
            {
                DataTable dt = new DataTable();
                if (tokenIdx == 0)
                {
                    //0 status = pending treatment
                    //Query for get random patients.

                    dt = ui.FetchinControldt(@"select top 1 p.idx as patientIdx, t.idx as tokenIdx, 
                                p.cardNumber, p.patientName, p.age, p.contactNumber1, p.contactNumber2,
                                t.tokenNumber, (u.firstName + ' ' + u.lastName) as doctorName, t.appointmentDate, t.fee
                                from token t
                                inner join patentRegistration p on p.idx = t.patientIdx
                                inner join users u on u.idx = t.physicianIdx
                                where  Convert(date, t.appointmentDate, 103) = Convert(date, getdate(), 103) and physicianIdx = " + Session["appUserId"].ToString() + @" and t.visible = 1 and t.status = 0
                                order by t.tokenNumber asc");
                }
                else if (tokenIdx > 0)
                {
                    //0 status = pending treatment
                    //Query for get skipped patients by token / appointment number.

                    dt = ui.FetchinControldt(@"select top 1 p.idx as patientIdx, t.idx as tokenIdx, 
                                p.cardNumber, p.patientName, p.age, p.contactNumber1, p.contactNumber2,
                                t.tokenNumber, (u.firstName + ' ' + u.lastName) as doctorName, t.appointmentDate, t.fee
                                from token t
                                inner join patentRegistration p on p.idx = t.patientIdx
                                inner join users u on u.idx = t.physicianIdx
                                where  Convert(date, t.appointmentDate, 103) = Convert(date, getdate(), 103) and physicianIdx = " + Session["appUserId"].ToString() + @" and t.visible = 1 and t.idx = " + tokenIdx + @"
                                order by t.tokenNumber asc");
                }
                if (dt.Rows.Count > 0)
                {
                    txtCardNumber.Text = dt.Rows[0]["cardNumber"].ToString();
                    lblPatientName.Text = dt.Rows[0]["patientName"].ToString();
                    lblAge.Text = dt.Rows[0]["age"].ToString();
                    lblContactNumber1.Text = dt.Rows[0]["contactNumber1"].ToString();
                    lblContactNumber2.Text = dt.Rows[0]["contactNumber2"].ToString();
                    lblTokenNumber.Text = dt.Rows[0]["tokenNumber"].ToString();
                    lblDoctor.Text = dt.Rows[0]["doctorName"].ToString();
                    lblAppointmentDate.Text = dt.Rows[0]["appointmentDate"].ToString();
                    Session["patientIdx"] = dt.Rows[0]["patientIdx"].ToString();
                    Session["tokenIdx"] = dt.Rows[0]["tokenIdx"].ToString();
                }
            }
            catch (Exception ex)
            { }
        }

        protected void btnNextPatient_Click(object sender, EventArgs e)
        {
            fillpatientDetails(0);
        }

        protected void btnSkipNextPatient_Click(object sender, EventArgs e)
        {
            try
            {
                //0 status = pending treatment
                //1 status = checked treatment
                //2 status = skip treatment

                bool x;
                x = ui.ExecuteNonQuery(@"update token set status = 2 where idx = " + Session["tokenIdx"].ToString());
                if (x == true)
                {
                    fillpatientDetails(0);
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "", "<script>showErrorMessage()</script>", false);
                }
            }
            catch (Exception ex)
            { }
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            int x = 0;
            x = ui.ExecuteScalar(@"INSERT INTO [dbo].[treatment]
           (
		   [patientIdx]
           ,[tokenIdx]
           ,[fever]
           ,[bp]
           ,[sugar]
           ,[otherDaisies]
           ,[diagnostics]
           ,[createdByUserIdx]
		   )
     VALUES
           (
            '" + Session["patientIdx"].ToString() + @"',
            '" + Session["tokenIdx"].ToString() + @"',
            '" + ui.GetSQLInject(txtFever.Text) + @"',
            '" + ui.GetSQLInject(txtBp.Text) + @"',
            '" + ui.GetSQLInject(txtSugar.Text) + @"',
            '" + ui.GetSQLInject(txtOtherDaisies.Text) + @"',
            '" + ui.GetSQLInject(txtDiagnostic.Text) + @"',
            '" + Session["appUserId"].ToString() + @"'
		   )" + "SELECT CAST(scope_identity() AS int)");

            if (x > 0)
            {
                //0 status = pending treatment
                //1 status = checked treatment
                //2 status = skip treatment
                ui.ExecuteNonQuery(@"update token set [status] = 1 where idx = " + Session["tokenIdx"].ToString());

                if (ViewState["medicalDrug"] != null)
                {
                    DataTable dtMdicalDrug = (DataTable)ViewState["medicalDrug"];
                    for (int i = 0; i < dtMdicalDrug.Rows.Count; i++)
                    {
                        DataRow dr = dtMdicalDrug.Rows[i];
                        ui.ExecuteNonQuery(@"INSERT INTO [dbo].[medicineLog]
                       ([treatmentIdx]
                       ,[medicineName]
                       ,[qty]
                       ,[usage]
                       ,[days]
                       ,[createdByUserIdx]
		               )
                 VALUES
                       (
                        '" + x + @"',
                        '" + dr["MedicalDrugName"] + @"',
                        '" + dr["MedicalDrugQty"] + @"',
                        '" + dr["MedicalMedUsage"] + @"',
                        '" + dr["MedicalMedDays"] + @"',
                        '" + Session["appUserId"].ToString() + @"'
                       )");
                    }
                }
                if (ViewState["PrescriptionDrug"] != null)
                {
                    DataTable dt = (DataTable)ViewState["PrescriptionDrug"];
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        DataRow dr = dt.Rows[i];
                        ui.ExecuteNonQuery(@"INSERT INTO [dbo].[prescriptionLog]
                      ([treatmentIdx]
                       ,[medicineName]
                       ,[qty]
                       ,[usage]
                       ,[days]
                       ,[createdByUserIdx]
		               )
                 VALUES
                       (
                        '" + x + @"',
                        '" + dr["PrescriptionDrugName"] + @"',
                        '" + dr["PrescriptionQty"] + @"',
                        '" + dr["PrescriptionMedUsage"] + @"',
                        '" + dr["PrescriptionMedDays"] + @"',
                        '" + Session["appUserId"].ToString() + @"'
                       )");
                    }
                }

                clearAll();
                //fillpatientDetails(0);

                ScriptManager.RegisterStartupScript(this, this.GetType(), "", "<script>showSuccessMessage()</script>", false);
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "", "<script>showErrorMessage()</script>", false);
            }
        }
        protected void btnCancel_Click(object sender, EventArgs e)
        {
            clearAll();
        }

        protected void clearAll()
        {

            txtCardNumber.Text = "";
            lblPatientName.Text = "";
            lblAge.Text = "";
            lblContactNumber1.Text = "";
            lblContactNumber2.Text = "";
            lblTokenNumber.Text = "";
            lblDoctor.Text = "";
            lblAppointmentDate.Text = "";
            Session["patientIdx"] = "";
            Session["tokenIdx"] = "";


            txtFever.Text = "";
            txtBp.Text = "";
            txtSugar.Text = "";
            txtOtherDaisies.Text = "";
            txtDiagnostic.Text = "";
            ddlMedicalDrugName.ClearSelection();
            txtMedicalDrugQty.Text = "";
            ddlMedicalMedUsage.SelectedIndex = 0;
            grvMedicalDrug.DataSource = null;
            grvMedicalDrug.DataBind();

            ddlPrescriptionDrugName.ClearSelection();
            txtPrescriptionDrugQty.Text = "";
            ddlPrescriptionMedUsage.SelectedIndex = 0;

            grvPrescriptionDrug.DataSource = null;
            grvPrescriptionDrug.DataBind();


        }

        #region Internal Medicine

        protected void bindInternalMedicine()
        {
            grvMedicalDrug.DataSource = (DataTable)ViewState["internalMedicine"];
            grvMedicalDrug.DataBind();
        }

        protected void insertInternalMedicine(object sender, EventArgs e)
        {
            if (ddlMedicalDrugName.SelectedIndex == 0)
            {
                internalMedicineError.Visible = true;
                internalMedicineError.Text = "Please select Medicine";
            }
            else
            {
                if (txtMedicalDrugQty.Text == "")
                {
                    internalMedicineError.Visible = true;
                    internalMedicineError.Text = "Please enter Qty";
                }
                else
                {
                    if (ddlMedicalMedUsage.SelectedIndex == 0)
                    {
                        internalMedicineError.Visible = true;
                        internalMedicineError.Text = "Please select Usage Timing";
                    }
                    else
                    {
                        if (ddlMedicalDrugDays.SelectedIndex == 0)
                        {
                            internalMedicineError.Visible = true;
                            internalMedicineError.Text = "Please select Usage Days";
                        }
                        else
                        {
                            internalMedicineError.Visible = false;
                            DataTable dtMdicalDrug = (DataTable)ViewState["internalMedicine"];
                            dtMdicalDrug.Rows.Add(ui.GetSQLInject(ddlMedicalDrugName.SelectedItem.ToString()), ui.GetSQLInject(txtMedicalDrugQty.Text.Trim()), ui.GetSQLInject(ddlMedicalMedUsage.SelectedItem.ToString()), ui.GetSQLInject(ddlMedicalDrugDays.SelectedItem.ToString()));
                            ViewState["internalMedicine"] = dtMdicalDrug;
                            this.bindInternalMedicine();

                            ddlMedicalDrugName.ClearSelection();
                            ddlMedicalDrugDays.ClearSelection();
                            ddlMedicalMedUsage.ClearSelection();
                            txtMedicalDrugQty.Text = "";
                        }
                    }
                }
            }
        }

        protected void OnRowDataBoundInternlMedicine(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                string item = e.Row.Cells[0].Text;
                foreach (Button button in e.Row.Cells[4].Controls.OfType<Button>())
                {
                    if (button.CommandName == "Delete")
                    {
                        button.Attributes["onclick"] = "if(!confirm('Do you want to delete " + item + "?')){ return false; };";
                    }
                }
            }
        }

        protected void OnRowDeletingInternlMedicine(object sender, GridViewDeleteEventArgs e)
        {
            int index = Convert.ToInt32(e.RowIndex);
            DataTable dt = ViewState["internalMedicine"] as DataTable;
            dt.Rows[index].Delete();
            ViewState["internalMedicine"] = dt;
            bindInternalMedicine();
        }

        #endregion

        #region Pharmacy Medicine

        protected void bindPharmacyMedicine()
        {
            grvPrescriptionDrug.DataSource = (DataTable)ViewState["pharmacyMedicine"];
            grvPrescriptionDrug.DataBind();
        }

        protected void insertPharmacyMedicine(object sender, EventArgs e)
        {
            if (ddlPrescriptionDrugName.SelectedIndex == 0)
            {
                pharmacyMedicineError.Visible = true;
                pharmacyMedicineError.Text = "Please select Medicine";
            }
            else
            {
                if (txtPrescriptionDrugQty.Text == "")
                {
                    pharmacyMedicineError.Visible = true;
                    pharmacyMedicineError.Text = "Please enter Qty";
                }
                else
                {
                    if (ddlPrescriptionMedUsage.SelectedIndex == 0)
                    {
                        pharmacyMedicineError.Visible = true;
                        pharmacyMedicineError.Text = "Please select Usage Timing";
                    }
                    else
                    {
                        if (ddlPresMedcDays.SelectedIndex == 0)
                        {
                            pharmacyMedicineError.Visible = true;
                            pharmacyMedicineError.Text = "Please select Usage Days";
                        }
                        else
                        {
                            pharmacyMedicineError.Visible = false;
                            DataTable dtPrescription = (DataTable)ViewState["pharmacyMedicine"];
                            dtPrescription.Rows.Add(ui.GetSQLInject(ddlPrescriptionDrugName.SelectedItem.ToString()), ui.GetSQLInject(txtPrescriptionDrugQty.Text.Trim()), ui.GetSQLInject(ddlPrescriptionMedUsage.SelectedItem.ToString()), ui.GetSQLInject(ddlPresMedcDays.SelectedItem.ToString()));
                            ViewState["pharmacyMedicine"] = dtPrescription;
                            this.bindPharmacyMedicine();
                            ddlPrescriptionDrugName.ClearSelection();
                            ddlPrescriptionMedUsage.ClearSelection();
                            ddlPresMedcDays.ClearSelection();
                            txtPrescriptionDrugQty.Text = "";
                        }
                    }
                }
            }
        }

        protected void OnRowDataBoundPharmacyMedicine(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                string item = e.Row.Cells[0].Text;
                foreach (Button button in e.Row.Cells[4].Controls.OfType<Button>())
                {
                    if (button.CommandName == "Delete")
                    {
                        button.Attributes["onclick"] = "if(!confirm('Do you want to delete " + item + "?')){ return false; };";
                    }
                }
            }
        }

        protected void OnRowDeletingPharmacyMedicine(object sender, GridViewDeleteEventArgs e)
        {
            int index = Convert.ToInt32(e.RowIndex);
            DataTable dt = ViewState["pharmacyMedicine"] as DataTable;
            dt.Rows[index].Delete();
            ViewState["pharmacyMedicine"] = dt;
            bindPharmacyMedicine();
        }

        #endregion


    }
}