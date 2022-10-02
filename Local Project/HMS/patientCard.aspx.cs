using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Drawing.Text;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using QRCoder;

namespace HMS
{
    public partial class patientCard : System.Web.UI.Page
    {
        Utilities ui = new Utilities();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["sysAccess"] != null)
                {
                    DataTable dtSysAccess = (DataTable)Session["sysAccess"];
                    if (dtSysAccess.Rows[0]["patients"].ToString() == "0")
                    {
                        Session["page"] = "Patients Medical Card Print";
                        Response.Redirect("404.aspx");
                    }
                }

                if (Session["cardNumberForPrint"] != null)
                {
                    fillPatientDetails();
                }
            }
        }
        protected void fillPatientDetails()
        {
            try
            {
                DataTable dt = new DataTable();
                dt = ui.FetchinControldt(@"select p.idx, p.cardNumber, p.patientName, Convert(varchar(50), p.creationDate, 103) as registrationDate from patentRegistration p where p.cardNumber = " + Session["cardNumberForPrint"].ToString() + " and p.visible = 1");
                if (dt.Rows.Count > 0)
                {
                    lblCardNumber.Text = dt.Rows[0]["cardNumber"].ToString();
                    string patientName = dt.Rows[0]["patientName"].ToString();
                    lblPatientName.Text = patientName.ToUpper();
                    lblRegistrationDate.Text = dt.Rows[0]["registrationDate"].ToString();
                    barcode(lblCardNumber.Text);
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