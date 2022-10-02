<%@ Page Language="C#" AutoEventWireup="true" CodeFile="tokenPrint.aspx.cs" Inherits="HMS.tokenPrint" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>YIFF Token</title>

    <style>
        .bold {
            font-weight: bold;
        }

        .fontfamily {
            font-family: Verdana;
        }

        .size14 {
            font-size: 14px;
        }

        .size13 {
            font-size: 10px;
        }

        .w30 {
            width: 37%;
        }

        .w70 {
            width: 60%;
        }

        .w60 {
            width: 60%;
        }

        .lft {
            float: left;
        }

        .rgt {
            float: right;
        }

        .barcode img {
            height: 100px!important;
            width: 100px!important;
        }

    </style>

</head>
<body>
    <form id="form1" runat="server">
        <div style="width: 33%; height: 350px;">
            <div style="width: 100%;">
                <center>
                        <img src="assets/images/Hms-Logo.png" style="width: 20%; margin-top: 8px; margin-left: 15px;" />
                            </center>
                <div style="float: left; width: 100%; font-size: 11px; font-weight: bold; font-family: sans-serif;">
                    <center>
                        <span>YOUSUF IQBAL FAMILY FOUNDATION</span>
                <br />
                        <span>Area 5-D house 12 street 10 Landhi 6 Karachi</span>
                            </center>
                </div>
            </div>
            <div style="margin-top: 44px; margin-bottom: 8px;">
                <center>
                    <span class="bold fontfamily size14">
                        <asp:Label ID="lblTokenNumber" runat="server"></asp:Label>
                    </span>
                </center>
            </div>
            <div style="height: 160px; padding: 8px; width: 100%;">
                <div style="width: 100%;">
                    <div class="w30 lft">
                        <span class="bold size13 fontfamily">Card Number:</span>
                    </div>
                    <div class="w70 lft">
                        <asp:Label ID="lblCardNumber" runat="server" CssClass="fontfamily size13"></asp:Label>
                    </div>
                </div>
                <div style="width: 100%;">
                    <div class="w30 lft">
                        <span class="bold size13 fontfamily">Patient Name:</span>
                    </div>
                    <div class="w70 lft">
                        <asp:Label ID="lblPatienName" runat="server" CssClass="fontfamily size13"></asp:Label>
                    </div>
                </div>
                <div style="width: 100%;">
                    <div class="w30 lft">
                        <span class="bold size13 fontfamily">Doctor Name:</span>
                    </div>
                    <div class="w70 lft">
                        <asp:Label ID="lblDoctorName" runat="server" CssClass="fontfamily size13"></asp:Label>
                    </div>
                </div>
                <div style="width: 100%;">
                    <div class="w30 lft">
                        <span class="bold size13 fontfamily">Appointment:</span>
                    </div>
                    <div class="w70 lft">
                        <asp:Label ID="lblDOA" runat="server" CssClass="fontfamily size13"></asp:Label>
                    </div>
                </div>
                <div style="width: 100%;">
                    <div class="w30 lft">
                        <span class="bold size13 fontfamily">Fee:</span>
                    </div>
                    <div class="w70 lft">
                        <asp:Label ID="lblFee" runat="server" CssClass="fontfamily size13"></asp:Label>
                    </div>
                </div>
                <div style="width: 100%;">
                    <div class="barcode" style="font-size: x-large; font-weight: bold">
                        <center> 
                            <asp:PlaceHolder ID="PlaceHolder1" runat="server" ></asp:PlaceHolder> 
                        </center>
                    </div>
                </div>
            </div>
        </div>
    </form>
</body>
</html>
