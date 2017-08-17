<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="frmPaciente.aspx.cs" Inherits="Fuji.RISLite.Site.frmPaciente" Culture="es-MX" UICulture="Auto" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="page-content">
        <div class="page-header">
		    <h1>
			    Pacientes
			    <small>
				    <i class="ace-icon fa fa-angle-double-right"></i>
				    Búsqueda
			    </small>
		    </h1>
	    </div><!-- /.page-header -->
        <div class="row">
            <div class="col-lg-12">
                <div class="row">
                    <div class="col-lg-6">
                        <asp:TextBox Text="" runat="server" ID="txtBusqueda" CssClass="form-search" OnTextChanged="txtBusqueda_TextChanged" AutoPostBack="true"></asp:TextBox>
                    </div>
                    <div class="col-lg-6"></div>
                </div>
            </div>
        </div><!-- /.row -->
        <hr />
        <div class="row">
            <div class="col-lg-12">
                <div class="row">
                    <div class="col-lg-6">
                        <asp:UpdatePanel runat="server" >
                            <ContentTemplate>
                                <asp:Panel runat="server">
                                    <asp:GridView ID="grvPacientes" runat="server" AllowPaging="true" CssClass="table table-striped table-bordered"
                                        PageSize="10" AutoGenerateColumns="false" OnRowDataBound="grvPacientes_RowDataBound" Font-Size="10px"
                                        OnPageIndexChanging="grvPacientes_PageIndexChanging" DataKeyNames="vchCatalogoID"
                                        OnRowCommand="grvPacientes_RowCommand"
                                        EmptyDataText="No hay resultado bajo el criterio de búsqueda.">
                                        <Columns>
                                            <asp:BoundField DataField="vchCatalogoID" HeaderText="ID" ReadOnly="true" ItemStyle-CssClass="hidden-md" HeaderStyle-CssClass="hidden-md"/>
                                            <asp:BoundField DataField="vchCatalogo"  HeaderText="Nombre" ReadOnly="true" />
                                            <asp:BoundField DataField="vchCatalogo"  HeaderText="Apellidos" ReadOnly="true" />
                                            <asp:BoundField DataField="NSS" HeaderText="NSS" ReadOnly="true" />
                                            <asp:TemplateField HeaderText="Detalle">
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
                    <div class="col-lg-6">
                        <div class="row">
                            <div class="col-lg-5 col-md-5 col-sm-12 col-xs-12">
                                <asp:Label runat="server" Text="Nombre"></asp:Label>
                            </div>
                            <div class="col-lg-7 col-md-7 col-sm-12 col-xs-12">
                                <asp:TextBox runat="server" Text="" ID="txtNombrePaciente" Width="100%"></asp:TextBox>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-lg-5 col-md-5 col-sm-12 col-xs-12">
                                <asp:Label runat="server" Text="Apellidos"></asp:Label>
                            </div>
                            <div class="col-lg-7 col-md-7 col-sm-12 col-xs-12">
                                <asp:TextBox runat="server" Text="" ID="txtApellidos" Width="100%"></asp:TextBox>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-lg-5 col-md-5 col-sm-12 col-xs-12">
                                <asp:Label runat="server" Text="Fecha de Nacimiento"></asp:Label>
                            </div>
                            <div class="col-lg-7 col-md-7 col-sm-12 col-xs-12">
                                <table style="width:100%">
                                    <tr>
                                        <td style="width:90%">
                                            <asp:TextBox runat="server" ID="Date1" autocomplete="off" CssClass="form-control" Width="100%" Font-Size="Small"/>
                                        </td>
                                        <td style="width:10%">
                                            <asp:ImageButton ID="imgPopup" ImageUrl="~/Images/ic_action_calendar_month.png"  Width="25px" Height="25px" ImageAlign="Bottom" runat="server" />
                                            <asp:CalendarExtender ID="customCalendarExtender" runat="server" TargetControlID="Date1" PopupButtonID="imgPopup"
                                            CssClass="cal" Format="dd/MM/yyyy" />
                                        </td>
                                    </tr>
                                </table>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div><!-- /.row -->
	</div><!-- /.page-content -->
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
</asp:Content>
