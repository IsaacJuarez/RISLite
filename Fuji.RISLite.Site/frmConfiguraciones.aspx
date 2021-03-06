﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="frmConfiguraciones.aspx.cs" Inherits="Fuji.RISLite.Site.frmConfiguraciones" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <!-- bootstrap & fontawesome -->
    <link rel="stylesheet" href="assets/css/bootstrap.min.css" />
    <link rel="stylesheet" href="assets/font-awesome/4.5.0/css/font-awesome.min.css" />

    <!-- page specific plugin styles -->
    <link rel="stylesheet" href="assets/css/jquery-ui.custom.min.css" />
    <link rel="stylesheet" href="assets/css/jquery.gritter.min.css" />

    <!-- text fonts -->
    <link rel="stylesheet" href="assets/css/fonts.googleapis.com.css" />

    <!-- ace styles -->
    <link rel="stylesheet" href="assets/css/ace.min.css" class="ace-main-stylesheet" id="main-ace-style" />

    <!--[if lte IE 9]>
		<link rel="stylesheet" href="assets/css/ace-part2.min.css" class="ace-main-stylesheet" />
	<![endif]-->
    <link rel="stylesheet" href="assets/css/ace-skins.min.css" />
    <link rel="stylesheet" href="assets/css/ace-rtl.min.css" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <telerik:RadAjaxManager ID="RadAjaxManager1" runat="server">
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="ddlTipoUsuario">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grvVista" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="grvVista">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grvVista" />
                </UpdatedControls>
            </telerik:AjaxSetting>
          </AjaxSettings>
    </telerik:RadAjaxManager>

    <div class="page-content">
        <div class="page-header">
            <div class="messagealert" id="alert_container"></div>
		    <h1>
			    Configuración 
			    <small>
				    <i class="ace-icon fa fa-angle-double-right"></i>
				    General
			    </small>
		    </h1>
	    </div><!-- /.page-header -->
        <div class="row">
            <div class="col-lg-6 col-md-12 col-sm-12">
                <div class="row">
                    <div class="widget-box">
                        <div class="widget-header widget-header-flat widget-header-small">
						    <h5 class="widget-title">
							    <i class="ace-icon fa fa-picture-o"></i>
							    Agregar Vista
						    </h5>
                        </div>
                        <div class="widget-body">
                            <div class="widget-main">
                                <div class="row">
                                    <div class="modal-body">
                                        <div class="row">
                                            <div class="form-group">
	                                            <label class="col-lg-3 col-md-3 col-sm-3 control-label no-padding-right" for="form-field-1"> Nombre de Vista</label>
	                                            <div class="col-lg-9 col-md-9 col-sm-9">
                                                    <asp:TextBox runat="server" ID="txtNombreVista" Text="" placeholader="Nombre de la vista" CssClass="form-control"></asp:TextBox>
                                                    <asp:RequiredFieldValidator runat="server" ID="rfvNOmbreVista" ControlToValidate="txtNombreVista" ErrorMessage="*" Text="*" ForeColor="Red" ValidationGroup="vgAddVista"></asp:RequiredFieldValidator>
	                                            </div>
                                            </div>
                                        </div>
                                        <div class="row">
                                            <div class="form-group">
	                                            <label class="col-lg-3 col-md-3 col-sm-3 control-label no-padding-right" for="form-field-1"> Identificador de Vista</label>
	                                            <div class="col-lg-9 col-md-9 col-sm-9">
                                                    <asp:TextBox runat="server" ID="txtIdentificador" Text="" placeholader="Identificador de la vista" CssClass="form-control"></asp:TextBox>
                                                    <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator1" ControlToValidate="txtIdentificador" ErrorMessage="*" Text="*" ForeColor="Red" ValidationGroup="vgAddVista"></asp:RequiredFieldValidator>
	                                            </div>
                                            </div>
                                        </div>
                                        <div class="row">
                                            <div class="form-group">
	                                            <label class="col-lg-3 col-md-3 col-sm-3 control-label no-padding-right" for="form-field-1"> Icono de Vista</label>
	                                            <div class="col-lg-9 col-md-9 col-sm-9">
                                                    <asp:TextBox runat="server" ID="txtIconoVista" Text="" placeholader="Icono de la vista" CssClass="form-control"></asp:TextBox>
                                                    <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator2" ControlToValidate="txtIconoVista" ErrorMessage="*" Text="*" ForeColor="Red" ValidationGroup="vgAddVista"></asp:RequiredFieldValidator>
	                                            </div>
                                            </div>
                                        </div>
                                        <div class="row">
                                            <div class="form-group text-right">
                                                <div class="col-lg-12 col-md-12 col-sm-12">
                                                    <asp:Button runat="server" ID="btnCancelarVista" Text="Cancelar" CssClass="btn btn-danger" OnClick="btnCancelarVista_Click" />
                                                    <asp:Button runat="server" ID="btnAgregar" Text="Agregar" CssClass="btn btn-success" OnClick="btnAgregar_Click" ValidationGroup="vgAddVista"/>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="row">
                    <div class="widget-box">
                        <div class="widget-header widget-header-flat widget-header-small">
						    <h5 class="widget-title">
							    <i class="ace-icon fa fa-puzzle-piece"></i>
							    Agregar Vista a Tipo de Usuario
						    </h5>
                        </div>
                        <div class="widget-body">
                            <div class="widget-main">
                                <div class="row">
                                    <div class="modal-body">
                                        <div class="row">
                                            <div class="form-group">
	                                            <label class="col-sm-3 control-label no-padding-right" for="form-field-1"> Tipo de Usuario</label>
	                                            <div class="col-sm-9">
		                                            <asp:DropDownList runat="server" ID="ddlTipoUsuarioAdd" CssClass="form-control"></asp:DropDownList>
	                                            </div>
                                            </div>
                                        </div>
                                        <div class="row">
                                            <div class="form-group">
	                                            <label class="col-sm-3 control-label no-padding-right" for="form-field-1"> Botón</label>
	                                            <div class="col-sm-9">
		                                            <asp:DropDownList runat="server" ID="ddlBotonAdd" CssClass="form-control"></asp:DropDownList>
	                                            </div>
                                            </div>
                                        </div>
                                        <div class="row">
                                            <div class="form-group">
	                                            <label class="col-sm-3 control-label no-padding-right" for="form-field-1"> Vista</label>
	                                            <div class="col-sm-9">
		                                            <asp:DropDownList runat="server" ID="ddlVistaAdd" CssClass="form-control"></asp:DropDownList>
	                                            </div>
                                            </div>
                                        </div>
                                        <div class="row">
                                            <div class="form-group text-right">
                                                <div class="col-lg-12 col-md-12 col-sm-12">
                                                    <asp:Button runat="server" ID="btnCancelAdd" Text="Cancelar" CssClass="btn btn-danger" OnClick="btnCancelAdd_Click" />
                                                    <asp:Button runat="server" ID="btnAddRelVistaBoton" Text="Agregar" CssClass="btn btn-success" OnClick="btnAddRelVistaBoton_Click"/>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <div class="col-lg-6 col-md-12 col-sm-12">
                <div class="widget-box">
					<div class="widget-header widget-header-flat widget-header-small">
						<h5 class="widget-title">
							<i class="ace-icon fa fa-signal"></i>
							Vistas por Tipo de Usuario
						</h5>
                    </div>
                    <div class="widget-body">
                        <div class="row">
                            <div class="col-lg-6 col-md-12 col-sm-12 text-right">
                            </div>
                            <div class="col-lg-6 col-md-12 col-sm-12 text-right">
                                <div class="modal-body">
                                    <div class="row">
                                        <div class="col-lg-12 col-md-12 col-sm-12">
                                            <telerik:RadComboBox runat="server" ID="ddlTipoUsuario"  RenderMode="Lightweight" ForeColor="DarkGreen" Width="100%"  OnClientSelectedIndexChanged="ComboTipoUsuario"></telerik:RadComboBox>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <hr />
                        <div class="row">
                            <div class="modal-body">
                                <div class="row">
                                    <div class="col-lg-12 col-md-12 col-sm-12">
                                        <telerik:RadAjaxPanel ID="radAjaxPanelVista" runat="server" OnAjaxRequest="radAjaxPanelVista_AjaxRequest">
                                            <asp:GridView ID="grvVista" runat="server" AllowPaging="true" CssClass="table table-striped table-bordered"
                                                PageSize="10" AutoGenerateColumns="false" OnRowDataBound="grvVista_RowDataBound" Font-Size="10px"
                                                OnPageIndexChanging="grvVista_PageIndexChanging" DataKeyNames="intRELUsuarioBotonID"
                                                OnRowCommand="grvVista_RowCommand"
                                                EmptyDataText="No hay resultado bajo el criterio de búsqueda.">
                                                <Columns>
                                                    <%--<asp:BoundField DataField="intRELUsuarioBotonID" HeaderText="ID" ReadOnly="true" ItemStyle-CssClass="hidden-md" HeaderStyle-CssClass="hidden-md"/>--%>
                                                    <asp:BoundField DataField="vchNombreBoton"  HeaderText="Botón" ReadOnly="true" />
                                                    <asp:BoundField DataField="vchNombreVista"  HeaderText="Vista" ReadOnly="true" />
                                                    <asp:TemplateField HeaderText="Editar" ItemStyle-HorizontalAlign="Center">
                                                        <ItemTemplate>      
                                                            <asp:LinkButton ID="btnVisualizar" CausesValidation="false" CommandName="viewEditar" CommandArgument='<%# Bind("intRELUsuarioBotonID") %>' runat="server">
                                                                <asp:Image ID="ImageVisializa" runat="server" ImageUrl="~/images/ic_action_edit.png" Height="25px" Width="25px" ToolTip="Editar"/>
                                                            </asp:LinkButton>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Estatus" ItemStyle-HorizontalAlign="Center">
                                                        <ItemTemplate>
                                                            <asp:ImageButton ID="imbEstatus" runat="server" BackColor="Transparent"  Height="25px" Width="25px" 
                                                                CommandArgument='<%#Eval("intRELUsuarioBotonID") %>' CommandName="Estatus" ToolTip="Cambia el estatus del Sitio" />
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
                                       </telerik:RadAjaxPanel>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>

        <div class="modal fade bs-example-modal-sm" id="myModal" tabindex="-1" role="dialog" aria-hidden="true">
            <div class="modal-dialog modal-sm">
                <div class="modal-content">

                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close"><span aria-hidden="true">×</span>
                    </button>
                    <h4 class="modal-title" id="myModalLabel2">Editar Vistas</h4>
                </div>
                <div class="modal-body">
                    <div class="form-horizontal" role="form">
                    <div class="form-group">
	                    <label class="col-sm-3 control-label no-padding-right" for="form-field-1"> Boton</label>

	                    <div class="col-sm-9">
		                    <input type="text" id="form-field-1" placeholder="Boton" class="col-xs-10 col-sm-5" />
	                    </div>
                    </div>

                    <div class="form-group">
                        <label for="form-field-select-1">Vistas</label>
                        <select class="form-control" id="form-field-select-1">
                            <option value="0">Seleccionar Vista</option>
		                    <option value="1">Dashborad (Default.aspx)</option>
		                    <option value="2">Lista de Trabajo (frmListaTrabajo.aspx)</option>
		                    <option value="3">Reporte y Estadisticas (frmRepEstadisticas.aspx)</option>
		                    <option value="4">Parámetros de Agenda (frmConfigAgenda.aspx)</option>
                            <option value="5">...</option>
                        </select>
                    </div>
                    </div>
                </div>
                <div class="modal-footer">
                    <button type="button" class="btn btn-default" data-dismiss="modal">Cerrar</button>
                    <button type="button" class="btn btn-primary">Actualizar</button>
                </div>

                </div>
            </div>
        </div>
        <!-- /modals -->
    </div>

    <script type="text/javascript">
        function openModal() {
            $('#myModal').modal('show');
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
            function ComboTipoUsuario(sender, eventArgs) {
                //var evento = eventArgs.Command.get_name();
                var idsitio = sender._value;
                $find('<%= radAjaxPanelVista.ClientID%>').ajaxRequestWithTarget('<%= radAjaxPanelVista.UniqueID %>', idsitio);
            }
        </script>
    </telerik:RadScriptBlock>
</asp:Content>
