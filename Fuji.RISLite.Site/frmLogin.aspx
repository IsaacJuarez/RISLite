<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="frmLogin.aspx.cs" Inherits="Fuji.RISLite.Site.frmLogin" %>
<link href="assets/css/bootstrap.min.css" rel="stylesheet" />
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <link rel="icon" href="Images/faviconF.ico" type="image/ico" />
    <title>Axon Ris</title>
    <link rel="stylesheet" href="assets/css/style.css"/>
    <link rel="stylesheet" href="assets/css/bootstrap.min.css"/>
    <script src="assets/js/jquery-1.11.3.min.js"></script>
    <script src="assets/css/bootstrap.min.css"></script>
    <style type="text/css">
        .imagen {
            -webkit-background-size: cover;
            -moz-background-size: cover;
            -o-background-size: cover;
            background-size: cover;
            height: 80%;
            width: 80%;
            text-align: center;
        }
        .imagenFuji {
            -webkit-background-size: cover;
            -moz-background-size: cover;
            -o-background-size: cover;
            background-size: cover;
            height: 7%;
            width: 48%;
            text-align: center;
        }
        .btn-info {
            color: #fff;
            background-color: #5bc0de;
            border-color: #46b8da;
        }
    </style>
</head>
<body>
    <div class="bg-bubbles">
        <div class="wrapper">
            <center>
                <div class="container">
                    <h2>Bienvenido</h2>
                    <div class="col-md-12 col-sm-12 col-xs-12">
                        <%--<img src="Images/HorseNapoleon.png" class="imagen"/>--%>
                        <center><h1>Axon<small>Ris</small></h1></center>
                        <form id="form" runat="server" >
                            <asp:ScriptManager runat="server" ></asp:ScriptManager>
                            <div>
                                <asp:TextBox ID="txtUsuario" placeholder="Usuario" runat="server" ValidationGroup="vgLogin" class="form" ></asp:TextBox>
		                        <asp:TextBox ID="txtPass" TextMode="Password" placeholder="Contraseña" runat="server" ValidationGroup="vgLogin" class="form"></asp:TextBox>
                                <asp:Button type="submit" id="btnLogin" runat="server" Text="Entrar" OnClick="btnLogin_Click" ValidationGroup="vgLogin" class="form"></asp:Button>
                                <div>
                                    <asp:RequiredFieldValidator ErrorMessage="Usuario requerido." ForeColor="#61210B" Font-Bold="true" runat="server" ID="rfvUser" ControlToValidate="txtUsuario" ValidationGroup="vgLogin"></asp:RequiredFieldValidator>
                                    <asp:RequiredFieldValidator ErrorMessage="Contraseña requerida" ForeColor="#61210B" Font-Bold="true" runat="server" ID="rfvPass" ControlToValidate="txtPass" ValidationGroup="vgLogin"></asp:RequiredFieldValidator>
                                </div>
                                <br />
                                <div>
                                    <asp:UpdatePanel runat="server">
                                        <ContentTemplate>
                                            <asp:Label runat="server" ID="lblMensaje" Text="" ForeColor="DarkGreen"></asp:Label>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </div>
                            </div>
                            <img src="Images/FUji_Logo_svg.png" class="imagenFuji"/>
                        </form>
                    </div>
                    <!-- footer content -->
                    <footer>
                        <div class="pull-center">
                            <p><%: DateTime.Now.Year %> - Axon Ris - Versión 1.0    </p>
                        </div>
                    </footer>
                    <!-- /footer content -->                    
                </div>
            </center>
        </div>
    </div>
</body>
</html>
