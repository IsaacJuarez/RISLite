<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="frmArribo.aspx.cs" Inherits="Fuji.RISLite.Site.frmArribo" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>Axon Ris</title>
    <link rel="icon" href="Images/favicon.ico" type="image/ico" />
    <!-- bootstrap & fontawesome -->
    <link rel="stylesheet" href="assets/css/bootstrap.min.css" />
    <link rel="stylesheet" href="assets/font-awesome/4.5.0/css/font-awesome.min.css" />

    <!-- page specific plugin styles -->

	<!-- text fonts -->
	<link rel="stylesheet" href="assets/css/fonts.googleapis.com.css" />

	<!-- ace styles -->
	<link rel="stylesheet" href="assets/css/ace.min.css" class="ace-main-stylesheet" id="main_ace_style" />

    <link rel="stylesheet" href="assets/css/Site.css" />

	<!--[if lte IE 9]>
		<link rel="stylesheet" href="assets/css/ace-part2.min.css" class="ace-main-stylesheet" />
	<![endif]-->
	<link rel="stylesheet" href="assets/css/ace-skins.min.css" />
	<link rel="stylesheet" href="assets/css/ace-rtl.min.css" />

	<!--[if lte IE 9]>
		<link rel="stylesheet" href="assets/css/ace-ie.min.css" />
	<![endif]-->

	<!-- inline styles related to this page -->

    <script src="assets/jquery/dist/jquery.min.js"></script>
	<!-- ace settings handler -->
	<script src="assets/js/ace-extra.min.js"></script>
    <script src="assets/js/bootbox.js"></script>
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
    <form id="form1" runat="server">
        <asp:ScriptManager runat="server" ></asp:ScriptManager>
        <div class="row">
            <div class="col-lg-2 col-md-1 col-sm-1 col-xs-1">
            </div>
            <div class="col-lg-8 col-md-10 col-sm-10 col-xs-10">
                <div class="page-content">
                    <div class="page-header">
                        <div class="messagealert" id="alert_container"></div>
                        <h1>Paciente en Cita
			            <small>
                            <i class="ace-icon fa fa-angle-double-right"></i>
                            Arribos
                        </small>
                        </h1>
                    </div>
                    <!-- /.page-header -->
                    <div class="row">
                        <asp:UpdatePanel runat="server">
                            <ContentTemplate>
                                <div id="divPrincipal" runat="server" class="col-lg-12 col-md-12 col-sm-12 col-xs-12">
                                    <div class="row">
                                        <div class="col-lg-10 col-md-10 col-sm-12 col-xs-12">
                                            <h1><small><asp:Label runat="server" Text="Paciente  " ForeColor="DarkGreen" ></asp:Label></small><asp:Label runat="server" ID="lblNamePacient" Text="Nombre del paciente" ForeColor="DarkGreen" Font-Bold="true"></asp:Label></h1>
                                        </div>
                                        <div class="col-lg-2 col-md-2 col-sm-12 col-xs-12 text-right">
                                            <asp:HiddenField runat="server" Value="" ID="hfintCitaID" />
                                            <asp:LinkButton ID="btnEditPaciente" runat="server" OnClick="btnEditPaciente_Click" Text="Detalle" ToolTip="Detalle del Paciente">
                                                <i class="fa fa-pencil-square-o" aria-hidden="true"  title="Detalle del Paciente" style="font-size:25px;"></i>
                                            </asp:LinkButton>
                                        </div>
                                    </div>
                                    <hr />
                                    <div class="row">
                                        <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12">
                                            <h1><small><asp:Label runat="server" Text="Fecha y Hora:  " ForeColor="DarkGreen" ></asp:Label></small><asp:Label runat="server" ID="lblFechaCita" Text="dd/MM/yyyy HH:mm" ForeColor="DarkGreen" Font-Bold="true"></asp:Label></h1>
                                        </div>
                                    </div>
                                    <hr />
                                    <div class="row">
                                        <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12">
                                            <asp:Label runat="server" ID="Label2" Text="Estudios." ForeColor="DarkGreen" Font-Bold="true"></asp:Label>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12">
                                            <asp:GridView ID="grvEstudios" runat="server" AllowPaging="true" CssClass="table table-striped table-bordered"
                                                PageSize="10" AutoGenerateColumns="false" OnRowDataBound="grvEstudios_RowDataBound" Font-Size="10px"
                                                OnPageIndexChanging="grvEstudios_PageIndexChanging" DataKeyNames="intEstudioID"
                                                OnRowCommand="grvEstudios_RowCommand"
                                                EmptyDataText="No hay estudios para la cita.">
                                                <Columns>
                                                    <asp:BoundField DataField="vchPrestacion" HeaderText="Estudio" ReadOnly="true"  />
                                                    <asp:TemplateField HeaderText="Fecha de Inicio" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblFechaInicio" runat="server" Text='<%# String.Format("{0:dd/MM/yyyy}", Eval("fechaInicio")) %>' ForeColor="DarkGreen" />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Hora de Inicio" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblHoraInicio" runat="server" Text='<%# String.Format("{0:hh:mm}", Eval("fechaInicio")) %>' ForeColor="DarkGreen" />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:BoundField DataField="vchEstatus" HeaderText="Estatus" ReadOnly="true" />
                                                    <asp:TemplateField HeaderText="Seleccionar" ItemStyle-HorizontalAlign="Center">
                                                        <ItemTemplate>
                                                           <asp:CheckBox runat="server" ID="chkEstudio"  EnableViewState="true" ForeColor="DarkBlue" Checked="true" />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                </Columns>
                                                <PagerTemplate>
                                                    <asp:Label ID="lblTemplate" runat="server" Text="Muestra Filas: " CssClass="Label" />
                                                    <asp:DropDownList ID="ddlBandeja" runat="server" AutoPostBack="true" CausesValidation="false"
                                                        Enabled="true" OnSelectedIndexChanged="ddlBandeja_SelectedIndexChanged">
                                                        <asp:ListItem Value="10" />
                                                        <asp:ListItem Value="15" />
                                                        <asp:ListItem Value="20" />
                                                    </asp:DropDownList>
                                                    &nbsp;Página
                                                        <asp:TextBox ID="txtBandeja" runat="server" AutoPostBack="true" OnTextChanged="txtBandeja_TextChanged"
                                                            Width="40" MaxLength="10" />
                                                    de
                                                        <asp:Label ID="lblBandejaTotal" runat="server" />
                                                    &nbsp;
                                                        <asp:Button ID="btnBandeja_I" runat="server" CommandName="Page" CausesValidation="false"
                                                            ToolTip="Página Anterior" CommandArgument="Prev" CssClass="previous" />
                                                    <asp:Button ID="btnBandeja_II" runat="server" CommandName="Page" CausesValidation="false"
                                                        ToolTip="Página Siguiente" CommandArgument="Next" CssClass="next" />
                                                </PagerTemplate>
                                                <HeaderStyle CssClass="headerstyle" />
                                                <FooterStyle CssClass="text-center" />
                                                <PagerStyle CssClass="text-center" />
                                            </asp:GridView>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12 text-right">
                                            <asp:Button runat="server" ID="btnArribo" Text="Aceptar" CssClass="btn btn-success" OnClick="btnArribo_Click" />
                                        </div>
                                    </div>
                                </div>
                                <div id="divMensaje" runat="server" class="col-lg-12 col-md-12 col-sm-12 col-xs-12">
                                    <h1><asp:Label runat="server" Text="" ForeColor="DarkGreen" ID="lblMensaje"></asp:Label></h1>
                                </div>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </div>
                    <!-- /.row -->
                </div>
            </div>
            <div class="col-lg-2 col-md-1 col-sm-1 col-xs-1">
            </div>
        </div>
        <!-- /.page-content -->


        <!-- modals -->
        <div class="modal fade bs-example-modal-sm" id="modalLogin" tabindex="-1" role="dialog" aria-hidden="true">
            <div class="modal-dialog modal-sm">
                <div class="modal-content">
                    <div class="modal-header">
                        <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                            <span aria-hidden="true">×</span>
                        </button>
                        <h4 class="modal-title" id="myModalLabel3">
                            Acceso
                        </h4>
                    </div>
                    <div class="modal-body">
                        <div>
                            <asp:TextBox ID="txtUsuario" placeholder="Usuario" runat="server" ValidationGroup="vgLogin" class="form-control" Width="100%"></asp:TextBox>
		                    <asp:TextBox ID="txtPass" TextMode="Password" placeholder="Contraseña" runat="server" ValidationGroup="vgLogin" class="form-control" Width="100%"></asp:TextBox>
                            <div>
                                <asp:RequiredFieldValidator ErrorMessage="Usuario requerido." ForeColor="#61210B" Font-Bold="true" runat="server" ID="rfvUser" ControlToValidate="txtUsuario" ValidationGroup="vgLogin"></asp:RequiredFieldValidator>
                                <asp:RequiredFieldValidator ErrorMessage="Contraseña requerida" ForeColor="#61210B" Font-Bold="true" runat="server" ID="rfvPass" ControlToValidate="txtPass" ValidationGroup="vgLogin"></asp:RequiredFieldValidator>
                            </div>
                            <br />
                            <div>
                                <asp:UpdatePanel runat="server">
                                    <ContentTemplate>
                                        <asp:Label runat="server" ID="Label1" Text="" ForeColor="DarkGreen"></asp:Label>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </div>
                        </div>
                    </div>
                    <div class="modal-footer">
                        <asp:Button runat="server" ID="btnCancelLogin" class="btn btn-default" Text="Cancelar" data-dismiss="modal"></asp:Button>
                        <asp:Button type="submit" id="Button1" runat="server" Text="Entrar" OnClick="btnLogin_Click" ValidationGroup="vgLogin" class="btn btn-success"></asp:Button>
                    </div>

                </div>
            </div>
        </div>
        <!-- /modals -->

    </form>
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
    <script src="assets/js/bootstrap.min.js"></script>
    <!-- page specific plugin scripts -->
    <script src="assets/js/jquery-ui.custom.min.js"></script>
    <script src="assets/js/jquery.ui.touch-punch.min.js"></script>
    <script src="assets/js/moment.min.js"></script>
    <script src="assets/js/bootbox.js"></script>
    <!-- inline scripts related to this page -->

    <script src="assets/js/ace-elements.min.js"></script>
    <script src="assets/js/ace.min.js"></script>
    
</body>
</html>
