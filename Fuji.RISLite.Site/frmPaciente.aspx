<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="frmPaciente.aspx.cs" Inherits="Fuji.RISLite.Site.frmPaciente" Culture="es-MX" UICulture="Auto" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <telerik:RadAjaxManager runat="server" ID="radScriptManager1">
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="grvPacientes">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grvPacientes" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="txtBusqueda">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grvPacientes" />
                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>
    </telerik:RadAjaxManager>

    <div class="page-content">
        <div class="page-header">
            <div class="messagealert" id="alert_container"></div>
		    <h1>
			    Pacientes
			    <small>
				    <i class="ace-icon fa fa-angle-double-right"></i>
				    Búsqueda
			    </small>
		    </h1>
	    </div><!-- /.page-header -->
        <div class="row">
            <div class="col-lg-12 col-md-12 col-sm-12">
                <div class="row">
                    <div class="col-lg-6 col-md-6 col-sm-6">
                        <div class="form-search">      
                            <span class="input-icon">
                                <asp:TextBox Text="" runat="server" ID="txtBusqueda" CssClass="form-search" OnTextChanged="txtBusqueda_TextChanged" AutoPostBack="true"></asp:TextBox>
                                <i runat="server" id="imgSearch" class="ace-icon fa fa-search nav-search-icon"></i>
                            </span>
                        </div>
                    </div>
                    <div class="col-lg-6 col-md-6 col-sm-6"></div>
                </div>
            </div>
        </div><!-- /.row -->
        <hr />
        <div class="row">
            <div class="col-lg-12 col-md-12 col-sm-12">
                <div class="row">
                    <div class="col-lg-3 col-md-1 col-sm-1">
                    </div>
                    <div class="col-lg-6 col-md-10 col-sm-10">
                        <telerik:RadAjaxPanel runat="server" ID="radAjaxPanelPacientes" OnAjaxRequest="radAjaxPanelPacientes_AjaxRequest">
                            <asp:GridView ID="grvPacientes" runat="server" AllowPaging="true" CssClass="table table-striped table-bordered"
                                PageSize="10" AutoGenerateColumns="false" OnRowDataBound="grvPacientes_RowDataBound" Font-Size="10px"
                                OnPageIndexChanging="grvPacientes_PageIndexChanging" DataKeyNames="intPacienteID"
                                OnRowCommand="grvPacientes_RowCommand"
                                EmptyDataText="No hay resultado bajo el criterio de búsqueda.">
                                <Columns>
                                    <%--<asp:BoundField DataField="intPacienteID" HeaderText="ID" ReadOnly="true" ItemStyle-CssClass="hidden-md" HeaderStyle-CssClass="hidden-md"/>--%>
                                    <asp:BoundField DataField="vchApellidos"  HeaderText="NSS" ReadOnly="true" />
                                    <asp:BoundField DataField="vchNombre"  HeaderText="Nombre" ReadOnly="true" />
                                    <asp:BoundField DataField="datFechaNac" dataformatstring="{0:dd/MM/yyyy}" HeaderText="Fecha de Nacimiento" ReadOnly="true" />
                                    <asp:BoundField DataField="vchNombreSitio" HeaderText="Sitio" ReadOnly="true" />
                                    <asp:TemplateField HeaderText="Detalle" ItemStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="imbDetalle" runat="server" BackColor="Transparent"  Height="25px" Width="25px" 
                                                CommandArgument='<%#Eval("intPacienteID") %>' CommandName="Detalle" ToolTip="Ver el detalle del paciente" >
                                                <i class="fa fa-user" aria-hidden="true" title="Detalle" style="font-size:25px;"></i>
                                            </asp:LinkButton>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Nueva Cita" ItemStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="imbCita" runat="server" BackColor="Transparent"  Height="25px" Width="25px" 
                                                CommandArgument='<%#Eval("intPacienteID") %>' CommandName="CrearCita" ToolTip="Realizar una cita" >
                                                <i class="fa fa-calendar-plus-o" aria-hidden="true" title="Crear Cita" style="font-size:25px;"></i>
                                            </asp:LinkButton>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Estudios" ItemStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="imbEstudios" runat="server" BackColor="Transparent"  Height="25px" Width="25px" 
                                                CommandArgument='<%#Eval("intPacienteID") %>' CommandName="Estudios" ToolTip="Lista de Estudios del paciente" >
                                                <i class="fa fa-server" aria-hidden="true" title="Lista de Estudios del paciente" style="font-size:25px;"></i>
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
                                        ToolTip="Página Anterior" CommandArgument="Prev" CssClass="previous" style="background: url(../Images/previous.gif)" />
                                    <asp:Button ID="btnBandeja_II" runat="server" CommandName="Page" CausesValidation="false"
                                        ToolTip="Página Siguiente" CommandArgument="Next" CssClass="next" style="background: url(../Images/next.gif)"/>
                                </PagerTemplate>
                                <HeaderStyle CssClass="headerstyle" />
                                <FooterStyle CssClass="text-center" />
                                <PagerStyle CssClass="text-center" />
                            </asp:GridView>
                        </telerik:RadAjaxPanel>
                    </div>
                    <div class="col-lg-3 col-md-1 col-sm-1">
                    </div>
                </div>
            </div>
        </div><!-- /.row -->
	</div><!-- /.page-content -->

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
		                                    <asp:TextBox runat="server" type="text" ID="txtNombreDet" CssClass="form-control" placeholder="Nombre" Enabled="false" class="col-xs-10 col-sm-5"  />
	                                    </div>
                                        <div class="col-sm-1 text-right">
                                            <asp:RequiredFieldValidator ID="rfvNombrePacienteDet" runat="server" ErrorMessage="* " ForeColor="Red" ControlToValidate="txtNombreDet" ValidationGroup="vgAddPaciente"></asp:RequiredFieldValidator>
                                        </div>
                                    </div>
                                    <div class="form-group">
	                                    <asp:Label runat="server"  class="col-sm-3 control-label no-padding-right" AssociatedControlID="txtApellidosDet"> Apellidos</asp:Label>
	                                    <div class="col-sm-8">
		                                    <asp:TextBox runat="server" type="text" id="txtApellidosDet" CssClass="form-control"  Enabled="false" placeholder="Apellidos" class="col-xs-10 col-sm-5" />
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
                                                        <asp:ImageButton ID="imgPopup" ImageUrl="~/Images/ic_action_calendar_month.png" Width="25px" Height="25px" ImageAlign="Bottom" runat="server" Visible="false" />
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
                                            <asp:DropDownList class="form-control" runat="server" Enabled="false" ID="ddlGeneroDet">
                                                <%--<asp:ListItem value="0">Seleccionar</asp:ListItem>
		                                        <asp:ListItem value="1">Masculino</asp:ListItem>
		                                        <asp:ListItem value="2">Femenino</asp:ListItem>
		                                        <asp:ListItem value="3">No especificado</asp:ListItem>--%>
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
		                                        <asp:TextBox runat="server" type="text" id="txtNumContactDet" CssClass="form-control" Enabled="false" Width="100%" placeholder="Número de Contacto" />
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
		                                        <asp:TextBox runat="server" type="text" id="txtEmailDet" CssClass="form-control" Enabled="false" Width="100%" placeholder="Email" />
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
                                            <asp:TextBox runat="server" type="text" ID="txtCalleDet" CssClass="form-control" Enabled="false" placeholder="Calle" class="col-xs-10 col-sm-5" />
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <asp:Label runat="server"  class="col-sm-3 control-label no-padding-right" AssociatedControlID="txtNumeroDet"> Número</asp:Label>
                                        <div class="col-sm-9">
                                            <asp:TextBox runat="server" type="text" ID="txtNumeroDet" CssClass="form-control" Enabled="false" placeholder="Número" class="col-xs-10 col-sm-5" />
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <asp:Label runat="server"  class="col-sm-3 control-label no-padding-right" AssociatedControlID="txtCodigoPostal"> Código Postal</asp:Label>
                                        <div class="col-sm-9">
                                            <asp:TextBox runat="server" type="text" ID="txtCodigoPostal" TextMode="Number" Enabled="false" AutoPostBack="true"  OnTextChanged="txtCodigoPostal_TextChanged" CssClass="form-control" placeholder="Código Postal" class="col-xs-10 col-sm-5" />
                                            <asp:HiddenField runat="server" ID="intCodigoPostalID" ClientIDMode="Static" />
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <asp:Label runat="server"  class="col-sm-3 control-label no-padding-right" AssociatedControlID="txtEstadoDet">Estado</asp:Label>
                                        <div class="col-sm-9">
                                            <asp:TextBox runat="server" type="text" ID="txtEstadoDet" CssClass="form-control" Enabled="false" placeholder="Estado" class="col-xs-10 col-sm-5" />
                                            <asp:HiddenField runat="server" ID="idEstadoDet" ClientIDMode="Static" />
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <asp:Label runat="server"  class="col-sm-3 control-label no-padding-right" AssociatedControlID="txtmunicipioDet">Municipio</asp:Label>
                                        <div class="col-sm-9">
                                            <asp:TextBox runat="server" type="text" ID="txtmunicipioDet"  CssClass="form-control" Enabled="false" placeholder="Municipio/Delegación" class="col-xs-10 col-sm-5" />
                                            <asp:HiddenField runat="server" ID="intMunicipioID" ClientIDMode="Static" />
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <asp:Label runat="server"  class="col-sm-3 control-label no-padding-right" AssociatedControlID="ddlColoniaDet">Colonia</asp:Label>
                                        <div class="col-sm-9">
                                            <asp:DropDownList runat="server" type="text" ID="ddlColoniaDet" CssClass="form-control" Enabled="false" placeholder="Colonia" class="col-xs-10 col-sm-5" />
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
                <asp:Button runat="server" ID="btnCancelPacienteDet" class="btn btn-default" Text="Cerrar" OnClick="btnCancelPacienteDet_Click"></asp:Button>
            </div>

            </div>
        </div>
    </div>
    <!-- /modals -->


    <!-- modals -->
    <div class="modal fade bs-example-modal-sm" id="mdlEstudios" tabindex="-1" role="dialog" aria-hidden="true">
        <div class="modal-dialog" style="width:70%">
            <div class="modal-content">

            <div class="modal-header">
                <button type="button" class="close" data-dismiss="modal" aria-label="Close"><span aria-hidden="true">×</span>
                </button>
                <h4 class="modal-title" id="mdlEstudiosLabel">Estudios</h4>
            </div>
            <div class="modal-body">
                <asp:UpdatePanel runat="server">
                    <ContentTemplate>
                        <div class="form-horizontal" role="form">
                            <div class="row">
                                <div class="col-lg-12 col-md-12">
                                    <asp:Label runat="server" ID="lblNombrePaciente" Text="" ForeColor="DarkGreen" Font-Bold="true"></asp:Label>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-lg-12 col-md-12">
                                    <asp:Panel runat="server">
                                        <asp:GridView ID="grvEstudios" runat="server" AllowPaging="true" CssClass="table table-striped table-bordered"
                                            PageSize="10" AutoGenerateColumns="false" OnRowDataBound="grvEstudios_RowDataBound" Font-Size="10px"
                                            OnPageIndexChanging="grvEstudios_PageIndexChanging" DataKeyNames="intPacienteID"
                                            OnRowCommand="grvEstudios_RowCommand"
                                            EmptyDataText="No hay resultado bajo el criterio de búsqueda.">
                                            <Columns>
                                                <asp:BoundField DataField="intPacienteID" HeaderText="Estudio" ReadOnly="true" ItemStyle-CssClass="hidden-md" HeaderStyle-CssClass="hidden-md"/>
                                                <asp:BoundField DataField="datFechaCita" DataFormatString="{0:dd-MM-yyyy hh:mm tt}" HeaderText="Fecha de Estudio" ReadOnly="true" />
                                                <asp:BoundField DataField="vchPrestacion"  HeaderText="Estudio" ReadOnly="true" />
                                                <asp:BoundField DataField="vchEstatusCita"  HeaderText="Estudio" ReadOnly="true" />
                                            </Columns>
                                            <PagerTemplate>
                                                <asp:Label ID="lblTemplate" runat="server" Text="Muestra Filas: " CssClass="Label" />
                                                <asp:DropDownList ID="ddlBandejaEst" runat="server" AutoPostBack="true" CausesValidation="false"
                                                    Enabled="true" OnSelectedIndexChanged="ddlBandejaEst_SelectedIndexChanged">
                                                        <asp:ListItem Value="10" />
                                                        <asp:ListItem Value="15" />
                                                        <asp:ListItem Value="20" />
                                                </asp:DropDownList>
                                                &nbsp;Página
                                                <asp:TextBox ID="txtBandejaEst" runat="server" AutoPostBack="true" OnTextChanged="txtBandejaEst_TextChanged"
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
                                </div>
                            </div>
                        </div>
                    </ContentTemplate>
                </asp:UpdatePanel>                
            </div>
            <div class="modal-footer">
                <asp:Button runat="server" ID="btnCancelEstudios" class="btn btn-default" Text="Cerrar" OnClick="btnCancelEstudios_Click"></asp:Button>
            </div>

            </div>
        </div>
    </div>
    <!-- /modals -->
    <style type="text/css"> 
        .completionList {
            border:solid 1px Gray;
            margin:0px;
            padding:3px;
            height: 120px;
            overflow:auto;
            background-color: #FFFFFF;     
        } 
        .listItem {
            color: #191919;
        } 
        .itemHighlighted {
            background-color: #ADD6FF;       
        }
        .ajax__calendar_today
        {
            color:Red;    
        }

        .ajax__calendar_active  
        {
            color: #004080;
            font-weight: bold;
            background-color: #000;
        }

        .cal .ajax__calendar_header
        {
            background-color: Silver;
        }

        .cal .ajax__calendar_container
        {
            background-color: #CEECF5;
        }
        .btn span.glyphicon {    			
	        opacity: 0;				
        }
        .btn.active span.glyphicon {				
	        opacity: 1;				
        }
    </style>

    <script type="text/javascript">
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
</asp:Content>
