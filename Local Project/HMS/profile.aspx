<%@ Page Title="" Language="C#" MasterPageFile="~/HMS.Master" AutoEventWireup="true" CodeFile="profile.aspx.cs" Inherits="HMS.profile" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="header">
        <h2>My Profile</h2>
    </div>
    <div class="body">
        <div class="row clearfix">
            <div class="col-md-6">
                <div class="form-group form-float">
                    <div class="row clearfix">
                        <div class="col-md-6">
                            <div class="form-group form-float">
                                <div class="form-line">
                                    <asp:TextBox ID="txtFirstName" runat="server" CssClass="form-control" required></asp:TextBox>
                                    <label class="form-label">First Name</label>
                                </div>
                            </div>
                        </div>
                        <div class="col-md-6">
                            <div class="form-group form-float">
                                <div class="form-line">
                                    <asp:TextBox ID="txtLastName" runat="server" CssClass="form-control" required></asp:TextBox>
                                    <label class="form-label">Last Name</label>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="row clearfix">
                        <div class="col-md-6">
                            <div class="form-group form-float">
                                <div class="form-line">
                                    <asp:DropDownList ID="ddlGender" runat="server" CssClass="form-control show-tick" data-live-search="true">
                                    </asp:DropDownList>
                                </div>
                            </div>
                        </div>
                        <div class="col-md-6">
                            <div class="form-group form-float">
                                <div class="form-line">
                                    <asp:DropDownList ID="ddlMaritalStatus" runat="server" CssClass="form-control show-tick" data-live-search="true">
                                    </asp:DropDownList>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <div class="col-md-6">
                <div class="form-group form-float">
                    <asp:Image ID="imgFaculty" runat="server" Style="width: 20%; border: 1px solid #ccc;" />
                </div>
            </div>
        </div>
        <div class="row clearfix">
            <div class="col-md-6">
                <div class="form-group form-float">
                    <div class="form-line">
                        <asp:TextBox ID="txtDob" runat="server" CssClass="form-control"></asp:TextBox>
                        <label class="form-label">Date Of Birth</label>
                    </div>
                </div>
            </div>
            <div class="col-md-6">
                <div class="form-group form-float">
                    <div class="form-line">
                        <asp:TextBox ID="txtCnic" runat="server" CssClass="form-control"></asp:TextBox>
                        <label class="form-label">CNIC #</label>
                    </div>
                </div>
            </div>
        </div>
        <div class="row clearfix">
            <div class="col-md-6">
                <div class="form-group form-float">
                    <div class="form-line">
                        <asp:TextBox ID="txtContactNumber" runat="server" CssClass="form-control"></asp:TextBox>
                        <label class="form-label">Contact #</label>
                    </div>
                </div>
            </div>
            <div class="col-md-6">
                <div class="form-group form-float">
                    <div class="form-line">
                        <asp:TextBox ID="txtEmail" runat="server" CssClass="form-control email"></asp:TextBox>
                        <label class="form-label">Email ID</label>
                    </div>
                </div>
            </div>
        </div>
        <div class="row clearfix">
            <div class="col-md-12">
                <div class="form-group form-float">
                    <div class="form-line">
                        <asp:TextBox ID="txtAddress" runat="server" CssClass="form-control" Height="30px" TextMode="MultiLine"></asp:TextBox>
                        <label class="form-label">Residential Address</label>
                    </div>
                </div>
            </div>
        </div>
        <div class="row clearfix">
            <div class="col-md-6">
                <div class="form-group form-float">
                    <div class="form-line">
                        <asp:DropDownList ID="ddlDepartment" runat="server" CssClass="form-control show-tick" data-live-search="true">
                        </asp:DropDownList>
                    </div>
                </div>
            </div>
            <div class="col-md-6">
                <div class="form-group form-float">
                    <div class="form-line">
                        <asp:DropDownList ID="ddlDesignation" runat="server" CssClass="form-control show-tick" data-live-search="true">
                        </asp:DropDownList>
                    </div>
                </div>
            </div>
        </div>
        <div class="row clearfix">
            <div class="col-md-6">
                <div class="form-group form-float">
                    <div class="form-line">
                        <asp:TextBox ID="txtHiringDate" runat="server" CssClass="form-control"></asp:TextBox>
                        <label class="form-label">Hiring Date</label>
                    </div>
                </div>
            </div>
            <div class="col-md-6">
                <div class="form-group form-float">
                    <div class="form-line">
                        <asp:TextBox ID="txtSalary" runat="server" CssClass="form-control"></asp:TextBox>
                        <label class="form-label">Salary</label>
                    </div>
                </div>
            </div>
        </div>
        <div class="row clearfix">
            <div class="col-md-6">
                <div class="form-group form-float">
                    <div class="form-line">
                        <asp:DropDownList ID="ddlUserType" runat="server" CssClass="form-control show-tick" required data-live-search="true">
                        </asp:DropDownList>
                    </div>
                </div>
            </div>
            <div class="col-md-6">
                <div class="form-group form-float">
                    <div class="form-line">
                        <asp:DropDownList ID="ddlSpeciality" runat="server" CssClass="form-control show-tick" required data-live-search="true">
                        </asp:DropDownList>
                    </div>
                </div>
            </div>
        </div>
        <div class="row clearfix" id="divSchedule" runat="server" visible="false">
            <div class="col-md-12">
                <div class="form-group">
                    <div class="body">
                        <div class="table-responsive">
                            <asp:UpdatePanel ID="up1" runat="server">
                                <ContentTemplate>
                                    <asp:GridView ID="grvSchedule" runat="server" AutoGenerateColumns="false" CssClass="table table-bordered">
                                        <Columns>
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:CheckBox ID="chkRow" runat="server" CssClass="chk" />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Day">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblMonday" runat="server" Text='<%# Bind("day") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="From Time">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="txtFromTime" runat="server" CssClass="timepicker form-control" Text='<%# Bind("fromTime") %>' placeholder="From TIme"></asp:TextBox>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="To Time">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="txtToTime" runat="server" CssClass="timepicker form-control" Text='<%# Bind("toTime") %>' placeholder="From TIme"></asp:TextBox>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                    </asp:GridView>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <div class="row clearfix">
            <div class="col-md-6">
                <div class="form-group form-float">
                    <div class="form-line">
                        <asp:TextBox ID="txtLoginID" runat="server" CssClass="form-control" required></asp:TextBox>
                        <label class="form-label">Login ID</label>
                    </div>
                </div>
            </div>
          
        </div>
    </div>
</asp:Content>
