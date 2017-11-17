<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="frmListaTrabajo.aspx.cs" Inherits="Fuji.RISLite.Site.frmListaTrabajo" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="page-content">
        <div class="page-header">
            <h1>Lista de Trabajo
                <small>
                    <i class="ace-icon fa fa-angle-double-right"></i>
                    Estudios próximos
                </small>
            </h1>
        </div>
        <!-- /.page-header -->
        <div class="row">
            <div class="col-xs-12">
                <asp:GridView ID="GV_ListaTrabajo" runat="server" AllowPaging="true" CssClass="table table-striped table-bordered"
                    PageSize="20" AutoGenerateColumns="false" OnRowDataBound="GV_ListaTrabajo_RowDataBound" Font-Size="10px"
                    OnPageIndexChanging="GV_ListaTrabajo_PageIndexChanging"
                    OnRowCommand="GV_ListaTrabajo_RowCommand1"
                    EmptyDataText="No hay resultado bajo el criterio de búsqueda.">
                    <Columns>

                        <asp:BoundField HeaderStyle-CssClass="center" ItemStyle-CssClass="center" DataField="intEstudioID" HeaderText="ID" ItemStyle-ForeColor="DarkGreen" />
                        <asp:BoundField HeaderStyle-CssClass="center" ItemStyle-CssClass="center" DataField="vchNombre" HeaderText="Nombre" ItemStyle-ForeColor="DarkGreen" />
                        <asp:BoundField HeaderStyle-CssClass="center" ItemStyle-CssClass="center" DataField="vchModalidad" HeaderText="Modalidad" ItemStyle-ForeColor="DarkGreen" />
                        <asp:BoundField HeaderStyle-CssClass="center" ItemStyle-CssClass="center" DataField="vchPrestacion" HeaderText="Estudio" ItemStyle-ForeColor="DarkGreen" />
                        <asp:BoundField HeaderStyle-CssClass="center" ItemStyle-CssClass="center" DataField="datFechaInicio" DataFormatString="{0:dd-MM-yyyy hh:mm tt}" HeaderText="Fecha" ItemStyle-ForeColor="DarkGreen" />
                        <asp:BoundField HeaderStyle-CssClass="center" ItemStyle-CssClass="center" DataField="vchEstatus" HeaderText="Estatus" ItemStyle-ForeColor="DarkGreen" />
                        <asp:TemplateField HeaderText="Adicionales" HeaderStyle-CssClass="center" ItemStyle-HorizontalAlign="Center">
                            <ItemTemplate>
                                <asp:LinkButton ID="btn1" CausesValidation="false" CommandName="Adicional" runat="server" Text="Adicionales" CommandArgument='<%#Eval("intCitaID") %>'>
                                 <i class="fa fa-info-circle" aria-hidden="true" title="Tomar" style="font-size:25px;"></i>
                                </asp:LinkButton>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Tomar" HeaderStyle-CssClass="center" ItemStyle-HorizontalAlign="Center">
                            <ItemTemplate>
                                <asp:LinkButton ID="btn1" CausesValidation="false" CommandName="Tomar" runat="server" Text="Tomar" CommandArgument='<%#Eval("intEstudioID") %>'>
                                 <i class="fa fa-arrow-circle-up" aria-hidden="true" title="Tomar" style="font-size:25px;"></i>
                                </asp:LinkButton>
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Finalizar" HeaderStyle-CssClass="center" ItemStyle-HorizontalAlign="Center">
                            <ItemTemplate>
                                <asp:LinkButton ID="btn2" CausesValidation="false" CommandName="Finalizar" runat="server" Text="Finalizar" CommandArgument='<%#Eval("intEstudioID") %>'>
                                 <i class="fa fa-arrow-circle-down" aria-hidden="true" title="Finalizar" style="font-size:25px;"></i>
                                </asp:LinkButton>
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Cancelar" HeaderStyle-CssClass="center" ItemStyle-HorizontalAlign="Center">
                            <ItemTemplate>
                                <asp:LinkButton ID="btn3" CausesValidation="false" CommandName="Cancelar" runat="server" Text="Cancelar" CommandArgument='<%#Eval("intEstudioID") %>'>
                                 <i class="fa fa-times-circle" aria-hidden="true" title="Cancelar" style="font-size:25px;" ></i>
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
                            ToolTip="Página Anterior" CommandArgument="Prev" CssClass="previous" />
                        <asp:Button ID="btnBandeja_II" runat="server" CommandName="Page" CausesValidation="false"
                            ToolTip="Página Siguiente" CommandArgument="Next" CssClass="next" />
                    </PagerTemplate>
                    <HeaderStyle CssClass="headerstyle" />
                    <FooterStyle CssClass="text-center" />
                    <PagerStyle CssClass="text-center" />
                </asp:GridView>
            </div>
            <!-- /.col -->
        </div>
        <!-- /.row -->
    </div>
    <!-- /.page-content -->
    <!-- page specific plugin scripts -->

    <!-- modals -->
    <div class="modal fade bs-example-modal-sm" id="mdlCita" tabindex="-1" role="dialog" aria-hidden="true">
        <div class="modal-dialog" style="width:70%">
            <div class="modal-content">

            <div class="modal-header">
                <button type="button" class="close" data-dismiss="modal" aria-label="Close"><span aria-hidden="true">×</span>
                </button>
                <h4 class="modal-title" id="mdlEstudiosLabel">Adicionales de la cita</h4>
            </div>
            <div class="modal-body">
                <div class="row">
                    <div class="col-lg-6 col-md-12 col-sm-12">
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
                    <div class="col-lg-6 col-md-12 col-sm-12">
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
            <div class="modal-footer">
                <asp:Button runat="server" ID="btnCancelEstudios" class="btn btn-default" Text="Cerrar" OnClick="btnCancelEstudios_Click"></asp:Button>
            </div>

            </div>
        </div>
    </div>
    <!-- /modals -->
</asp:Content>
