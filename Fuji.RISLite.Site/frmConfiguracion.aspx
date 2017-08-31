<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="frmConfiguracion.aspx.cs" Inherits="Fuji.RISLite.Site.frmConfiguracion" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
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
                                                        <asp:Image ID="imgLogo" runat="server" Width="100%" Height="340px" Visible="false" />
                                                    </div>
                                                </div>                                                
                                            </div>
                                        </div> 
                                        <div class="row form-group ">
                                            <div class="col-lg-5 col-md-12 col-sm-12 col-xs-12">
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
                                        <div class="row form-group">
                                            <div class="col-lg-4 col-sm-12">
                                               <asp:Label runat="server" Text="Nombre" ></asp:Label>
                                            </div>
                                            <div class="col-lg-4 col-sm-12">
                                                 <asp:Label runat="server" Text="Usuario" ></asp:Label>
                                            </div>
                                            <div class="col-lg-4 col-sm-12">
                                                 <asp:Label runat="server" Text="Tipo de Usuario" ></asp:Label>
                                            </div>
                                        </div>
                                        <div class="row form-group">
                                            <div class="col-lg-4 col-sm-12">
                                                <asp:TextBox runat="server" ID="txtNombre" Text="" CssClass="form-control" placeholder="Nombre de Usuario"></asp:TextBox>
                                                <asp:RequiredFieldValidator ID="rfvNombreUser" runat="server" Enabled="false" Text="* Capturar Nombre." ForeColor="Red"  ControlToValidate="txtNombre" ValidationGroup="vgAddUser"></asp:RequiredFieldValidator>
                                            </div>
                                            <div class="col-lg-4 col-sm-12">
                                                 <asp:TextBox runat="server" ID="txtUsuario" Text="" CssClass="form-control" placeholder="Usuario"></asp:TextBox>
                                                <asp:RequiredFieldValidator ID="rfvUsuario" runat="server" Enabled="false" Text="* Capturar Usuario." ForeColor="Red"  ControlToValidate="txtUsuario" ValidationGroup="vgAddUser"></asp:RequiredFieldValidator>
                                            </div>
                                            <div class="col-lg-4 col-sm-12">
                                                 <asp:DropDownList ID="ddlTipoUsuario" runat="server" CssClass="form-control"></asp:DropDownList>
                                                <asp:RequiredFieldValidator ID="rfvTipoUsuario" runat="server" Enabled="false" Text="* Seleccionar tipo de usuario." ForeColor="Red" InitialValue="0"  ControlToValidate="txtUsuario" ValidationGroup="vgAddUser"></asp:RequiredFieldValidator>
                                            </div>
                                        </div>
                                        <div class="row form-group ">
                                            <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12">
                                                <div class="row">
                                                    <div class="col-lg-12 text-right">
                                                        <asp:Button runat="server" ID="btnBuscar" CssClass="btn btn-primary" Text="Buscar" OnClick="btnBuscar_Click" />
                                                        <asp:Button runat="server" ID="btnAgregar" CssClass="btn btn-success" Text="Agregar" OnClick="btnAgregar_Click"  ValidationGroup="vgAddUser"/>
                                                    </div>
                                                </div>
                                                <hr />
                                                <asp:UpdatePanel runat="server">
                                                    <ContentTemplate>
                                                        <asp:Panel runat="server">
                                                            <asp:GridView ID="grvUsuario" runat="server" AllowPaging="true" CssClass="table table-striped table-bordered"
                                                                    PageSize="10" AutoGenerateColumns="false" OnRowDataBound="grvUsuario_RowDataBound" Font-Size="10px"
                                                                    OnPageIndexChanging="grvUsuario_PageIndexChanging" DataKeyNames="intUsuarioID"
                                                                    OnRowCommand="grvUsuario_RowCommand"
                                                                    EmptyDataText="No hay resultado bajo el criterio de búsqueda.">
                                                                    <Columns>
                                                                        <asp:BoundField DataField="intUsuarioID" HeaderText="ID" ReadOnly="true" ItemStyle-CssClass="hidden-md" HeaderStyle-CssClass="hidden-md"/>
                                                                        <asp:BoundField DataField="vchNombre"  HeaderText="Nombre" ReadOnly="true" />
                                                                        <asp:BoundField DataField="vchUsuario"  HeaderText="Usuario" ReadOnly="true" />
                                                                        <asp:BoundField DataField="vchTipoUsuario"  HeaderText="Tipo de Usuario" ReadOnly="true" />
                                                                        <asp:TemplateField HeaderText="Editar" ItemStyle-HorizontalAlign="Center">
                                                                            <ItemTemplate>      
                                                                                <asp:LinkButton ID="btnVisualizar" CausesValidation="false" CommandName="viewEditar" CommandArgument='<%# Bind("intUsuarioID") %>' runat="server">
                                                                                    <asp:Image ID="ImageVisializa" runat="server" ImageUrl="~/images/ic_action_edit.png" Height="25px" Width="25px" ToolTip="Editar"/>
                                                                                </asp:LinkButton>
                                                                            </ItemTemplate>
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
									                    <h4 class="widget-title">Paciente</h4>

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
                                                            <div class="row form-group">
                                                                <div class="col-lg-12 col-md-12 col-sm-12">
                                                                    <asp:RequiredFieldValidator ID="rfvAddVarAdiPac" runat="server" ValidationGroup="vgAddVarAdiPac" ErrorMessage="* Campo requerido" ControlToValidate="txtAddVarPac" Text="* Campo requerido" ForeColor="Red"></asp:RequiredFieldValidator>
                                                                    <asp:TextBox runat="server" Text="" CssClass="form-control" ID="txtAddVarPac" placeholder="Nombre de la Variable"></asp:TextBox>
                                                                </div>
                                                            </div>
                                                            <div class="row form-group">
                                                                <div class="col-lg-12 col-md-12 col-sm-12 text-right">
                                                                    <asp:Button runat="server" ID="btnBuscarVarPac" Text="Buscar" OnClick="btnBuscarVarPac_Click" CssClass="btn btn-primary" />
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
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="col-lg-6 col-md-12 col-sm-12">
                                                <div class="widget-box">
								                    <div class="widget-header">
									                    <h4 class="widget-title">Estudio</h4>

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
                                                        </div>
                                                    </div>
                                                </div>
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
    </script>
</asp:Content>
