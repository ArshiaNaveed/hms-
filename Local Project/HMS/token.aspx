<%@ Page Title="" Language="C#" MasterPageFile="~/HMS.Master" AutoEventWireup="true" CodeFile="token.aspx.cs" Inherits="HMS.token" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="header">
        <h2>Appointment (Registered Patient)</h2>
    </div>
    <div class="body">
        <!-- Nav tabs -->
        <ul class="nav nav-tabs" role="tablist">
            <li role="presentation" class="active">
                <a href="#profile_with_icon_title" data-toggle="tab">
                    <i class="material-icons">traffic</i> Appointment (Registered Patient)</a>
            </li>
            <li role="presentation">
                <a href="patientRegistration.aspx">
                    <i class="material-icons">person_add</i> New Patients Registration / Appointment
                                    </a>
            </li>
        </ul>
        <!-- Tab panes -->
        <div class="tab-content">
            <div role="tabpanel" class="tab-pane fade in active" id="profile_with_icon_title">
                <div class="row clearfix">
                    <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12">
                        <div class="card">
                            <div class="body">
                                <div class="row clearfix">
                                    <div class="col-md-10">
                                        <div class="form-group form-float">
                                            <div class="form-line">
                                                <asp:TextBox ID="txtRegCardNumber" runat="server"  CssClass="form-control" required></asp:TextBox>
                                                <label class="form-label">Card Number</label>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-md-2">
                                        <div class="form-group form-float">
                                            <asp:Button ID="btnSearch" runat="server" Text="Search" CssClass="btn btn-primary waves-effect" OnClick="btnSearch_Click" UseSubmitBehavior="false" />
                                        </div>
                                    </div>
                                    <div class="col-md-6">
                                        <div class="form-group form-float">
                                            <div class="form-line">
                                                <asp:TextBox ID="txtRegPatientName" runat="server" CssClass="form-control" Enabled="false"></asp:TextBox>
                                                <label class="form-label">Patient Name</label>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-md-6">
                                        <div class="form-group form-float">
                                            <div class="form-line">
                                                <asp:TextBox ID="txtRegAge" runat="server" CssClass="form-control" Enabled="false"></asp:TextBox>
                                                <label class="form-label">Age</label>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-md-6">
                                        <div class="form-group form-float">
                                            <div class="form-line">
                                                <asp:TextBox ID="txtRegContactNumber1" runat="server" CssClass="form-control" Enabled="false"></asp:TextBox>
                                                <label class="form-label">Contact Number 1</label>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-md-6">
                                        <div class="form-group form-float">
                                            <div class="form-line">
                                                <asp:TextBox ID="txtRegContactNumber2" runat="server" CssClass="form-control" Enabled="false"></asp:TextBox>
                                                <label class="form-label">Contact Number 2</label>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-md-12">
                                        <div class="form-group">
                                            <hr />
                                        </div>
                                    </div>
                                    <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12">
                                        <div class="card">
                                            <div class="body">
                                                <div class="row clearfix">
                                                    <div class="col-md-6">
                                                        <div class="form-group form-float">
                                                            <div class="form-line">
                                                                <asp:DropDownList ID="ddlRegPhysision" runat="server" CssClass="form-control show-tick" OnSelectedIndexChanged="ddlRegPhysision_SelectedIndexChanged" AutoPostBack="true">
                                                                </asp:DropDownList>
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <div class="col-md-6">
                                                        <div class="form-group form-float">
                                                            <div class="form-line">
                                                                <asp:TextBox ID="txtRegToken" runat="server" CssClass="form-control" Enabled="false"></asp:TextBox>
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
                                                                <asp:TextBox ID="txtRegAppointmentDate" runat="server" CssClass="form-control" required Enabled="false" ></asp:TextBox>
                                                                <label class="form-label">Appointment Date</label>
                                                                <asp:CalendarExtender ID="CalendarExtender1" runat="server" Format="dd/MM/yyyy" TargetControlID="txtRegAppointmentDate">
                                                                </asp:CalendarExtender>
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <div class="col-md-3">
                                                        <div class="form-group form-float">
                                                            <div class="form-line">
                                                                <asp:TextBox ID="txtRegFee" runat="server" CssClass="form-control" required></asp:TextBox>
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
                                            <asp:Button ID="btnSubmitRegPatient" runat="server" Text="Submit" CssClass="btn btn-primary waves-effect btnSize" OnClick="btnSubmitRegPatient_Click" />
                                            <asp:Button ID="btnCancelRegPatient" runat="server" Text="Clear" CssClass="btn btn-danger waves-effect btnSize" OnClick="btnCancelRegPatient_Click" />
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
