<%@ Page Title="" Language="C#" MasterPageFile="~/HMS.Master" AutoEventWireup="true" CodeFile="patientMedicalLog.aspx.cs" Inherits="HMS.patientMedicalLog" %>

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
                    <h2>Patient Internal Medical Log
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
                <div class="col-md-2">
                    <div class="form-group form-float">
                        <asp:Button ID="btnSearch" runat="server" Text="Search" CssClass="btn btn-primary waves-effect" OnClick="btnSearch_Click" />
                    </div>
                </div>
                <div class="body">
                    <div class="col-md-12">
                        <div class="form-group form-float">
                            <asp:Label ID="lblError" runat="server" Text="Record Not Found!" style="color:red;" Visible="false" ></asp:Label>
                            <div class="panel panel-primary" id="pnlMain" runat="server" visible="false">
                                <div class="panel-heading" role="tab" id="headingOne_1">
                                    <h4 class="panel-title">
                                        <a role="button" data-toggle="collapse" data-parent="#accordion_1" href="#collapseOne_1" aria-expanded="true" aria-controls="collapseOne_1">Date: &nbsp
                                                <asp:Label ID="lblDate" runat="server"></asp:Label>
                                        </a>
                                    </h4>
                                </div>
                                <div id="collapseOne_1"  class="panel-collapse collapse in" role="tabpanel" aria-labelledby="headingOne_1">
                                    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                        <ContentTemplate>
                                            <div class="panel-body">
                                                <div class="col-md-6">
                                                    <span><b>Card Number:</b>&nbsp</span>
                                                    <asp:Label ID="lblCardNumber" runat="server"></asp:Label>
                                                </div>
                                                <div class="col-md-6">
                                                    <span><b>Patient Name:</b>&nbsp</span>
                                                    <asp:Label ID="lblPatientName" runat="server"></asp:Label>
                                                </div>
                                                <div class="col-md-6">
                                                    <span><b>Age:</b>&nbsp</span>
                                                    <asp:Label ID="lblAge" runat="server"></asp:Label>
                                                </div>
                                                <div class="col-md-6">
                                                    <span><b>Priamry Contact #:</b>&nbsp</span>
                                                    <asp:Label ID="lblContactNumber1" runat="server"></asp:Label>
                                                </div>
                                                <div class="col-md-6">
                                                    <span><b>Secondary Contact #:</b>&nbsp</span>
                                                    <asp:Label ID="lblContactNumber2" runat="server"></asp:Label>
                                                </div>
                                                <div class="col-md-12">
                                                    <ol class="breadcrumb breadcrumb-bg-grey">
                                                            <li class="active"><i class="material-icons">library_books</i> Appointment Details</li>
                                                        </ol>
                                                </div>
                                                <div class="col-md-6">
                                                    <span><b>Token Number:</b>&nbsp</span>
                                                    <asp:Label ID="lblTokenNumber" runat="server"></asp:Label>
                                                </div>
                                                <div class="col-md-6">
                                                    <span><b>Doctor:</b>&nbsp</span>
                                                    <asp:Label ID="lblDoctor" runat="server"></asp:Label>
                                                </div>
                                                <div class="col-md-6">
                                                    <span><b>Appointment Date:</b>&nbsp</span>
                                                    <asp:Label ID="lblAppointmentDate" runat="server"></asp:Label>
                                                </div>
                                               
                                                <div class="col-md-12">
                                                   <ol class="breadcrumb breadcrumb-bg-grey">
                                                            <li class="active"><i class="material-icons">library_books</i> Internal Medicine</li>
                                                        </ol>
                                                </div>
                                                <div class="col-md-12">
                                                    <div class="table-responsive">
                                                        <table class="table table-bordered table-striped table-hover dataTable js-exportable">
                                                            <thead>
                                                                <tr>
                                                                    <th>#
                                                                    </th>
                                                                    <th>Medicine Name
                                                                    </th>
                                                                    <th>Qty
                                                                    </th>
                                                                    <th>Usage Timing
                                                                    </th>
                                                                    <th>Usage Days
                                                                    </th>
                                                                </tr>
                                                            </thead>
                                                            <tbody>
                                                                <asp:Repeater ID="rptMedicalLog" runat="server">
                                                                    <ItemTemplate>
                                                                        <tr>
                                                                            <td>
                                                                                <asp:Label ID="lblUserIdx" runat="server" Text='<%# Eval("idx")%>' Visible="false"></asp:Label>
                                                                                <center>
                                                                                    <%# Eval("sn")%>
                                                                                </center>
                                                                            </td>
                                                                            <td>
                                                                                <%# Eval("medicineName")%>
                                                                            </td>
                                                                            <td>
                                                                                <%# Eval("qty")%>
                                                                            </td>
                                                                            <td>
                                                                                <%# Eval("usage")%>
                                                                            </td>
                                                                            <td>
                                                                                <%# Eval("days")%>
                                                                            </td>
                                                                        </tr>
                                                                    </ItemTemplate>
                                                                </asp:Repeater>
                                                            </tbody>
                                                        </table>
                                                    </div>
                                                </div>
                                            </div>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
