<%@ Page Title="" Language="C#" MasterPageFile="~/HMS.Master" AutoEventWireup="true" CodeFile="treatmentHistory.aspx.cs" Inherits="HMS.treatmentHistory" %>

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
                    <h2>Patient History
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
                    <div class="col-md-12" id="divPatientDetails" runat="server" visible="false">
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
                    </div>

                    <!-- Nav tabs -->
                    <ul class="nav nav-tabs" role="tablist">
                        <li role="presentation" class="active">
                            <a href="#home_with_icon_title" data-toggle="tab">
                                <i class="material-icons">insert_drive_file</i>Patient History
                                    </a>
                        </li>
                        <li role="presentation">
                            <a href="treatment.aspx">
                                <i class="material-icons">add_to_photos</i> Treatment        
                            </a>
                        </li>
                    </ul>

                    <!-- Tab panes -->
                    <div class="tab-content">
                        <div role="tabpanel" class="tab-pane fade in active" id="profile_with_icon_title">
                            <asp:Repeater ID="rptHistory" runat="server" OnItemDataBound="rptHistory_ItemDataBound">
                                <ItemTemplate>
                                    <div class="panel-group" id="accordion_1" role="tablist" aria-multiselectable="true">
                                        <div class="panel panel-primary">
                                            <div class="panel-heading" role="tab" id="headingOne_1">
                                                <h4 class="panel-title">
                                                    <a role="button" data-toggle="collapse" data-parent="#accordion_1" href='#<%# Eval("tokenIdx")%>' aria-expanded="true" aria-controls='<%# Eval("tokenIdx")%>'>Date:
                                                    </a>
                                                </h4>
                                            </div>
                                            <div id='<%# Eval("tokenIdx")%>' class="panel-collapse collapse in" role="tabpanel" aria-labelledby="headingOne_1">
                                                <div class="panel-body">
                                                    <div class="col-md-6">
                                                        <span><b>Token Number:</b>&nbsp</span>
                                                        <asp:Label ID="lblTokenNumber" runat="server" Text='<%# Eval("tokenNumber")%>'></asp:Label>
                                                    </div>
                                                    <div class="col-md-6">
                                                        <span><b>Doctor:</b>&nbsp</span>
                                                        <asp:Label ID="lblDoctor" runat="server" Text='<%# Eval("doctorName")%>'></asp:Label>
                                                    </div>
                                                    <div class="col-md-6">
                                                        <span><b>Appointment Date:</b>&nbsp</span>
                                                        <asp:Label ID="lblAppointmentDate" runat="server" Text='<%# Eval("appointmentDate")%>'></asp:Label>
                                                    </div>
                                                    <div class="col-md-12">
                                                       <ol class="breadcrumb breadcrumb-bg-grey">
                                                            <li class="active"><i class="material-icons">library_books</i> Internal Medicine</li>
                                                        </ol>
                                                        <asp:Label ID="lblTreatmentIdx" runat="server" Text='<%# Eval("treatmentIdx")%>' Visible="false"></asp:Label>
                                                    </div>
                                                    <div class="col-md-12">
                                                        <div class="table-responsive">
                                                            <table class="table table-bordered table-striped table-hover dataTable js-exportable">
                                                                <thead>
                                                                    <tr>
                                                                        <th>#
                                                                        </th>
                                                                        <th>Drug Name
                                                                        </th>
                                                                        <th>Qty
                                                                        </th>
                                                                        <th>Usage
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
                                                                            </tr>
                                                                        </ItemTemplate>
                                                                    </asp:Repeater>
                                                                </tbody>
                                                            </table>
                                                        </div>
                                                    </div>
                                                    <br />
                                                    <div class="col-md-12">
                                                        <ol class="breadcrumb breadcrumb-bg-grey">
                                                            <li class="active"><i class="material-icons">library_books</i> Pharmacy Prescription</li>
                                                        </ol>
                                                    </div>
                                                    <div class="col-md-12">
                                                        <div class="table-responsive">
                                                            <table class="table table-bordered table-striped table-hover dataTable js-exportable">
                                                                <thead>
                                                                    <tr>
                                                                        <th>#
                                                                        </th>
                                                                        <th>Drug Name
                                                                        </th>
                                                                        <th>Qty
                                                                        </th>
                                                                        <th>Usage
                                                                        </th>
                                                                    </tr>
                                                                </thead>
                                                                <tbody>
                                                                    <asp:Repeater ID="rptPrescriptionLog" runat="server">
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
                                                                            </tr>
                                                                        </ItemTemplate>
                                                                    </asp:Repeater>
                                                                </tbody>
                                                            </table>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </ItemTemplate>
                            </asp:Repeater>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
