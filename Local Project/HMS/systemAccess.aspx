<%@ Page Title="" Language="C#" MasterPageFile="~/HMS.Master" AutoEventWireup="true" CodeFile="systemAccess.aspx.cs" Inherits="HMS.systemAccess" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <style>
        /*.chkPageUrl tbody tr td label {
            padding-left: 10px;
 
        }*/
        

        [type="checkbox"]:not(:checked), [type="checkbox"]:checked {
            position: initial !important;
            left: -9999px !important;
            opacity: 1 !important;
        }

        #ContentPlaceHolder1_chkAll {
        visibility:hidden!important;
        }

    </style>

    <script src="http://code.jquery.com/jquery-1.10.2.js"></script>
    <script language="javascript" type="text/javascript">
        function Selectallcheckbox(val) {
            if (!$(this).is(':checked')) {
                $('input:checkbox').prop('checked', val.checked);
            } else {
                $("#chkroot").removeAttr('checked');
            }
        }
    </script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="row clearfix">
        <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12">
            <div class="card">
                <div class="header">
                    <h2>System Access
                            </h2>
                </div>
                <div class="body">
                    <div class="table-responsive">
                        <asp:UpdatePanel ID="up1" runat="server">
                            <ContentTemplate>
                                <div class="col-md-12">
                                    <div class="form-group form-float">
                                        <div class="form-line">
                                            <asp:DropDownList ID="ddlEmployees" runat="server" CssClass="form-control show-tick" OnSelectedIndexChanged="ddlEmployees_SelectedIndexChanged1" AutoPostBack="true" >
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                </div>
                                <table class="table table-bordered  ">
                                    <thead>
                                        <tr>
                                            <th>#
                                            </th>
                                            <th>
                                                <asp:CheckBox ID="chkAll" Text="Select All" runat="server" onclick="javascript:Selectallcheckbox(this);" /></th>
                                            <th>Navigation Name
                                            </th>
                                             <th>Page Name
                                            </th>
                                        </tr>
                                    </thead>
                                    <tbody>
                                        <asp:Repeater ID="rptUserRole" runat="server">
                                            <ItemTemplate>
                                                <tr>
                                                    <td>
                                                        <asp:Label ID="Label1" Text='<%# Container.ItemIndex + 1 %>' runat="server" />
                                                        <asp:Label ID="lbpgurl" Text='<%# Eval("pageUrl")%>' runat="server" Visible="false" />
                                                    </td>
                                                    <td>
                                                        <asp:CheckBox ID="chkPageUrl1" runat="server" />
                                                    </td>
                                                    <td>
                                                           <%# Eval("navigationName")%>
                                                    </td>
                                                    <td>
                                                        <%# Eval("pageName")%>
                                                        <asp:Label ID="lblIdx" Text='<%# Eval("idx")%>' runat="server" Visible="false" />
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
            <div class="col-md-12">
                <div class="form-group form-float">
                    <asp:Button ID="btnSubmit" runat="server" Text="Submit" CssClass="btn btn-primary waves-effect btnSize" OnClick="btnSubmit_Click" />
                    <asp:Button ID="btnCancel" runat="server" Text="Clear" CssClass="btn btn-danger waves-effect btnSize" OnClick="btnCancel_Click" />
                </div>
            </div>
        </div>
    </div>
</asp:Content>

