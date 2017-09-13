<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="frmConfiguracion.aspx.cs" Inherits="Fuji.RISLite.Site.frmConfiguracion" %>
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
    <div class="page-content">
        <div class="page-header">
            <div class="messagealert" id="alert_container"></div>
		    <h1>
			    Configuración
			    <small>
				    <i class="ace-icon fa fa-angle-double-right"></i>
				    Sistema
			    </small>
		    </h1>
	    </div><!-- /.page-header -->
        <div class="row">
            <div class="col-lg-12 col-md-12 col-sm-12">
                <div class="tabbable tabs-left" id="Tabs">
				    <ul class="nav nav-tabs" id="myTab3">
					    <li class="active">
						    <a data-toggle="tab" href="#sistemaConfig">
							    <i class="pink ace-icon fa fa-cog bigger-110"></i>
							    Sistema
						    </a>
					    </li>

					    <li>
						    <a data-toggle="tab" href="#emailConfig">
							    <i class="blue ace-icon fa fa-envelope-o bigger-110"></i>
							    Email Sistema
						    </a>
					    </li>

                        <li>
                            <a data-toggle="tab" href="#userConfig">
                                <i class="red ace-icon fa fa-user"></i>
							    Usuarios del Sistema
                            </a>
                        </li>

                        <li>
                            <a data-toggle="tab" href="#AditionalConfig">
                                <i class="green ace-icon fa fa-wrench"></i>
							    Variables adicionales
                            </a>
                        </li>

                        <li>
                            <a data-toggle="tab" href="#catalogConfig">
                                <i class="badge-yellow ace-icon fa fa-tasks"></i>
							    Catálogos
                            </a>
                        </li>

                        <li>
                            <a data-toggle="tab" href="#ModalityConfig">
                                <i class="purple ace-icon fa fa-cogs"></i>
							    Modalidades, Prestaciones y Equipos
                            </a>
                        </li>

				    </ul>

				    <div class="tab-content">
					    <div id="sistemaConfig" class="tab-pane in active">
						    <div class="widget-box">
								<div class="widget-header">
									<h4 class="widget-title">Configuración General para el Sitio</h4>

								</div>

								<div class="widget-body">
									<div class="widget-main">
                                        <div class="row form-group ">
                                            <div class="col-lg-5 col-md-12 col-sm-12 col-xs-12 text-right">
                                                <asp:RequiredFieldValidator runat="server" ID="rfvNameSite" ErrorMessage="* Campo requerido." ForeColor="Red" Text="* Campo requerido" ControlToValidate="txtNombreSitio" ValidationGroup="vgConfigSistema"></asp:RequiredFieldValidator>
                                                <asp:Label runat="server" Text="Nombre del Sitio" AssociatedControlID="txtNombreSitio"></asp:Label>
                                            </div>
                                            <div class="col-lg-7 col-md-12 col-sm-12 col-xs-12">
                                               <asp:TextBox runat="server" Text="" ID="txtNombreSitio" placeholder="Nombre del Sitio" CssClass="form-control"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="row form-group ">
                                            <div class="col-lg-5 col-md-12 col-sm-12 col-xs-12 text-right">
                                               <asp:Label runat="server" Text="Dirección del Sitio" AssociatedControlID="txtDireccionSitio"></asp:Label>
                                            </div>
                                            <div class="col-lg-7 col-md-12 col-sm-12 col-xs-12">
                                               <asp:TextBox runat="server" Text="" ID="txtDireccionSitio" placeholder="Dirección del Sitio" CssClass="form-control"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="row form-group ">
                                            <div class="col-lg-5 col-md-12 col-sm-12 col-xs-12 text-right">
                                               <asp:Label runat="server" Text="Logo del Sitio" AssociatedControlID="fuLogo"></asp:Label>
                                            </div>
                                            <div class="col-lg-7 col-md-12 col-sm-12 col-xs-12">
                                                <div class="row">
                                                    <div class="col-lg-6 col-md-6 col-sm-6">
                                                        <asp:FileUpload ID="fuLogo" runat="server"  CssClass="btn btn-primary" accept=".png,.jpg,.jpeg,.gif"/>
                                                    </div>
                                                    <div class="col-lg-6 col-md-6 col-sm-6">
                                                        <asp:Image ID="imgLogo" runat="server" Width="250px" Height="150px" Visible="false" />
                                                    </div>
                                                </div>                                                
                                            </div>
                                        </div> 
                                        <div class="row form-group ">
                                            <div class="col-lg-5 col-md-12 col-sm-12 col-xs-12 text-right">
                                               <asp:Label runat="server" Text="Versión del Sistema" AssociatedControlID="txtPathRepositorio"></asp:Label>
                                            </div>
                                            <div class="col-lg-7 col-md-12 col-sm-12 col-xs-12">
                                               <asp:TextBox runat="server" Text="" ID="txtPathRepositorio"  placeholder="Versión dels Sistema" CssClass="form-control"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="row form-group">
                                            <div  class="col-lg-12 text-right">
                                                <asp:Button ID="btnSaveConfig" runat="server" Text="Guardar" CssClass="btn btn-success" OnClick="btnSaveConfig_Click" ValidationGroup="vgConfigSistema" />
                                            </div>
                                        </div>
                                    </div>
                                </div>
						    </div>
					    </div>

					    <div id="emailConfig" class="tab-pane">
						    <div class="widget-box">
								<div class="widget-header">
									<h4 class="widget-title">Configuración de la cuenta de correo</h4>

								</div>

								<div class="widget-body">
									<div class="widget-main">
                                        <div class="row form-group ">
                                            <div class="col-lg-5 col-md-12 col-sm-12 col-xs-12 text-right">
                                                <asp:RequiredFieldValidator runat="server" ID="rfvEmailSistema" ErrorMessage="Campo requerido" ControlToValidate="txtEmailSistema" ForeColor="Red" ValidationGroup="vgEmailSistema"></asp:RequiredFieldValidator>
                                                <asp:RegularExpressionValidator ID="revEmail" runat="server" Text="* Formato Email incorrecto" ValidationGroup="vgEmailSistema" ForeColor="red"
                                                ErrorMessage="Invalid Email" ControlToValidate="txtEmailSistema" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"></asp:RegularExpressionValidator>
                                                <asp:Label runat="server" Text="Cuenta de correo" AssociatedControlID="txtEmailSistema"></asp:Label>
                                            </div>
                                            <div class="col-lg-7 col-md-12 col-sm-12 col-xs-12">
                                               <asp:TextBox runat="server" Text="" ID="txtEmailSistema"  placeholder="Email: correo@dominio.mx" CssClass="form-control"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="row form-group ">
                                            <div class="col-lg-5 col-md-12 col-sm-12 col-xs-12 text-right">
                                                <asp:RequiredFieldValidator ID="rfvPassword" runat="server" Text="* Capturar contraseña." ForeColor="Red"  ControlToValidate="txtPasswordSistema" ValidationGroup="vgEmailSistema"></asp:RequiredFieldValidator>¿
                                                <asp:Label runat="server" Text="Contraseña del correo" AssociatedControlID="txtPasswordSistema"></asp:Label>                                                
                                            </div>
                                            <div class="col-lg-7 col-md-12 col-sm-12 col-xs-12">
                                               <asp:TextBox runat="server" Text="" TextMode="Password"   placeholder="Password" ID="txtPasswordSistema" CssClass="form-control"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="row form-group ">
                                            <div class="col-lg-5 col-md-12 col-sm-12 col-xs-12 text-right">
                                                <asp:RequiredFieldValidator ID="rfvHost" runat="server" Text="* Capturar Host." ForeColor="Red"  ControlToValidate="txtHost" ValidationGroup="vgEmailSistema"></asp:RequiredFieldValidator>
                                                <asp:Label runat="server" Text="Host de Correo" AssociatedControlID="txtHost"></asp:Label>
                                            </div>
                                            <div class="col-lg-7 col-md-12 col-sm-12 col-xs-12">
                                                <asp:TextBox runat="server" Text="" ID="txtHost"  placeholder="Host ejemplo: smtp.gmail.com" CssClass="form-control"></asp:TextBox>
                                            </div>
                                        </div> 
                                        <div class="row form-group ">
                                            <div class="col-lg-5 col-md-12 col-sm-12 col-xs-12 text-right">
                                                <asp:RequiredFieldValidator runat="server" ID="rfvPuertoCorreo" ErrorMessage="Campo requerido" ControlToValidate="txtPortCorreo" ForeColor="Red" ValidationGroup="vgEmailSistema"></asp:RequiredFieldValidator>
                                                <asp:Label runat="server" Text="Puerto Correo" AssociatedControlID="txtPortCorreo"></asp:Label>                                                
                                            </div>
                                            <div class="col-lg-7 col-md-12 col-sm-12 col-xs-12">
                                                <asp:TextBox runat="server" Text="" ID="txtPortCorreo" placeholder="Puerto (solo números)" TextMode="Number" CssClass="form-control"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="row form-group ">
                                            <div class="col-lg-5 col-md-12 col-sm-12 col-xs-12 text-right">
                                               <asp:Label runat="server" Text="SSL" ></asp:Label>
                                            </div>
                                            <div class="col-lg-7 col-md-12 col-sm-12 col-xs-12">
                                                <label>
													<input runat="server" id="chkSSL" name="switch-field-1" class="ace ace-switch" type="checkbox" />
													<span class="lbl" data-lbl="SI &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;NO"></span>
												</label>
                                            </div>
                                        </div>
                                        <div class="row form-group">
                                            <div  class="col-lg-12 text-right">
                                                <asp:Button ID="btnSaveEmailSistema" runat="server" Text="Guardar" CssClass="btn btn-success" OnClick="btnSaveEmailSistema_Click" ValidationGroup="vgEmailSistema" />
                                            </div>
                                        </div>
                                    </div>
                                </div>
						    </div>
					    </div>

                        <div id="userConfig" class="tab-pane">
						    <div class="widget-box">
								<div class="widget-header">
									<h4 class="widget-title">Administración de Usuarios</h4>

								</div>

								<div class="widget-body">
									<div class="widget-main">
                                        <div class="row form-group">
                                            <div class="col-lg-4 col-sm-12">
                                                <div class="row">
                                                    <div class="col-lg-12">
                                                        <asp:Label runat="server" Text="Nombre de Usuario" AssociatedControlID="txtNombre" Width="100%"></asp:Label>
                                                        <asp:RequiredFieldValidator ID="rfvNombreUser" runat="server" Text="* Capturar Nombre." ForeColor="Red"  ControlToValidate="txtNombre" ValidationGroup="vgAddUser"></asp:RequiredFieldValidator>
                                                    </div>
                                                </div>
                                                <div class="row">
                                                    <div class="col-lg-12">
                                                        <asp:TextBox runat="server" ID="txtNombre" Text="" CssClass="form-control" placeholder="Nombre de Usuario"></asp:TextBox>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="col-lg-3 col-sm-12">
                                                <div class="row">
                                                    <div class="col-lg-12">
                                                        <asp:Label runat="server" Text="Usuario" AssociatedControlID="txtUsuario" Width="100%"></asp:Label>
                                                        <asp:RequiredFieldValidator ID="rfvUsuario" runat="server" Text="* Capturar Usuario." ForeColor="Red"  ControlToValidate="txtUsuario" ValidationGroup="vgAddUser"></asp:RequiredFieldValidator>
                                                    </div>
                                                </div>
                                                <div class="row">
                                                    <div class="col-lg-12">
                                                        <asp:TextBox runat="server" ID="txtUsuario" Text="" CssClass="form-control" placeholder="Usuario"></asp:TextBox>
                                                     </div>
                                                </div>
                                            </div>
                                            <div class="col-lg-4 col-sm-12">
                                                <div class="row">
                                                    <div class="col-lg-12">
                                                        <asp:Label runat="server" Text="Tipo de Usuario" AssociatedControlID="ddlTipoUsuario" Width="100%"></asp:Label>
                                                        <asp:RequiredFieldValidator ID="rfvTipoUsuario" runat="server" Text="* Seleccionar tipo de usuario." ForeColor="Red" InitialValue="0"  ControlToValidate="ddlTipoUsuario" ValidationGroup="vgAddUser"></asp:RequiredFieldValidator>
                                                    </div>
                                                </div>
                                                <div class="row">
                                                    <div class="col-lg-12">
                                                        <asp:DropDownList ID="ddlTipoUsuario" runat="server" CssClass="form-control" ></asp:DropDownList>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="col-lg-1 col-sm-12">
                                                <div class="row">
                                                    <div class="col-lg-12">
                                                    </div>
                                                </div>
                                                <div class="row">
                                                    <div class="col-lg-12 text-right">
                                                        <asp:Button runat="server" ID="btnAgregar" CssClass="btn btn-success" Text="Agregar" OnClick="btnAgregar_Click"  ValidationGroup="vgAddUser"/>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                        <hr />
                                        <div class="row ">
                                            <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12">
                                                <asp:UpdatePanel runat="server">
                                                    <ContentTemplate>
                                                        <asp:Panel runat="server">
                                                            <asp:GridView ID="grvUsuario" runat="server" AllowPaging="true" CssClass="table table-striped table-bordered"
                                                                    PageSize="10" AutoGenerateColumns="false" OnRowDataBound="grvUsuario_RowDataBound" Font-Size="10px"
                                                                    OnPageIndexChanging="grvUsuario_PageIndexChanging" DataKeyNames="intUsuarioID" OnRowCancelingEdit="grvUsuario_RowCancelingEdit"
                                                                    OnRowCommand="grvUsuario_RowCommand" OnRowEditing="grvUsuario_RowEditing" OnRowUpdating="grvUsuario_RowUpdating"
                                                                    EmptyDataText="No hay resultado bajo el criterio de búsqueda.">
                                                                    <Columns>
                                                                        <asp:BoundField DataField="intUsuarioID" HeaderText="ID" ReadOnly="true" ItemStyle-CssClass="hidden-md" HeaderStyle-CssClass="hidden-md"/>
                                                                        <asp:TemplateField HeaderText="Nombre">
                                                                            <ItemTemplate>
                                                                                <asp:Label runat="server" ID="lblNombreUsuario" Text='<%#Eval("vchNombre") %>' />
                                                                            </ItemTemplate>
                                                                            <EditItemTemplate>
                                                                                    <asp:TextBox ID="txtNombreUsuario" width="100%"  runat="server" Text='<%#Eval("vchNombre") %>'/>
                                                                            </EditItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="Usuario">
                                                                            <ItemTemplate>
                                                                                <asp:Label runat="server" ID="lblUsuario" Text='<%#Eval("vchUsuario") %>' />
                                                                            </ItemTemplate>
                                                                            <EditItemTemplate>
                                                                                    <asp:TextBox ID="txtUsuario" width="100%"  runat="server" Text='<%#Eval("vchUsuario") %>'/>
                                                                            </EditItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:BoundField DataField="vchTipoUsuario"  HeaderText="Tipo de Usuario" ReadOnly="true" />
                                                                        <asp:TemplateField HeaderText="Editar" ItemStyle-HorizontalAlign="Center">
                                                                            <ItemTemplate>      
                                                                                <asp:LinkButton ID="btnVisualizar" CausesValidation="false" CommandName="Edit" runat="server">
                                                                                    <i class="fa fa-pencil" aria-hidden="true" title="Editar" style="font-size:25px;"></i>
                                                                                </asp:LinkButton>
                                                                            </ItemTemplate>
                                                                            <EditItemTemplate>
                                                                                <asp:LinkButton ID="btnUpdateVarPacinete" runat="server" CommandName="Update"  Text="Actualizar">
                                                                                    <i class="fa fa-floppy-o" aria-hidden="true"  title="Actualizar" style="font-size:25px;"></i>
                                                                                </asp:LinkButton>
                                                                                <asp:LinkButton ID="bntCancelEditPaciente" runat="server" CommandName="Cancel"  Text="Cancelar">
                                                                                    <i class="fa fa-ban" aria-hidden="true" title="Cancelar" style="font-size:25px;"></i>
                                                                                </asp:LinkButton>
                                                                            </EditItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="Estatus" ItemStyle-HorizontalAlign="Center">
                                                                            <ItemTemplate>
                                                                                <asp:ImageButton ID="imbEstatus" runat="server" BackColor="Transparent"  Height="25px" Width="25px" 
                                                                                    CommandArgument='<%#Eval("intUsuarioID") %>' CommandName="Estatus" ToolTip="Cambia el estatus del Sitio" />
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

                        <div id="AditionalConfig" class="tab-pane">
						    <div class="widget-box">
								<div class="widget-header">
									<h4 class="widget-title">Administración de variables adicionales</h4>
								</div>

								<div class="widget-body">
									<div class="widget-main">
                                        <div class="row">
                                            <div class="col-lg-6 col-md-12 col-sm-12">
                                                <div class="widget-box">
								                    <div class="widget-header">
									                    <h4 class="widget-title">Paciente</h4>
								                    </div>

								                    <div class="widget-body">
									                    <div class="widget-main">
                                                            <div class="row form-group">
                                                                <div class="col-lg-12 col-md-12 col-sm-12">
                                                                    <div class="row">
                                                                        <div class="col-lg-12">
                                                                            <asp:Label runat="server" Text="Nombre de la Variable" AssociatedControlID="txtAddVarPac" Width="100%"></asp:Label>
                                                                            <asp:RequiredFieldValidator ID="rfvAddVarAdiPac" runat="server" ValidationGroup="vgAddVarAdiPac" ErrorMessage="* Campo requerido" ControlToValidate="txtAddVarPac" Text="* Campo requerido" ForeColor="Red"></asp:RequiredFieldValidator>
                                                                        </div>
                                                                    </div>
                                                                    <div class="row">
                                                                        <div class="col-lg-12">
                                                                            <asp:TextBox runat="server" Text="" CssClass="form-control" ID="txtAddVarPac" Width="100%" placeholder="Nombre de la Variable"></asp:TextBox>
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                            <div class="row form-group">
                                                                <div class="col-lg-12 col-md-12 col-sm-12 text-right">
                                                                    <asp:Button runat="server" ID="btnAddVarPac" Text="Guardar" OnClick="btnAddVarPac_Click" CssClass="btn btn-primary" ValidationGroup="vgAddVarAdiPac"/>
                                                                </div>
                                                            </div>
                                                            <div class="row">
                                                                <asp:UpdatePanel runat="server">
                                                                    <ContentTemplate>
                                                                        <asp:Panel runat="server">
                                                                            <asp:GridView ID="grvAddPaciente" runat="server" AllowPaging="true" CssClass="table table-striped table-bordered"
                                                                                    PageSize="10" AutoGenerateColumns="false" OnRowDataBound="grvAddPaciente_RowDataBound" Font-Size="10px"
                                                                                    OnPageIndexChanging="grvAddPaciente_PageIndexChanging" DataKeyNames="intVariableAdiID" OnRowCancelingEdit="grvAddPaciente_RowCancelingEdit"
                                                                                    OnRowCommand="grvAddPaciente_RowCommand" OnRowEditing="grvAddPaciente_RowEditing" OnRowUpdating="grvAddPaciente_RowUpdating"
                                                                                    EmptyDataText="No hay resultado bajo el criterio de búsqueda.">
                                                                                    <Columns>
                                                                                        <asp:BoundField DataField="intVariableAdiID" HeaderText="ID" ReadOnly="true" ItemStyle-CssClass="hidden-md" HeaderStyle-CssClass="hidden-md"/>
                                                                                        <asp:TemplateField HeaderText="Nombre Variable">
                                                                                            <ItemTemplate>
                                                                                                <asp:Label runat="server" ID="lblNomVar" Text='<%#Eval("vchNombreVarAdi") %>' />
                                                                                            </ItemTemplate>
                                                                                            <EditItemTemplate>
                                                                                                 <asp:TextBox ID="txtname" width="100%"  runat="server" Text='<%#Eval("vchNombreVarAdi") %>'/>
                                                                                            </EditItemTemplate>
                                                                                        </asp:TemplateField>
                                                                                        
                                                                                        <asp:TemplateField HeaderText="Editar" ItemStyle-HorizontalAlign="Center">
                                                                                            <ItemTemplate>      
                                                                                                <asp:LinkButton ID="btnVisualizar" CausesValidation="false" CommandName="Edit"  runat="server">
                                                                                                    <i class="fa fa-pencil" aria-hidden="true" title="Editar" style="font-size:25px;"></i>
                                                                                                </asp:LinkButton>
                                                                                            </ItemTemplate>
                                                                                            <EditItemTemplate>
                                                                                                <asp:LinkButton ID="btnUpdateVarPacinete" runat="server" CommandName="Update"  Text="Actualizar">
                                                                                                    <i class="fa fa-floppy-o" aria-hidden="true"  title="Actualizar" style="font-size:25px;"></i>
                                                                                                </asp:LinkButton>
                                                                                                <asp:LinkButton ID="bntCancelEditPaciente" runat="server" CommandName="Cancel"  Text="Cancelar">
                                                                                                    <i class="fa fa-ban" aria-hidden="true" title="Cancelar" style="font-size:25px;"></i>
                                                                                                </asp:LinkButton>
                                                                                            </EditItemTemplate>
                                                                                        </asp:TemplateField>
                                                                                        <asp:TemplateField HeaderText="Estatus" ItemStyle-HorizontalAlign="Center">
                                                                                            <ItemTemplate>
                                                                                                <asp:ImageButton ID="imbEstatus" runat="server" BackColor="Transparent"  Height="25px" Width="25px" 
                                                                                                    CommandArgument='<%#Eval("intVariableAdiID") %>' CommandName="Estatus" ToolTip="Cambia el estatus de la variable" />
                                                                                            </ItemTemplate>
                                                                                        </asp:TemplateField>
                                                                                    </Columns>
                                                                                    <PagerTemplate>
                                                                                        <asp:Label ID="lblTemplate" runat="server" Text="Muestra Filas: " CssClass="Label" />
                                                                                        <asp:DropDownList ID="ddlBandejaPaciente" runat="server" AutoPostBack="true" CausesValidation="false"
                                                                                            Enabled="true" OnSelectedIndexChanged="ddlBandejaPaciente_SelectedIndexChanged">
                                                                                                <asp:ListItem Value="10" />
                                                                                                <asp:ListItem Value="15" />
                                                                                                <asp:ListItem Value="20" />
                                                                                        </asp:DropDownList>
                                                                                        &nbsp;Página
                                                                                        <asp:TextBox ID="txtBandejaPaciente" runat="server" AutoPostBack="true" OnTextChanged="txtBandejaPaciente_TextChanged"
                                                                                            Width="40" MaxLength="10" />
                                                                                        de
                                                                                        <asp:Label ID="lblBandejaTotalPaciente" runat="server" />
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
                                            <div class="col-lg-6 col-md-12 col-sm-12">
                                                <div class="widget-box">
								                    <div class="widget-header">
									                    <h4 class="widget-title">Cita</h4>
								                    </div>

								                    <div class="widget-body">
									                    <div class="widget-main">
                                                            <div class="row form-group">
                                                                <div class="col-lg-12 col-md-12 col-sm-12">
                                                                    <div class="row">
                                                                        <div class="col-lg-12">
                                                                            <asp:Label runat="server" Text="Nombre de la Variable" AssociatedControlID="txtNombreVarCita" Width="100%"></asp:Label>
                                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ValidationGroup="vgAddVarAdiCita" ErrorMessage="* Campo requerido" ControlToValidate="txtNombreVarCita" Text="* Campo requerido" ForeColor="Red"></asp:RequiredFieldValidator>
                                                                        </div>
                                                                    </div>
                                                                    <div class="row">
                                                                        <div class="col-lg-12">
                                                                            <asp:TextBox runat="server" Text="" CssClass="form-control" ID="txtNombreVarCita" Width="100%" placeholder="Nombre de la Variable"></asp:TextBox>
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                            <div class="row form-group">
                                                                <div class="col-lg-12 col-md-12 col-sm-12 text-right">
                                                                    <asp:Button runat="server" ID="btnAddVarCita" Text="Guardar" OnClick="btnAddVarCita_Click" CssClass="btn btn-primary" ValidationGroup="vgAddVarAdiCita"/>
                                                                </div>
                                                            </div>
                                                            <div class="row">
                                                                <asp:UpdatePanel runat="server">
                                                                    <ContentTemplate>
                                                                        <asp:Panel runat="server">
                                                                            <asp:GridView ID="grvAddCita" runat="server" AllowPaging="true" CssClass="table table-striped table-bordered"
                                                                                    PageSize="10" AutoGenerateColumns="false" OnRowDataBound="grvAddCita_RowDataBound" Font-Size="10px"
                                                                                    OnPageIndexChanging="grvAddCita_PageIndexChanging" DataKeyNames="intVariableAdiID" OnRowCancelingEdit="grvAddCita_RowCancelingEdit"
                                                                                    OnRowCommand="grvAddCita_RowCommand" OnRowEditing="grvAddCita_RowEditing" OnRowUpdating="grvAddCita_RowUpdating"
                                                                                    EmptyDataText="No hay resultado bajo el criterio de búsqueda.">
                                                                                    <Columns>
                                                                                        <asp:BoundField DataField="intVariableAdiID" HeaderText="ID" ReadOnly="true" ItemStyle-CssClass="hidden-md" HeaderStyle-CssClass="hidden-md"/>
                                                                                        <asp:TemplateField HeaderText="Nombre Variable">
                                                                                            <ItemTemplate>
                                                                                                <asp:Label runat="server" ID="lblNomVar" Text='<%#Eval("vchNombreVarAdi") %>' />
                                                                                            </ItemTemplate>
                                                                                            <EditItemTemplate>
                                                                                                 <asp:TextBox ID="txtname" width="100%"  runat="server" Text='<%#Eval("vchNombreVarAdi") %>'/>
                                                                                            </EditItemTemplate>
                                                                                        </asp:TemplateField>
                                                                                        
                                                                                        <asp:TemplateField HeaderText="Editar" ItemStyle-HorizontalAlign="Center">
                                                                                            <ItemTemplate>      
                                                                                                <asp:LinkButton ID="btnVisualizar" CausesValidation="false" CommandName="Edit"  runat="server">
                                                                                                    <i class="fa fa-pencil" aria-hidden="true" title="Editar" style="font-size:25px;"></i>
                                                                                                </asp:LinkButton>
                                                                                            </ItemTemplate>
                                                                                            <EditItemTemplate>
                                                                                                <asp:LinkButton ID="btnUpdateVarCita" runat="server" CommandName="Update"  Text="Actualizar">
                                                                                                    <i class="fa fa-floppy-o" aria-hidden="true"  title="Actualizar" style="font-size:25px;"></i>
                                                                                                </asp:LinkButton>
                                                                                                <asp:LinkButton ID="bntCancelEditCita" runat="server" CommandName="Cancel"  Text="Cancelar">
                                                                                                    <i class="fa fa-ban" aria-hidden="true" title="Cancelar" style="font-size:25px;"></i>
                                                                                                </asp:LinkButton>
                                                                                            </EditItemTemplate>
                                                                                        </asp:TemplateField>
                                                                                        <asp:TemplateField HeaderText="Estatus" ItemStyle-HorizontalAlign="Center">
                                                                                            <ItemTemplate>
                                                                                                <asp:ImageButton ID="imbEstatus" runat="server" BackColor="Transparent"  Height="25px" Width="25px" 
                                                                                                    CommandArgument='<%#Eval("intVariableAdiID") %>' CommandName="Estatus" ToolTip="Cambia el estatus de la variable" />
                                                                                            </ItemTemplate>
                                                                                        </asp:TemplateField>
                                                                                    </Columns>
                                                                                    <PagerTemplate>
                                                                                        <asp:Label ID="lblTemplate" runat="server" Text="Muestra Filas: " CssClass="Label" />
                                                                                        <asp:DropDownList ID="ddlBandejaCita" runat="server" AutoPostBack="true" CausesValidation="false"
                                                                                            Enabled="true" OnSelectedIndexChanged="ddlBandejaCita_SelectedIndexChanged">
                                                                                                <asp:ListItem Value="10" />
                                                                                                <asp:ListItem Value="15" />
                                                                                                <asp:ListItem Value="20" />
                                                                                        </asp:DropDownList>
                                                                                        &nbsp;Página
                                                                                        <asp:TextBox ID="txtBandejaCita" runat="server" AutoPostBack="true" OnTextChanged="txtBandejaCita_TextChanged"
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
                                            <div class="col-lg-6 col-md-12 col-sm-12">
                                                <div class="widget-box">
								                    <div class="widget-header">
									                    <h4 class="widget-title">Identificaciones Requeridas para el Paciente</h4>
								                    </div>

								                    <div class="widget-body">
									                    <div class="widget-main">
                                                            <div class="row form-group">
                                                                <div class="col-lg-12 col-md-12 col-sm-12">
                                                                    <div class="row">
                                                                        <div class="col-lg-12">
                                                                            <asp:Label runat="server" Text="Nombre de la Identificación" AssociatedControlID="txtIdentificacion" Width="100%"></asp:Label>
                                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator10" runat="server" ValidationGroup="vgAddVarId" ErrorMessage="* Campo requerido" ControlToValidate="txtIdentificacion" Text="* Campo requerido" ForeColor="Red"></asp:RequiredFieldValidator>
                                                                        </div>
                                                                    </div>
                                                                    <div class="row">
                                                                        <div class="col-lg-12">
                                                                            <asp:TextBox runat="server" Text="" CssClass="form-control" ID="txtIdentificacion" Width="100%" placeholder="Nombre de la Identificación"></asp:TextBox>
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                            <div class="row form-group">
                                                                <div class="col-lg-12 col-md-12 col-sm-12 text-right">
                                                                    <asp:Button runat="server" ID="btnAddVarID" Text="Guardar" OnClick="btnAddVarID_Click" CssClass="btn btn-primary" ValidationGroup="vgAddVarId"/>
                                                                </div>
                                                            </div>
                                                            <div class="row">
                                                                <asp:UpdatePanel runat="server">
                                                                    <ContentTemplate>
                                                                        <asp:Panel runat="server">
                                                                            <asp:GridView ID="grvVarID" runat="server" AllowPaging="true" CssClass="table table-striped table-bordered"
                                                                                    PageSize="10" AutoGenerateColumns="false" OnRowDataBound="grvVarID_RowDataBound" Font-Size="10px"
                                                                                    OnPageIndexChanging="grvVarID_PageIndexChanging" DataKeyNames="intIdentificacionID" OnRowCancelingEdit="grvVarID_RowCancelingEdit"
                                                                                    OnRowCommand="grvVarID_RowCommand" OnRowEditing="grvVarID_RowEditing" OnRowUpdating="grvVarID_RowUpdating"
                                                                                    EmptyDataText="No hay resultado bajo el criterio de búsqueda.">
                                                                                    <Columns>
                                                                                        <asp:BoundField DataField="intIdentificacionID" HeaderText="ID" ReadOnly="true" ItemStyle-CssClass="hidden-md" HeaderStyle-CssClass="hidden-md"/>
                                                                                        <asp:TemplateField HeaderText="Nombre Identificación">
                                                                                            <ItemTemplate>
                                                                                                <asp:Label runat="server" ID="lblNomVar" Text='<%#Eval("vchNombreID") %>' />
                                                                                            </ItemTemplate>
                                                                                            <EditItemTemplate>
                                                                                                 <asp:TextBox ID="txtname" width="100%"  runat="server" Text='<%#Eval("vchNombreID") %>'/>
                                                                                            </EditItemTemplate>
                                                                                        </asp:TemplateField>
                                                                                        
                                                                                        <asp:TemplateField HeaderText="Editar" ItemStyle-HorizontalAlign="Center">
                                                                                            <ItemTemplate>      
                                                                                                <asp:LinkButton ID="btnVisualizar" CausesValidation="false" CommandName="Edit"  runat="server">
                                                                                                    <i class="fa fa-pencil" aria-hidden="true" title="Editar" style="font-size:25px;"></i>
                                                                                                </asp:LinkButton>
                                                                                            </ItemTemplate>
                                                                                            <EditItemTemplate>
                                                                                                <asp:LinkButton ID="btnUpdateVarID" runat="server" CommandName="Update"  Text="Actualizar">
                                                                                                    <i class="fa fa-floppy-o" aria-hidden="true"  title="Actualizar" style="font-size:25px;"></i>
                                                                                                </asp:LinkButton>
                                                                                                <asp:LinkButton ID="bntCancelEditID" runat="server" CommandName="Cancel"  Text="Cancelar">
                                                                                                    <i class="fa fa-ban" aria-hidden="true" title="Cancelar" style="font-size:25px;"></i>
                                                                                                </asp:LinkButton>
                                                                                            </EditItemTemplate>
                                                                                        </asp:TemplateField>
                                                                                        <asp:TemplateField HeaderText="Estatus" ItemStyle-HorizontalAlign="Center">
                                                                                            <ItemTemplate>
                                                                                                <asp:ImageButton ID="imbEstatus" runat="server" BackColor="Transparent"  Height="25px" Width="25px" 
                                                                                                    CommandArgument='<%#Eval("intIdentificacionID") %>' CommandName="Estatus" ToolTip="Cambia el estatus de la variable" />
                                                                                            </ItemTemplate>
                                                                                        </asp:TemplateField>
                                                                                    </Columns>
                                                                                    <PagerTemplate>
                                                                                        <asp:Label ID="lblTemplate" runat="server" Text="Muestra Filas: " CssClass="Label" />
                                                                                        <asp:DropDownList ID="ddlBandejaID" runat="server" AutoPostBack="true" CausesValidation="false"
                                                                                            Enabled="true" OnSelectedIndexChanged="ddlBandejaID_SelectedIndexChanged">
                                                                                                <asp:ListItem Value="10" />
                                                                                                <asp:ListItem Value="15" />
                                                                                                <asp:ListItem Value="20" />
                                                                                        </asp:DropDownList>
                                                                                        &nbsp;Página
                                                                                        <asp:TextBox ID="txtBandejaID" runat="server" AutoPostBack="true" OnTextChanged="txtBandejaID_TextChanged"
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
                                            <div class="col-lg-6 col-md-12 col-sm-12">
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
					    </div>

                        <div id="catalogConfig" class="tab-pane">
						    <div class="widget-box">
								<div class="widget-header">
									<h4 class="widget-title">Administración de los catálogos del sistema</h4>
								</div>

								<div class="widget-body">
									<div class="widget-main">
                                        <div class="row form-group">
                                            <div class="col-lg-5 col-sm-12">
                                                <div class="row">
                                                    <div class="col-lg-12">
                                                        <asp:Label runat="server" Text="Item del Catálogo" AssociatedControlID="txtItemCat" Width="100%"></asp:Label>
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" Text="* Capturar Nombre." ForeColor="Red"  ControlToValidate="txtItemCat" ValidationGroup="vgAddCat"></asp:RequiredFieldValidator>
                                                    </div>
                                                </div>
                                                <div class="row">
                                                    <div class="col-lg-12">
                                                        <asp:TextBox runat="server" ID="txtItemCat" Text="" CssClass="form-control" placeholder="Item del Catálogo"></asp:TextBox>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="col-lg-5 col-sm-12">
                                                <div class="row">
                                                    <div class="col-lg-12">
                                                        <asp:Label runat="server" Text="Catálogo" AssociatedControlID="ddlCatalogo" Width="100%"></asp:Label>
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" Text="* Seleccionar Catálogo." ForeColor="Red" InitialValue="0"  ControlToValidate="ddlCatalogo" ValidationGroup="vgAddCat"></asp:RequiredFieldValidator>
                                                    </div>
                                                </div>
                                                <div class="row">
                                                    <div class="col-lg-12">
                                                        <asp:DropDownList ID="ddlCatalogo" runat="server" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlCatalogo_SelectedIndexChanged"></asp:DropDownList>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="col-lg-2 col-sm-12 text-right">
                                                <div class="row">
                                                    <div class="col-lg-12">
                                                    </div>
                                                </div>
                                                <div class="row">
                                                    <div class="col-lg-12">
                                                        <asp:Button runat="server" ID="btnAddItemCat" Text="Agregar" OnClick="btnAddItemCat_Click" CssClass="btn btn-success" ValidationGroup="vgAddCat"/>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="row">
                                            <asp:UpdatePanel runat="server">
                                                <ContentTemplate>
                                                    <asp:Panel runat="server">
                                                        <asp:GridView ID="grvCatalogos" runat="server" AllowPaging="true" CssClass="table table-striped table-bordered"
                                                            PageSize="10" AutoGenerateColumns="false" OnRowDataBound="grvCatalogos_RowDataBound" Font-Size="10px"
                                                            OnPageIndexChanging="grvCatalogos_PageIndexChanging" DataKeyNames="vchCatalogoID" OnRowCancelingEdit="grvCatalogos_RowCancelingEdit"
                                                            OnRowCommand="grvCatalogos_RowCommand" OnRowEditing="grvCatalogos_RowEditing" OnRowUpdating="grvCatalogos_RowUpdating"
                                                            EmptyDataText="No hay resultado bajo el criterio de búsqueda.">
                                                            <Columns>
                                                                <asp:BoundField DataField="vchCatalogoID" HeaderText="ID" ReadOnly="true" ItemStyle-CssClass="hidden-md" HeaderStyle-CssClass="hidden-md"/>
                                                                <asp:TemplateField>
                                                                    <ItemTemplate>
                                                                        <asp:Label runat="server" ID="lblNomVar" Text='<%#Eval("vchCatalogo") %>' />
                                                                    </ItemTemplate>
                                                                    <EditItemTemplate>
                                                                        <asp:TextBox runat="server" ID="txtItemNombre" width="100%" Text='<%#Eval("vchCatalogo") %>'></asp:TextBox>
                                                                    </EditItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Editar" ItemStyle-HorizontalAlign="Center">
                                                                    <ItemTemplate>      
                                                                        <asp:LinkButton ID="btnVisualizar" CausesValidation="false" CommandName="Edit" runat="server">
                                                                            <i class="fa fa-pencil" aria-hidden="true" title="Editar" style="font-size:25px;"></i>
                                                                        </asp:LinkButton>
                                                                    </ItemTemplate>
                                                                    <EditItemTemplate>
                                                                        <asp:LinkButton ID="btnUpdateVarPacinete" runat="server" CommandName="Update"  Text="Actualizar">
                                                                            <i class="fa fa-floppy-o" aria-hidden="true"  title="Actualizar" style="font-size:25px;"></i>
                                                                        </asp:LinkButton>
                                                                        <asp:LinkButton ID="bntCancelEditPaciente" runat="server" CommandName="Cancel"  Text="Cancelar">
                                                                            <i class="fa fa-ban" aria-hidden="true" title="Cancelar" style="font-size:25px;"></i>
                                                                        </asp:LinkButton>
                                                                    </EditItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Estatus" ItemStyle-HorizontalAlign="Center">
                                                                    <ItemTemplate>
                                                                        <asp:ImageButton ID="imbEstatus" runat="server" BackColor="Transparent"  Height="25px" Width="25px" 
                                                                            CommandArgument='<%#Eval("vchCatalogoID") %>' CommandName="Estatus" ToolTip="Cambia el estatus del Sitio" />
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                            </Columns>
                                                            <PagerTemplate>
                                                                <asp:Label ID="lblTemplate" runat="server" Text="Muestra Filas: " CssClass="Label" />
                                                                <asp:DropDownList ID="ddlBandejaCat" runat="server" AutoPostBack="true" CausesValidation="false"
                                                                    Enabled="true" OnSelectedIndexChanged="ddlBandejaCat_SelectedIndexChanged">
                                                                        <asp:ListItem Value="10" />
                                                                        <asp:ListItem Value="15" />
                                                                        <asp:ListItem Value="20" />
                                                                </asp:DropDownList>
                                                                &nbsp;Página
                                                                <asp:TextBox ID="txtBandejaCat" runat="server" AutoPostBack="true" OnTextChanged="txtBandejaCat_TextChanged"
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

                        <div id="ModalityConfig" class="tab-pane">
						    <div class="widget-box">
								<div class="widget-header">
									<h4 class="widget-title">Administración de Modalidades, Prestaciones y Equipos</h4>

									<div class="widget-toolbar">
										<a href="#" data-action="collapse">
											<i class="ace-icon fa fa-chevron-up"></i>
										</a>

										<a href="#" data-action="close">
											<i class="ace-icon fa fa-times"></i>
										</a>
									</div>
								</div>

								<div class="widget-body">
									<div class="widget-main">
                                        <div class="row">
                                            <div class="col-lg-6 col-md-12 col-sm-12">
                                                <div class="widget-box">
								                    <div class="widget-header">
									                    <h4 class="widget-title">Prestaciones</h4>

								                    </div>

								                    <div class="widget-body">
									                    <div class="widget-main">
                                                            <div class="row form-group">
                                                                <div class="col-lg-3 col-sm-12 text-left">
                                                                    <div class="row">
                                                                        <div class="col-lg-12">
                                                                            <asp:Label runat="server" Text="Nombre de la prestación" AssociatedControlID="txtPrestacion" Width="100%"></asp:Label>
                                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" Text="* Campo requerido." ForeColor="Red"  ControlToValidate="txtPrestacion" ValidationGroup="vgAddPres"></asp:RequiredFieldValidator>
                                                                        </div>
                                                                    </div>
                                                                    <div class="row">
                                                                        <div class="col-lg-12">
                                                                            <asp:TextBox runat="server" ID="txtPrestacion" Text="" CssClass="form-control" placeholder="Prestación"></asp:TextBox>
                                                                        </div>
                                                                   </div>
                                                                </div>
                                                                <div class="col-lg-3 col-sm-12 text-left">
                                                                    <div class="row">
                                                                        <div class="col-lg-12">
                                                                            <asp:Label runat="server" Text="Duración de la prestación" AssociatedControlID="txtDuracionPres" Width="100%"></asp:Label>
                                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" Text="* Campo requerido." ForeColor="Red"  ControlToValidate="txtDuracionPres" ValidationGroup="vgAddPres"></asp:RequiredFieldValidator>
                                                                        </div>
                                                                    </div>
                                                                    <div class="row">
                                                                        <div class="col-lg-12">
                                                                            <asp:TextBox ID="txtDuracionPres" runat="server" CssClass="form-control" TextMode="Number" placeholder="Duración (minutos)"></asp:TextBox>
                                                                            <asp:RangeValidator ID="rangev" runat="server" ControlToValidate="txtDuracionPres" Type="Integer" ErrorMessage="Valor debe estar entre 1 y 120 minutos" MinimumValue="1" MaximumValue="120" ValidationGroup="vgAddPres"></asp:RangeValidator>
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                                <div class="col-lg-4 col-sm-12 text-left">
                                                                    <div class="row">
                                                                        <div class="col-lg-12">
                                                                            <asp:Label runat="server" Text="Modalidad" AssociatedControlID="ddlModalidad" Width="100%"></asp:Label>
                                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" Text="* Seleccionar modalidad." ForeColor="Red" InitialValue="0"  ControlToValidate="ddlModalidad" ValidationGroup="vgAddPres"></asp:RequiredFieldValidator>
                                                                        </div>
                                                                    </div>
                                                                    <div class="row">
                                                                        <div class="col-lg-12">
                                                                            <asp:DropDownList ID="ddlModalidad" runat="server" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlModalidad_SelectedIndexChanged"></asp:DropDownList>
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                                <div class="col-lg-2 col-sm-12 text-center">
                                                                        <asp:Button runat="server" ID="btnAddPres" Text="Agregar" OnClick="btnAddPres_Click" CssClass="btn btn-success" ValidationGroup="vgAddPres"/>
                                                                </div>
                                                            </div>
                                                            <div class="row">
                                                                <asp:UpdatePanel runat="server">
                                                                    <ContentTemplate>
                                                                        <asp:Panel runat="server">
                                                                            <asp:GridView ID="grvPrestaciones" runat="server" AllowPaging="true" CssClass="table table-striped table-bordered"
                                                                                PageSize="10" AutoGenerateColumns="false" OnRowDataBound="grvPrestaciones_RowDataBound" Font-Size="10px"
                                                                                OnPageIndexChanging="grvPrestaciones_PageIndexChanging" DataKeyNames="intRELModPres, intPrestacionID" OnRowCancelingEdit="grvPrestaciones_RowCancelingEdit"
                                                                                OnRowCommand="grvPrestaciones_RowCommand" OnRowEditing="grvPrestaciones_RowEditing" OnRowUpdating="grvPrestaciones_RowUpdating"
                                                                                EmptyDataText="No hay resultado bajo el criterio de búsqueda.">
                                                                                <Columns>
                                                                                    <asp:BoundField DataField="intRELModPres" HeaderText="ID" ReadOnly="true" ItemStyle-CssClass="hidden-md" HeaderStyle-CssClass="hidden-md"/>
                                                                                    <asp:TemplateField HeaderText="Prestación">
                                                                                        <ItemTemplate>
                                                                                            <asp:Label runat="server" ID="lblNomVar" Text='<%#Eval("vchPrestacion") %>' />
                                                                                        </ItemTemplate>
                                                                                        <EditItemTemplate>
                                                                                            <asp:TextBox runat="server" ID="txtItemNombre" width="100%" Text='<%#Eval("vchPrestacion") %>'></asp:TextBox>
                                                                                        </EditItemTemplate>
                                                                                    </asp:TemplateField>
                                                                                    <asp:TemplateField  HeaderText="Duración (minutos)">
                                                                                        <ItemTemplate>
                                                                                            <asp:Label runat="server" ID="lblDuracion" Text='<%#Eval("intDuracionMin") %>' />
                                                                                        </ItemTemplate>
                                                                                        <EditItemTemplate>
                                                                                            <asp:TextBox runat="server" ID="txtDuracionItem" width="100%" Text='<%#Eval("intDuracionMin") %>'></asp:TextBox>
                                                                                        </EditItemTemplate>
                                                                                    </asp:TemplateField>
                                                                                    <asp:TemplateField HeaderText="Editar" ItemStyle-HorizontalAlign="Center">
                                                                                        <ItemTemplate>      
                                                                                            <asp:LinkButton ID="btnVisualizar" CausesValidation="false" CommandName="Edit" runat="server">
                                                                                                <i class="fa fa-pencil" aria-hidden="true" title="Editar" style="font-size:25px;"></i>
                                                                                            </asp:LinkButton>
                                                                                        </ItemTemplate>
                                                                                        <EditItemTemplate>
                                                                                            <asp:LinkButton ID="btnUpdateVarPacinete" runat="server" CommandName="Update"  Text="Actualizar">
                                                                                                <i class="fa fa-floppy-o" aria-hidden="true"  title="Actualizar" style="font-size:25px;"></i>
                                                                                            </asp:LinkButton>
                                                                                            <asp:LinkButton ID="bntCancelEditPaciente" runat="server" CommandName="Cancel"  Text="Cancelar">
                                                                                                <i class="fa fa-ban" aria-hidden="true" title="Cancelar" style="font-size:25px;"></i>
                                                                                            </asp:LinkButton>
                                                                                        </EditItemTemplate>
                                                                                    </asp:TemplateField>
                                                                                    <asp:TemplateField HeaderText="Indicaciones" ItemStyle-HorizontalAlign="Center">
                                                                                        <ItemTemplate>
                                                                                            <asp:LinkButton ID="imbIndicaciones" runat="server" BackColor="Transparent"  Height="25px" Width="25px" 
                                                                                                CommandArgument='<%#Eval("intPrestacionID") %>' CommandName="Indicacion" ToolTip="Ver las indicaciones" >
                                                                                                <i class="fa fa-comments-o" aria-hidden="true" title="Indicaciones" style="font-size:25px;"></i>
                                                                                            </asp:LinkButton>
                                                                                        </ItemTemplate>
                                                                                    </asp:TemplateField>
                                                                                    <asp:TemplateField HeaderText="Cuestionario" ItemStyle-HorizontalAlign="Center">
                                                                                        <ItemTemplate>
                                                                                            <asp:LinkButton ID="imbCuestionario" runat="server" BackColor="Transparent"  Height="25px" Width="25px" 
                                                                                                CommandArgument='<%#Eval("intPrestacionID") %>' CommandName="Cuestionario" ToolTip="Ver los cuestionarios">
                                                                                                <i class="fa fa-question" aria-hidden="true" title="Cuestionario" style="font-size:25px;"></i>
                                                                                            </asp:LinkButton>
                                                                                        </ItemTemplate>
                                                                                    </asp:TemplateField>
                                                                                    <asp:TemplateField HeaderText="Restricciones" ItemStyle-HorizontalAlign="Center">
                                                                                        <ItemTemplate>
                                                                                            <asp:LinkButton ID="imbRestricciones" runat="server" BackColor="Transparent"  Height="25px" Width="25px" 
                                                                                                CommandArgument='<%#Eval("intPrestacionID") %>' CommandName="Restricciones" ToolTip="Ver las Restricciones">
                                                                                                <i class="fa fa-ban" aria-hidden="true" title="Restricciones" style="font-size:25px;"></i>
                                                                                            </asp:LinkButton>
                                                                                        </ItemTemplate>
                                                                                    </asp:TemplateField>
                                                                                    <asp:TemplateField HeaderText="Estatus" ItemStyle-HorizontalAlign="Center">
                                                                                        <ItemTemplate>
                                                                                            <asp:ImageButton ID="imbEstatus" runat="server" BackColor="Transparent"  Height="25px" Width="25px" 
                                                                                                CommandArgument='<%#Eval("intRELModPres") %>' CommandName="Estatus" ToolTip="Cambia el estatus del Sitio" />
                                                                                        </ItemTemplate>
                                                                                    </asp:TemplateField>
                                                                                </Columns>
                                                                                <PagerTemplate>
                                                                                    <asp:Label ID="lblTemplate" runat="server" Text="Muestra Filas: " CssClass="Label" />
                                                                                    <asp:DropDownList ID="ddlBandejaPres" runat="server" AutoPostBack="true" CausesValidation="false"
                                                                                        Enabled="true" OnSelectedIndexChanged="ddlBandejaPres_SelectedIndexChanged">
                                                                                            <asp:ListItem Value="10" />
                                                                                            <asp:ListItem Value="15" />
                                                                                            <asp:ListItem Value="20" />
                                                                                    </asp:DropDownList>
                                                                                    &nbsp;Página
                                                                                    <asp:TextBox ID="txtBandejaPres" runat="server" AutoPostBack="true" OnTextChanged="txtBandejaPres_TextChanged"
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
                                            <div class="col-lg-6 col-md-12 col-sm-12">
                                                <div class="widget-box">
								                    <div class="widget-header">
									                    <h4 class="widget-title">Equipos</h4>

								                    </div>

								                    <div class="widget-body">
									                    <div class="widget-main">
                                                            <div class="row form-group">
                                                                <div class="col-lg-3 col-sm-12 text-left">
                                                                    <div class="row">
                                                                        <div class="col-lg-12">
                                                                            <asp:Label runat="server" Text="Nombre del Equipo" AssociatedControlID="txtNomEquipo"  Width="100%"></asp:Label>
                                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" Text="* Campo requerido." ForeColor="Red"  ControlToValidate="txtNomEquipo" ValidationGroup="vgAddEquipo"></asp:RequiredFieldValidator>
                                                                        </div>
                                                                    </div>
                                                                    <div class="row">
                                                                        <div class="col-lg-12">
                                                                            <asp:TextBox runat="server" ID="txtNomEquipo" Text="" CssClass="form-control" placeholder="Nombre del Equipo"></asp:TextBox>
                                                                        </div>
                                                                    </div>                                                                    
                                                                </div>
                                                                <div class="col-lg-2 col-sm-12 text-left">
                                                                    <div class="row">
                                                                        <div class="col-lg-12">
                                                                            <asp:Label runat="server" Text="Código del Equipo" AssociatedControlID="txtCodeequipo" Width="100%"></asp:Label>
                                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator11" runat="server" Text="* Campo requerido." ForeColor="Red"  ControlToValidate="txtCodeequipo" ValidationGroup="vgAddEquipo"></asp:RequiredFieldValidator>
                                                                        </div>
                                                                    </div>
                                                                    <div class="row">
                                                                        <div class="col-lg-12">
                                                                            <asp:TextBox runat="server" ID="txtCodeequipo" Text="" CssClass="form-control" placeholder="Código de equipo"></asp:TextBox>
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                                <div class="col-lg-2 col-sm-12 text-left">
                                                                    <div class="row">
                                                                        <div class="col-lg-12">
                                                                            <asp:Label runat="server" Text="AETitle del Equipo" AssociatedControlID="txtAEtitle" Width="100%"></asp:Label>
                                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" Text="* Campo requerido." ForeColor="Red"  ControlToValidate="txtAEtitle" ValidationGroup="vgAddEquipo"></asp:RequiredFieldValidator>
                                                                        </div>
                                                                    </div>
                                                                    <div class="row">
                                                                        <div class="col-lg-12">
                                                                            <asp:TextBox runat="server" ID="txtAEtitle" Text="" CssClass="form-control" placeholder="AETitle del Equipo"></asp:TextBox>
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                                <div class="col-lg-3 col-sm-12 text-left">
                                                                    <div class="row">
                                                                        <div class="col-lg-12">
                                                                            <asp:Label runat="server" Text="Modalidad" AssociatedControlID="ddlModalidadEquipo"  Width="100%"></asp:Label>
                                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator9" runat="server" Text="* Seleccionar modalidad." ForeColor="Red" InitialValue="0"  ControlToValidate="ddlModalidadEquipo" ValidationGroup="vgAddEquipo"></asp:RequiredFieldValidator>
                                                                        </div>
                                                                    </div>
                                                                    <div class="row">
                                                                        <div class="col-lg-12">
                                                                            <asp:DropDownList ID="ddlModalidadEquipo" runat="server" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlModalidadEquipo_SelectedIndexChanged"></asp:DropDownList>
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                                <div class="col-lg-2 col-sm-12  text-center">
                                                                    <asp:Button runat="server" ID="btnAddEquipo" Text="Agregar" OnClick="btnAddEquipo_Click" CssClass="btn btn-success" ValidationGroup="vgAddEquipo"/>
                                                                </div>
                                                            </div>
                                                            <div class="row">
                                                                <asp:UpdatePanel runat="server">
                                                                    <ContentTemplate>
                                                                        <asp:Panel runat="server">
                                                                            <asp:GridView ID="grvEquipo" runat="server" AllowPaging="true" CssClass="table table-striped table-bordered"
                                                                                PageSize="10" AutoGenerateColumns="false" OnRowDataBound="grvEquipo_RowDataBound" Font-Size="10px"
                                                                                OnPageIndexChanging="grvEquipo_PageIndexChanging" DataKeyNames="intEquipoID,intModalidadID" OnRowCancelingEdit="grvEquipo_RowCancelingEdit"
                                                                                OnRowCommand="grvEquipo_RowCommand" OnRowEditing="grvEquipo_RowEditing" OnRowUpdating="grvEquipo_RowUpdating"
                                                                                EmptyDataText="No hay resultado bajo el criterio de búsqueda.">
                                                                                <Columns>
                                                                                    <asp:BoundField DataField="intEquipoID" HeaderText="ID" ReadOnly="true" ItemStyle-CssClass="hidden-md" HeaderStyle-CssClass="hidden-md"/>
                                                                                    <asp:TemplateField HeaderText="Equipo">
                                                                                        <ItemTemplate>
                                                                                            <asp:Label runat="server" ID="lblNomEquipo" Text='<%#Eval("vchNombreEquipo") %>' />
                                                                                        </ItemTemplate>
                                                                                        <EditItemTemplate>
                                                                                            <asp:TextBox runat="server" ID="txtItemNombre" width="100%" Text='<%#Eval("vchNombreEquipo") %>'></asp:TextBox>
                                                                                        </EditItemTemplate>
                                                                                    </asp:TemplateField>
                                                                                    <asp:TemplateField  HeaderText="Código">
                                                                                        <ItemTemplate>
                                                                                            <asp:Label runat="server" ID="lblCodigo" Text='<%#Eval("vchCodigoEquipo") %>' />
                                                                                        </ItemTemplate>
                                                                                        <EditItemTemplate>
                                                                                            <asp:TextBox runat="server" ID="txtCodigoItem" width="100%" Text='<%#Eval("vchCodigoEquipo") %>'></asp:TextBox>
                                                                                        </EditItemTemplate>
                                                                                    </asp:TemplateField>
                                                                                    <asp:TemplateField  HeaderText="AETitle">
                                                                                        <ItemTemplate>
                                                                                            <asp:Label runat="server" ID="lblAETILTE" Text='<%#Eval("vchAETitle") %>' />
                                                                                        </ItemTemplate>
                                                                                        <EditItemTemplate>
                                                                                            <asp:TextBox runat="server" ID="txtAEtitleItem" width="100%" Text='<%#Eval("vchAETitle") %>'></asp:TextBox>
                                                                                        </EditItemTemplate>
                                                                                    </asp:TemplateField>
                                                                                    <asp:TemplateField HeaderText="Editar" ItemStyle-HorizontalAlign="Center">
                                                                                        <ItemTemplate>      
                                                                                            <asp:LinkButton ID="btnVisualizar" CausesValidation="false" CommandName="Edit" runat="server">
                                                                                                <i class="fa fa-pencil" aria-hidden="true" title="Editar" style="font-size:25px;"></i>
                                                                                            </asp:LinkButton>
                                                                                        </ItemTemplate>
                                                                                        <EditItemTemplate>
                                                                                            <asp:LinkButton ID="btnUpdateEquipo" runat="server" CommandName="Update"  Text="Actualizar">
                                                                                                <i class="fa fa-floppy-o" aria-hidden="true"  title="Actualizar" style="font-size:25px;"></i>
                                                                                            </asp:LinkButton>
                                                                                            <asp:LinkButton ID="bntCancelEditEquipo" runat="server" CommandName="Cancel"  Text="Cancelar">
                                                                                                <i class="fa fa-ban" aria-hidden="true" title="Cancelar" style="font-size:25px;"></i>
                                                                                            </asp:LinkButton>
                                                                                        </EditItemTemplate>
                                                                                    </asp:TemplateField>
                                                                                    <asp:TemplateField HeaderText="Estatus" ItemStyle-HorizontalAlign="Center">
                                                                                        <ItemTemplate>
                                                                                            <asp:ImageButton ID="imbEstatus" runat="server" BackColor="Transparent"  Height="25px" Width="25px" 
                                                                                                CommandArgument='<%#Eval("intEquipoID") %>' CommandName="Estatus" ToolTip="Cambia el estatus del Sitio" />
                                                                                        </ItemTemplate>
                                                                                    </asp:TemplateField>
                                                                                </Columns>
                                                                                <PagerTemplate>
                                                                                    <asp:Label ID="lblTemplate" runat="server" Text="Muestra Filas: " CssClass="Label" />
                                                                                    <asp:DropDownList ID="ddlBandejaEquipo" runat="server" AutoPostBack="true" CausesValidation="false"
                                                                                        Enabled="true" OnSelectedIndexChanged="ddlBandejaEquipo_SelectedIndexChanged">
                                                                                            <asp:ListItem Value="10" />
                                                                                            <asp:ListItem Value="15" />
                                                                                            <asp:ListItem Value="20" />
                                                                                    </asp:DropDownList>
                                                                                    &nbsp;Página
                                                                                    <asp:TextBox ID="txtBandejaEquipo" runat="server" AutoPostBack="true" OnTextChanged="txtBandejaEquipo_TextChanged"
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
                            </div>
					    </div>
				    </div>
			    </div>
            </div>
        </div>
        <asp:HiddenField ID="TabName" runat="server" />
    </div>

    <!-- modals -->
    <div class="modal fade bs-example-modal-sm" id="mdlIndicaciones" tabindex="-1" role="dialog" aria-hidden="true">
        <div class="modal-dialog" style="width:70%">
            <div class="modal-content">

            <div class="modal-header">
                <button type="button" class="close" data-dismiss="modal" aria-label="Close"><span aria-hidden="true">×</span>
                </button>
                <h4 class="modal-title" id="myModalLabel3">Indicaciones</h4>
            </div>
            <div class="modal-body">
                <asp:UpdatePanel runat="server">
                    <ContentTemplate>
                        <div class="form-horizontal" role="form">
                            <div class="row">
                                <div class="col-lg-10 col-md-10 col-sm-10">
                                    <div class="row">
                                        <div class="col-lg-10">
                                            <asp:TextBox runat="server" Text="" ID="txtInstruccion" placeholder="Instrucción" CssClass="form-control"></asp:TextBox>
                                        </div>
                                        <div class="col-lg-2">
                                            <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator12" ErrorMessage="*" ForeColor="Red" Text="*" ControlToValidate="txtInstruccion" ValidationGroup="vgIndicaciones"></asp:RequiredFieldValidator>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-lg-2 col-md-2 col-sm-2">
                                    <asp:Button runat="server" ID="btnAddIndicaciones" OnClick="btnAddIndicaciones_Click" Text="Guardar" CssClass="btn btn-primary" ValidationGroup="vgIndicaciones"></asp:Button>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-lg-12">
                                    <asp:UpdatePanel runat="server">
                                        <ContentTemplate>
                                            <asp:Panel runat="server">
                                                <asp:GridView ID="grvIndicaciones" runat="server" AllowPaging="true" CssClass="table table-striped table-bordered"
                                                        PageSize="10" AutoGenerateColumns="false" OnRowDataBound="grvIndicaciones_RowDataBound" Font-Size="10px"
                                                        OnPageIndexChanging="grvIndicaciones_PageIndexChanging" DataKeyNames="intIndicacionID,intPrestacionID" OnRowCancelingEdit="grvIndicaciones_RowCancelingEdit"
                                                        OnRowCommand="grvIndicaciones_RowCommand" OnRowEditing="grvIndicaciones_RowEditing" OnRowUpdating="grvIndicaciones_RowUpdating"
                                                        EmptyDataText="No hay resultado bajo el criterio de búsqueda.">
                                                        <Columns>
                                                            <asp:BoundField DataField="intIndicacionID" HeaderText="ID" ReadOnly="true" ItemStyle-CssClass="hidden-md" HeaderStyle-CssClass="hidden-md"/>
                                                            <asp:TemplateField HeaderText="Nombre">
                                                                <ItemTemplate>
                                                                    <asp:Label runat="server" ID="lblNombreUsuario" Text='<%#Eval("vchIndicacion") %>' />
                                                                </ItemTemplate>
                                                                <EditItemTemplate>
                                                                        <asp:TextBox ID="txtname" width="100%"  runat="server" Text='<%#Eval("vchIndicacion") %>'/>
                                                                </EditItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Editar" ItemStyle-HorizontalAlign="Center">
                                                                <ItemTemplate>      
                                                                    <asp:LinkButton ID="btnVisualizar" CausesValidation="false" CommandName="Edit" runat="server">
                                                                        <i class="fa fa-pencil" aria-hidden="true" title="Editar" style="font-size:25px;"></i>
                                                                    </asp:LinkButton>
                                                                </ItemTemplate>
                                                                <EditItemTemplate>
                                                                    <asp:LinkButton ID="btnUpdateVarPacinete" runat="server" CommandName="Update"  Text="Actualizar">
                                                                        <i class="fa fa-floppy-o" aria-hidden="true"  title="Actualizar" style="font-size:25px;"></i>
                                                                    </asp:LinkButton>
                                                                    <asp:LinkButton ID="bntCancelEditPaciente" runat="server" CommandName="Cancel"  Text="Cancelar">
                                                                        <i class="fa fa-ban" aria-hidden="true" title="Cancelar" style="font-size:25px;"></i>
                                                                    </asp:LinkButton>
                                                                </EditItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Estatus" ItemStyle-HorizontalAlign="Center">
                                                                <ItemTemplate>
                                                                    <asp:ImageButton ID="imbEstatus" runat="server" BackColor="Transparent"  Height="25px" Width="25px" 
                                                                        CommandArgument='<%#Eval("intIndicacionID") %>' CommandName="Estatus" ToolTip="Cambia el estatus del Sitio" />
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                        </Columns>
                                                        <PagerTemplate>
                                                            <asp:Label ID="lblTemplate" runat="server" Text="Muestra Filas: " CssClass="Label" />
                                                            <asp:DropDownList ID="ddlBandejaInd" runat="server" AutoPostBack="true" CausesValidation="false"
                                                                Enabled="true" OnSelectedIndexChanged="ddlBandejaInd_SelectedIndexChanged">
                                                                    <asp:ListItem Value="10" />
                                                                    <asp:ListItem Value="15" />
                                                                    <asp:ListItem Value="20" />
                                                            </asp:DropDownList>
                                                            &nbsp;Página
                                                            <asp:TextBox ID="txtBandejaInd" runat="server" AutoPostBack="true" OnTextChanged="txtBandejaInd_TextChanged"
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
                    </ContentTemplate>
                </asp:UpdatePanel>                
            </div>
            <div class="modal-footer">
                <asp:Button runat="server" ID="btnCancelIndicaciones" class="btn btn-default" Text="Cerrar" OnClick="btnCancelIndicaciones_Click" data-dismiss="modal"></asp:Button>
            </div>

            </div>
        </div>
    </div>
    <!-- /modals -->

    <!-- modals -->
    <div class="modal fade bs-example-modal-sm" id="mdlCuestionarios" tabindex="-1" role="dialog" aria-hidden="true">
        <div class="modal-dialog" style="width:70%">
            <div class="modal-content">

                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close"><span aria-hidden="true">×</span>
                    </button>
                    <h4 class="modal-title" id="myModalLabel2">Cuestionarios</h4>
                </div>
                <div class="modal-body">
                    <asp:UpdatePanel runat="server">
                        <ContentTemplate>
                            <div class="form-horizontal" role="form">
                                <div class="row">
                                    <div class="col-lg-10 col-md-10 col-sm-10">
                                        <div class="row">
                                            <div class="col-lg-10">
                                                <asp:TextBox runat="server" Text="" ID="txtCuestionario" placeholder="Cuestionario" CssClass="form-control"></asp:TextBox>
                                            </div>
                                            <div class="col-lg-2">
                                                <asp:RequiredFieldValidator runat="server" ID="rfvCuestionario" ErrorMessage="*" ForeColor="Red" Text="*" ControlToValidate="txtCuestionario" ValidationGroup="vgCuestionario"></asp:RequiredFieldValidator>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-lg-2 col-md-2 col-sm-2">
                                        <asp:Button runat="server" ID="btnAddCuestionario" Text="Agregar" CssClass="btn btn-success" ValidationGroup="vgCuestionario" OnClick="btnAddCuestionario_Click" />
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-lg-12">
                                        <asp:UpdatePanel runat="server">
                                            <ContentTemplate>
                                                <asp:Panel runat="server">
                                                    <asp:GridView ID="grvCuestionario" runat="server" AllowPaging="true" CssClass="table table-striped table-bordered"
                                                            PageSize="10" AutoGenerateColumns="false" OnRowDataBound="grvCuestionario_RowDataBound" Font-Size="10px"
                                                            OnPageIndexChanging="grvCuestionario_PageIndexChanging" DataKeyNames="intDETCuestionarioID,intPrestacionID" OnRowCancelingEdit="grvCuestionario_RowCancelingEdit"
                                                            OnRowCommand="grvCuestionario_RowCommand" OnRowEditing="grvCuestionario_RowEditing" OnRowUpdating="grvCuestionario_RowUpdating"
                                                            EmptyDataText="No hay resultado bajo el criterio de búsqueda.">
                                                            <Columns>
                                                                <asp:BoundField DataField="intDETCuestionarioID" HeaderText="ID" ReadOnly="true" ItemStyle-CssClass="hidden-md" HeaderStyle-CssClass="hidden-md"/>
                                                                <asp:TemplateField HeaderText="Nombre">
                                                                    <ItemTemplate>
                                                                        <asp:Label runat="server" ID="lblNombreUsuario" Text='<%#Eval("vchCuestionario") %>' />
                                                                    </ItemTemplate>
                                                                    <EditItemTemplate>
                                                                            <asp:TextBox ID="txtname" width="100%"  runat="server" Text='<%#Eval("vchCuestionario") %>'/>
                                                                    </EditItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Editar" ItemStyle-HorizontalAlign="Center">
                                                                    <ItemTemplate>      
                                                                        <asp:LinkButton ID="btnVisualizar" CausesValidation="false" CommandName="Edit" runat="server">
                                                                            <i class="fa fa-pencil" aria-hidden="true" title="Editar" style="font-size:25px;"></i>
                                                                        </asp:LinkButton>
                                                                    </ItemTemplate>
                                                                    <EditItemTemplate>
                                                                        <asp:LinkButton ID="btnUpdateVarPacinete" runat="server" CommandName="Update"  Text="Actualizar">
                                                                            <i class="fa fa-floppy-o" aria-hidden="true"  title="Actualizar" style="font-size:25px;"></i>
                                                                        </asp:LinkButton>
                                                                        <asp:LinkButton ID="bntCancelEditPaciente" runat="server" CommandName="Cancel"  Text="Cancelar">
                                                                            <i class="fa fa-ban" aria-hidden="true" title="Cancelar" style="font-size:25px;"></i>
                                                                        </asp:LinkButton>
                                                                    </EditItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Estatus" ItemStyle-HorizontalAlign="Center">
                                                                    <ItemTemplate>
                                                                        <asp:ImageButton ID="imbEstatus" runat="server" BackColor="Transparent"  Height="25px" Width="25px" 
                                                                            CommandArgument='<%#Eval("intDETCuestionarioID") %>' CommandName="Estatus" ToolTip="Cambia el estatus del Sitio" />
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                            </Columns>
                                                            <PagerTemplate>
                                                                <asp:Label ID="lblTemplate" runat="server" Text="Muestra Filas: " CssClass="Label" />
                                                                <asp:DropDownList ID="ddlBandejaCues" runat="server" AutoPostBack="true" CausesValidation="false"
                                                                    Enabled="true" OnSelectedIndexChanged="ddlBandejaCues_SelectedIndexChanged">
                                                                        <asp:ListItem Value="10" />
                                                                        <asp:ListItem Value="15" />
                                                                        <asp:ListItem Value="20" />
                                                                </asp:DropDownList>
                                                                &nbsp;Página
                                                                <asp:TextBox ID="txtBandejaCues" runat="server" AutoPostBack="true" OnTextChanged="txtBandejaCues_TextChanged"
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
                        </ContentTemplate>
                    </asp:UpdatePanel>                
                </div>
                <div class="modal-footer">
                    <asp:Button runat="server" ID="btnCancelCuestionarios" class="btn btn-default" Text="Cerrar" OnClick="btnCancelCuestionarios_Click" data-dismiss="modal"></asp:Button>
                </div>
            </div>
        </div>
    </div>
    <!-- /modals -->

    <!-- modals -->
    <div class="modal fade bs-example-modal-sm" id="mdlRestricciones" tabindex="-1" role="dialog" aria-hidden="true">
        <div class="modal-dialog" style="width:70%">
            <div class="modal-content">

            <div class="modal-header">
                <button type="button" class="close" data-dismiss="modal" aria-label="Close"><span aria-hidden="true">×</span>
                </button>
                <h4 class="modal-title" id="myModalLabel4">Restricciones</h4>
            </div>
            <div class="modal-body">
                <asp:UpdatePanel runat="server">
                        <ContentTemplate>
                            <div class="form-horizontal" role="form">
                                <div class="row">
                                    <div class="col-lg-10 col-md-10 col-sm-10">
                                        <div class="row">
                                            <div class="col-lg-10">
                                                <asp:TextBox runat="server" Text="" ID="txtRestriccion" placeholder="Restricción" CssClass="form-control"></asp:TextBox>
                                            </div>
                                            <div class="col-lg-2">
                                                <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator13" ErrorMessage="*" ForeColor="Red" Text="*" ControlToValidate="txtRestriccion" ValidationGroup="vgRestriccion"></asp:RequiredFieldValidator>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-lg-2 col-md-2 col-sm-2">
                                        <asp:Button runat="server" ID="btnAddRestricciones" OnClick="btnAddRestricciones_Click" Text="Guardar" CssClass="btn btn-primary" ValidationGroup="vgRestriccion"></asp:Button>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-lg-12">
                                        <asp:UpdatePanel runat="server">
                                            <ContentTemplate>
                                                <asp:Panel runat="server">
                                                    <asp:GridView ID="grvRestriccion" runat="server" AllowPaging="true" CssClass="table table-striped table-bordered"
                                                            PageSize="10" AutoGenerateColumns="false" OnRowDataBound="grvRestriccion_RowDataBound" Font-Size="10px"
                                                            OnPageIndexChanging="grvRestriccion_PageIndexChanging" DataKeyNames="intReestriccionID,intPrestacionID" OnRowCancelingEdit="grvRestriccion_RowCancelingEdit"
                                                            OnRowCommand="grvRestriccion_RowCommand" OnRowEditing="grvRestriccion_RowEditing" OnRowUpdating="grvRestriccion_RowUpdating"
                                                            EmptyDataText="No hay resultado bajo el criterio de búsqueda.">
                                                            <Columns>
                                                                <asp:BoundField DataField="intReestriccionID" HeaderText="ID" ReadOnly="true" ItemStyle-CssClass="hidden-md" HeaderStyle-CssClass="hidden-md"/>
                                                                <asp:TemplateField HeaderText="Nombre">
                                                                    <ItemTemplate>
                                                                        <asp:Label runat="server" ID="lblNombreUsuario" Text='<%#Eval("vchNombreReestriccion") %>' />
                                                                    </ItemTemplate>
                                                                    <EditItemTemplate>
                                                                            <asp:TextBox ID="txtname" width="100%"  runat="server" Text='<%#Eval("vchNombreReestriccion") %>'/>
                                                                    </EditItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Editar" ItemStyle-HorizontalAlign="Center">
                                                                    <ItemTemplate>      
                                                                        <asp:LinkButton ID="btnVisualizar" CausesValidation="false" CommandName="Edit" runat="server">
                                                                            <i class="fa fa-pencil" aria-hidden="true" title="Editar" style="font-size:25px;"></i>
                                                                        </asp:LinkButton>
                                                                    </ItemTemplate>
                                                                    <EditItemTemplate>
                                                                        <asp:LinkButton ID="btnUpdateVarPacinete" runat="server" CommandName="Update"  Text="Actualizar">
                                                                            <i class="fa fa-floppy-o" aria-hidden="true"  title="Actualizar" style="font-size:25px;"></i>
                                                                        </asp:LinkButton>
                                                                        <asp:LinkButton ID="bntCancelEditPaciente" runat="server" CommandName="Cancel"  Text="Cancelar">
                                                                            <i class="fa fa-ban" aria-hidden="true" title="Cancelar" style="font-size:25px;"></i>
                                                                        </asp:LinkButton>
                                                                    </EditItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Estatus" ItemStyle-HorizontalAlign="Center">
                                                                    <ItemTemplate>
                                                                        <asp:ImageButton ID="imbEstatus" runat="server" BackColor="Transparent"  Height="25px" Width="25px" 
                                                                            CommandArgument='<%#Eval("intReestriccionID") %>' CommandName="Estatus" ToolTip="Cambia el estatus del Sitio" />
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                            </Columns>
                                                            <PagerTemplate>
                                                                <asp:Label ID="lblTemplate" runat="server" Text="Muestra Filas: " CssClass="Label" />
                                                                <asp:DropDownList ID="ddlBandejaRes" runat="server" AutoPostBack="true" CausesValidation="false"
                                                                    Enabled="true" OnSelectedIndexChanged="ddlBandejaRes_SelectedIndexChanged">
                                                                        <asp:ListItem Value="10" />
                                                                        <asp:ListItem Value="15" />
                                                                        <asp:ListItem Value="20" />
                                                                </asp:DropDownList>
                                                                &nbsp;Página
                                                                <asp:TextBox ID="txtBandejaRes" runat="server" AutoPostBack="true" OnTextChanged="txtBandejaRes_TextChanged"
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
                        </ContentTemplate>
                    </asp:UpdatePanel>                
            </div>
            <div class="modal-footer">
                <asp:Button runat="server" ID="btnCancelRestricciones" class="btn btn-default" Text="Cerrar" OnClick="btnCancelRestricciones_Click" data-dismiss="modal"></asp:Button>
            </div>

            </div>
        </div>
    </div>
    <!-- /modals -->

    <script type="text/javascript">
        $(function () {
            var tabName = $("[id*=TabName]").val() != "" ? $("[id*=TabName]").val() : "sistemaConfig";
            $('#Tabs a[href="#' + tabName + '"]').tab('show');
            $("#Tabs a").click(function () {
                $("[id*=TabName]").val($(this).attr("href").replace("#", ""));
            });
        });

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

        function openModal() {
            $('#mdlCuestionarios').modal('show');
        }

        function openModal() {
            $('#mdlIndicaciones').modal('show');
        }

        function openModal() {
            $('#mdlRestricciones').modal('show');
        }
    </script>
</asp:Content>
