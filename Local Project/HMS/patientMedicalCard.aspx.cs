using System;
using System.Data;
using System.Web.UI;
using System.Drawing;
using System.IO;
using QRCoder;

namespace HMS
{
    public partial class patientMedicalCard : System.Web.UI.Page
    {
        Utilities ui = new Utilities();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["sysAccess"] != null)
                {
                    DataTable dtSysAccess = (DataTable)Session["sysAccess"];
                    if (dtSysAccess.Rows[0]["medicalCardPrint"].ToString() == "0")
                    {
                        Session["page"] = "Patients Medical Card Search / Print";
                        Response.Redirect("404.aspx");
                    }
                }
                GetAccessRights();
            }
        }
        private void GetAccessRights()
        {
            DataTable dt = new DataTable();
            dt = ui.FetchinControldtPara(@"SELECT r.userIdx,u.idx,u.pageUrl FROM Roles r inner join Url u On u.idx = r.pageUrl Where r.visible = 1 and r.userIdx =  @param AND u.idx='7'", Session["appUserId"].ToString());
            if (dt.Rows.Count > 0)
            {
                if (dt.Rows[0]["pageUrl"].ToString() == "patientMedicalCard.aspx")
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
                if (txtCardNumber.Text != null)
                {
                    DataTable dt = new DataTable();
                    dt = ui.FetchinControldt(@"select p.idx, p.cardNumber, p.patientName, Convert(varchar(50), p.creationDate, 103) as registrationDate from patentRegistration p where p.cardNumber = " + ui.GetSQLInject(txtCardNumber.Text) + " and p.visible = 1");
                    if (dt.Rows.Count > 0)
                    {
                        card.Visible = true;
                        cardBody.Visible = true;
                        Session["patientCardId"] = dt.Rows[0]["cardNumber"].ToString();
                        fillPatientDetails();
                        lblError.Visible = false;
                    }
                    else
                    {
                        card.Visible = false;
                        cardBody.Visible = false;
                        lblError.Visible = true;
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "", "<script>showErrorMessage()</script>", false);
                    }
                }
            }
            catch (Exception ex)
            { }
        }

        protected void btnPrint_Click(object sender, EventArgs e)
        {
            Session["cardNumberForPrint"] = Session["patientCardId"].ToString();
            OpenNewBrowserWindow("patientCard.aspx", this);
        }

        public static void OpenNewBrowserWindow(string Url, Control control)
        {
            ScriptManager.RegisterStartupScript(control, control.GetType(), "Open", "window.open('" + Url + "','','top=100px,bottom=100px,right=100px,left=100px, directories=no,menubar=no,toolbar=no,location=no,resizable=no,height=1000px,width=1500,status=no,scrollbars=yes,maximize=null,resizable=0,titlebar=no');", true);
        }

        protected void fillPatientDetails()
        {
            try
            {
                DataTable dt = new DataTable();
                dt = ui.FetchinControldt(@"select p.idx, p.cardNumber, p.patientName, Convert(varchar(50), p.creationDate, 103) as registrationDate from patentRegistration p where p.cardNumber = " + Session["patientCardId"].ToString() + " and p.visible = 1");
                if (dt.Rows.Count > 0)
                {
                    lblCardNumber.Text = dt.Rows[0]["cardNumber"].ToString();
                    string patientName = dt.Rows[0]["patientName"].ToString();
                    lblPatientName.Text = patientName.ToUpper();
                    lblRegistrationDate.Text = dt.Rows[0]["registrationDate"].ToString();
                    barcode(dt.Rows[0]["cardNumber"].ToString());
                }
            }
            catch (Exception ex)
            { }
        }
        protected void barcode(string num)
        {
            QRCodeGenerator qrGenerator = new QRCodeGenerator();
            QRCodeGenerator.QRCode qrCode = qrGenerator.CreateQrCode(num, QRCodeGenerator.ECCLevel.Q);
            System.Web.UI.WebControls.Image imgBarCode = new System.Web.UI.WebControls.Image();
            imgBarCode.Height = 150;
            imgBarCode.Width = 150;
            using (Bitmap bitMap = qrCode.GetGraphic(20))
            {
                using (MemoryStream ms = new MemoryStream())
                {
                    bitMap.Save(ms, System.Drawing.Imaging.ImageFormat.Png);
                    byte[] byteImage = ms.ToArray();
                    imgBarCode.ImageUrl = "data:image/png;base64," + Convert.ToBase64String(byteImage);
                }
                PlaceHolder1.Controls.Add(imgBarCode);
            }
        }
    }
}