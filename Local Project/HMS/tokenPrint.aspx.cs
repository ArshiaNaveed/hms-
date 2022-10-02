using System;
using System.Data;
using System.Drawing;
using System.IO;
using QRCoder;

namespace HMS
{
    public partial class tokenPrint : System.Web.UI.Page
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
                        Session["page"] = "Appointment / Token Print";
                        Response.Redirect("404.aspx");
                    }
                }

                if (Session["tokenIdx"] != null)
                {
                    getTokenDetails();
                }
            }
        }
        protected void getTokenDetails()
        {
            try
            {
                DataTable dt = new DataTable();
                dt = ui.FetchinControldt(@"select t.idx, t.tokenNumber, p.cardNumber, p.patientName, (u.firstName + ' '+ u.lastName) as doctorName, t.appointmentDate, t.fee from token t
                                        inner join patentRegistration p on p.idx = t.patientIdx
                                        inner join users u on u.idx = t.physicianIdx
                                        where t.visible = 1 and t.idx = " + Session["tokenIdx"].ToString());
                if (dt.Rows.Count > 0)
                {
                    lblCardNumber.Text = dt.Rows[0]["cardNumber"].ToString();
                    lblPatienName.Text = dt.Rows[0]["patientName"].ToString();
                    lblDoctorName.Text = "Dr. " + dt.Rows[0]["doctorName"].ToString();
                    lblDOA.Text = dt.Rows[0]["appointmentDate"].ToString();
                    lblFee.Text = dt.Rows[0]["fee"].ToString();
                    lblTokenNumber.Text = dt.Rows[0]["tokenNumber"].ToString();
                    barcode(lblTokenNumber.Text);
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