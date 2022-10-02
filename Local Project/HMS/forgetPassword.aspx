<%@ Page Language="C#" AutoEventWireup="true" CodeFile="forgetPassword.aspx.cs" Inherits="HMS.forgetPassword" %>

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



    <style>
        .mainBg {
            background-image: url("assets/img/bg2.png");
        }

        .btnHov {
            width: 100%;
            cursor: pointer;
        }

        ::-webkit-scrollbar {
            width: 10px;
        }

        ::-webkit-scrollbar-track {
            background: #f1f1f1;
        }

        ::-webkit-scrollbar-thumb {
            background: #888;
        }

            ::-webkit-scrollbar-thumb:hover {
                background: #555;
            }
    </style>

</head>

<body class="login-page">
    <form id="form1" runat="server">
        <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
        <asp:UpdatePanel ID="up1" runat="server">
            <ContentTemplate>
                <div class="login-box">
                    <div class="logo">
                        <a href="javascript:void(0);">
                            <img src="assets/images/Hms-Logo.png" style="width: 20%;" />
                        </a>
                    </div>
                    <div class="card">
                        <div class="body">
                            <div class="msg">Forget Password</div>
                            <div class="input-group" id="divEmail" runat="server">
                                <span class="input-group-addon">
                                    <i class="material-icons">Login ID</i>
                                </span>
                                <div class="form-line">
                                    <asp:TextBox ID="txtEmail" runat="server" class="form-control" placeholder="Email ID" required autofocus></asp:TextBox>
                                </div>
                            </div>
                            <div class="input-group" id="divPassCode" runat="server" visible="false">
                                <span class="input-group-addon">
                                    <i class="material-icons">Passcode</i>
                                </span>
                                <div class="form-line">
                                    <asp:TextBox ID="txtPassCode" runat="server" class="form-control" placeholder="Passcode" TextMode="Password" required></asp:TextBox>
                                </div>
                            </div>
                            <div class="input-group" id="divPassword" runat="server" visible="false">
                                <span class="input-group-addon">
                                    <i class="material-icons">Password</i>
                                </span>
                                <div class="form-line">
                                    <asp:TextBox ID="txtNewPassword" runat="server" class="form-control" placeholder="Password" TextMode="Password" required></asp:TextBox>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-xs-12">
                                    <asp:Button runat="server" Text="Send" ID="btnSend" CssClass="btn btn-success" OnClick="btnSend_Click" />
                                    <asp:Button runat="server" Visible="false" Text="Change Password" ID="btnConfirmPassword" CssClass="btn btn-primary" OnClick="btnConfirmPassword_Click" />
                                    <asp:Button runat="server" Text="Cancel" ID="btnCancel" CssClass="btn btn-danger" OnClick="btnCancel_Click" UseSubmitBehavior="false" />
                                    <asp:Button runat="server" Text="Back" ID="btnBack" CssClass="btn btn-default" OnClick="btnBack_Click" UseSubmitBehavior="false" />

                                </div>

                            </div>
                            <div class="row">
                                <div class="col-xs-12">
                                    <asp:Label ID="lblErrorMessege" runat="server" Style="font-family: verdana; font-weight: bold; font-size: 12px;"></asp:Label>
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
