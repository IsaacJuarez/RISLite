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

    <telerik:RadAjaxManager ID="RadAjaxManager1" runat="server">
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="ddlTipoUsuario">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="ddlSitioUser" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="ddlSitioSistema">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="txtNombreSitio" />
                    <telerik:AjaxUpdatedControl ControlID="txtDireccionSitio" />
                    <telerik:AjaxUpdatedControl ControlID="imgLogo" />
                    <telerik:AjaxUpdatedControl ControlID="txtPathRepositorio" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="ddlSitioCorreo">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="txtEmailSistema" />
                    <telerik:AjaxUpdatedControl ControlID="txtPasswordSistema" />
                    <telerik:AjaxUpdatedControl ControlID="txtHost" />
                    <telerik:AjaxUpdatedControl ControlID="txtPortCorreo" />
                    <telerik:AjaxUpdatedControl ControlID="RadAjaxPanel1" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="radCbxCatalogo">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grvCatalogos" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="radCbxSitioCatalogo">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grvCatalogos" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="grvCatalogos">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grvCatalogos" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="btnAddItemCat">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grvCatalogos" />
                </UpdatedControls>
            </telerik:AjaxSetting>

            <telerik:AjaxSetting AjaxControlID="ddlModalidad">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grvPrestaciones" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="ddlSitioMod">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="ddlModalidad" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="grvPrestaciones">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grvPrestaciones" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="btnAddPres">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grvPrestaciones" />
                </UpdatedControls>
            </telerik:AjaxSetting>

            <telerik:AjaxSetting AjaxControlID="ddlSitioModEquipo">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="AjaxPanelModalidadEquipo" />
                    <telerik:AjaxUpdatedControl ControlID="AjaxPanelEquipo" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="btnSearchEquipo">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grvEquipo" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="ddlModalidadEquipo">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grvEquipo" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="grvEquipo">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grvEquipo" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="btnAddEquipo">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grvEquipo" />
                </UpdatedControls>
            </telerik:AjaxSetting>

            <telerik:AjaxSetting AjaxControlID="ddlTipoVariable">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grvAdicional" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="grvAdicional">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grvAdicional" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="ddlSitioAdicionales">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grvAdicional" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="btnAgregarAdicional">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grvAdicional" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="grvUsuario">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="ddlModalidadTecnico" />
                    <telerik:AjaxUpdatedControl ControlID="grvModalidadTecnico" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="btnAddMod">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="ddlModalidadTecnico" />
                    <telerik:AjaxUpdatedControl ControlID="grvModalidadTecnico" />
                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>
    </telerik:RadAjaxManager>

    <div class="page-content">
        <div class="page-header">
            <div class="messagealert" id="alert_container"></div>
            <h1>Configuración
			    <small>
                    <i class="ace-icon fa fa-angle-double-right"></i>
                    Sistema
                </small>
            </h1>
        </div>
        <!-- /.page-header -->
        <div class="row">
            <div class="col-lg-12 col-md-12 col-sm-12">
                <div class="tabbable tabs-left" id="Tabs">
                    <ul class="nav nav-tabs" id="myTab3">
                        <li class="active">
                            <a data-toggle="tab" href="#SitiosConfig">
                                <i class="pink ace-icon fa fa-suitcase bigger-110"></i>
                                Sitios
                            </a>
                        </li>

                        <li>
                            <a data-toggle="tab" href="#userConfig">
                                <i class="red ace-icon fa fa-user"></i>
                                Usuarios del Sistema
                            </a>
                        </li>

                        <li>
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

                        <li>
                            <a data-toggle="tab" href="#Adicionales">
                                <i class="orange ace-icon fa fa-cubes"></i>
                                Adicionales
                            </a>
                        </li>

                    </ul>

                    <div class="tab-content">

                        <div id="SitiosConfig" class="tab-pane in active">
                            <div class="widget-box">
                                <div class="widget-header">
                                    <h4 class="widget-title">Sitios</h4>

                                </div>

                                <div class="widget-body">
                                    <div class="widget-main">
                                        <div class="row">
                                            <div class="col-lg-12 col-md-12 col-sm-12">
                                                <div class="row">
                                                    <div class="col-lg-12">
                                                        <table style="width: 100%">
                                                            <tr>
                                                                <td colspan="2" class="text-left">
                                                                    <asp:Label runat="server" ID="lblSitio" Text="Nombre del Sitio" ForeColor="DarkGreen"></asp:Label>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td style="width: 90%">
                                                                    <asp:RequiredFieldValidator runat="server" ID="rfvAddSitio" ErrorMessage="*" ForeColor="Red" ControlToValidate="txtAddSitio" Text="*" ValidationGroup="vgAddSitio"></asp:RequiredFieldValidator>
                                                                    <asp:TextBox ID="txtAddSitio" runat="server" Text="" CssClass="form-control" Width="90%"></asp:TextBox>
                                                                </td>
                                                                <td style="width: 10%" class="text-right">
                                                                    <div class="text-right">
                                                                        <asp:Button runat="server" ID="btnAddSitio" OnClick="btnAddSitio_Click" ValidationGroup="vgAddSitio" Text="Agregar Sitio" CssClass="btn btn-success text-right" />
                                                                    </div>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                        <hr />
                                        <div class="row">
                                            <div class="col-lg-12 col-md-12 col-sm-12">
                                                <asp:UpdatePanel runat="server">
                                                    <ContentTemplate>
                                                        <asp:Panel runat="server">
                                                            <asp:GridView ID="grvSitio" runat="server" AllowPaging="true" CssClass="table table-striped table-bordered"
                                                                PageSize="10" AutoGenerateColumns="false" OnRowDataBound="grvSitio_RowDataBound" Font-Size="10px"
                                                                OnPageIndexChanging="grvSitio_PageIndexChanging" DataKeyNames="intSitioID" OnRowCancelingEdit="grvSitio_RowCancelingEdit"
                                                                OnRowCommand="grvSitio_RowCommand" OnRowEditing="grvSitio_RowEditing" OnRowUpdating="grvSitio_RowUpdating"
                                                                EmptyDataText="No hay resultado bajo el criterio de búsqueda.">
                                                                <Columns>
                                                                    <asp:BoundField DataField="intSitioID" HeaderText="ID" ReadOnly="true" ItemStyle-CssClass="hidden-md" HeaderStyle-CssClass="hidden-md" />
                                                                    <asp:TemplateField HeaderText="Sitio">
                                                                        <ItemTemplate>
                                                                            <asp:Label runat="server" ID="lblNombreSitio" Text='<%#Eval("vchNombreSitio") %>' />
                                                                        </ItemTemplate>
                                                                        <EditItemTemplate>
                                                                            <asp:TextBox ID="txtNombreSitio" Width="100%" runat="server" Text='<%#Eval("vchNombreSitio") %>' />
                                                                        </EditItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Editar" ItemStyle-HorizontalAlign="Center">
                                                                        <ItemTemplate>
                                                                            <asp:LinkButton ID="btnVisualizar" CausesValidation="false" CommandName="Edit" runat="server">
                                                                            <i class="fa fa-pencil" aria-hidden="true" title="Editar" style="font-size:25px;"></i>
                                                                            </asp:LinkButton>
                                                                        </ItemTemplate>
                                                                        <EditItemTemplate>
                                                                            <asp:LinkButton ID="btnUpdateVarPacinete" runat="server" CommandName="Update" Text="Actualizar">
                                                                            <i class="fa fa-floppy-o" aria-hidden="true"  title="Actualizar" style="font-size:25px;"></i>
                                                                            </asp:LinkButton>
                                                                            <asp:LinkButton ID="bntCancelEditPaciente" runat="server" CommandName="Cancel" Text="Cancelar">
                                                                            <i class="fa fa-ban" aria-hidden="true" title="Cancelar" style="font-size:25px;"></i>
                                                                            </asp:LinkButton>
                                                                        </EditItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Estatus" ItemStyle-HorizontalAlign="Center">
                                                                        <ItemTemplate>
                                                                            <asp:ImageButton ID="imbEstatus" runat="server" BackColor="Transparent" Height="25px" Width="25px"
                                                                                CommandArgument='<%#Eval("intSitioID") %>' CommandName="Estatus" ToolTip="Cambia el estatus del Sitio" />
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                </Columns>
                                                                <PagerTemplate>
                                                                    <asp:Label ID="lblTemplate" runat="server" Text="Muestra Filas: " CssClass="Label" />
                                                                    <asp:DropDownList ID="ddlBandejaSitio" runat="server" AutoPostBack="true" CausesValidation="false"
                                                                        Enabled="true" OnSelectedIndexChanged="ddlBandejaSitio_SelectedIndexChanged">
                                                                        <asp:ListItem Value="10" />
                                                                        <asp:ListItem Value="15" />
                                                                        <asp:ListItem Value="20" />
                                                                    </asp:DropDownList>
                                                                    &nbsp;Página
                                                                <asp:TextBox ID="txtBandejaSitio" runat="server" AutoPostBack="true" OnTextChanged="txtBandejaSitio_TextChanged"
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

                        <div id="userConfig" class="tab-pane">
                            <div class="widget-box">
                                <div class="widget-header">
                                    <h4 class="widget-title">Administración de Usuarios</h4>

                                </div>

                                <div class="widget-body">
                                    <div class="widget-main">
                                        <div class="row form-group">
                                            <div class="col-lg-2 col-md-6 col-sm-12">
                                                <div class="row">
                                                    <div class="col-lg-12 col-md-12 col-sm-12">
                                                        <asp:Label runat="server" Text="Nombre de Usuario" AssociatedControlID="txtNombre" Width="100%"></asp:Label>
                                                        <asp:RequiredFieldValidator ID="rfvNombreUser" runat="server" Text="*" ForeColor="Red" ControlToValidate="txtNombre" ValidationGroup="vgAddUser"></asp:RequiredFieldValidator>
                                                    </div>
                                                </div>
                                                <div class="row">
                                                    <div class="col-lg-12 col-md-12 col-sm-12">
                                                        <asp:TextBox runat="server" ID="txtNombre" Text="" CssClass="form-control" placeholder="Nombre de Usuario"></asp:TextBox>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="col-lg-2 col-md-6 col-sm-12">
                                                <div class="row">
                                                    <div class="col-lg-12 col-md-12 col-sm-12">
                                                        <asp:Label runat="server" Text="Usuario" AssociatedControlID="txtUsuario" Width="100%"></asp:Label>
                                                        <asp:RequiredFieldValidator ID="rfvUsuario" runat="server" Text="*" ForeColor="Red" ControlToValidate="txtUsuario" ValidationGroup="vgAddUser"></asp:RequiredFieldValidator>
                                                    </div>
                                                </div>
                                                <div class="row">
                                                    <div class="col-lg-12 col-md-12 col-sm-12">
                                                        <asp:TextBox runat="server" ID="txtUsuario" Text="" CssClass="form-control" placeholder="Usuario"></asp:TextBox>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="col-lg-1 col-md-6 col-sm-12">
                                                <div class="row">
                                                    <div class="col-lg-12 col-md-12 col-sm-12">
                                                        <asp:Label runat="server" Text="Password" AssociatedControlID="txtPasswordUser" Width="100%"></asp:Label>
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator14" runat="server" Text="*" ForeColor="Red" ControlToValidate="txtPasswordUser" ValidationGroup="vgAddUser"></asp:RequiredFieldValidator>
                                                    </div>
                                                </div>
                                                <div class="row">
                                                    <div class="col-lg-12 col-md-12 col-sm-12">
                                                        <asp:TextBox runat="server" ID="txtPasswordUser" TextMode="Password" Text="" CssClass="form-control" placeholder="Password"></asp:TextBox>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="col-lg-2 col-md-6 col-sm-12">
                                                <div class="row">
                                                    <div class="col-lg-12 col-md-12 col-sm-12">
                                                        <asp:Label runat="server" Text="Email" ForeColor="DarkGreen"></asp:Label>
                                                        <asp:RequiredFieldValidator runat="server" ID="rfvEmailUser" ForeColor="Red" Text="*"
                                                            ControlToValidate="txtEmailUser" ValidationGroup="vgAddUser"></asp:RequiredFieldValidator>
                                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" Text="* Formato Email incorrecto" ValidationGroup="vgAgregarSitio" ForeColor="red"
                                                            ErrorMessage="Invalid Email" ControlToValidate="txtEmailUser" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"></asp:RegularExpressionValidator>
                                                    </div>
                                                </div>
                                                <div class="row">
                                                    <div class="col-lg-12 col-md-12 col-sm-12">
                                                        <asp:TextBox runat="server" ID="txtEmailUser" Text="" CssClass="form-control" placeholder="Email"></asp:TextBox>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="col-lg-2 col-md-6 col-sm-12">
                                                <div class="row">
                                                    <div class="col-lg-12 col-md-12 col-sm-12">
                                                        <asp:Label runat="server" Text="Tipo de Usuario" AssociatedControlID="ddlTipoUsuario" Width="100%"></asp:Label>
                                                        <asp:RequiredFieldValidator ID="rfvTipoUsuario" runat="server" Text="*" ForeColor="Red" InitialValue="0"
                                                            ControlToValidate="ddlTipoUsuario" ValidationGroup="vgAddUser"></asp:RequiredFieldValidator>
                                                    </div>
                                                </div>
                                                <div class="row">
                                                    <div class="col-lg-12 col-md-12 col-sm-12">
                                                        <asp:DropDownList ID="ddlTipoUsuario" runat="server" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlTipoUsuario_SelectedIndexChanged"></asp:DropDownList>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="col-lg-2 col-md-6 col-sm-12">
                                                <div class="row">
                                                    <div class="col-lg-12 col-md-12 col-sm-12">
                                                        <asp:Label runat="server" Text="Sitio" ForeColor="DarkGreen" AssociatedControlID="ddlSitioUser"></asp:Label>
                                                        <asp:RequiredFieldValidator runat="server" ID="rfvSitioUser" ForeColor="Red" ErrorMessage="*" Text="*" Enabled="false"
                                                            ControlToValidate="ddlSitioUser" ValidationGroup="vgAddUser" InitialValue="0"></asp:RequiredFieldValidator>
                                                    </div>
                                                </div>
                                                <div class="row">
                                                    <div class="col-lg-12 col-md-12 col-sm-12">
                                                        <asp:DropDownList ID="ddlSitioUser" Enabled="false" runat="server" CssClass="form-control"></asp:DropDownList>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="col-lg-1 col-md-6 col-sm-12">
                                                <div class="row">
                                                    <div class="col-lg-12 col-md-12 col-sm-12">
                                                    </div>
                                                </div>
                                                <div class="row">
                                                    <div class="col-lg-12 col-md-12 col-sm-12 text-right">
                                                        <asp:Button runat="server" ID="btnAgregar" CssClass="btn btn-success" Text="Agregar" OnClick="btnAgregar_Click" ValidationGroup="vgAddUser" />
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
                                                                OnPageIndexChanging="grvUsuario_PageIndexChanging" DataKeyNames="intUsuarioID,intSitioID,vchNombre" OnRowCancelingEdit="grvUsuario_RowCancelingEdit"
                                                                OnRowCommand="grvUsuario_RowCommand" OnRowEditing="grvUsuario_RowEditing" OnRowUpdating="grvUsuario_RowUpdating"
                                                                EmptyDataText="No hay resultado bajo el criterio de búsqueda.">
                                                                <Columns>
                                                                    <asp:BoundField DataField="intUsuarioID" HeaderText="ID" ReadOnly="true" ItemStyle-CssClass="hidden-md" HeaderStyle-CssClass="hidden-md" />
                                                                    <asp:TemplateField HeaderText="Nombre">
                                                                        <ItemTemplate>
                                                                            <asp:Label runat="server" ID="lblNombreUsuario" Text='<%#Eval("vchNombre") %>' />
                                                                        </ItemTemplate>
                                                                        <EditItemTemplate>
                                                                            <asp:TextBox ID="txtNombreUsuario" Width="100%" runat="server" Text='<%#Eval("vchNombre") %>' />
                                                                        </EditItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Usuario">
                                                                        <ItemTemplate>
                                                                            <asp:Label runat="server" ID="lblUsuario" Text='<%#Eval("vchUsuario") %>' />
                                                                        </ItemTemplate>
                                                                        <EditItemTemplate>
                                                                            <asp:TextBox ID="txtUsuario" Width="100%" runat="server" Text='<%#Eval("vchUsuario") %>' />
                                                                        </EditItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Contraseña">
                                                                        <ItemTemplate>
                                                                            <asp:Label runat="server" Visible="false" ID="lblPassword" Text='<%#Eval("vchPassword") %>' />
                                                                        </ItemTemplate>
                                                                        <EditItemTemplate>
                                                                            <asp:Label runat="server" ID="lblPass" Visible="false" Text='<%#Eval("vchPassword") %>' />
                                                                            <asp:TextBox ID="txtPass" Width="100%" TextMode="Password" runat="server" ToolTip="Si no se desea cambiar de contraseña mantener este campo en blanco." Text='<%#Eval("vchPassword") %>' />
                                                                        </EditItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Email">
                                                                        <ItemTemplate>
                                                                            <asp:Label runat="server" ID="lblEmailUser" Text='<%#Eval("vchEmail") %>' />
                                                                        </ItemTemplate>
                                                                        <EditItemTemplate>
                                                                            <asp:TextBox ID="txtEmailUser" Width="100%" runat="server" Text='<%#Eval("vchEmail") %>' />
                                                                        </EditItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:BoundField DataField="vchSitio" HeaderText="Sitio" ReadOnly="true" />
                                                                    <asp:BoundField DataField="vchTipoUsuario" HeaderText="Tipo de Usuario" ReadOnly="true" />
                                                                    <asp:TemplateField HeaderText="Editar" ItemStyle-HorizontalAlign="Center">
                                                                        <ItemTemplate>
                                                                            <asp:LinkButton ID="btnVisualizar" CausesValidation="false" CommandName="Edit" runat="server">
                                                                                    <i class="fa fa-pencil" aria-hidden="true" title="Editar" style="font-size:25px;"></i>
                                                                            </asp:LinkButton>
                                                                        </ItemTemplate>
                                                                        <EditItemTemplate>
                                                                            <asp:LinkButton ID="btnUpdateVarPacinete" runat="server" CommandName="Update" Text="Actualizar">
                                                                                    <i class="fa fa-floppy-o" aria-hidden="true"  title="Actualizar" style="font-size:25px;"></i>
                                                                            </asp:LinkButton>
                                                                            <asp:LinkButton ID="bntCancelEditPaciente" runat="server" CommandName="Cancel" Text="Cancelar">
                                                                                    <i class="fa fa-ban" aria-hidden="true" title="Cancelar" style="font-size:25px;"></i>
                                                                            </asp:LinkButton>
                                                                        </EditItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Modalidades" ItemStyle-HorizontalAlign="Center">
                                                                        <ItemTemplate>
                                                                            <asp:LinkButton ID="btnModalidades" CausesValidation="false" CommandName="Modalidades" CommandArgument='<%#Eval("intUsuarioID") %>' runat="server">
                                                                                    <i class="fa fa-stethoscope" aria-hidden="true" title="Editar" style="font-size:25px;"></i>
                                                                            </asp:LinkButton>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Estatus" ItemStyle-HorizontalAlign="Center">
                                                                        <ItemTemplate>
                                                                            <asp:ImageButton ID="imbEstatus" runat="server" BackColor="Transparent" Height="25px" Width="25px"
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

                        <div id="sistemaConfig" class="tab-pane">
                            <div class="widget-box">
                                <div class="widget-header">
                                    <h4 class="widget-title">Configuración General para el Sitio</h4>
                                </div>

                                <div class="widget-body">
                                    <div class="widget-main">
                                        <div class="row">
                                            <div class="col-lg-5 col-md-12 col-sm-12">
                                                <asp:Label runat="server" ID="lblSitioSistema" Text="Sitio: " ForeColor="DarkGreen" AssociatedControlID="ddlSitioSistema"></asp:Label>
                                                <asp:DropDownList runat="server" ID="ddlSitioSistema" AutoPostBack="true" OnSelectedIndexChanged="ddlSitioSistema_SelectedIndexChanged" CssClass="form-control"></asp:DropDownList>
                                                <asp:RequiredFieldValidator runat="server" ID="rfvSSistema" ErrorMessage="*" Text="Seleccionar sitio" ForeColor="Red" ControlToValidate="ddlSitioSistema" InitialValue="0" ValidationGroup="vgConfigSistema"></asp:RequiredFieldValidator>
                                            </div>
                                        </div>
                                        <hr />
                                        <div class="row form-group ">
                                            <div class="col-lg-5 col-md-12 col-sm-12 col-xs-12 text-right">
                                                <asp:RequiredFieldValidator runat="server" ID="rfvNameSite" ErrorMessage="* Campo requerido." ForeColor="Red" Text="* Campo requerido" ControlToValidate="txtNombreSitio" ValidationGroup="vgConfigSistema"></asp:RequiredFieldValidator>
                                                <asp:Label runat="server" Text="Nombre del Sitio" AssociatedControlID="txtNombreSitio"></asp:Label>
                                            </div>
                                            <div class="col-lg-7 col-md-12 col-sm-12 col-xs-12">
                                                <asp:TextBox runat="server" Text="" ID="txtNombreSitio" Enabled="false" placeholder="Nombre del Sitio" CssClass="form-control"></asp:TextBox>
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
                                                    <div class="col-lg-6 col-md-12 col-sm-12">
                                                        <asp:FileUpload runat="server" ID="fuLogo" AllowMultiple="false" />
                                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator7"
                                                            runat="server" ControlToValidate="fuLogo"
                                                            ErrorMessage="png,jpeg,gif,jpg" ForeColor="Red"
                                                            ValidationExpression="^.+(.jpeg|.jpg|.gif|.png)$"
                                                            ValidationGroup="vgConfigSistema" SetFocusOnError="true"></asp:RegularExpressionValidator>
                                                    </div>
                                                    <div class="col-lg-6 col-md-12 col-sm-12">
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
                                                <asp:TextBox runat="server" Text="" ID="txtPathRepositorio" placeholder="Versión dels Sistema" CssClass="form-control"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="row form-group">
                                            <div class="col-lg-12 text-right">
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
                                        <div class="row">
                                            <div class="col-lg-5 col-md-12 col-sm-12">
                                                <asp:Label runat="server" ID="Label1" Text="Sitio: " ForeColor="DarkGreen" AssociatedControlID="ddlSitioCorreo"></asp:Label>
                                                <asp:DropDownList runat="server" ID="ddlSitioCorreo" AutoPostBack="true" OnSelectedIndexChanged="ddlSitioCorreo_SelectedIndexChanged" CssClass="form-control"></asp:DropDownList>
                                                <asp:RequiredFieldValidator runat="server" ErrorMessage="*" Text="Seleccion sitio." ForeColor="Red" ControlToValidate="ddlSitioCorreo" InitialValue="0" ValidationGroup="vgEmailSistema"></asp:RequiredFieldValidator>
                                            </div>
                                        </div>
                                        <hr />
                                        <div class="row form-group ">
                                            <div class="col-lg-5 col-md-12 col-sm-12 col-xs-12 text-right">
                                                <asp:RequiredFieldValidator runat="server" ID="rfvEmailSistema" ErrorMessage="Campo requerido" ControlToValidate="txtEmailSistema" ForeColor="Red" ValidationGroup="vgEmailSistema"></asp:RequiredFieldValidator>
                                                <asp:RegularExpressionValidator ID="revEmail" runat="server" Text="* Formato Email incorrecto" ValidationGroup="vgEmailSistema" ForeColor="red"
                                                    ErrorMessage="Invalid Email" ControlToValidate="txtEmailSistema" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"></asp:RegularExpressionValidator>
                                                <asp:Label runat="server" Text="Cuenta de correo" AssociatedControlID="txtEmailSistema"></asp:Label>
                                            </div>
                                            <div class="col-lg-7 col-md-12 col-sm-12 col-xs-12">
                                                <asp:TextBox runat="server" Text="" ID="txtEmailSistema" placeholder="Email: correo@dominio.mx" CssClass="form-control"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="row form-group ">
                                            <div class="col-lg-5 col-md-12 col-sm-12 col-xs-12 text-right">
                                                <asp:RequiredFieldValidator ID="rfvPassword" runat="server" Text="* Capturar contraseña." ForeColor="Red" ControlToValidate="txtPasswordSistema" ValidationGroup="vgEmailSistema"></asp:RequiredFieldValidator>¿
                                                <asp:Label runat="server" Text="Contraseña del correo" AssociatedControlID="txtPasswordSistema"></asp:Label>
                                            </div>
                                            <div class="col-lg-7 col-md-12 col-sm-12 col-xs-12">
                                                <asp:TextBox runat="server" Text="" TextMode="Password" placeholder="Password" ID="txtPasswordSistema" CssClass="form-control"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="row form-group ">
                                            <div class="col-lg-5 col-md-12 col-sm-12 col-xs-12 text-right">
                                                <asp:RequiredFieldValidator ID="rfvHost" runat="server" Text="* Capturar Host." ForeColor="Red" ControlToValidate="txtHost" ValidationGroup="vgEmailSistema"></asp:RequiredFieldValidator>
                                                <asp:Label runat="server" Text="Host de Correo" AssociatedControlID="txtHost"></asp:Label>
                                            </div>
                                            <div class="col-lg-7 col-md-12 col-sm-12 col-xs-12">
                                                <asp:TextBox runat="server" Text="" ID="txtHost" placeholder="Host ejemplo: smtp.gmail.com" CssClass="form-control"></asp:TextBox>
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
                                                <asp:Label runat="server" Text="SSL"></asp:Label>
                                            </div>
                                            <div class="col-lg-7 col-md-12 col-sm-12 col-xs-12">
                                                <label>
                                                    <telerik:RadAjaxPanel ID="RadAjaxPanel1" runat="server">
                                                        <input runat="server" id="chkSSL" name="switch-field-1" class="ace ace-switch" type="checkbox" />
                                                        <span class="lbl" data-lbl="SI &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;NO"></span>
                                                    </telerik:RadAjaxPanel>
                                                </label>
                                            </div>
                                        </div>
                                        <div class="row form-group">
                                            <div class="col-lg-12 text-right">
                                                <asp:Button ID="btnSaveEmailSistema" runat="server" Text="Guardar" CssClass="btn btn-success" OnClick="btnSaveEmailSistema_Click" ValidationGroup="vgEmailSistema" />
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
                                                <asp:Label runat="server" ID="Label2" Text="Sitio: " ForeColor="DarkGreen" AssociatedControlID="ddlSitioVarAdi"></asp:Label>
                                                <asp:DropDownList runat="server" ID="ddlSitioVarAdi" AutoPostBack="true" OnSelectedIndexChanged="ddlSitioVarAdi_SelectedIndexChanged" CssClass="form-control"></asp:DropDownList>
                                            </div>
                                            <div class="col-lg-6 col-md-12 col-sm-12">
                                                <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator15" ErrorMessage="*" Text="Seleccionar sitio" ForeColor="Red" ControlToValidate="ddlSitioVarAdi" InitialValue="0" ValidationGroup="vgAddVarAdiPac"></asp:RequiredFieldValidator>
                                            </div>
                                        </div>
                                        <hr />
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
                                                                        <div class="col-lg-12 col-md-12 col-sm-12">
                                                                            <asp:Label runat="server" Text="Nombre de la Variable" AssociatedControlID="txtAddVarPac" Width="100%"></asp:Label>
                                                                            <asp:RequiredFieldValidator ID="rfvAddVarAdiPac" runat="server" ValidationGroup="vgAddVarAdiPac" ErrorMessage="* Campo requerido" ControlToValidate="txtAddVarPac" Text="* Campo requerido" ForeColor="Red"></asp:RequiredFieldValidator>
                                                                        </div>
                                                                    </div>
                                                                    <div class="row">
                                                                        <div class="col-lg-10 col-md-10 col-sm-10">
                                                                            <asp:TextBox runat="server" Text="" CssClass="form-control" ID="txtAddVarPac" Width="100%" placeholder="Nombre de la Variable"></asp:TextBox>
                                                                        </div>
                                                                        <div class="col-lg-2 col-md-2 col-sm-2  text-right">
                                                                            <asp:Button runat="server" ID="btnAddVarPac" Text="Guardar" OnClick="btnAddVarPac_Click" CssClass="btn btn-primary" ValidationGroup="vgAddVarAdiPac" />
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                            <div class="row form-group">
                                                                <asp:UpdatePanel runat="server">
                                                                    <ContentTemplate>
                                                                        <asp:Panel runat="server">
                                                                            <asp:GridView ID="grvAddPaciente" runat="server" AllowPaging="true" CssClass="table table-striped table-bordered"
                                                                                PageSize="10" AutoGenerateColumns="false" OnRowDataBound="grvAddPaciente_RowDataBound" Font-Size="10px"
                                                                                OnPageIndexChanging="grvAddPaciente_PageIndexChanging" DataKeyNames="intVariableAdiID" OnRowCancelingEdit="grvAddPaciente_RowCancelingEdit"
                                                                                OnRowCommand="grvAddPaciente_RowCommand" OnRowEditing="grvAddPaciente_RowEditing" OnRowUpdating="grvAddPaciente_RowUpdating"
                                                                                EmptyDataText="No hay resultado bajo el criterio de búsqueda.">
                                                                                <Columns>
                                                                                    <asp:BoundField DataField="intVariableAdiID" HeaderText="ID" ReadOnly="true" ItemStyle-CssClass="hidden-md" HeaderStyle-CssClass="hidden-md" />
                                                                                    <asp:TemplateField HeaderText="Nombre Variable">
                                                                                        <ItemTemplate>
                                                                                            <asp:Label runat="server" ID="lblNomVar" Text='<%#Eval("vchNombreVarAdi") %>' />
                                                                                        </ItemTemplate>
                                                                                        <EditItemTemplate>
                                                                                            <asp:TextBox ID="txtname" Width="100%" runat="server" Text='<%#Eval("vchNombreVarAdi") %>' />
                                                                                        </EditItemTemplate>
                                                                                    </asp:TemplateField>

                                                                                    <asp:TemplateField HeaderText="Editar" ItemStyle-HorizontalAlign="Center">
                                                                                        <ItemTemplate>
                                                                                            <asp:LinkButton ID="btnVisualizar" CausesValidation="false" CommandName="Edit" runat="server">
                                                                                                    <i class="fa fa-pencil" aria-hidden="true" title="Editar" style="font-size:25px;"></i>
                                                                                            </asp:LinkButton>
                                                                                        </ItemTemplate>
                                                                                        <EditItemTemplate>
                                                                                            <asp:LinkButton ID="btnUpdateVarPacinete" runat="server" CommandName="Update" Text="Actualizar">
                                                                                                    <i class="fa fa-floppy-o" aria-hidden="true"  title="Actualizar" style="font-size:25px;"></i>
                                                                                            </asp:LinkButton>
                                                                                            <asp:LinkButton ID="bntCancelEditPaciente" runat="server" CommandName="Cancel" Text="Cancelar">
                                                                                                    <i class="fa fa-ban" aria-hidden="true" title="Cancelar" style="font-size:25px;"></i>
                                                                                            </asp:LinkButton>
                                                                                        </EditItemTemplate>
                                                                                    </asp:TemplateField>
                                                                                    <asp:TemplateField HeaderText="Estatus" ItemStyle-HorizontalAlign="Center">
                                                                                        <ItemTemplate>
                                                                                            <asp:ImageButton ID="imbEstatus" runat="server" BackColor="Transparent" Height="25px" Width="25px"
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
                                                                        <div class="col-lg-12 col-md-12 col-sm-12">
                                                                            <asp:Label runat="server" Text="Nombre de la Variable" AssociatedControlID="txtNombreVarCita" Width="100%"></asp:Label>
                                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ValidationGroup="vgAddVarAdiCita" ErrorMessage="* Campo requerido" ControlToValidate="txtNombreVarCita" Text="* Campo requerido" ForeColor="Red"></asp:RequiredFieldValidator>
                                                                        </div>
                                                                    </div>
                                                                    <div class="row">
                                                                        <div class="col-lg-10 col-md-10 col-sm-10">
                                                                            <asp:TextBox runat="server" Text="" CssClass="form-control" ID="txtNombreVarCita" Width="100%" placeholder="Nombre de la Variable"></asp:TextBox>
                                                                        </div>
                                                                        <div class="col-lg-2 col-md-2 col-sm-2 text-right">
                                                                            <asp:Button runat="server" ID="btnAddVarCita" Text="Guardar" OnClick="btnAddVarCita_Click" CssClass="btn btn-primary" ValidationGroup="vgAddVarAdiCita" />
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                            <div class="row form-group">
                                                                <asp:UpdatePanel runat="server">
                                                                    <ContentTemplate>
                                                                        <asp:Panel runat="server">
                                                                            <asp:GridView ID="grvAddCita" runat="server" AllowPaging="true" CssClass="table table-striped table-bordered"
                                                                                PageSize="10" AutoGenerateColumns="false" OnRowDataBound="grvAddCita_RowDataBound" Font-Size="10px"
                                                                                OnPageIndexChanging="grvAddCita_PageIndexChanging" DataKeyNames="intVariableAdiID" OnRowCancelingEdit="grvAddCita_RowCancelingEdit"
                                                                                OnRowCommand="grvAddCita_RowCommand" OnRowEditing="grvAddCita_RowEditing" OnRowUpdating="grvAddCita_RowUpdating"
                                                                                EmptyDataText="No hay resultado bajo el criterio de búsqueda.">
                                                                                <Columns>
                                                                                    <asp:BoundField DataField="intVariableAdiID" HeaderText="ID" ReadOnly="true" ItemStyle-CssClass="hidden-md" HeaderStyle-CssClass="hidden-md" />
                                                                                    <asp:TemplateField HeaderText="Nombre Variable">
                                                                                        <ItemTemplate>
                                                                                            <asp:Label runat="server" ID="lblNomVar" Text='<%#Eval("vchNombreVarAdi") %>' />
                                                                                        </ItemTemplate>
                                                                                        <EditItemTemplate>
                                                                                            <asp:TextBox ID="txtname" Width="100%" runat="server" Text='<%#Eval("vchNombreVarAdi") %>' />
                                                                                        </EditItemTemplate>
                                                                                    </asp:TemplateField>

                                                                                    <asp:TemplateField HeaderText="Editar" ItemStyle-HorizontalAlign="Center">
                                                                                        <ItemTemplate>
                                                                                            <asp:LinkButton ID="btnVisualizar" CausesValidation="false" CommandName="Edit" runat="server">
                                                                                                    <i class="fa fa-pencil" aria-hidden="true" title="Editar" style="font-size:25px;"></i>
                                                                                            </asp:LinkButton>
                                                                                        </ItemTemplate>
                                                                                        <EditItemTemplate>
                                                                                            <asp:LinkButton ID="btnUpdateVarCita" runat="server" CommandName="Update" Text="Actualizar">
                                                                                                    <i class="fa fa-floppy-o" aria-hidden="true"  title="Actualizar" style="font-size:25px;"></i>
                                                                                            </asp:LinkButton>
                                                                                            <asp:LinkButton ID="bntCancelEditCita" runat="server" CommandName="Cancel" Text="Cancelar">
                                                                                                    <i class="fa fa-ban" aria-hidden="true" title="Cancelar" style="font-size:25px;"></i>
                                                                                            </asp:LinkButton>
                                                                                        </EditItemTemplate>
                                                                                    </asp:TemplateField>
                                                                                    <asp:TemplateField HeaderText="Estatus" ItemStyle-HorizontalAlign="Center">
                                                                                        <ItemTemplate>
                                                                                            <asp:ImageButton ID="imbEstatus" runat="server" BackColor="Transparent" Height="25px" Width="25px"
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
                                                                        <div class="col-lg-12 col-md-12 col-sm-12">
                                                                            <asp:Label runat="server" Text="Nombre de la Identificación" AssociatedControlID="txtIdentificacion" Width="100%"></asp:Label>
                                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator10" runat="server" ValidationGroup="vgAddVarId" ErrorMessage="* Campo requerido" ControlToValidate="txtIdentificacion" Text="* Campo requerido" ForeColor="Red"></asp:RequiredFieldValidator>
                                                                        </div>
                                                                    </div>
                                                                    <div class="row">
                                                                        <div class="col-lg-10 col-md-10 col-sm-10">
                                                                            <asp:TextBox runat="server" Text="" CssClass="form-control" ID="txtIdentificacion" Width="100%" placeholder="Nombre de la Identificación"></asp:TextBox>
                                                                        </div>
                                                                        <div class="col-lg-2 col-md-2 col-sm-2 text-right">
                                                                            <asp:Button runat="server" ID="btnAddVarID" Text="Guardar" OnClick="btnAddVarID_Click" CssClass="btn btn-primary" ValidationGroup="vgAddVarId" />
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                            <div class="row form-group">
                                                                <asp:UpdatePanel runat="server">
                                                                    <ContentTemplate>
                                                                        <asp:Panel runat="server">
                                                                            <asp:GridView ID="grvVarID" runat="server" AllowPaging="true" CssClass="table table-striped table-bordered"
                                                                                PageSize="10" AutoGenerateColumns="false" OnRowDataBound="grvVarID_RowDataBound" Font-Size="10px"
                                                                                OnPageIndexChanging="grvVarID_PageIndexChanging" DataKeyNames="intIdentificacionID" OnRowCancelingEdit="grvVarID_RowCancelingEdit"
                                                                                OnRowCommand="grvVarID_RowCommand" OnRowEditing="grvVarID_RowEditing" OnRowUpdating="grvVarID_RowUpdating"
                                                                                EmptyDataText="No hay resultado bajo el criterio de búsqueda.">
                                                                                <Columns>
                                                                                    <asp:BoundField DataField="intIdentificacionID" HeaderText="ID" ReadOnly="true" ItemStyle-CssClass="hidden-md" HeaderStyle-CssClass="hidden-md" />
                                                                                    <asp:TemplateField HeaderText="Nombre Identificación">
                                                                                        <ItemTemplate>
                                                                                            <asp:Label runat="server" ID="lblNomVar" Text='<%#Eval("vchNombreID") %>' />
                                                                                        </ItemTemplate>
                                                                                        <EditItemTemplate>
                                                                                            <asp:TextBox ID="txtname" Width="100%" runat="server" Text='<%#Eval("vchNombreID") %>' />
                                                                                        </EditItemTemplate>
                                                                                    </asp:TemplateField>

                                                                                    <asp:TemplateField HeaderText="Editar" ItemStyle-HorizontalAlign="Center">
                                                                                        <ItemTemplate>
                                                                                            <asp:LinkButton ID="btnVisualizar" CausesValidation="false" CommandName="Edit" runat="server">
                                                                                                    <i class="fa fa-pencil" aria-hidden="true" title="Editar" style="font-size:25px;"></i>
                                                                                            </asp:LinkButton>
                                                                                        </ItemTemplate>
                                                                                        <EditItemTemplate>
                                                                                            <asp:LinkButton ID="btnUpdateVarID" runat="server" CommandName="Update" Text="Actualizar">
                                                                                                    <i class="fa fa-floppy-o" aria-hidden="true"  title="Actualizar" style="font-size:25px;"></i>
                                                                                            </asp:LinkButton>
                                                                                            <asp:LinkButton ID="bntCancelEditID" runat="server" CommandName="Cancel" Text="Cancelar">
                                                                                                    <i class="fa fa-ban" aria-hidden="true" title="Cancelar" style="font-size:25px;"></i>
                                                                                            </asp:LinkButton>
                                                                                        </EditItemTemplate>
                                                                                    </asp:TemplateField>
                                                                                    <asp:TemplateField HeaderText="Estatus" ItemStyle-HorizontalAlign="Center">
                                                                                        <ItemTemplate>
                                                                                            <asp:ImageButton ID="imbEstatus" runat="server" BackColor="Transparent" Height="25px" Width="25px"
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
                                            <div class="col-lg-3 col-md-12 col-sm-12">
                                                <div class="row">
                                                    <div class="col-lg-12 col-md-12 col-sm-12">
                                                        <asp:Label runat="server" Text="Item del Catálogo" AssociatedControlID="txtItemCat" Width="100%"></asp:Label>
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" Text="* Capturar Nombre." ForeColor="Red" ControlToValidate="txtItemCat" ValidationGroup="vgAddCat"></asp:RequiredFieldValidator>
                                                    </div>
                                                </div>
                                                <div class="row">
                                                    <div class="col-lg-12 col-md-12 col-sm-12">
                                                        <asp:TextBox runat="server" ID="txtItemCat" Text="" CssClass="form-control" placeholder="Item del Catálogo"></asp:TextBox>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="col-lg-3 col-md-12 col-sm-12">
                                                <div class="row">
                                                    <div class="col-lg-12 col-md-12 col-sm-12">
                                                        <asp:Label runat="server" Text="Catálogo" AssociatedControlID="radCbxCatalogo" Width="100%"></asp:Label>
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" Text="* Seleccionar Catálogo." ForeColor="Red" InitialValue="0" ControlToValidate="radCbxCatalogo" ValidationGroup="vgAddCat"></asp:RequiredFieldValidator>
                                                    </div>
                                                </div>
                                                <div class="row">
                                                    <div class="col-lg-12 col-md-12 col-sm-12">
                                                        <telerik:RadComboBox ID="radCbxCatalogo" runat="server" RenderMode="Lightweight" ForeColor="DarkGreen" Width="100%" OnClientSelectedIndexChanged="Carga_Catalogo"></telerik:RadComboBox>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="col-lg-3 col-md-12 col-sm-12">
                                                <div class="row">
                                                    <div class="col-lg-12 col-md-12 col-sm-12">
                                                        <asp:Label runat="server" Text="Sitio" AssociatedControlID="radCbxSitioCatalogo" Width="100%"></asp:Label>
                                                        <asp:RequiredFieldValidator ID="rfvSitioCatalago" runat="server" Text="* Seleccionar Sitio." ForeColor="Red" InitialValue="0" ControlToValidate="radCbxSitioCatalogo" ValidationGroup="vgAddCat"></asp:RequiredFieldValidator>
                                                    </div>
                                                </div>
                                                <div class="row">
                                                    <div class="col-lg-12 col-md-12 col-sm-12">
                                                        <telerik:RadComboBox ID="radCbxSitioCatalogo" runat="server" RenderMode="Lightweight" ForeColor="DarkGreen" Width="100%" OnClientSelectedIndexChanged="Carga_Catalogo_sitio"></telerik:RadComboBox>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="col-lg-2 col-md-12 col-sm-12 text-right">
                                                <div class="row">
                                                    <div class="col-lg-12 col-md-12 col-sm-12">
                                                    </div>
                                                </div>
                                                <div class="row">
                                                    <div class="col-lg-12 col-md-12 col-sm-12">
                                                        <asp:Button runat="server" ID="btnAddItemCat" Text="Agregar" OnClick="btnAddItemCat_Click" CssClass="btn btn-success" ValidationGroup="vgAddCat" />
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                        <telerik:RadAjaxPanel runat="server" ID="ajxPanelAdminCat" OnAjaxRequest="ajxPanelAdminCat_AjaxRequest">
                                            <div class="row form-group">
                                                <asp:GridView ID="grvCatalogos" runat="server" AllowPaging="true" CssClass="table table-striped table-bordered"
                                                    PageSize="10" AutoGenerateColumns="false" OnRowDataBound="grvCatalogos_RowDataBound" Font-Size="10px"
                                                    OnPageIndexChanging="grvCatalogos_PageIndexChanging" DataKeyNames="vchCatalogoID" OnRowCancelingEdit="grvCatalogos_RowCancelingEdit"
                                                    OnRowCommand="grvCatalogos_RowCommand" OnRowEditing="grvCatalogos_RowEditing" OnRowUpdating="grvCatalogos_RowUpdating"
                                                    EmptyDataText="No hay resultado bajo el criterio de búsqueda.">
                                                    <Columns>
                                                        <asp:BoundField DataField="vchCatalogoID" HeaderText="ID" ReadOnly="true" ItemStyle-CssClass="hidden-md" HeaderStyle-CssClass="hidden-md" />
                                                        <asp:TemplateField>
                                                            <ItemTemplate>
                                                                <asp:Label runat="server" ID="lblNomVar" Text='<%#Eval("vchCatalogo") %>' />
                                                            </ItemTemplate>
                                                            <EditItemTemplate>
                                                                <asp:TextBox runat="server" ID="txtItemNombre" Width="100%" Text='<%#Eval("vchCatalogo") %>'></asp:TextBox>
                                                            </EditItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Editar" ItemStyle-HorizontalAlign="Center">
                                                            <ItemTemplate>
                                                                <asp:LinkButton ID="btnVisualizar" CausesValidation="false" CommandName="Edit" runat="server">
                                                                    <i class="fa fa-pencil" aria-hidden="true" title="Editar" style="font-size:25px;"></i>
                                                                </asp:LinkButton>
                                                            </ItemTemplate>
                                                            <EditItemTemplate>
                                                                <asp:LinkButton ID="btnUpdateVarPacinete" runat="server" CommandName="Update" Text="Actualizar">
                                                                    <i class="fa fa-floppy-o" aria-hidden="true"  title="Actualizar" style="font-size:25px;"></i>
                                                                </asp:LinkButton>
                                                                <asp:LinkButton ID="bntCancelEditPaciente" runat="server" CommandName="Cancel" Text="Cancelar">
                                                                    <i class="fa fa-ban" aria-hidden="true" title="Cancelar" style="font-size:25px;"></i>
                                                                </asp:LinkButton>
                                                            </EditItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Estatus" ItemStyle-HorizontalAlign="Center">
                                                            <ItemTemplate>
                                                                <asp:ImageButton ID="imbEstatus" runat="server" BackColor="Transparent" Height="25px" Width="25px"
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
                                            </div>
                                        </telerik:RadAjaxPanel>
                                    </div>
                                </div>
                            </div>
                        </div>

                        <div id="ModalityConfig" class="tab-pane">
                            <div class="widget-box">
                                <div class="widget-header">
                                    <h4 class="widget-title">Administración de Modalidades, Prestaciones y Equipos</h4>
                                </div>

                                <div class="widget-body">
                                    <div class="widget-main">
                                        <div class="row">
                                            <div class="col-lg-12 col-md-12 col-sm-12">
                                                <div class="widget-box">
                                                    <div class="widget-header">
                                                        <h4 class="widget-title">Prestaciones</h4>

                                                        <div class="widget-toolbar">
                                                            <a href="#" data-action="collapse">
                                                                <i class="ace-icon fa fa-chevron-up"></i>
                                                            </a>
                                                        </div>
                                                    </div>

                                                    <div class="widget-body">
                                                        <div class="widget-main">
                                                            <div class="row form-group">
                                                                <div class="col-lg-3 col-sm-12 text-left">
                                                                    <div class="row">
                                                                        <div class="col-lg-12">
                                                                            <asp:Label runat="server" Text="Nombre de la prestación" AssociatedControlID="txtPrestacion" Width="100%"></asp:Label>
                                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" Text="* Campo requerido." ForeColor="Red" ControlToValidate="txtPrestacion" ValidationGroup="vgAddPres"></asp:RequiredFieldValidator>
                                                                        </div>
                                                                    </div>
                                                                    <div class="row">
                                                                        <div class="col-lg-12 col-md-12 col-sm-12">
                                                                            <asp:TextBox runat="server" ID="txtPrestacion" Text="" CssClass="form-control" placeholder="Prestación"></asp:TextBox>
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                                <div class="col-lg-2 col-sm-12 text-left">
                                                                    <div class="row">
                                                                        <div class="col-lg-12 col-md-12 col-sm-12">
                                                                            <asp:Label runat="server" Text="Duración de la prestación" AssociatedControlID="txtDuracionPres" Width="100%"></asp:Label>
                                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" Text="* Campo requerido." ForeColor="Red" ControlToValidate="txtDuracionPres" ValidationGroup="vgAddPres"></asp:RequiredFieldValidator>
                                                                        </div>
                                                                    </div>
                                                                    <div class="row">
                                                                        <div class="col-lg-12 col-md-12 col-sm-12">
                                                                            <asp:TextBox ID="txtDuracionPres" runat="server" CssClass="form-control" TextMode="Number" placeholder="Duración (minutos)"></asp:TextBox>
                                                                            <asp:RangeValidator ID="rangev" runat="server" ControlToValidate="txtDuracionPres" Type="Integer" ErrorMessage="Valor debe estar entre 1 y 120 minutos" MinimumValue="1" MaximumValue="120" ValidationGroup="vgAddPres"></asp:RangeValidator>
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                                <div class="col-lg-3 col-sm-12 text-left">
                                                                    <div class="row">
                                                                        <div class="col-lg-12 col-md-12 col-sm-12">
                                                                            <asp:Label runat="server" ID="Label3" Text="Sitio: " ForeColor="DarkGreen" AssociatedControlID="ddlSitioMod"></asp:Label>
                                                                        </div>
                                                                    </div>
                                                                    <div class="row">
                                                                        <div class="col-lg-12 col-md-12 col-sm-12">
                                                                            <telerik:RadComboBox runat="server" ID="ddlSitioMod" OnClientSelectedIndexChanged="Combo_SitioMod" RenderMode="Lightweight" ForeColor="DarkGreen" Width="100%"></telerik:RadComboBox>
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                                <div class="col-lg-3 col-sm-12 text-left">
                                                                    <div class="row">
                                                                        <div class="col-lg-12 col-md-12 col-sm-12">
                                                                            <asp:Label runat="server" Text="Modalidad" AssociatedControlID="ddlModalidad" Width="100%"></asp:Label>
                                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" Text="* Seleccionar modalidad." ForeColor="Red" InitialValue="0" ControlToValidate="ddlModalidad" ValidationGroup="vgAddPres"></asp:RequiredFieldValidator>
                                                                        </div>
                                                                    </div>
                                                                    <div class="row">
                                                                        <div class="col-lg-12 col-md-12 col-sm-12">
                                                                            <telerik:RadComboBox ID="ddlModalidad" runat="server" RenderMode="Lightweight" ForeColor="DarkGreen" Width="100%" OnClientSelectedIndexChanged="Combo_Modalidad"></telerik:RadComboBox>
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                                <div class="col-lg-1 col-sm-12 text-center">
                                                                    <asp:Button runat="server" ID="btnAddPres" Text="Agregar" OnClick="btnAddPres_Click" CssClass="btn btn-success" ValidationGroup="vgAddPres" />
                                                                </div>
                                                            </div>
                                                            <telerik:RadAjaxPanel runat="server" OnAjaxRequest="radAjaxPanel2_AjaxRequest" ID="radAjaxPanel2">
                                                                <div class="row">
                                                                    <asp:GridView ID="grvPrestaciones" runat="server" AllowPaging="true" CssClass="table table-striped table-bordered"
                                                                        PageSize="10" AutoGenerateColumns="false" OnRowDataBound="grvPrestaciones_RowDataBound" Font-Size="10px"
                                                                        OnPageIndexChanging="grvPrestaciones_PageIndexChanging" DataKeyNames="intRELModPres, intPrestacionID, intSitioID" OnRowCancelingEdit="grvPrestaciones_RowCancelingEdit"
                                                                        OnRowCommand="grvPrestaciones_RowCommand" OnRowEditing="grvPrestaciones_RowEditing" OnRowUpdating="grvPrestaciones_RowUpdating"
                                                                        EmptyDataText="No hay resultado bajo el criterio de búsqueda.">
                                                                        <Columns>
                                                                            <asp:BoundField DataField="intRELModPres" HeaderText="ID" ReadOnly="true" ItemStyle-CssClass="hidden-md" HeaderStyle-CssClass="hidden-md" />
                                                                            <asp:TemplateField HeaderText="Prestación">
                                                                                <ItemTemplate>
                                                                                    <asp:Label runat="server" ID="lblNomVar" Text='<%#Eval("vchPrestacion") %>' />
                                                                                </ItemTemplate>
                                                                                <EditItemTemplate>
                                                                                    <asp:TextBox runat="server" ID="txtItemNombre" Width="100%" Text='<%#Eval("vchPrestacion") %>'></asp:TextBox>
                                                                                </EditItemTemplate>
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderText="Duración (minutos)">
                                                                                <ItemTemplate>
                                                                                    <asp:Label runat="server" ID="lblDuracion" Text='<%#Eval("intDuracionMin") %>' />
                                                                                </ItemTemplate>
                                                                                <EditItemTemplate>
                                                                                    <asp:TextBox runat="server" ID="txtDuracionItem" TextMode="Number" Width="100%" Text='<%#Eval("intDuracionMin") %>'></asp:TextBox>
                                                                                </EditItemTemplate>
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderText="Editar" ItemStyle-HorizontalAlign="Center">
                                                                                <ItemTemplate>
                                                                                    <asp:LinkButton ID="btnVisualizar" CausesValidation="false" CommandName="Edit" runat="server">
                                                                                        <i class="fa fa-pencil" aria-hidden="true" title="Editar" style="font-size:25px;"></i>
                                                                                    </asp:LinkButton>
                                                                                </ItemTemplate>
                                                                                <EditItemTemplate>
                                                                                    <asp:LinkButton ID="btnUpdateVarPacinete" runat="server" CommandName="Update" Text="Actualizar">
                                                                                        <i class="fa fa-floppy-o" aria-hidden="true"  title="Actualizar" style="font-size:25px;"></i>
                                                                                    </asp:LinkButton>
                                                                                    <asp:LinkButton ID="bntCancelEditPaciente" runat="server" CommandName="Cancel" Text="Cancelar">
                                                                                        <i class="fa fa-ban" aria-hidden="true" title="Cancelar" style="font-size:25px;"></i>
                                                                                    </asp:LinkButton>
                                                                                </EditItemTemplate>
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderText="Indicaciones" ItemStyle-HorizontalAlign="Center">
                                                                                <ItemTemplate>
                                                                                    <asp:LinkButton ID="imbIndicaciones" runat="server" BackColor="Transparent" Height="25px" Width="25px"
                                                                                        CommandArgument='<%#Eval("intPrestacionID") %>' CommandName="Indicacion" ToolTip="Ver las indicaciones">
                                                                                        <i class="fa fa-comments-o" aria-hidden="true" title="Indicaciones" style="font-size:25px;"></i>
                                                                                    </asp:LinkButton>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderText="Cuestionario" ItemStyle-HorizontalAlign="Center">
                                                                                <ItemTemplate>
                                                                                    <asp:LinkButton ID="imbCuestionario" runat="server" BackColor="Transparent" Height="25px" Width="25px"
                                                                                        CommandArgument='<%#Eval("intPrestacionID") %>' CommandName="Cuestionario" ToolTip="Ver los cuestionarios">
                                                                                        <i class="fa fa-question" aria-hidden="true" title="Cuestionario" style="font-size:25px;"></i>
                                                                                    </asp:LinkButton>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderText="Restricciones" ItemStyle-HorizontalAlign="Center">
                                                                                <ItemTemplate>
                                                                                    <asp:LinkButton ID="imbRestricciones" runat="server" BackColor="Transparent" Height="25px" Width="25px"
                                                                                        CommandArgument='<%#Eval("intPrestacionID") %>' CommandName="Restricciones" ToolTip="Ver las Restricciones">
                                                                                        <i class="fa fa-ban" aria-hidden="true" title="Restricciones" style="font-size:25px;"></i>
                                                                                    </asp:LinkButton>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderText="Estatus" ItemStyle-HorizontalAlign="Center">
                                                                                <ItemTemplate>
                                                                                    <asp:ImageButton ID="imbEstatus" runat="server" BackColor="Transparent" Height="25px" Width="25px"
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
                                                                </div>
                                                            </telerik:RadAjaxPanel>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="row">
                                            <div class="col-lg-12 col-md-12 col-sm-12">
                                                <div class="widget-box">
                                                    <div class="widget-header">
                                                        <h4 class="widget-title">Equipos</h4>

                                                        <div class="widget-toolbar">
                                                            <a href="#" data-action="collapse">
                                                                <i class="ace-icon fa fa-chevron-up"></i>
                                                            </a>
                                                        </div>
                                                    </div>

                                                    <div class="widget-body">
                                                        <div class="widget-main">
                                                            <div class="row form-group">
                                                                <div class="col-lg-2 col-sm-12 text-left">
                                                                    <div class="row">
                                                                        <div class="col-lg-12 col-md-12 col-sm-12">
                                                                            <asp:Label runat="server" Text="Nombre del Equipo" AssociatedControlID="txtNomEquipo" Width="100%"></asp:Label>
                                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" Text="* Campo requerido." ForeColor="Red" ControlToValidate="txtNomEquipo" ValidationGroup="vgAddEquipo"></asp:RequiredFieldValidator>
                                                                        </div>
                                                                    </div>
                                                                    <div class="row">
                                                                        <div class="col-lg-12 col-md-12 col-sm-12">
                                                                            <asp:TextBox runat="server" ID="txtNomEquipo" Text="" CssClass="form-control" placeholder="Nombre del Equipo"></asp:TextBox>
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                                <div class="col-lg-2 col-sm-12 text-left">
                                                                    <div class="row">
                                                                        <div class="col-lg-12 col-md-12 col-sm-12">
                                                                            <asp:Label runat="server" Text="Código del Equipo" AssociatedControlID="txtCodeequipo" Width="100%"></asp:Label>
                                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator11" runat="server" Text="* Campo requerido." ForeColor="Red" ControlToValidate="txtCodeequipo" ValidationGroup="vgAddEquipo"></asp:RequiredFieldValidator>
                                                                        </div>
                                                                    </div>
                                                                    <div class="row">
                                                                        <div class="col-lg-12 col-md-12 col-sm-12">
                                                                            <asp:TextBox runat="server" ID="txtCodeequipo" Text="" CssClass="form-control" placeholder="Código de equipo"></asp:TextBox>
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                                <div class="col-lg-2 col-sm-12 text-left">
                                                                    <div class="row">
                                                                        <div class="col-lg-12 col-md-12 col-sm-12">
                                                                            <asp:Label runat="server" Text="AETitle del Equipo" AssociatedControlID="txtAEtitle" Width="100%"></asp:Label>
                                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" Text="* Campo requerido." ForeColor="Red" ControlToValidate="txtAEtitle" ValidationGroup="vgAddEquipo"></asp:RequiredFieldValidator>
                                                                        </div>
                                                                    </div>
                                                                    <div class="row">
                                                                        <div class="col-lg-12 col-md-12 col-sm-12">
                                                                            <asp:TextBox runat="server" ID="txtAEtitle" Text="" CssClass="form-control" placeholder="AETitle del Equipo"></asp:TextBox>
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                                <div class="col-lg-2 col-sm-12 text-left">
                                                                    <div class="row">
                                                                        <div class="col-lg-12 col-md-12 col-sm-12">
                                                                            <asp:Label runat="server" ID="Label4" Text="Sitio: " ForeColor="DarkGreen" AssociatedControlID="ddlSitioModEquipo"></asp:Label>
                                                                        </div>
                                                                    </div>
                                                                    <div class="row">
                                                                        <div class="col-lg-12 col-md-12 col-sm-12">
                                                                            <telerik:RadComboBox runat="server" ID="ddlSitioModEquipo" OnClientSelectedIndexChanged="ddlSitioModEquipo_SelectedIndexChanged" RenderMode="Lightweight" ForeColor="DarkGreen" Width="100%"></telerik:RadComboBox>
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                                <div class="col-lg-2 col-sm-12 text-left">
                                                                    <div class="row">
                                                                        <div class="col-lg-12 col-md-12 col-sm-12">
                                                                            <asp:Label runat="server" Text="Modalidad" AssociatedControlID="ddlModalidadEquipo" Width="100%"></asp:Label>
                                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator9" runat="server" Text="* Seleccionar modalidad." ForeColor="Red" InitialValue="0" ControlToValidate="ddlModalidadEquipo" ValidationGroup="vgAddEquipo"></asp:RequiredFieldValidator>
                                                                        </div>
                                                                    </div>
                                                                    <div class="row">
                                                                        <div class="col-lg-12 col-md-12 col-sm-12">
                                                                            <telerik:RadAjaxPanel runat="server" ID="AjaxPanelModalidadEquipo" OnAjaxRequest="AjaxPanelModalidadEquipo_AjaxRequest">
                                                                                <telerik:RadComboBox ID="ddlModalidadEquipo" runat="server" RenderMode="Lightweight" ForeColor="DarkGreen" Width="100%" OnClientSelectedIndexChanged="ComboMod_Equipo"></telerik:RadComboBox>
                                                                            </telerik:RadAjaxPanel>
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                                <div class="col-lg-2 col-sm-12  text-center">
                                                                    <asp:Button runat="server" ID="btnAddEquipo" Text="Agregar" OnClick="btnAddEquipo_Click" CssClass="btn btn-success" ValidationGroup="vgAddEquipo" />
                                                                    <asp:Button runat="server" ID="btnSearchEquipo" Text="Buscar" OnClick="btnSearchEquipo_Click" CssClass="btn btn-info" />
                                                                </div>
                                                            </div>
                                                            <div class="row">
                                                                <telerik:RadAjaxPanel runat="server" ID="AjaxPanelEquipo" OnAjaxRequest="AjaxPanelEquipo_AjaxRequest">
                                                                    <asp:GridView ID="grvEquipo" runat="server" AllowPaging="true" CssClass="table table-striped table-bordered"
                                                                        PageSize="10" AutoGenerateColumns="false" OnRowDataBound="grvEquipo_RowDataBound" Font-Size="10px"
                                                                        OnPageIndexChanging="grvEquipo_PageIndexChanging" DataKeyNames="intEquipoID,intModalidadID,intSitioID" OnRowCancelingEdit="grvEquipo_RowCancelingEdit"
                                                                        OnRowCommand="grvEquipo_RowCommand" OnRowEditing="grvEquipo_RowEditing" OnRowUpdating="grvEquipo_RowUpdating"
                                                                        EmptyDataText="No hay resultado bajo el criterio de búsqueda.">
                                                                        <Columns>
                                                                            <asp:BoundField DataField="intEquipoID" HeaderText="ID" ReadOnly="true" ItemStyle-CssClass="hidden-md" HeaderStyle-CssClass="hidden-md" />
                                                                            <asp:TemplateField HeaderText="Equipo">
                                                                                <ItemTemplate>
                                                                                    <asp:Label runat="server" ID="lblNomEquipo" Text='<%#Eval("vchNombreEquipo") %>' />
                                                                                </ItemTemplate>
                                                                                <EditItemTemplate>
                                                                                    <asp:TextBox runat="server" ID="txtItemNombre" Width="100%" Text='<%#Eval("vchNombreEquipo") %>'></asp:TextBox>
                                                                                </EditItemTemplate>
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderText="Código">
                                                                                <ItemTemplate>
                                                                                    <asp:Label runat="server" ID="lblCodigo" Text='<%#Eval("vchCodigoEquipo") %>' />
                                                                                </ItemTemplate>
                                                                                <EditItemTemplate>
                                                                                    <asp:TextBox runat="server" ID="txtCodigoItem" Width="100%" Text='<%#Eval("vchCodigoEquipo") %>'></asp:TextBox>
                                                                                </EditItemTemplate>
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderText="AETitle">
                                                                                <ItemTemplate>
                                                                                    <asp:Label runat="server" ID="lblAETILTE" Text='<%#Eval("vchAETitle") %>' />
                                                                                </ItemTemplate>
                                                                                <EditItemTemplate>
                                                                                    <asp:TextBox runat="server" ID="txtAEtitleItem" Width="100%" Text='<%#Eval("vchAETitle") %>'></asp:TextBox>
                                                                                </EditItemTemplate>
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderText="Editar" ItemStyle-HorizontalAlign="Center">
                                                                                <ItemTemplate>
                                                                                    <asp:LinkButton ID="btnVisualizar" CausesValidation="false" CommandName="Edit" runat="server">
                                                                                        <i class="fa fa-pencil" aria-hidden="true" title="Editar" style="font-size:25px;"></i>
                                                                                    </asp:LinkButton>
                                                                                </ItemTemplate>
                                                                                <EditItemTemplate>
                                                                                    <asp:LinkButton ID="btnUpdateEquipo" runat="server" CommandName="Update" Text="Actualizar">
                                                                                        <i class="fa fa-floppy-o" aria-hidden="true"  title="Actualizar" style="font-size:25px;"></i>
                                                                                    </asp:LinkButton>
                                                                                    <asp:LinkButton ID="bntCancelEditEquipo" runat="server" CommandName="Cancel" Text="Cancelar">
                                                                                        <i class="fa fa-ban" aria-hidden="true" title="Cancelar" style="font-size:25px;"></i>
                                                                                    </asp:LinkButton>
                                                                                </EditItemTemplate>
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderText="Estatus" ItemStyle-HorizontalAlign="Center">
                                                                                <ItemTemplate>
                                                                                    <asp:ImageButton ID="imbEstatus" runat="server" BackColor="Transparent" Height="25px" Width="25px"
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
                                                                </telerik:RadAjaxPanel>
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

                        <div id="Adicionales" class="tab-pane">
                            <div class="widget-box">
                                <div class="widget-header">
                                    <h4 class="widget-title">Adicionales</h4>
                                </div>

                                <div class="widget-body">
                                    <div class="widget-main">
                                        <div class="row form-group">
                                            <div class="col-lg-6 col-md-12 col-sm-12 col-sm-12">
                                                <div class="row form-group">
                                                    <div class="col-lg-12 col-md-12 col-sm-12">
                                                        <asp:Label runat="server" ID="Label5" Text="Sitio" AssociatedControlID="ddlSitioAdicionales"></asp:Label>
                                                        <telerik:RadComboBox runat="server" ID="ddlSitioAdicionales" RenderMode="Lightweight" ForeColor="DarkGreen" Width="100%" OnClientSelectedIndexChanged="Combo_SitioAdicional"></telerik:RadComboBox>
                                                    </div>
                                                </div>
                                                <div class="row form-group">
                                                    <div class="col-lg-12 col-md-12 col-sm-12">
                                                        <asp:Label runat="server" ID="lblNomAdi" Text="Nombre" AssociatedControlID="txtNomAdi"></asp:Label>
                                                        <asp:RequiredFieldValidator runat="server" ID="rfvNomAdi" Text="* Nombre" ErrorMessage="* Nombre" ForeColor="Red" ControlToValidate="txtNomAdi" ValidationGroup="vgAdicional"></asp:RequiredFieldValidator>
                                                        <asp:TextBox runat="server" Text="" ID="txtNomAdi" CssClass="form-control" placeholder="Nombre"></asp:TextBox>
                                                    </div>
                                                </div>
                                                <div class="row form-group">
                                                    <div class="col-lg-12 col-md-12 col-sm-12">
                                                        <asp:Label runat="server" ID="lblURL" Text="Imagen" AssociatedControlID="RadFileUp"></asp:Label>
                                                        <telerik:RadAsyncUpload runat="server" ID="RadFileUp" Width="100%" MaxFileInputsCount="1" Font-Size="12"  AllowedFileExtensions=".png,.jpeg,.gif,.jpg" RenderMode="Lightweight" ForeColor="DarkGreen"></telerik:RadAsyncUpload>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="col-lg-6 col-md-12 col-sm-12 col-sm-12">
                                                <div class="row form-group">
                                                    <div class="col-lg-12 col-md-12 col-sm-12">
                                                        <asp:Label runat="server" ID="lblTipoControl" Text="Tipo de Control" AssociatedControlID="ddlTipoControl"></asp:Label>
                                                        <telerik:RadComboBox runat="server" RenderMode="Lightweight" ForeColor="DarkGreen" Width="100%" ID="ddlTipoControl"></telerik:RadComboBox>
                                                    </div>
                                                </div>
                                                <div class="row form-group">
                                                    <div class="col-lg-12 col-md-12 col-sm-12">
                                                        <asp:Label runat="server" ID="lblTipoAdicional" Text="Tipo de Variable" AssociatedControlID="ddlTipoVariable"></asp:Label>
                                                        <telerik:RadComboBox runat="server" RenderMode="Lightweight" ForeColor="DarkGreen" Width="100%" ID="ddlTipoVariable" OnClientSelectedIndexChanged="ComboTipoVar"></telerik:RadComboBox>
                                                    </div>
                                                </div>
                                                <div class="row form-group">
                                                    <div class="col-lg-12 col-md-12 col-sm-12">
                                                        <asp:Label runat="server" ID="lblObservac" Text="¿Se requiere capturar observaciones?" AssociatedControlID="chkObservaciones"></asp:Label>
                                                        <asp:CheckBox runat="server" Text="SI" ID="chkObservaciones" ToolTip="¿Requiere de Observaciones?" CssClass="form-control"></asp:CheckBox>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="row form-group">
                                            <div class="col-lg-6 col-md-12 col-sm-12">
                                                <telerik:RadComboBox RenderMode="Lightweight" ID="radComboxGenero" runat="server" CheckBoxes="true" Width="100%" EnableCheckAllItemsCheckBox="true" Label="Activo por Genero para:">
                                                    <Localization AllItemsCheckedString="Todos..." ItemsCheckedString="Todos..." CheckAllString="Todos..."/>
                                                    <Items>
                                                        <telerik:RadComboBoxItem Text="Hombre" Value="1"/>
                                                        <telerik:RadComboBoxItem Text="Mujer" Value="2" />
                                                    </Items>
                                                </telerik:RadComboBox>
                                                <asp:RequiredFieldValidator runat="server" Text="*" ErrorMessage="*" ForeColor="Red" ControlToValidate="radComboxGenero" ValidationGroup="vgAdicional"></asp:RequiredFieldValidator>
                                                <telerik:RadComboBox RenderMode="Lightweight" ID="radComboEdad" runat="server" CheckBoxes="true" Width="100%" EnableCheckAllItemsCheckBox="true" Label="Activo por Edad para:">
                                                    <Localization AllItemsCheckedString="Todos.." ItemsCheckedString="Todos..."  CheckAllString="Todos..."/>
                                                    <Items>
                                                        <telerik:RadComboBoxItem Text="Adulto Mayor" Value="3"/>
                                                        <telerik:RadComboBoxItem Text="Menor" Value="4" />
                                                    </Items>
                                                </telerik:RadComboBox>
                                                <asp:RequiredFieldValidator runat="server" Text="*" ErrorMessage="*" ForeColor="Red" ControlToValidate="radComboEdad" ValidationGroup="vgAdicional"></asp:RequiredFieldValidator>
                                            </div>
                                            <div class="col-lg-6 col-md-12 col-sm-12 text-right">
                                                <asp:Button runat="server" ID="btnAgregarAdicional" Text="Agregar" CssClass="btn btn-success" ValidationGroup="vgAdicional" OnClick="btnAgregarAdicional_Click" />
                                            </div>
                                        </div>
                                        <hr />
                                        <telerik:RadAjaxPanel runat="server" ID="radAjaxPanelAdicionales" OnAjaxRequest="radAjaxPanelAdicionales_AjaxRequest">
                                            <div class="row form-group">
                                                <asp:GridView ID="grvAdicional" runat="server" AllowPaging="true" CssClass="table table-striped table-bordered"
                                                    PageSize="10" AutoGenerateColumns="false" OnRowDataBound="grvAdicional_RowDataBound" Font-Size="10px"
                                                    OnPageIndexChanging="grvAdicional_PageIndexChanging" DataKeyNames="intAdicionalesID, intTipoAdicionalID" OnRowCancelingEdit="grvAdicional_RowCancelingEdit"
                                                    OnRowCommand="grvAdicional_RowCommand" OnRowEditing="grvAdicional_RowEditing" OnRowUpdating="grvAdicional_RowUpdating"
                                                    EmptyDataText="No hay resultado bajo el criterio de búsqueda.">
                                                    <Columns>
                                                        <asp:BoundField DataField="intAdicionalesID" HeaderText="ID" ReadOnly="true" ItemStyle-CssClass="hidden-md" HeaderStyle-CssClass="hidden-md" />
                                                        <asp:TemplateField HeaderText="Nombre">
                                                            <ItemTemplate>
                                                                <asp:Label runat="server" ID="lblNomVar" Text='<%#Eval("vchNombreAdicional") %>' />
                                                            </ItemTemplate>
                                                            <EditItemTemplate>
                                                                <asp:TextBox runat="server" ID="txtItemNombre" Width="100%" Text='<%#Eval("vchNombreAdicional") %>'></asp:TextBox>
                                                            </EditItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Imagen">
                                                            <ItemTemplate>
                                                                <asp:Label runat="server" ID="lblImagen" Text='<%#Eval("vchURLImagen") %>' />
                                                            </ItemTemplate>
                                                            <EditItemTemplate>
                                                                <asp:Label runat="server" ID="txtImagenItem" Width="100%" Text='<%#Eval("vchURLImagen") %>'></asp:Label>
                                                                <telerik:RadAsyncUpload runat="server" ID="RadFileUpItem" Width="100%" MaxFileInputsCount="1" Font-Size="12"  AllowedFileExtensions=".png,.jpeg,.gif,.jpg" RenderMode="Lightweight" ForeColor="DarkGreen"></telerik:RadAsyncUpload>
                                                            </EditItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Tipo Control">
                                                            <ItemTemplate>
                                                                <asp:Label runat="server" ID="lblControl" Text='<%#Eval("vchTipoBoton") %>' />
                                                            </ItemTemplate>
                                                            <EditItemTemplate>
                                                                <asp:Label ID="lblControl" runat="server" Text='<%# Eval("vchTipoBoton")%>' Visible="false"></asp:Label>
                                                                <asp:DropDownList runat="server" ID="ddlTipoControlITem" Width="100%"></asp:DropDownList>
                                                            </EditItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Tipo Variable">
                                                            <ItemTemplate>
                                                                <asp:Label runat="server" ID="lblAdicional" Text='<%#Eval("vchTipoAdicional") %>' />
                                                            </ItemTemplate>
                                                            <EditItemTemplate>
                                                                <asp:Label runat="server" ID="lblAdicional" Text='<%#Eval("vchTipoAdicional") %>' Visible="false" />
                                                                <asp:DropDownList runat="server" ID="ddlTipoAdicionalItem" Width="100%"></asp:DropDownList>
                                                            </EditItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Observaciones">
                                                            <ItemTemplate>
                                                                <asp:CheckBox ID="chkObsItem" runat="server" Checked='<%#Eval("bitObservaciones")%>' Enabled="false" />
                                                            </ItemTemplate>
                                                            <EditItemTemplate>
                                                                <asp:CheckBox runat="server" ID="chkObsItem" Width="100%" Checked='<%#Eval("bitObservaciones")%>'></asp:CheckBox>
                                                            </EditItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Activar: " ItemStyle-HorizontalAlign="Center">
                                                            <ItemTemplate>
                                                                <asp:LinkButton ID="btnActivar" runat="server" CommandName="Activar" CommandArgument='<%#Eval("intAdicionalesID") %>' Text="Actualizar">
                                                                    <i class="fa fa-external-link-square" aria-hidden="true"  title="Activar para: " style="font-size:25px;"></i>
                                                                </asp:LinkButton>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Editar" ItemStyle-HorizontalAlign="Center">
                                                            <ItemTemplate>
                                                                <asp:LinkButton ID="btnVisualizar" CausesValidation="false" CommandName="Edit" runat="server">
                                                                    <i class="fa fa-pencil" aria-hidden="true" title="Editar" style="font-size:25px;"></i>
                                                                </asp:LinkButton>
                                                            </ItemTemplate>
                                                            <EditItemTemplate>
                                                                <asp:LinkButton ID="btnUpdateVarPacinete" runat="server" CommandName="Update" Text="Actualizar">
                                                                    <i class="fa fa-floppy-o" aria-hidden="true"  title="Actualizar" style="font-size:25px;"></i>
                                                                </asp:LinkButton>
                                                                <asp:LinkButton ID="bntCancelEditPaciente" runat="server" CommandName="Cancel" Text="Cancelar">
                                                                    <i class="fa fa-ban" aria-hidden="true" title="Cancelar" style="font-size:25px;"></i>
                                                                </asp:LinkButton>
                                                            </EditItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Estatus" ItemStyle-HorizontalAlign="Center">
                                                            <ItemTemplate>
                                                                <asp:ImageButton ID="imbEstatus" runat="server" BackColor="Transparent" Height="25px" Width="25px"
                                                                    CommandArgument='<%#Eval("intAdicionalesID") %>' CommandName="Estatus" ToolTip="Cambia el estatus" />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                    </Columns>
                                                    <PagerTemplate>
                                                        <asp:Label ID="lblTemplate" runat="server" Text="Muestra Filas: " CssClass="Label" />
                                                        <asp:DropDownList ID="ddlBandejaAdi" runat="server" AutoPostBack="true" CausesValidation="false"
                                                            Enabled="true" OnSelectedIndexChanged="ddlBandejaAdi_SelectedIndexChanged">
                                                            <asp:ListItem Value="10" />
                                                            <asp:ListItem Value="15" />
                                                            <asp:ListItem Value="20" />
                                                        </asp:DropDownList>
                                                        &nbsp;Página
                                                        <asp:TextBox ID="txtBandejaAdi" runat="server" AutoPostBack="true" OnTextChanged="txtBandejaAdi_TextChanged"
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
                                        </telerik:RadAjaxPanel>
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
        <div class="modal-dialog" style="width: 70%">
            <div class="modal-content">

                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                        <span aria-hidden="true">×</span>
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
                                                            <asp:BoundField DataField="intIndicacionID" HeaderText="ID" ReadOnly="true" ItemStyle-CssClass="hidden-md" HeaderStyle-CssClass="hidden-md" />
                                                            <asp:TemplateField HeaderText="Nombre">
                                                                <ItemTemplate>
                                                                    <asp:Label runat="server" ID="lblNombreUsuario" Text='<%#Eval("vchIndicacion") %>' />
                                                                </ItemTemplate>
                                                                <EditItemTemplate>
                                                                    <asp:TextBox ID="txtname" Width="100%" runat="server" Text='<%#Eval("vchIndicacion") %>' />
                                                                </EditItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Editar" ItemStyle-HorizontalAlign="Center">
                                                                <ItemTemplate>
                                                                    <asp:LinkButton ID="btnVisualizar" CausesValidation="false" CommandName="Edit" runat="server">
                                                                        <i class="fa fa-pencil" aria-hidden="true" title="Editar" style="font-size:25px;"></i>
                                                                    </asp:LinkButton>
                                                                </ItemTemplate>
                                                                <EditItemTemplate>
                                                                    <asp:LinkButton ID="btnUpdateVarPacinete" runat="server" CommandName="Update" Text="Actualizar">
                                                                        <i class="fa fa-floppy-o" aria-hidden="true"  title="Actualizar" style="font-size:25px;"></i>
                                                                    </asp:LinkButton>
                                                                    <asp:LinkButton ID="bntCancelEditPaciente" runat="server" CommandName="Cancel" Text="Cancelar">
                                                                        <i class="fa fa-ban" aria-hidden="true" title="Cancelar" style="font-size:25px;"></i>
                                                                    </asp:LinkButton>
                                                                </EditItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Estatus" ItemStyle-HorizontalAlign="Center">
                                                                <ItemTemplate>
                                                                    <asp:ImageButton ID="imbEstatus" runat="server" BackColor="Transparent" Height="25px" Width="25px"
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
        <div class="modal-dialog" style="width: 70%">
            <div class="modal-content">

                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                        <span aria-hidden="true">×</span>
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
                                                            <asp:BoundField DataField="intDETCuestionarioID" HeaderText="ID" ReadOnly="true" ItemStyle-CssClass="hidden-md" HeaderStyle-CssClass="hidden-md" />
                                                            <asp:TemplateField HeaderText="Nombre">
                                                                <ItemTemplate>
                                                                    <asp:Label runat="server" ID="lblNombreUsuario" Text='<%#Eval("vchCuestionario") %>' />
                                                                </ItemTemplate>
                                                                <EditItemTemplate>
                                                                    <asp:TextBox ID="txtname" Width="100%" runat="server" Text='<%#Eval("vchCuestionario") %>' />
                                                                </EditItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Editar" ItemStyle-HorizontalAlign="Center">
                                                                <ItemTemplate>
                                                                    <asp:LinkButton ID="btnVisualizar" CausesValidation="false" CommandName="Edit" runat="server">
                                                                            <i class="fa fa-pencil" aria-hidden="true" title="Editar" style="font-size:25px;"></i>
                                                                    </asp:LinkButton>
                                                                </ItemTemplate>
                                                                <EditItemTemplate>
                                                                    <asp:LinkButton ID="btnUpdateVarPacinete" runat="server" CommandName="Update" Text="Actualizar">
                                                                            <i class="fa fa-floppy-o" aria-hidden="true"  title="Actualizar" style="font-size:25px;"></i>
                                                                    </asp:LinkButton>
                                                                    <asp:LinkButton ID="bntCancelEditPaciente" runat="server" CommandName="Cancel" Text="Cancelar">
                                                                            <i class="fa fa-ban" aria-hidden="true" title="Cancelar" style="font-size:25px;"></i>
                                                                    </asp:LinkButton>
                                                                </EditItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Estatus" ItemStyle-HorizontalAlign="Center">
                                                                <ItemTemplate>
                                                                    <asp:ImageButton ID="imbEstatus" runat="server" BackColor="Transparent" Height="25px" Width="25px"
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
        <div class="modal-dialog" style="width: 70%">
            <div class="modal-content">

                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                        <span aria-hidden="true">×</span>
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
                                                            <asp:BoundField DataField="intReestriccionID" HeaderText="ID" ReadOnly="true" ItemStyle-CssClass="hidden-md" HeaderStyle-CssClass="hidden-md" />
                                                            <asp:TemplateField HeaderText="Nombre">
                                                                <ItemTemplate>
                                                                    <asp:Label runat="server" ID="lblNombreUsuario" Text='<%#Eval("vchNombreReestriccion") %>' />
                                                                </ItemTemplate>
                                                                <EditItemTemplate>
                                                                    <asp:TextBox ID="txtname" Width="100%" runat="server" Text='<%#Eval("vchNombreReestriccion") %>' />
                                                                </EditItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Editar" ItemStyle-HorizontalAlign="Center">
                                                                <ItemTemplate>
                                                                    <asp:LinkButton ID="btnVisualizar" CausesValidation="false" CommandName="Edit" runat="server">
                                                                            <i class="fa fa-pencil" aria-hidden="true" title="Editar" style="font-size:25px;"></i>
                                                                    </asp:LinkButton>
                                                                </ItemTemplate>
                                                                <EditItemTemplate>
                                                                    <asp:LinkButton ID="btnUpdateVarPacinete" runat="server" CommandName="Update" Text="Actualizar">
                                                                            <i class="fa fa-floppy-o" aria-hidden="true"  title="Actualizar" style="font-size:25px;"></i>
                                                                    </asp:LinkButton>
                                                                    <asp:LinkButton ID="bntCancelEditPaciente" runat="server" CommandName="Cancel" Text="Cancelar">
                                                                            <i class="fa fa-ban" aria-hidden="true" title="Cancelar" style="font-size:25px;"></i>
                                                                    </asp:LinkButton>
                                                                </EditItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Estatus" ItemStyle-HorizontalAlign="Center">
                                                                <ItemTemplate>
                                                                    <asp:ImageButton ID="imbEstatus" runat="server" BackColor="Transparent" Height="25px" Width="25px"
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

    <!-- modals -->
    <div class="modal fade bs-example-modal-sm" id="mdlModalidades" tabindex="-1" role="dialog" aria-hidden="true">
        <div class="modal-dialog" style="width:50%">
            <div class="modal-content">

            <div class="modal-header">
                <button type="button" class="close" data-dismiss="modal" aria-label="Close"><span aria-hidden="true">×</span>
                </button>
                <h4 class="modal-title" id="mdlEstudiosLabel">Modalidades del Técnico</h4>
            </div>
            <div class="modal-body">
                <div class="row form-group">
                    <div class="col-lg-12 col-md-12 col-sm-12">
                        <asp:UpdatePanel runat="server">
                            <ContentTemplate>
                                <asp:Label ID="lblTecnicoID" runat="server" Text="" ForeColor="DarkGreen" Visible="false" Font-Bold="true" ></asp:Label>
                                <asp:Label ID="lblTecnico" runat="server" Text="" ForeColor="DarkGreen" Font-Bold="true" ></asp:Label>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </div>
                </div>
                <div class="row form-group">                    
                    <div class="col-lg-9 col-md-12 col-sm-12">
                        <div class="row">
                            <div class="col-lg-4 col-md-12 col-sm-12">
                                <asp:Label runat="server" Text="Modalidades:" ForeColor="DarkGreen" ></asp:Label>
                            </div>
                            <div class="col-lg-8 col-md-12 col-sm-12">
                                <telerik:RadComboBox runat="server" ID="ddlModalidadTecnico" RenderMode="Lightweight" Width="100%" ForeColor="DarkGreen" OnClientSelectedIndexChanged="comboTecnico_Mod"></telerik:RadComboBox>
                            </div>
                        </div>                        
                    </div>
                    <div class="col-lg-3 col-md-12 col-sm-12">
                        <asp:LinkButton runat="server" ID="btnAddMod" OnClick="btnAddMod_Click" CssClass="btn btn-success" Text="Agregar" ToolTip="Agregar Modalidad">
                            <span aria-hidden="true" class="fa fa-plus"></span>Agregar
                        </asp:LinkButton>
                    </div>
                </div>
                <div class="row form-group">
                    <div class="col-lg-12 col-md-12 col-sm-12">
                        <telerik:RadAjaxPanel runat="server" ID="ajaxPanelTecnico" OnAjaxRequest="ajaxPanelTecnico_AjaxRequest">
                            <asp:GridView ID="grvModalidadTecnico" runat="server" AllowPaging="true" CssClass="table table-striped table-bordered"
                                PageSize="10" AutoGenerateColumns="false" OnRowDataBound="grvModalidadTecnico_RowDataBound" Font-Size="10px"
                                OnPageIndexChanging="grvModalidadTecnico_PageIndexChanging" DataKeyNames="intRELModTecnicoID" 
                                OnRowCommand="grvModalidadTecnico_RowCommand" 
                                EmptyDataText="No hay resultado bajo el criterio de búsqueda.">
                                <Columns>
                                    <asp:TemplateField HeaderText="Nombre">
                                        <ItemTemplate>
                                            <asp:Label runat="server" ID="lblNombreModalidadTec" Text='<%#Eval("vchModalidad") %>' />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Eliminar" ItemStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                            <asp:ImageButton ID="imbEstatus" runat="server" BackColor="Transparent" Height="25px" Width="25px"
                                                CommandArgument='<%#Eval("intRELModTecnicoID") %>' CommandName="Estatus" ToolTip="Cambia el estatus del Sitio" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                                <PagerTemplate>
                                    <asp:Label ID="lblTemplate" runat="server" Text="Muestra Filas: " CssClass="Label" />
                                    <asp:DropDownList ID="ddlBandejaMT" runat="server" AutoPostBack="true" CausesValidation="false"
                                        Enabled="true" OnSelectedIndexChanged="ddlBandejaMT_SelectedIndexChanged">
                                        <asp:ListItem Value="10" />
                                        <asp:ListItem Value="15" />
                                        <asp:ListItem Value="20" />
                                    </asp:DropDownList>
                                    &nbsp;Página
                                        <asp:TextBox ID="txtBandejaMT" runat="server" AutoPostBack="true" OnTextChanged="txtBandejaMT_TextChanged"
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
            <div class="modal-footer">
                <asp:Button runat="server" ID="btnCancelMod" class="btn btn-default" Text="Cerrar" data-dismiss="modal"></asp:Button>
            </div>

            </div>
        </div>
    </div>
    <!-- /modals -->

    <!-- modals -->
    <div class="modal fade bs-example-modal-sm" id="mdlActivos" tabindex="-1" role="dialog" aria-hidden="true">
        <div class="modal-dialog" style="width:50%">
            <div class="modal-content">
                <asp:UpdatePanel runat="server">
                    <ContentTemplate>
                        <div class="modal-header">
                            <button type="button" class="close" data-dismiss="modal" aria-label="Close"><span aria-hidden="true">×</span>
                            </button>
                            <h4 class="modal-title" id="mdlActivosLabel"><asp:Label runat="server" ID="lblAdicionalItem" Text="" ForeColor="DarkGreen"></asp:Label><small>  Activo para: </small></h4>
                            <asp:Label runat="server" ID="lblintAdicionalID" Text="" Visible="false"></asp:Label>
                        </div>
                        <div class="modal-body">
                            <telerik:RadComboBox RenderMode="Lightweight" ID="radComboGeneroItem" runat="server" CheckBoxes="true" Width="100%" EnableCheckAllItemsCheckBox="true" Label="Genero:">
                                <Localization AllItemsCheckedString="Todos..." ItemsCheckedString="Todos..." CheckAllString="Todos..."/>
                                <Items>
                                    <telerik:RadComboBoxItem Text="Hombre" Value="1"/>
                                    <telerik:RadComboBoxItem Text="Mujer" Value="2" />
                                </Items>
                            </telerik:RadComboBox>
                            <telerik:RadComboBox RenderMode="Lightweight" ID="radComboEdadItem" runat="server" CheckBoxes="true" Width="100%" EnableCheckAllItemsCheckBox="true" Label="Edad:">
                                <Localization AllItemsCheckedString="Todos..." ItemsCheckedString="Todos..." CheckAllString="Todos..."/>
                                <Items>
                                    <telerik:RadComboBoxItem Text="Adulto Mayor" Value="3"/>
                                    <telerik:RadComboBoxItem Text="Menor" Value="4" />
                                </Items>
                            </telerik:RadComboBox>
                        </div>
                    </ContentTemplate>
                </asp:UpdatePanel>
                <div class="modal-footer">
                    <asp:Button runat="server" ID="btnCerrar" class="btn btn-default" Text="Cerrar" data-dismiss="modal"></asp:Button>
                    <asp:Button runat="server" ID="btnGuardarActivos" class="btn btn-success" Text="Guardar" OnClick="btnGuardarActivos_Click"></asp:Button>
                </div>
            </div>
        </div>
    </div>
    <!-- /modals -->

    <script type="text/javascript">
        $(function () {
            var tabName = $("[id*=TabName]").val() != "" ? $("[id*=TabName]").val() : "SitiosConfig";
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
    <telerik:RadScriptBlock runat="server">
        <script type="text/javascript">
            function Carga_Catalogo_sitio(sender, eventArgs) {
                //var evento = eventArgs.Command.get_name();
                var idsitio = sender._value;
                $find('<%= ajxPanelAdminCat.ClientID%>').ajaxRequestWithTarget('<%= ajxPanelAdminCat.UniqueID %>', idsitio);
            }

            function Carga_Catalogo(sender, eventArgs) {
                //var evento = eventArgs.Command.get_name();
                var idsitio = sender._value;
                $find('<%= ajxPanelAdminCat.ClientID%>').ajaxRequestWithTarget('<%= ajxPanelAdminCat.UniqueID %>', idsitio);
            }

            function Combo_Modalidad(sender, eventArgs) {
                //var evento = eventArgs.Command.get_name();
                var idsitio = sender._value;
                $find('<%= radAjaxPanel2.ClientID%>').ajaxRequestWithTarget('<%= radAjaxPanel2.UniqueID %>', 'modalidad');
            }

            function Combo_SitioMod(sender, eventArgs) {
                //var evento = eventArgs.Command.get_name();
                var idsitio = sender._value;
                $find('<%= radAjaxPanel2.ClientID%>').ajaxRequestWithTarget('<%= radAjaxPanel2.UniqueID %>', 'sitio');
            }

            function ComboMod_Equipo(sender, eventArgs) {
                //var evento = eventArgs.Command.get_name();
                var idsitio = sender._value;
                $find('<%= AjaxPanelEquipo.ClientID%>').ajaxRequestWithTarget('<%= AjaxPanelEquipo.UniqueID %>', idsitio);
            }

            function Combo_SitioModEquipo(sender, eventArgs) {
                //var evento = eventArgs.Command.get_name();
                var idsitio = sender._value;
                $find('<%= AjaxPanelEquipo.ClientID%>').ajaxRequestWithTarget('<%= AjaxPanelEquipo.UniqueID %>', 'sitio');
            }

            function ComboTipoVar(sender, eventArgs) {
                //var evento = eventArgs.Command.get_name();
                var idsitio = sender._value;
                $find('<%= radAjaxPanelAdicionales.ClientID%>').ajaxRequestWithTarget('<%= radAjaxPanelAdicionales.UniqueID %>', idsitio);
            }

            function Combo_SitioAdicional(sender, eventArgs) {
                //var evento = eventArgs.Command.get_name();
                var idsitio = sender._value;
                $find('<%= radAjaxPanelAdicionales.ClientID%>').ajaxRequestWithTarget('<%= radAjaxPanelAdicionales.UniqueID %>', idsitio);
            }

            function ddlSitioModEquipo_SelectedIndexChanged(sender, eventArgs) {
                $find('<%= AjaxPanelModalidadEquipo.ClientID%>').ajaxRequestWithTarget('<%= AjaxPanelModalidadEquipo.UniqueID %>', '');
            }

            function comboTecnico_Mod(sender, eventArgs) {
                $find('<%= ajaxPanelTecnico.ClientID%>').ajaxRequestWithTarget('<%= ajaxPanelTecnico.UniqueID %>', '');
            }
        </script>
    </telerik:RadScriptBlock>
</asp:Content>
