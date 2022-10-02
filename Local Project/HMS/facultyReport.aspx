<%@ Page Language="C#" AutoEventWireup="true" CodeFile="facultyReport.aspx.cs" Inherits="HMS.facultyReport" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Faculty Report</title>
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
                       <h2 style="margin-top: 20px;" >Faculty Report</h2>
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
            <table class="table table-bordered table-striped table-hover dataTable js-exportable">
                <thead>
                    <tr>
                        <th>#
                        </th>
                        <th>Emp ID
                        </th>
                        <th>Full Name
                        </th>
                        <th>Department
                        </th>
                        <th>Designation
                        </th>
                        <th>User Type
                        </th>
                        <th>Status
                        </th>
                       
                    </tr>
                </thead>
                <tbody>
                    <asp:Repeater ID="rptUsers" runat="server">
                        <ItemTemplate>
                            <tr>
                                <td>
                                    <center>
                                                    <%# Eval("sn")%>
                                                </center>
                                </td>
                                <td>
                                    <asp:Label ID="lblUserIdx" runat="server" Text='<%# Eval("idx")%>'></asp:Label>
                                </td>
                                <td>
                                    <%# Eval("fullName")%>
                                </td>
                                <td>
                                    <%# Eval("departmentName")%>
                                </td>
                                <td>
                                    <%# Eval("designationName")%>
                                </td>
                                <td>
                                    <%# Eval("userTypeName")%> &nbsp / &nbsp <%# Eval("specialty")%>
                                </td>
                                <td>
                                    <%# Eval("status")%>
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
