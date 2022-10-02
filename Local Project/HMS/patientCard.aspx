<%@ Page Language="C#" AutoEventWireup="true" CodeFile="patientCard.aspx.cs" Inherits="HMS.patientCard" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Patient Medical Card</title>
    <style>
        .barcode img {
            height: 85px !important;
            margin-top: 20px !important;
            width: 80% !important;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <center>
            <div style="width:336px; height:200px; border:1px solid; border-radius:7px; font-family:Verdana;">
                <div style="width:100%; height:60px;">
                    <div style="width:20%; float:left;">
                       <img src="assets/images/Hms-Logo.png" style="width: 50%;margin-top: 9px;" />
                    </div>
                    <div style="width:75%; float:right; font-size:10px;margin-top:10px;">
                       <center>
                            <span style="font-size: 11px; font-weight:bold; float:left;"> YOUSUF IQBAL FAMILY FOUNDATION</span>
                            <br />
                            <span style="margin-top:5px; float:left;">Area 5-D house 12 street 10 Landhi 6 Karachi</span>
                        </center>
                    </div>
                </div>
                <div style="width:100%; height:60px;">
                    <center>
                        <asp:Label ID="lblCardNumber" runat="server" style="font-size:22px;" ></asp:Label>
                    </center>
                </div>
                <div style="width:95%; height:60px; float:left; text-align:left;padding-left:10px;margin-top: -24px;">
                    <asp:Label ID="lblPatientName" runat="server"></asp:Label>
                    <br />
                    <span style="font-size:13px;" >DOR</span>&nbsp&nbsp<asp:Label ID="lblRegistrationDate" runat="server" style="font-size:13px;"></asp:Label>
                    <br />
                    <div class="barcode" style="font-size: x-large; font-weight: bold;margin-top: -46px;margin-left: 157px;">
                        <center> 
                            <asp:PlaceHolder ID="PlaceHolder1" runat="server" ></asp:PlaceHolder> 
                        </center>
                    </div>
                </div>
            </div>
        </center>
    </form>
</body>
</html>
