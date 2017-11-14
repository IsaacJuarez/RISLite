<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="frmSinPermiso.aspx.cs" Inherits="Fuji.RISLite.Site.frmSinPermiso" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="page-content">
        <div class="page-header">
            <div class="messagealert" id="alert_container"></div>
            <h1>Mensaje de Sistema
			    <small>
                    <i class="ace-icon fa fa-angle-double-right"></i>
                    Error de permisos
                </small>
            </h1>
        </div>
        <!-- /.page-header -->
        <div class="row">
            <h4><asp:Label runat="server" Text="No se cuenta con los permisos, consultar con el area de sistemas."></asp:Label> </h4>
        </div>
    </div>
</asp:Content>
