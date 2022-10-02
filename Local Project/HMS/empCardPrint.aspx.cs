using System;
using System.Data;
using System.Web.UI;
using System.Drawing;
using System.IO;
using QRCoder;

namespace HMS
{
    public partial class empCardPrint : System.Web.UI.Page
    {
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
                GetAccessRights();
            }
        }
        private void GetAccessRights()
        {
            DataTable dt = new DataTable();
            dt = ui.FetchinControldtPara(@"SELECT r.userIdx,u.idx,u.pageUrl FROM Roles r inner join Url u On u.idx = r.pageUrl Where r.visible = 1 and r.userIdx =  @param AND u.idx='4'", Session["appUserId"].ToString());
            if (dt.Rows.Count > 0)
            {
                if (dt.Rows[0]["pageUrl"].ToString() == "empCardPrint.aspx")
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
                if (txtEmpId.Text != null)
                {
                    DataTable dt = new DataTable();
                    dt = ui.FetchinControldt(@"select u.idx, (u.firstName + ' ' + u.lastName) as employeeName, u.cnic, u.isActive from users u
                                            where u.idx = " + ui.GetSQLInject(txtEmpId.Text) + " and u.visible = 1 and u.idx != 1");
                    if (dt.Rows.Count > 0)
                    {
                        if (dt.Rows[0]["isActive"].ToString() == "1")
                        {
                            card.Visible = true;
                            cardBody.Visible = true;
                            Session["empCardIdx"] = dt.Rows[0]["idx"].ToString();
                            lblError.Visible = false;
                            fillEmployeeDetails();
                        }
                        else
                        {
                            lblError.Visible = true;
                            lblError.Text = "The Employee is not active.";
                           
                        }
                    }
                    else
                    {
                        card.Visible = false;
                        cardBody.Visible = false;
                        lblError.Visible = true;
                        lblError.Text = "The Employee is not available.";
                        
                    }
                }
            }
            catch (Exception ex)
            { }
        }

        protected void btnPrint_Click(object sender, EventArgs e)
        {
            Session["empIdForPrint"] = Session["empCardIdx"].ToString();
            OpenNewBrowserWindow("employeeCard.aspx", this);
        }
        public static void OpenNewBrowserWindow(string Url, Control control)
        {
            ScriptManager.RegisterStartupScript(control, control.GetType(), "Open", "window.open('" + Url + "','','top=100px,bottom=100px,right=100px,left=100px, directories=no,menubar=no,toolbar=no,location=no,resizable=no,height=1000px,width=1500,status=no,scrollbars=yes,maximize=null,resizable=0,titlebar=no');", true);
        }

        protected void fillEmployeeDetails()
        {
            try
            {
                DataTable dt = new DataTable();
                dt = ui.FetchinControldt(@"select u.idx, (u.firstName + ' ' + u.lastName) as employeeName, u.cnic, u.isActive, u.doj, d.designationName 
                                            from users u
                                            inner join designation d on d.idx = u.designationIdx
                                            where u.idx = " + Session["empCardIdx"].ToString() + " and u.visible = 1");
                if (dt.Rows.Count > 0)
                {
                    lblEmployeeID.Text = dt.Rows[0]["idx"].ToString();
                    string empName = dt.Rows[0]["employeeName"].ToString();
                    lblEmployeeName.Text = empName.ToUpper();
                    lblDesignation.Text = dt.Rows[0]["designationName"].ToString();
                    lblJoiningDate.Text = dt.Rows[0]["doj"].ToString();
                    lblCnic.Text = dt.Rows[0]["cnic"].ToString();
                    barcode(dt.Rows[0]["idx"].ToString());
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