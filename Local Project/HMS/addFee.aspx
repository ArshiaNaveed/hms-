<%@ Page Title="" Language="C#" MasterPageFile="~/HMS.Master" AutoEventWireup="true" CodeFile="addFee.aspx.cs" Inherits="HMS.addFee" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="row clearfix">
        <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12">
            <div class="card">
                <div class="header">
                    <h2>Add Fee</h2>
                </div>
                <div class="body">
                    <div class="row clearfix">
                        <div class="col-md-12">
                            <div class="col-md-6">
                                <div class="form-group form-float">
                                    <div class="form-line">
                                        <asp:DropDownList ID="ddlDoctor" runat="server" CssClass="form-control show-tick" data-live-search="true">
                                        </asp:DropDownList>
                                    </div>
                                </div>
                            </div>
                            <div class="col-md-6">
                                <div class="form-group form-float">
                                    <div class="form-line">
                                        <asp:TextBox ID="txtFee" runat="server" CssClass="form-control" required></asp:TextBox>
                                        <label class="form-label">Fee</label>
                                    </div>
                                </div>
                            </div>
                            <div class="col-md-12">
                                <div class="form-group form-float">
                                    <asp:Label ID="lblError" runat="server" style="color:red;" Visible="false" ></asp:Label>
                                </div>
                            </div>

                            <div class="col-md-12">
                                <div class="form-group">
                                    <asp:Button ID="btnSubmit" runat="server" Text="Submit" OnClick="btnSubmit_Click" CssClass="btn btn-primary waves-effect" />
                                    <asp:Button ID="btnEdit" runat="server" Text="Edit" CssClass="btn btn-primary waves-effect" Enabled="false" OnClick="btnEdit_Click" />
                                    <asp:Button ID="btnUpdate" runat="server" Text="Update" CssClass="btn btn-success waves-effect" Enabled="false" OnClick="btnUpdate_Click" />
                                    <asp:Button ID="btnCancel" runat="server" Text="Clear" OnClick="btnCancel_Click" CssClass="btn btn-danger waves-effect" />
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
