<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="frmCitas.aspx.cs" Inherits="Fuji.RISLite.Site.frmCitas" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <telerik:RadAjaxManager ID="RadAjaxManager1" runat="server">
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="ddlModalidadBuesqueda">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grvCitas" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="btnBuscarCita">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grvCitas" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="grvCitas">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grvCitas" />
                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>
    </telerik:RadAjaxManager>

    <div class="page-content">
        <div class="page-header">
            <div class="messagealert" id="alert_container"></div>
            <h1>Agenda
			    <small>
                    <i class="ace-icon fa fa-angle-double-right"></i>
                    Citas
                </small>
            </h1>
        </div>
        <!-- /.page-header -->
        <div class="row">
            <div class="col-lg-12 col-md-12 col-sm-12">
                <div class="row">
                    <div class="col-lg-12 col-md-12 col-sm-12">
                        <div class="row">
                            <div class="col-lg-6 col-md-12 col-sm-12">
                                <asp:Label runat="server" ID="lblNombreBus" Text="Nombre del Paciente" AssociatedControlID="txtNombreBus"></asp:Label>
                                <asp:TextBox runat="server" ID="txtNombreBus" Text="" CssClass="form-control"></asp:TextBox>
                            </div>
                            <div class="col-lg-6 col-md-12 col-sm-12">
                                <asp:Label Text="Modalidad" ID="lblModalidad" runat="server"  AssociatedControlID="ddlModalidadBuesqueda"/>
                                <telerik:RadComboBox runat="server" ID="ddlModalidadBuesqueda" RenderMode="Lightweight" ForeColor="DarkGreen" Width="100%" OnClientSelectedIndexChanged="combo_modalidad"></telerik:RadComboBox>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-lg-12 col-md-12 col-sm-12">
                                <table style="width:100%">
                                    <tr>
                                        <td style="width:40%">
                                            <asp:TextBox runat="server" ID="Date1" autocomplete="off" CssClass="form-control" Width="100%" Enabled="false" Font-Size="Small"/>
                                        </td>
                                        <td style="width:10%">
                                            <asp:ImageButton ID="imgPopup" ImageUrl="~/Images/ic_action_calendar_month.png"  Width="25px" Height="25px" ImageAlign="Bottom" runat="server" />
                                            <ajaxToolkit:CalendarExtender ID="customCalendarExtender" runat="server" TargetControlID="Date1" PopupButtonID="imgPopup"
                                            CssClass="cal" Format="dd/MM/yyyy" />
                                        </td>
                                        <td style="width:40%">
                                            <asp:TextBox runat="server" ID="Date2" autocomplete="off" CssClass="form-control" Width="100%" Enabled="false" Font-Size="Small"/>
                                        </td>
                                        <td style="width:10%">
                                            <asp:ImageButton ID="imgPopup2" ImageUrl="~/Images/ic_action_calendar_month.png" Width="25px" Height="25px" ImageAlign="Bottom" runat="server" />
                                            <ajaxToolkit:CalendarExtender ID="customCalendarExtender2" runat="server" TargetControlID="Date2" PopupButtonID="imgPopup2"
                                            CssClass="cal" Format="dd/MM/yyyy" />
                                        </td>
                                    </tr>
                                </table>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="row">
                    <div class="col-lg-12 col-md-12 col-sm-12">
                        <asp:Button runat="server" ID="btnBuscarCita" OnClick="btnBuscarCita_Click" Text="Buscar" CssClass="btn btn-success" />
                    </div>
                </div>
            </div>
        </div>
        <hr />
        <div class="row">
            <telerik:RadAjaxPanel runat="server" ID="ajaxPanelCitas" OnAjaxRequest="ajaxPanelCitas_AjaxRequest">
                <asp:GridView ID="grvCitas" runat="server" AllowPaging="true" CssClass="table table-striped table-bordered"
                    PageSize="20" AutoGenerateColumns="false" OnRowDataBound="grvCitas_RowDataBound" Font-Size="10px"
                    OnPageIndexChanging="grvCitas_PageIndexChanging" DataKeyNames="intEstudioID" OnRowCancelingEdit="grvCitas_RowCancelingEdit"
                    OnRowCommand="grvCitas_RowCommand" OnRowEditing="grvCitas_RowEditing" OnRowUpdating="grvCitas_RowUpdating"
                    EmptyDataText="No hay resultado bajo el criterio de búsqueda.">
                    <Columns>
                        <asp:BoundField DataField="intEstudioID" HeaderText="ID" ReadOnly="true" ItemStyle-CssClass="hidden-md" HeaderStyle-CssClass="hidden-md" />
                        <asp:boundfield datafield="vchNombre" HeaderText="Nombre"  ReadOnly="true"/>
                        <asp:BoundField DataField="datFechaInicio" HeaderText="Fecha de Cita"  DataFormatString="{0:dd-MM-yyyy hh:mm tt}"  />
                        <asp:boundfield datafield="vchModalidad" HeaderText="Modalidad"  ReadOnly="true"/>
                        <asp:boundfield datafield="vchPrestacion" HeaderText="Prestacion"  ReadOnly="true"/>
                        <asp:boundfield datafield="vchEstatus" HeaderText="Estatus Estudio"  ReadOnly="true"/>
                        <asp:TemplateField HeaderText="Re-enviar Email" ItemStyle-HorizontalAlign="Center">
                            <ItemTemplate>
                                <asp:LinkButton ID="btnReEmail" CausesValidation="false" CommandName="Email" CommandArgument='<%#Eval("intCitaID") %>' runat="server" ToolTip="Re-enviar cita por correo electrónico.">
                                    <i class="fa fa-envelope-o" aria-hidden="true" title="Re-enviar cita por correo electrónico." style="font-size:25px;"></i>
                                </asp:LinkButton>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Imprimir Cita" ItemStyle-HorizontalAlign="Center">
                            <ItemTemplate>
                                <asp:LinkButton ID="btnImprimir" CausesValidation="false" CommandName="Imprimir" CommandArgument='<%#Eval("intCitaID") %>' runat="server" ToolTip="Imprimir Cita">
                                    <i class="fa fa-print" aria-hidden="true" title="Imprimir Cita" style="font-size:25px;"></i>
                                </asp:LinkButton>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Marcar Arribo" ItemStyle-HorizontalAlign="Center">
                            <ItemTemplate>
                                <asp:LinkButton ID="btnArribo" CausesValidation="false" CommandName="Arribo" CommandArgument='<%#Eval("intEstudioID") %>' runat="server" ToolTip="Marcar arribo del paciente a realizar estudio.">
                                    <i class="fa fa-hand-pointer-o" aria-hidden="true" title="Marcar arribo del paciente a realizar estudio." style="font-size:25px;"></i>
                                </asp:LinkButton>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                    <PagerTemplate>
                        <asp:Label ID="lblTemplate" runat="server" Text="Muestra Filas: " CssClass="Label" />
                        <asp:DropDownList ID="ddlBandeja" runat="server" AutoPostBack="true" CausesValidation="false"
                            Enabled="true" OnSelectedIndexChanged="ddlBandeja_SelectedIndexChanged">
                            <asp:ListItem Value="20" />
                            <asp:ListItem Value="25" />
                            <asp:ListItem Value="30" />
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
            </telerik:RadAjaxPanel>
        </div>
    </div>

    <telerik:RadScriptBlock runat="server">
        <script type="text/javascript">
            function combo_modalidad(sender, eventArgs) {
                //var evento = eventArgs.Command.get_name();
                var idsitio = sender._value;
                $find('<%= ajaxPanelCitas.ClientID%>').ajaxRequestWithTarget('<%= ajaxPanelCitas.UniqueID %>', idsitio);
            }
        </script>
    </telerik:RadScriptBlock>
</asp:Content>
