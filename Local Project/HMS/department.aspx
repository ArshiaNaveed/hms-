<%@ Page Title="" Language="C#" MasterPageFile="~/HMS.Master" AutoEventWireup="true" CodeFile="department.aspx.cs" Inherits="HMS.department" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="row clearfix">
        <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12">
            <div class="card">
                <div class="header">
                    <h2>Manage Department
                            </h2>
                </div>
                <div class="body">
                    <div class="form-group form-float">
                        <div class="form-line">
                            <asp:TextBox ID="txtDepartment" runat="server" CssClass="form-control" required></asp:TextBox>
                            <label class="form-label">Department</label>
                        </div>
                    </div>
                    <div class="form-group form-float">
                        <asp:Label ID="lblError" runat="server" Style="color: red;" Visible="false"></asp:Label>
                    </div>
                    <asp:Button ID="btnSubmit" runat="server" Text="Submit" OnClick="btnSubmit_Click" CssClass="btn btn-primary waves-effect" />
                    <asp:Button ID="btnUpdate" runat="server" Text="Update" OnClick="btnUpdate_Click" CssClass="btn btn-primary waves-effect" Enabled="false" />
                    <asp:Button ID="btnCancel" runat="server" Text="Clear" OnClick="btnCancel_Click" CssClass="btn btn-danger waves-effect" />

                </div>
                 <div class="body">
                    <div class="table-responsive">
                        <asp:UpdatePanel ID="up1" runat="server">
                            <ContentTemplate>
                                <table id="example" style="width:100%!important" class="table table-bordered table-striped table-hover dataTable js-exportable">
                                    <thead>
                                        <tr>
                                            <th>#
                                            </th>
                                            <th>Department
                                            </th>
                                            <th>Action
                                            </th>
                                        </tr>
                                    </thead>
                                    <tbody>
                                        <asp:Repeater ID="rptDepartment" runat="server">
                                            <ItemTemplate>
                                                <tr>
                                                    <td>
                                                        <asp:Label ID="lblPatientIdx" runat="server" Text='<%# Eval("idx")%>' Visible="false"></asp:Label>
                                                        <center>
                                                    <%# Eval("sn")%>
                                                </center>
                                                    </td>
                                                    <td>
                                                        <%# Eval("departmentName")%>
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
