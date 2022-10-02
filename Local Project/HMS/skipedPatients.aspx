<%@ Page Title="" Language="C#" MasterPageFile="~/HMS.Master" AutoEventWireup="true" CodeFile="skipedPatients.aspx.cs" Inherits="HMS.skipedPatients" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
       .dt-buttons {
        display:none;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="row clearfix">
        <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12">
            <div class="card">
                <div class="header">
                    <h2>Skipped Patients
                            </h2>
                </div>
                <div class="body">
                    <div class="table-responsive">
                        <asp:UpdatePanel ID="up1" runat="server">
                            <ContentTemplate>
                                <table class="table table-bordered table-striped table-hover dataTable js-exportable">
                                    <thead>
                                        <tr>
                                            <th>#
                                            </th>
                                            <th>Patients Nmae
                                            </th>
                                            <th>Card Number
                                            </th>
                                            <th>Token Number
                                            </th>
                                            <th>Appointment Date
                                            </th>
                                            <th>Contact Number
                                            </th>
                                            <th>Action
                                            </th>
                                        </tr>
                                    </thead>
                                    <tbody>
                                        <asp:Repeater ID="rptSkippedPatients" runat="server">
                                            <ItemTemplate>
                                                <tr>
                                                    <td>
                                                        <asp:Label ID="lblPatientIdx" runat="server" Text='<%# Eval("patientIdx")%>' Visible="false"></asp:Label>
                                                        <asp:Label ID="lblTokenIdx" runat="server" Text='<%# Eval("tokenIdx")%>' Visible="false"></asp:Label>
                                                        <center>
                                                    <%# Eval("sn")%>
                                                </center>
                                                    </td>
                                                    <td>
                                                        <%# Eval("patientName")%>
                                                    </td>
                                                    <td>
                                                        <%# Eval("cardNumber")%>
                                                    </td>
                                                    <td>
                                                        <%# Eval("tokenNumber")%>
                                                    </td>
                                                    <td>
                                                        <%# Eval("appointmentDate")%>
                                                    </td>
                                                    <td>
                                                        <%# Eval("contactNumber1")%>&nbsp/&nbsp<%# Eval("contactNumber2")%>
                                                    </td>
                                                    <td>
                                                        <asp:LinkButton ID="lnkTreatment" runat="server" Text="Start Treatment" CommandArgument='<%# Eval("tokenIdx") %>'
                                                            ToolTip="Start Treatment" class="btn" OnClick="lnkTreatment_Click"><i class="glyphicon glyphicon-eye-open"></i></asp:LinkButton>
                                                    </td>
                                                </tr>
                                            </ItemTemplate>
                                        </asp:Repeater>
                                    </tbody>
                                </table>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>

