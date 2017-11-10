<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="frmAdminUser.aspx.cs" Inherits="Fuji.RISLite.Site.frmAdminUser" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div>
        <div class="page-header">
            <div class="messagealert" id="alert_container"></div>
            <h1>
            Administrador de Sitios y Usuarios
            </h1>
        </div><!-- /.page-header -->
        <div class="row">
            <div class="col-lg-5 col-md-12 col-sm-12 col-xs-12">
				<div class="widget-box">
                    <div class="widget-header widget-header-flat widget-header-small">
						<h5 class="widget-title">
							<i class="ace-icon fa fa-tags"></i>
							Sitios
						</h5>
                    </div>
                    <div class="widget-body">
                        <div class="widget-main">
                            
                        </div>
                    </div>
                </div>
            </div>
            <div class="col-lg-7 col-md-12 col-sm-12 col-xs-12">
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
                                <div class="col-lg-12">
                                    <table style="width:100%">
                                        <tr>
                                            <td>
                                                <div class="row">
                                                    <div class="col-lg-12">
                                                        <table style="width:100%">
                                                            <tr>
                                                                <td>
                                                                    <asp:Label runat="server" Text="Nombre" ForeColor="DarkGreen"></asp:Label>
                                                                    <asp:RequiredFieldValidator runat="server" ID="rfvNombreUser" ForeColor="Red" ErrorMessage="*" Text="*" 
                                                                    ControlToValidate="txtNombre" ValidationGroup="vgAddUser"></asp:RequiredFieldValidator>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td>
                                                                    <asp:TextBox runat="server" ID="txtNombre" Text="" CssClass="form-control"></asp:TextBox>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </div>
                                                </div>
                                            </td>
                                            <td style="width:5%"></td>
                                            <td>
                                                <div class="row">
                                                    <div class="col-lg-12">
                                                        <table style="width:100%">
                                                            <tr>
                                                                <td>
                                                                    <asp:Label runat="server" Text="Usuario" ForeColor="DarkGreen"></asp:Label>
                                                                    <asp:RequiredFieldValidator runat="server" ID="rfvUsuarioUser" ForeColor="Red" ErrorMessage="*" Text="*"
                                                                        ControlToValidate="txtUsuario" ValidationGroup="vgAddUser"></asp:RequiredFieldValidator>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td>
                                                                    <asp:TextBox runat="server" ID="txtUsuario" Text="" CssClass="form-control"></asp:TextBox>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </div>
                                                </div>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <div class="row">
                                                    <div class="col-lg-12">
                                                        <table style="width:100%">
                                                            <tr>
                                                                <td>
                                                                    <asp:Label runat="server" Text="Email" ForeColor="DarkGreen"></asp:Label>
                                                                    <asp:RequiredFieldValidator runat="server" ID="rfvEmailUser" ForeColor="Red" ErrorMessage="*" Text="*"
                                                                        ControlToValidate="txtEmailUser" ValidationGroup="vgAddUser"></asp:RequiredFieldValidator>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td>
                                                                    <asp:TextBox runat="server" ID="txtEmailUser" Text="" CssClass="form-control"></asp:TextBox>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </div>
                                                </div>
                                            </td>
                                            <td style="width:5%"></td>
                                            <td>
                                                <div class="row">
                                                    <div class="col-lg-12">
                                                        <table style="width:100%">
                                                            <tr>
                                                                <td>
                                                                    <asp:Label runat="server" Text="Tipo de Usuario" ForeColor="DarkGreen"></asp:Label>
                                                                    <asp:RequiredFieldValidator runat="server" ID="rfvTipoUsuario" ForeColor="Red" ErrorMessage="*" Text="*"
                                                                            ControlToValidate="ddlTipoUsuario" ValidationGroup="vgAddUser" InitialValue="0"></asp:RequiredFieldValidator>                                                          
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td>
                                                                    <asp:DropDownList ID="ddlTipoUsuario" runat="server" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlTipoUsuario_SelectedIndexChanged"></asp:DropDownList>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </div>
                                                </div> 
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <div class="row">
                                                    <div class="col-lg-12">
                                                        <table style="width:100%">
                                                            <tr>
                                                                <td>
                                                                    <asp:Label runat="server" Text="Sitio" ForeColor="DarkGreen"></asp:Label>
                                                                    <asp:RequiredFieldValidator runat="server" ID="rfvSitioUser" ForeColor="Red" ErrorMessage="*" Text="*"
                                                                        ControlToValidate="ddlSitioUser" ValidationGroup="vgAddUser" InitialValue="0"></asp:RequiredFieldValidator>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td>
                                                                    <asp:UpdatePanel runat="server">
                                                                        <ContentTemplate>
                                                                            <asp:DropDownList ID="ddlSitioUser" Enabled="false" runat="server" CssClass="form-control"></asp:DropDownList>
                                                                        </ContentTemplate>
                                                                    </asp:UpdatePanel>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </div>
                                                </div> 
                                            </td>
                                            <td style="width:5%"></td>
                                            <td>
                                                <div class="row">
                                                    <div class="col-lg-12 text-right">
                                                        <table style="width:100%">
                                                            <tr>
                                                                <td>
                                                                    <asp:Button runat="server" ID="btnBuscar" CssClass="btn btn-info" Text="Buscar" OnClick="btnBuscar_Click" />
                                                                    <asp:Button runat="server" ID="btnAgregar" CssClass="btn btn-success" Text="Agregar" OnClick="btnAgregar_Click"  ValidationGroup="vgAddUser"/>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </div>
                                                </div> 
                                            </td>
                                        </tr>
                                    </table>
                                </div>
                            </div>
                            <hr />
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
                                                        <asp:Label runat="server" ID="lblNombreUser" Text='<%#Eval("vchNombre") %>' />
                                                    </ItemTemplate>
                                                    <EditItemTemplate>
                                                        <asp:TextBox ID="txtNombreUserItem" width="100%"  runat="server" Text='<%#Eval("vchNombre") %>'/>
                                                    </EditItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Usuario">
                                                    <ItemTemplate>
                                                        <asp:Label runat="server" ID="lblUser" Text='<%#Eval("vchUsuario") %>' />
                                                    </ItemTemplate>
                                                    <EditItemTemplate>
                                                        <asp:TextBox ID="txtUserItem" width="100%"  runat="server" Text='<%#Eval("vchUsuario") %>'/>
                                                    </EditItemTemplate>
                                                </asp:TemplateField>
                                                <asp:BoundField DataField="vchTipoUsuario"  HeaderText="Tipo de Usuario" ReadOnly="true" />
                                                <asp:BoundField DataField="vchSitio"  HeaderText="Sitio" ReadOnly="true" />
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
</asp:Content>
