<%@ Page Language="C#" AutoEventWireup="true" CodeFile="allDrAccountReportPrint.aspx.cs" Inherits="HMS.allDrAccountReportPrint" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Accounts Report</title>
    <link href="https://fonts.googleapis.com/css?family=Roboto:400,700&subset=latin,cyrillic-ext" rel="stylesheet" type="text/css">
    <link href="https://fonts.googleapis.com/icon?family=Material+Icons" rel="stylesheet" type="text/css">
    <!-- Bootstrap Core Css -->
    <link href="assets/plugins/bootstrap/css/bootstrap.css" rel="stylesheet">
</head>
<body>
    <form id="form1" runat="server">
        <div class="row clearfix">
            <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12">
                <div class="header">
                    <div class="col-lg-6 col-md-6 col-sm-6 col-xs-6">
                        <img src="assets/images/Hms-Logo.png" style="width: 15%; padding: 10px;" />
                    </div>
                    <div class="col-lg-6 col-md-6 col-sm-6 col-xs-6">
                        <br />
                        <br />
                        <br />
                        <div style="float: right;">
                            <span><b>Report Date:</b> &nbsp
                                <asp:Label ID="lblDate" runat="server"></asp:Label>
                            </span>
                            <br />
                            <span><b>Report By:</b> &nbsp
                                <asp:Label ID="lblReportBy" runat="server"></asp:Label>
                            </span>
                            <br />
                            <span><b>Report Name:</b> &nbsp
                                Accounts Report
                            </span>
                            <br />
                            <span><b>Report of:</b> All Doctors
                            </span>
                        </div>
                    </div>
                </div>
                <div class="" style="height: 300px;">
                    <div class="col-md-12">
                        <div class="table-responsive">
                            <table id="example" class="table table-bordered table-striped table-hover dataTable js-exportable">
                            <thead>
                                <tr>
                                    <th>#
                                    </th>
                                    <th>Doctor Name
                                    </th>
                                    <th>Appointment Date
                                    </th>
                                    <th>Appointment Day
                                    </th>
                                    <th>Total Patients
                                    </th>
                                    <th>Fee
                                    </th>
                                </tr>
                            </thead>
                            <tbody>
                                <asp:Repeater ID="rptAllDoctors" runat="server">
                                    <ItemTemplate>
                                        <tr>
                                            <td>
                                                <center>
                                                    <%# Eval("sn")%>
                                                </center>
                                            </td>
                                            <td>
                                                <%# Eval("doctorName")%>
                                            </td>
                                            <td>
                                                <%# Eval("appointmentDate")%>
                                            </td>
                                            <td>
                                                <%# Eval("appointmentDay")%>
                                            </td>
                                            <td>
                                                <%# Eval("totalPatients")%>
                                            </td>
                                            <td>
                                                <%# Eval("fee")%>
                                            </td>
                                        </tr>
                                    </ItemTemplate>
                                </asp:Repeater>
                            </tbody>
                        </table>
                        </div>
                    </div>
                    <div class="col-md-12">
                        <div class="col-lg-6 col-md-6 col-sm-6 col-xs-6">
                            &nbsp
                        </div>
                        <div class="col-lg-6 col-md-6 col-sm-6 col-xs-6">
                            <div style="float: right;">
                                <span><b>Total Patient(s):</b> &nbsp
                                    <asp:Label ID="lblTotalPatients" runat="server"></asp:Label>
                                </span>
                                <br />
                                <span><b>Total Amount:</b> &nbsp
                                    <asp:Label ID="lblTotalAmount" runat="server"></asp:Label>/-
                                </span>
                                <br />
                                <span><b>Amount In Words:</b> &nbsp
                                    RUPEES &nbsp<asp:Label ID="lblAmountInWords" runat="server"></asp:Label>
                                </span>

                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </form>
</body>
</html>
