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

using iTextSharp.text;
using iTextSharp.text.html.simpleparser;
using iTextSharp.text.pdf;

namespace HMS
{
    public partial class exportEmployeeCard : System.Web.UI.Page
    {
        //Models.Utilities ui = new Models.Utilities();
        Utilities ui = new Utilities();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["sysAccess"] != null)
                {
                    DataTable dtSysAccess = (DataTable)Session["sysAccess"];
                    if (dtSysAccess.Rows[0]["empCardPrint"].ToString() == "0")
                    {
                        Session["page"] = "Employee ID Card Search / Print";
                        Response.Redirect("404.aspx");
                    }
                }

                if (Session["empIdForPrint"] != null)
                {
                    fillEmployeeDetails();
                    exportToPdf();
                }
            }
        }

        protected void exportToPdf()
        {
            Response.ContentType = "application/pdf";
            Response.AddHeader("content-disposition", "attachment;filename=Panel.pdf");
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            StringWriter stringWriter = new StringWriter();
            HtmlTextWriter htmlTextWriter = new HtmlTextWriter(stringWriter);
            //Panel1.RenderControl(htmlTextWriter);
            StringReader stringReader = new StringReader(stringWriter.ToString());
            Document Doc = new Document(PageSize.A4, 10f, 10f, 100f, 0f);
            HTMLWorker htmlparser = new HTMLWorker(Doc);
            PdfWriter.GetInstance(Doc, Response.OutputStream);
            Doc.Open();
            htmlparser.Parse(stringReader);
            Doc.Close();
            Response.Write(Doc);
            Response.End();
        }


        protected void fillEmployeeDetails()
        {
            try
            {
                DataTable dt = new DataTable();
                dt = ui.FetchinControldt(@"select u.idx, (u.firstName + ' ' + u.lastName) as employeeName, u.cnic, u.isActive, u.doj, d.designationName 
                                            from users u
                                            inner join designation d on d.idx = u.designationIdx
                                            where u.idx = " + Session["empIdForPrint"].ToString() + " and u.visible = 1");
                if (dt.Rows.Count > 0)
                {
                    lblEmployeeID.Text = dt.Rows[0]["idx"].ToString();
                    string empName = dt.Rows[0]["employeeName"].ToString();
                    lblEmployeeName.Text = empName.ToUpper();
                    lblDesignation.Text = dt.Rows[0]["designationName"].ToString();
                    lblJoiningDate.Text = dt.Rows[0]["doj"].ToString();
                    lblCnic.Text = dt.Rows[0]["cnic"].ToString();
                    barcode(lblEmployeeID.Text);
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