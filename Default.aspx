<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="_Default" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta charset="UTF-8">
    <meta content="width=device-width, initial-scale=1, maximum-scale=1, shrink-to-fit=no" name="viewport">
    <title>EMS</title>
    <!-- General CSS Files -->
    <link rel="stylesheet" href="plugins/assets/css/app.min.css">
    <link rel="stylesheet" href="plugins/assets/bundles/bootstrap-social/bootstrap-social.css">
    <!-- Template CSS -->
    <link rel="stylesheet" href="plugins/assets/css/style.css">
    <link rel="stylesheet" href="plugins/assets/css/components.css">
    <!-- Custom style CSS -->
    <link rel="stylesheet" href="plugins/assets/css/custom.css">
    <link rel='shortcut icon' type='image/x-icon' href="assets/images/mini.jpg" />
</head>
<body>
    <form id="form1" runat="server">
        <div id="app">
            <div class="text-center mt-4 mb-3">
                <a href="#">
                    <img alt="image" src="assets/images/mini.jpg" class="header-logo" />
                </a>
                <div class=" menu-header">
                    <h2>PARTY MANAGEMENT SYSTEM</h2>
                </div>
            </div>
            <section class="section">
                <div class="container mt-5">
                    <div class="row">
                        <div class="col-12 col-sm-8 offset-sm-2 col-md-6 offset-md-3 col-lg-6 offset-lg-3 col-xl-4 offset-xl-4">
                            <div class="card card-primary">
                                <div class="card-header">
                                    <h4>Login Page</h4>
                                </div>
                                <div class="card-body">
                                    <form action="#" class="needs-validation" novalidate="">
                                        <div class="form-group">
                                            <label for="credit2">
                                                Local Government:
                                    <asp:RequiredFieldValidator ControlToValidate="LgList" ID="RequiredFieldValidator4" ErrorMessage="*" runat="server" SetFocusOnError="true"></asp:RequiredFieldValidator>
                                            </label>
                                            <asp:DropDownList ID="LgList" runat="server" required CssClass="form-control form-control-rounded txtInput">
                                                <asp:ListItem Value="">--Select Local Government-</asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                        <div class="form-group">
                                            <div class="d-block">
                                                <label for="password" class="control-label">Password</label>
                                                <div class="float-right">
                                                    <%--<a href="#" class="text-small">
                          Forgot Password?
                        </a>--%>
                                                </div>
                                            </div>
                                            <asp:TextBox ID="passTxt" runat="server" class="form-control" TextMode="Password"></asp:TextBox>
                                            <div class="invalid-feedback">
                                                please fill in your password
                                            </div>
                                        </div>
                                        <div class="form-group">
                                            <div class="custom-control custom-checkbox">
                                                <input type="checkbox" name="remember" class="custom-control-input" tabindex="3" id="remember-me">
                                                <label class="custom-control-label" for="remember-me">Remember Me</label><br />
                                                <asp:Literal ID="Literal1" runat="server"></asp:Literal>
                                            </div>
                                        </div>
                                        <div class="form-group">
                                            <asp:Button ID="loginbtn" runat="server" Text="Login" class="btn btn-primary btn-lg btn-block" OnClick="loginbtn_Click" />
                                        </div>
                                    </form>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </section>
        </div>
        <!-- General JS Scripts -->
        <script src="plugins/assets/js/app.min.js"></script>
        <!-- JS Libraies -->
        <!-- Page Specific JS File -->
        <!-- Template JS File -->
        <script src="plugins/assets/js/scripts.js"></script>
        <!-- Custom JS File -->
        <script src="plugins/assets/js/custom.js"></script>
    </form>
</body>
</html>
