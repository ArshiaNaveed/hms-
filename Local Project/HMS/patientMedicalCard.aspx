﻿<%@ Page Title="" Language="C#" MasterPageFile="~/HMS.Master" AutoEventWireup="true" CodeFile="patientMedicalCard.aspx.cs" Inherits="HMS.patientMedicalCard" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        .barcode img {
            height: 85px !important;
            margin-top: 20px !important;
            width: 80% !important;
        }
    </style>
    <link href="assets/css/bootstrapcard.css" rel="stylesheet" />
    <script src="https://ajax.googleapis.com/ajax/libs/jquery/3.3.1/jquery.min.js"></script>
    <script src="https://maxcdn.bootstrapcdn.com/bootstrap/4.0.0/js/bootstrap.min.js"></script>
    <script src="https://cdnjs.cloudflare.com/ajax/libs/html2canvas/0.4.1/html2canvas.js"></script>
    <script src="https://cdnjs.cloudflare.com/ajax/libs/jspdf/1.0.272/jspdf.debug.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="row clearfix">
        <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12">
            <div class="card" style="height: 300px;">
                <div class="header">
                    <h2>Patient Medical Card
                            </h2>
                </div>
                <div class="col-md-10">
                    <div class="form-group form-float">
                        <div class="form-line">
                            <asp:TextBox ID="txtCardNumber" runat="server" CssClass="form-control"></asp:TextBox>
                            <label class="form-label">Card Number</label>
                        </div>
                    </div>
                </div>
                <div class="col-md-10">
                    <div class="form-group form-float">
                        <div style="width: 8%; float: left;">
                            <asp:Button ID="btnSearch" runat="server" Text="Search" CssClass="btn btn-primary waves-effect" OnClick="btnSearch_Click" />
                        </div>
                        <div id="card" runat="server" visible="false">
                            <asp:Button ID="btnPrint" runat="server" Text="Print" CssClass="btn btn-primary waves-effect" OnClick="btnPrint_Click" />
                            <input type="button" class="btn btn-primary waves-effect" data-html2canvas-ignore="true" value="Export to PDF" id="btnExport" onclick="testPDF()" />
                        </div>
                    </div>
                </div>
                <div class="col-md-10" id="cardBody" runat="server" visible="false">
                    <div id="reportPage" style="background-color: white;">
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

                        <script type="text/javascript">

                            function testPDF() {
                                html2canvas(document.getElementById("reportPage"), {
                                    onrendered: function (canvas) {

                                        var imgData = canvas.toDataURL('image/png');
                                        console.log('Report Image URL: ' + imgData);
                                        var doc = new jsPDF('p', 'mm', [297, 510]); //210mm wide and 297mm high

                                        doc.addImage(imgData, 'PNG', 10, 10);
                                        //doc.save('EmployeeCard.pdf');

                                        var pdfName = 'PatientCard_' + (Math.floor(Math.random() * 99999999) + 1);

                                        setTimeout(function () {
                                            doc.save(pdfName + '.pdf');
                                        }, 1000);
                                    }
                                });
                            }
                        </script>
                    </div>
                    <div class="col-md-12">
                        <asp:Label ID="lblError" runat="server" Style="color: red;" Visible="false"></asp:Label>
                        <asp:Label ID="lblEmpId" runat="server" Visible="false"></asp:Label>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>