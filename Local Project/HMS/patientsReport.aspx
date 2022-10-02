<%@ Page Language="C#" AutoEventWireup="true" CodeFile="patientsReport.aspx.cs" Inherits="HMS.patientsReport" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Patients Report</title>
    <link rel="stylesheet" type="text/css" href="/assets/css/style.css" />
    <link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/bootstrap/4.0.0/css/bootstrap.min.css" />
    <meta name="viewport" content="width=device-width, initial-scale=1">
</head>
<body class="container-fluid" onload="window.print()">
    <div class="row container-fluid">
        <div class="col-12">
            <div class="row container-fluid font-weight-bolder">
                <div class="col-4">
                    <img src="assets/images/Hms-Logo.png" style="width: 13%; margin-top: 12px;" />
                </div>
                <div class="col-4">
                    <center>  <div class="row text-capitalize text-center">
                       <h2 style="margin-top: 20px;" >Patients Report</h2>
                    </div></center>
                </div>
                <div class="col-4">
                    <div>
                        <div class="row">
                            <div class="col-6">
                                Printing Date:
                            </div>
                            <div class="col-6">
                                <asp:Label runat="server" CssClass="control-label" ID="lblDate"></asp:Label>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-6">Printing Time:</div>
                            <div class="col-6">
                                <asp:Label runat="server" CssClass="control-label" ID="lblTime"></asp:Label>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-6">Print By:</div>
                            <div class="col-6">
                                <asp:Label runat="server" CssClass="control-label" ID="lblUserName"></asp:Label>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <hr />

    <div class="row container-fluid">
        <div class="col-12 col-sm-12" id="divResult" runat="server" visible="false">
            <table id="example" style="width:100%!important" class="table table-bordered table-striped table-hover dataTable js-exportable">
                                    <thead>
                                        <tr>
                                            <th>#
                                            </th>
                                            <th>Card Number
                                            </th>
                                            <th>Patient Name
                                            </th>
                                            <th>Gender
                                            </th>
                                            <th>Age
                                            </th>
                                            <th>Contact #
                                            </th>
                                          
                                        </tr>
                                    </thead>
                                    <tbody>
                                        <asp:Repeater ID="rptPatient" runat="server">
                                            <ItemTemplate>
                                                <tr>
                                                    <td>
                                                        <asp:Label ID="lblPatientIdx" runat="server" Text='<%# Eval("idx")%>' Visible="false"></asp:Label>
                                                        <center>
                                                    <%# Eval("sn")%>
                                                </center>
                                                    </td>
                                                    <td>
                                                        <%# Eval("cardNumber")%>
                                                    </td>
                                                    <td>
                                                        <%# Eval("patientName")%>
                                                    </td>
                                                    <td>
                                                        <%# Eval("genderName")%>
                                                    </td>
                                                    <td>
                                                        <%# Eval("age")%>
                                                    </td>
                                                    <td>
                                                        <%# Eval("contactNumber1")%>
                                                    </td>
                                                   
                                                </tr>
                                            </ItemTemplate>
                                        </asp:Repeater>
                                    </tbody>
                                </table>
            <asp:Label ID="lblError" runat="server" Visible="false"></asp:Label>
        </div>
    </div>
</body>
</html>
