<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="frmAdminCatalogo.aspx.cs" Inherits="Fuji.RISLite.Site.frmAdminCatalogo" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script src="assets/jquery/dist/jquery.min.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div>
        <div class="page-header">
            <div class="messagealert" id="alert_container"></div>
            <h1>
            Administrador de Catálogos
            </h1>
        </div><!-- /.page-header -->
        <div class="row">
            <div class="col-xs-12">
                <div class="col-sm-7">
					<div class="widget-box">
                        <div class="widget-header widget-header-flat widget-header-small">
							<h5 class="widget-title">
								<i class="ace-icon fa fa-tags"></i>
								Catálogos
							</h5>
                        </div>
                        <div class="widget-body">
                            <div class="widget-main">
                                <div class="row">
                                    <div class="col-lg-4 col-sm-2">

                                    </div>
                                    <div class="col-lg-8 col-sm-10">
                                        <asp:DropDownList ID="ddlListCatalogo" runat="server" CssClass="form-control"  ToolTip="Catálogo"
                                        AutoPostBack="true" OnSelectedIndexChanged="ddlListCatalogo_SelectedIndexChanged"></asp:DropDownList>
                                    </div>
                                </div>
                                <br />
                                <div class="row">
                                    <asp:UpdatePanel runat="server">
                                        <ContentTemplate>
                                            <asp:Panel runat="server">
                                                <asp:GridView ID="grvCatalogo" runat="server" AllowPaging="true" CssClass="table table-striped table-bordered"
                                                    PageSize="10" AutoGenerateColumns="false" OnRowDataBound="grvCatalogo_RowDataBound" Font-Size="10px"
                                                    OnPageIndexChanging="grvCatalogo_PageIndexChanging" DataKeyNames="vchCatalogoID"
                                                    OnRowCommand="grvCatalogo_RowCommand"
                                                    EmptyDataText="No hay resultado bajo el criterio de búsqueda.">
                                                    <Columns>
                                                        <asp:BoundField DataField="vchCatalogoID" HeaderText="ID" ReadOnly="true" ItemStyle-CssClass="hidden-md" HeaderStyle-CssClass="hidden-md"/>
                                                        <asp:BoundField DataField="vchCatalogo"  HeaderText="Nombre" ReadOnly="true" />
                                                        <asp:TemplateField HeaderText="Editar" ItemStyle-HorizontalAlign="Center">
                                                            <ItemTemplate>      
                                                                <asp:LinkButton ID="btnVisualizar" CausesValidation="false" CommandName="viewEditar" CommandArgument='<%# Bind("vchCatalogoID") %>' runat="server">
                                                                    <asp:Image ID="ImageVisializa" runat="server" ImageUrl="~/images/ic_action_edit.png" Height="25px" Width="25px" ToolTip="Editar"/>
                                                                </asp:LinkButton>
                                                            </ItemTemplate>
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
                <div class="col-sm-5">
					<div class="widget-box">
                        <div class="widget-header widget-header-flat widget-header-small">
							<h5 class="widget-title">
								<i class="ace-icon fa fa-caret-square-o-left"></i>
								Agregar/Editar
							</h5>
                        </div>
                        <div class="widget-body">
                            <div class="widget-main">
                                <div>
                                    <asp:UpdatePanel runat="server">
                                        <ContentTemplate>
                                            <asp:Panel runat="server">
                                                <div class="row">
                                                    <div class="col-lg-12 text-center">
                                                        <asp:Label runat="server" ID="lblCatalogo" Text=""></asp:Label>
                                                    </div>
                                                </div>
                                                <div class="row">
                                                    <div class="col-lg-12 col-md-12 col-sm-12 text-left">
                                                        <asp:Label runat="server" Text="ID" ID="lblPrimary" Font-Bold="true" ></asp:Label>
                                                    </div>
                                                </div>
                                                <div class="row">
                                                    <div class="col-lg-4 col-md-4 col-sm-4 text-right">
                                                        <asp:RequiredFieldValidator Text="Campo requerido" runat="server" ID="rfvValueItem" ErrorMessage="Campo Requerido" ForeColor="Red" ControlToValidate="txtValorCatalogo" ValidationGroup="vgAddItemCat"></asp:RequiredFieldValidator>
                                                        <asp:Label runat="server" Text="Valor" Font-Bold="true" ></asp:Label>
                                                    </div>
                                                    <div class="col-lg-8 col-md-8 col-sm-8">
                                                        <asp:TextBox ID="txtValorCatalogo" Width="100%" runat="server" Text="" CssClass="form-control"></asp:TextBox>
                                                    </div>
                                                </div>
                                                <hr />
                                                <div class="row">
                                                    <div class="col-lg-12 col-md-12 col-sm-12 text-right">
                                                        <asp:Button runat="server" ID="btnCancelCat" Text="Cancelar" CssClass="btn btn-danger" OnClick="btnCancelCat_Click" />
                                                        <asp:Button runat="server" ID="btnAddItemCat" Text="Agregar/actualizar" CssClass="btn btn-success" ValidationGroup="vgAddItemCat" OnClick="btnAddItemCat_Click" />
                                                    </div>
                                                </div>
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
