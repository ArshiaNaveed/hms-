<%@ Page Title="" Language="C#" MasterPageFile="~/HMS.Master" AutoEventWireup="true" CodeFile="accountReport.aspx.cs" Inherits="HMS.accountReport" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        .dt-buttons {
        visibility:hidden!important;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div class="row clearfix">
        <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12">
            <div class="" style="height: 300px;">
                <div class="header">
                    <h2>Accounts Report
                            </h2>
                </div>
                <div class="col-md-12">
                    <div class="form-group form-float">
                        &nbsp
                    </div>
                </div>
                <div class="col-md-6">
                    <div class="form-group form-float">
                        <div class="form-line">
                            <asp:TextBox ID="txtFromDate" runat="server" CssClass="form-control"></asp:TextBox>
                            <label class="form-label">From Date</label>
                            <asp:CalendarExtender ID="CalendarExtender1" runat="server" Format="dd/MM/yyyy" TargetControlID="txtFromDate">
                            </asp:CalendarExtender>
                        </div>
                    </div>
                </div>
                <div class="col-md-6">
                    <div class="form-group form-float">
                        <div class="form-line">
                            <asp:TextBox ID="txtToDate" runat="server" CssClass="form-control"></asp:TextBox>
                            <label class="form-label">To Date</label>
                            <asp:CalendarExtender ID="CalendarExtender2" runat="server" Format="dd/MM/yyyy" TargetControlID="txtToDate">
                            </asp:CalendarExtender>
                        </div>
                    </div>
                </div>
                <div class="col-md-6">
                    <div class="form-group form-float">
                        <div class="form-line">
                            <asp:DropDownList ID="ddlRegPhysision" runat="server" CssClass="form-control show-tick">
                            </asp:DropDownList>
                        </div>
                    </div>
                </div>
                <div class="col-md-2">
                    <div class="form-group form-float">
                        <asp:Button ID="btnSearch" runat="server" Text="Search" CssClass="btn btn-primary waves-effect" OnClick="btnSearch_Click" />
                         <asp:Button ID="btnPrint" runat="server" Text="Print" CssClass="btn btn-success waves-effect" OnClick="btnPrint_Click" />
                    </div>
                </div>
                <div class="col-md-12" id="physitionList" runat="server" visible="false">
                    <div class="table-responsive">
                        <table id="example" class="table table-bordered table-striped table-hover dataTable js-exportable">
                            <thead>
                                <tr>
                                    <th>#
                                    </th>
                                    <th>Doctor Name
                                    </th>
                                    <th>Card Number
                                    </th>
                                    <th>Appointment Date
                                    </th>
                                    <th>Appointment Day
                                    </th>
                                    <th>Patient Name
                                    </th>
                                    <th>Gender
                                    </th>
                                    <th>Age
                                    </th>
                                    <th>Fee
                                    </th>
                                </tr>
                            </thead>
                            <tbody>
                                <asp:Repeater ID="rptAccount" runat="server">
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
                                                <%# Eval("cardNumber")%>
                                            </td>
                                            <td>
                                                <%# Eval("appointmentDate")%>
                                            </td>
                                            <td>
                                                <%# Eval("appointmentDay")%>
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
                                                <%# Eval("fee")%>
                                            </td>
                                        </tr>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <strong>Total: </strong><%#totalsal()%> /-<br />
                                        <strong>Total in Words: </strong>RUPEES &nbsp <%#totalsalinWords()%> ONLY<br />
                                        <br />
                                    </FooterTemplate>
                                </asp:Repeater>
                            </tbody>
                        </table>
                    </div>
                </div>
                <div class="col-md-12" id="allDoctorsList" runat="server" visible="false">
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
                                    <FooterTemplate>
                                        <strong>Total: </strong><%#totalsal()%>/-<br />
                                        <strong>Total in Words: </strong>RUPEES &nbsp <%#totalsalinWords()%> ONLY<br />
                                        <br />
                                    </FooterTemplate>
                                </asp:Repeater>
                            </tbody>
                        </table>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
