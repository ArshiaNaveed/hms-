<%@ Page Language="C#" AutoEventWireup="true" CodeFile="login.aspx.cs" Inherits="HMS.login" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <meta charset="UTF-8">
    <meta content="width=device-width, initial-scale=1, maximum-scale=1, user-scalable=no" name="viewport">
    <title>YIFF - Hospital Management System</title>
    <!-- Bootstrap Core Css -->
    <link href="assets/plugins/bootstrap/css/bootstrap.css" rel="stylesheet">
    <!-- Waves Effect Css -->
    <link href="assets/plugins/node-waves/waves.css" rel="stylesheet" />
    <!-- Animation Css -->
    <link href="assets/plugins/animate-css/animate.css" rel="stylesheet" />
    <!-- Custom Css -->
    <link href="assets/css/style.css" rel="stylesheet">
</head>

<body class="login-page">
    <form id="form1" runat="server">
        <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
        <asp:UpdatePanel ID="up1" runat="server">
            <ContentTemplate>
                <div class="login-box">
                    <div class="logo">
                        <a href="javascript:void(0);">
                            <img src="assets/images/Hms-Logo.png" />
                        </a>
                    </div>
                    <div class="card">
                        <div class="body">
                            <div class="msg">Login Panel</div>
                            <div class="input-group">
                                <span class="input-group-addon">
                                    <i class="material-icons">Login ID</i>
                                </span>
                                <div class="form-line">
                                    <asp:TextBox ID="txtLoginId" runat="server" class="form-control" placeholder="Login ID" required autofocus></asp:TextBox>
                                </div>
                            </div>
                            <div class="input-group">
                                <span class="input-group-addon">
                                    <i class="material-icons">Password</i>
                                </span>
                                <div class="form-line">
                                    <asp:TextBox ID="txtPassword" runat="server" class="form-control" placeholder="Password" TextMode="Password" required></asp:TextBox>
                                </div>
                            </div>
                            <div class="row">
                                <asp:Label ID="lblError" runat="server" Style="font-size: 12px; color: red;"></asp:Label>
                                <div class="col-xs-4">

                                    <asp:Button ID="btnLogin" runat="server" Text="Login" CssClass="btn btn-block bg-pink waves-effect" OnClick="btnLogin_Click" />

                                </div>
                                 <div class="col-xs-8">
                                     <a href="forgetPassword.aspx" style="float:right;" >Forget Password</a>
                                 </div>
                            </div>
                        </div>
                    </div>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
    </form>

    <!-- Jquery Core Js -->
    <script src="assets/plugins/jquery/jquery.min.js"></script>

    <!-- Bootstrap Core Js -->
    <script src="assets/plugins/bootstrap/js/bootstrap.js"></script>

    <!-- Waves Effect Plugin Js -->
    <script src="assets/plugins/node-waves/waves.js"></script>

    <!-- Validation Plugin Js -->
    <script src="assets/plugins/jquery-validation/jquery.validate.js"></script>

    <!-- Custom Js -->
    <script src="assets/js/admin.js"></script>
    <script src="assets/js/pages/examples/sign-in.js"></script>
</body>
</html>
