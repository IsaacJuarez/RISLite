﻿<%@ Page Title="" Language="C#" AutoEventWireup="true" CodeBehind="frmSalir.aspx.cs" Inherits="Fuji.RISLite.Site.frmSalir" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title>Axon Salida</title>
</head>
<body>
    <center>
        <div class="row">
            <h1><asp:Label ID="lblMensaje" runat="server" Text="" Font-Bold="true"></asp:Label></h1>
        </div>
        <hr />
        <h1>
            <button onclick="window.location.href='/frmLogin.aspx'">Inicio</button>
        </h1>
    </center>
</body>
</html>