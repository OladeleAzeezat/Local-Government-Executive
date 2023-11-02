<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Dashboard.aspx.cs" Inherits="Default2" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link href="assets/FileInput/fileinput.css" media="all" rel="stylesheet" type="text/css" />
    <link rel="stylesheet" href="assets/FileInput/all.css">
    <link href="assets/css/jquery-ui.css" rel='stylesheet'>
    <script src="assets/js/jquery-ui.min.js"> </script>
    <script type="text/javascript"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="content" runat="Server">
    <div class="main-content">
        <div>
            <h4 class="mr-2 text-center text-uppercase">WELCOME TO
                <asp:Label ID="LgLabel" runat="server" Text=" "></asp:Label>
                LOCAL GOVERNMENT DASHBOARD</h4>
        </div>
        <div class="separator-breadcrumb border-top"></div>
        <div class="col-lg-12 col-md-12 col-sm-12">
            <h6 class="text-center text-primary">LOCAL GOVERNMENT EXECUTIVES</h6>
            <h1>
                <asp:Label ID="Label2" runat="server" Text=""></asp:Label></h1>
            <asp:HiddenField runat="server" ID="hid" Value="" />
        </div>
        <section class="section">
            <div class="row ">
                <div class="col-lg-6 col-md-12 col-sm-12">
                    <asp:Button ID="btnLgList" runat="server" Text="Submit Exco List" class="btn btn-rounded btn-block btn-primary mt-2" OnClick="btnLgList_Click" />
                </div>
                <div class="col-lg-6 col-md-12 col-sm-12">
                    <asp:Button ID="btnLgView" runat="server" Text="View Exco List" class="btn btn-rounded btn-block btn-primary mt-2" OnClick="btnLgView_Click" />
                </div>
            </div>
        </section>
        <br />
        <div class="separator-breadcrumb border-top"></div>
        <div class="row">
            <div class="col-md-12">
                <asp:Panel runat="server" ID="Panel1" Visible="false">
                    <div class="alert alert-card alert-danger" role="alert">
                        <strong class="text-capitalize">Error!</strong>
                        <asp:Label runat="server" ID="Label1" Text=""></asp:Label>
                        <button class="close" type="button" data-dismiss="alert" aria-label="Close"><span aria-hidden="true">&times;</span></button>
                    </div>
                </asp:Panel>
            </div>
        </div>
        <div class="col-lg-12 col-md-12 col-sm-12">
            <h6 class="text-center text-primary">WARD EXECUTIVES</h6>
            <h1>
                <asp:Label ID="Label3" runat="server" Text=""></asp:Label></h1>
            <asp:HiddenField runat="server" ID="HiddenField1" Value="" />
        </div>
        <section class="section">
          <div class="row ">
                <div class="col-lg-6 col-md-12 col-sm-12">
                    <asp:Button ID="btnWaList" runat="server" Text="Submit Ward Exco List" class="btn btn-rounded btn-block btn-primary mt-2" OnClick="btnWaList_Click" />
                </div>
                <div class="col-lg-6 col-md-12 col-sm-12">
                    <asp:Button ID="btnWaView" runat="server" Text="View Ward Exco List" class="btn btn-rounded btn-block btn-primary mt-2" OnClick="btnWaView_Click" />
                </div>
            </div>  
        </section>
        <br />
        <div class="separator-breadcrumb border-top"></div>
        <div class="row">
            <div class="col-md-12">
                <asp:Panel runat="server" ID="Panel2" Visible="false">
                    <div class="alert alert-card alert-danger" role="alert">
                        <strong class="text-capitalize">Error!</strong>
                        <asp:Label runat="server" ID="Label4" Text=""></asp:Label>
                        <button class="close" type="button" data-dismiss="alert" aria-label="Close"><span aria-hidden="true">&times;</span></button>
                    </div>
                </asp:Panel>
            </div>
        </div>
        <div class="col-lg-12 col-md-12 col-sm-12">
            <h6 class="text-center text-primary">POLLING UNIT EXECUTIVES AND CANVASSERS</h6>
            <h1>
                <asp:Label ID="Label5" runat="server" Text=""></asp:Label></h1>
            <asp:HiddenField runat="server" ID="HiddenField2" Value="" />
        </div>
        <section class="section">
           <div class="row ">
                <div class="col-lg-3 col-md-12 col-sm-12">
                    <asp:Button ID="btnPuList" runat="server" Text="Submit Unit Exco List" class="btn btn-rounded btn-block btn-primary mt-2" OnClick="btnPuList_Click" />
                </div>
                <div class="col-lg-3 col-md-12 col-sm-12">
                    <asp:Button ID="btnPuView" runat="server" Text="View Unit Exco List" class="btn btn-rounded btn-block btn-primary mt-2" OnClick="btnPuView_Click" />
                </div>           
                <div class="col-lg-3 col-md-12 col-sm-12">
                    <asp:Button ID="btnCanList" runat="server" Text="Submit Canvassers" class="btn btn-rounded btn-block btn-primary mt-2" OnClick="btnCanList_Click" />
                </div>
                <div class="col-lg-3 col-md-12 col-sm-12">
                    <asp:Button ID="btnCanView" runat="server" Text="View Canvassers" class="btn btn-rounded btn-block btn-primary mt-2" OnClick="btnCanView_Click" />
                </div>
            </div>  
        </section>


    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="bottom" runat="Server">
</asp:Content>

