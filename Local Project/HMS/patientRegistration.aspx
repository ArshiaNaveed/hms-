<%@ Page Title="" Language="C#" MasterPageFile="~/HMS.Master" AutoEventWireup="true" CodeFile="patientRegistration.aspx.cs" Inherits="HMS.patientRegistration" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div class="header">
        <h2>New Patients Registration / Appointment</h2>
    </div>
    <div class="body">
        <!-- Nav tabs -->
        <ul class="nav nav-tabs" role="tablist">
            <li role="presentation" class="active">
                <a href="#home_with_icon_title" data-toggle="tab">
                    <i class="material-icons">person_add</i> New Patients Registration / Appointment
                                    </a>
            </li>
            <li role="presentation">
                <a href="token.aspx">
                    <i class="material-icons">traffic</i> Appointment (Registered Patient)</a>
            </li>
        </ul>

        <!-- Tab panes -->
        <div class="tab-content">
            <div role="tabpanel" class="tab-pane fade in active" id="home_with_icon_title">
                <div class="row clearfix">
                    <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12">
                        <div class="card">
                            <div class="body">
                                <div class="row clearfix">
                                    <div class="col-md-12">
                                        <div class="form-group form-float">
                                            <div class="form-line">
                                                <asp:TextBox ID="txtCardNumber" runat="server" CssClass="form-control" Enabled="false"></asp:TextBox>
                                                <label class="form-label">Card Number</label>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-md-6">
                                        <div class="form-group form-float">
                                            <div class="form-line">
                                                <asp:TextBox ID="txtPatientName" runat="server" CssClass="form-control" required></asp:TextBox>
                                                <label class="form-label">Patient Name</label>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-md-3">
                                        <div class="form-group form-float">
                                            <div class="form-line">
                                                <asp:TextBox ID="txtAge" runat="server" CssClass="form-control" required></asp:TextBox>
                                                <label class="form-label">Age</label>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-md-3">
                                        <div class="form-group form-float">
                                            <div class="form-line">
                                                <asp:DropDownList ID="ddlGender" runat="server" CssClass="form-control show-tick" data-live-search="true" trqurid>
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-md-6">
                                        <div class="form-group form-float">
                                            <div class="form-line">
                                                <asp:TextBox ID="txtContactNumber1" runat="server" CssClass="form-control"></asp:TextBox>
                                                <label class="form-label">Contact Number 1</label>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-md-6">
                                        <div class="form-group form-float">
                                            <div class="form-line">
                                                <asp:TextBox ID="txtContactNumber2" runat="server" CssClass="form-control"></asp:TextBox>
                                                <label class="form-label">Contact Number 2</label>
                                            </div>
                                        </div>
                                    </div>
                                     <div class="col-md-6">
                                        <div class="form-group form-float">
                                            <div class="form-line">
                                                <asp:TextBox ID="txtReferenceName" runat="server" CssClass="form-control"></asp:TextBox>
                                                <label class="form-label">Reference Name</label>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-md-6">
                                        <div class="form-group form-float">
                                            <div class="form-line">
                                                <asp:TextBox ID="txtReferenceCNIC" runat="server" CssClass="form-control"></asp:TextBox>
                                                <label class="form-label">Reference CNIC</label>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-md-12">
                                        <div class="form-group form-float">
                                            <hr />
                                        </div>
                                    </div>

                                    <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12" id="tokenDiv" runat="server">
                                        <div class="card">
                                            <div class="body">
                                                <div class="row clearfix">
                                                    <div class="col-md-6">
                                                        <div class="form-group form-float">
                                                            <div class="form-line">
                                                                <asp:DropDownList ID="ddlPhysician" runat="server" CssClass="form-control show-tick" OnSelectedIndexChanged="ddlPhysician_SelectedIndexChanged" AutoPostBack="true" data-live-search="true" required>
                                                                </asp:DropDownList>
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <div class="col-md-6">
                                                        <div class="form-group form-float">
                                                            <div class="form-line">
                                                                <asp:TextBox ID="txtTokenNumber" runat="server" CssClass="form-control" Enabled="false" required></asp:TextBox>
                                                                <label class="form-label">Token Number</label>
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <div class="col-md-6">
                                                        <div class="form-group form-float">
                                                            <div class="form-line">
                                                                <asp:DropDownList ID="ddlDisess" runat="server" CssClass="form-control show-tick" required>
                                                                </asp:DropDownList>
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <div class="col-md-3">
                                                        <div class="form-group form-float">
                                                            <div class="form-line">
                                                                <asp:TextBox ID="txtApointmentDate" runat="server" CssClass="form-control" required Enabled="false"></asp:TextBox>
                                                                <label class="form-label">Appointment Date</label>
                                                                <asp:CalendarExtender ID="CalendarExtender1" runat="server" Format="dd/MM/yyyy" TargetControlID="txtApointmentDate">
                                                                </asp:CalendarExtender>
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <div class="col-md-3">
                                                        <div class="form-group form-float">
                                                            <div class="form-line">
                                                                <asp:TextBox ID="txtFee" runat="server" CssClass="form-control" required></asp:TextBox>
                                                                <label class="form-label">Fee</label>
                                                            </div>
                                                        </div>
                                                    </div>

                                                </div>
                                            </div>
                                        </div>
                                    </div>

                                    <div class="col-md-12">
                                        <div class="form-group">
                                            <asp:Button ID="btnSubmitNewPatient" runat="server" Text="Submit" CssClass="btn btn-primary waves-effect btnSize" OnClick="btnSubmitNewPatient_Click" />
                                            <asp:Button ID="btnEdit" runat="server" Text="Edit" CssClass="btn btn-primary waves-effect btnSize" OnClick="btnEdit_Click" Enabled="false" />
                                            <asp:Button ID="btnUpdate" runat="server" Text="Update" CssClass="btn btn-success waves-effect btnSize" OnClick="btnUpdate_Click" Enabled="false" />
                                            <asp:Button ID="btnCancelNewPatient" runat="server" Text="Clear" CssClass="btn btn-danger waves-effect btnSize" OnClick="btnCancelNewPatient_Click" />
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>

</asp:Content>
