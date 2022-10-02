<%@ Page Title="" Language="C#" MasterPageFile="~/HMS.Master" AutoEventWireup="true" CodeFile="404.aspx.cs" Inherits="HMS._404" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="row clearfix">
        <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12">
            <div class="header">
                <h2>Error
                            </h2>
            </div>
            <div class="body">
                <center>
                    <span> <b> Oops 404 Page Not Found! </b></span>
                    <br />
                    <span>Sorry! You have no access on &nbsp
                    <asp:Label ID="lblPage" runat="server"></asp:Label>
                </span>
                </center>

            </div>
        </div>
    </div>
</asp:Content>
