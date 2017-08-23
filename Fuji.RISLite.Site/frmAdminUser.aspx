<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="frmAdminUser.aspx.cs" Inherits="Fuji.RISLite.Site.frmAdminUser" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div>
        <div class="page-header">
            <div class="messagealert" id="alert_container"></div>
            <h1>
            Administrador de Usuarios
            </h1>
        </div><!-- /.page-header -->
        <div class="row">
            <div class="col-xs-12">
                <div class="col-sm-7">
					<div class="widget-box">
                        <div class="widget-header widget-header-flat widget-header-small">
							<h5 class="widget-title">
								<i class="ace-icon fa fa-tags"></i>
								Usuarios
							</h5>
                        </div>
                        <div class="widget-body">
                            <div class="widget-main">
                                <div class="row">
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
                                <div class="row">
                                    <div class="col-lg-4 col-sm-12">
                                         <asp:TextBox runat="server" ID="txtNombre" Text="" CssClass="form-control"></asp:TextBox>
                                    </div>
                                    <div class="col-lg-4 col-sm-12">
                                         <asp:TextBox runat="server" ID="txtUsuario" Text="" CssClass="form-control"></asp:TextBox>
                                    </div>
                                    <div class="col-lg-4 col-sm-12">
                                         <asp:DropDownList ID="ddlTipoUsuario" runat="server" CssClass="form-control"></asp:DropDownList>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-lg-12 text-right">
                                        <asp:Button runat="server" ID="btnBuscar" CssClass="btn btn-info" Text="Buscar" OnClick="btnBuscar_Click" />
                                        <asp:Button runat="server" ID="btnAgregar" CssClass="btn btn-success" Text="Agregar" OnClick="btnAgregar_Click" />
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
    </div>
</asp:Content>
