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
                        <asp:TemplateField HeaderText="Fecha" HeaderStyle-CssClass="center" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center">
                            <ItemTemplate>
                                <asp:Label ID="lblFecha_Inicio" runat="server" Text='<%# String.Format("{0:dd-MM-yyyy hh:mm tt}", Eval("datFechaInicio")) %>' ForeColor="DarkGreen" />
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:BoundField HeaderStyle-CssClass="center" ItemStyle-CssClass="center" DataField="vchEstatus" HeaderText="Estatus" ItemStyle-ForeColor="DarkGreen" />

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

                </asp:GridView>


            </div>
            <!-- /.col -->
        </div>
        <!-- /.row -->
    </div>
    <!-- /.page-content -->
    <!-- page specific plugin scripts -->
</asp:Content>
