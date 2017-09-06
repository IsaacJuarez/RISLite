<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="frmConfiguraciones.aspx.cs" Inherits="Fuji.RISLite.Site.frmConfiguraciones" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="assets/css/bootstrap.min.css" rel="stylesheet" />
    <script src="assets/jquery/dist/jquery.min.js"></script>
    <script src="assets/js/bootstrap.min.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="page-content">
        <div class="page-header">
		    <h1>
			    Configuración 
			    <small>
				    <i class="ace-icon fa fa-angle-double-right"></i>
				    General
			    </small>
		    </h1>
	    </div><!-- /.page-header -->
        <div class="row">
            <div class="col-lg-6 col-md-6 col-sm-12">
                <div class="widget-box">
                    <div class="widget-header widget-header-flat widget-header-small">
						<h5 class="widget-title">
							<i class="ace-icon fa fa-user-md"></i>
							Sistema
						</h5>
                    </div>
                    <div class="widget-body">
                        <div class="widget-main">
                            <div class="widget-box">
								<div class="widget-header widget-header-flat widget-header-small">
									<h5 class="widget-title">
										<i class="ace-icon fa fa-signal"></i>
										Vistas por Tipo de Usuario
									</h5>
                                </div>
                                <div class="widget-body">
                                    <div class="row">
                                        <div class="col-lg-6 text-right">
                                        </div>
                                        <div class="col-lg-6 text-right">
                                            <asp:DropDownList runat="server" CssClass="form-control" ID="ddlTipoUsuario" AutoPostBack="true" OnSelectedIndexChanged="ddlTipoUsuario_SelectedIndexChanged"></asp:DropDownList>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-lg-12 col-md-12 ">
                                            <asp:UpdatePanel runat="server">
                                                <ContentTemplate>
                                                    <asp:Panel runat="server">
                                                        <asp:GridView ID="grvVista" runat="server" AllowPaging="true" CssClass="table table-striped table-bordered"
                                                            PageSize="10" AutoGenerateColumns="false" OnRowDataBound="grvVista_RowDataBound" Font-Size="10px"
                                                            OnPageIndexChanging="grvVista_PageIndexChanging" DataKeyNames="intRELUsuarioBotonID"
                                                            OnRowCommand="grvVista_RowCommand"
                                                            EmptyDataText="No hay resultado bajo el criterio de búsqueda.">
                                                            <Columns>
                                                                <asp:BoundField DataField="intRELUsuarioBotonID" HeaderText="ID" ReadOnly="true" ItemStyle-CssClass="hidden-md" HeaderStyle-CssClass="hidden-md"/>
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
                                                    </asp:Panel>
                                                </ContentTemplate>
                                            </asp:UpdatePanel>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <div class="col-lg-6 col-md-6 col-sm-12">
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
	                                            <label class="col-sm-3 control-label no-padding-right" for="form-field-1"> Nombre de Vista</label>
	                                            <div class="col-sm-9">
		                                            <input type="text" id="form-field-12" placeholder="Nombre" class="col-xs-10 col-sm-5" />
	                                            </div>
                                            </div>
                                        </div>
                                        <div class="row">
                                            <div class="form-group">
	                                            <label class="col-sm-3 control-label no-padding-right" for="form-field-1"> Identificador de Vista</label>
	                                            <div class="col-sm-9">
		                                            <input type="text" id="form-field-13" placeholder="Identificador" class="col-xs-10 col-sm-5" />
	                                            </div>
                                            </div>
                                        </div>
                                        <div class="row">
                                            <div class="form-group">
	                                            <label class="col-sm-3 control-label no-padding-right" for="form-field-1"> Icono de Vista</label>
	                                            <div class="col-sm-9">
		                                            <input type="text" id="form-field-14" placeholder="Icono" class="col-xs-10 col-sm-5" />
	                                            </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="text-right">
                                        <asp:Button runat="server" ID="btnCancelarVista" Text="Cancelar" CssClass="btn btn-danger" OnClick="btnCancelarVista_Click" />
                                        <asp:Button runat="server" ID="btnAgregar" Text="Agregar" CssClass="btn btn-success" OnClick="btnAgregar_Click"/>
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
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="text-right">
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

        <div class="row">
            <div class="col-lg-6 col-md-6 col-sm-12">
                <div class="widget-box">
                    <div class="widget-header widget-header-flat widget-header-small">
						<h5 class="widget-title">
							<i class="ace-icon fa fa-medkit"></i>
							Agenda
						</h5>
                    </div>
                    <div class="widget-body">
                        <div class="widget-main">
                        </div>
                    </div>
                </div>
            </div>
            <div class="col-lg-6 col-md-6 col-sm-12">
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
    </script>
</asp:Content>
