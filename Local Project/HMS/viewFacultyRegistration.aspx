<%@ Page Title="" Language="C#" MasterPageFile="~/HMS.Master" AutoEventWireup="true" CodeFile="viewFacultyRegistration.aspx.cs" Inherits="HMS.viewFacultyRegistration" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
       <style>
        .buttons-pdf{
            display:none;
        }
    @media print {
       table td:last-child {display:none}
       table th:last-child {display:none}
   }
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
                    <h2>Employee List
                            </h2>
                </div>
                <div class="body">
                    <div class="table-responsive">
                        <asp:UpdatePanel ID="up1" runat="server">
                            <ContentTemplate>
                                <asp:Button ID="btnPrint" runat="server" Text="Print" CssClass="btn btn-primary" OnClick="btnPrint_Click" />
                                <table class="table table-bordered table-striped table-hover dataTable js-exportable">
                                    <thead>
                                        <tr>
                                            <th>#
                                            </th>
                                            <th>Emp ID
                                            </th>
                                            <th>Full Name
                                            </th>
                                            <th>Department
                                            </th>
                                            <th>Designation
                                            </th>
                                            <th>User Type
                                            </th>
                                            <th>Status
                                            </th>
                                            <th>Action
                                            </th>
                                        </tr>
                                    </thead>
                                    <tbody>
                                        <asp:Repeater ID="rptUsers" runat="server">
                                            <ItemTemplate>
                                                <tr>
                                                    <td>
                                                        <center>
                                                    <%# Eval("sn")%>
                                                </center>
                                                    </td>
                                                    <td>
                                                        <asp:Label ID="lblUserIdx" runat="server" Text='<%# Eval("idx")%>'></asp:Label>
                                                    </td>
                                                    <td>
                                                        <%# Eval("fullName")%>
                                                    </td>
                                                    <td>
                                                        <%# Eval("departmentName")%>
                                                    </td>
                                                    <td>
                                                        <%# Eval("designationName")%>
                                                    </td>
                                                    <td>
                                                        <%# Eval("userTypeName")%> &nbsp / &nbsp <%# Eval("specialty")%>
                                                    </td>
                                                    <td>
                                                        <%# Eval("status")%>
                                                    </td>
                                                    <td>
                                                        <asp:LinkButton ID="lnkView" runat="server" Text="View" CommandArgument='<%# Eval("idx") %>'
                                                            ToolTip="View" class="btn" OnClick="lnkView_Click"><i class="glyphicon glyphicon-eye-open"></i></asp:LinkButton>
                                                        <asp:LinkButton ID="lnkDelete" runat="server" Text="Delete" CommandArgument='<%# Eval("idx") %>'
                                                            ToolTip="Delete" class="btn" OnClientClick="return confirm('Are you sure you wish to Delete this record?');"
                                                            OnClick="lnkDelete_Click"><i class="glyphicon glyphicon-trash"></i></asp:LinkButton>
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
