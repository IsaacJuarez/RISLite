<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="frmUsuario.aspx.cs" Inherits="Fuji.RISLite.Site.frmUsuario" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        .RadUpload_Default * {
            font-size: 11px;
            line-height: 1.24;
            font-family: arial,verdana,sans-serif;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <telerik:RadAjaxManager runat="server" ID="ajaxManager">
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="ajaxManager">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="RadFileUp" />
                    <telerik:AjaxUpdatedControl ControlID="preview" />
                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>
    </telerik:RadAjaxManager>

    <div class="page-content">
        <div class="page-header">
            <div class="messagealert" id="alert_container"></div>
            <h1>Administración
			    <small>
                    <i class="ace-icon fa fa-angle-double-right"></i>
                    Perfil de Usuario
                </small>
            </h1>
        </div>
        <!-- /.page-header -->
        <div class="row">
            <div class="col-lg-3 col-md-2 col-sm-12">
            </div>
            <div class="col-lg-6 col-md-8 col-sm-12">
                <div class="row form-group">
                    <div class="col-lg-6 col-md-12 col-sm-12 col-xs-12">
                        <asp:Label runat="server" ID="lblNameUser" Text="Nombre" ForeColor="DarkGreen" AssociatedControlID="txtNameUser"></asp:Label>
                        <asp:RequiredFieldValidator runat="server" ControlToValidate="txtNameUser" ValidationGroup="vg_AdminUser"></asp:RequiredFieldValidator>
                    </div>
                    <div class="col-lg-6 col-md-12 col-sm-12 col-xs-12">
                        <asp:TextBox runat="server" ID="txtNameUser" Text="" CssClass="form-control"></asp:TextBox>
                    </div>
                </div>
                <div class="row form-group">
                    <div class="col-lg-6 col-md-12 col-sm-12 col-xs-12">
                        <asp:Label runat="server" ID="lblPassUser" Text="Contraseña" ForeColor="DarkGreen" AssociatedControlID="txtNameUser"></asp:Label>                        
                    </div>
                    <div class="col-lg-6 col-md-12 col-sm-12 col-xs-12">
                        <asp:Label runat="server" ID="lblPassword" Visible="false" Text="" CssClass="form-control"></asp:Label>
                         <asp:Label Text="Si no se desea cambiar la contraseña, dejar el espacio vacio" runat="server" ForeColor="OrangeRed" Font-Size="10" ></asp:Label>
                        <asp:TextBox runat="server" ID="txtPassUser" TextMode="Password" Text="" CssClass="form-control" ToolTip="Si no se desea cambiar la contraseña, dejar el espacio en vacio"></asp:TextBox>
                    </div>
                </div>
                <div class="row form-group">
                    <div class="col-lg-6 col-md-12 col-sm-12 col-xs-12">
                        <asp:Label runat="server" ID="lblURL" Text="Imagen de usuario" AssociatedControlID="RadFileUp"></asp:Label>
                    </div>
                    <div class="col-lg-6 col-md-12 col-sm-12 col-xs-12">
                        <telerik:RadAjaxPanel runat="server" ID="ajaxPanelFileUpload" OnAjaxRequest="ajaxPanelFileUpload_AjaxRequest">
                            <telerik:RadAsyncUpload runat="server" ID="RadFileUp" Width="100%" MaxFileInputsCount="1" OnClientFileUploaded="fileUploaded" OnFileUploaded="RadFileUp_FileUploaded" AllowedFileExtensions=".png,.jpeg,.gif,.jpg" RenderMode="Lightweight" ForeColor="DarkGreen"></telerik:RadAsyncUpload>
                            <asp:Image runat="server" ID="imgUser" />
                            <telerik:RadBinaryImage runat="server" ID="preview" />
                        </telerik:RadAjaxPanel>
                    </div>
                </div>
                <div class="row form-group">
                    <div class="col-lg-6 col-md-12 col-sm-12 col-xs-12">
                    </div>
                    <div class="col-lg-6 col-md-12 col-sm-12 col-xs-12">
                        <asp:Button runat="server" ID="btnSavePerfil" OnClick="btnSavePerfil_Click" Text="Guardar" CssClass="btn btn-success" ValidationGroup="vg_AdminUser" />
                    </div>
                </div>
            </div>
            <div class="col-lg-3 col-md-2 col-sm-12">
            </div>
        </div>
        <hr />
        <div class="row">
        </div>
    </div>
    <script type="text/javascript">
        function Redirecciona(strRuta) {
            var sID = Math.round(Math.random() * 10000000000);
            var winX = screen.availWidth;
            var winY = screen.availHeight;
            sID = "E" + sID;
            window.open(strRuta, sID,
                "menubar=yes,toolbar=yes,location=yes,directories=yes,status=yes,resizable=yes" +
                ",scrollbars=yes,top=0,left=0,screenX=0,screenY=0,Width=" +
                winX + ",Height=" + winY);
        }

        function ShowMessage(message, messagetype, idControl) {
            var cssclass;
            switch (messagetype) {
                case 'Correcto':
                    cssclass = 'alert-success'
                    break;
                case 'Error':
                    cssclass = 'alert-danger'
                    break;
                case 'Advertencia':
                    cssclass = 'alert-warning'
                    break;
                default:
                    cssclass = 'alert-info'
            }
            var control = "#" + idControl;
            $(control).append('<div id="' + idControl + '" style="margin: 0 0.5%; -webkit-box-shadow: 3px 4px 6px #999;" class="alert fade in ' + cssclass + '"><a href="#" class="close" data-dismiss="alert" aria-label="close">&times;</a><strong>' + messagetype + '!</strong> <span>' + message + '</span></div>');
            $(control).fadeTo(2000, 700).slideUp(700, function () {
                $(control).slideUp(700);
            });
        }
    </script>

    <telerik:RadScriptBlock runat="server">
        <script type="text/javascript">
        function fileUploaded(sender, args) {
            $find('<%= ajaxPanelFileUpload.ClientID%>').ajaxRequestWithTarget('<%= ajaxPanelFileUpload.UniqueID %>', '');
            }
        </script>
    </telerik:RadScriptBlock>
</asp:Content>
