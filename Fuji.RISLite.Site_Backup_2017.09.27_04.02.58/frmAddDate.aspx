﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="frmAddDate.aspx.cs" Inherits="Fuji.RISLite.Site.frmAddDate" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

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
            <telerik:AjaxSetting AjaxControlID="pnlAdiClin" >
                <UpdatedControls><telerik:AjaxUpdatedControl  ControlID="pnlAdiClin"/></UpdatedControls>
            </telerik:AjaxSetting>

            <telerik:AjaxSetting AjaxControlID="pnlAdiOpe" >
                <UpdatedControls><telerik:AjaxUpdatedControl  ControlID="pnlAdiOpe"/></UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>
    </telerik:RadAjaxManager>
    <div class="page-content">
        <div class="page-header">
            <div class="messagealert" id="alert_container"></div>
            <h1>Agenda
			    <small>
                    <i class="ace-icon fa fa-angle-double-right"></i>
                    Citas
                </small>
            </h1>
        </div>
        <!-- /.page-header -->
        <div class="row">
            <div class="col-lg-5 col-md-12 col-sm-12 col-xs-12">
                <div class="row">
                    <div class="widget-box">
                        <div class="widget-header widget-header-flat widget-header-small">
                            <h5 class="widget-title">
                                <i class="ace-icon fa fa-id-card-o"></i>
                                Agregar Cita
                            </h5>
                        </div>
                        <div class="widget-body">
                            <div class="widget-main">
                                <div class="row">
                                    <div class="col-lg-10 col-md-10 col-sm-10 col-xs-10 form-search">
                                        <span class="input-icon" style="width:100%">
                                            <asp:AutoCompleteExtender ID="acxBusqueda" runat="server" TargetControlID="txtBusquedaPaciente" MinimumPrefixLength="1" EnableCaching="true" CompletionSetCount="1"
                                                CompletionInterval="500" ServiceMethod="obtenerPacienteBusqueda" CompletionListCssClass="completionList" CompletionListItemCssClass="listItem" OnClientItemSelected="autoCompleteEx_ItemSelected"
                                                CompletionListHighlightedItemCssClass="itemHighlighted">
                                            </asp:AutoCompleteExtender>
                                            <asp:TextBox ID="txtBusquedaPaciente" runat="server" CssClass="nav-search-input" placeholder="Busquéda Paciente..." 
                                                ToolTip="Búsqueda de Paciente por Nombre, Apellido, NSS, ID del paciente." Width="100%" OnTextChanged="txtBusquedaPaciente_TextChanged"></asp:TextBox>
                                            <i runat="server" id="imgSearchNC" class="ace-icon fa fa-search nav-search-icon"></i>
                                        </span>
                                    </div>
                                    <div class="col-lg-2 col-md-2 col-sm-2 col-xs-2 text-center">
                                        <asp:LinkButton ID="btnAddUser" runat="server" OnClick="btnAddUser_Click" Text="Actualizar">
                                            <i class="fa fa-user-plus" aria-hidden="true"  title="Agregar Usuario" style="font-size:25px;"></i>
                                        </asp:LinkButton>
                                    </div>
                                </div>
                                <hr />
                                <div class="row">
                                    <div class="col-lg-6 col-md-12 col-sm-12 col-xs-12">
                                        <asp:LinkButton ID="btnEditPaciente" runat="server" OnClick="btnEditPaciente_Click" Text="Editar" Visible="false">
                                            <i class="fa fa-pencil-square-o" aria-hidden="true"  title="Editar Paciente" style="font-size:25px;"></i>
                                        </asp:LinkButton>
                                        <asp:Label runat="server" ID="lblPacienteTitulo" ForeColor="DarkBlue" Text="Paciente" Font-Bold="true"></asp:Label>
                                        <asp:HiddenField runat="server" ID="HFintPacienteID" ClientIDMode="Static" />                                        
                                    </div>
                                    <div class="col-lg-6 col-md-12 col-sm-12 col-xs-12 text-right">
                                        <asp:RequiredFieldValidator runat="server" ID="rfvPaciente" Text="* Seleccionar paciente" ErrorMessage="* Seleccionar Paciente" ForeColor="Red" ControlToValidate="txtNombrePaciente" ValidationGroup="vgSaveCita"></asp:RequiredFieldValidator>
                                        <asp:Label runat="server" ID="lblIDs" ForeColor="PowderBlue" Text="" Font-Bold="true" Visible="false"></asp:Label>
                                    </div>
                                </div>
                                <div class="row form-group">
                                    <div class="col-lg-5 col-md-5 col-sm-12 col-xs-12">
                                        <asp:Label runat="server" Text="Nombre" AssociatedControlID="txtNombrePaciente"></asp:Label>
                                    </div>
                                    <div class="col-lg-7 col-md-7 col-sm-12 col-xs-12">
                                        <asp:TextBox runat="server" Text="" ID="txtNombrePaciente" Width="100%" Enabled="false"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="row form-group">
                                    <div class="col-lg-5 col-md-5 col-sm-12 col-xs-12">
                                        <asp:Label runat="server" Text="Apellidos"></asp:Label>
                                    </div>
                                    <div class="col-lg-7 col-md-7 col-sm-12 col-xs-12">
                                        <asp:TextBox runat="server" Text="" ID="txtApellidos" Width="100%" Enabled="false"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="row form-group">
                                    <div class="col-lg-5 col-md-5 col-sm-12 col-xs-12">
                                        <asp:Label runat="server" Text="Fecha de Nacimiento"></asp:Label>
                                    </div>
                                    <div class="col-lg-7 col-md-7 col-sm-12 col-xs-12">
                                        <asp:TextBox runat="server" ID="Date1" autocomplete="off" CssClass="form-control" Width="100%"  Enabled="false"/>
                                    </div>
                                </div>
                                <hr />
                                <div class="row">
                                    <div class="col-lg-5 col-md-12 col-sm-12 col-xs-12">
                                        <asp:Label runat="server" ID="Label1" ForeColor="DarkBlue" Text="Adicionales" Font-Bold="true"></asp:Label>
                                    </div>
                                    <div class="col-lg-7 col-md-12 col-sm-12 col-xs-12 text-left">
                                        <asp:Panel runat="server" ID="pnlAdiClin" CssClass="form-group">

                                        </asp:Panel>
                                        <asp:UpdatePanel runat="server">
                                            <ContentTemplate>
                                                <asp:Panel runat="server" ID="pnlObservaciones" ClientIDMode="Static">

                                                </asp:Panel>
                                            </ContentTemplate>
                                        </asp:UpdatePanel>
                                    </div>
                                </div>
                                <hr />
                                <div class="row">
                                    <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12 text-right">
                                        <asp:Panel runat="server" ID="pnlAdiOpe" CssClass="form-group">

                                        </asp:Panel>
                                        <asp:LinkButton runat="server" ID="lnkImprimir" CssClass="btn btn-app btn-yellow radius-4" ToolTip="Imprimir Cita" OnClick="lnkImprimir_Click" Enabled="false">
                                            <i class="fa fa-print -o" aria-hidden="true"  title="Imprimir Cita" style="font-size:25px;"></i>
                                        </asp:LinkButton>
                                        <asp:LinkButton runat="server" ID="lnkReEnviarCorreo" CssClass="btn btn-app btn-primary radius-4" ToolTip="Re-enviar cita por correo" OnClick="lnkReEnviarCorreo_Click" Enabled="false">
                                            <i class="fa fa-envelope-o -o" aria-hidden="true"  title="Re-enviar cita por correo" style="font-size:25px;"></i>
                                        </asp:LinkButton>
                                        <asp:LinkButton runat="server" ID="lnkInterpretacion" CssClass="btn btn-app btn-purple radius-4" ToolTip="Interpretación" OnClick="lnkInterpretacion_Click" Enabled="false">
                                            <i class="fa fa-user-md -o" aria-hidden="true"  title="Interpretación" style="font-size:25px;"></i>
                                        </asp:LinkButton>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <div class="col-lg-7 col-md-12 col-sm-12 col-sx-12">
                <div class="row">
                    <div class="col-lg-12 col-md-12 col-sm-12 col-sx-12">
                        <div class="row">
                            <div class="col-lg-2 col-md-2 col-sm-12 col-xs-12">
                                <asp:Label runat="server" ID="Label2" ForeColor="DarkBlue" Text="Estudios" Font-Bold="true"></asp:Label>
                            </div>
                            <div class="col-lg-10 col-md-10 col-sm-12 col-xs-12">
                                <span class="input-icon" style="width:100%">
                                    <asp:AutoCompleteExtender ID="AutoCompleteExtender1" runat="server" TargetControlID="txtBusquedaEstudio" MinimumPrefixLength="1" EnableCaching="true" CompletionSetCount="1"
                                        CompletionInterval="500" ServiceMethod="obtenerEstudioBusqueda" CompletionListCssClass="completionList" CompletionListItemCssClass="listItem" OnClientItemSelected="autoCompleteEx_ItemSelected"
                                        CompletionListHighlightedItemCssClass="itemHighlighted">
                                    </asp:AutoCompleteExtender>
                                    <asp:TextBox ID="txtBusquedaEstudio" runat="server" CssClass="nav-search-input" placeholder="Estudio..." ToolTip="Seleccionar Estudio" Width="100%" OnTextChanged="txtBusquedaEstudio_TextChanged"></asp:TextBox>
                                    <i runat="server" id="i1" class="ace-icon fa fa-search nav-search-icon"></i>
                                </span>
                            </div>
                        </div>
                        <hr />
                        <div class="row">
                            <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12">
                                <asp:UpdatePanel runat="server">
                                    <ContentTemplate>
                                        <asp:Panel runat="server">
                                            <asp:GridView ID="grvEstudios" runat="server" AllowPaging="true" CssClass="table table-striped table-bordered"
                                                PageSize="10" AutoGenerateColumns="false" OnRowDataBound="grvEstudios_RowDataBound" Font-Size="10px"
                                                OnPageIndexChanging="grvEstudios_PageIndexChanging" DataKeyNames="intRELModPres, intEstudioID, intPrestacionID" OnRowCancelingEdit="grvEstudios_RowCancelingEdit"
                                                OnRowCommand="grvEstudios_RowCommand" OnRowEditing="grvEstudios_RowEditing" OnRowUpdating="grvEstudios_RowUpdating"
                                                OnRowDeleting="grvEstudios_RowDeleting" EmptyDataText="No hay resultado bajo el criterio de búsqueda.">
                                                <Columns>
                                                    <asp:BoundField DataField="vchPrestacion" HeaderText="Estudio" ReadOnly="true" ItemStyle-CssClass="hidden-md" HeaderStyle-CssClass="hidden-md"/>
                                                    <asp:TemplateField HeaderText="fechaInicio" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblFechaInicio" runat="server" Text='<%# String.Format("{0:MMMM d, yyyy}", Eval("fechaInicio")) %>' ForeColor="DarkGreen" />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Hora de Inicio" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblHoraInicio" runat="server" Text='<%# String.Format("{0:hh/mm}", Eval("fechaInicio")) %>' ForeColor="DarkGreen" />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Elegir Horario" ItemStyle-HorizontalAlign="Center">
                                                        <ItemTemplate>
                                                            <asp:LinkButton ID="btnVisualizar" CausesValidation="false" CommandName="ElegirHorario" CommandArgument='<%#Eval("intRELModPres") %>' runat="server">
                                                                <i class="fa fa-calendar-check-o" aria-hidden="true" title="Buscar horario" style="font-size:25px;"></i>
                                                            </asp:LinkButton>
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
                                        </asp:Panel>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="row">
                    <div class="col-lg-12 col-md-12 col-sm-12 col-sx-12">
                        <div class="tabbable">
                            <ul class="nav nav-tabs" id="myTab">
                                <li class="active">
                                    <a data-toggle="tab" href="#home">
                                        <i class="green ace-icon fa fa-home bigger-120"></i>
                                        Horarios
                                    </a>
                                </li>

                                <li>
                                    <a data-toggle="tab" href="#messages">Agenda
								        <span class="badge badge-danger">4</span>
                                    </a>
                                </li>
                            </ul>
                            <div class="tab-content">
                                <div id="home" class="tab-pane fade in active">
                                    <div class="widget-box">
								        <div class="widget-header">
									        <h4 class="widget-title">Sugerencias</h4>

								        </div>

								        <div class="widget-body">
									        <div class="widget-main">
                                                <asp:UpdatePanel runat="server"><ContentTemplate><p><asp:Label runat="server" id="lblTituloSug" Text="Elegir un estudio" ForeColor="DarkGreen"></asp:Label></p></ContentTemplate></asp:UpdatePanel>
                                            </div>
                                        </div>
						            </div> 
                                </div>

                                <div id="messages" class="tab-pane fade">
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <!-- /.col -->
        </div>
        <div class="row">
            <div class="col-lg-3 col-md-3 col-sm-1 col-xs-1">
            </div>
            <div class="col-lg-9 col-md-9 col-sm-11 col-xs-11 text-right">
                <asp:Button ID="btnCancelPaciente" runat="server" OnClick="btnCancelPaciente_Click" Text="Limpiar" CssClass="btn btn-danger" />
                <asp:Button ID="btnAddCita" OnClick="btnAddCita_Click" runat="server" Text="Guardar" CssClass="btn btn-success" ToolTip="Generar Cita" ValidationGroup="vgSaveCita"/>
            </div>
        </div>
        <!-- /.row -->
    </div>
    <!-- /.page-content -->

    <!-- modals -->
    <div class="modal fade bs-example-modal-sm" id="myModal" tabindex="-1" role="dialog" aria-hidden="true">
        <div class="modal-dialog" style="width:70%">
            <div class="modal-content">

            <div class="modal-header">
                <button type="button" class="close" data-dismiss="modal" aria-label="Close"><span aria-hidden="true">×</span>
                </button>
                <h4 class="modal-title" id="myModalLabel2">Paciente</h4>
            </div>
            <div class="modal-body">
                <asp:UpdatePanel runat="server">
                    <ContentTemplate>
                        <div class="form-horizontal" role="form">
                            <div class="row">
                            </div>
                            <div class="row">
                                <div class="col-lg-6 col-md-12">
                                    <div class="row ">
                                        <div class="col-12">
                                            <asp:Label runat="server" Text="Datos del Paciente" ForeColor="DarkBlue" Font-Bold="true"></asp:Label>
                                        </div>
                                    </div>
                                    <hr />
                                    <div class="form-group">
	                                    <asp:Label runat="server"  class="col-sm-3 control-label no-padding-right" AssociatedControlID="txtNombreDet"> Nombre</asp:Label>
	                                    <div class="col-sm-8">
		                                    <asp:TextBox runat="server" type="text" ID="txtNombreDet" CssClass="form-control" placeholder="Nombre" class="col-xs-10 col-sm-5"  />
	                                    </div>
                                        <div class="col-sm-1 text-right">
                                            <asp:RequiredFieldValidator ID="rfvNombrePacienteDet" runat="server" ErrorMessage="* " ForeColor="Red" ControlToValidate="txtNombreDet" ValidationGroup="vgAddPaciente"></asp:RequiredFieldValidator>
                                        </div>
                                    </div>
                                    <div class="form-group">
	                                    <asp:Label runat="server"  class="col-sm-3 control-label no-padding-right" AssociatedControlID="txtApellidosDet"> Apellidos</asp:Label>
	                                    <div class="col-sm-8">
		                                    <asp:TextBox runat="server" type="text" id="txtApellidosDet" CssClass="form-control"  placeholder="Apellidos" class="col-xs-10 col-sm-5" />
	                                    </div>
                                        <div class="col-sm-1  text-right">
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="* " ForeColor="Red" ControlToValidate="txtApellidosDet" ValidationGroup="vgAddPaciente"></asp:RequiredFieldValidator>
                                        </div>
                                    </div>
                                    <div class="form-group">
	                                    <asp:Label runat="server"  class="col-sm-3 control-label no-padding-right" AssociatedControlID="txtFecNacDet"> Fecha de Nacimiento</asp:Label>
	                                    <div class="col-sm-8">
                                            <table style="width: 100%">
                                                <tr>
                                                    <td style="width: 90%">
                                                        <asp:TextBox runat="server" ID="txtFecNacDet" autocomplete="off" CssClass="form-control" Width="100%"  Enabled="false"/>
                                                    </td>
                                                    <td style="width: 10%">
                                                        <asp:ImageButton ID="imgPopup" ImageUrl="~/Images/ic_action_calendar_month.png" Width="25px" Height="25px" ImageAlign="Bottom" runat="server" />
                                                        <asp:CalendarExtender ID="customCalendarExtender" runat="server" TargetControlID="txtFecNacDet" PopupButtonID="imgPopup"
                                                            CssClass="cal" Format="dd/MM/yyyy" />
                                                    </td>
                                                </tr>
                                            </table>
	                                    </div>
                                        <div class="col-sm-1 text-right">
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="* " ForeColor="Red" ControlToValidate="txtFecNacDet" ValidationGroup="vgAddPaciente"></asp:RequiredFieldValidator>
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <asp:Label runat="server" class="col-sm-3 control-label no-padding-right"  AssociatedControlID="ddlGeneroDet">Genero</asp:Label>
                                        <div class="col-sm-8">
                                            <asp:DropDownList class="form-control" runat="server" ID="ddlGeneroDet">
                                            </asp:DropDownList>
                                        </div>
                                        <div class="col-sm-1 text-right">
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ErrorMessage="* " ForeColor="Red" InitialValue="0" ControlToValidate="ddlGeneroDet" ValidationGroup="vgAddPaciente"></asp:RequiredFieldValidator>
                                        </div>
                                    </div>
                                    <div class="form-group">
	                                    <asp:Label runat="server" class="col-sm-3 control-label no-padding-right" AssociatedControlID="txtNumContactDet"> Número de Contacto</asp:Label>
	                                    <div class="col-sm-8">
                                            <span class="input-icon" style="width:100%">
		                                        <asp:TextBox runat="server" type="text" id="txtNumContactDet" CssClass="form-control" Width="100%" placeholder="Número de Contacto" />
                                                <i class="ace-icon fa fa-mobile blue"></i>
                                            </span>
	                                    </div>
                                        <div class="col-sm-1 text-right">
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ErrorMessage="*" ForeColor="Red" ControlToValidate="txtNumContactDet" ValidationGroup="vgAddPaciente"></asp:RequiredFieldValidator>
                                        </div>
                                    </div>
                                    <div class="form-group">
	                                    <asp:Label runat="server" class="col-sm-3 control-label no-padding-right" AssociatedControlID="txtEmailDet"> Email</asp:Label>
	                                    <div class="col-sm-8">
                                            <span class="input-icon" style="width:100%">
		                                        <asp:TextBox runat="server" type="text" id="txtEmailDet" CssClass="form-control" Width="100%" placeholder="Email" />
                                                <i class="ace-icon fa fa-envelope-o blue"></i>
                                            </span>
	                                    </div>
                                        <div class="col-sm-1 text-right">
                                            <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" Text="* Formato" ValidationGroup="vgAddPaciente" ForeColor="red"
                                                ErrorMessage="Invalid Email" ControlToValidate="txtEmailDet" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"></asp:RegularExpressionValidator>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ErrorMessage="* " ForeColor="Red" ControlToValidate="txtEmailDet" ValidationGroup="vgAddPaciente"></asp:RequiredFieldValidator>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-lg-6 col-md-12">
                                    <div class="row ">
                                        <div class="col-12">
                                            <asp:Label runat="server" Text="Dirección del Paciente" ForeColor="DarkBlue" Font-Bold="true"></asp:Label>
                                        </div>
                                    </div>
                                    <hr />
                                    <div class="form-group">
                                        <asp:Label runat="server"  class="col-sm-3 control-label no-padding-right" AssociatedControlID="txtCalleDet"> Calle</asp:Label>
                                        <div class="col-sm-9">
                                            <asp:TextBox runat="server" type="text" ID="txtCalleDet" CssClass="form-control" placeholder="Calle" class="col-xs-10 col-sm-5" />
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <asp:Label runat="server"  class="col-sm-3 control-label no-padding-right" AssociatedControlID="txtNumeroDet"> Número</asp:Label>
                                        <div class="col-sm-9">
                                            <asp:TextBox runat="server" type="text" ID="txtNumeroDet" CssClass="form-control" placeholder="Número" class="col-xs-10 col-sm-5" />
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <asp:Label runat="server"  class="col-sm-3 control-label no-padding-right" AssociatedControlID="txtCodigoPostal"> Código Postal</asp:Label>
                                        <div class="col-sm-9">
                                            <asp:TextBox runat="server" type="text" ID="txtCodigoPostal" TextMode="Number" AutoPostBack="true"  OnTextChanged="txtCodigoPostal_TextChanged" CssClass="form-control" placeholder="Código Postal" class="col-xs-10 col-sm-5" />
                                            <asp:HiddenField runat="server" ID="intCodigoPostalID" ClientIDMode="Static" />
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <asp:Label runat="server"  class="col-sm-3 control-label no-padding-right" AssociatedControlID="txtEstadoDet">Estado</asp:Label>
                                        <div class="col-sm-9">
                                            <asp:TextBox runat="server" type="text" ID="txtEstadoDet" CssClass="form-control" placeholder="Estado" class="col-xs-10 col-sm-5" />
                                            <asp:HiddenField runat="server" ID="idEstadoDet" ClientIDMode="Static" />
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <asp:Label runat="server"  class="col-sm-3 control-label no-padding-right" AssociatedControlID="txtmunicipioDet">Municipio</asp:Label>
                                        <div class="col-sm-9">
                                            <asp:TextBox runat="server" type="text" ID="txtmunicipioDet"  CssClass="form-control" placeholder="Municipio/Delegación" class="col-xs-10 col-sm-5" />
                                            <asp:HiddenField runat="server" ID="intMunicipioID" ClientIDMode="Static" />
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <asp:Label runat="server"  class="col-sm-3 control-label no-padding-right" AssociatedControlID="ddlColoniaDet">Colonia</asp:Label>
                                        <div class="col-sm-9">
                                            <asp:DropDownList runat="server" type="text" ID="ddlColoniaDet" CssClass="form-control" placeholder="Colonia" class="col-xs-10 col-sm-5" />
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <hr />
                            <div class="row">
                                <div class="col-lg-6 col-md-12">
                                    <div class="row " runat="server" id="divID">
                                        <div class="col-12">
                                            <asp:Label runat="server" Text="Identificaciones" ForeColor="DarkBlue" Font-Bold="true"></asp:Label>
                                        </div>
                                    </div>
                                    <hr />
                                    <div runat="server" id="divIDContenido">
                                        <asp:Panel ID="pnlIDContenido" CssClass="form-group" runat="server">

                                        </asp:Panel>
                                    </div>
                                </div>
                                <div class="col-lg-6 col-md-12">
                                    <div class="row" runat="server" id="divDinamico">
                                        <div class="col-12">
                                            <asp:Label runat="server" Text="Adicionales" ForeColor="DarkBlue" Font-Bold="true"></asp:Label>
                                        </div>
                                    </div>
                                    <hr />
                                    <div runat="server" id="divDinamicoContenido">
                                        <asp:Panel ID="pnlDinamicoContenido"  CssClass="form-group" runat="server">

                                        </asp:Panel>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </ContentTemplate>
                </asp:UpdatePanel>                
            </div>
            <div class="modal-footer">
                <asp:Button runat="server" ID="btnCancelPacienteDet" class="btn btn-default" Text="Cerrar" OnClick="btnCancelPacienteDet_Click" data-dismiss="modal"></asp:Button>
                <asp:Button runat="server" ID="bntAddPacienteDEt" OnClick="bntAddPacienteDEt_Click" Text="Guardar" CssClass="btn btn-primary" ValidationGroup="vgAddPaciente"></asp:Button>
            </div>

            </div>
        </div>
    </div>
    <!-- /modals -->


    <!-- modals -->
    <div class="modal fade bs-example-modal-sm" id="modalObs" tabindex="-1" role="dialog" aria-hidden="true">
        <div class="modal-dialog modal-sm">
            <div class="modal-content">

            <div class="modal-header">
                <button type="button" class="close" data-dismiss="modal" aria-label="Close"><span aria-hidden="true">×</span>
                </button>
                <h4 class="modal-title" id="myModalLabel3">
                    <asp:UpdatePanel runat="server">
                    <ContentTemplate>
                        <asp:Label runat="server" ID="lblTitObs" ForeColor="DarkGreen" Text=""></asp:Label>
                        <asp:HiddenField runat="server" Value="" ID="hfintAdicionalID" />
                    </ContentTemplate>
                </asp:UpdatePanel></h4>
            </div>
            <div class="modal-body">
                <asp:UpdatePanel runat="server">
                    <ContentTemplate>
                        <asp:TextBox runat="server" Text="" TextMode="MultiLine" Height="100px" Width="100%" CssClass="form-control" ID="txtObservaciones"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="rfvObservaciones" runat="server" ErrorMessage="* Campo requerido" Text="* Campo requerido" ForeColor="Red" ControlToValidate="txtObservaciones" ValidationGroup="vgObs"></asp:RequiredFieldValidator>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
            <div class="modal-footer">
                <asp:Button runat="server" ID="btnCancelObs" class="btn btn-default" Text="Cancelar" OnClick="btnCancelObs_Click"></asp:Button>
                <asp:Button runat="server" ID="btnSaveObs" OnClick="btnSaveObs_Click" Text="Guardar" CssClass="btn btn-primary" ValidationGroup="vgObs"></asp:Button>
            </div>

            </div>
        </div>
    </div>
    <!-- /modals -->

    <style type="text/css">

        /* Hiding the checkbox, but allowing it to be focused */
        .badgebox
        {
            opacity: 0;
        }

        .badgebox + .badge
        {
            /* Move the check mark away when unchecked */
            text-indent: -999999px;
            /* Makes the badge's width stay the same checked and unchecked */
	        width: 27px;
        }

        .badgebox:focus + .badge
        {
            /* Set something to make the badge looks focused */
            /* This really depends on the application, in my case it was: */
    
            /* Adding a light border */
            box-shadow: inset 0px 0px 5px;
            /* Taking the difference out of the padding */
        }

        .badgebox:checked + .badge
        {
            /* Move the check mark back when checked */
	        text-indent: 0;
        }
        .completionList {
            border: solid 1px Gray;
            margin: 0px;
            padding: 3px;
            height: 120px;
            overflow: auto;
            background-color: #FFFFFF;
        }

        .listItem {
            color: #191919;
        }

        .itemHighlighted {
            background-color: #ADD6FF;
        }

        .ajax__calendar_today {
            color: Red;
        }

        .ajax__calendar_active {
            color: #004080;
            font-weight: bold;
            background-color: #000;
        }

        .cal .ajax__calendar_header {
            background-color: Silver;
        }

        .cal .ajax__calendar_container {
            background-color: #CEECF5;
        }

        .btn span.glyphicon {
            opacity: 0;
        }

        .btn.active span.glyphicon {
            opacity: 1;
        }
    </style>

    <script src="assets/js/bootstrap.min.js"></script>
    <!-- page specific plugin scripts -->
    <script src="assets/js/jquery-ui.custom.min.js"></script>
    <script src="assets/js/jquery.ui.touch-punch.min.js"></script>
    <script src="assets/js/moment.min.js"></script>
    <script src="assets/js/bootbox.js"></script>
    <!-- inline scripts related to this page -->

    <script src="assets/js/ace-elements.min.js"></script>
	<script src="assets/js/ace.min.js"></script>
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

        function autoCompleteEx_ItemSelected(sender, args) {
            __doPostBack(sender.get_element().name, "");
        }
    </script>

</asp:Content>
