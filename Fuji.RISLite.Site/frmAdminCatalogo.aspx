<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="frmAdminCatalogo.aspx.cs" Inherits="Fuji.RISLite.Site.frmAdminCatalogo" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div>
        <div class="page-header">
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
                                <div class="row">
                                    <asp:UpdatePanel runat="server">
                                        <ContentTemplate>
                                            <asp:Panel runat="server">
                                                <asp:GridView ID="grvCatalogo" runat="server" AllowPaging="true" CssClass="table table-striped table-bordered"
                                                PageSize="10" AutoGenerateColumns="false" OnRowDataBound="grvCatalogo_RowDataBound" Font-Size="10px"
                                                OnPageIndexChanging="grvCatalogo_PageIndexChanging" DataKeyNames="idCatalogo"
                                                OnRowCommand="grvCatalogo_RowCommand"
                                                EmptyDataText="No hay resultado bajo el criterio de búsqueda.">
                                                <Columns>
                                                    <asp:BoundField DataField="idCatalogo" HeaderText="ID" ReadOnly="true" ItemStyle-CssClass="hidden-md" HeaderStyle-CssClass="hidden-md"/>
                                                    <asp:BoundField DataField="vchNombre"  HeaderText="Nombre" ReadOnly="true" />
                                                    <asp:BoundField DataField="vchValor" HeaderText="Valor" ReadOnly="true" HeaderStyle-CssClass="visible-lg" ItemStyle-CssClass="visible-lg" />
                                                    <asp:TemplateField HeaderText="Editar">
                                                        <ItemTemplate>      
                                                            <asp:LinkButton ID="btnVisualizar" CausesValidation="false" CommandName="viewEditar" CommandArgument='<%# Bind("idCatalogo") %>' runat="server">
                                                                <asp:Image ID="ImageVisializa" runat="server" ImageUrl="~/images/ic_action_edit.png" Height="25px" Width="25px" ToolTip="Editar"/>
                                                            </asp:LinkButton>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Estatus">
                                                        <ItemTemplate>
                                                            <asp:LinkButton CommandArgument='<%# Bind("idCatalogo") %>' CommandName="Estatus" runat="server" ID="lblEstatus"></asp:LinkButton>
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
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
