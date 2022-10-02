<%@ Page Title="" Language="C#" MasterPageFile="~/HMS.Master" AutoEventWireup="true" CodeFile="changePassword.aspx.cs" Inherits="HMS.changePassword" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="row clearfix">
        <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12">
            <div class="card">
                <div class="header">
                    <h2>Change Password
                            </h2>
                </div>
                <div class="body">
                    <div class="form-group form-float">
                        <div class="form-line">
                            <asp:TextBox ID="txtOldPassword" runat="server" CssClass="form-control" TextMode="Password" required></asp:TextBox>
                            <label class="form-label">Old Password</label>
                        </div>
                    </div>
                    <div class="form-group form-float">
                        <div class="form-line">
                            <asp:TextBox ID="txtNewPassword" runat="server" CssClass="form-control" TextMode="Password" required></asp:TextBox>
                            <label class="form-label">New Password</label>
                        </div>
                    </div>
                    <div class="form-group form-float">
                        <div class="form-line">
                            <asp:TextBox ID="txtConfirmNewPassword" runat="server" CssClass="form-control" TextMode="Password" required></asp:TextBox>
                            <label class="form-label">Confirm New Password</label>
                        </div>
                    </div>
                    <div class="form-group form-float">
                        <asp:Label ID="lblError" runat="server" Style="color: red;" Visible="false"></asp:Label>
                    </div>
                    <asp:Button ID="btnSubmit" runat="server" Text="Submit" OnClick="btnSubmit_Click" CssClass="btn btn-primary waves-effect" />
                    <asp:Button ID="btnCancel" runat="server" Text="Clear" OnClick="btnCancel_Click" UseSubmitBehavior="false" CssClass="btn btn-danger waves-effect" />

                </div>
            </div>
        </div>
    </div>
</asp:Content>
