<%@ Page Title="" Language="C#" MasterPageFile="~/HMS.Master" AutoEventWireup="true" CodeFile="facultyRegistration.aspx.cs" Inherits="HMS.facultyRegistration" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        .chk input {
            position: absolute!important;
            left: 0!important;
            opacity: 1!important;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div class="header">
        <h2>Faculty Registration</h2>
    </div>
    <div class="body">
        <div class="row clearfix">
            <div class="col-md-6">
                <div class="form-group form-float">
                    <div class="row clearfix">
                        <div class="col-md-6">
                            <div class="form-group form-float">
                                <div class="form-line">
                                    <asp:TextBox ID="txtFirstName" runat="server" CssClass="form-control" Required></asp:TextBox>
                                    <label class="form-label">First Name</label>
                                </div>
                            </div>
                        </div>
                        <div class="col-md-6">
                            <div class="form-group form-float">
                                <div class="form-line">
                                    <asp:TextBox ID="txtLastName" runat="server" CssClass="form-control" Required></asp:TextBox>
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
                    <asp:FileUpload ID="fuFaculty" runat="server" />
                    <asp:Button ID="btnImageUpload" runat="server" Text="Upload" CssClass="btn btn-primary waves-effect" UseSubmitBehavior="false" OnClick="btnImageUpload_Click" />
                    <asp:Label ID="lblImageError" runat="server" Style="color: red;" Visible="false"></asp:Label>
                </div>
            </div>
        </div>


        <div class="row clearfix">
            <div class="col-md-6">
                <div class="form-group form-float">
                    <div class="form-line">
                        <asp:TextBox ID="txtDob" runat="server" CssClass="form-control"></asp:TextBox>
                        <label class="form-label">Date Of Birth</label>
                        <asp:CalendarExtender ID="CalendarExtender1" runat="server" Format="dd/MM/yyyy" TargetControlID="txtDob">
                        </asp:CalendarExtender>
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

                        <asp:DropDownList ID="ddlDepartment" runat="server" CssClass="form-control show-tick" OnSelectedIndexChanged="ddlDepartment_SelectedIndexChanged" AutoPostBack="true" data-live-search="true">
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
                        <asp:CalendarExtender ID="CalendarExtender2" runat="server" Format="dd/MM/yyyy" TargetControlID="txtHiringDate">
                        </asp:CalendarExtender>
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
                        <asp:DropDownList ID="ddlUserType" runat="server" CssClass="form-control show-tick" Required OnSelectedIndexChanged="ddlUserType_OnSelectedIndexChanged" AutoPostBack="true" data-live-search="true">
                        </asp:DropDownList>
                    </div>
                </div>
            </div>
            <div class="col-md-6">
                <div class="form-group form-float">
                    <div class="form-line">
                        <asp:DropDownList ID="ddlSpeciality" runat="server" CssClass="form-control show-tick"  Required data-live-search="true">
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
                                                    <asp:TextBox ID="txtFromTime" runat="server" CssClass="timepicker form-control" Text='<%# Bind("fromTime") %>'  placeholder="From TIme"></asp:TextBox>
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
                        <asp:UpdatePanel ID="up2" runat="server">
                            <ContentTemplate>
                                <asp:TextBox ID="txtLoginID" runat="server" CssClass="form-control" onkeyup="usernameChecker(this.value)" Required></asp:TextBox>
                                <label class="form-label">Login ID</label>
                                </div>
                                <span runat="server" clientidmode="Static" id="spanAvailability1"></span>
                                <script type="text/javascript">
                                    var usernameCheckerTimer;
                                    var spanAvailability = document.getElementById("spanAvailability1");
                                    function usernameChecker(username) {

                                        clearTimeout(usernameCheckerTimer);
                                        if (username.length == 0)
                                            spanAvailability1.innerHTML = "";
                                        else {
                                            spanAvailability1.innerHTML = "<span style='color: #1B1464;'>checking...</span>";
                                            usernameCheckerTimer = setTimeout("checkUsernameUsage('" + username + "');", 750);
                                        }
                                    }

                                    function checkUsernameUsage(username) {
                                        // initiate the Ajax page method call
                                        // upon completion, the OnSucceded callback will be executed

                                        PageMethods.IsUserAvailable(username, OnSucceeded);
                                    }

                                    // Callback function invoked on successful completion of the page method.
                                    function OnSucceeded(result, userContext, methodName) {
                                        if (methodName == "IsUserAvailable") {
                                            if (result == true) {
                                                spanAvailability1.innerHTML = "<span style='color: DarkGreen;'>Available</span>";
                                            }
                                            else { spanAvailability1.innerHTML = "<span style='color: Red;'>Unavailable</span>"; }
                                        }
                                    }
                                </script>
                            </ContentTemplate>
                        </asp:UpdatePanel>

                    </div>
                </div>
                <div class="col-md-6">
                    <div class="form-group form-float">
                        <div class="form-line">
                            <asp:TextBox ID="txtPassword" runat="server" CssClass="form-control" TextMode="Password" Required></asp:TextBox>
                            <label class="form-label">Password</label>
                        </div>
                    </div>
                </div>
            </div>
            <asp:Button ID="btnSubmit" runat="server" Text="Submit" CssClass="btn btn-primary waves-effect btnSize" OnClick="btnSubmit_Click" />
            <asp:Button ID="btnEdit" runat="server" Text="Edit" CssClass="btn btn-primary waves-effect btnSize" OnClick="btnEdit_Click" Enabled="false" />
            <asp:Button ID="btnUpdate" runat="server" Text="Update" CssClass="btn btn-success waves-effect btnSize" OnClick="btnUpdate_Click" Enabled="false" />
            <asp:Button ID="btnActivate" runat="server" Text="Activate" CssClass="btn btn-default waves-effect btnSize" OnClick="btnActivate_Click" Visible="false" />
            <asp:Button ID="btnDeActivate" runat="server" Text="DeActivate" CssClass="btn btn-warning waves-effect btnSize" OnClick="btnDeActivate_Click" Visible="false" />
            <asp:Button ID="btnCancel" runat="server" Text="Clear" CssClass="btn btn-danger waves-effect btnSize" UseSubmitBehavior="false" OnClick="btnCancel_Click" />
        </div>


        <%--<div class="row clearfix" id="divSchedule" runat="server" visible="false">
                <div class="col-md-3">
                    <div class="form-group form-float">
                        <div class="form-line">
                            <asp:DropDownList ID="ddlScheduleDay" runat="server" CssClass="form-control show-tick" data-live-search="true">
                                <asp:ListItem Value="0" Text="Select Day"></asp:ListItem>
                                <asp:ListItem Value="1" Text="Monday"></asp:ListItem>
                                <asp:ListItem Value="2" Text="Tuesday"></asp:ListItem>
                                <asp:ListItem Value="3" Text="Wednesday"></asp:ListItem>
                                <asp:ListItem Value="4" Text="Thursday"></asp:ListItem>
                                <asp:ListItem Value="5" Text="Friday"></asp:ListItem>
                                <asp:ListItem Value="6" Text="Saturday"></asp:ListItem>
                                <asp:ListItem Value="7" Text="Sunday"></asp:ListItem>
                            </asp:DropDownList>
                        </div>
                    </div>
                </div>
                <div class="col-md-3">
                    <div class="form-group form-float">
                        <div class="form-line">
                            <asp:TextBox ID="txtFromTime" runat="server" CssClass="timepicker form-control" placeholder="From TIme"></asp:TextBox>

                        </div>
                    </div>
                </div>
                <div class="col-md-3">
                    <div class="form-group form-float">
                        <div class="form-line">
                            <asp:TextBox ID="txtToTime" runat="server" CssClass="timepicker form-control" placeholder="To TIme"></asp:TextBox>

                        </div>
                    </div>
                </div>
                <div class="col-md-3">
                    <div class="form-group">
                        <asp:Button ID="btnAdd" runat="server" Text="Add" CssClass="btn btn-primary waves-effect" OnClick="Insert" UseSubmitBehavior="false" />
                    </div>
                </div>
                <div class="col-md-12">
                    <div class="form-group">
                        <asp:GridView ID="grvFacultySchedule" runat="server" CssClass="table table-bordered" AutoGenerateColumns="false">
                            <Columns>
                                <asp:BoundField DataField="Day" HeaderText="Day" ItemStyle-Width="120" />
                                <asp:BoundField DataField="FromTime" HeaderText="From Time" ItemStyle-Width="120" />
                                <asp:BoundField DataField="ToTime" HeaderText="To Time" ItemStyle-Width="120" />
                            </Columns>
                        </asp:GridView>
                    </div>
                </div>
            </div>--%>
    </div>
</asp:Content>
