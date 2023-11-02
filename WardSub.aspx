<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="WardSub.aspx.cs" Inherits="Default2"  EnableEventValidation="false" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link href="assets/FileInput/fileinput.css" media="all" rel="stylesheet" type="text/css" />
    <link rel="stylesheet" href="assets/FileInput/all.css">
    <link href="assets/css/jquery-ui.css" rel='stylesheet'>
    <script src="assets/js/jquery-ui.min.js"> </script>
    <script type="text/javascript">      

      <%-- function GetWard() {
            if ($("#<%=LgList.ClientID%>")[0].selectedIndex > 0) {
                LoadImage("imgloadWd");
                $.ajax({
                    type: "POST",
                    url: "/generalService.asmx/GetWard",
                    data: '{lg: "' + $("#<%=LgList.ClientID%>")[0].value + '" }',
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    success: OnSuccessWard,
                    failure: function (response) {
                        alert(response);
                    }
                });
            }
            else {
                var codeList = $("#<%=WardList.ClientID%>");
                codeList.empty().append('<option selected="selected" value="">--Select Ward--</option>');
            }
        }

        function OnSuccessWard(response) {

            var img1 = $("#imgloadWd")[0];
            img1.style.display = "none";
            var wList = $("#<%=WardList.ClientID%>");
            wList.empty().append('<option selected="selected" value="">--Select Ward--</option>');
            if (response.d.length > 0) {
                $.each(response.d, function () {
                    wList.append($("<option></option>").val(this['wardTitle']).html(this['wardTitle']));
                });
            }
            else alertify.alert("Ward Not Exist for the selected state");
        }

        function LoadImage(id) {
            var img = $("#" + id)[0];
            img.style.display = "inline";
        }--%>

       <%-- function GetPu() {
            if ($("#<%=WardList.ClientID%>")[0].selectedIndex > 0) {
                LoadImage("imgloadPu");
                $.ajax({
                    type: "POST",
                    url: "/generalService.asmx/GetPu",
                    data: '{ward: "' + $("#<%=WardList.ClientID%>")[0].value + '" }',
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    success: OnSuccessPU,
                    failure: function (response) {
                        alert(response);
                    }
                });
            }
            else {
                var codeList = $("#<%=PuList.ClientID%>");
                codeList.empty().append('<option selected="selected" value="">--Select Polling Unit--</option>');
            }
        }

        function OnSuccessPU(response) {

            var img1 = $("#imgloadPu")[0];
            img1.style.display = "none";
            var pList = $("#<%=PuList.ClientID%>");
            pList.empty().append('<option selected="selected" value="">--Select Polling Unit--</option>');
            if (response.d.length > 0) {
                $.each(response.d, function () {
                    pList.append($("<option></option>").val(this['puTitle']).html(this['puTitle']));
                });
            }
            else alertify.alert("Polling Unit Not Exist for the selected Ward");
        }

        function LoadImage(id) {
            var img = $("#" + id)[0];
            img.style.display = "inline";
        }--%>


    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="content" runat="Server">
    <div class="main-content">
        <div>
            <h4 class="mr-2 text-center text-uppercase">WELCOME TO
                <asp:Label ID="LgLabel" runat="server" Text=" "></asp:Label>
                LOCAL GOVERNMENT DASHBOARD</h4>
        </div>
        <div class="separator-breadcrumb border-top"></div>
        <div class="row">
            <div class="col-md-12">
                <asp:Panel runat="server" ID="errorPanel" Visible="false">
                    <div class="alert alert-card alert-danger" role="alert">
                        <strong class="text-capitalize">Error!</strong>
                        <asp:Label runat="server" ID="ErrorLabel" Text=""></asp:Label>
                        <button class="close" type="button" data-dismiss="alert" aria-label="Close"><span aria-hidden="true">&times;</span></button>
                    </div>
                </asp:Panel>
            </div>
        </div>
        <div class="row" runat="server" id="panel0" visible="true">
            <div class="col-lg-12 col-md-12 col-sm-12">
                <h6 class="text-center text-primary">NOTE: WARD PAGE
                    <br />
                     <small>Submit Details for each position</small>
                </h6>
                <h1> <asp:Label ID="Label2" runat="server" Text=""></asp:Label></h1>
                <asp:HiddenField runat="server" ID="hid" Value="" />
            </div>
            <div class="col-lg-12 col-md-12 col-sm-12">
                <div class="card mb-5">
                    <div class="card-body">
                        <div class="row">                            
                            <div class="form-group col-lg-4 col-md-12 col-sm-12">
                                <label for="credit2">Local Government:
                                    <asp:RequiredFieldValidator ControlToValidate="txtLg" ID="RequiredFieldValidator1" ErrorMessage="*" runat="server" SetFocusOnError="true"></asp:RequiredFieldValidator>
                                </label>
                                <asp:TextBox ID="txtLg" runat="server" AutoPostBack="True" onchange="GetWard()" CssClass="form-control form-control-rounded txtInput" readonly="true"></asp:TextBox>
                                </div>
                            <div class="form-group col-lg-4 col-md-12 col-sm-12">
                                <label for="credit2">Wards:
                                    <asp:RequiredFieldValidator ControlToValidate="WardList" ID="RequiredFieldValidator2" ErrorMessage="*" runat="server" SetFocusOnError="true"></asp:RequiredFieldValidator>
                                </label>
                                <img style="display: none; margin-top: 0px; border-radius: 10px;" id="imgloadWd" src="../images/ajax-loader.gif" />
                                <asp:DropDownList ID="WardList" onchange="GetPu()" runat="server"  CssClass="form-control form-control-rounded txtInput">
                                    <asp:ListItem Value="">--Select Ward--</asp:ListItem>
                                </asp:DropDownList>
                            </div>
                            <div class="form-group col-lg-4 col-md-12 col-sm-12">
                                <label for="credit2">Position:
                                    <asp:RequiredFieldValidator ControlToValidate="PosList" ID="RequiredFieldValidator3" ErrorMessage="*" runat="server" SetFocusOnError="true" required="true"></asp:RequiredFieldValidator>
                                </label>
                                 <asp:DropDownList ID="PosList" onchange="GetFc()" runat="server" required="true" CssClass="form-control form-control-rounded txtInput">
                                    <asp:ListItem Value="">--Select Position-</asp:ListItem>
                                </asp:DropDownList>
                            </div>
                            </div>
                       <div class="row">
                            <div class="col-lg-4 col-md-12 col-sm-12">
                                <label for="firstName1">Name</label>
                                <asp:TextBox ID="txtName" runat="server" class="form-control form-control-rounded" type="text" placeholder="Name" required="true"></asp:TextBox>
                            </div>
                            <div class="col-lg-4 col-md-12 col-sm-12">
                                <label for="firstName1">Phone Number</label>
                                <asp:TextBox ID="txtNumber" runat="server" class="form-control form-control-rounded" type="text" placeholder="Phone Number" required="true"></asp:TextBox>
                            </div>
                            <div class="col-lg-4 col-md-12 col-sm-12">
                                <label for="firstName1">Voters Card Number</label>
                                <asp:TextBox ID="txtCard" runat="server" class="form-control form-control-rounded" type="text" placeholder="Voters Card Number" required="true"></asp:TextBox>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-lg-12 col-md-12 col-sm-12">
                                <asp:Button ID="Button1" runat="server" Text="Submit" class="btn btn-rounded btn-block btn-primary mt-2" OnClick="Button1_Click"/>
                            </div>
                           <h1> <asp:Label ID="Label1" runat="server" Text=" "></asp:Label></h1>
                        </div>
                    </div>
                </div>
            </div>
        </div>

    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="bottom" runat="Server">
    <script src="assets/FileInput/fileinput.js" type="text/javascript"></script>
    <script src="assets/FileInput/theme.js" type="text/javascript"></script>
    
</asp:Content>

